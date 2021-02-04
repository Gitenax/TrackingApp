using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using TrackingSystem.Helpers;

namespace TrackingSystem.Models
{
	class CarModel : Base
	{
		// Свойства класса(таблицы базы данных)
		public string ID { get; set; } // ID Строки(№ пп)
		public string Brand { get; set; } // Производитель
		public string ModelName { get; set; } // Модельный ряд

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
					CommandText = "INSERT INTO CarModel(Carmodel_brand, Carmodel_name) VALUES(@Brand, @ModelName)"
				};

				// Параметры
				command.Parameters.AddWithValue("@Brand", Brand);
				command.Parameters.AddWithValue("@ModelName", ModelName);

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
					CommandText = $"UPDATE CarModel SET Carmodel_brand=@Brand, Carmodel_name=@ModelName WHERE ID={ID}"
				};

				// Параметры
				command.Parameters.AddWithValue("@Brand", Brand);
				command.Parameters.AddWithValue("@ModelName", ModelName);

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
					CommandText = $"DELETE FROM CarModel WHERE ID={ID}"
				};

				// Исполнение
				command.ExecuteNonQuery();
			}
		}
	}
}
