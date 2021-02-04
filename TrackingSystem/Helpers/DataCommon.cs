using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using TrackingSystem.Models;


namespace TrackingSystem.Helpers
{
	class DataCommon : Base
	{
		public enum FormType : int { Add = 1, Edit }

		public static object[] Values;

		public static void FillComoBox(ComboBox combo, string colID, string colName, string table, string condition = "")
		{
			using(var connection = new OleDbConnection(DB_CONNECTION_STRING))
			{
				DataTable data = new DataTable();
				OleDbDataAdapter Adapter;

				if (condition.Any())
				{
					Adapter = new OleDbDataAdapter($"SELECT * FROM {table} WHERE {condition}", connection);
				}
				else
				{
					Adapter = new OleDbDataAdapter($"SELECT * FROM {table}", connection);
				}

				Adapter.Fill(data);

				combo.DisplayMember = colName;
				combo.ValueMember = colID;
				combo.DataSource = data;
			}
		}
		public static void FillComboboxExtra(ComboBox combo, string colID, string combine, string tableCombine, string condition = "")
		{
			using (var connection = new OleDbConnection(DB_CONNECTION_STRING))
			{
				DataTable data = new DataTable();
				OleDbDataAdapter Adapter;

				if (condition.Any())
				{
					Adapter = new OleDbDataAdapter($"SELECT {combine} FROM {tableCombine} WHERE {condition}", Connection);
				}
				else
				{
					Adapter = new OleDbDataAdapter($"SELECT {combine} FROM {tableCombine}", Connection);
				}

				Adapter.Fill(data);

				combo.DisplayMember = "Result";
				combo.ValueMember = colID;
				combo.DataSource = data;
			}
		}
		public static string GetFieldValue(int colID, string colName, string table)
		{
			string Result = "";
			using (var connection = new OleDbConnection(DB_CONNECTION_STRING))
			{
				connection.Open();
				var command = new OleDbCommand
				{
					Connection = connection,
					CommandText = $"SELECT {colName} FROM {table} WHERE ID={colID}"
				};
				OleDbDataReader reader = command.ExecuteReader();

				while (reader.Read()) { Result = reader.GetValue(0).ToString(); }
			}
			return Result;
		}

	}
}
