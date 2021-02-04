using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Data.OleDb;
using TrackingSystem.Models;

namespace TrackingSystem.Helpers
{
	public static class ExtensionMethods
	{

		/// <summary>
		/// Заполняет DataSet списка данными из указанной в условиях таблицы БД
		/// </summary>
		/// <param name="control"></param>
		/// <param name="colID">Наименование столбца с идентификатором [Value Member]</param>
		/// <param name="colName">Столбец который будет отображаться в списке [Display Member]</param>
		/// <param name="table">Таблица из которой будут взяты значения</param>
		/// <param name="condition">(Необязательное) Дополнительное условие для выборки</param>
		public static void exFill(this ComboBox control, string colID, string colName, string table, string condition = "")
		{
			DataCommon.FillComoBox(control, colID, colName, table, condition);
		}


		/// <summary>
		/// Заполняет DataSet списка данными из указанной в условиях таблицы БД c комбинированными значениями
		/// </summary>
		/// <param name="control"></param>
		/// <param name="colID">Наименование столбца с идентификатором [Value Member]</param>
		/// <param name="combine">Столбец который будет отображаться в списке [Display Member]</param>
		/// <param name="tableCombine">Таблица из которой будут взяты значения</param>
		/// <param name="condition">(Необязательное) Дополнительное условие для выборки</param>
		public static void exFillExtra(this ComboBox control, string colID, string combine, string tableCombine, string condition = "")
		{
			DataCommon.FillComboboxExtra(control, colID, combine, tableCombine, condition);
		}


		/// <summary>
		/// Конвертирует текущее выращение в тип данных Int32
		/// </summary>
		/// <param name="value">Значение</param>
		/// <returns>Возвращает object в Int32</returns>
		public static int exToInt(this object value)
		{
			return Convert.ToInt32(value);
		}

		/// <summary>
		/// Конвертирует текущее выращение в тип данных Int32
		/// </summary>
		/// <param name="value">Значение</param>
		/// <returns>Возвращает object в Int32</returns>
		public static int exToInt(this string value)
		{
			return Convert.ToInt32(value);

		}

		/// <summary>
		/// Конвертирует текущее выращение в тип данных Double
		/// </summary>
		/// <param name="value">Значение</param>
		/// <returns>Возвращает object в Int32</returns>
		public static double exToDouble(this string value)
		{
			return Convert.ToDouble(value);
		}


		/// <summary>
		/// Заполняет DataSet текущего DataGrid данными DataTable взятых из БД
		/// </summary>
		/// <param name="control">DataGridView</param>
		/// <param name="TableName">Имя загружаемой таблицы</param>
		/// <param name="CustomQuery">З</param>
		public static void exFill(this DataGridView control, string TableName, string CustomQuery = null)
		{
			using (var connection = new OleDbConnection(Base.DB_CONNECTION_STRING))
			{
				var Table = new DataTable();
				connection.Open();

				if (CustomQuery != null)
					new OleDbDataAdapter(CustomQuery, connection).Fill(Table);
				else
					new OleDbDataAdapter($"SELECT * FROM {TableName}", connection).Fill(Table);

				control.DataSource = Table;
			}
		}

		public static string[] exToArray(this DataTable table, int rowID)
		{
			string[] result = new string[table.Columns.Count];

			for(int i = 0; i < result.Length; i++) result[i] = table.Rows[rowID][i].ToString();
			
			return result;
		}

		public static string exToString(this string[] array, string splitter, int startIndex = 0)
		{
			string result = "";
			for(int i = startIndex; i < array.Length; i++)
			{
				result += array[i] + splitter;
			}

			result = result.Remove(result.Length - splitter.Length);

			return result;
		}


		public static void exSetDoubleBuffered(this DataGridView control)
		{
			PropertyInfo info = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
			if (info != null) info.SetValue(control, true, null);
		}
	}
}
