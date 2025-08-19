using System.Text.Json;

namespace notecli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // SETUP
            string userFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            DirectoryInfo directory = Directory.CreateDirectory(userFilePath + @"\NoteCLI");
            string finalPath = directory.FullName + @"\notes.json";

            bool isJsonFileExists = File.Exists(finalPath);
            string jsonNotes = isJsonFileExists ? File.ReadAllText(finalPath) : "[]";

            List<Note>? notes = JsonSerializer.Deserialize<List<Note>>(jsonNotes);

            if (notes == null)
                notes = new List<Note>();


            // ARGUMENTS HANDLING
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments provided. Type notecli help for usage.");
                return;
            }

            if (args[0] == "help" || args[0] == "h")
            {
                NoteManager.PrintHelp();

            } 
            else if (args[0] == "create" || args[0] == "c")
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("\nPlease provide a note like this:\n" +
                        "   notecli [create | c] [YOUR NOTE]");
                }
                else
                {
                    notes = notes.AddNote(args[1]);
                }
            } 
            else if (args[0] == "delete" || args[0] == "d") 
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("\nPlease provide a note id like this:\n" +
                        "   notecli [delete | d] [NOTE ID]");
                }
                else
                {
                    notes = notes.DeleteNote(args[1]);
                }
            }
            else if (args[0] == "update" || args[0] == "u")
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("\nPlease provide a note id like this:\n" +
                        "   notecli [update | u] [NOTE ID]");
                }
                else
                {
                    notes.UpdateNote(args[1]);
                }
            }
            else if (args[0] == "finish" || args[0] == "f")
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("\nPlease provide a note id like this:\n" +
                        "   notecli [finish | f] [NOTE ID]");
                }
                else
                {
                    notes.FinishNote(args[1]);
                }
            }
            else if (args[0] == "list" || args[0] == "l")
            {
                if(notes.Count == 0)
                {
                    Console.WriteLine("No notes available.");
                }
                else
                {
                    NoteManager.ListNotes(notes);
                }
            }
            else
            {
                Console.WriteLine("Unknown command. Type notecli help for usage.");
            }


            // SAVE NOTES
            jsonNotes = JsonSerializer.Serialize(notes);
            File.WriteAllText(finalPath, jsonNotes);
        }
    }
}
