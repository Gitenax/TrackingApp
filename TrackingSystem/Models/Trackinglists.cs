using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using TrackingSystem.Helpers;
using System.Diagnostics;

namespace TrackingSystem.Models
{
	enum ReportType
	{
		Trackinglist,
		Reference,
		Registry,
		Invoice,
		Act
	}

	class Trackinglists : Base
	{
		// Свойства класса(таблицы базы данных)
		public string ID { get; set; } // ID Строки(№ пп)
		public string Series { get; set; } // Серия путевого листа
		public string Number { get; set; } // Номер путевого листа
		public string Date { get; set; } // Дата создания
		public string Organization { get; set; } // Организация составитель
		public string Transport { get; set; } // Автомобиль
		public string Driver { get; set; } // Водитель
		public string Dispatcher { get; set; } // Диспетчер
		public string Medic { get; set; } // Медик
		public string Mechanic { get; set; } // Механик
		public string Organization_2 { get; set; } // Организация Ген. подрядчик
		public string Organization_3 { get; set; } // Организация Заказчик


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
					CommandText = "INSERT INTO Trackinglists(Trackinglist_series, Trackinglist_number, Trackinglist_date, Trackinglist_organization, Trackinglist_transport, Trackinglist_driver, Trackinglist_dispatcher, Trackinglist_medic, Trackinglist_mechanic, Trackinglist_organization_2, Trackinglist_organization_3) " +
												"VALUES(@Series, @Number, @Date, @Organization, @Transport, @Driver, @Dispatcher, @Medic, @Mechanic, @Organization_2, @Organization_3)"
				};

