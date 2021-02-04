using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TrackingSystem
{
	public partial class SettingsForm : Form
	{
		// +---------------------------------------------------+
		// |											 ФОРМА                       |
		// +---------------------------------------------------+
		string Temp_ReportDefaultPath;
		public SettingsForm()
		{
			InitializeComponent();
			Temp_ReportDefaultPath = Properties.Settings.Default.ReportDefaultPath;
			tbxReportPath.Text = Properties.Settings.Default.ReportDefaultPath;
		}


		// +---------------------------------------------------+
		// |			          ИЗМЕНЕНИЕ НАСТРОЕК                 |
		// +---------------------------------------------------+
		private void btnChangePath_Click(object sender, EventArgs e)
		{
			var dialog = new FolderBrowserDialog
			{
				RootFolder = Environment.SpecialFolder.MyComputer,
				Description = "Папка для отчетов по умолчанию",
				ShowNewFolderButton = true
			};
			
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				Properties.Settings.Default.ReportDefaultPath = dialog.SelectedPath;
				tbxReportPath.Text = dialog.SelectedPath;
			}
		}


		// +---------------------------------------------------+
		// |			         СОХРАНЕНИЕ НАСТРОЕК                 |
		// +---------------------------------------------------+
		private void Button_Save_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.Save();
		}


		// +---------------------------------------------------+
		// |			          ОТМЕНА НАСТРОЕК                    |
		// +---------------------------------------------------+
		private void Button_Cancel_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.ReportDefaultPath = Temp_ReportDefaultPath;
		}
	}
}
