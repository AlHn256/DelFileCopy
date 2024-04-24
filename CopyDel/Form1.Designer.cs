namespace CopyDel
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.DelCopyBtn = new System.Windows.Forms.Button();
            this.RTB = new System.Windows.Forms.RichTextBox();
            this.textdir = new System.Windows.Forms.TextBox();
            this.ShowCopyBtn = new System.Windows.Forms.Button();
            this.MaxLenghtFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.StopButton = new System.Windows.Forms.Button();
            this.checkFilesBox = new System.Windows.Forms.CheckBox();
            this.dataGru = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.CountFilesInDirBtn = new System.Windows.Forms.Button();
            this.AditionalOptionsChkBox = new System.Windows.Forms.CheckBox();
            this.DirInfoBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGru)).BeginInit();
            this.SuspendLayout();
            // 
            // DelCopyBtn
            // 
            this.DelCopyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DelCopyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DelCopyBtn.Location = new System.Drawing.Point(12, 100);
            this.DelCopyBtn.Name = "DelCopyBtn";
            this.DelCopyBtn.Size = new System.Drawing.Size(1306, 32);
            this.DelCopyBtn.TabIndex = 27;
            this.DelCopyBtn.Text = "Del Copy";
            this.DelCopyBtn.UseVisualStyleBackColor = true;
            this.DelCopyBtn.Click += new System.EventHandler(this.DelCopyBtn_Click);
            // 
            // RTB
            // 
            this.RTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB.Location = new System.Drawing.Point(12, 818);
            this.RTB.Name = "RTB";
            this.RTB.Size = new System.Drawing.Size(1306, 219);
            this.RTB.TabIndex = 28;
            this.RTB.Text = "";
            // 
            // textdir
            // 
            this.textdir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textdir.Location = new System.Drawing.Point(100, 17);
            this.textdir.Name = "textdir";
            this.textdir.Size = new System.Drawing.Size(1115, 20);
            this.textdir.TabIndex = 29;
            // 
            // ShowCopyBtn
            // 
            this.ShowCopyBtn.Location = new System.Drawing.Point(12, 71);
            this.ShowCopyBtn.Name = "ShowCopyBtn";
            this.ShowCopyBtn.Size = new System.Drawing.Size(82, 23);
            this.ShowCopyBtn.TabIndex = 30;
            this.ShowCopyBtn.Text = "Show Copy";
            this.ShowCopyBtn.UseVisualStyleBackColor = true;
            this.ShowCopyBtn.Click += new System.EventHandler(this.ShowCopyBtn_Click);
            // 
            // MaxLenghtFile
            // 
            this.MaxLenghtFile.Location = new System.Drawing.Point(100, 44);
            this.MaxLenghtFile.Name = "MaxLenghtFile";
            this.MaxLenghtFile.Size = new System.Drawing.Size(150, 20);
            this.MaxLenghtFile.TabIndex = 31;
            this.MaxLenghtFile.Text = "16777216";
            this.MaxLenghtFile.TextChanged += new System.EventHandler(this.MaxLenghtFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Max Lenght File:";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(100, 71);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1137, 23);
            this.progressBar1.TabIndex = 33;
            // 
            // StopButton
            // 
            this.StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopButton.Location = new System.Drawing.Point(1243, 71);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 35;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // checkFilesBox
            // 
            this.checkFilesBox.AutoSize = true;
            this.checkFilesBox.Location = new System.Drawing.Point(257, 46);
            this.checkFilesBox.Name = "checkFilesBox";
            this.checkFilesBox.Size = new System.Drawing.Size(61, 17);
            this.checkFilesBox.TabIndex = 36;
            this.checkFilesBox.Text = "All Files";
            this.checkFilesBox.UseVisualStyleBackColor = true;
            // 
            // dataGru
            // 
            this.dataGru.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGru.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGru.Location = new System.Drawing.Point(12, 138);
            this.dataGru.Name = "dataGru";
            this.dataGru.Size = new System.Drawing.Size(1306, 674);
            this.dataGru.TabIndex = 37;
            this.dataGru.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGru_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Serch File:";
            // 
            // CountFilesInDirBtn
            // 
            this.CountFilesInDirBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CountFilesInDirBtn.Location = new System.Drawing.Point(1221, 15);
            this.CountFilesInDirBtn.Name = "CountFilesInDirBtn";
            this.CountFilesInDirBtn.Size = new System.Drawing.Size(97, 23);
            this.CountFilesInDirBtn.TabIndex = 40;
            this.CountFilesInDirBtn.Text = "Count Files In Dir";
            this.CountFilesInDirBtn.UseVisualStyleBackColor = true;
            this.CountFilesInDirBtn.Click += new System.EventHandler(this.CountFilesInDirBtn_Click);
            // 
            // AditionalOptionsChkBox
            // 
            this.AditionalOptionsChkBox.AutoSize = true;
            this.AditionalOptionsChkBox.Location = new System.Drawing.Point(334, 46);
            this.AditionalOptionsChkBox.Name = "AditionalOptionsChkBox";
            this.AditionalOptionsChkBox.Size = new System.Drawing.Size(105, 17);
            this.AditionalOptionsChkBox.TabIndex = 41;
            this.AditionalOptionsChkBox.Text = "Aditional Options";
            this.AditionalOptionsChkBox.UseVisualStyleBackColor = true;
            this.AditionalOptionsChkBox.CheckedChanged += new System.EventHandler(this.AditionalOptionsChkBox_CheckedChanged);
            // 
            // DirInfoBtn
            // 
            this.DirInfoBtn.Location = new System.Drawing.Point(462, 42);
            this.DirInfoBtn.Name = "DirInfoBtn";
            this.DirInfoBtn.Size = new System.Drawing.Size(82, 23);
            this.DirInfoBtn.TabIndex = 42;
            this.DirInfoBtn.Text = "Dir Info";
            this.DirInfoBtn.UseVisualStyleBackColor = true;
            this.DirInfoBtn.Click += new System.EventHandler(this.DirInfoBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 1049);
            this.Controls.Add(this.DirInfoBtn);
            this.Controls.Add(this.AditionalOptionsChkBox);
            this.Controls.Add(this.CountFilesInDirBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGru);
            this.Controls.Add(this.checkFilesBox);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MaxLenghtFile);
            this.Controls.Add(this.ShowCopyBtn);
            this.Controls.Add(this.textdir);
            this.Controls.Add(this.RTB);
            this.Controls.Add(this.DelCopyBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGru)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DelCopyBtn;
        private System.Windows.Forms.RichTextBox RTB;
        private System.Windows.Forms.TextBox textdir;
        private System.Windows.Forms.Button ShowCopyBtn;
        private System.Windows.Forms.TextBox MaxLenghtFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.CheckBox checkFilesBox;
        private System.Windows.Forms.DataGridView dataGru;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CountFilesInDirBtn;
        private System.Windows.Forms.CheckBox AditionalOptionsChkBox;
        private System.Windows.Forms.Button DirInfoBtn;
    }
}

