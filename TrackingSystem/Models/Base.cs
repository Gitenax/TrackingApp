using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace TrackingSystem.Models
{
	class Base
	{
		#region Поля
		// Соединение с БД
		public static bool AddClient = false;
		protected static OleDbConnection Connection;
		protected static OleDbCommand Command;
		protected static OleDbDataReader Reader;
		public static string DB_CONNECTION_STRING;
		// Кастомные запросы в виде строк
		public static Dictionary<string, string> CustomQueries = new Dictionary<string, string>();
		// Массив заголовков DataGridColumn
		public static Dictionary<string, List<string>> ColumnHeaders = new Dictionary<string, List<string>>();
		#endregion

		#region Конструкторы
		public Base() { }
		public Base(string dbPath)
		{
			// Создание соединения с БД
			DB_CONNECTION_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath;
			Connection = new OleDbConnection(DB_CONNECTION_STRING);

			_SetCustomQueries();
			_SetColumnHeaders();
		}
		#endregion

		#region Статичные методы
		// Выполнение SQL запроса(Добавление/Редактирование/Удаление)
		public static void SQL(string QueryString)
		{
			Connection.Open();
			new OleDbCommand(QueryString, Connection).ExecuteNonQuery();
			Connection.Close();
		}

		// Получение массива данных одной строки из БД
		public static object[] GetRowFields(int ID, string TableName)
		{
			Connection.Open();
			Command = new OleDbCommand($"SELECT * FROM {TableName} WHERE ID={ID}", Connection);
			Reader = Command.ExecuteReader();

			object[] Values = new object[Reader.FieldCount - 1];

			while (Reader.Read())
			{
				for (int i = 0; i < Reader.FieldCount - 1; i++)
					Values[i] = Reader.GetValue(i + 1);
			}

			Connection.Close();

			return Values;
		}

		// Заполнение DataGridView данными из БД
		public static DataTable Fill(string TableName, string CustomQuery = null)
		{
			// Возвращаемая таблица
			DataTable Table = new DataTable();
			Connection.Open();

			if (CustomQuery != null)
				new OleDbDataAdapter(CustomQuery, Connection).Fill(Table);
			else
				new OleDbDataAdapter($"SELECT * FROM {TableName}", Connection).Fill(Table);

			Connection.Close();
			return Table;
		}

		public static void Update(DataGridView Grid, string TableName, string CustomQuery = null)
		{
			if (CustomQuery != null)
				Grid.DataSource = Fill(TableName, CustomQuery);
			else
				Grid.DataSource = Fill(TableName);
		}

		// Закрытие подключения
		public static void CloseConnection() { Connection.Close(); }

		/// <summary>
		/// Удаление строки из БД
		/// </summary>
		/// <param name="table">Имя таблицы</param>
		/// <param name="ID">Уникальный идентификатор по которому осуществляется выборка из БД</param>
		public static void RemoveRows(string table, int[] ID)
		{
			string CONDITION = "";

			for (int i = 0; i < ID.Length; i++)
				CONDITION += ID[i] + ", ";

			CONDITION = CONDITION.Remove(CONDITION.Length - 2);

			using (var connection = new OleDbConnection(DB_CONNECTION_STRING))
			{
				// Подключение
				connection.Open();
				var command = new OleDbCommand
				{
					Connection = connection,
					CommandText = $"DELETE FROM {table} WHERE ID IN({CONDITION})"
				};

				// Исполнение
				command.ExecuteNonQuery();
			}
		}

		#endregion

		#region Настройки таблиц
		// Объявление кастомных запросов
		void _SetCustomQueries()
		{
			CustomQueries.Add("EstimateFill", "SELECT Estimate.ID, Estimate.Estimate_packetnumber, Estimate.Estimate_order, Trackinglists.Trackinglist_number, Trackinglists.Trackinglist_date, Estimate.Estimate_lot, Estimate.Estimate_duration, Cars.Car_number, CarModel.Carmodel_name, CarType.Cartype_capacity, CarType.Cartype_name, CarType.Cartype_code, Estimate.Estimate_inwork_time, Estimate.Estimate_inwork_rate, Estimate.Estimate_inwork_hols, Estimate.Estimate_inwork_price, Estimate.Estimate_inwait_time, Estimate.Estimate_inwait_rate, Estimate.Estimate_inwait_hols, Estimate.Estimate_inwait_price, Estimate.Estimate_finalprice FROM(CarType INNER JOIN(((CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand) INNER JOIN Cars ON CarModel.ID = Cars.Car_model) INNER JOIN Trackinglists ON Cars.ID = Trackinglists.Trackinglist_transport) ON CarType.ID = Cars.Car_type) INNER JOIN Estimate ON Trackinglists.ID = Estimate.Estimate_tracklist;");
			CustomQueries.Add("EstimateFillForNewTab", "SELECT Estimate.ID, Estimate.Estimate_packetnumber, Estimate.Estimate_order, Trackinglists.Trackinglist_number, Trackinglists.Trackinglist_date, Estimate.Estimate_lot, Estimate.Estimate_duration, Cars.Car_number, CarModel.Carmodel_name, CarType.Cartype_capacity, CarType.Cartype_name, CarType.Cartype_code, Estimate.Estimate_inwork_time, Estimate.Estimate_inwork_rate, Estimate.Estimate_inwork_hols, Estimate.Estimate_inwork_price, Estimate.Estimate_inwait_time, Estimate.Estimate_inwait_rate, Estimate.Estimate_inwait_hols, Estimate.Estimate_inwait_price, Estimate.Estimate_finalprice FROM(CarType INNER JOIN(((CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand) INNER JOIN Cars ON CarModel.ID = Cars.Car_model) INNER JOIN Trackinglists ON Cars.ID = Trackinglists.Trackinglist_transport) ON CarType.ID = Cars.Car_type) INNER JOIN Estimate ON Trackinglists.ID = Estimate.Estimate_tracklist WHERE Trackinglist_Organization_2=3;");
			CustomQueries.Add("CarModelFill", "SELECT CarModel.ID, CarBrand.Carbrand_name, CarModel.Carmodel_name FROM CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand;");
			CustomQueries.Add("CarsFill", "SELECT Cars.ID, [CarBrand]![Carbrand_name] & \" \" & [CarModel]![Carmodel_name] AS Выражение1, Cars.Car_number, Cars.Car_inventorynumber, '[' & [CarType]![Cartype_code] & \"] \" & [CarType]![Cartype_name] & \" - Грузоподъемность: \" & [CarType]![Cartype_capacity] AS Выражение2 FROM CarBrand INNER JOIN(CarType INNER JOIN (CarModel INNER JOIN Cars ON CarModel.ID = Cars.Car_model) ON CarType.ID = Cars.Car_type) ON CarBrand.ID = CarModel.Carmodel_brand;");
			CustomQueries.Add("StaffFill", "SELECT Staff.ID, Staff.Staff_firstname, Staff.Staff_lastname, Staff.Staff_thirdname, StaffPosition.Staffposition_name FROM StaffPosition INNER JOIN Staff ON StaffPosition.ID = Staff.Staff_position;");
			CustomQueries.Add("TrackinglistFill", "SELECT Trackinglists.ID, Cars.ID, Trackinglists.Trackinglist_series, Trackinglists.Trackinglist_number, Trackinglists.Trackinglist_date, Organizations.Organization_name, Organizations_1.Organization_name, [CarBrand]![Carbrand_name] & \" \" & [CarModel]![Carmodel_name] & \", Гос.номер: \" & [Cars]![Car_number] AS Выражение1, [Drivers]![Driver_lastname] & \" \" & [Drivers]![Driver_firstname] & \" \" & [Drivers]![Driver_thirdname] AS Выражение2 FROM Organizations INNER JOIN(Drivers INNER JOIN (((CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand) INNER JOIN Cars ON CarModel.ID = Cars.Car_model) INNER JOIN(Trackinglists INNER JOIN Organizations AS Organizations_1 ON Trackinglists.Trackinglist_organization_3 = Organizations_1.ID) ON Cars.ID = Trackinglists.Trackinglist_transport) ON Drivers.ID = Trackinglists.Trackinglist_driver) ON Organizations.ID = Trackinglists.Trackinglist_organization_2;");
			//CustomQueries.Add("TrackinglistFill", "SELECT Trackinglists.ID, Cars.ID, Trackinglists.Trackinglist_series, Trackinglists.Trackinglist_number, Trackinglists.Trackinglist_date, Organizations.Organization_name, [CarBrand]![Carbrand_name] & \" \" & [CarModel]![Carmodel_name] & \", Гос. номер: \" & [Cars]![Car_number] AS Выражение1, [Drivers]![Driver_lastname] & \" \" & [Drivers]![Driver_firstname] & \" \" &  [Drivers]![Driver_thirdname] AS Выражение2 FROM Drivers INNER JOIN(((CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand) INNER JOIN Cars ON CarModel.ID = Cars.Car_model) INNER JOIN(Organizations INNER JOIN Trackinglists ON Organizations.ID = Trackinglists.Trackinglist_organization_2) ON Cars.ID = Trackinglists.Trackinglist_transport) ON Drivers.ID = Trackinglists.Trackinglist_driver;");
		}

		// Список заголовков DataGridColumn
		void _SetColumnHeaders()
		{
			ColumnHeaders.Add("DataList_CarBrand",
				new List<string>
				{
					"№ п.п.",
					"Производитель"
				});

			ColumnHeaders.Add("DataList_CarModel",
				new List<string>
				{
					"№ п.п.",
					"Производитель",
					"Модель"
				});

			ColumnHeaders.Add("DataList_Cars",
				new List<string>
				{
					"№ п.п.",
					"Модель",
					"Номер",
					"Инв. номер",
					"Тип ТС"
				});

			ColumnHeaders.Add("DataList_CarType",
				new List<string>
				{
					"№ п.п.",
					"Код типа",
					"Наименование типа",
					"Грузоподъемность",
					"Тариф(в работе)",
					"Тариф(в простое)"
				});

			ColumnHeaders.Add("DataList_Drivers",
				new List<string>
				{
					"№ п.п.",
					"Имя",
					"Фамилия",
					"Отчество",
					"Удостоверение",
					"Класс"
				});

			ColumnHeaders.Add("DataList_Estimate",
				new List<string>
				{
					"№ п.п.",
					"№ пачки",
					"№ заказа",
					"№ п.л.",
					"Дата п.л.",
					"Лот",
					"Пробег",
					"Гос.№.",
					"Модель",
					"Грузопод-сть",
					"Тип ТС",
					"Код типа ТС",
					"Часов в работе",
					"Тариф(В работе)",
					"Часов в выходные",
					"Сумма(В работе)",
					"Часов в ожидании",
					"Тариф(В ожидании)",
					"Часов в выходные/П",
					"Сумма(В ожидании)",
					"Итого"
				});

			ColumnHeaders.Add("DataList_Organizations",
				new List<string>
				{
					"№ п.п.",
					"Организация",
					"Отношение"
				});

			ColumnHeaders.Add("DataList_Staff",
				new List<string>
				{
					"№ п.п.",
					"Имя",
					"Фамилия",
					"Отчество",
					"Должность"
				});

			ColumnHeaders.Add("DataList_StaffPosition",
				new List<string>
				{
					"№ п.п.",
					"Должность"
				});

			ColumnHeaders.Add("DataList_Trackinglists",
				new List<string>
				{
					"№ п.п.",
					"Серия",
					"Номер",
					"Дата",
					"Ген. подрядчик",
					"Заказчик",
					"Автомобиль",
					"Водитель",
					"Диспетчер",
					"Медик",
					"Механик"
				});
		}
		#endregion
	}
}
