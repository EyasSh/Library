using System.Text.Json;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            //Remaining are DB to write to files and serch functionality for books
            //then continue functionality for Manager + Admin
            Library l= Library.GetInstance();
            Admin a = new("Juan", "Juanito", "123");
            
            
            
            //string filePath = "Worker.json";
            //string jsonData = JsonSerializer.Serialize(a +$",role= {nameof(Admin)}", new JsonSerializerOptions
            //{
            //    WriteIndented = true // Adds indentation for better readability
            //});
            //File.WriteAllText(filePath, jsonData);

            //Console.WriteLine("Data has been written to " + filePath);
            //Console.WriteLine(Directory.GetCurrentDirectory());
            //Console.WriteLine("No errs");

        }
    }
}
