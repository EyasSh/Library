using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book
    {
        public Guid Id { 
            get { return Id; } 
            init 
            { 
                Id = Guid.NewGuid();
            } 
        }
        public string Name { get; init; }
        public string Author { get; init; }
        public Book(string name, string author)
        {
            Name = name;
            Author = author;
        }
        public override bool Equals(object b)
        {
            if(b is Book)
            {
                return Name == ((Book)b).Name && Author == ((Book)b).Author;
            }
            return false;
        }
    }
}
