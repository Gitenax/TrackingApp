using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TrackingSystem
{
  class ReportEngine
  {
    public static void MakeReportWord(Dictionary<string, string> Values)
    {
      //string tackseries = "________"; // Arial Bold 10px

      List<string> lst = new List<string>
      {
        // Первый лист
        "{@Trackinglist_series}",
        "{@Trackinglist_number}",
        "{@date_d}",
        "{@date_m}",
        "{@date_y}",
        "{@Trackinglist_organization}", // х2
        "{@Car_name}", // х3
        "{@Car_number}", // х3
        "{@Driver_name}",
        "{@Driver_licence}",
        "{@Driver_class}",
        "{@dispatcher_short}",
        "{@medic_short}",
        "{@mechanic_short}",
        "{@driver_short}",
        // Второй лист
        "{@Trackinglist_organization_another}" // х2
      };
    }
    public static void MakeReportExcel()
    {

    }


    /*
    private void toExcel(DataGridView dgv)
    {
      if (dgv.RowCount > 0)
      {
        Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook workbook;
        Microsoft.Office.Interop.Excel.Worksheet worksheet;

        // Переименование листа с данными
        workbook = Excel.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, "результат.xlsx"));
        worksheet = workbook.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
        worksheet.Name = "Отчет о состязании";

        /* Заголовок
        worksheet.Range["C4"].Value = "Дугин Никита Владимирович";      // Оператор
        worksheet.Range["G4"].Value = DT.CurrentAction;                 // Номер забега
        worksheet.Range["C5"].Value = DT.CurrentDate;                   // Дата
        worksheet.Range["C28"].Value = DT.CurrentDate;                  // Дата
        worksheet.Range["F5"].Value = DT.CurrentTime;                   // Время
        worksheet.Range["C6"].Value = DT.CurrentName;                   // Мероприятие
        worksheet.Range["C7"].Value = DT.CurrentTrack;                  // Ипподром
       
        // Участники
        int row = 12;
        for (int i = 0; i < dgv.RowCount; i++)
        {
          worksheet.Range[$"B{row}"].Value = dgv.Rows[i].Cells[3].Value.ToString(); // Жокей
          worksheet.Range[$"E{row}"].Value = dgv.Rows[i].Cells[4].Value.ToString(); // Скакун
          worksheet.Range[$"F{row}"].Value = dgv.Rows[i].Cells[5].Value.ToString(); // Время
          worksheet.Range[$"G{row}"].Value = dgv.Rows[i].Cells[6].Value.ToString(); // Очки
          row++;
        }
        // Отображение окна Excel
        Excel.Visible = true;
      }
      else
      {
        MessageBox.Show("Экспорт данных не возможен т.к. отсутствуют данные для экспорта!", "Экспорт данных Excel", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }*/
  }
}
