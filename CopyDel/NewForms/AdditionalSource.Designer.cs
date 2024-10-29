namespace CopyDel.NewForms
{
    partial class AdditionalSource
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
            this.AditionalSourceDataGrid = new System.Windows.Forms.DataGridView();
            this.OkBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.DelAllBtn = new System.Windows.Forms.Button();
            this.FileInfoLab = new System.Windows.Forms.Label();
            this.FileFilterLb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AditionalSourceDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // AditionalSourceDataGrid
            // 
            this.AditionalSourceDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AditionalSourceDataGrid.Location = new System.Drawing.Point(12, 12);
            this.AditionalSourceDataGrid.Name = "AditionalSourceDataGrid";
            this.AditionalSourceDataGrid.Size = new System.Drawing.Size(776, 380);
            this.AditionalSourceDataGrid.TabIndex = 0;
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(12, 398);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(143, 40);
            this.OkBtn.TabIndex = 1;
            this.OkBtn.Text = "Ok";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(645, 398);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(143, 40);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // DelAllBtn
            // 
            this.DelAllBtn.Location = new System.Drawing.Point(496, 398);
            this.DelAllBtn.Name = "DelAllBtn";
            this.DelAllBtn.Size = new System.Drawing.Size(143, 40);
            this.DelAllBtn.TabIndex = 4;
            this.DelAllBtn.Text = "Delete All";
            this.DelAllBtn.UseVisualStyleBackColor = true;
            this.DelAllBtn.Click += new System.EventHandler(this.DelAllBtn_Click);
            // 
            // FileInfoLab
            // 
            this.FileInfoLab.AutoSize = true;
            this.FileInfoLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FileInfoLab.Location = new System.Drawing.Point(168, 405);
            this.FileInfoLab.Name = "FileInfoLab";
            this.FileInfoLab.Size = new System.Drawing.Size(0, 18);
            this.FileInfoLab.TabIndex = 5;
            // 
            // FileFilterLb
            // 
            this.FileFilterLb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FileFilterLb.AutoSize = true;
            this.FileFilterLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FileFilterLb.Location = new System.Drawing.Point(165, 402);
            this.FileFilterLb.Name = "FileFilterLb";
            this.FileFilterLb.Size = new System.Drawing.Size(101, 15);
            this.FileFilterLb.TabIndex = 6;
            this.FileFilterLb.Text = "FileFilter is Off";
            // 
            // AdditionalSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FileFilterLb);
            this.Controls.Add(this.FileInfoLab);
            this.Controls.Add(this.DelAllBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.AditionalSourceDataGrid);
            this.Name = "AdditionalSource";
            this.Text = "AdditionalSource";
            ((System.ComponentModel.ISupportInitialize)(this.AditionalSourceDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AditionalSourceDataGrid;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button DelAllBtn;
        private System.Windows.Forms.Label FileInfoLab;
        private System.Windows.Forms.Label FileFilterLb;
    }
}