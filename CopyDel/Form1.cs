using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using CopyDel.Models;
using System.Threading.Tasks;
using System.Threading;

namespace CopyDel
{
    public partial class Form1 : Form
    {
        //string Dir = @"D:\Pr";
        string Dir = @"E:\X File I\Pic";
        //string Dir = @"F:\img\test\";
        List<CopyList> CheckFileList;
        private object _context;
        private FileList fileList;


        public Form1()
        {
            InitializeComponent();
            MaxLenghtFile.Text = "16777216";

            textdir.Text = Dir;
            this.AllowDrop = true;
            richTextBox1.AllowDrop = true;
            this.DragEnter += new DragEventHandler(WindowsForm_DragEnter);
            this.DragDrop += new DragEventHandler(WindowsForm_DragDrop);
            richTextBox1.DragEnter += new DragEventHandler(WindowsForm_DragEnter);
            richTextBox1.DragDrop += new DragEventHandler(WindowsForm_DragDrop);

            Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _context = SynchronizationContext.Current;
        }

        private string ComputeMD5Checksum(string path)
        {
            using (FileStream fs = File.OpenRead(path))
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


        private async void FindCopy(bool Del)
        {
            CheckFileList = new List<CopyList>();


            if (Directory.Exists(Dir))
            {
                long maxLenghtFile = 0;
                long.TryParse(MaxLenghtFile.Text, out maxLenghtFile);
                fileList = new FileList(Dir, maxLenghtFile, checkFilesBox.Checked);
                fileList.ProcessChanged += worker_ProcessChanged;

                string text = "\nDir - " + Dir+ "\nStart";
                await Task.Run(() => { fileList.MadeList(_context); });

                CheckFileList = fileList.GetList();
                richTextBox1.Text += "\nFinish " + CheckFileList.Count();


                int i = 0, j = 0;
                for (i = 0; i < CheckFileList.Count() - 1; i++)
                {
                    string heshI = CheckFileList[i].Hesh;
                    long fileLength = CheckFileList[i].FileLength;

                    for (j = i + 1; j < CheckFileList.Count(); j++)
                    {
                        if (fileLength != 0)
                        {
                            if (CheckFileList[j].Copy == -1 && fileLength == CheckFileList[j].FileLength)
                            {
                                CheckFileList[i].Copy = i;
                                CheckFileList[j].Copy = i;
                            }
                        }
                        else
                        {
                            if (CheckFileList[j].Copy == -1 && heshI == CheckFileList[j].Hesh)
                            {
                                CheckFileList[i].Copy = i;
                                CheckFileList[j].Copy = i;
                            }
                        }
                    }
                }



                var copyList = CheckFileList.Where(x => x.Copy != -1).OrderBy(y => y.Copy).ToList();

                if (copyList.Count > 0)
                {
                    i = 0;
                    foreach (var elem in copyList)
                    {
                        if (elem.FileLength == 0)
                        {
                            if (i != elem.Copy)
                            {
                                text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Hesh + "  -  HeshCOPY";
                            }
                            else
                            {
                                text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Hesh + "  -  HeshCOPY FOR DELETE";
                            }
                        }
                        else
                        {
                            if (i != elem.Copy)
                            {
                                text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.FileLength + "  -  LengthCOPY";
                            }
                            else
                            {
                                text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.FileLength + "  -  LengthCOPY FOR DELETE";
                            }
                        }
                        i = elem.Copy;
                    }
                }

                //for (i = 0; i < CheckFileList.Count; i++)
                //{
                //    if (CheckFileList[i].Copy != -1 && CheckFileList[i].Copy != i)
                //    {
                //        j++;
                //        text += "\n" + i + " " + CheckFileList[i].Copy + " " + CheckFileList[i].File + " " + CheckFileList[i].Hesh + "    -    COPY";
                //        if (Del == true) { File.Delete(CheckFileList[i].File); text += "\n" + CheckFileList[i].File + "    -   DEL"; }
                //    }
                //}

                //text += "\n\n" + copyList.Count + " Копий из " + dirs.Length + "файлов";
                richTextBox1.Text = text += "\n\n" + copyList.Count + " kопий "; 
            }

            //if (Directory.Exists(Dir))
            //{
            //    string text = "\nDir - " + Dir;
            //    string[] dirs = Directory.GetFiles(Dir, "*.*");

            //    if (dirs.Length != 0)
            //    {
            //        long maxLenghtFile = 0;
            //        long.TryParse(MaxLenghtFile.Text, out maxLenghtFile);


            //        foreach (string file in dirs)
            //        {
            //            FileInfo fileInf = new FileInfo(file);
            //            if (maxLenghtFile == 0)
            //            {
            //                string md5 = ComputeMD5Checksum(file);
            //                CheckFileList.Add(new CopyList(file, md5, fileInf.Length));
            //            }
            //            else
            //            {
            //                if (fileInf.Length < maxLenghtFile)
            //                {
            //                    string md5 = ComputeMD5Checksum(file);
            //                    CheckFileList.Add(new CopyList(file, md5, fileInf.Length));
            //                }
            //                else
            //                {
            //                    CheckFileList.Add(new CopyList(file, "", fileInf.Length));
            //                }
            //            }
            //        }



            //        int i = 0, j = 0;
            //        for (i = 0; i < CheckFileList.Count() - 1; i++)
            //        {
            //            string heshI = CheckFileList[i].Hesh;
            //            long fileLength = CheckFileList[i].FileLength;

            //            for (j = i + 1; j < CheckFileList.Count(); j++)
            //            {
            //                if (fileLength != 0)
            //                {
            //                    if (CheckFileList[j].Copy == -1 && fileLength == CheckFileList[j].FileLength)
            //                    {
            //                        CheckFileList[i].Copy = i;
            //                        CheckFileList[j].Copy = i;
            //                    }
            //                }
            //                else
            //                {
            //                    if (CheckFileList[j].Copy == -1 && heshI == CheckFileList[j].Hesh)
            //                    {
            //                        CheckFileList[i].Copy = i;
            //                        CheckFileList[j].Copy = i;
            //                    }
            //                }
            //            }
            //        }



            //        var copyList = CheckFileList.Where(x => x.Copy != -1).OrderBy(y => y.Copy).ToList();

            //        if (copyList.Count > 0)
            //        {
            //            i = 0;
            //            foreach (var elem in copyList)
            //            {
            //                if (elem.FileLength == 0)
            //                {

            //                    if (i != elem.Copy)
            //                    {
            //                        text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Hesh + "  -  HeshCOPY";
            //                    }
            //                    else
            //                    {
            //                        text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Hesh + "  -  HeshCOPY FOR DELETE";
            //                    }

            //                }
            //                else
            //                {
            //                    if (i != elem.Copy)
            //                    {
            //                        text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.FileLength + "  -  LengthCOPY";
            //                    }
            //                    else
            //                    {
            //                        text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.FileLength + "  -  LengthCOPY FOR DELETE";
            //                    }
            //                }
            //                i = elem.Copy;
            //            }
            //        }

            //        //for (i = 0; i < CheckFileList.Count; i++)
            //        //{
            //        //    if (CheckFileList[i].Copy != -1 && CheckFileList[i].Copy != i)
            //        //    {
            //        //        j++;
            //        //        text += "\n" + i + " " + CheckFileList[i].Copy + " " + CheckFileList[i].File + " " + CheckFileList[i].Hesh + "    -    COPY";
            //        //        if (Del == true) { File.Delete(CheckFileList[i].File); text += "\n" + CheckFileList[i].File + "    -   DEL"; }
            //        //    }
            //        //}

            //        text += "\n\n" + copyList.Count + " Копий из " + dirs.Length + "файлов";
            //        richTextBox1.Text = text;
            //    }
            //}
        }

        private void test_Click(object sender, EventArgs e) { FindCopy(true); }
        private void button1_Click(object sender, EventArgs e) { FindCopy(false); }
        void WindowsForm_DragEnter(object sender, DragEventArgs e) { if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy; }
        void WindowsForm_DragDrop(object sender, DragEventArgs e)
        {
            richTextBox1.Text = "";
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                FileAttributes attr = File.GetAttributes(file);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory) textdir.Text = file;
                else textdir.Text = Path.GetDirectoryName(file);
                Dir = textdir.Text;
            }
        }

        private void MaxLenghtFile_TextChanged(object sender, EventArgs e)
        {
            long Long = 0;

            if (!long.TryParse(MaxLenghtFile.Text, out Long))
            {

                MaxLenghtFile.Text = "0";
            }
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            Metode();
        }




        async void Metode()
        {

            if (Directory.Exists(Dir))
            {
                long maxLenghtFile = 0;
                long.TryParse(MaxLenghtFile.Text, out maxLenghtFile);
                fileList = new FileList(Dir, maxLenghtFile);
                fileList.ProcessChanged += worker_ProcessChanged;

                richTextBox1.Text = "Start";
                await Task.Run(() =>{fileList.MadeList(_context);});
                richTextBox1.Text += "\nFinish "+ fileList.GetList().Count();

            }
        }

        private void worker_ProcessChanged(int progress)
        {
            progressBar1.Value = progress;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
        }
    }
}
