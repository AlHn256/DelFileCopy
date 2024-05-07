using CopyDel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CopyDel.NewForms
{
    public partial class AdditionalSource : Form
    {
        public bool IsOk = false;
        FileEdit fileEdit = new FileEdit();
        public List<CopyList> FileList = new List<CopyList>();
        public AdditionalSource()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(WindowsForm_DragEnter);
            this.DragDrop += new DragEventHandler(WindowsForm_DragDrop);
        }
        void WindowsForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void WindowsForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
            {
                //string test = file;
                FileAttributes attr = File.GetAttributes(file);
                string rsh = Path.GetExtension(file);

                if (rsh == ".rafl" || rsh == ".vafl")
                {
                    bool isVirtual = rsh == ".vafl" ? true : false;

                    var FileList = fileEdit.GetFileList(file);
                    if (FileList.Count != 0)
                        foreach (var elem in FileList) AddFile(elem, isVirtual);
                }
                else if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    FileInfo[] fileInf = fileEdit.SearchFiles(file);
                    foreach (FileInfo fileInfo in fileInf) AddFile(fileInfo);
                }
                else AddFile(file);
            }
            
            RenewInfo();
        }

        private void AddFile(string file)=>AddFile(new FileInfo(file));
        private void AddFile(string file, bool isVirtual)
        {
            if (isVirtual) { }
            else AddFile(file);
        }
        private void AddFile(FileInfo fileInfo)
        {
            if (!FileList.Any(x => x.File == fileInfo.FullName)&& fileInfo.Exists)
                FileList.Add(new CopyList(fileInfo.FullName, fileInfo.Length));
        }
        private void OkBtn_Click(object sender, EventArgs e)
        {
            IsOk = true;
            Close();
        }
        private void CancelBtn_Click(object sender, EventArgs e) => Close();
        private void RenewInfo()
        {
            FileList = FileList.OrderByDescending(x => x.Size).ToList();
            AditionalSourceDataGrid.DataSource = null;
            AditionalSourceDataGrid.DataSource = FileList;
            AditionalSourceDataGrid.Columns["File"].Width = 500;
            AditionalSourceDataGrid.Columns["ForDel"].Visible = false;
            AditionalSourceDataGrid.Columns["Hesh"].Width = 50;
            AditionalSourceDataGrid.Columns["Copy"].Visible = false;
            AditionalSourceDataGrid.Columns["Size"].Width = 60;
            AditionalSourceDataGrid.Columns["IsVirtual"].Width = 60;
            if (!AditionalSourceDataGrid.Columns.Contains("del"))
            {
                DataGridViewButtonColumn DelButtonColumn = new DataGridViewButtonColumn();
                DelButtonColumn.Visible = true;
                DelButtonColumn.Text = "del";
                DelButtonColumn.Name = "del";
                DelButtonColumn.HeaderText = "del";
                DelButtonColumn.FlatStyle = FlatStyle.Popup;
                //DelButtonColumn.Usecolumntextforbuttonvalue = true;
                DelButtonColumn.Width = 30;
                AditionalSourceDataGrid.Columns.Add(DelButtonColumn);
            }
            var size = FileList.Sum(x => x.Size) / 1048576;
            FileInfoLab.Text = FileList.Count.ToString() + " files  " + Math.Round((double)size, 2) +" Mb";
        }

        //private void RefreshAditionalSourceDataGrid(List<CopyList> copyList)
        //{
        //    BindingSource bind = new BindingSource { DataSource = copyList };
        //    AditionalSourceDataGrid.DataSource = bind;
        //    AditionalSourceDataGrid.Columns["File"].Width = 750;
        //    AditionalSourceDataGrid.Columns["ForDel"].Width = 35;
        //    AditionalSourceDataGrid.Columns["Hesh"].Width = 280;
        //    AditionalSourceDataGrid.Columns["Copy"].Width = 60;
        //    AditionalSourceDataGrid.Columns["FileLength"].Width = 60;
        //    if (!AditionalSourceDataGrid.Columns.Contains("Del"))
        //    {
        //        DataGridViewButtonColumn DelButtonColumn = new DataGridViewButtonColumn();
        //        DelButtonColumn.Visible = true;
        //        DelButtonColumn.Text = "Del";
        //        DelButtonColumn.Name = "Del";
        //        DelButtonColumn.HeaderText = "Del";
        //        DelButtonColumn.FlatStyle = FlatStyle.Popup;
        //        DelButtonColumn.UseColumnTextForButtonValue = true;
        //        DelButtonColumn.Width = 30;
        //        AditionalSourceDataGrid.Columns.Add(DelButtonColumn);
        //    }
        //    if (!AditionalSourceDataGrid.Columns.Contains("Dir"))
        //    {
        //        DataGridViewButtonColumn DirButtonColumn = new DataGridViewButtonColumn();
        //        DirButtonColumn.Visible = true;
        //        DirButtonColumn.Text = "Dir";
        //        DirButtonColumn.Name = "Dir";
        //        DirButtonColumn.HeaderText = "Dir";
        //        DirButtonColumn.FlatStyle = FlatStyle.Popup;
        //        DirButtonColumn.UseColumnTextForButtonValue = true;
        //        DirButtonColumn.Width = 30;
        //        AditionalSourceDataGrid.Columns.Add(DirButtonColumn);
        //    }

        //    foreach (DataGridViewRow row in AditionalSourceDataGrid.Rows)
        //        if ((bool)row.Cells["ForDel"].Value) row.DefaultCellStyle.BackColor = Color.DimGray;
        //}

        private void DelAllBtn_Click(object sender, EventArgs e)
        {
            FileList.Clear();
            RenewInfo();
        }
    }
}
