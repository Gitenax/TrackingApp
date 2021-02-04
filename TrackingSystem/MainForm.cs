using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Data.OleDb;
using TrackingSystem.Helpers;
using TrackingSystem.Models;

namespace TrackingSystem
{
  public partial class MainForm : Form
  {
    string dbPath = Application.StartupPath + @"/Data/base.mdb";
		int ID; // Переменная хранящая ID из ячейки строки для обращения к БД
		int ROW_INDEX; // Переменная хранящая Index выделенной строки

		List<string> infoHeadersList = new List<string>();
    List<string> infoHeadersEst = new List<string>();

		#region Работа с формой
    public MainForm()
    {
      InitializeComponent();

      // Инициализация базового класса для работы с БД
      new Base(dbPath);

      DataList_Estimate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      DataList_Trackinglists.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			DataList_Estimate.exSetDoubleBuffered();
			DataList_Trackinglists.exSetDoubleBuffered();
    }
    void MainForm_Load(object sender, EventArgs e)
    {
      LoadAllData();
      foreach (TabPage page in TabMain.TabPages)
      {
        foreach (var grid in page.Controls.OfType<DataGridView>())
        {   
          if (grid.RowCount > 0) grid.CurrentCell = grid.Rows[0].Cells[0]; // Делает первую строку активной
          if (grid.Name.Equals("DataList_Trackinglists"))
          {
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
          }
          else
          {
            grid.Columns[0].Visible = false;
          }
        }
      }

			// CreateNewTabs();
    }

		/// <summary>
		/// Создание новой вкладки TabControl по подразделениям
		/// </summary>
		void CreateNewTabs()
		{
			using (var connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath))
			{
				// Подключение
				connection.Open();
				var command = new OleDbCommand
				{
					Connection = connection,
					CommandText = "SELECT * FROM Organizations WHERE Organization_ratio=1"
				};

				// Исполнение
				var reader = command.ExecuteReader();

				List<object[]> data = new List<object[]>();

				// Чтение данных
				while (reader.Read())
				{
					data.Add(new object[]
					{
						reader.GetValue(0), // ID
						reader.GetValue(1), // Наименование
						reader.GetValue(2)	// Отношение
					});
				}

				for(int i = 0; i < data.Count; i++)
				{
					object[] values = data[i].ToArray(); // Выгружаем массив из листа

					// Создаем вкладку
					TabPage newPage = new TabPage
					{
						Name = "FPage_OrganizationEstimate_" + values[0].ToString(),
						Text = "[ГЕН] " + values[1].ToString()
					};

					// Создаем DataGridView который поместим в созданную вкладку
					DataGridView newGrid = new DataGridView
					{
						Name = "DataList_Organization_" + values[0].ToString(),
						BackgroundColor = Color.Wheat,
						Dock = DockStyle.Fill,
						RowHeadersVisible = false,
						AllowUserToAddRows = false,
						AllowUserToDeleteRows = false,
						AllowUserToResizeRows = false,
						ReadOnly = true,
						SelectionMode = DataGridViewSelectionMode.FullRowSelect,
						AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
						ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
					};
					string QUERY = "SELECT Estimate.ID, Estimate.Estimate_packetnumber, Estimate.Estimate_order, Trackinglists.Trackinglist_number, Trackinglists.Trackinglist_date, Estimate.Estimate_lot, Estimate.Estimate_duration, Cars.Car_number, CarModel.Carmodel_name, CarType.Cartype_capacity, CarType.Cartype_name, CarType.Cartype_code, Estimate.Estimate_inwork_time, Estimate.Estimate_inwork_rate, Estimate.Estimate_inwork_hols, Estimate.Estimate_inwork_price, Estimate.Estimate_inwait_time, Estimate.Estimate_inwait_rate, Estimate.Estimate_inwait_hols, Estimate.Estimate_inwait_price, Estimate.Estimate_finalprice " +
						"FROM(CarType INNER JOIN(((CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand) INNER JOIN Cars ON CarModel.ID = Cars.Car_model) INNER JOIN Trackinglists ON Cars.ID = Trackinglists.Trackinglist_transport) ON CarType.ID = Cars.Car_type) INNER JOIN Estimate ON Trackinglists.ID = Estimate.Estimate_tracklist " +
						$"WHERE Trackinglist_Organization_2={values[0].exToInt()};";

					// Доп. настройка DataGridView
					newGrid.exSetDoubleBuffered();
					newGrid.exFill("Estimate", QUERY);
					newGrid.CellDoubleClick += Grid_CellDoubleClick;
					newGrid.CellClick += Grid_CellClick;
					newGrid.RowsAdded += RowCountChanged;
					newGrid.RowsRemoved += RowCountChanged;
					newGrid.SelectionChanged += RowSelectionChanged;

					// Добавляем компоненты на форму
					newPage.Controls.Add(newGrid);
					TabMain.TabPages.Add(newPage);

					// Переименование заголовков DataGridView
					List<string> lst = Base.ColumnHeaders["DataList_Estimate"];
					for (int x = 0; x < newGrid.ColumnCount; x++)
						newGrid.Columns[x].HeaderText = lst[x];

					// Сокрытие ячеек с идентификатором
					if (newGrid.RowCount > 0)
					{
						newGrid.CurrentCell = newGrid.Rows[0].Cells[0];
						//newGrid.Columns[0].Visible = false;
					}
				}

			}

		}
		#endregion

