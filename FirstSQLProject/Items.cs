using System;

namespace FirstSQLProject
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Items()
        {
        }

        public Items(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
