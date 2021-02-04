using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using TrackingSystem.Helpers;
using TrackingSystem.Models;

namespace TrackingSystem
{
  public partial class DataManage : Form
  {
    int ID; // Переменная хранящая ID из ячейки строки для обращения к БД
		int ROW_INDEX; // Переменная хранящая Index выделенной строки

    #region Работа с формой
    
    // Конструктор
    public DataManage()
    {
      InitializeComponent();

      // Смена типа размера колонок
      DataList_CarBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      DataList_CarModel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      DataList_Cars.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      DataList_CarType.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      DataList_Drivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      DataList_Organizations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      DataList_Staff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      DataList_StaffPosition.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }
    // Загрузка формы
    void DataManage_Load(object sender, EventArgs e)
    {
      LoadAllData();
      
      // Скрытие колонки с ID и указание текущей строки при загрузке формы
      foreach (TabPage page in TabMain.TabPages)
      {
				var grid = (DataGridView)page.Controls[0];

				if (grid.RowCount > 0)
				{
					grid.ClearSelection();
					grid.CurrentCell = grid[1, 0];
					grid.ClearSelection();
					grid[1, 0].Selected = true;
					grid.Columns[0].Visible = false;
				}
				else
				{
					grid.Columns[0].Visible = false;
					SelectedRow.Text = "0";
				}

				// Добавление событий для каждого DataGridView
				grid.RowsAdded += RowCountChanged;
				grid.RowsRemoved += RowCountChanged;
				grid.SelectionChanged += RowSelectionChanged;

      }
      LabelRowCount.Text = DataList_Cars.RowCount.ToString(); // Получение числа строк таблицы
		}

    #endregion

    #region Управление данными

