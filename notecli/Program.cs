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
                Console.WriteLine("No arguments provided. Type note help for usage.");
                return;
            }

            if (args[0] == "help")
            {
                NoteManager.PrintHelp();

            } 
            else if (args[0] == "add")
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("\nPlease provide a note like this:\n" +
                        "   note add [YOUR NOTE]");
                }
                else
                {
                    notes = notes.AddNote(args[1]);
                }
            } 
            else if (args[0] == "delete") 
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("\nPlease provide a note id like this:\n" +
                        "   note delete [NOTE ID]");
                }
                else
                {
                    notes = notes.DeleteNote(args[1]);
                }
            }
            else if (args[0] == "update")
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("\nPlease provide a note id like this:\n" +
                        "   note update [NOTE ID]");
                }
                else
                {
                    notes.UpdateNote(args[1]);
                }
            }
            else if (args[0] == "finish")
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("\nPlease provide a note id like this:\n" +
                        "   note finish [NOTE ID]");
                }
                else
                {
                    notes.FinishNote(args[1]);
                }
            }
            else if (args[0] == "unfinish")
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("\nPlease provide a note id like this:\n" +
                        "   note unfinish [NOTE ID]");
                }
                else
                {
                    notes.UnfinishNote(args[1]);
                }
            }
            else if (args[0] == "list")
            {
                bool showAll = args.Length == 2 && (args[1] == "--all" || args[1] == "-a");
                NoteManager.ListNotes(notes, showAll);
            }
            else if (args[0] == "clear")
            {
                notes.ClearNotes();
            }
            else if (args[0] == "--version" || args[0] == "-v")
            {
                NoteManager.ShowVersion();
            }
            else
            {
                Console.WriteLine("Unknown command. Type note help for usage.");
            }


            // SAVE NOTES
            jsonNotes = JsonSerializer.Serialize(notes);
            File.WriteAllText(finalPath, jsonNotes);
        }
    }
}
