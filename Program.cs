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
                Console.WriteLine("\nUsage:\n");
                Console.WriteLine("notecli [create | c] [YOUR NOTE]");
                Console.WriteLine("notecli [delete | d] [NOTE ID]");
                Console.WriteLine("notecli [update | u] [NOTE ID]");
                Console.WriteLine("notecli [finish | f] [NOTE ID]");
                Console.WriteLine("notecli [list | l]");
             
            } 
            else if (args[0] == "create" || args[0] == "c")
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("\nPlease provide a note like this:\n" +
                        "   notecli [create | c] [YOUR NOTE]");
                } else
                {
                    notes = notes.AddNote(args[1]);
                    Console.WriteLine("Your note created!");
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
                    int noteId = -1;
                    bool isValidId = int.TryParse(args[1], out noteId) && (noteId > 0 && noteId <= notes.Count);
                    if (isValidId)
                    {
                        notes.RemoveAt(noteId);
                        Console.WriteLine($"Note {noteId} deleted");
                    } else
                    {
                        Console.WriteLine("Invalid note ID. Please provide a valid integer ID.");
                    }   
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
                    int noteId = -1;
                    bool isValidId = int.TryParse(args[1], out noteId) && (noteId > 0 && noteId <= notes.Count);
                    if (isValidId)
                    {
                        Console.WriteLine("Please enter the updated note:");
                        string? updatedNote = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(updatedNote))
                        {
                            notes[noteId - 1].Text = updatedNote;
                            Console.WriteLine($"Note {noteId} updated");
                        }
                        else
                        {
                            Console.WriteLine("Note text cannot be empty.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid note ID. Please provide a valid integer ID.");
                    }
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
                    int noteId = -1;
                    bool isValidId = int.TryParse(args[1], out noteId) && (noteId > 0 && noteId <= notes.Count);
                    if (isValidId)
                    {
                        notes[noteId - 1].IsFinished = true;
                        Console.WriteLine($"Note {noteId} deleted");
                    }
                    else
                    {
                        Console.WriteLine("Invalid note ID. Please provide a valid integer ID.");
                    }
                }
            }
            else if (args[0] == "list" || args[0] == "l")
            {
                if(notes.Count == 0)
                {
                    Console.WriteLine("No notes available.");
                    return;
                }

                Console.WriteLine("ID\tCREATED");
                notes.ForEach(note =>
                {
                    int index = notes.IndexOf(note) + 1;
                    Console.WriteLine($"{index}\t{note.CreatedAt.ToString("dd/MM/yyyy")}\t{note.Text}");
                });
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
