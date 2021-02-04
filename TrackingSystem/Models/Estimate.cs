using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using TrackingSystem.Helpers;

namespace TrackingSystem.Models
{
	class Estimate : Base
	{
		// Свойства класса(таблицы базы данных)
		public string ID { get; set; } // ID Строки(№ пп)
		public string PacketNumber { get; set; } // Номер пачки
		public string Order { get; set; } // Номер заказа
		public string TrackinglistNum { get; set; } // Номер путевого листа
		public string TrackinglistDate { get; set; } // Дата путевого листа
		public string Lot { get; set; } // Лот
		public string Duration { get; set; } // Пробег
		public string InWorkTime { get; set; } // [В работе] - Кол-во часов
		public string InWorkRate { get; set; } // [В работе] - Тариф
		public string InWorkHols { get; set; } // [В работе] - Выходные
		public string InWorkPrice { get; set; } // [В работе] - Сумма
		public string InWaitTime { get; set; } // [В сто-ке] - Кол-во часов
		public string InWaitRate { get; set; } // [В сто-ке] - Тариф
		public string InWaitHols { get; set; } // [В сто-ке] - Выходные 5%
		public string InWaitPrice { get; set; } // [В сто-ке] - Сумма
		public string FinalPrice { get; set; } // Общая сумма

