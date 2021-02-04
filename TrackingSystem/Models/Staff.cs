using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using TrackingSystem.Helpers;

namespace TrackingSystem.Models
{
	class Staff : Base
	{
		// Свойства класса(таблицы базы данных)
		public string ID { get; set; } // ID Строки(№ пп)
		public string FirstName { get; set; } // Имя
		public string LastName { get; set; } // Фамилия
		public string ThirdName { get; set; } // Отчество
		public string Position { get; set; } // Должность

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
					CommandText = "INSERT INTO Staff(Staff_firstname, Staff_lastname, Staff_thirdname, Staff_position) VALUES(@FirstName, @LastName, @ThirdName, @Position)"
				};

				// Параметры
				command.Parameters.AddWithValue("@FirstName", FirstName);
				command.Parameters.AddWithValue("@LastName", LastName);
				command.Parameters.AddWithValue("@ThirdName", ThirdName);
				command.Parameters.AddWithValue("@Position", Position);

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
					CommandText = $"UPDATE Staff SET Staff_firstname=@FirstName, Staff_lastname=@LastName, Staff_thirdname=@ThirdName, Staff_position=@Position WHERE ID={ID}"
				};

				// Параметры
				command.Parameters.AddWithValue("@FirstName", FirstName);
				command.Parameters.AddWithValue("@LastName", LastName);
				command.Parameters.AddWithValue("@ThirdName", ThirdName);
				command.Parameters.AddWithValue("@Position", Position);

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
					CommandText = $"DELETE FROM Staff WHERE ID={ID}"
				};

				// Исполнение
				command.ExecuteNonQuery();
			}
		}

	}
}
