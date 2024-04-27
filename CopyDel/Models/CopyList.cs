namespace CopyDel.Models
{
    public class CopyList
    {
        public string File { get; set; }
        public string Hesh { get; set; }
        public bool ForDel { get; set; } = false;
        public int Copy { get; set; } = -1;
        public long FileLength { get; set; } =0;
        public bool IsVirtual { get; set; } = false;

        public CopyList(string file, string hesh, int copy)
        {
            File = file;
            Hesh = hesh;
            Copy = copy;
        }

        public CopyList(string file, string hesh, long fileLength)
        {
            File = file;
            Hesh = hesh;
            FileLength = fileLength;
        }

        public CopyList(string file, string hesh)
        {
            File = file;
            Hesh = hesh;
        }

        public CopyList(string file, string hesh, bool isVirtual)
        {
            File = file;
            Hesh = hesh;
            IsVirtual = isVirtual;
        }

        public CopyList(string file, string hesh, long fileLength , bool isVirtual)
        {
            File = file;
            Hesh = hesh;
            IsVirtual = isVirtual;
            FileLength = fileLength;
        }

        public CopyList(string file,  long fileLength)
        {
            File = file;
            FileLength = fileLength;
        }
    }
}
