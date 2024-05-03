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
        private List<CopyList> AdditionalCopyList = new List<CopyList>();
        private List<CopyList> CheckFileList = new List<CopyList>();
        private object _context;
        private FileList fileList;
        private bool serchInDirectory = true;

        public Form1()
        {
            InitializeComponent();

            textdir.Text = @"E:\Test";
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
                for (int i = dataGru.RowCount - 1; i > -1; i--)
                {
                    if ((bool)dataGru["ForDel", i].Value) RTB.Text += DelFile(dataGru["File", i].Value.ToString(), i);
                }
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

                    RTB.Text += "Start search" + "\nDir - " + textdir.Text;
                    await Task.Run(() => { fileList.MadeList(_context); });
                    CheckFileList = fileList.GetList();
                    RTB.Text += "\nFinish " + CheckFileList.Count();
                }


                if (AditionalOptionsChkBox.Checked && AdditionalCopyList.Count > 0)
                {

                    if (CheckFileList.Count > 0)
                    {
                        //CheckFileList = Merg(CheckFileList, AdditionalCopyList);
                    }
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
            if (e.RowIndex > -1)
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

        private void CountFilesInDirBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textdir.Text) && Directory.Exists(textdir.Text))
            {
                List<DirInfo> dirInfoList = new List<DirInfo>();
                DirectoryInfo Dires = new DirectoryInfo(textdir.Text);
                foreach (var Dir in Dires.GetDirectories())
                {
                    var Files = Directory.GetFiles(Dir.FullName, "*.*", SearchOption.AllDirectories);
                    long size = Files.Select(x => new FileInfo(x).Length).Sum();

                    dirInfoList.Add(new DirInfo
                    {
                        Name = Dir.FullName,
                        FileNumber = Files.Count(),
                        Size = size,
                        TextSize = size > 1048576 ? (size/ 1048576).ToString() + " Mb" : size + " Kb"
                    });
                }
                BindingSource bind = new BindingSource { DataSource = dirInfoList };
                dataGru.DataSource = bind;

                dataGru.Columns["Name"].Width = 750;
                dataGru.Columns["Size"].Visible = false;
            }
            else
            {
                if (Directory.Exists(textdir.Text)) RTB.Text = "Err: Файл " + textdir.Text + "не найден!!!";
                if (string.IsNullOrEmpty(textdir.Text)) RTB.Text = "Err: string.IsNullOrEmpty(textdir.Text)!!!";
            }
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
        private void DirInfoBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textdir.Text) && Directory.Exists(textdir.Text))
            {
                string txt = string.Empty;
                DirectoryInfo Dires = new DirectoryInfo(textdir.Text);
                foreach (var Dir in Dires.GetDirectories())
                {
                    var Files = Directory.GetFiles(Dir.FullName, "*.*", SearchOption.AllDirectories);
                    long size = 0;
                    foreach (var file in Files) size += new FileInfo(file).Length;
                    txt += Dir.FullName + "\\" + Files.Count() + "\\" + size + "\n";
                }
                RTB.Text = txt;
            }
            else RTB.Text = "Err такой папки не ообнаруженно!!!\n" + textdir.Text;
        }
    }
}
