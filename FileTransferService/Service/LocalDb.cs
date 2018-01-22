using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransferService.Service
{
    public class LocalDb : ILocalDb, ILocalReadDb
    {
        private static string connection = "Data Source=LocalFile.sqlite;Version=3;";
        public void CreateDatabase()
        {
            if (!System.IO.File.Exists("LocalFile.sqlite"))
            {
                SQLiteConnection.CreateFile("LocalFile.sqlite");
                SQLiteConnection m_dbConnection = new SQLiteConnection(connection);
                m_dbConnection.Open();

                string sql = @"CREATE TABLE IF NOT EXISTS [Files] (
                        [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [FileName] NVARCHAR(2048)  NULL
                        )";

                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                m_dbConnection.Close();
            }
        }

        public void InsertDatabase(string fileName)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(connection);
            m_dbConnection.Open();

            string sql = @"insert into Files (FileName) values('" + fileName + "')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        public IList<string> GetFileFromTable(string fileName)
        {
            IList<string> Files = new List<string>();
            using (SQLiteConnection connect = new SQLiteConnection(connection))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    fmd.CommandText = @"SELECT FileName FROM Files where FileName='" + fileName + "'";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader r = fmd.ExecuteReader();
                    while (r.Read())
                    {
                        Files.Add(Convert.ToString(r["FileName"]));
                    }
                }
            }
            return Files;
        }

        public IList<string> GetAllFilesFromTable()
        {
            IList<string> Files = new List<string>();
            using (SQLiteConnection connect = new SQLiteConnection(connection))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    fmd.CommandText = @"SELECT FileName FROM Files ";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader r = fmd.ExecuteReader();
                    while (r.Read())
                    {
                        Files.Add(Convert.ToString(r["FileName"]));
                    }
                }
            }
            return Files;
        }

        public bool IsFileExist(string fileName)
        {
            IList<string> Files = new List<string>();
            using (SQLiteConnection connect = new SQLiteConnection(connection))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    fmd.CommandText = @"SELECT FileName FROM Files where FileName='" + fileName + "'";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader r = fmd.ExecuteReader();
                    while (r.Read())
                    {
                        Files.Add(Convert.ToString(r["FileName"]));
                    }
                }
            }

            if (Files.Count > 0)
                return true;
            else
                return false;

        }
    }
}
