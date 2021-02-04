using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrackingSystem.Helpers;
using TrackingSystem.Models; // Модели описываемы таблицами БД

namespace TrackingSystem
{
  public partial class AddEdit : Form
  {
		// +---------------------------------------------------+
		// |			                ФОРМА                        |
		// +---------------------------------------------------+
		const string DB_ID = "ID"; // Константа идентификатора таблиц в БД
		int ID;
		string selectedIndex;
		DataCommon.FormType TYPE;
    public AddEdit(string page, int type, int id = -1)
    {
      InitializeComponent();
      selectedIndex = page;
			TYPE = (DataCommon.FormType)type;
			ID = id;
		}

		// +---------------------------------------------------+
		// |			            ЗАГРУЗКА ФОРМЫ                   |
		// +---------------------------------------------------+
		private void FormLoad(object sender, EventArgs e)
		{
			TabMain.ItemSize = new Size(0, 1);

			if (selectedIndex.Split('_')[1].Equals("OrganizationEstimate"))
				TabMain.SelectTab("FPage_Estimate");
			else
				TabMain.SelectTab(selectedIndex);

			object[] Values; // Для редактирования

			#region Косметические изменения формы
			int constW1 = 305;
			switch (TabMain.SelectedTab.Name)
			{
				case "FPage_CarBrand":			Width = constW1;			Height = 110;	break;
				case "FPage_CarModel":			Width = 337;					Height = 150;	break;
				case "FPage_Cars":					Width = constW1;			Height = 230;	break;
				case "FPage_CarType":				Width = constW1;			Height = 230;	break;
				case "FPage_Drivers":				Width = constW1;			Height = 275;	break;
				case "FPage_Estimate":			Width = 560;					Height = 340;	break;
				case "FPage_Organizations": Width = constW1;			Height = 160;	break;
				case "FPage_Staff":					Width = constW1;			Height = 235;	break;
				case "FPage_StaffPosition": Width = constW1;			Height = 110;	break;
				case "FPage_Trackinglists": Width = constW1;			Height = 500;	break;
			}
			Controls.Add(new Panel
			{
				BackColor = Color.White,
				Location = new Point(0, 0),
				Width = Width,
				Height = 6
			});
			TabMain.SendToBack();
			#endregion

			// Заполнение полей в зависимости от типа открываемой формы
			switch(TYPE)
			{
				case DataCommon.FormType.Add:
					switch (TabMain.SelectedTab.Name)
					{
						case "FPage_CarModel":
							Image img = new Bitmap(Properties.Resources.add, 16, 16);
							Button_AddNew_CarBrand.BackgroundImage = img;
							comboBox1.exFill(DB_ID, "Carbrand_name", "CarBrand");
							break;

						case "FPage_Cars":
							Width = constW1; Height = 230;
							comboBox2.exFillExtra(DB_ID, "CarModel.ID, [CarBrand]![Carbrand_name] & \" - \" & [CarModel]![Carmodel_name] AS Result", "CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand");
							comboBox3.exFillExtra(DB_ID, "CarType.ID,  \"[\" & [CarType]![Cartype_code] & \"] - \" & [CarType]![Cartype_name] & \" - \" & [CarType]![Cartype_capacity] AS Result", "CarType");
							break;

						case "FPage_Estimate":
							comboBox4.exFill(DB_ID, "Trackinglist_number", "Trackinglists", "Trackinglists.ID NOT IN(SELECT Estimate.Estimate_tracklist FROM Estimate)");
							break;

						case "FPage_Organizations":
							comboBox5.SelectedIndex = 0;
							break;

						case "FPage_Staff":
							comboBox6.exFill(DB_ID, "Staffposition_name", "StaffPosition");
							break;

						case "FPage_Trackinglists":
							int lastnum = Trackinglists.GetLastTrackingNumber();
							if (lastnum == 0) textBox23.Text = "0";
							else textBox23.Text = (lastnum + 1).ToString();
							comboBox7.exFill(DB_ID, "Organization_name", "Organizations", "Organization_ratio=0");
							comboBox8.exFillExtra(DB_ID, "Cars.ID, [CarBrand]![Carbrand_name] & \" \" & [CarModel]![Carmodel_name] & \", Гос. номер: \" & [Cars]![Car_number] AS Result", "(CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand) INNER JOIN Cars ON CarModel.ID = Cars.Car_model");
							comboBox9.exFillExtra(DB_ID, "Drivers.ID, [Drivers]![Driver_lastname] & \" \" & [Drivers]![Driver_firstname] & \" \" &  [Drivers]![Driver_thirdname] AS Result", "Drivers");
							comboBox10.exFillExtra(DB_ID, "Staff.ID, [Staff]![Staff_lastname] & \" \" & [Staff]![Staff_firstname] & \" \" &  [Staff]![Staff_thirdname] AS Result", "Staff");
							comboBox11.exFillExtra(DB_ID, "Staff.ID, [Staff]![Staff_lastname] & \" \" & [Staff]![Staff_firstname] & \" \" &  [Staff]![Staff_thirdname] AS Result", "Staff");
							comboBox12.exFillExtra(DB_ID, "Staff.ID, [Staff]![Staff_lastname] & \" \" & [Staff]![Staff_firstname] & \" \" &  [Staff]![Staff_thirdname] AS Result", "Staff");
							comboBox13.exFill(DB_ID, "Organization_name", "Organizations", "Organization_ratio=1");
							comboBox14.exFill(DB_ID, "Organization_name", "Organizations", "Organization_ratio=2");
							if (comboBox11.Items.Count > 0) comboBox11.SelectedIndex = 1;
							if (comboBox12.Items.Count > 0) comboBox12.SelectedIndex = 1;
							break;
					}
					break;

				case DataCommon.FormType.Edit:
					switch (TabMain.SelectedTab.Name)
					{
						case "FPage_CarBrand":
							Values = Base.GetRowFields(ID, "CarBrand");
							textBox1.Text = Values[0].ToString();
							break;

						case "FPage_CarModel":
							Values = Base.GetRowFields(ID, "CarModel");
							comboBox1.exFill(DB_ID, "Carbrand_name", "CarBrand");
							comboBox1.SelectedValue = (int)Values[0];
							textBox2.Text = Values[1].ToString();
							break;

						case "FPage_Cars":
							Values = Base.GetRowFields(ID, "Cars");
							comboBox2.exFillExtra(DB_ID, "CarModel.ID, [CarBrand]![Carbrand_name] & \" - \" & [CarModel]![Carmodel_name] AS Result", "CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand");
							comboBox3.exFillExtra(DB_ID, "CarType.ID,  \"[\" & [CarType]![Cartype_code] & \"] - \" & [CarType]![Cartype_name] & \" - \" & [CarType]![Cartype_capacity] AS Result", "CarType");
							comboBox2.SelectedValue = (int)Values[0];
							textBox3.Text = Values[1].ToString();
							textBox4.Text = Values[2].ToString();
							comboBox3.SelectedValue = (int)Values[3];
							break;

						case "FPage_CarType":
							Values = Base.GetRowFields(ID, "CarType");
							textBox5.Text = Values[0].ToString();
							textBox6.Text = Values[1].ToString();
							textBox7.Text = Values[2].ToString();
							textBox33.Text = Values[3].ToString();
							textBox32.Text = Values[4].ToString();
							break;

						case "FPage_Drivers":
							Values = Base.GetRowFields(ID, "Drivers");
							textBox9.Text = Values[0].ToString();
							textBox8.Text = Values[1].ToString();
							textBox10.Text = Values[2].ToString();
							textBox11.Text = Values[3].ToString();
							textBox16.Text = Values[4].ToString();
							break;

						case "FPage_Estimate":
							int ThisTrackingList = DataCommon.GetFieldValue(ID, "Estimate_tracklist", "Estimate").exToInt();
							Values = Base.GetRowFields(ID, "Estimate");
							comboBox4.exFill(DB_ID, "Trackinglist_number", "Trackinglists", $"Trackinglists.ID NOT IN(SELECT Estimate.Estimate_tracklist FROM Estimate WHERE Trackinglists.ID NOT IN({ThisTrackingList}))");
							textBox13.Text = Values[0].ToString();
							textBox14.Text = Values[1].ToString();
							comboBox4.SelectedValue = (int)Values[2];
							//comboBox4.Enabled = false;
							dateTimePicker1.Value = Convert.ToDateTime(Values[3]);
							textBox15.Text = Values[4].ToString();
							textBox12.Text = Values[5].ToString();//Пробег
							textBox24.Text = Values[6].ToString();
							textBox25.Text = Values[7].ToString();
							textBox26.Text = Values[8].ToString();
							textBox31.Text = Values[9].ToString();
							textBox29.Text = Values[10].ToString();
							textBox30.Text = Values[11].ToString();
							textBox27.Text = Values[12].ToString();
							textBox28.Text = Values[13].ToString();
							textBox35.Text = Values[14].ToString();
							break;

						case "FPage_Organizations":
							Values = Base.GetRowFields(ID, "Organizations");
							textBox17.Text = Values[0].ToString();
							comboBox5.SelectedIndex = (int)Values[1];
							break;

						case "FPage_Staff":
							Values = Base.GetRowFields(ID, "Staff");
							comboBox6.exFill(DB_ID, "Staffposition_name", "StaffPosition");
							textBox20.Text = Values[0].ToString();
							textBox21.Text = Values[1].ToString();
							textBox19.Text = Values[2].ToString();
							comboBox6.SelectedValue = (int)Values[3];
							break;

						case "FPage_StaffPosition":
							Values = Base.GetRowFields(ID, "StaffPosition");
							textBox18.Text = Values[0].ToString();
							break;

						case "FPage_Trackinglists":
							Values = Base.GetRowFields(ID, "Trackinglists");
							comboBox7.exFill(DB_ID, "Organization_name", "Organizations", "Organization_ratio=0");
							comboBox8.exFillExtra(DB_ID, "Cars.ID, [CarBrand]![Carbrand_name] & \" \" & [CarModel]![Carmodel_name] & \", Гос. номер: \" & [Cars]![Car_number] AS Result", "(CarBrand INNER JOIN CarModel ON CarBrand.ID = CarModel.Carmodel_brand) INNER JOIN Cars ON CarModel.ID = Cars.Car_model");
							comboBox9.exFillExtra(DB_ID, "Drivers.ID, [Drivers]![Driver_lastname] & \" \" & [Drivers]![Driver_firstname] & \" \" &  [Drivers]![Driver_thirdname] AS Result", "Drivers");
							comboBox10.exFillExtra(DB_ID, "Staff.ID, [Staff]![Staff_lastname] & \" \" & [Staff]![Staff_firstname] & \" \" &  [Staff]![Staff_thirdname] AS Result", "Staff");
							comboBox11.exFillExtra(DB_ID, "Staff.ID, [Staff]![Staff_lastname] & \" \" & [Staff]![Staff_firstname] & \" \" &  [Staff]![Staff_thirdname] AS Result", "Staff");
							comboBox12.exFillExtra(DB_ID, "Staff.ID, [Staff]![Staff_lastname] & \" \" & [Staff]![Staff_firstname] & \" \" &  [Staff]![Staff_thirdname] AS Result", "Staff");
							comboBox13.exFill(DB_ID, "Organization_name", "Organizations", "Organization_ratio=1");
							comboBox14.exFill(DB_ID, "Organization_name", "Organizations", "Organization_ratio=2");
							textBox22.Text = Values[0].ToString();
							textBox23.Text = Values[1].ToString();
							dateTimePicker2.Value = Convert.ToDateTime(Values[2]);
							comboBox7.SelectedValue = (int)Values[3];
							comboBox8.SelectedValue = (int)Values[4];
							comboBox9.SelectedValue = (int)Values[5];
							comboBox10.SelectedValue = (int)Values[6];
							comboBox11.SelectedValue = (int)Values[7];
							comboBox12.SelectedValue = (int)Values[8];
							comboBox13.SelectedValue = (int)Values[9];
							comboBox14.SelectedValue = (int)Values[10];
							break;
					}
					break;
			}

			if(TYPE == DataCommon.FormType.Edit)
			{
				// Замена событий
				Button_Add.Click -= Button_Add_Click;
				Button_Add.Click += Button_Save_Click;

				// Прочие изменения
				Text = "Редактировать запись";
				Button_Add.Text = "Сохранить";
			}
		}


