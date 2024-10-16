namespace CopyDel.NewForms
{
    partial class RshForm
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
            this.ApplyButton = new System.Windows.Forms.Button();
            this.AddCheckBox = new System.Windows.Forms.Button();
            this.RshTextBox = new System.Windows.Forms.TextBox();
            this.CenselButton = new System.Windows.Forms.Button();
            this.DelAllBtn = new System.Windows.Forms.Button();
            this.RshListComBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ApplyButton.Location = new System.Drawing.Point(18, 375);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(98, 35);
            this.ApplyButton.TabIndex = 0;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // AddCheckBox
            // 
            this.AddCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddCheckBox.Location = new System.Drawing.Point(18, 276);
            this.AddCheckBox.Name = "AddCheckBox";
            this.AddCheckBox.Size = new System.Drawing.Size(98, 23);
            this.AddCheckBox.TabIndex = 2;
            this.AddCheckBox.Text = "Add";
            this.AddCheckBox.UseVisualStyleBackColor = true;
            this.AddCheckBox.Click += new System.EventHandler(this.AddCheckBox_Click);
            // 
            // RshTextBox
            // 
            this.RshTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RshTextBox.Location = new System.Drawing.Point(18, 248);
            this.RshTextBox.Name = "RshTextBox";
            this.RshTextBox.Size = new System.Drawing.Size(98, 20);
            this.RshTextBox.TabIndex = 3;
            // 
            // CenselButton
            // 
            this.CenselButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CenselButton.Location = new System.Drawing.Point(18, 416);
            this.CenselButton.Name = "CenselButton";
            this.CenselButton.Size = new System.Drawing.Size(98, 39);
            this.CenselButton.TabIndex = 4;
            this.CenselButton.Text = "Censel";
            this.CenselButton.UseVisualStyleBackColor = true;
            this.CenselButton.Click += new System.EventHandler(this.CenselButton_Click);
            // 
            // DelAllBtn
            // 
            this.DelAllBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DelAllBtn.Location = new System.Drawing.Point(18, 346);
            this.DelAllBtn.Name = "DelAllBtn";
            this.DelAllBtn.Size = new System.Drawing.Size(98, 23);
            this.DelAllBtn.TabIndex = 5;
            this.DelAllBtn.Text = "Del All";
            this.DelAllBtn.UseVisualStyleBackColor = true;
            this.DelAllBtn.Click += new System.EventHandler(this.DelAllBtn_Click);
            // 
            // RshListComBox
            // 
            this.RshListComBox.FormattingEnabled = true;
            this.RshListComBox.Location = new System.Drawing.Point(18, 311);
            this.RshListComBox.Name = "RshListComBox";
            this.RshListComBox.Size = new System.Drawing.Size(98, 21);
            this.RshListComBox.TabIndex = 6;
            this.RshListComBox.SelectedIndexChanged += new System.EventHandler(this.RshListComBox_SelectedIndexChanged);
            // 
            // RshForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(134, 462);
            this.Controls.Add(this.RshListComBox);
            this.Controls.Add(this.DelAllBtn);
            this.Controls.Add(this.CenselButton);
            this.Controls.Add(this.RshTextBox);
            this.Controls.Add(this.AddCheckBox);
            this.Controls.Add(this.ApplyButton);
            this.MaximumSize = new System.Drawing.Size(150, 1700);
            this.MinimumSize = new System.Drawing.Size(150, 500);
            this.Name = "RshForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button AddCheckBox;
        private System.Windows.Forms.TextBox RshTextBox;
        private System.Windows.Forms.Button CenselButton;
        private System.Windows.Forms.Button DelAllBtn;
        private System.Windows.Forms.ComboBox RshListComBox;
    }
}