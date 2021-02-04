namespace TrackingSystem
{
	partial class Remove
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Remove));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.L_INFO = new System.Windows.Forms.Label();
			this.B_OK = new System.Windows.Forms.Button();
			this.B_CANCEL = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::TrackingSystem.Properties.Resources.question;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(96, 98);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// L_INFO
			// 
			this.L_INFO.AutoSize = true;
			this.L_INFO.Location = new System.Drawing.Point(114, 42);
			this.L_INFO.Name = "L_INFO";
			this.L_INFO.Size = new System.Drawing.Size(244, 13);
			this.L_INFO.TabIndex = 1;
			this.L_INFO.Text = "Вы действительно хотите удалить эту запись?";
			// 
			// B_OK
			// 
			this.B_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.B_OK.Location = new System.Drawing.Point(202, 85);
			this.B_OK.Name = "B_OK";
			this.B_OK.Size = new System.Drawing.Size(75, 23);
			this.B_OK.TabIndex = 2;
			this.B_OK.Text = "ОК";
			this.B_OK.UseVisualStyleBackColor = true;
			// 
			// B_CANCEL
			// 
			this.B_CANCEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.B_CANCEL.Location = new System.Drawing.Point(283, 85);
			this.B_CANCEL.Name = "B_CANCEL";
			this.B_CANCEL.Size = new System.Drawing.Size(75, 23);
			this.B_CANCEL.TabIndex = 2;
			this.B_CANCEL.Text = "Отмена";
			this.B_CANCEL.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.B_CANCEL.UseVisualStyleBackColor = true;
			// 
			// Remove
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 111);
			this.Controls.Add(this.B_CANCEL);
			this.Controls.Add(this.B_OK);
			this.Controls.Add(this.L_INFO);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Remove";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Удалить";
			this.Load += new System.EventHandler(this.Remove_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label L_INFO;
		private System.Windows.Forms.Button B_OK;
		private System.Windows.Forms.Button B_CANCEL;
	}
}