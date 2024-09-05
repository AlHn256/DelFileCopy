using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using CopyDel.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using CopyDel.NewForms;

namespace CopyDel
{
    public partial class Form1 : Form
    {
        //private string Dir 
        //private System.Security.Principal.WindowsIdentity _principal;
        private List<CopyList> AdditionalCopyList = new List<CopyList>();
        private List<CopyList> CheckFileList = new List<CopyList>();
        private object _context;
        private FileList fileList;
        FileEdit fileEdit = new FileEdit();
        private bool serchInDirectory = true;
        private bool DataGruDirectoryMode = false;

        public Form1()
        {
            InitializeComponent();

            textdir.Text = @"D:\KN";
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(WindowsForm_DragEnter);
            this.DragDrop += new DragEventHandler(WindowsForm_DragDrop);

            checkFilesBox.Checked = true;
            _context = SynchronizationContext.Current;
        }
        private async void FindCopy(bool Del = false)
        {
            if (dataGru.RowCount > 0 && Del)
            {
                string txt = string.Empty;
                for (int i = dataGru.RowCount - 1; i > -1; i--)
                {
                    if ((bool)dataGru["ForDel", i].Value) RTB.Text += DelFile(dataGru["File", i].Value.ToString(), i);
                }
                RTB.Text += txt;
            }
            else
            {
                string text = string.Empty;
                CheckFileList.Clear();

                if (string.IsNullOrEmpty(textdir.Text))
                {
                    RTB.Text = "Err Textdir IsNullOrEmpty!!!";
                    return;
                }


                if (!Directory.Exists(textdir.Text) && !AditionalOptionsChkBox.Checked)
                {
                    RTB.Text = "Err Dir "+ textdir.Text + " не найдена!!!";
                    return;
                }

                if (Directory.Exists(textdir.Text))
                {
                    long maxLenghtFile = 0;
                    long.TryParse(MaxLenghtFile.Text, out maxLenghtFile);
                    fileList = new FileList(textdir.Text, maxLenghtFile, checkFilesBox.Checked);
                    fileList.ProcessChanged += worker_ProcessChanged;

                    RTB.Text = "Start search" + "\nDir - " + textdir.Text;
                    await Task.Run(() => { fileList.MadeList(_context); });
                    CheckFileList = fileList.GetList();
                    RTB.Text += "\nFinish " + CheckFileList.Count();
                }

                if (AditionalOptionsChkBox.Checked && AdditionalCopyList.Count > 0)
                {
                    if (CheckFileList.Count > 0)   Merg();
                    else CheckFileList = AdditionalCopyList;
                }

                int i = 0, j = 0;
                for (i = 0; i < CheckFileList.Count() - 1; i++)
                {
                    if (CheckFileList[i].Copy > -1) continue;
                    string heshI = CheckFileList[i].Hesh;
                    long fileLength = CheckFileList[i].Size;

                    for (j = i + 1; j < CheckFileList.Count(); j++)
                    {
                        if (CheckFileList[j].Copy > -1) continue;
                        if (fileLength != 0)
                        {
                            if (CheckFileList[j].Copy == -1 && fileLength == CheckFileList[j].Size)
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
                    i = -1;
                    int nDelFiles = 0;
                    foreach (var elem in copyList)
                    {
                        if (i == elem.Copy)
                        {
                            elem.ForDel = true;
                            if (Del)
                            {
                                File.Delete(elem.File);
                                if (!File.Exists(elem.File)) nDelFiles++;
                                if (elem.Size == 0) text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Hesh + "  - DELETED by HeshCOPY";
                                else text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Size + "  - DELETED by LengthCOPY";
                            }
                            else
                            {
                                if (elem.Size == 0) text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Hesh + "  -  HeshCOPY FOR DELETE";
                                else text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Size + "  -  LengthCOPY FOR DELETE";
                            }
                        }
                        else
                        {
                            if (elem.Size == 0) text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Hesh + "  -  HeshCOPY";
                            else text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Size + "  -  LengthCOPY";
                        }
                        i = elem.Copy;
                    }
                    if (nDelFiles > 0) text += "\n" + nDelFiles + " Deleted Files!!!";
                }

                RefreshDataGru(copyList);
                if (copyList.Count == 0) RTB.Text = text + "\n" + "Kопий Nет!";
                else RTB.Text = text + "\n" + copyList.Count + " kопий ";
            }
        }

        private void Merg()
        {
            long maxLenghtFile = 0;
            long.TryParse(MaxLenghtFile.Text, out maxLenghtFile);
            
            foreach (CopyList additionalList in AdditionalCopyList)
            {
                if (!CheckFileList.Any(a => a.File == additionalList.File))
                {
                    if(additionalList.Size < maxLenghtFile)
                    {
                        additionalList.Size = 0;
                        FileInfo file = new FileInfo(additionalList.File);
                        if (file.FullName == "D:\\Development\\3.avi")
                        {

                        }
                        else
                        {
                            if (!fileEdit.IsFileLocked(file)) additionalList.Hesh = fileEdit.ComputeMD5Checksum(additionalList.File);
                            else
                            {
                                string fl = file.FullName;
                            }
                        }
                    }

                    //FileInfo fileInf = new FileInfo(additionalList.File);
                    //if (maxLenghtFile == 0)
                    //{
                    //    string md5 = fileEdit.ComputeMD5Checksum(additionalList.File);
                    //    CheckFileList.Add(new CopyList(additionalList.File, md5, fileInf.Length));
                    //}
                    //else
                    //{
                    //    if (fileInf.Length < maxLenghtFile)
                    //    {
                    //        string md5 = fileEdit.ComputeMD5Checksum(additionalList.File);
                    //        CheckFileList.Add(new CopyList(additionalList.File, md5));
                    //    }
                    //    else CheckFileList.Add(new CopyList(additionalList.File, "", fileInf.Length));
                    //}

                    CheckFileList.Add(additionalList);
                }
            }
        }

        private void RefreshDataGru(List<CopyList> copyList)
        {
            BindingSource bind = new BindingSource { DataSource = copyList };
            dataGru.DataSource = bind;
            dataGru.Columns["File"].Width = 750;
            dataGru.Columns["ForDel"].Width = 35;
            dataGru.Columns["Hesh"].Width = 280;
            dataGru.Columns["Copy"].Width = 60;
            dataGru.Columns["Size"].Width = 60;
            if (!dataGru.Columns.Contains("Del"))
            {
                DataGridViewButtonColumn DelButtonColumn = new DataGridViewButtonColumn();
                DelButtonColumn.Visible = true;
                DelButtonColumn.Text = "Del";
                DelButtonColumn.Name = "Del";
                DelButtonColumn.HeaderText = "Del";
                DelButtonColumn.FlatStyle = FlatStyle.Popup;
                DelButtonColumn.UseColumnTextForButtonValue = true;
                DelButtonColumn.Width = 30;
                dataGru.Columns.Add(DelButtonColumn);
            }
            if (!dataGru.Columns.Contains("Dir"))
            {
                DataGridViewButtonColumn DirButtonColumn = new DataGridViewButtonColumn();
                DirButtonColumn.Visible = true;
                DirButtonColumn.Text = "Dir";
                DirButtonColumn.Name = "Dir";
                DirButtonColumn.HeaderText = "Dir";
                DirButtonColumn.FlatStyle = FlatStyle.Popup;
                DirButtonColumn.UseColumnTextForButtonValue = true;
                DirButtonColumn.Width = 30;
                dataGru.Columns.Add(DirButtonColumn);
            }

            DataGruDirectoryMode = false;
            foreach (DataGridViewRow row in dataGru.Rows)
                if ((bool)row.Cells["ForDel"].Value)row.DefaultCellStyle.BackColor = Color.DimGray;
        }

        void WindowsForm_DragEnter(object sender, DragEventArgs e) { if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy; }
        void WindowsForm_DragDrop(object sender, DragEventArgs e)
        {
            RTB.Text = "";
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 1)
            {
                serchInDirectory = false;
                List<DirInfo> dirInfoList = new List<DirInfo>();
                string text = string.Empty;
                foreach (var file in files)
                {
                    long size =  new FileInfo(file).Length;
                    dirInfoList.Add(new DirInfo
                    {
                        Name = file,
                        TextSize = size > 1048576 ? (size / 1048576).ToString() + " Mb" : size + " Kb"
                    });
                    text += file + "\n";
                }

                RTB.Text = text;
                BindingSource bind = new BindingSource { DataSource = dirInfoList };
                dataGru.DataSource = bind;
                DataGruDirectoryMode = false;
                dataGru.Columns["Name"].Width = 750;
                dataGru.Columns["Size"].Visible = false;
                dataGru.Columns["FileNumber"].Visible = false;
            }
            else
            {
                serchInDirectory = true;
                foreach (string file in files)
                {
                    FileAttributes attr = File.GetAttributes(file);
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory) textdir.Text = file;
                    else textdir.Text = Path.GetDirectoryName(file);
                }
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

        private void worker_ProcessChanged(int progress)
        {
            progressBar1.Value = progress;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if(fileList!=null) fileList.Cansel(); 
        }

        private void DataGru_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!serchInDirectory) return;
            if (e.RowIndex > -1 && !DataGruDirectoryMode)
            {
                string file = dataGru["File", e.RowIndex].Value.ToString();
                string dir = Path.GetDirectoryName(file);
                var asd = dataGru.Columns[e.ColumnIndex].Name;
                if (dataGru.Columns[e.ColumnIndex].Name == "Del") RTB.Text += DelFile(file, e.RowIndex);

                if (dataGru.Columns[e.ColumnIndex].Name == "Dir")
                {
                    var proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = dir;
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
                }
            }
        }
        private string DelFile(string file, int rowIndex)
        {
            if (File.Exists(file))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    return file + " - !!!ERRRR!!!  - " + ex.Message + "\n";
                }

