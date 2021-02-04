using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using TrackingSystem.Helpers;


namespace TrackingSystem.Models
{
	class CarType : Base
	{
		// Свойства класса(таблицы базы данных)
		public string ID { get; set; } // ID Строки(№ пп)
		public string TypeCode { get; set; } // Код типа авто
		public string TypeName { get; set; } // Наименование типа
		public string Capacity { get; set; } // Грузоподъемность
		public string InWorkRate { get; set; } // [В работе] - Тариф
		public string InWaitRate { get; set; } // [В простое] - Тариф


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
					CommandText = "INSERT INTO CarType(Cartype_code, Cartype_name, Cartype_capacity, Cartype_inwork_rate, Cartype_inwait_rate) VALUES(@TypeCode, @TypeName, @Capacity, @InWorkRate, @InWaitkRate)"
				};

				// Параметры
				command.Parameters.AddWithValue("@TypeCode", TypeCode);
				command.Parameters.AddWithValue("@TypeName", TypeName);
				command.Parameters.AddWithValue("@Capacity", Capacity);
				command.Parameters.AddWithValue("@InWorkRate", InWorkRate);
				command.Parameters.AddWithValue("@InWaitkRate", InWaitRate);

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
					CommandText = $"UPDATE CarType SET Cartype_code=@TypeCode, Cartype_name=@TypeName, Cartype_capacity=@Capacity, Cartype_inwork_rate=@InWorkRate, Cartype_inwait_rate=@InWaitRate WHERE ID={ID}"
				};

				// Параметры
				command.Parameters.AddWithValue("@TypeCode", TypeCode);
				command.Parameters.AddWithValue("@TypeName", TypeName);
				command.Parameters.AddWithValue("@Capacity", Capacity);
				command.Parameters.AddWithValue("@InWorkRate", InWorkRate);
				command.Parameters.AddWithValue("@InWaitRate", InWaitRate);

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
					CommandText = $"DELETE FROM CarType WHERE ID={ID}"
				};

				// Исполнение
				command.ExecuteNonQuery();
			}
		}

	}
}
