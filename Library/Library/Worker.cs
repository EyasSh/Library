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
        private string userName;
        private string password;
       
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
        public string UserName { get; set; }
        public string Password { get; set; }
        #endregion
        public Worker(string Name, string userName, string password)
        {
            this.Name = Name;
            _id = Guid.NewGuid();
            UserName = userName;
            Password = password;
        }
        public void AddBook(Library lib,Book book)
        {
            if (lib is not null && lib.WorksAtLib(this))
                lib.AddBook(this, book);
        }
        public void RemoveBook(Library lib, Book book)
        {
            if (lib.WorksAtLib(this))
                lib.RemoveBook(this, book);
        }
           
    }
    public class Manager : Worker
    {
        public Manager(string Name, string userName, string password):base(Name, userName,password)
        {
        }
        public void Hire(Library l,Worker w)
        {
            l.AddWorker(this, w);
        }
      
    }
    public sealed class Admin : Manager
    {
        public Admin(string Name,string username,string password):base(Name,username,password)
        {

        }
        public void Promote(Library l,Worker w)
        {
            l.PromoteWorker(this, w);
        }
        public void Demote(Library l,Worker w)
        {
            l.DemoteWorker(this, w);
        }
        public void Fire(Library l, Worker w)
        {
            l.RemoveWorker(this, w);
        }
    }
}
