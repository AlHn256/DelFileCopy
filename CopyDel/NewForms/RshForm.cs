using CopyDel.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CopyDel.NewForms
{
    public partial class RshForm : Form
    {
        private int ChkBoxId;
        public bool ApplyChanges {  get; set; } = false;
        public string[] MFilter { get; set; }

        private List<RSHList> RshList = new List<RSHList> {
            new RSHList(){ Name = "All", ShortName = "All", RSH = new string[] { "*.*" }},
            new RSHList(){ Name = "Video", ShortName = "Vid", RSH = new string[] { "*.mpg", "*.avi", "*.mpeg", "*.wmv", "*.dat", "*.asf" , "*.opus"}},
            new RSHList(){ Name = "Audio", ShortName = "Wav", RSH = new string[] {  "*.mp4", "*.mp3", "*.wav", "*.webm", "*.ogg", "*.wma"  }},
            new RSHList(){ Name = "Text Files", ShortName = "Txt", RSH = new string[] { "*.txt", "*.doc", "*.pdf", "*.djvu"}},
            new RSHList(){ Name = "Img", ShortName = "Img", RSH = new string[] { "*.bmp", "*.jpeg", "*.tif", "*.png", "*.webm" }},
            new RSHList(){ Name = "Arhive", ShortName = "Rar", RSH = new string[] { "*.rar", "*.zip", "*.7z"}},
        };

        public RshForm(string[] mFilter)
        {
            InitializeComponent();

            if (mFilter == null) MFilter = RshList[0].RSH;
            else if (mFilter.Length == 0) MFilter = RshList[0].RSH;
            else MFilter = mFilter;

            RshListComBox.DataSource = RshList;
            RshListComBox.DisplayMember = "Name";

            //LoadeCheckBox();
        }
        private void LoadeCheckBox(){foreach (var elem in MFilter) AddBtn(elem);}
        private void AddBtn(string Name)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.AutoSize = true;
            checkBox.Location = new Point(12, 12 + 23 * ChkBoxId);
            checkBox.Name = "checkBox" + ChkBoxId;
            checkBox.Size = new Size(80, 17);
            checkBox.TabIndex = ChkBoxId++;
            checkBox.Text = Name;
            checkBox.UseVisualStyleBackColor = true;
            checkBox.Checked = true;
            Controls.Add(checkBox);
        }
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            List<string> strL = new List<string>();
            foreach (var elem in Controls)
            {
                if (elem is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)elem;
                    if (checkBox.Checked) strL.Add(checkBox.Text);
                }
            }
            ApplyChanges = true;
            MFilter = strL.ToArray();
            this.Close();
        }
        private void AddCheckBox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RshTextBox.Text))
            {
                if (RshTextBox.Text == "*.*" || CheckForAll()) DelAllCheckBox();
                AddBtn(RshTextBox.Text);
            }
        }
        private bool CheckForAll()
        {
            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)c;
                    if(checkBox.Text == "*.*")return true;
                }
            }

            return false;
        }
        private void DelAllCheckBox()
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in this.Controls) controlList.Add(c);
            foreach (Control c in controlList) if (c is CheckBox)this.Controls.Remove(c);

            ChkBoxId = 0;
        }
        private void CenselButton_Click(object sender, EventArgs e)
        {
            MFilter = new string[] { };
            this.Close();
        }

        private void RshListComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var RshIndex = RshListComBox.SelectedIndex;
            MFilter = RshList[RshIndex].RSH;
            DelAllCheckBox();
            LoadeCheckBox();
        }

        private void DelAllBtn_Click(object sender, EventArgs e) => DelAllCheckBox();
    }
}