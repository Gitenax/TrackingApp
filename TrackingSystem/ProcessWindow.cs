using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrackingSystem
{
	public partial class ProcessWindow : Form
	{
		Timer timer = new Timer
		{
			Interval = 500,
			Enabled = false
		};
		byte counter = 0;

		public ProcessWindow()
		{
			InitializeComponent();
			timer.Tick += Timer_Tick;
		}

		private void ProcessWindow_Load(object sender, EventArgs e)
		{
			timer.Enabled = true;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			switch (counter)
			{
				case 0: label1.Text = "Идет создание отчета"; counter++;			break;
				case 1: label1.Text = "Идет создание отчета."; counter++;			break;
				case 2: label1.Text = "Идет создание отчета.."; counter++;		break;
				case 3: label1.Text = "Идет создание отчета..."; counter = 0; break;
			}
		}
	}
}
