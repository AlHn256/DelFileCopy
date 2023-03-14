using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace CopyDel
{
    public partial class Form1 : Form
    {
        string Dir = @"F:\img\test\";
        public Form1()
        {
            InitializeComponent();

            textdir.Text = Dir;
            this.AllowDrop = true;
            richTextBox1.AllowDrop = true;
            this.DragEnter += new DragEventHandler(WindowsForm_DragEnter);
            this.DragDrop += new DragEventHandler(WindowsForm_DragDrop);
            richTextBox1.DragEnter += new DragEventHandler(WindowsForm_DragEnter);
            richTextBox1.DragDrop += new DragEventHandler(WindowsForm_DragDrop);
        }

        List<CopyList> CList;

        public class CopyList
        {
            public string File { get; set; }
            public string Hesh { get; set; }
            public int Copy { get; set; }

            public CopyList(string File, string Hesh, int Copy)
            {
                this.File = File;
                this.Hesh = Hesh;
                this.Copy = Copy;
            }
        }

        private string ComputeMD5Checksum(string path)
        {
            using (FileStream fs = System.IO.File.OpenRead(path))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                byte[] checkSum = md5.ComputeHash(fileData);
                //string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
                string result = BitConverter.ToString(checkSum);
                return result;
            }
        }


        private void FindCopy(bool Del)
        {
            richTextBox1.Text = "";
            CList = new List<CopyList>();
            string text="";

            if (System.IO.Directory.Exists(Dir) == true)
            {
                text += "\nDir - " + Dir;
                string[] dirs = Directory.GetFiles(Dir, "*.*");

                text += "\n" + dirs.Length + " files";

                if (dirs.Length != 0)
                {
                    foreach (string file in dirs)
                    {
                        string md5 = ComputeMD5Checksum(file);
                        CopyList elm = new CopyList(file, md5,-1); CList.Add(elm);
                    }

                    int i = 0, j = 0;
                    for (i = 0; i < CList.Count() - 1; i++)
                    {
                        string HeshI = CList[i].Hesh;
                        for (j = i + 1; j < CList.Count(); j++)
                        {
                            if (CList[j].Copy == -1 && HeshI == CList[j].Hesh)
                            {
                                CList[i].Copy = i;
                                CList[j].Copy = i;
                            }
                        }
                    }

                    i = 0; j = 0;
                    for (i = 0; i < CList.Count; i++)
                    {
                        if (CList[i].Copy != -1 && CList[i].Copy != i)
                        {
                            j++;
                            text += "\n" + i + " " + CList[i].Copy + " " + CList[i].File + " " + CList[i].Hesh + "    -    COPY";
                            if (Del == true) { File.Delete(CList[i].File); text += "\n" + CList[i].File + "    -   DEL"; }
                        }
                    }
                    text += "\n" + j + " Копий";
                }
            }
            richTextBox1.Text += text;
        }

        private void test_Click(object sender, EventArgs e){FindCopy(true);}
        private void button1_Click(object sender, EventArgs e){FindCopy(false);}
        void WindowsForm_DragEnter(object sender, DragEventArgs e){if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;}
        void WindowsForm_DragDrop(object sender, DragEventArgs e)
        {
            richTextBox1.Text = "";
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                FileAttributes attr = File.GetAttributes(file);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory) textdir.Text =  file;
                else textdir.Text=Path.GetDirectoryName(file);
                Dir = textdir.Text;
            }
        }
    }
}
