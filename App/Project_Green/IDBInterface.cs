using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Project_Green
{
    public interface IDBInterface
    {
        SQLiteConnection CreateConnection();
    }
}
