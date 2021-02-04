namespace TrackingSystem
{
	partial class SettingsForm
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.Button_Cancel = new System.Windows.Forms.Button();
			this.Button_Save = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tbxReportPath = new System.Windows.Forms.TextBox();
			this.btnChangePath = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.Button_Cancel);
			this.panel1.Controls.Add(this.Button_Save);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 193);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 25);
			this.panel1.TabIndex = 3;
			// 
			// Button_Cancel
			// 
			this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Button_Cancel.Location = new System.Drawing.Point(303, 0);
			this.Button_Cancel.Name = "Button_Cancel";
			this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Button_Cancel.TabIndex = 1;
			this.Button_Cancel.Text = "Отмена";
			this.Button_Cancel.UseVisualStyleBackColor = true;
			this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
			// 
			// Button_Save
			// 
			this.Button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Button_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Button_Save.Location = new System.Drawing.Point(225, 0);
			this.Button_Save.Name = "Button_Save";
			this.Button_Save.Size = new System.Drawing.Size(75, 23);
			this.Button_Save.TabIndex = 0;
			this.Button_Save.Text = "Сохранить";
			this.Button_Save.UseVisualStyleBackColor = true;
			this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.groupBox1.Controls.Add(this.tbxReportPath);
			this.groupBox1.Controls.Add(this.btnChangePath);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(384, 77);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Путь";
			// 
			// tbxReportPath
			// 
			this.tbxReportPath.Location = new System.Drawing.Point(6, 48);
			this.tbxReportPath.Name = "tbxReportPath";
			this.tbxReportPath.ReadOnly = true;
			this.tbxReportPath.Size = new System.Drawing.Size(372, 20);
			this.tbxReportPath.TabIndex = 5;
			// 
			// btnChangePath
			// 
			this.btnChangePath.Location = new System.Drawing.Point(6, 19);
			this.btnChangePath.Name = "btnChangePath";
			this.btnChangePath.Size = new System.Drawing.Size(75, 23);
			this.btnChangePath.TabIndex = 0;
			this.btnChangePath.Text = "Изменить";
			this.btnChangePath.UseVisualStyleBackColor = true;
			this.btnChangePath.Click += new System.EventHandler(this.btnChangePath_Click);
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 218);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Глобальные настройки";
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button Button_Cancel;
		private System.Windows.Forms.Button Button_Save;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox tbxReportPath;
		private System.Windows.Forms.Button btnChangePath;
	}
}