namespace TrackingSystem
{
  partial class Duration
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Start = new System.Windows.Forms.DateTimePicker();
			this.End = new System.Windows.Forms.DateTimePicker();
			this.B_OK = new System.Windows.Forms.Button();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "С";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(21, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "По";
			// 
			// Start
			// 
			this.Start.Location = new System.Drawing.Point(38, 17);
			this.Start.Name = "Start";
			this.Start.Size = new System.Drawing.Size(200, 20);
			this.Start.TabIndex = 1;
			// 
			// End
			// 
			this.End.Location = new System.Drawing.Point(38, 46);
			this.End.Name = "End";
			this.End.Size = new System.Drawing.Size(200, 20);
			this.End.TabIndex = 1;
			// 
			// B_OK
			// 
			this.B_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.B_OK.Location = new System.Drawing.Point(95, 134);
			this.B_OK.Name = "B_OK";
			this.B_OK.Size = new System.Drawing.Size(75, 23);
			this.B_OK.TabIndex = 2;
			this.B_OK.Text = "ОК";
			this.B_OK.UseVisualStyleBackColor = true;
			this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.B_Cancel.Location = new System.Drawing.Point(176, 134);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(75, 23);
			this.B_Cancel.TabIndex = 2;
			this.B_Cancel.Text = "Отмена";
			this.B_Cancel.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(38, 15);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(200, 21);
			this.comboBox1.TabIndex = 3;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.End);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.Start);
			this.groupBox1.Location = new System.Drawing.Point(2, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(249, 76);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Период";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.comboBox1);
			this.groupBox2.Location = new System.Drawing.Point(2, 84);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(249, 47);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Подрядчик";
			// 
			// Duration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(254, 161);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.B_Cancel);
			this.Controls.Add(this.B_OK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Duration";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Период";
			this.Load += new System.EventHandler(this.Duration_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DateTimePicker Start;
    private System.Windows.Forms.DateTimePicker End;
    private System.Windows.Forms.Button B_OK;
    private System.Windows.Forms.Button B_Cancel;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
	}
}