using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmpShell.WinShell
{
    public class PifFileParser
    {
        private readonly string filePath = string.Empty;

        public PifFileParser(string filePath)
        {
            this.filePath = filePath;
        }

        public string GetGameName() => Path.GetFileNameWithoutExtension(this.filePath);

        public string GetWorkingDirectory() => this.GetPathValue(0x65);

        public string GetTargetFilePath() => this.GetPathValue(0x24);

        private string GetPathValue(int offset)
        {
            try
            {
                var stream = File.OpenRead(this.filePath);
                if (stream.CanSeek)
                {
                    stream.Seek(offset, 0);
                }
                if (stream.CanRead)
                {
                    var values = new List<int>();
                    var value = stream.ReadByte();
                    var index = 1;
                    values.Add(value);
                    while (value != 0 && stream.CanRead && index < NativeMethods.MAX_PATH)
                    {
                        value = stream.ReadByte();
                        index++;
                        if (value == 0)
                        {
                            break;
                        }
                        values.Add(value);
                    }
                    var path = Encoding.ASCII.GetString(values.Select(x => (byte)x).ToArray());
                    return path;
                }
            }
            catch
            {
            }
            return string.Empty;
        }
    }
}