namespace TrackingSystem
{
	partial class Reference
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnCancel = new System.Windows.Forms.Button();
			this.ckbAddOrganisation = new System.Windows.Forms.CheckBox();
			this.cbOrganisations = new System.Windows.Forms.ComboBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(163, 69);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// ckbAddOrganisation
			// 
			this.ckbAddOrganisation.AutoSize = true;
			this.ckbAddOrganisation.Location = new System.Drawing.Point(6, 12);
			this.ckbAddOrganisation.Name = "ckbAddOrganisation";
			this.ckbAddOrganisation.Size = new System.Drawing.Size(132, 17);
			this.ckbAddOrganisation.TabIndex = 1;
			this.ckbAddOrganisation.Text = "Добавить заказчика";
			this.ckbAddOrganisation.UseVisualStyleBackColor = true;
			this.ckbAddOrganisation.CheckedChanged += new System.EventHandler(this.ckbAddOrganisation_CheckedChanged);
			// 
			// cbOrganisations
			// 
			this.cbOrganisations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbOrganisations.FormattingEnabled = true;
			this.cbOrganisations.Location = new System.Drawing.Point(6, 35);
			this.cbOrganisations.Name = "cbOrganisations";
			this.cbOrganisations.Size = new System.Drawing.Size(232, 21);
			this.cbOrganisations.TabIndex = 2;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(82, 69);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "ОК";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// Reference
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(242, 104);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.cbOrganisations);
			this.Controls.Add(this.ckbAddOrganisation);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Reference";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Справки";
			this.Load += new System.EventHandler(this.Reference_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox ckbAddOrganisation;
		private System.Windows.Forms.ComboBox cbOrganisations;
		private System.Windows.Forms.Button btnOK;
	}
}