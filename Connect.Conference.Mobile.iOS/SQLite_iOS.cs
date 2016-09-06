using Connect.Conference.Mobile.Data;
using Connect.Conference.Mobile.iOS;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_iOS))]
namespace Connect.Conference.Mobile.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "Connect.Conference.Mobile.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            //if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}
