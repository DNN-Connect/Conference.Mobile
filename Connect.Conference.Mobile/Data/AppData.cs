using SQLite;
using Xamarin.Forms;

namespace Connect.Conference.Mobile.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
    public class AppData
    {
        public SQLiteConnection db { get; private set; }
        public AppData()
        {
            db = DependencyService.Get<ISQLite>().GetConnection();
            if (db.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'Conferences'") == 0)
            {
                db.CreateTable<Models.Conference>();
            }
        }
        public TableQuery<Models.Conference> Conferences
        {
            get
            {
                return db.Table<Models.Conference>().OrderBy(c => c.Name);
            }
        }
    }
}
