using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrackingSystem.Helpers;

namespace TrackingSystem
{
  public partial class Duration : Form
  {
    public Duration()
    {
      InitializeComponent();
    }

    private void B_OK_Click(object sender, EventArgs e)
    {
      DataCommon.Values = new object[3];
      DataCommon.Values[0] = Start.Value.ToShortDateString();
      DataCommon.Values[1] = End	.Value.ToShortDateString();
			DataCommon.Values[2] = comboBox1.SelectedValue;
		}

		private void Duration_Load(object sender, EventArgs e)
		{
			DataCommon.FillComoBox(comboBox1, "ID", "Organization_name", "Organizations", "Organization_ratio=1");
			comboBox1.SelectedIndex = 0;
		}
	}
}
