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
            this.test = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textdir = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.MaxLenghtFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.TestButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.checkFilesBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(739, 15);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(75, 23);
            this.test.TabIndex = 27;
            this.test.Text = "Del Copy";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 94);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(802, 479);
            this.richTextBox1.TabIndex = 28;
            this.richTextBox1.Text = "";
            // 
            // textdir
            // 
            this.textdir.Location = new System.Drawing.Point(87, 17);
            this.textdir.Name = "textdir";
            this.textdir.Size = new System.Drawing.Size(646, 20);
            this.textdir.TabIndex = 29;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "Show Copy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MaxLenghtFile
            // 
            this.MaxLenghtFile.Location = new System.Drawing.Point(100, 42);
            this.MaxLenghtFile.Name = "MaxLenghtFile";
            this.MaxLenghtFile.Size = new System.Drawing.Size(150, 20);
            this.MaxLenghtFile.TabIndex = 31;
            this.MaxLenghtFile.TextChanged += new System.EventHandler(this.MaxLenghtFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Max Lenght File:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 68);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(802, 20);
            this.progressBar1.TabIndex = 33;
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(377, 42);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(69, 23);
            this.TestButton.TabIndex = 34;
            this.TestButton.Text = "TestButton";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(463, 42);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(69, 23);
            this.StopButton.TabIndex = 35;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // checkFilesBox
            // 
            this.checkFilesBox.AutoSize = true;
            this.checkFilesBox.Location = new System.Drawing.Point(257, 44);
            this.checkFilesBox.Name = "checkFilesBox";
            this.checkFilesBox.Size = new System.Drawing.Size(61, 17);
            this.checkFilesBox.TabIndex = 36;
            this.checkFilesBox.Text = "All Files";
            this.checkFilesBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 594);
            this.Controls.Add(this.checkFilesBox);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MaxLenghtFile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textdir);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.test);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button test;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textdir;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox MaxLenghtFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.CheckBox checkFilesBox;
    }
}