                if (!File.Exists(file))
                {
                    dataGru.Rows.RemoveAt(rowIndex);
                    return file + " - deleted!\n";
                }
            }
            return file + " deleted Err!!!\n";
        }


        private void AditionalOptionsChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(AditionalOptionsChkBox.Checked)
            {
                AdditionalSource additionalSource = new AdditionalSource();
                additionalSource.ShowDialog();
                if (additionalSource.IsOk)
                {
                    AdditionalCopyList = additionalSource.FileList;
                }
                else AdditionalCopyList.Clear();
            }
            else AdditionalCopyList.Clear();
        }

        private void DelCopyBtn_Click(object sender, EventArgs e) => FindCopy(true);
        private void ShowCopyBtn_Click(object sender, EventArgs e) => FindCopy();
        //private void DirInfoBtn_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(textdir.Text) && Directory.Exists(textdir.Text))
        //    {
        //        string txt = string.Empty;
        //        List< DirInfo > dirInfoList = new List< DirInfo >();
        //        DirectoryInfo Dires = new DirectoryInfo(textdir.Text);
        //        foreach (var Dir in Dires.GetDirectories())
        //        {
        //            var Files = Directory.GetFiles(Dir.FullName, "*.*", SearchOption.AllDirectories);
        //            long size = Files.Select(x => new FileInfo(x).Length).Sum();

        //            dirInfoList.Add(new DirInfo
        //            {
        //                Name = Dir.FullName,
        //                FileNumber = Files.Count(),
        //                Size = size,
        //                TextSize = size > 1048576 ? (size / 1048576).ToString() + " Mb" : size + " Kb"
        //            });
        //            txt += Dir.FullName + "\\" + Files.Count() + "\\" + size + "\n";
        //        }
        //        dirInfoList = dirInfoList.OrderByDescending(x => x.Size).ToList();
        //        dataGru.DataSource = null;
        //        BindingSource bind = new BindingSource { DataSource = dirInfoList };
        //        dataGru.DataSource = bind;
        //        DataGruDirectoryMode = true;

        //        RTB.Text = txt;
        //    }
        //    else RTB.Text = "Err такой папки не ообнаруженно!!!\n" + textdir.Text;
        //}
        private void CountFilesInDirBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textdir.Text) && Directory.Exists(textdir.Text))
            {
                string txt = string.Empty;
                List<DirInfo> dirInfoList = new List<DirInfo>();
                DirectoryInfo Dires = new DirectoryInfo(textdir.Text);
                foreach (var Dir in Dires.GetDirectories())
                {
                    if(Dir.FullName == @"D:\$RECYCLE.BIN")
                    {

                    }
                    if (fileEdit.CheckAccessToFolder(Dir.FullName) && Dir.FullName != @"D:\$RECYCLE.BIN" && Dir.FullName != @"D:\Development")
                    {
                        // ?? Перед этим нужно проверить все папки на доступ
                        var Files = Directory.GetFiles(Dir.FullName, "*.*", SearchOption.AllDirectories);
                        long size = Files.Select(x => new FileInfo(x).Length).Sum();

                        dirInfoList.Add(new DirInfo
                        {
                            Name = Dir.FullName,
                            FileNumber = Files.Count(),
                            Size = size,
                            TextSize = size > 1048576 ? (size / 1048576).ToString() + " Mb" : size + " Kb"
                        });
                        txt += Dir.FullName + "\\" + Files.Count() + "\\" + size + "\n";
                    }
                    else
                    {
                        dirInfoList.Add(new DirInfo
                        {
                            Name = Dir.FullName,
                            AccessInfo = "К папке нет доступа!"
                        });
                        txt += Dir.FullName + "К папке нет доступа!\n";
                    }
                    // ?? TODO CheckAcsess befor...
                }

                dirInfoList = dirInfoList.OrderByDescending(x => x.Size).ToList();
                BindingSource bind = new BindingSource { DataSource = dirInfoList };
                dataGru.DataSource = bind;
                DataGruDirectoryMode = true;
                dataGru.Columns["Name"].Width = 750;
                dataGru.Columns["Size"].Visible = false;
                RTB.Text = txt;
            }
            else
            {
                if (Directory.Exists(textdir.Text)) RTB.Text = "Err: Файл " + textdir.Text + "не найден!!!";
                if (string.IsNullOrEmpty(textdir.Text)) RTB.Text = "Err: string.IsNullOrEmpty(textdir.Text)!!!";
            }
        }

        class FileInf
        {
            public int Id { get; set; }
            public string File { get; set; }
            public string FileName { get; set; }
            public string FullName { get; set; }
        }

        void CheckBox()
        {
            //string dir = @"D:\$RECYCLE.BIN\S-1-5-21-934136088-583011989-1724144-1283";
            //dir = @"D:\Work\Exampels\22Mn";
            //var gsdf= fileEdit.CheckAccessToFolder(dir);

            int id = 0;
            if(AdditionalCopyList.Count!=0)
            {
                var FileInfList = (from x in AdditionalCopyList
                           where x.File.IndexOf(".frames") != -1
                           select new FileInf()
                           {
                               Id = id++,
                               FullName = x.File,
                               FileName = Path.GetFileName(x.File),
                               File = Path.GetFileNameWithoutExtension(x.File)
                           }).ToList();


                int y = 0;
                string Insert = string.Empty;
                foreach (var elem in FileInfList)
                {
                    if(int.TryParse(elem.File, out y)) elem.Id = y; 
                    else elem.Id = elem.Id * 1000 + 999;
                    Insert += "(" + elem.Id + ",'" + elem.File + "','" + elem.FileName + "','" + elem.FullName + "'),\n";
                }
                
                string saveText = string.Empty;
                AdditionalCopyList.Select(x =>saveText += x.File +"\n").ToArray();
                string saveFile = fileEdit.GetDefoltDirectory() + "files.txt";
                if(fileEdit.SetFileString(saveFile, saveText)) RTB.Text = AdditionalCopyList.Count + " files saved to \n" + saveFile;
                else RTB.Text = fileEdit.ErrText+"\nFile to save " + saveFile;

                saveFile = fileEdit.GetDefoltDirectory() + "files2.txt";

                if (fileEdit.SetFileString(saveFile, Insert)) RTB.Text += "\nSql query \n" + saveFile;
                else RTB.Text += fileEdit.ErrText;
            }
        }

        void CheckBox2()
        {
            string dir = @"D:\Development\3.avi";
            dir = @"D:\Work\Exampels\22Mn\004.bmp";
            
            var gsdf = fileEdit.CheckAccessToFile(dir);
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            CheckBox();
        }
    }
}
