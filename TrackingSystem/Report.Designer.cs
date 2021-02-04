namespace TrackingSystem
{
  partial class Report
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.DataList_Report = new System.Windows.Forms.DataGridView();
			this.ReportMenuStrip = new System.Windows.Forms.MenuStrip();
			this.отчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.таблицаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ReportStatusStrip = new System.Windows.Forms.StatusStrip();
			this.panel1 = new System.Windows.Forms.Panel();
			this.B_Reference = new System.Windows.Forms.Button();
			this.B_Act = new System.Windows.Forms.Button();
			this.B_Invoice = new System.Windows.Forms.Button();
			this.Mode = new System.Windows.Forms.ComboBox();
			this.B_Registry = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.B_Trackinglist = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.DataList_Report)).BeginInit();
			this.ReportMenuStrip.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// DataList_Report
			// 
			this.DataList_Report.AllowUserToAddRows = false;
			this.DataList_Report.AllowUserToDeleteRows = false;
			this.DataList_Report.AllowUserToResizeRows = false;
			this.DataList_Report.BackgroundColor = System.Drawing.Color.Wheat;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DataList_Report.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.DataList_Report.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DataList_Report.DefaultCellStyle = dataGridViewCellStyle2;
			this.DataList_Report.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataList_Report.Location = new System.Drawing.Point(0, 0);
			this.DataList_Report.Name = "DataList_Report";
			this.DataList_Report.ReadOnly = true;
			this.DataList_Report.RowHeadersVisible = false;
			this.DataList_Report.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DataList_Report.Size = new System.Drawing.Size(893, 508);
			this.DataList_Report.TabIndex = 2;
			this.DataList_Report.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataList_Report_CellClick);
			// 
			// ReportMenuStrip
			// 
			this.ReportMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отчетToolStripMenuItem,
            this.таблицаToolStripMenuItem});
			this.ReportMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.ReportMenuStrip.Name = "ReportMenuStrip";
			this.ReportMenuStrip.Size = new System.Drawing.Size(1117, 24);
			this.ReportMenuStrip.TabIndex = 3;
			this.ReportMenuStrip.Text = "menuStrip1";
			this.ReportMenuStrip.Visible = false;
			// 
			// отчетToolStripMenuItem
			// 
			this.отчетToolStripMenuItem.Name = "отчетToolStripMenuItem";
			this.отчетToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			this.отчетToolStripMenuItem.Text = "Отчет";
			// 
			// таблицаToolStripMenuItem
			// 
			this.таблицаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.видToolStripMenuItem});
			this.таблицаToolStripMenuItem.Name = "таблицаToolStripMenuItem";
			this.таблицаToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
			this.таблицаToolStripMenuItem.Text = "Таблица";
			// 
			// видToolStripMenuItem
			// 
			this.видToolStripMenuItem.Name = "видToolStripMenuItem";
			this.видToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
			this.видToolStripMenuItem.Text = "Вид";
			// 
			// ReportStatusStrip
			// 
			this.ReportStatusStrip.Location = new System.Drawing.Point(0, 486);
			this.ReportStatusStrip.Name = "ReportStatusStrip";
			this.ReportStatusStrip.Size = new System.Drawing.Size(1117, 22);
			this.ReportStatusStrip.TabIndex = 4;
			this.ReportStatusStrip.Text = "statusStrip1";
			this.ReportStatusStrip.Visible = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.B_Reference);
			this.panel1.Controls.Add(this.B_Act);
			this.panel1.Controls.Add(this.B_Invoice);
			this.panel1.Controls.Add(this.Mode);
			this.panel1.Controls.Add(this.B_Registry);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.B_Trackinglist);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(893, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(224, 508);
			this.panel1.TabIndex = 5;
			// 
			// B_Reference
			// 
			this.B_Reference.Image = global::TrackingSystem.Properties.Resources.x_office_spreadsheet;
			this.B_Reference.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.B_Reference.Location = new System.Drawing.Point(5, 93);
			this.B_Reference.Name = "B_Reference";
			this.B_Reference.Size = new System.Drawing.Size(215, 40);
			this.B_Reference.TabIndex = 2;
			this.B_Reference.Text = "Справка к путевому листу";
			this.B_Reference.UseVisualStyleBackColor = true;
			this.B_Reference.Click += new System.EventHandler(this.ReportButtonClick);
			// 
			// B_Act
			// 
			this.B_Act.Enabled = false;
			this.B_Act.Image = global::TrackingSystem.Properties.Resources.x_office_spreadsheet;
			this.B_Act.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.B_Act.Location = new System.Drawing.Point(5, 231);
			this.B_Act.Name = "B_Act";
			this.B_Act.Size = new System.Drawing.Size(215, 40);
			this.B_Act.TabIndex = 2;
			this.B_Act.Text = "Акт  выполненных работ";
			this.B_Act.UseVisualStyleBackColor = true;
			this.B_Act.Visible = false;
			this.B_Act.Click += new System.EventHandler(this.ReportButtonClick);
			// 
			// B_Invoice
			// 
			this.B_Invoice.Enabled = false;
			this.B_Invoice.Image = global::TrackingSystem.Properties.Resources.x_office_spreadsheet;
			this.B_Invoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.B_Invoice.Location = new System.Drawing.Point(6, 185);
			this.B_Invoice.Name = "B_Invoice";
			this.B_Invoice.Size = new System.Drawing.Size(215, 40);
			this.B_Invoice.TabIndex = 2;
			this.B_Invoice.Text = "Счет фактура";
			this.B_Invoice.UseVisualStyleBackColor = true;
			this.B_Invoice.Visible = false;
			this.B_Invoice.Click += new System.EventHandler(this.ReportButtonClick);
			// 
			// Mode
			// 
			this.Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Mode.FormattingEnabled = true;
			this.Mode.Items.AddRange(new object[] {
            "Путевые листы",
            "Смета"});
			this.Mode.Location = new System.Drawing.Point(6, 20);
			this.Mode.Name = "Mode";
			this.Mode.Size = new System.Drawing.Size(215, 21);
			this.Mode.TabIndex = 1;
			this.Mode.SelectedIndexChanged += new System.EventHandler(this.Mode_SelectedIndexChanged);
			// 
			// B_Registry
			// 
			this.B_Registry.Image = global::TrackingSystem.Properties.Resources.x_office_spreadsheet;
			this.B_Registry.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.B_Registry.Location = new System.Drawing.Point(5, 139);
			this.B_Registry.Name = "B_Registry";
			this.B_Registry.Size = new System.Drawing.Size(215, 40);
			this.B_Registry.TabIndex = 2;
			this.B_Registry.Text = "Реестр оказанных услуг\r\nза период\r\n\r\n";
			this.B_Registry.UseVisualStyleBackColor = true;
			this.B_Registry.Click += new System.EventHandler(this.ReportButtonClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Отображение";
			// 
			// B_Trackinglist
			// 
			this.B_Trackinglist.Image = global::TrackingSystem.Properties.Resources.x_office_spreadsheet;
			this.B_Trackinglist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.B_Trackinglist.Location = new System.Drawing.Point(5, 47);
			this.B_Trackinglist.Name = "B_Trackinglist";
			this.B_Trackinglist.Size = new System.Drawing.Size(215, 40);
			this.B_Trackinglist.TabIndex = 2;
			this.B_Trackinglist.Text = "Путевой лист";
			this.B_Trackinglist.UseVisualStyleBackColor = true;
			this.B_Trackinglist.Click += new System.EventHandler(this.ReportButtonClick);
			// 
			// Report
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1117, 508);
			this.Controls.Add(this.DataList_Report);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.ReportStatusStrip);
			this.Controls.Add(this.ReportMenuStrip);
			this.MainMenuStrip = this.ReportMenuStrip;
			this.Name = "Report";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Управление отчетами";
			this.Load += new System.EventHandler(this.Report_Load);
			((System.ComponentModel.ISupportInitialize)(this.DataList_Report)).EndInit();
			this.ReportMenuStrip.ResumeLayout(false);
			this.ReportMenuStrip.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView DataList_Report;
    private System.Windows.Forms.MenuStrip ReportMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem отчетToolStripMenuItem;
    private System.Windows.Forms.StatusStrip ReportStatusStrip;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ComboBox Mode;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStripMenuItem таблицаToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
    private System.Windows.Forms.Button B_Reference;
    private System.Windows.Forms.Button B_Registry;
    private System.Windows.Forms.Button B_Trackinglist;
    private System.Windows.Forms.Button B_Act;
    private System.Windows.Forms.Button B_Invoice;
  }
}