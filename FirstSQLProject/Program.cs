using System;

namespace FirstSQLProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = new Class1();
            conn.Connection();
            conn.SaveToJson();
            conn.InsertDb();

        }

    }
}
