using System.Collections.Generic;

namespace FileTransferService.Service
{
    public interface ILocalDb
    {
        void CreateDatabase();
        void InsertDatabase(string fileName);
    }

    public interface ILocalReadDb
    {
        IList<string> GetFileFromTable(string fileName);
        bool IsFileExist(string fileName);
        IList<string> GetAllFilesFromTable();
    }
}