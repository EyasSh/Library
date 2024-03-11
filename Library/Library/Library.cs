using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Library
    {
        static Library? lib = null;
        List<Worker> Workers = new List<Worker>();
        List<Book> Books = new List<Book>();
        private Library()
        {
            Workers.Add(new Admin("Default"));
        }
        public static Library GetInstance()
        {
            if (lib == null)
            {
                lib = new Library();
                return lib;
            }
            return lib;
        }
        public bool WorksAtLib(Worker worker)
        {
            foreach (Worker w in Workers)
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
            if (book != null)
            {
                Books.Add(book);
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
                    Books.Remove(book);
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
            if (worker != null && adder != null)
            {
                Workers.Add(worker);
                MessageHandler.SuccessMsg($"Worker {worker.Name}" +
                    $"added successfully by {adder.Name}");
            }
            else
                MessageHandler.FailureMsg("Failiure to add worker");
        }
        public void RemoveWorker(Admin remover, Worker worker)
        {
            if (worker != null && remover != null)
            {
                Workers.Remove(worker);
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
                        Manager a = new Manager(worker.Name);
                        Workers.Remove(worker);
                        Workers.Add(a);
                        MessageHandler.SuccessMsg($"Worker {worker.Name}" +
                    $"promoted to manager successfully by {admin.Name}");
                    }

                    break;
                case '2':
                    if (worker != null && worker is not Manager && worker is not Admin)
                    {
                        Admin a = new Admin(worker.Name);
                        MessageHandler.SuccessMsg($"Worker {worker.Name}" +
                    $"promoted to admin successfully by {admin.Name}");
                    }
                    break;
                default:
                    MessageHandler.FailureMsg("Wrong charecter pressed");
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
                Manager m = worker as Manager;
                Workers.Remove(worker);
                Workers.Add(m);
            }
            else if (bothWorkAtLib && worker is Manager)
            {
                Worker w = worker as Worker;
                Workers.Remove(worker);
                Workers.Add(w);
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