		// +---------------------------------------------------+
		// |			          ДОБАВЛЕНИЕ ДАННЫХ                  |
		// +---------------------------------------------------+
    private void Button_Add_Click(object sender, EventArgs e)
    {
      switch (TabMain.SelectedTab.Name)
      {
        case "FPage_CarBrand":
					CarBrand carBrand = new CarBrand
					{
						BrandName = textBox1.Text
					};
					carBrand.AddRow();
          break;

        case "FPage_CarModel":
					CarModel carModel = new CarModel
					{
						Brand = comboBox1.SelectedValue.ToString(),
						ModelName = textBox2.Text
					};
					carModel.AddRow();
          break;

        case "FPage_Cars":
					Cars cars = new Cars
					{
						Model = comboBox2.SelectedValue.ToString(),
						Number = textBox3.Text,
						InvNumber = textBox4.Text,
						Type = comboBox3.SelectedValue.ToString()
					};
					cars.AddRow();
          break;

        case "FPage_CarType":
					CarType carType = new CarType
					{
						TypeCode = textBox5.Text,
						TypeName = textBox6.Text,
						Capacity = textBox7.Text,
						InWorkRate = textBox33.Text,
						InWaitRate = textBox32.Text
					};
					carType.AddRow();
          break;

        case "FPage_Drivers":
					Drivers drivers = new Drivers
					{
						FirstName = textBox9.Text,
						LastName = textBox8.Text,
						ThirdName = textBox10.Text,
						Licence = textBox11.Text,
						Class = textBox16.Text
					};
					drivers.AddRow();
          break;

        case "FPage_Estimate":
					Estimate estimate = new Estimate
					{
						PacketNumber = textBox13.Text,
						Order = textBox14.Text,
						TrackinglistNum = comboBox4.SelectedValue.ToString(),
						TrackinglistDate = dateTimePicker1.Value.ToShortDateString(),
						Lot = textBox15.Text,
						Duration = textBox12.Text,
						InWorkTime = textBox24.Text,
						InWorkRate = textBox25.Text,
						InWorkHols = textBox26.Text,
						InWorkPrice = textBox31.Text,
						InWaitTime = textBox29.Text,
						InWaitRate = textBox30.Text,
						InWaitHols = textBox27.Text,
						InWaitPrice = textBox28.Text,
						FinalPrice = textBox35.Text
					};
					estimate.AddRow();
          break;

        case "FPage_Organizations":
					Organisations organisations = new Organisations
					{
						Name = textBox17.Text,
						Ratio = comboBox5.SelectedIndex.ToString()
					};
					organisations.AddRow();
          break;

        case "FPage_Staff":
					Staff staff = new Staff
					{
						FirstName = textBox20.Text,
						LastName = textBox21.Text,
						ThirdName = textBox19.Text,
						Position = comboBox6.SelectedValue.ToString()
					};
					staff.AddRow();
          break;

        case "FPage_StaffPosition":
					StaffPosition staffPosition = new StaffPosition
					{
						PositionName = textBox18.Text
					};
					staffPosition.AddRow();
          break;

        case "FPage_Trackinglists":
					Trackinglists trackinglists = new Trackinglists
					{
						Series = textBox22.Text,
						Number = textBox23.Text,
						Date = dateTimePicker2.Value.ToShortDateString(), 
						Organization = comboBox7.SelectedValue.ToString(),
						Transport = comboBox8.SelectedValue.ToString(),
						Driver = comboBox9.SelectedValue.ToString(),
						Dispatcher = comboBox10.SelectedValue.ToString(),
						Medic = comboBox11.SelectedValue.ToString(),
						Mechanic = comboBox12.SelectedValue.ToString(),
						Organization_2 = comboBox13.SelectedValue.ToString(),
						Organization_3 = comboBox14.SelectedValue.ToString()
					};
					trackinglists.AddRow();
          break;
      }
    }


