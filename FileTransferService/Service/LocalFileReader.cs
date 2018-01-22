using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransferService.Service
{
    public class LocalFileReader : ILocalFileReader
    {
        public IList<string> ReadFilesFromDiectory(string directoryPath)
        {
            IList<string> files = Directory.GetFiles(@""+ directoryPath).ToList();
            return files;
        }

        public byte[] ReadFileDataFromDiectory(string filePath)
        {
            try
            {
                byte[] fileData = File.ReadAllBytes(filePath);
                return fileData;
            }
            catch
            {
                return null;
            }
           
        }
    }
}