		#region Управление данными
		// +------------------------------------+
		// |    Синхронизация работы DataGrid   |
		// +====================================+
		// | LoadAllData---------------[  OK  ] |
		// | UpdateData----------------[  OK  ] |
		// | UpdateAllData-------------[  OK  ] |
		// | AddRow--------------------[  OK  ] |
		// | EditRow-------------------[  OK  ] |
		// | RemoveRow-----------------[  OK  ] |
		// | GetCurrentRowInfo---------[  OK  ] |
		// +------------------------------------+

		// Загрузка данных из БД
		void LoadAllData()
    {
			DataList_Estimate.exFill("Estimate", Base.CustomQueries["EstimateFill"]);
			DataList_Trackinglists.exFill("Trackinglists", Base.CustomQueries["TrackinglistFill"]);
			SetColumnHeaders();

      void SetColumnHeaders()
      {
        // [STEP: 1] - Перебирает каждую вкладку в TabControl
        foreach (TabPage p in TabMain.TabPages)
        {
          // [STEP: 2] - Перебирает каждый DataGridView в текущей вкладке(но он всего один)
          foreach (DataGridView grid in p.Controls.OfType<DataGridView>())
          {
            // [STEP: 3] - Получение списка заголовков по ключу(наименование DataGrid)
            grid.CellDoubleClick += Grid_CellDoubleClick;
            grid.CellClick += Grid_CellClick;
            List<string> lst = Base.ColumnHeaders[grid.Name];
            for (int i = 0; i < grid.ColumnCount; i++)
            {
              if (grid.Name.Equals("DataList_Trackinglists"))
              {
                break;
              }
              else
              {
                grid.Columns[i].HeaderText = lst[i];
              }
            }
          }
        }
        // Костыль для заголовков Trackinglist
        for (int i = 1; i < DataList_Trackinglists.ColumnCount - 1; i++)
        {
          List<string> lst = Base.ColumnHeaders[DataList_Trackinglists.Name];
          DataList_Trackinglists.Columns[i + 1].HeaderText = lst[i];
        }
      }
    }
		// Обновление конкретной таблицы
		void UpdateData(int rowIndex = -1, bool delete = false, bool multirow = false)
		{
			DataGridView grid = (DataGridView)TabMain.SelectedTab.Controls[0]; // Выбираем DataGridView в TabPage. NOTE: Controls[0], потому, что DataGridView единственный контрол в TabPage
			switch (TabMain.SelectedTab.Name)
			{
				case "FPage_Estimate": DataList_Estimate.exFill("Estimate", Base.CustomQueries["EstimateFill"]); break;
				case "FPage_Trackinglists": DataList_Trackinglists.exFill("Trackinglists", Base.CustomQueries["TrackinglistFill"]); break;
			}
			#region Выделение последней выделенной строки
			if (rowIndex != -1 && delete == false && multirow == false)
			{
				grid.ClearSelection();
				grid.CurrentCell = grid[2, rowIndex];
				grid.ClearSelection();
				grid[2, rowIndex].Selected = true;
			}
			else if (delete == true && multirow == false)
			{
				if (grid.RowCount > 0)
				{
					int lastRow = grid.Rows.Count;
					if (rowIndex == lastRow)
					{
						grid.ClearSelection();
						grid.CurrentCell = grid[2, grid.Rows.Count - 1];
						grid.ClearSelection();
						grid[2, grid.Rows.Count - 1].Selected = true;
					}
					else
					{
						grid.ClearSelection();
						grid.CurrentCell = grid[2, rowIndex];
						grid.ClearSelection();
						grid[2, rowIndex].Selected = true;
					}
				}
			}
			else if (delete == true && multirow == true)
			{
				if (grid.RowCount > 0)
				{
					grid.ClearSelection();
					grid.CurrentCell = grid[1, 0];
					grid.ClearSelection();
					grid[1, 0].Selected = true;
				}
			}
			else
			{
				if (grid.RowCount > 1)
				{
					grid.ClearSelection();
					grid.CurrentCell = grid[2, grid.Rows.Count - 1];
					grid.ClearSelection();
					grid[2, grid.Rows.Count - 1].Selected = true;
				}
			}
			#endregion
		}
		// Обновление всех столбцов
		void UpdateAllData()
		{
			DataList_Estimate.exFill("Estimate", Base.CustomQueries["EstimateFill"]);
			DataList_Trackinglists.exFill("Trackinglists", Base.CustomQueries["TrackinglistFill"]);
		}
		// Добавление строки
		void AddRow()
    {
			if (new AddEdit(TabMain.SelectedTab.Name, (int)DataCommon.FormType.Add).ShowDialog() == DialogResult.OK) UpdateData();
    }
    // редактирование строки
    void EditRow()
    {
      if (new AddEdit(TabMain.SelectedTab.Name, (int)DataCommon.FormType.Edit, ID).ShowDialog() == DialogResult.OK) UpdateData(ROW_INDEX);
    }
    // Удаление строки
    void RemoveRow()
    {
			var page = TabMain.SelectedTab;
			var grid = (DataGridView)page.Controls[0];
			string table = page.Name.Split('_')[1];

			/* Если выбелено более одной строки */
			if (grid.SelectedRows.Count > 1)
			{
				// Получаем индексы выделенных ячеек
				int[] indexes = new int[grid.SelectedRows.Count];

				for (int i = 0; i < grid.SelectedRows.Count; i++)
					indexes[i] = grid.SelectedRows[i].Cells[0].Value.exToInt();

				if (new Remove(indexes, table, CheckMoreRelationships(table, indexes)).ShowDialog() == DialogResult.OK)
				{
					Base.RemoveRows(table, indexes);
					UpdateData(ROW_INDEX, true, true); // Обновление стобца
				}
			}
			/* Если выделена всего одна строка */
			else
			{
				if (new Remove(CheckRelationships(table, ID)).ShowDialog() == DialogResult.OK)
				{
					try
					{
						switch (page.Name)
						{
							case "FPage_Estimate": Estimate.RemoveRow(ID); break;
							case "FPage_Trackinglists": Trackinglists.RemoveRow(ID); break;
						}
						UpdateData(ROW_INDEX, true); // Обновление стобца
					}
					catch (Exception e)
					{
						MessageBox.Show(e.Message, "Ошибка при удалении", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}

			bool CheckRelationships(string initialTable, int id)
			{
				DataTable checkingTable = new DataTable();
				OleDbDataAdapter adapter;

				// Проверка связанных таблиц
				using (var connection = new OleDbConnection(Base.DB_CONNECTION_STRING))
				{
					connection.Open();
					// Выборка из БД, в зависимости от активной таблицы
					switch (page.Name)
					{
						case "FPage_Trackinglists":
							adapter = new OleDbDataAdapter($"SELECT * FROM Estimate WHERE Estimate_tracklist={id}", connection);
							adapter.Fill(checkingTable);
							break;
					}
				}

				if (checkingTable.Rows.Count != 0)
					return true; // Если есть строки то true
				else
					return false;  // Если нет строк то false
			}

			bool CheckMoreRelationships(string initialTable, int[] values)
			{
				DataTable checkingTable = new DataTable();
				OleDbDataAdapter adapter;

				#region Созданние строки запроса
				string CONDITION = "";
				string CONDITION_COLUMN = "Estimate_tracklist";

				for (int i = 0; i < values.Length; i++)
					CONDITION += CONDITION_COLUMN + "=" + values[i] + " OR ";

				CONDITION = CONDITION.Remove(CONDITION.Length - 4);
				#endregion

				// Проверка связанных таблиц
				using (var connection = new OleDbConnection(Base.DB_CONNECTION_STRING))
				{
					connection.Open();
					// Выборка из БД, в зависимости от активной таблицы
					switch (page.Name)
					{
						case "FPage_Trackinglists":
							adapter = new OleDbDataAdapter($"SELECT * FROM Estimate WHERE {CONDITION}", connection);
							adapter.Fill(checkingTable);
							break;
					}
				}

				if (checkingTable.Rows.Count != 0)
					return true; // Если есть строки то true
				else
					return false; // Если нет строк то false
			}
    }
    // Получение ID строки и краткой информации
    void GetCurrentRowInfo()
    {
			var grid = (DataGridView)TabMain.SelectedTab.Controls[0];

			try
			{
				ID = grid.CurrentRow.Cells[0].Value.exToInt();
				ROW_INDEX = grid.CurrentRow.Index;
			}
			catch { /* Костыльная заглушка */ }

			// Если панель не свернута, то ищем информацию
			if (!FormSplit.Panel2Collapsed)
			{
				LabelTrackingDataE.Text = LabelEstimate.Text = "";

				// Получаем инфу если выделена одна стрка
				if (grid.SelectedRows.Count == 1)
				{
					try
					{
						switch (TabMain.SelectedTab.Name)
						{
							case "FPage_Estimate":
								int listID_E = DataCommon.GetFieldValue(ID, "Estimate_tracklist", "Estimate").exToInt();
								LabelEstimate.Text = GetEstimate(ID);
								LabelTrackingDataE.Text = GetTrackinglist(listID_E);
								break;

							case "FPage_Trackinglists":
								LabelTrackingDataE.Text = GetTrackinglist(ID);
								break;

							default:
								/*string gridId = TabMain.SelectedTab.Name.Split('_')[2];
								var CurrentGrid = (DataGridView)TabMain.SelectedTab.Controls["DataList_Organization_" + gridId];*/

								int listID_DEF = DataCommon.GetFieldValue(ID, "Estimate_tracklist", "Estimate").exToInt();
								LabelEstimate.Text = GetEstimate(ID);
								LabelTrackingDataE.Text = GetTrackinglist(listID_DEF);
								break;
						}
					}
					catch { /* Костыльная заглушка */ }
				}
			}

			#region Внутренние методы
			string GetEstimate(int ID)
			{
				// Возвращаемая строка
				string Result = "";

				// Получение данных из БД
				string[] info = Estimate.GetGeneralData(ID);

				// Преобразование строки
				for (int i = 0; i < info.Length; i++) Result += info[i] + "\n";

				return Result;
			}
			string GetTrackinglist(int ID)
			{
				// Возвращаемая строка
				string Result = "";

				// Получение данных из БД
				string[] info = Trackinglists.GetGeneralData(ID);

				// Преобразование строки
				for (int i = 0; i < info.Length; i++) Result += info[i] + "\n";

				return Result;
			}
			#endregion

		}
		#endregion

		#region События панели ToolStrip
		void toolStrip_Add_Click(object sender, EventArgs e) { AddRow(); }
    void toolStrip_Edit_Click(object sender, EventArgs e) { EditRow(); }
    void toolStrip_Remove_Click(object sender, EventArgs e) { RemoveRow(); }
    void toolStrip_Mark_Click(object sender, EventArgs e){ new DataManage().ShowDialog(); }
    void toolStrip_Report_Click(object sender, EventArgs e){ new Report().ShowDialog(); }
		#endregion

		// +------------------------------------------+
		// |       Синхронизация прочих методов       |
		// +==========================================+
		// | StatusStripRowCountUpdate-------[  OK  ] |
		// | StatusStripRowChangedUpdate-----[  OK  ] |
		// | TabMain_SelectedIndexChanged----[  OK  ] |
		// | Grid_CellClick------------------[  OK  ] |
		// | Grid_CellDoubleClick------------[  OK  ] |
		// | RowCountChanged-----------------[  OK  ] |
		// | RowCountChanged-----------------[  OK  ] |
		// | RowSelectionChanged-------------[  OK  ] |
		// +------------------------------------------+
		
		void StatusStripRowCountUpdate()
    {
      // Общее число строк и выделенная строка
      foreach(DataGridView grid in TabMain.SelectedTab.Controls.OfType<DataGridView>())
        StatusStripLabel_RowCount.Text = grid.RowCount.ToString();
    }
    void StatusStripRowChangedUpdate()
    {
			// Текущая строка
			var grid = (DataGridView)TabMain.SelectedTab.Controls[0];
		
      if (grid.RowCount > 0) try { StatusStripLabel_CurrentRow.Text = (grid.CurrentRow.Index + 1).ToString(); } catch { StatusStripLabel_CurrentRow.Text = "1"; }
			else try { StatusStripLabel_CurrentRow.Text = "0"; } catch { StatusStripLabel_CurrentRow.Text = "0"; }
		}

    void TabMain_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.InvokeEx(StatusStripRowCountUpdate);
      this.InvokeEx(StatusStripRowChangedUpdate);

			GetCurrentRowInfo();

			switch (TabMain.SelectedTab.Name)
      {
        case "FPage_Estimate": panel2.Show(); break;
        case "FPage_Trackinglists": panel2.Hide(); break;
				default: panel2.Show(); break;
			}
    }

		void LabelLinkTracking_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      switch (TabMain.SelectedTab.Name)
      {
        case "FPage_Estimate":
          int nID = Convert.ToInt32(DataCommon.GetFieldValue(ID, "Estimate_tracklist", "Estimate"));
          if (new AddEdit("FPage_TrackingLists", (int)DataCommon.FormType.Edit ,nID).ShowDialog() == DialogResult.OK) UpdateAllData(); break;

        case "FPage_TrackingLists": EditRow(); break;
      }
    }

		#region События при работе с DataGrid
    void Grid_CellClick(object sender, DataGridViewCellEventArgs e) { /* TODO: Nothing */ }
    void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditRow(); }
    // Обновление списка строк/выделенной строки при добавлении и удалении строки
    void RowCountChanged(object sender, DataGridViewRowsAddedEventArgs e){ this.InvokeEx(StatusStripRowCountUpdate); }
    void RowCountChanged(object sender, DataGridViewRowsRemovedEventArgs e){ this.InvokeEx(StatusStripRowCountUpdate); }
    void RowSelectionChanged(object sender, EventArgs e){ this.InvokeEx(StatusStripRowChangedUpdate); this.InvokeEx(GetCurrentRowInfo); }
		#endregion

		#region Кнопки главного меню

		#region [Вид]
		private void MainMenu_View_Click(object sender, EventArgs e)
		{
			var item = ((ToolStripMenuItem)sender);

			switch (item.Name)
			{
				case "MainMenu_View_AdditionalInfo":
					if (item.Checked)
					{
						FormSplit.Panel2Collapsed = true;
						item.Checked = false;
					}
					else
					{
						FormSplit.Panel2Collapsed = false;
						item.Checked = true;
						GetCurrentRowInfo();
					}
					break;
			}
		}
		#endregion
		#region [Настройки]
		private void MainMenu_Settings_Click(object sender, EventArgs e)
		{
			var item = ((ToolStripMenuItem)sender);

			switch (item.Name)
			{
				case "MainMenu_Settings":
					new SettingsForm().ShowDialog();
					break;
			}
		}
		#endregion

		#endregion

	}
}