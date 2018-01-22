using FileTransferService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLocalToEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            ILocalDb localDb = new LocalDb();
            localDb.CreateDatabase();
           
            ILocalFileReader fileReader = new LocalFileReader();
            IList<string> files = fileReader.ReadFilesFromDiectory(AppConfiguration.LocalDirectory);
            
            ILocalReadDb readFromTable = new LocalDb();

            foreach (string file in files)
            {
                if(!readFromTable.IsFileExist(file))
                {
                    byte[] fileData = fileReader.ReadFileDataFromDiectory(file);
                    if (fileData != null)
                    {
                        try
                        {
                            string[] to = { AppConfiguration.DestinationEmailAddress };
                            EmailManager email = new EmailManager(to, "Hello", "test file attacment", fileData, file);
                            email.Send();
                            localDb.InsertDatabase(file);
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
                
            }
        }
    }
}
