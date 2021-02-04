using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrackingSystem.Helpers;
using TrackingSystem.Models;

namespace TrackingSystem
{
	public partial class Reference : Form
	{
		public Reference()
		{
			InitializeComponent();
		}

		private void Reference_Load(object sender, EventArgs e)
		{
			if (!ckbAddOrganisation.Checked) cbOrganisations.Enabled = false;
			cbOrganisations.exFill("ID", "Organization_name", "Organizations", "Organization_ratio=2");
			Base.AddClient = false;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (ckbAddOrganisation.Checked)
			{
				DataCommon.Values = new object[1];
				DataCommon.Values[0] = cbOrganisations.SelectedValue;
				Base.AddClient = true;
			}
			
		}

		private void ckbAddOrganisation_CheckedChanged(object sender, EventArgs e)
		{
			if (ckbAddOrganisation.Checked == true) cbOrganisations.Enabled = true;
			else cbOrganisations.Enabled = false;
		}
	}
}
