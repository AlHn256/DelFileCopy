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
        public List<FileInformation> FileInfoList = new List<FileInformation>();
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

        public class FileInformation
        {
            public string Name { get; set; }
            public string FullName { get; set; }
            public double Size { get; set; }
            public bool IsSelected { get; set; }
            public bool IsVirtual { get; set; }
        }

        void WindowsForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //AdditionalSourceList.Clear();
            foreach (var file in files)
            {
                string test = file;
                FileAttributes attr = File.GetAttributes(file);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    FileInfo[] fileInf = fileEdit.SearchFiles(file);
                    foreach (FileInfo fileInfo in fileInf)
                        FileInfoList.Add(new FileInformation()
                        {
                            FullName = fileInfo.FullName,
                            Name = fileInfo.Name,
                            Size = fileInfo.Length,
                            IsSelected = false
                        });
                }
                else
                {
                    var fileInfo = new FileInfo(file);
                    FileInfoList.Add(new FileInformation()
                    { 
                        FullName = fileInfo.FullName,
                        Name = fileInfo.Name,
                        Size = fileInfo.Length,
                        IsSelected = false
                    });
                }
                    
            }
            AditionalSourceDataGrid.DataSource = null;
            AditionalSourceDataGrid.DataSource = FileInfoList;
            RenewInfo();
        }
        private void OkBtn_Click(object sender, EventArgs e)
        {
            IsOk = true;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)=>Close();

        // Переименование файлов в обратном направлении
        //private void TestBtn_Click(object sender, EventArgs e)
        //{

        //    string Dir = "D:\\Work\\Exampels\\20";
        //    var files = fileEdit.SearchFiles(Dir);

        //    files = files.OrderByDescending(x=>x.Name).ToArray();

        //    if (files.Length > 0)
        //    {
        //        for (int i = 0;i< files.Length; i++)
        //        {
        //            string file = i<10? "00" + i + ".bmp":  "0" +i+".bmp";
        //            string newfilename = Dir +"\\"+ file;
        //            File.Move(files[i].FullName, newfilename);
        //        }
        //    }
        //}

        private void RenewInfo()
        {
            var size = FileInfoList.Sum(x => x.Size)/ 1048576;
            FileInfoLab.Text = FileInfoList.Count.ToString() + " files  " + Math.Round(size, 2) +" Mb";
        }
        private void DelAllBtn_Click(object sender, EventArgs e)
        {
            FileInfoList.Clear();
            AditionalSourceDataGrid.DataSource = null;
            AditionalSourceDataGrid.DataSource = FileInfoList;
            RenewInfo();
        }
    }
}
