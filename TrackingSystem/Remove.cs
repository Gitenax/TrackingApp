using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using TrackingSystem.Helpers;
using TrackingSystem.Models;

namespace TrackingSystem
{
	public partial class Remove : Form
	{
		int[] VALUES;
		string TABLE;
		bool relationship;

		public Remove(bool hasRelation)
		{
			InitializeComponent();
			relationship = hasRelation;
		}

		public Remove(int[] values, string table, bool hasRelation)
		{
			InitializeComponent();
			VALUES = values; //Передаем индексы
			TABLE = table; //Наименование таблицы в БД
			relationship = hasRelation;
		}

		private void Remove_Load(object sender, EventArgs e)
		{
			if (VALUES != null)
			{
				// DESIGN
				Width = 500;
				Height = 180;

				if(relationship) L_INFO.Text = "Эти записи имеют связи в других таблицах, продолжить?";
				else L_INFO.Text = "Вы действительно хотите удалить эти записи?";

				L_INFO.Location = new Point(114, 12);
				ListBox listBox = new ListBox
				{
					Name = "LB_LIST",
					Width = 360,
					Height = 82,
					Location = new Point(117, 28),
					SelectionMode = SelectionMode.None
				};
				Controls.Add(listBox);

				// DATA MANAGE
				using (var connection = new OleDbConnection(Base.DB_CONNECTION_STRING))
				{
					connection.Open();
					List<string> result = new List<string>();
					string CMD_TEXT = "";
					DataTable Table = new DataTable();
					OleDbDataAdapter adater;
					var lst = (ListBox)Controls["LB_LIST"];

					for (int i = 0; i < VALUES.Length; i++)
					{
						CMD_TEXT += VALUES[i].ToString() + ", ";
					}
					CMD_TEXT = CMD_TEXT.Remove(CMD_TEXT.Length - 2);

					adater = new OleDbDataAdapter($"SELECT * FROM {TABLE} WHERE ID IN({CMD_TEXT})", connection);
					adater.Fill(Table);

					for (int i = 0; i < Table.Rows.Count; i++)
					{
						string line = Table.exToArray(i).exToString(" - ", 1);
						result.Add(line);
					}

					for (int i = 0; i < result.Count; i++)
						lst.Items.Add(result[i]);
				}
			}
			else
			{
				if (relationship) L_INFO.Text = "Эта запись имеет связи в других таблицах,\nпродолжить?";
				else L_INFO.Text = "Вы действительно хотите удалить эту запись?";

			}
		}
	}
}
