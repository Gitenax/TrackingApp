using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using TrackingSystem.Helpers;

namespace TrackingSystem.Models
{
	class StaffPosition : Base
	{
		// Свойства класса(таблицы базы данных)
		public string ID { get; set; } // ID Строки(№ пп)
		public string PositionName { get; set; } // Наименование должности

		/// <summary>
		/// Добавление строки в БД
		/// </summary>
		public void AddRow()
		{
			using (var connection = new OleDbConnection(DB_CONNECTION_STRING))
			{
				// Подключение
				connection.Open();
				var command = new OleDbCommand
				{
					Connection = connection,
					CommandText = "INSERT INTO StaffPosition(Staffposition_name) VALUES(@PositionName)"
				};

				// Параметры
				command.Parameters.AddWithValue("@PositionName", PositionName);

				// Исполнение
				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Редактирование строки в БД
		/// </summary>
		/// <param name="ID">Уникальный идентификатор по которому осуществляется выборка из БД</param>
		public void EditRow(int ID)
		{
			using (var connection = new OleDbConnection(DB_CONNECTION_STRING))
			{
				// Подключение
				connection.Open();
				var command = new OleDbCommand
				{
					Connection = connection,
					CommandText = $"UPDATE StaffPosition SET Staffposition_name=@PositionName WHERE ID={ID}"
				};

				// Параметры
				command.Parameters.AddWithValue("@PositionName", PositionName);

				// Исполнение
				command.ExecuteNonQuery();
			}

		}

		/// <summary>
		/// Удаление строки из БД
		/// </summary>
		/// <param name="ID">Уникальный идентификатор по которому осуществляется выборка из БД</param>
		public static void RemoveRow(int ID)
		{
			using (var connection = new OleDbConnection(DB_CONNECTION_STRING))
			{
				// Подключение
				connection.Open();
				var command = new OleDbCommand
				{
					Connection = connection,
					CommandText = $"DELETE FROM StaffPosition WHERE ID={ID}"
				};

				// Исполнение
				command.ExecuteNonQuery();
			}
		}

	}
}
