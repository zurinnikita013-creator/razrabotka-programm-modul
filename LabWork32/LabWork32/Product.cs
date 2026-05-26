using System;

namespace LabWork32
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Rating { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price} руб.";
        }
    }
}