		/// <summary>
		/// Добавление строки в БД
		/// </summary>
		public void AddRow()
		{
			using (var connection = new OleDbConnection(DB_CONNECTION_STRING))
			{
				// Стока запроса
				string INSERT = "INSERT INTO Estimate(Estimate_packetnumber, Estimate_order, Estimate_tracklist, Estimate_date, Estimate_lot, " +
					"Estimate_duration, Estimate_inwork_time, Estimate_inwork_rate, Estimate_inwork_hols, Estimate_inwork_price, " +
					"Estimate_inwait_time, Estimate_inwait_rate, Estimate_inwait_hols, Estimate_inwait_price, Estimate_finalprice) ";
				string VALUES = "VALUES(@PacketNumber, @Order, @TrackinglistNum, @TrackinglistDate, @Lot, " +
					"@Duration, @InWorkTime, @InWorkRate, @InWorkHols, @InWorkPrice, " +
					"@InWaitTime, @InWaitRate, @InWaitHols, @InWaitPrice, @FinalPrice)";

				// Подключение
				connection.Open();
				var command = new OleDbCommand
				{
					Connection = connection,
					CommandText = INSERT + VALUES
				};

				// Параметры
				command.Parameters.AddWithValue("@PacketNumber", PacketNumber);
				command.Parameters.AddWithValue("@Order", Order);
				command.Parameters.AddWithValue("@TrackinglistNum", TrackinglistNum);
				command.Parameters.AddWithValue("@TrackinglistDate", TrackinglistDate);
				command.Parameters.AddWithValue("@Lot", Lot);
				command.Parameters.AddWithValue("@Duration", Duration);
				command.Parameters.AddWithValue("@InWorkTime", InWorkTime);
				command.Parameters.AddWithValue("@InWorkRate", InWorkRate);
				command.Parameters.AddWithValue("@InWorkHols", InWorkHols);
				command.Parameters.AddWithValue("@InWorkPrice", InWorkPrice);
				command.Parameters.AddWithValue("@InWaitTime", InWaitTime);
				command.Parameters.AddWithValue("@InWaitRate", InWaitRate);
				command.Parameters.AddWithValue("@InWaitHols", InWaitHols);
				command.Parameters.AddWithValue("@InWaitPrice", InWaitPrice);
				command.Parameters.AddWithValue("@FinalPrice", FinalPrice);

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
				#region Строка запроса
				string QUERY = 
				"UPDATE Estimate SET " +
				"Estimate_packetnumber=@PacketNumber, " +
				"Estimate_order=@Order, " +
				"Estimate_tracklist=@TrackinglistNum, " +
				"Estimate_date=@TrackinglistDate, " +
				"Estimate_lot=@Lot, " +
				"Estimate_duration=@Duration, " +
				"Estimate_inwork_time=@InWorkTime, " +
				"Estimate_inwork_rate=@InWorkRate, " +
				"Estimate_inwork_hols=@InWorkHols, " +
				"Estimate_inwork_price=@InWorkPrice, " +
				"Estimate_inwait_time=@InWaitTime, " +
				"Estimate_inwait_rate=@InWaitRate, " +
				"Estimate_inwait_hols=@InWaitHols, " +
				"Estimate_inwait_price=@InWaitPrice, " +
				"Estimate_finalprice=@FinalPrice " +
				$"WHERE ID={ID}";
				#endregion

				// Подключение
				connection.Open();
				var command = new OleDbCommand
				{
					Connection = connection,
					CommandText = QUERY
				};

				// Параметры
				command.Parameters.AddWithValue("@PacketNumber", PacketNumber);
				command.Parameters.AddWithValue("@Order", Order);
				command.Parameters.AddWithValue("@TrackinglistNum", TrackinglistNum);
				command.Parameters.AddWithValue("@TrackinglistDate", TrackinglistDate);
				command.Parameters.AddWithValue("@Lot", Lot);
				command.Parameters.AddWithValue("@Duration", Duration);
				command.Parameters.AddWithValue("@InWorkTime", InWorkTime);
				command.Parameters.AddWithValue("@InWorkRate", InWorkRate);
				command.Parameters.AddWithValue("@InWorkHols", InWorkHols);
				command.Parameters.AddWithValue("@InWorkPrice", InWorkPrice);
				command.Parameters.AddWithValue("@InWaitTime", InWaitTime);
				command.Parameters.AddWithValue("@InWaitRate", InWaitRate);
				command.Parameters.AddWithValue("@InWaitHols", InWaitHols);
				command.Parameters.AddWithValue("@InWaitPrice", InWaitPrice);
				command.Parameters.AddWithValue("@FinalPrice", FinalPrice);

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
					CommandText = $"DELETE FROM Estimate WHERE ID={ID}"
				};

				// Исполнение
				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Возвращает все поля класса в виде массива строк
		/// </summary>
		/// <returns>Массив строк string[]</returns>
		public string[] ToArray()
		{
			string[] array = new string[21];

			array[0] = ID;
			array[1] = PacketNumber;
			array[2] = Order;
			array[3] = TrackinglistNum;
			array[4] = TrackinglistDate;
			array[5] = Lot;
			array[6] = Duration;
			array[7] = InWorkTime;
			array[8] = InWorkRate;
			array[9] = InWorkHols;
			array[10] = InWorkPrice;
			array[11] = InWaitTime;
			array[12] = InWaitRate;
			array[13] = InWaitHols;
			array[14] = InWaitPrice;
			array[15] = FinalPrice;

			return array;
		}

		public static string[] GetGeneralData(int ID)
		{
			// номер пачки
			// Номер заказа
			// Номер путевого листа
			// дата путевого листа
			// Лот
			// Пробег
			// В рабоче часов
			// Тариф
			// Вых- праз
			// Сумма
			// В отдыхе час
			// Тариф
			// Вых- праз
			// Сумма
			// Общая сумма

			string[] Data = new string[0];

			try
			{
				Connection.Open();
				string Query = "SELECT Estimate.Estimate_packetnumber, Estimate.Estimate_order, Trackinglists.Trackinglist_number, Estimate.Estimate_date, Estimate.Estimate_lot, Estimate.Estimate_duration, Estimate.Estimate_inwork_time, Estimate.Estimate_inwork_rate, Estimate.Estimate_inwork_hols, Estimate.Estimate_inwork_price, Estimate.Estimate_inwait_time, Estimate.Estimate_inwait_rate, Estimate.Estimate_inwait_hols, Estimate.Estimate_inwait_price, Estimate.Estimate_finalprice " +
											 "FROM Trackinglists INNER JOIN Estimate ON Trackinglists.ID = Estimate.Estimate_tracklist " +
											$"WHERE Estimate.ID={ID};";
				Command = new OleDbCommand(Query, Connection);
				Reader = Command.ExecuteReader();
				Data = new string[Reader.FieldCount];

				while (Reader.Read())
				{
					for (int i = 0; i < Reader.FieldCount; i++)
						Data[i] = Reader.GetValue(i).ToString();
				}
				Connection.Close();
			}
			catch
			{ }

			return Data;
		}

	}
}