    // Загрузка/Обновление данных из БД
    void LoadAllData()
    {
			DataList_CarBrand.exFill("CarBrand");
			DataList_CarModel.exFill("CarModel", Base.CustomQueries["CarModelFill"]);
			DataList_Cars.exFill("Cars", Base.CustomQueries["CarsFill"]);
			DataList_CarType.exFill("CarType");
			DataList_Drivers.exFill("Drivers");
			DataList_Organizations.exFill("Organizations");
			DataList_Staff.exFill("Staff", Base.CustomQueries["StaffFill"]);
			DataList_StaffPosition.exFill("StaffPosition");
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
            grid.CellDoubleClick += Grid_CellDoubleClick; ;
            grid.CellClick += Grid_CellClick; ;
            List<string> lst = Base.ColumnHeaders[grid.Name];
            for (int i = 0; i < grid.ColumnCount; i++)
            {
              grid.Columns[i].HeaderText = lst[i];
            }
          }
        }
      }
    }
		// Обновление конкретной таблицы
		void UpdateData(int rowIndex = -1, bool delete = false, bool multirow = false)
		{
			DataGridView grid = (DataGridView)TabMain.SelectedTab.Controls[0]; // Выбираем DataGridView в TabPage. NOTE: Controls[0], потому, что DataGridView единственный контрол в TabPage
			switch (TabMain.SelectedTab.Name)
			{
				case "FPage_CarBrand":			DataList_CarBrand.exFill("CarBrand");																			break;
				case "FPage_CarModel":			DataList_CarModel.exFill("CarModel", Base.CustomQueries["CarModelFill"]); break;
				case "FPage_Cars":					DataList_Cars.exFill("Cars", Base.CustomQueries["CarsFill"]);							break;
				case "FPage_CarType":				DataList_CarType.exFill("CarType");																				break;
				case "FPage_Drivers":				DataList_Drivers.exFill("Drivers");																				break;
				case "FPage_Organizations": DataList_Organizations.exFill("Organizations");														break;
				case "FPage_Staff":					DataList_Staff.exFill("Staff", Base.CustomQueries["StaffFill"]);					break;
				case "FPage_StaffPosition": DataList_StaffPosition.exFill("StaffPosition");														break;
			}
			#region Выделение последней выделенной строки
			if (rowIndex != -1 && delete == false && multirow == false)
			{
				grid.ClearSelection();
				grid.CurrentCell = grid[1, rowIndex];
				grid.ClearSelection();
				grid[1, rowIndex].Selected = true;
			}
			else if(delete == true && multirow == false)
			{
				if (grid.RowCount > 0)
				{
					int lastRow = grid.Rows.Count;
					if (rowIndex == lastRow)
					{
						grid.ClearSelection();
						grid.CurrentCell = grid[1, grid.Rows.Count - 1];
						grid.ClearSelection();
						grid[1, grid.Rows.Count - 1].Selected = true;
					}
					else
					{
						grid.ClearSelection();
						grid.CurrentCell = grid[1, rowIndex];
						grid.ClearSelection();
						grid[1, rowIndex].Selected = true;
					}
				}
			}
			else if(delete == true && multirow == true)
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
				grid.ClearSelection();
				grid.CurrentCell = grid[1, grid.Rows.Count - 1];
				grid.ClearSelection();
				grid[1, grid.Rows.Count - 1].Selected = true;
			}
			#endregion
		}
		// Добавление строки
		void AddRow()
    {
      if (new AddEdit(TabMain.SelectedTab.Name, (int)DataCommon.FormType.Add).ShowDialog() == DialogResult.OK) UpdateData();
    }
    // Редактирование строки
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
							case "FPage_CarBrand": CarBrand.RemoveRow(ID); break;
							case "FPage_CarModel": CarModel.RemoveRow(ID); break;
							case "FPage_Cars": Cars.RemoveRow(ID); break;
							case "FPage_CarType": CarType.RemoveRow(ID); break;
							case "FPage_Drivers": Drivers.RemoveRow(ID); break;
							case "FPage_Organizations": Organisations.RemoveRow(ID); break;
							case "FPage_Staff": Staff.RemoveRow(ID); break;
							case "FPage_StaffPosition": StaffPosition.RemoveRow(ID); break;
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
					/* +--------------------------------------------------+
					 * |											NOTE												|
					 * +--------------------------------------------------+
					 * |																									|
					 * |																									|
					 * | Имеют связь и требуют обработки									|
					 * +--------------------------------------------------+
					 * | CarType -> Cars ->								[TrackingLists]	|
					 * | CarBrand -> CarModel -> Cars ->	[TrackingLists]	|
					 * | StaffPosition -> Staff ->				[TrackingLists]	|
					 * |																									|
					 * | Не имеют особой связи, и не требуют обработки		|
					 * +--------------------------------------------------+
					 * | Estimate ->											[TrackingLists]	|
					 * | Drivers ->												[TrackingLists]	|
					 * | organisations ->									[TrackingLists]	|
					 * |																									|
					 * |																									|
					 * +--------------------------------------------------+
					 * |										END NOTE											|
					 * +--------------------------------------------------+
					*/
					connection.Open();
					// Выборка из БД, в зависимости от активной таблицы
					switch (page.Name)
					{
						case "FPage_CarBrand":
							adapter = new OleDbDataAdapter($"SELECT * FROM CarModel WHERE Carmodel_brand={id}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_CarModel":
							adapter = new OleDbDataAdapter($"SELECT * FROM Cars WHERE Car_model={id}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_Cars":
							adapter = new OleDbDataAdapter($"SELECT * FROM Trackinglists WHERE Trackinglist_transport={id}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_CarType":
							adapter = new OleDbDataAdapter($"SELECT * FROM Cars WHERE Car_type={id}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_Drivers":
							adapter = new OleDbDataAdapter($"SELECT * FROM Trackinglists WHERE Trackinglist_driver={id}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_Organizations":
							adapter = new OleDbDataAdapter($"SELECT * FROM Trackinglists WHERE " +
								$"Trackinglist_organization={id} OR Trackinglist_organization_2={id}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_Staff":
							adapter = new OleDbDataAdapter($"SELECT * FROM Trackinglists WHERE " +
								$"Trackinglist_dispatcher={id} OR Trackinglist_medic={id} OR Trackinglist_mechanic={id}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_StaffPosition":
							adapter = new OleDbDataAdapter($"SELECT * FROM Staff WHERE Staff_position={id}", connection);
							adapter.Fill(checkingTable);
							break;
					}
				}

				if(checkingTable.Rows.Count != 0) return true;	// Если есть строки то true
				else											return false;	// Если нет строк то false
			}

			bool CheckMoreRelationships(string initialTable, int[] values)
			{
				DataTable checkingTable = new DataTable();
				OleDbDataAdapter adapter;

				#region Созданние строки запроса
				string CONDITION = "";
				string CONDITION_COLUMN = "", CONDITION_COLUMN2 = "", CONDITION_COLUMN3 = "";

				switch (page.Name)
				{
					case "FPage_CarBrand":
						CONDITION_COLUMN = "Carmodel_brand";
						break;
					case "FPage_CarModel":
						CONDITION_COLUMN = "Car_model";
						break;
					case "FPage_Cars":
						CONDITION_COLUMN = "Trackinglist_transport";
						break;
					case "FPage_CarType":
						CONDITION_COLUMN = "Car_type";
						break;
					case "FPage_Drivers":
						CONDITION_COLUMN = "Trackinglist_driver";
						break;
					case "FPage_Organizations":
						CONDITION_COLUMN = "Trackinglist_organization";
						CONDITION_COLUMN2 = "Trackinglist_organization_2";
						break;
					case "FPage_Staff":
						CONDITION_COLUMN = "Trackinglist_dispatcher";
						CONDITION_COLUMN2 = "Trackinglist_medic";
						CONDITION_COLUMN3 = "Trackinglist_mechanic";
						break;
					case "FPage_StaffPosition":
						CONDITION_COLUMN = "Staff_position";
						break;
				}

				// Если в выборке только одно поле
				if(CONDITION_COLUMN2 == "" && CONDITION_COLUMN3 == "")
				{
					for (int i = 0; i < values.Length; i++)
						CONDITION += CONDITION_COLUMN + "=" + values[i] + " OR ";

					CONDITION = CONDITION.Remove(CONDITION.Length - 4);
				}
				// Если в выборке 2 поля
				else if(CONDITION_COLUMN2 != "" && CONDITION_COLUMN3 == "")
				{
					for (int i = 0; i < values.Length; i++)
						CONDITION += CONDITION_COLUMN + "=" + values[i] + " OR ";

					for (int i = 0; i < values.Length; i++)
						CONDITION += CONDITION_COLUMN2 + "=" + values[i] + " OR ";

					CONDITION = CONDITION.Remove(CONDITION.Length - 4);
				}
				// Если в выборке 3 поля
				else if (CONDITION_COLUMN2 != "" && CONDITION_COLUMN3 != "")
				{
					for (int i = 0; i < values.Length; i++)
						CONDITION += CONDITION_COLUMN + "=" + values[i] + " OR ";

					for (int i = 0; i < values.Length; i++)
						CONDITION += CONDITION_COLUMN2 + "=" + values[i] + " OR ";

					for (int i = 0; i < values.Length; i++)
						CONDITION += CONDITION_COLUMN3 + "=" + values[i] + " OR ";

					CONDITION = CONDITION.Remove(CONDITION.Length - 4);
				}
				#endregion

				// Проверка связанных таблиц
				using (var connection = new OleDbConnection(Base.DB_CONNECTION_STRING))
				{
					/* +--------------------------------------------------+
					 * |											NOTE												|
					 * +--------------------------------------------------+
					 * |																									|
					 * |																									|
					 * | Имеют связь и требуют обработки									|
					 * +--------------------------------------------------+
					 * | CarType -> Cars ->								[TrackingLists]	|
					 * | CarBrand -> CarModel -> Cars ->	[TrackingLists]	|
					 * | StaffPosition -> Staff ->				[TrackingLists]	|
					 * |																									|
					 * | Не имеют особой связи, и не требуют обработки		|
					 * +--------------------------------------------------+
					 * | Estimate ->											[TrackingLists]	|
					 * | Drivers ->												[TrackingLists]	|
					 * | organisations ->									[TrackingLists]	|
					 * |																									|
					 * |																									|
					 * +--------------------------------------------------+
					 * |										END NOTE											|
					 * +--------------------------------------------------+
					*/
					connection.Open();
					// Выборка из БД, в зависимости от активной таблицы
					switch (page.Name)
					{
						case "FPage_CarBrand":
							adapter = new OleDbDataAdapter($"SELECT * FROM CarModel WHERE {CONDITION}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_CarModel":
							adapter = new OleDbDataAdapter($"SELECT * FROM Cars WHERE {CONDITION}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_Cars":
							adapter = new OleDbDataAdapter($"SELECT * FROM Trackinglists WHERE {CONDITION}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_CarType":
							adapter = new OleDbDataAdapter($"SELECT * FROM Cars WHERE {CONDITION}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_Drivers":
							adapter = new OleDbDataAdapter($"SELECT * FROM Trackinglists WHERE {CONDITION}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_Organizations":
							adapter = new OleDbDataAdapter($"SELECT * FROM Trackinglists WHERE {CONDITION}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_Staff":
							adapter = new OleDbDataAdapter($"SELECT * FROM Trackinglists WHERE {CONDITION}", connection);
							adapter.Fill(checkingTable);
							break;
						case "FPage_StaffPosition":
							adapter = new OleDbDataAdapter($"SELECT * FROM Staff WHERE {CONDITION}", connection);
							adapter.Fill(checkingTable);
							break;
					}
				}

				if (checkingTable.Rows.Count != 0) return true; // Если есть строки то true
				else return false;                              // Если нет строк то false
			}
		}

		// Получение ID в строке и номер строки
		void GetCurrentRowID()
		{
			var grid = (DataGridView)TabMain.SelectedTab.Controls[0];
			try
			{
				ID = grid.CurrentRow.Cells[0].Value.exToInt();
				ROW_INDEX = grid.CurrentRow.Index;
			}
			catch { /* Костыльная заглушка */ }
		}

		#endregion

		#region События при работе с ToolStrip

		void ButtonAddRow(object sender, EventArgs e)   { AddRow();    }
    void ButtonEditRow(object sender, EventArgs e)  { EditRow();   }
    void ButtonRemoveRow(object sender, EventArgs e){ RemoveRow(); }
    
    #endregion

    #region События при работе с TabControl

    // Смена вкладки
    void TabMain_SelectedIndexChanged(object sender, EventArgs e) { this.InvokeEx(StatusStripRowCountUpdate); this.InvokeEx(StatusStripRowChangedUpdate); }

    #endregion

    #region События при работе с DataGrid

    // Вызов редактора строки при двойном щелчке по строке
    void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { EditRow(); }
    void Grid_CellClick(object sender, DataGridViewCellEventArgs e){ /* TODO: Nothing */ }
    // Обновление списка строк/выделенной строки при добавлении и удалении строки
    void RowCountChanged(object sender, DataGridViewRowsAddedEventArgs e) { this.InvokeEx(StatusStripRowCountUpdate); }
    void RowCountChanged(object sender, DataGridViewRowsRemovedEventArgs e) { this.InvokeEx(StatusStripRowCountUpdate); }
    void RowSelectionChanged(object sender, EventArgs e) { this.InvokeEx(StatusStripRowChangedUpdate); this.InvokeEx(GetCurrentRowID); }

    #endregion

    #region Прочие методы

    // Отображение в строке состояние кол-во строк в текущей таблице и выделенную строку
    void StatusStripRowCountUpdate()
    {
      // Общее число строк и выделенная строка
      foreach (DataGridView grid in TabMain.SelectedTab.Controls.OfType<DataGridView>())
        LabelRowCount.Text = grid.RowCount.ToString();
    }
    void StatusStripRowChangedUpdate()
    {
      // Текущая строка
			var grid = (DataGridView)TabMain.SelectedTab.Controls[0];

			if (grid.RowCount > 0) try { SelectedRow.Text = (grid.CurrentRow.Index + 1).ToString(); } catch { SelectedRow.Text = "1"; }
			else try { SelectedRow.Text = "0"; } catch { SelectedRow.Text = "0"; }

		}

		#endregion
	}
}