				// Параметры
				command.Parameters.AddWithValue("@Series", Series);
				command.Parameters.AddWithValue("@Number", Number);
				command.Parameters.AddWithValue("@Date", Date);
				command.Parameters.AddWithValue("@Organization", Organization);
				command.Parameters.AddWithValue("@Transport", Transport);
				command.Parameters.AddWithValue("@Driver", Driver);
				command.Parameters.AddWithValue("@Dispatcher", Dispatcher);
				command.Parameters.AddWithValue("@Medic", Medic);
				command.Parameters.AddWithValue("@Mechanic", Mechanic);
				command.Parameters.AddWithValue("@Organization_2", Organization_2);
				command.Parameters.AddWithValue("@Organization_3", Organization_3);

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
				"UPDATE Trackinglists SET " +
				"Trackinglist_series=@Series, " +
				"Trackinglist_number=@Number, " +
				"Trackinglist_date=@Date, " +
				"Trackinglist_organization=@Organization, " +
				"Trackinglist_transport=@Transport, " +
				"Trackinglist_driver=@Driver, " +
				"Trackinglist_dispatcher=@Dispatcher, " +
				"Trackinglist_medic=@Medic, " +
				"Trackinglist_mechanic=@Mechanic, " +
				"Trackinglist_organization_2=@Organization_2, " +
				"Trackinglist_organization_3=@Organization_3 " +
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
				command.Parameters.AddWithValue("@Series", Series);
				command.Parameters.AddWithValue("@Number", Number);
				command.Parameters.AddWithValue("@Date", Date);
				command.Parameters.AddWithValue("@Organization", Organization);
				command.Parameters.AddWithValue("@Transport", Transport);
				command.Parameters.AddWithValue("@Driver", Driver);
				command.Parameters.AddWithValue("@Dispatcher", Dispatcher);
				command.Parameters.AddWithValue("@Medic", Medic);
				command.Parameters.AddWithValue("@Mechanic", Mechanic);
				command.Parameters.AddWithValue("@Organization_2", Organization_2);
				command.Parameters.AddWithValue("@Organization_3", Organization_3);

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
					CommandText = $"DELETE FROM Trackinglists WHERE ID={ID}"
				};

				// Исполнение
				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Полечение последнего номера путевого лист
		/// </summary>
		/// <returns>Возвращает последний путевой лист</returns>
		public static int GetLastTrackingNumber()
		{
			string Query = "SELECT MAX(Trackinglist_number) FROM Trackinglists";
			int result = 0;

			object temp = 0;
			Connection.Open();
			Command = new OleDbCommand(Query, Connection);
			Reader = Command.ExecuteReader();

			while (Reader.Read())
			{
				temp = Reader.GetValue(0);
			}
			Connection.Close();

			if (int.TryParse(temp.ToString(), out result))
				return result;
			else
				return 0;
		}

		/// <summary>
		/// Генерация отчета
		/// </summary>
		/// <param name="ID">Идентификатор путевого листа</param>
		/// <param name="type">Тип отчета</param>
		public static void Report(ReportType type, string folderPath = "", int ID = -1, int[] ArrayOfIDs = null)
		{
			switch (type)
			{
				case ReportType.Trackinglist:
					#region Путевой лист
					// +----------------------------------------+
					// |					Объявление переменных					|
					// +----------------------------------------+
					var Data = new Dictionary<string, string>();
					var MoreData = new List<Dictionary<string, string>>();
					Word.Application App = new Word.Application() { Visible = false };
					Word.Document Doc = new Word.Document();

					// +----------------------------------------+
					// | Копирование шаблона в выбранную папку	|
					// |					и получение данных						|
					// +----------------------------------------+
					try {
						if(ArrayOfIDs != null)
						{
							for(int i = 0; i < ArrayOfIDs.Length; i++)
							{
								File.Copy(Application.StartupPath + @"/Reports/Template/TL.doc", folderPath + $@"/Путевой лист_{i + 1}.doc");
								MoreData.Add(GetData(ArrayOfIDs[i]));
							}
						}
						else
						{
							Data = GetData(ID);
							File.Copy(Application.StartupPath + @"/Reports/Template/TL.doc", folderPath + @"/Путевой лист.doc");
						}
					}
					catch (Exception e)
					{
						File.Delete(folderPath + @"/Путевой лист.doc");
						Doc.Close();
						App.Quit();
						MessageBox.Show("Не выбран путевой лист для создания отчета\n" + e.Message, "Создать отчет", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}

					// +----------------------------------------+
					// |					Оформление отчета							|
					// +----------------------------------------+
					// Замена
					if (ArrayOfIDs == null)
					{
						try
						{
							// Инициализаця приложения
							Doc = App.Documents.Open(folderPath + @"/Путевой лист.doc");

							// Замена содержимого
							Replace("{@Trackinglist_series}", "__" + Data["TL_Series"] + "__"); // Серия
							Replace("{@Trackinglist_number}", "__" + Data["TL_Number"] + "__"); // Номер
							Replace("{@date_d}", Data["TL_Date_D"]); // Дата - день
							Replace("{@date_m}", "____" + Data["TL_Date_M"] + "____"); // Дата - месяц
							Replace("{@date_y}", Data["TL_Date_Y"]); // Дата - год
							Replace("{@Trackinglist_organization}", Data["TL_Organization"]); // Ген. подрядчик x1
							Replace("{@Car_name}", Data["TL_CarName"]); // Автомобиль x1
							Replace("{@Car_number}", Data["TL_CarNumber"]); // Номер авто x1
							Replace("{@Driver_name}", Data["TL_DriverName"]); // Имя водителя 
							Replace("{@Driver_licence}", Data["TL_DriverLicense"]); // Удостоверение водителя
							Replace("{@Driver_class}", Data["TL_DriverClass"]); // Класс удостоверения
							Replace("{@dispatcher_short}", Data["TL_Dispatcher"]); // Диспетчер
							Replace("{@medic_short}", Data["TL_Medic"]); // Медик
							Replace("{@mechanic_short}", Data["TL_Mechanic"]); // Механик
							Replace("{@driver_short}", Data["TL_DriverName"]); // Имя водителя(сокр.)
							Replace("{@Trackinglist_organization}", Data["TL_Organization"]); // Ген. подрядчик x2
							Replace("{@Car_name}", Data["TL_CarName"]); // Автомобиль x2
							Replace("{@Car_number}", Data["TL_CarNumber"]); // Номер авто x2
							Replace("{@Trackinglist_organization_another}", Data["TL_Organization_2"]); // Суб. подрядчик x1
							Replace("{@Trackinglist_organization_another}", Data["TL_Organization_2"]); // Суб. подрядчик x2
							Replace("{@Car_name}", Data["TL_CarName"]); // Автомобиль x3
							Replace("{@Car_number}", Data["TL_CarNumber"]); // Номер авто x3

							Doc.Save();
							App.Visible = true;
						}
						catch
						{
							// [Костыль] - Заглушка
							try { Doc.Close(); App.Quit(); } catch { }
						}
					}
					else
					{
						for (int i = 0; i < ArrayOfIDs.Length; i++)
						{
							try
							{
								// Инициализаця приложения
								Doc = App.Documents.Open(folderPath + $@"/Путевой лист_{i + 1}.doc");

								// Замена содержимого
								Replace("{@Trackinglist_series}", "__" + MoreData[i]["TL_Series"] + "__"); // Серия
								Replace("{@Trackinglist_number}", "__" + MoreData[i]["TL_Number"] + "__"); // Номер
								Replace("{@date_d}", MoreData[i]["TL_Date_D"]); // Дата - день
								Replace("{@date_m}", "____" + MoreData[i]["TL_Date_M"] + "____"); // Дата - месяц
								Replace("{@date_y}", MoreData[i]["TL_Date_Y"]); // Дата - год
								Replace("{@Trackinglist_organization}", MoreData[i]["TL_Organization"]); // Ген. подрядчик x1
								Replace("{@Car_name}", MoreData[i]["TL_CarName"]); // Автомобиль x1
								Replace("{@Car_number}", MoreData[i]["TL_CarNumber"]); // Номер авто x1
								Replace("{@Driver_name}", MoreData[i]["TL_DriverName"]); // Имя водителя 
								Replace("{@Driver_licence}", MoreData[i]["TL_DriverLicense"]); // Удостоверение водителя
								Replace("{@Driver_class}", MoreData[i]["TL_DriverClass"]); // Класс удостоверения
								Replace("{@dispatcher_short}", MoreData[i]["TL_Dispatcher"]); // Диспетчер
								Replace("{@medic_short}", MoreData[i]["TL_Medic"]); // Медик
								Replace("{@mechanic_short}", MoreData[i]["TL_Mechanic"]); // Механик
								Replace("{@driver_short}", MoreData[i]["TL_DriverName"]); // Имя водителя(сокр.)
								Replace("{@Trackinglist_organization}", MoreData[i]["TL_Organization"]); // Ген. подрядчик x2
								Replace("{@Car_name}", MoreData[i]["TL_CarName"]); // Автомобиль x2
								Replace("{@Car_number}", MoreData[i]["TL_CarNumber"]); // Номер авто x2
								Replace("{@Trackinglist_organization_another}", MoreData[i]["TL_Organization_2"]); // Суб. подрядчик x1
								Replace("{@Trackinglist_organization_another}", MoreData[i]["TL_Organization_2"]); // Суб. подрядчик x2
								Replace("{@Car_name}", MoreData[i]["TL_CarName"]); // Автомобиль x3
								Replace("{@Car_number}", MoreData[i]["TL_CarNumber"]); // Номер авто x3

								Doc.Save();
								//App.Visible = true;
							}
							catch
							{
								// [Костыль] - Заглушка
								try { Doc.Close(); App.Quit(); } catch { }
							}
						}
						App.Quit();

						Process proc1 = new Process();
						proc1.StartInfo.FileName = "explorer";
						proc1.StartInfo.Arguments = folderPath;
						proc1.Start();
					}
					void Replace(string FWord, string SWord)
					{
						// Получение всего текста в документе
						var Content = Doc.Content;
						Content.Find.ClearFormatting();

						// Замена строк
						Content.Find.Execute(FindText: FWord, ReplaceWith: SWord);
					}
					break;
				#endregion
				////////////////////////////////// [ СПРАВКА ] //////////////////////////////////
				case ReportType.Reference:
					#region Справка
					// Переменные
					string ReferenceTemplate = Application.StartupPath + @"/Reports/Template/RF.xlsx";
					var RefData = new Dictionary<string, string>();
					var RefApp = new Excel.Application() { Visible = false };
					Excel.Workbook RefBook;
					Excel.Worksheet RefSheet;
					string Client= "";
					if (AddClient == true) Client = DataCommon.GetFieldValue(DataCommon.Values[0].exToInt(), "Organization_name", "Organizations");


					try { RefData = GetData(ID); } catch (Exception e) { RefApp.Quit(); MessageBox.Show("Не выбран путевой лист для создания справки" + e.Message, "Создать справку", MessageBoxButtons.OK, MessageBoxIcon.Information); }

					if (File.Exists(folderPath + @"/Справка_ГенПодрядчик.xlsx"))
						File.Delete(folderPath + @"/Справка_ГенПодрядчик.xlsx");

					if(AddClient == true)
					{
						if (File.Exists(folderPath + @"/Справка_Заказчик.xlsx"))
							File.Delete(folderPath + @"/Справка_Заказчик.xlsx");
					}

					File.Copy(ReferenceTemplate, folderPath + @"/Справка_ГенПодрядчик.xlsx");
					if(AddClient == true) File.Copy(ReferenceTemplate, folderPath + @"/Справка_Заказчик.xlsx");


					try
					{
						// Открытие шаблона
						RefBook = RefApp.Workbooks.Open(folderPath + @"/Справка_ГенПодрядчик.xlsx");
						RefSheet = RefBook.ActiveSheet as Excel.Worksheet;
						RefSheet.Name = "Справка";

						#region Заполнение данных
						// Номер путевого листа
						RefSheet.Range["F2"].Value =
						RefSheet.Range["F16"].Value = RefData["TL_Number"];

						// Ген подр.
						RefSheet.Range["C4"].Value =
						RefSheet.Range["C18"].Value = RefData["TL_Organization"];

						// [Дата] - день
						RefSheet.Range["C5"].Value =
						RefSheet.Range["C19"].Value = RefData["TL_Date_D"];

						// [Дата] - месяц
						RefSheet.Range["D5"].Value =
						RefSheet.Range["D19"].Value = RefData["TL_Date_M"];

						// [Дата] - Год
						RefSheet.Range["G5"].Value =
						RefSheet.Range["G19"].Value = RefData["TL_Date_Y"] + " года";

						// ФИО Водителя
						RefSheet.Range["C6"].Value =
						RefSheet.Range["C20"].Value = RefData["TL_DriverName"];

						// Марка авто
						RefSheet.Range["C7"].Value =
						RefSheet.Range["C21"].Value = RefData["TL_CarName"];

						// Гос. номер
						RefSheet.Range["G7"].Value =
						RefSheet.Range["G21"].Value = RefData["TL_CarNumber"];

						// Заказчик
						RefSheet.Range["C8"].Value =
						RefSheet.Range["C22"].Value = RefData["TL_Organization_2"];
						#endregion

						RefBook.Save();

						if (AddClient == true)
						{
							RefBook = RefApp.Workbooks.Open(folderPath + @"/Справка_Заказчик.xlsx");
							RefSheet = RefBook.ActiveSheet as Excel.Worksheet;
							RefSheet.Name = "Справка";

							#region Заполнение данных
							// Номер путевого листа
							RefSheet.Range["F2"].Value =
							RefSheet.Range["F16"].Value = RefData["TL_Number"];

							// Ген подр.
							RefSheet.Range["C4"].Value =
							RefSheet.Range["C18"].Value = RefData["TL_Organization_2"];

							// [Дата] - день
							RefSheet.Range["C5"].Value =
							RefSheet.Range["C19"].Value = RefData["TL_Date_D"];

							// [Дата] - месяц
							RefSheet.Range["D5"].Value =
							RefSheet.Range["D19"].Value = RefData["TL_Date_M"];

							// [Дата] - Год
							RefSheet.Range["G5"].Value =
							RefSheet.Range["G19"].Value = RefData["TL_Date_Y"] + " года";

							// ФИО Водителя
							RefSheet.Range["C6"].Value =
							RefSheet.Range["C20"].Value = RefData["TL_DriverName"];

							// Марка авто
							RefSheet.Range["C7"].Value =
							RefSheet.Range["C21"].Value = RefData["TL_CarName"];

							// Гос. номер
							RefSheet.Range["G7"].Value =
							RefSheet.Range["G21"].Value = RefData["TL_CarNumber"];

							// Заказчик
							RefSheet.Range["C8"].Value =
							RefSheet.Range["C22"].Value = Client;
							#endregion

							RefBook.Save();
						}

						RefBook.Close();
						RefApp.Quit();
					}
					catch
					{
						// [Костыль] - Заглушка
						try { RefApp.Quit(); } catch { }
					}

					Process proc = new Process();
					proc.StartInfo.FileName = "explorer";
					proc.StartInfo.Arguments = folderPath;
					proc.Start();

					break;
				#endregion
				/////////////////////////////////// [ РЕЕСТР ] //////////////////////////////////
				case ReportType.Registry:
					#region Реестр услуг
					// Переменные
					string GenRTemplate = Application.StartupPath + @"/Reports/Template/GNR.xlsx";
					var GenRData = new Dictionary<string, string>();
					var GenRApp = new Excel.Application() { Visible = false };
					Excel.Workbook GenRBook;
					Excel.Worksheet GenRSheet;

					// Даннные с формы Duration
					string startDate = DataCommon.Values[0].ToString(); // Начало периода
					string endDate = DataCommon.Values[1].ToString(); // Конец периода
					string organization = DataCommon.GetFieldValue((int)DataCommon.Values[2], "Organization_name", "Organizations");
					// Открытие шаблона
					GenRBook = GenRApp.Workbooks.Open(GenRTemplate);
					GenRSheet = GenRBook.ActiveSheet as Excel.Worksheet;
					GenRSheet.Name = "Реестр";


					#region Заполнение данных
					// Период "c по" 
					GenRSheet.Range["J2"].Value = startDate + " - " + endDate;

					GenRSheet.Range["D3"].Value = $"к счет-фактуре №Стр - С110  от {endDate} г. при расчетах за повременное пользование транспортными средствами";

					// Подрядчик
					GenRSheet.Range["B7"].Value = "     " + organization;
					GenRSheet.Range["B39", "Y39"].Merge();
					GenRSheet.Range["B39"].Value = "     Грузовые почасовые";


					string Query = "SELECT Trackinglists.ID, Estimate.Estimate_packetnumber, Estimate.Estimate_order, Trackinglists.Trackinglist_number, Estimate.Estimate_date, Estimate.Estimate_lot, Estimate.Estimate_duration, Estimate.Estimate_inwork_time, Estimate.Estimate_inwork_rate, Estimate.Estimate_inwork_hols, Estimate.Estimate_inwork_price, Estimate.Estimate_inwait_time, Estimate.Estimate_inwait_rate, Estimate.Estimate_inwait_hols, Estimate.Estimate_inwait_price, Estimate.Estimate_finalprice " +
							"FROM Trackinglists INNER JOIN Estimate ON Trackinglists.ID = Estimate.Estimate_tracklist " +
							$"WHERE Trackinglists.Trackinglist_date BETWEEN \"{startDate}\" AND \"{endDate}\";";

					DataTable FirstTable = Fill("Estimate", Query);


					/* Получаем ID путевых листов, чтобы в последующем достать оттуда данные об авто
					 * для последующей обработки */
					int[] IDs = new int[FirstTable.Rows.Count];
					for (int i = 0; i < FirstTable.Rows.Count; i++)
						IDs[i] = (int)FirstTable.Rows[i][0]; // Получаем ID путевого листа

					/* Получаем список используемых авто */
					DataTable SecondTable = Fill("Trackinglists"); // Получаем всю таблицу с путевыми листами
					int[] autoIDs = new int[FirstTable.Rows.Count]; // Массив ID авто из путевых листов
					for (int i = 0; i < FirstTable.Rows.Count; i++)
						autoIDs[i] = Convert.ToInt32(DataCommon.GetFieldValue(IDs[i], "Trackinglist_transport", "Trackinglists")); // Получаем ID авто по конкретному путевому листу

					// Удаляем повторяющиеся значения
					autoIDs = autoIDs.Distinct().ToArray();

					// Хранилище информации об авто. int - это ID авто, string[] - поля
					var AutoFields = new Dictionary<int, string[]>();
					// Привязка ID авто к путевым листам. int - это ID авто, int[] - ID путевых листов
					var AutoFieldsTracking = new Dictionary<int, int[]>();
					for (int i = 0; i < autoIDs.Length; i++)
					{
						string[] aboutAutoData = GetAutoName(autoIDs[i]); // Получение дополнительных полей об авто
						int[] trackingLists = GetTrackingLists(autoIDs[i]); // Получение всех связанных путевых листов с ID автомобиля
						AutoFields.Add(autoIDs[i], aboutAutoData);
						AutoFieldsTracking.Add(autoIDs[i], trackingLists);
					}

					/* [Начало динамического заполнения]  */
					// Преобразование DataTable в массив с привязкой по ID путевых листов
					var Rows = new Dictionary<int, string[]>();
					for (int i = 0; i < FirstTable.Rows.Count; i++)
					{
						string[] _data = new string[FirstTable.Columns.Count];
						for (int j = 0; j < FirstTable.Columns.Count; j++)
						{
							_data[j] = FirstTable.Rows[i][j].ToString();
						}
						Rows.Add(Convert.ToInt32(_data[0]), _data);
					}


					int counter = 1;
					int firstRow = 41;


					int RowCount = FirstTable.Rows.Count + (AutoFields.Count * 2) + 9;

					for (int i = 0; i < RowCount; i++)
						GenRSheet.Range["B" + (40 + i)].EntireRow.Insert();

					int fullCount = 0;
					double[][] fullCounting = new double[FirstTable.Rows.Count][];
					for (int i = 0; i < AutoFields.Count; i++)
					{
						string[] aboutAuto = AutoFields[autoIDs[i]].ToArray();

						// Получение массива с ID путевых листов
						int[] _IDs = AutoFieldsTracking[autoIDs[i]].ToArray();

						// Первая строка с авто
						GenRSheet.Range["B" + (firstRow - 1), "Y" + (firstRow - 1)].Merge();
						GenRSheet.Range["B" + (firstRow - 1)].Value = "     " + aboutAuto[0]; // Первая строчка блока(Номер и тип авто)
						GenRSheet.Range["B" + (firstRow - 1)].Font.Bold = false;
						GenRSheet.Range["B" + (firstRow - 1)].Font.Size = 9;

						int secCounter = 0;
						double[][] counting = new double[_IDs.Length][];

						foreach (int val in _IDs)
						{
							string[] values = Rows[val].ToArray(); // Получение данных о смете


							GenRSheet.Range["B" + firstRow, "Y" + firstRow].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
							GenRSheet.Range["B" + firstRow, "Y" + firstRow].Font.Size = 8;
							GenRSheet.Range["B" + firstRow, "Y" + firstRow].Font.Italic = false;
							GenRSheet.Range["B" + firstRow, "Y" + firstRow].Font.Bold = false;

							GenRSheet.Range["B" + firstRow].Value = counter;                      // № пп.
							GenRSheet.Range["C" + firstRow].Value = values[1];                    // № пачки
							GenRSheet.Range["D" + firstRow].Value = values[2];                    // № заказа
							GenRSheet.Range["E" + firstRow].Value = values[3];                    // № путевого листа
							GenRSheet.Range["F" + firstRow].Value = values[4];                    // Дата путевого листа
							GenRSheet.Range["G" + firstRow].Value = values[5];                    // Лот
							GenRSheet.Range["H" + firstRow].Value = values[6];                    // Пробег
							GenRSheet.Range["I" + firstRow].Value = aboutAuto[1];                 // Гос №
							GenRSheet.Range["J" + firstRow].Value = aboutAuto[2];                 // Модель
							GenRSheet.Range["K" + firstRow].Value = aboutAuto[3];                 // грузоподъемность
							GenRSheet.Range["L" + firstRow].Value = aboutAuto[4];                 // Тип ТС
							GenRSheet.Range["M" + firstRow].Value = aboutAuto[5];                 // Код типа ТС
							GenRSheet.Range["N" + firstRow].Value = (values[7] == "0") ? "-" : values[7];                    // В работе кол-во
							GenRSheet.Range["O" + firstRow].Value = (values[8] == "0") ? "-" : values[8];                    // Тариф
							GenRSheet.Range["P" + firstRow].Value = (values[9] == "0") ? "-" : values[9];                    // Вых
							GenRSheet.Range["Q" + firstRow].Value = (values[10] == "0") ? "-" : values[10];                     // Сумма
							GenRSheet.Range["R" + firstRow].Value = (values[11] == "0") ? "-" : values[11];                     // В ожидании кол-во
							GenRSheet.Range["S" + firstRow].Value = (values[12] == "0") ? "-" : values[12];                     // Тариф 
							GenRSheet.Range["T" + firstRow].Value = (values[13] == "0") ? "-" : values[13];                     // Вых
							GenRSheet.Range["U" + firstRow].Value = (values[14] == "0") ? "-" : values[14];                     // Сумма
							GenRSheet.Range["Y" + firstRow].Value = (values[15] == "0") ? "-" : values[15];                      // Итого

							// Выравнивание
							GenRSheet.Range["N" + firstRow, "Y" + firstRow].Select();
							GenRSheet.Range["N" + firstRow, "Y" + firstRow].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

							counter++;
							firstRow++;

							// +-----------------------------------------------------------------+
							// |                    СУМИРОВАНИЕ ЗНАЧЕНИЙ                         |
							// +-----------------------------------------------------------------+
							#region Сумирование значений
							counting[secCounter] = new double[9];
							counting[secCounter][0] = Convert.ToDouble(values[7]);
							counting[secCounter][1] = Convert.ToDouble(values[8]);
							counting[secCounter][2] = Convert.ToDouble(values[9]);
							counting[secCounter][3] = Convert.ToDouble(values[10]);
							counting[secCounter][4] = Convert.ToDouble(values[11]);
							counting[secCounter][5] = Convert.ToDouble(values[12]);
							counting[secCounter][6] = Convert.ToDouble(values[13]);
							counting[secCounter][7] = Convert.ToDouble(values[14]);
							counting[secCounter][8] = Convert.ToDouble(values[15]);

							fullCounting[fullCount] = new double[9];
							fullCounting[fullCount][0] = Convert.ToDouble(values[7]);
							fullCounting[fullCount][1] = Convert.ToDouble(values[8]);
							fullCounting[fullCount][2] = Convert.ToDouble(values[9]);
							fullCounting[fullCount][3] = Convert.ToDouble(values[10]);
							fullCounting[fullCount][4] = Convert.ToDouble(values[11]);
							fullCounting[fullCount][5] = Convert.ToDouble(values[12]);
							fullCounting[fullCount][6] = Convert.ToDouble(values[13]);
							fullCounting[fullCount][7] = Convert.ToDouble(values[14]);
							fullCounting[fullCount][8] = Convert.ToDouble(values[15]);

							fullCount++;
							secCounter++;
							#endregion
						}

						GenRSheet.Range["B" + firstRow, "L" + firstRow].Merge();
						GenRSheet.Range["B" + firstRow].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
						GenRSheet.Range["B" + firstRow].Value = "Итого по транспортному средству :";
						GenRSheet.Range["B" + firstRow].Font.Bold = false;

						double[] result = new double[counting[0].Length];
						for (int v = 0; v < result.Length; v++)
							for (int vi = 0; vi < counting.Length; vi++) result[v] += counting[vi][v];


						GenRSheet.Range["N" + firstRow, "Y" + firstRow].Font.Size = 8;
						GenRSheet.Range["N" + firstRow, "Y" + firstRow].Font.Bold = true;
						GenRSheet.Range["N" + firstRow, "Y" + firstRow].Font.Italic = true;
						GenRSheet.Range["N" + firstRow, "Y" + firstRow].Select();
						GenRSheet.Range["N" + firstRow, "Y" + firstRow].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

						GenRSheet.Range["N" + firstRow].Value = (result[0].ToString() == "0") ? "" : result[0].ToString();
						//GenRSheet.Range["O" + firstRow].Value = (result[1].ToString() == "0") ? "" : result[1].ToString();
						GenRSheet.Range["P" + firstRow].Value = (result[2].ToString() == "0") ? "" : result[2].ToString();
						GenRSheet.Range["Q" + firstRow].Value = (result[3].ToString() == "0") ? "" : result[3].ToString();
						GenRSheet.Range["R" + firstRow].Value = (result[4].ToString() == "0") ? "" : result[4].ToString();
						//GenRSheet.Range["S" + firstRow].Value = (result[5].ToString() == "0") ? "" : result[5].ToString();
						GenRSheet.Range["T" + firstRow].Value = (result[6].ToString() == "0") ? "" : result[6].ToString();
						GenRSheet.Range["U" + firstRow].Value = (result[7].ToString() == "0") ? "" : result[7].ToString();
						GenRSheet.Range["Y" + firstRow].Value = (result[8].ToString() == "0") ? "" : result[8].ToString();

						// Подсчет общих значений
						firstRow += 2;
					}


					// +-----------------------------------------------------------------+
					// |                     ПОДВЕДЕНИЕ ИТОГОВ                           |
					// +-----------------------------------------------------------------+
					#region Подведение итогов
					double[] fullResult = new double[fullCounting[0].Length];
					for (int v = 0; v < fullResult.Length; v++)
						for (int vi = 0; vi < fullCounting.Length; vi++) fullResult[v] += fullCounting[vi][v];


					GenRSheet.Range["B" + firstRow].EntireRow.Insert();
					GenRSheet.Range["B" + firstRow].EntireRow.Insert();

					GenRSheet.Range["B" + firstRow, "L" + firstRow].Merge();
					GenRSheet.Range["B" + firstRow].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
					GenRSheet.Range["B" + firstRow].Value = "Итого по подразделению:";

					GenRSheet.Range["B" + (firstRow + 1), "L" + (firstRow + 1)].Merge();
					GenRSheet.Range["B" + (firstRow + 1)].Font.Bold = true;
					GenRSheet.Range["B" + (firstRow + 1)].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
					GenRSheet.Range["B" + (firstRow + 1)].Value = "Итого по реестру :";

					// Вставка итоговых подсчетов
					GenRSheet.Range["N" + firstRow, "Y" + firstRow].Font.Size = 8;
					GenRSheet.Range["N" + (firstRow + 1), "Y" + (firstRow + 1)].Font.Size = 8;

					GenRSheet.Range["N" + firstRow].Value = (fullResult[0].ToString() == "0") ? "" : fullResult[0].ToString();
					//GenRSheet.Range["O" + firstRow].Value = (result[1].ToString() == "0") ? "" : result[1].ToString();
					GenRSheet.Range["P" + firstRow].Value = (fullResult[2].ToString() == "0") ? "" : fullResult[2].ToString();
					GenRSheet.Range["Q" + firstRow].Value = (fullResult[3].ToString() == "0") ? "" : fullResult[3].ToString();
					GenRSheet.Range["R" + firstRow].Value = (fullResult[4].ToString() == "0") ? "" : fullResult[4].ToString();
					//GenRSheet.Range["S" + firstRow].Value = (result[5].ToString() == "0") ? "" : result[5].ToString();
					GenRSheet.Range["T" + firstRow].Value = (fullResult[6].ToString() == "0") ? "" : fullResult[6].ToString();
					GenRSheet.Range["U" + firstRow].Value = (fullResult[7].ToString() == "0") ? "" : fullResult[7].ToString();
					GenRSheet.Range["Y" + firstRow].Value = (fullResult[8].ToString() == "0") ? "" : fullResult[8].ToString();

					GenRSheet.Range["N" + (firstRow + 1)].Value = (fullResult[0].ToString() == "0") ? "" : fullResult[0].ToString();
					//GenRSheet.Range["O" + (firstRow + 1)].Value = (fullResult[1].ToString() == "0") ? "" : result[1].ToString();
					GenRSheet.Range["P" + (firstRow + 1)].Value = (fullResult[2].ToString() == "0") ? "" : fullResult[2].ToString();
					GenRSheet.Range["Q" + (firstRow + 1)].Value = (fullResult[3].ToString() == "0") ? "" : fullResult[3].ToString();
					GenRSheet.Range["R" + (firstRow + 1)].Value = (fullResult[4].ToString() == "0") ? "" : fullResult[4].ToString();
					//GenRSheet.Range["S" + (firstRow + 1)].Value = (fullResult[5].ToString() == "0") ? "" : result[5].ToString();
					GenRSheet.Range["T" + (firstRow + 1)].Value = (fullResult[6].ToString() == "0") ? "" : fullResult[6].ToString();
					GenRSheet.Range["U" + (firstRow + 1)].Value = (fullResult[7].ToString() == "0") ? "" : fullResult[7].ToString();
					GenRSheet.Range["Y" + (firstRow + 1)].Value = (fullResult[8].ToString() == "0") ? "" : fullResult[8].ToString();
					#endregion

					#region Вставка Итого
					firstRow += 1;

					GenRSheet.Range["N" + (firstRow + 2), "U" + (firstRow + 2)].Merge();
					GenRSheet.Range["N" + (firstRow + 3), "U" + (firstRow + 3)].Merge();
					GenRSheet.Range["N" + (firstRow + 4), "U" + (firstRow + 4)].Merge();

					GenRSheet.Range["N" + (firstRow + 2)].Font.Italic = false;
					GenRSheet.Range["N" + (firstRow + 3)].Font.Italic = false;
					GenRSheet.Range["N" + (firstRow + 4)].Font.Italic = false;

					GenRSheet.Range["N" + (firstRow + 2)].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
					GenRSheet.Range["N" + (firstRow + 3)].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
					GenRSheet.Range["N" + (firstRow + 4)].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

					GenRSheet.Range["N" + (firstRow + 2)].Value = "Итого сумма";
					GenRSheet.Range["N" + (firstRow + 3)].Value = "Итого НДС";
					GenRSheet.Range["N" + (firstRow + 4)].Value = "Всего с НДС";

					double final = fullResult[8] + (fullResult[8] * 0.18);

					GenRSheet.Range["Y" + (firstRow + 2)].Value = fullResult[8];
					GenRSheet.Range["Y" + (firstRow + 3)].Value = fullResult[8] * 0.18;
					GenRSheet.Range["Y" + (firstRow + 4)].Value = final;

					for (int i = RowCount - 9; i < RowCount; i++)
					{
						GenRSheet.Range["B" + i, "Y" + i].Select();
						GenRSheet.Range["B" + i, "Y" + i].Borders.LineStyle = Excel.XlLineStyle.xlDouble;
					}
					#endregion


					// +-----------------------------------------------------------------+
					// |                     СОХРАНЕНИЕ ФАЙЛА                            |
					// +-----------------------------------------------------------------+
					#region Сохранение файла
					if (File.Exists(Application.StartupPath + @"/Reports/Estimate.xlsx"))
						File.Delete(Application.StartupPath + @"/Reports/Estimate.xlsx");

					GenRBook.SaveAs(Application.StartupPath + @"/Reports/Estimate.xlsx");
					GenRApp.Visible = true;
					#endregion

					#endregion


					#endregion
					break;

					// Внутренняя функция, для получения наименования авто
					string[] GetAutoName(int carID)
					{

						Connection.Open();
						string SelectQuery = "SELECT [Cars]![Car_number] & \" \" & [CarType]![Cartype_name] AS Выражение1, Cars.Car_number, [CarBrand]![Carbrand_name] & \" \" & [CarModel]![Carmodel_name] AS Выражение2, CarType.Cartype_capacity, CarType.Cartype_name, CarType.Cartype_code " +
							"FROM CarType INNER JOIN((CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand) INNER JOIN Cars ON CarModel.ID = Cars.Car_model) ON CarType.ID = Cars.Car_type " +
							$"WHERE(((Cars.ID) = {carID}))";

						Command = new OleDbCommand(SelectQuery, Connection);
						Reader = Command.ExecuteReader();
						string[] Result = new string[Reader.FieldCount];

						while (Reader.Read())
						{
							Result[0] = Reader.GetValue(0).ToString();
							Result[1] = Reader.GetValue(1).ToString();
							Result[2] = Reader.GetValue(2).ToString();
							Result[3] = Reader.GetValue(3).ToString();
							Result[4] = Reader.GetValue(4).ToString();
							Result[5] = Reader.GetValue(5).ToString();
						}

						Connection.Close();

						return Result;
					}

					// Внутренняя функция, для получения соответствия ID авто к путевым листам
					int[] GetTrackingLists(int carID)
					{
						var Result = new List<int>();
						Connection.Open();
						string SelectQuery = "SELECT Trackinglists.ID " +
							"FROM(Cars INNER JOIN Trackinglists ON Cars.ID = Trackinglists.Trackinglist_transport) INNER JOIN Estimate ON Trackinglists.ID = Estimate.Estimate_tracklist " +
							$"WHERE(((Trackinglists.Trackinglist_transport) = {carID}) AND((Trackinglists.Trackinglist_date)Between \"{startDate}\" And \"{endDate}\"))";

						Command = new OleDbCommand(SelectQuery, Connection);
						Reader = Command.ExecuteReader();

						while (Reader.Read())
							Result.Add((int)Reader.GetValue(0));

						Connection.Close();

						return Result.ToArray();
					}
			}

			Dictionary<string, string> GetData(int currentID)
			{
				var Params = new Dictionary<string, string>();

				// Подключение к БД и получение данных
				try
				{
					Connection.Open();
					Command = new OleDbCommand("SELECT Trackinglists.Trackinglist_series, Trackinglists.Trackinglist_number, Trackinglists.Trackinglist_date, Organizations.Organization_name, [CarBrand]![Carbrand_name] & \" \" & [CarModel]![Carmodel_name] AS Car_name, Cars.Car_number, [Drivers]![Driver_lastname] & \" \" & [Drivers]![Driver_firstname] & \" \" & [Drivers]![Driver_thirdname] AS Driver_name, Drivers.Driver_licensenumber, Drivers.Driver_class " +
						"FROM Organizations INNER JOIN(Drivers INNER JOIN(((CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand) INNER JOIN Cars ON CarModel.ID = Cars.Car_model) INNER JOIN Trackinglists ON Cars.ID = Trackinglists.Trackinglist_transport) ON Drivers.ID = Trackinglists.Trackinglist_driver) ON Organizations.ID = Trackinglists.Trackinglist_organization " +
						$"WHERE Trackinglists.ID={currentID};", Connection);

					Reader = Command.ExecuteReader();
					while (Reader.Read())
					{
						Params.Add("TL_Series", Reader.GetValue(0).ToString());
						Params.Add("TL_Number", Reader.GetValue(1).ToString());
						Params.Add("TL_Date", Reader.GetValue(2).ToString());
						Params.Add("TL_Organization", Reader.GetValue(3).ToString());
						Params.Add("TL_CarName", Reader.GetValue(4).ToString());
						Params.Add("TL_CarNumber", Reader.GetValue(5).ToString());
						Params.Add("TL_DriverName", Reader.GetValue(6).ToString());
						Params.Add("TL_DriverLicense", Reader.GetValue(7).ToString());
						Params.Add("TL_DriverClass", Reader.GetValue(8).ToString());
					}

					// Разделение даты на составляющие
					List<string> splitDate = new List<string>();
					splitDate = Params["TL_Date"].Split('.').ToList();

					Params.Add("TL_Date_D", splitDate[0]);
					Params.Add("TL_Date_M", splitDate[1]);
					Params.Add("TL_Date_Y", splitDate[2]);

					// Оргпнизация 2
					Command = new OleDbCommand("SELECT Trackinglists.Trackinglist_organization_2, Organizations.Organization_name " +
							"FROM Organizations INNER JOIN Trackinglists ON(Organizations.ID = Trackinglists.Trackinglist_organization_2) AND(Organizations.ID = Trackinglists.Trackinglist_organization_2) " +
							$"WHERE Trackinglists.ID={currentID};", Connection);

					Reader = Command.ExecuteReader();
					while (Reader.Read())
					{
						Params.Add("TL_Organization_2", Reader.GetValue(1).ToString());
					}

					// Получение персонала
					Command = new OleDbCommand("SELECT [Staff]![Staff_lastname] & \" \" & [Staff]![Staff_firstname] & \" \" & [Staff]![Staff_thirdname] AS Dispatcher " +
							"FROM Staff INNER JOIN Trackinglists ON Staff.ID = Trackinglists.Trackinglist_dispatcher " +
							$"WHERE Trackinglists.ID={currentID};", Connection);
					Reader = Command.ExecuteReader();
					while (Reader.Read())
					{
						Params.Add("TL_Dispatcher", Reader.GetValue(0).ToString());
					}
					Command = new OleDbCommand("SELECT [Staff]![Staff_lastname] & \" \" & [Staff]![Staff_firstname] & \" \" & [Staff]![Staff_thirdname] AS Dispatcher " +
							"FROM Staff INNER JOIN Trackinglists ON Staff.ID = Trackinglists.Trackinglist_medic " +
							$"WHERE Trackinglists.ID={currentID};", Connection);
					Reader = Command.ExecuteReader();
					while (Reader.Read())
					{
						Params.Add("TL_Medic", Reader.GetValue(0).ToString());
					}
					Command = new OleDbCommand("SELECT [Staff]![Staff_lastname] & \" \" & [Staff]![Staff_firstname] & \" \" & [Staff]![Staff_thirdname] AS Dispatcher " +
							"FROM Staff INNER JOIN Trackinglists ON Staff.ID = Trackinglists.Trackinglist_mechanic " +
							$"WHERE Trackinglists.ID={currentID};", Connection);

					Reader = Command.ExecuteReader();
					while (Reader.Read())
					{
						Params.Add("TL_Mechanic", Reader.GetValue(0).ToString());
					}
					Connection.Close();
				}
				catch
				{
					MessageBox.Show("Не выбран путевой лист для создания отчета\n", "Создать отчет", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Connection.Close();
				}

				return Params;
			}
		}

		public static string[] GetGeneralData(int ID)
		{
			string[] Data = new string[0];

			try
			{
				Connection.Open();
				string Query = "SELECT Trackinglists.Trackinglist_series, Trackinglists.Trackinglist_number, Trackinglists.Trackinglist_date, Organizations.Organization_name, [CarBrand]![Carbrand_name] & \" \" & [CarModel]![Carmodel_name] AS Выражение1, Cars.Car_number, [Drivers]![Driver_lastname] & \" \" & [Drivers]![Driver_firstname] & \" \" & [Drivers]![Driver_thirdname] AS Выражение2, [Staff]![Staff_lastname] & \" \" & [Staff]![Staff_firstname] & \" \" & [Staff]![Staff_thirdname] AS Выражение3, [Staff_1]![Staff_lastname] & \" \" & [Staff_1]![Staff_firstname] & \" \" & [Staff_1]![Staff_thirdname] AS Выражение4, [Staff_2]![Staff_lastname] & \" \" & [Staff_2]![Staff_firstname] & \" \" & [Staff_2]![Staff_thirdname] AS Выражение5, Organizations_1.Organization_name " +
											 "FROM Staff AS Staff_2 INNER JOIN(Staff AS Staff_1 INNER JOIN (Staff INNER JOIN (Organizations AS Organizations_1 INNER JOIN (Drivers INNER JOIN (((CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand) INNER JOIN Cars ON CarModel.ID = Cars.Car_model) INNER JOIN(Organizations INNER JOIN Trackinglists ON Organizations.ID = Trackinglists.Trackinglist_organization) ON Cars.ID = Trackinglists.Trackinglist_transport) ON Drivers.ID = Trackinglists.Trackinglist_driver) ON Organizations_1.ID = Trackinglists.Trackinglist_organization_2) ON Staff.ID = Trackinglists.Trackinglist_dispatcher) ON Staff_1.ID = Trackinglists.Trackinglist_medic) ON Staff_2.ID = Trackinglists.Trackinglist_mechanic " +
											 $"WHERE Trackinglists.ID={ID};";
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