		// +---------------------------------------------------+
		// |			          ОБНОВЛЕНИЕ ДАННЫХ                  |
		// +---------------------------------------------------+
		private void Button_Save_Click(object sender, EventArgs e)
		{
			switch (TabMain.SelectedTab.Name)
			{
				case "FPage_CarBrand":
					CarBrand carBrand = new CarBrand
					{
						BrandName = textBox1.Text
					};
					carBrand.EditRow(ID);
					break;

				case "FPage_CarModel":
					CarModel carModel = new CarModel
					{
						Brand = comboBox1.SelectedValue.ToString(),
						ModelName = textBox2.Text
					};
					carModel.EditRow(ID);
					break;

				case "FPage_Cars":
					Cars cars = new Cars
					{
						Model = comboBox2.SelectedValue.ToString(),
						Number = textBox3.Text,
						InvNumber = textBox4.Text,
						Type = comboBox3.SelectedValue.ToString()
					};
					cars.EditRow(ID);
					break;

				case "FPage_CarType":
					CarType carType = new CarType
					{
						TypeCode = textBox5.Text,
						TypeName = textBox6.Text,
						Capacity = textBox7.Text,
						InWorkRate = textBox33.Text,
						InWaitRate = textBox32.Text

					};
					carType.EditRow(ID);
					break;

				case "FPage_Drivers":
					Drivers drivers = new Drivers
					{
						FirstName = textBox9.Text,
						LastName = textBox8.Text,
						ThirdName = textBox10.Text,
						Licence = textBox11.Text,
						Class = textBox16.Text
					};
					drivers.EditRow(ID);
					break;

				case "FPage_Estimate":
					Estimate estimate = new Estimate
					{
						PacketNumber = textBox13.Text,
						Order = textBox14.Text,
						TrackinglistNum = comboBox4.SelectedValue.ToString(),
						TrackinglistDate = dateTimePicker1.Value.ToShortDateString(),
						Lot = textBox15.Text,
						Duration = textBox12.Text,
						InWorkTime = textBox24.Text,
						InWorkRate = textBox25.Text,
						InWorkHols = textBox26.Text,
						InWorkPrice = textBox31.Text,
						InWaitTime = textBox29.Text,
						InWaitRate = textBox30.Text,
						InWaitHols = textBox27.Text,
						InWaitPrice = textBox28.Text,
						FinalPrice = textBox35.Text
					};
					estimate.EditRow(ID);
					break;

				case "FPage_Organizations":
					Organisations organisations = new Organisations
					{
						Name = textBox17.Text,
						Ratio = comboBox5.SelectedIndex.ToString()
					};
					organisations.EditRow(ID);
					break;

				case "FPage_Staff":
					Staff staff = new Staff
					{
						FirstName = textBox20.Text,
						LastName = textBox21.Text,
						ThirdName = textBox19.Text,
						Position = comboBox6.SelectedValue.ToString()
					};
					staff.EditRow(ID);
					break;

				case "FPage_StaffPosition":
					StaffPosition staffPosition = new StaffPosition
					{
						PositionName = textBox18.Text
					};
					staffPosition.EditRow(ID);
					break;

				case "FPage_Trackinglists":
					Trackinglists trackinglists = new Trackinglists
					{
						Series = textBox22.Text,
						Number = textBox23.Text,
						Date = dateTimePicker2.Value.ToShortDateString(),
						Organization = comboBox7.SelectedValue.ToString(),
						Transport = comboBox8.SelectedValue.ToString(),
						Driver = comboBox9.SelectedValue.ToString(),
						Dispatcher = comboBox10.SelectedValue.ToString(),
						Medic = comboBox11.SelectedValue.ToString(),
						Mechanic = comboBox12.SelectedValue.ToString(),
						Organization_2 = comboBox13.SelectedValue.ToString(),
						Organization_3 = comboBox14.SelectedValue.ToString()
					};
					trackinglists.EditRow(ID);
					break;
			}
		}


