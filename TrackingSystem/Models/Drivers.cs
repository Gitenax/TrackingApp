using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using TrackingSystem.Helpers;

namespace TrackingSystem.Models
{
	class Drivers : Base
	{
		// Свойства класса(таблицы базы данных)
		public string ID { get; set; } // ID Строки(№ пп)
		public string FirstName { get; set; } // Имя
		public string LastName { get; set; } // Фамилия
		public string ThirdName { get; set; } // Отчество
		public string Licence { get; set; } // Номер лицензии(прав)
		public string Class { get; set; } // Класс

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
					CommandText = "INSERT INTO Drivers(Driver_firstname, Driver_lastname, Driver_thirdname, Driver_licensenumber, Driver_class) VALUES(@FirstName, @LastName, @ThirdName, @Licence, @Class)"
				};

				// Параметры
				command.Parameters.AddWithValue("@FirstName", FirstName);
				command.Parameters.AddWithValue("@LastName", LastName);
				command.Parameters.AddWithValue("@ThirdName", ThirdName);
				command.Parameters.AddWithValue("@Licence", Licence);
				command.Parameters.AddWithValue("@Class", Class);

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
					CommandText = $"UPDATE Drivers SET Driver_firstname=@FirstName, Driver_lastname=@LastName, Driver_thirdname=@ThirdName, Driver_licensenumber=@Licence, Driver_class=@Class WHERE ID={ID}"
				};

				// Параметры
				command.Parameters.AddWithValue("@FirstName", FirstName);
				command.Parameters.AddWithValue("@LastName", LastName);
				command.Parameters.AddWithValue("@ThirdName", ThirdName);
				command.Parameters.AddWithValue("@Licence", Licence);
				command.Parameters.AddWithValue("@Class", Class);

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
					CommandText = $"DELETE FROM Drivers WHERE ID={ID}"
				};

				// Исполнение
				command.ExecuteNonQuery();
			}
		}

	}
}
