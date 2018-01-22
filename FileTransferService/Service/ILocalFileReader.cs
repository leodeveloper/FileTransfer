using System.Collections.Generic;

namespace FileTransferService.Service
{
    public interface ILocalFileReader
    {
        IList<string> ReadFilesFromDiectory(string directoryPath);
        byte[] ReadFileDataFromDiectory(string filePath);
    }
}