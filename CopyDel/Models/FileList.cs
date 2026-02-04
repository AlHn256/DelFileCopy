using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CopyDel.Models
{
    public class FileList
    {
        private FileEdit fileEdit = new FileEdit();
        private List<CopyList> CheckFileList { get; set; }
        private String Dir { get; set; }
        private long MaxLenghtFile { get; set; }
        private int serrchOption { get; set; }
        private bool _canselled { get; set; }
        private string[] FileFilter { get; set; } = new string[1] { "*.*" };
        public void Cansel() =>_canselled = true;

        public FileList(string dir, long maxLenghtFile, string[] fileFilter ,  bool srchOptions = false) 
        {
            MaxLenghtFile = maxLenghtFile;
            Dir = dir;
            _canselled = false;
            CheckFileList = new List<CopyList>();
            FileFilter = fileFilter;
            if (srchOptions) serrchOption = 1;
            else serrchOption = 0;
        }
        public List<CopyList> GetList() => CheckFileList;
        public List<CopyList> MadeList(object param)
        {
            SynchronizationContext context = (SynchronizationContext)param;
            FileInfo[] fileList = new FileInfo[0];
            
            if (FileFilter.Length > 0) fileList = fileEdit.SearchFiles(Dir, FileFilter, serrchOption);
            else fileEdit.SearchFiles(Dir);
            
            if(fileList==null)return new List<CopyList>();
            if (fileList.Length > 0)
            {
                int i = 0;
                foreach (var fileInf in fileList)
                {
                    if (_canselled) break;
                    string file = fileInf.FullName;
                    if (MaxLenghtFile == 0)
                    {
                        string md5 = fileEdit.ComputeMD5Checksum(file);
                        CheckFileList.Add(new CopyList(file, md5, fileInf.Length));
                    }
                    else
                    {
                        if (fileInf.Length < MaxLenghtFile)
                        {
                            string md5 = fileEdit.ComputeMD5Checksum(file);
                            CheckFileList.Add(new CopyList(file, md5));
                        }
                        else CheckFileList.Add(new CopyList(file, "", fileInf.Length));
                    }
                    context.Send(OnProgressChanged, ++i * 100 / fileList.Length);
                }
                context.Send(OnProgressChanged, 100);
            }

            return CheckFileList;
        }

        public void OnProgressChanged(object i)
        {
            if (ProcessChanged != null) ProcessChanged((int)i);
        }

        public event Action<int> ProcessChanged;
    }
}
