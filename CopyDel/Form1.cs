using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using CopyDel.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace CopyDel
{
    public partial class Form1 : Form
    {
        string Dir = @"E:\Test";
        List<CopyList> CheckFileList;
        private object _context;
        private FileList fileList;

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

            Load += MainForm_Load;
            checkFilesBox.Checked = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _context = SynchronizationContext.Current;
        }

        private async void FindCopy(bool Del)
        {
            if(dataGru.RowCount>0 && Del)
            {
                for(int i= dataGru.RowCount-1; i>-1 ; i--)
                {
                    bool ForDel = (bool)dataGru["ForDel", i].Value;
                    if(ForDel) richTextBox1.Text += DelFile(dataGru["File", i].Value.ToString(), i);
                }
            }
            else
            {
                CheckFileList = new List<CopyList>();
                if (Directory.Exists(Dir))
                {
                    long maxLenghtFile = 0;
                    long.TryParse(MaxLenghtFile.Text, out maxLenghtFile);
                    fileList = new FileList(Dir, maxLenghtFile, checkFilesBox.Checked);
                    fileList.ProcessChanged += worker_ProcessChanged;

                    string text = string.Empty;
                    richTextBox1.Text += "Start search" + "\nDir - " + Dir;
                    await Task.Run(() => { fileList.MadeList(_context); });
                    CheckFileList = fileList.GetList();
                    richTextBox1.Text += "\nFinish " + CheckFileList.Count();

                    int i = 0, j = 0;
                    for (i = 0; i < CheckFileList.Count() - 1; i++)
                    {
                        if (CheckFileList[i].Copy > -1) continue;
                        string heshI = CheckFileList[i].Hesh;
                        long fileLength = CheckFileList[i].FileLength;


                        for (j = i + 1; j < CheckFileList.Count(); j++)
                        {
                            if (CheckFileList[j].Copy > -1) continue;
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
                                    if (elem.FileLength == 0) text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Hesh + "  - DELETED by HeshCOPY";
                                    else text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.FileLength + "  - DELETED by LengthCOPY";
                                }
                                else
                                {
                                    if (elem.FileLength == 0) text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Hesh + "  -  HeshCOPY FOR DELETE";
                                    else text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.FileLength + "  -  LengthCOPY FOR DELETE";
                                }
                            }
                            else
                            {
                                if (elem.FileLength == 0) text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.Hesh + "  -  HeshCOPY";
                                else text += "\n" + i + " " + elem.Copy + " " + elem.File + " " + elem.FileLength + "  -  LengthCOPY";
                            }
                            i = elem.Copy;
                        }
                        if (nDelFiles > 0) text += "\n" + nDelFiles + " Deleted Files!!!";
                    }

                    RefreshDataGru(copyList);
                    if(copyList.Count==0) richTextBox1.Text = text + "\n" + "Kопий Nет!";
                    else richTextBox1.Text = text + "\n" + copyList.Count + " kопий ";
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
            dataGru.Columns["FileLength"].Width = 60;
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
                if ((bool)row.Cells["ForDel"].Value)
                {
                    row.DefaultCellStyle.BackColor = Color.DimGray;
                }
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
            if (e.RowIndex > -1)
            {
                string file = dataGru["File", e.RowIndex].Value.ToString();
                string dir = Path.GetDirectoryName(file);
                var asd = dataGru.Columns[e.ColumnIndex].Name;
                if (dataGru.Columns[e.ColumnIndex].Name == "Del")
                {
                    richTextBox1.Text += DelFile(file, e.RowIndex);

                }
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

        private void checkBoxByDir_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxByDir.Checked)
            {
                if (!string.IsNullOrEmpty(textdir.Text) && Directory.Exists(textdir.Text))
                {
                    string txt = string.Empty;
                    DirectoryInfo Dires = new DirectoryInfo(textdir.Text);
                    foreach (var Dir in Dires.GetDirectories())
                    {
                        var Files = Directory.GetFiles(Dir.FullName,"*.*", SearchOption.AllDirectories);
                        long size = 0;
                        foreach (var file in Files)size += new FileInfo(file).Length;
                        txt += Dir.FullName + "\\" + Files.Count() + "\\" + size + "\n";
                    }
                    richTextBox1.Text = txt;
                }

                checkFilesBox.Checked = false;
                checkFilesBox.Enabled = false;
            }
            else
            {
                checkFilesBox.Enabled = true;
            }
        }
    }
}
