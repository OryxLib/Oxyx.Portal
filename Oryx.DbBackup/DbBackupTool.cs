using MySql.Data.MySqlClient;
using System;

namespace Oryx.DbBackup
{
    public class DbBackupTool
    {
        public void CreateBackup(string connstring, string fileTarget)
        {
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(fileTarget);
                        conn.Close();
                    }
                }
            }
        }

        public void RestoreDB(string connstring, string fileTarget)
        {
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(fileTarget);
                        conn.Close();
                    }
                }
            }
        }
    }
}
