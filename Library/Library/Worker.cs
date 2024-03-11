using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Worker
    {
        private Guid _id;
        private string name;
        Library l = Library.GetInstance();
        #region Props
        public Guid Id { get; }
        public string Name
        {
            get { return name ; }
            set
            {
                if (value=="")
                {
                    name = "John Doe";
                }
                else
                {
                    name= value;
                }
            } 
        }
        #endregion
        public Worker(string Name)
        {
            this.Name = Name;
            _id = Guid.NewGuid();
        }
        public void AddBook(Book book)
        {
            if (l.WorksAtLib(this))
                l.AddBook(this, book);
        }
        public void RemoveBook(Book book)
        {
            if (l.WorksAtLib(this))
                l.RemoveBook(this, book);
        }
           
    }
    public class Manager : Worker
    {
        public Manager(string Name):base(Name)
        {

        }
      
    }
    public sealed class Admin : Manager
    {
        public Admin(string Name):base(Name)
        {

        }
    }
}
