using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using TrackingSystem.Helpers;
using TrackingSystem.Models;
using System.Threading;

namespace TrackingSystem
{
  public partial class Report : Form
  {
    private int ID;

    public Report()
    {
      InitializeComponent();
    }

    private void Report_Load(object sender, EventArgs e)
    {
      Mode.SelectedIndex = 0;
			DataList_Report.exSetDoubleBuffered();

			// Заполнение DataGrid данными Trackinglist
			switch (Mode.SelectedIndex)
			{
				case 0: FillTracklist(); break;
				case 1: FillEstimate();	break;
			}
      FillTracklist();
    }

    private void Mode_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch (((ComboBox)sender).SelectedIndex)
      {
        case 0: FillTracklist();  break;
        case 1: FillEstimate();   break;
      }
    }

    private void FillTracklist()
    {
      DataList_Report.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			DataList_Report.exFill("Trackinglists", Base.CustomQueries["TrackinglistFill"]);
      DataList_Report.Columns[0].Visible = false;
      DataList_Report.Columns[1].Visible = false;

      List<string> lst = Base.ColumnHeaders["DataList_Trackinglists"];
      for (int i = 1; i < DataList_Report.ColumnCount - 1; i++)
      {
        DataList_Report.Columns[i + 1].HeaderText = lst[i];
      }
    }

    private void FillEstimate()
    {
			DataList_Report.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			DataList_Report.exFill("Estimate", Base.CustomQueries["EstimateFill"]);
			DataList_Report.Columns[0].Visible = false;

      List<string> lst = Base.ColumnHeaders["DataList_Estimate"];
      for (int i = 0; i < DataList_Report.ColumnCount; i++)
      {
        DataList_Report.Columns[i].HeaderText = lst[i];
      }
    }

    private void DataList_Report_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      ID = Convert.ToInt32(DataList_Report.CurrentRow.Cells[0].Value);
    }

    private void ReportButtonClick(object sender, EventArgs e)
    {
      var item = ((Button)sender);
			FolderBrowserDialog dialog = new FolderBrowserDialog
			{
				Description = "Выберите папку для сохранения отчета",
				RootFolder = Environment.SpecialFolder.MyComputer,
				ShowNewFolderButton = true
			};

			switch (Mode.SelectedIndex)
      {
        case 0: // Путевые листы
          switch(item.Name)
          {
            case "B_Trackinglist":
							if(dialog.ShowDialog() == DialogResult.OK)
							{
								
								if(DataList_Report.SelectedRows.Count > 1)
								{
									// Получаем индексы выделенных ячеек
									int[] indexes = new int[DataList_Report.SelectedRows.Count];
									for (int i = 0; i < DataList_Report.SelectedRows.Count; i++) indexes[i] = DataList_Report.SelectedRows[i].Cells[0].Value.exToInt();

									Trackinglists.Report(ReportType.Trackinglist, dialog.SelectedPath, ArrayOfIDs: indexes);
								}
								else
								{
									Trackinglists.Report(ReportType.Trackinglist, dialog.SelectedPath, ID);
									
								}
							}
							break;
            case "B_Reference":
							if(new Reference().ShowDialog() == DialogResult.OK)
							{
								if(dialog.ShowDialog() == DialogResult.OK)
								{
									Trackinglists.Report(ReportType.Reference, dialog.SelectedPath, ID);
								}
							}
							break;
            case "B_Registry":
							if(new Duration().ShowDialog() == DialogResult.OK)
							{
								Trackinglists.Report(ReportType.Registry, ID: ID);
							}
							
							break;
          }
          break;
        case 1: // Смета
          switch (item.Name)
          {
            case "B_Trackinglist":
              int T_ID = Convert.ToInt32(DataCommon.GetFieldValue(ID, "Estimate_tracklist", "Estimate"));
              Trackinglists.Report(ReportType.Trackinglist, ID: T_ID);
              break;
          }
          break;
      }
    }

    private void GetID()
    {

    }
  }
}
