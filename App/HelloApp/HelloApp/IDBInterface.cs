using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HelloApp
{
    public interface IDBInterface
    {
        SQLiteConnection CreateConnection();
    }
}
