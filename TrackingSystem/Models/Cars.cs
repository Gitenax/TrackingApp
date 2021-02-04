using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using TrackingSystem.Helpers;

namespace TrackingSystem.Models
{
	class Cars : Base
	{
		// Свойства класса(таблицы базы данных)
		public string ID { get; set; } // ID Строки(№ пп)
		public string Model { get; set; } // [class CarModel] - Модель авто
		public string Number { get; set; } // Гос. номер
		public string InvNumber { get; set; } // Инвентарный номер
		public string Type { get; set; } // [class CarType] - Тип ТС

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
					CommandText = "INSERT INTO Cars(Car_model, Car_number, Car_inventorynumber, Car_type) VALUES(@Model, @Number, @InvNumber, @Type)"
				};

				// Параметры
				command.Parameters.AddWithValue("@Model", Model);
				command.Parameters.AddWithValue("@Number", Number);
				command.Parameters.AddWithValue("@InvNumber", InvNumber);
				command.Parameters.AddWithValue("@Type", Type);

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
					CommandText = $"UPDATE Cars SET Car_model=@Model, Car_number=@Number, Car_inventorynumber=@InvNumber, Car_type=@Type WHERE ID={ID}"
				};

				// Параметры
				command.Parameters.AddWithValue("@Model", Model);
				command.Parameters.AddWithValue("@Number", Number);
				command.Parameters.AddWithValue("@InvNumber", InvNumber);
				command.Parameters.AddWithValue("@Type", Type);

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
					CommandText = $"DELETE FROM Cars WHERE ID={ID}"
				};

				// Исполнение
				command.ExecuteNonQuery();
			}
		}

	}
}
