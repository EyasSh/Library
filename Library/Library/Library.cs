using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
   public class Library
    {
        private static Library lib;
        List<Worker> workers = new List<Worker>();
        List<Book> books = new List<Book>();
        
        private Library(Admin a)
        {
            workers.Add(a);
        }
        public static Library GetInstance()
        {

            if (lib == null)
            {
                Admin a = new("John Doe", "JD","Dlay");
                Console.WriteLine(a==null);
                lib = new Library(a);
                Console.WriteLine("Admin added");
                return lib;
            }
            return lib;
        }
        public bool WorksAtLib(Worker worker)
        {
            foreach (Worker w in workers)
            {
                if (w.Id.ToString() == worker.Id.ToString())
                {
                    return true;
                }

            }
            return false;
        }
        public void AddBook(Worker w, Book book)
        {
            if (WorksAtLib(w) &&book != null)
            {
               books.Add(book);
                MessageHandler.SuccessMsg($"Book Added by {w.Name}");
            }
            else
            {
                MessageHandler.FailureMsg($"Book was not added. Invalid " +
                    $"authorization detected by {w.Name}");
            }


        }
        public void RemoveBook(Worker worker, Book book)
        {
            if (book == null || worker == null)
            {
                MessageHandler.FailureMsg("Book or worker are null");
            }
            else
            {
                try
                {
                    books.Remove(book);
                    MessageHandler.SuccessMsg($"{book.Name} removed by {worker.Name}");
                }
                catch (Exception e)
                {
                    MessageHandler.FailureMsg($" problem removing book exception:-\n{e.Message}");
                }

            }
        }
        public void AddWorker(Manager adder, Worker worker)
        {
            if (adder!=null && worker!=null && WorksAtLib(adder))
            {
                workers.Add(worker);
                MessageHandler.SuccessMsg($"Worker {worker.Name}" +
                    $"added successfully by {adder.Name}");
            }
            else
                MessageHandler.FailureMsg("Failiure to add worker either the adder or the worker are null \nor theadder does not work at lib");
        }
        public void RemoveWorker(Admin remover, Worker worker)
        {
            if ((worker != null && remover != null) && (WorksAtLib(remover) && WorksAtLib(worker)))
            {
                workers.Remove(worker);
                MessageHandler.SuccessMsg($"Worker {worker.Name}" +
                    $"added successfully by {remover.Name}");
            }
            else
                MessageHandler.FailureMsg("Failiure to remove worker");
        }
        public void PromoteWorker(Admin admin, Worker worker)
        {
            Console.WriteLine("Press 1 to promote worker to Manager" +
                "or 2 to promote worker to Admin");
            char c = Console.ReadKey().KeyChar;
            switch (c)
            {
                case '1':
                    if (worker != null && worker is not Manager && worker is not Admin)
                    {
                        Manager a = new Manager(worker.Name, worker.UserName,worker.Password);
                        workers.Remove(worker);
                        workers.Add(a);
                        MessageHandler.SuccessMsg($"Worker {worker.Name}" +
                        $"promoted to manager successfully by {admin.Name}");
                    }

                    break;
                case '2':
                    if (worker != null && worker is not Manager && worker is not Admin)
                    {
                        Admin a = new Admin(worker.Name, worker.UserName, worker.Password);
                        workers.Remove(worker);
                        workers.Add(a);
                        MessageHandler.SuccessMsg($"Worker {worker.Name}" +
                    $"promoted to admin successfully by {admin.Name}");
                    }
                    break;
                default:
                    MessageHandler.FailureMsg("Wrong charecter pressed/ null admin or worker");
                    break;


            }
        }
        /*
         * <Summary> @param admin an admin whose job is to demote a worker </Summary>
         */
        public void DemoteWorker(Admin admin, Worker worker)
        {
            bool bothWorkAtLib = (admin != null && worker != null) && (lib.WorksAtLib(admin) && lib.WorksAtLib(worker));
            if (worker is null || admin is null)
            {
                MessageHandler.FailureMsg("One of the workers is null or does not work at library");
                return;
            }

            if (bothWorkAtLib && worker is Admin)
            {
                
                if (worker != null)
                {
                    Manager m = new(worker.Name,worker.UserName,worker.Password);
                    workers.Add(m);
                    workers.Remove(worker);
                }
                else
                {
                    MessageHandler.FailureMsg("Manager object is null exiting program Library.cs Line:154");
                    return ;
                }
               
                
            }
            else if (bothWorkAtLib && worker is Manager)
            {
                Worker w = new(worker.Name,worker.UserName,worker.Password);
                workers.Remove(worker);
                workers.Add(w);
            }
            else
            {
                Console.WriteLine("Worker is already domoted to the lowest level would you like to fire them y/n");
                try
                {
                    char ch = char.Parse(Console.ReadLine());
                    if (char.ToLower(ch) != 'y' || char.ToLower(ch) != 'n')
                    {
                        Console.WriteLine("Charecter must be y/n");
                        return;
                    }

                    else if (char.ToLower(ch) == 'y')
                    {
                        RemoveWorker(admin, worker);
                        return;
                    }

                    else
                    {
                        Console.WriteLine("Process finished sucessfully worker was not fired");
                        return;
                    }
                }
                catch
                {
                    Console.WriteLine("Cannot take more than one charecter either y/n process will end");
                    return;
                }

            }


        }
    }

    public class MessageHandler
    {

        public static void SuccessMsg(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void FailureMsg(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
