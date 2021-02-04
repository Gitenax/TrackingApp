using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;

namespace AlterTable
{
	class Program
	{
		// Путь до БД
		static string DB_PATH = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"/Data/base.mdb";

		// Точка входа в программу
		static void Main(string[] args)
		{
			Console.WriteLine("Добавление нового столбца в таблицу \"Trackinglists\"...");
			UpdateTable();
			Console.WriteLine("Добавление завершено! Нажмите любую клавишу для выхода...");
			Console.ReadKey();
		}

		/// <summary>
		/// Функция добавления столбца в БД
		/// </summary>
		static void UpdateTable()
		{
			using (var connection = new OleDbConnection(DB_PATH))
			{
				try { connection.Open(); }
				catch
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Возникли проблемы с подключение к БД! Нажмите любую клавишу для выхода...");
					Console.ReadKey();
					Process.GetCurrentProcess().Kill();
				}

				OleDbCommand command = new OleDbCommand
				{
					Connection = connection,
					CommandText = "ALTER TABLE Trackinglists ADD COLUMN Trackinglist_organization_3 INT;"
				};

				try { command.ExecuteNonQuery(); }
				catch(OleDbException)
				{
					connection.Close();
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Столбец уже добавлен в таблицу! Нажмите любую клавишу для выхода...");
					Console.ReadKey();
					Process.GetCurrentProcess().Kill();
				}
			}

		}
	}
}