		// +---------------------------------------------------+
		// |			            ПРОЧИЕ МЕТОДЫ                    |
		// +---------------------------------------------------+
		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
    {
      dateTimePicker1.Value = Convert.ToDateTime(DataCommon.GetFieldValue((int)comboBox4.SelectedValue, "Trackinglist_date", "Trackinglists"));

			#region Получение установленного тарифа
			int machineID = DataCommon.GetFieldValue((int)comboBox4.SelectedValue, "Trackinglist_transport", "Trackinglists").exToInt();
			int machineType = DataCommon.GetFieldValue(machineID, "Car_type", "Cars").exToInt();
			double inWorkRate = DataCommon.GetFieldValue(machineType, "Cartype_inwork_rate", "CarType").exToDouble();
			double inWaitRate = DataCommon.GetFieldValue(machineType, "Cartype_inwait_rate", "CarType").exToDouble();
			textBox25.Text = inWorkRate.ToString();
			textBox30.Text = inWaitRate.ToString();
			#endregion
		}
		private void EstimateTextChanged(object sender, EventArgs e)
    {
      var item = ((TextBox)sender);
      item.SelectionStart = item.Text.Length;

      // Предотвращение ввода буквы Б заместо запятой
      if (item.Text.Contains('б') ) item.Text = item.Text.Replace('б', ',');
      else if(item.Text.Contains('Б')) item.Text = item.Text.Replace('Б', ',');

      double iwork_time   = textBox24.Text == "" ? 0 : Convert.ToDouble(textBox24.Text);
      double iwork_rate   = textBox25.Text == "" ? 0 : Convert.ToDouble(textBox25.Text);
      double iwork_hols   = textBox26.Text == "" ? 0 : Convert.ToDouble(textBox26.Text);
      double iwork_price  = textBox31.Text == "" ? 0 : Convert.ToDouble(textBox31.Text);
      double iwait_time   = textBox29.Text == "" ? 0 : Convert.ToDouble(textBox29.Text);
      double iwait_rate   = textBox30.Text == "" ? 0 : Convert.ToDouble(textBox30.Text);
      double iwait_hols   = textBox27.Text == "" ? 0 : Convert.ToDouble(textBox27.Text);
      double iwait_price  = textBox28.Text == "" ? 0 : Convert.ToDouble(textBox28.Text);
      double finalprice   = textBox35.Text == "" ? 0 : Convert.ToDouble(textBox35.Text);

      if (iwork_rate == 0) iwork_price = 0;
      if (iwait_rate == 0) iwait_price = 0;
      if (iwork_time != 0 && iwork_rate != 0 && iwork_hols == 0) iwork_price = iwork_time * iwork_rate;
      if (iwork_hols != 0 && iwork_rate != 0 && iwork_hols != 0) iwork_price = (iwork_time * iwork_rate) + (iwork_hols * iwork_rate * 1.05);
      if (iwait_time != 0 && iwait_rate != 0 && iwait_hols == 0) iwait_price = iwait_time * iwait_rate;
      if (iwait_hols != 0 && iwait_rate != 0 && iwait_hols != 0) iwait_price = (iwait_time * iwait_rate) + (iwait_hols * iwait_rate * 1.05);
      if (iwork_price != 0 && iwait_price != 0) finalprice = iwork_price + iwait_price;
      if (iwork_price != 0 && iwait_price == 0) finalprice = iwork_price;
      if (iwork_price == 0 && iwait_price != 0) finalprice = iwait_price;

      textBox24.Text = iwork_time.ToString();
      textBox25.Text = iwork_rate.ToString();
      textBox26.Text = iwork_hols.ToString();
      textBox31.Text = iwork_price.ToString();
      textBox29.Text = iwait_time.ToString();
      textBox30.Text = iwait_rate.ToString();
      textBox27.Text = iwait_hols.ToString();
      textBox28.Text = iwait_price.ToString();
      textBox35.Text = finalprice.ToString();
    }
    //Функция запрета ввода букв в поле
    void onlyDigits(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
      // Пропускаем цифровые кнопки
      if ((e.KeyCode >= Keys.D0) && (e.KeyCode <= Keys.D9)) e.SuppressKeyPress = false;
      // Пропускаем цифровые кнопки с NumPad'а
      if ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.NumPad9)) e.SuppressKeyPress = false;
      // Пропускаем Delete, Back, Left и Right
      if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back) ||
          (e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right) ||
          (e.KeyCode == Keys.Oemcomma && ((TextBox)sender).Text.Split(',').Length == 1))
          e.SuppressKeyPress = false;
    }
    //Функция запрета ввода чисел в поле
    void onlyChars(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
      // Пропускаем буквенные
      if ((e.KeyCode >= Keys.A)) e.SuppressKeyPress = false;
      // Не пропускаем цифровые кнопки
      if ((e.KeyCode >= Keys.D0) && (e.KeyCode <= Keys.D9)) e.SuppressKeyPress = true;
      // Не пропускаем цифровые кнопки с NumPad'а
      if ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.NumPad9)) e.SuppressKeyPress = true;
      // Пропускаем Delete, Back, Left и Right
      if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back) ||
          (e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right)) e.SuppressKeyPress = false;
    }
    private void ButtonAddNew(object sender, EventArgs e)
    {
      var item = ((Button)sender);
			/*
      switch (item.Name)
      {
        case "Button_AddNew_CarBrand":
          if (new Add("FPage_CarBrand").ShowDialog() == DialogResult.OK)
          {
            try
						{
							CarBrand.Add();
							DataCommon.FillComoBox(comboBox1, "ID", "Carbrand_name", "CarBrand");
							comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
						}
						catch (Exception ex)
            {
              MessageBox.Show(ex.Message, "Ошибка при добавлении", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              Base.CloseConnection();
            }
          }
          break;
      }*/
		}

	}

}
