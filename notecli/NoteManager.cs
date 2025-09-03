using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace notecli
{
    public static class NoteManager
    {
        public static void PrintHelp()
        {
            Console.WriteLine("\nnote [notecli-options]");
            Console.WriteLine("      notecli-options:");
            Console.WriteLine("        --version | -v  (Check the current version of notecli)");

            Console.WriteLine("\nnote [command] <command-argument> [command-options]");
            Console.WriteLine("      Commands:");
            Console.WriteLine("        add <YOUR NOTE>");
            Console.WriteLine("        delete <NOTE ID>");
            Console.WriteLine("        update <NOTE ID>");
            Console.WriteLine("        done <NOTE ID>");
            Console.WriteLine("        undone <NOTE ID>");
            Console.WriteLine("        list [options]");
            Console.WriteLine("        clear [options]\n");
            Console.WriteLine("      command-options:");
            Console.WriteLine("        --all | -a   (List all notes including done)");
            Console.WriteLine("        --done | -d  (clear only done notes)");
        }

        public static List<Note> AddNote(this List<Note> notes, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please proivide a new note." +
                    "   note add [note]");
                return notes;
            }

            notes.Add(new Note(text));
            Console.WriteLine("Your note created!");
            return notes;
        }

        public static List<Note> DeleteNote(this List<Note> notes, string idArgument)
        {
            int noteId = -1;
            bool isValidId = int.TryParse(idArgument, out noteId) && (noteId > 0 && noteId <= notes.Count);
            if (isValidId)
            {
                notes.RemoveAt(noteId - 1);
                Console.WriteLine($"Note {noteId} deleted");
            }
            else
            {
                Console.WriteLine("Invalid note ID. Please provide a valid integer ID.");
            }

            return notes;
        }

        public static List<Note> UpdateNote(this List<Note> notes, string idArgument)
        {
            int noteId = -1;
            bool isValidId = int.TryParse(idArgument, out noteId) && (noteId > 0 && noteId <= notes.Count);
            if (isValidId)
            {
                Console.WriteLine("Please enter the updated note:");
                string? updatedNote = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(updatedNote))
                {
                    notes[noteId - 1].Text = updatedNote;
                    notes[noteId - 1].UpdatedAt = DateTime.Now;
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

            return notes;
        }

        public static List<Note> DoneNote(this List<Note> notes, string idArgument)
        {
            int noteId = -1;
            bool isValidId = int.TryParse(idArgument, out noteId) && (noteId > 0 && noteId <= notes.Count);
            if (isValidId)
            {
                if (notes[noteId - 1].IsDone)
                {
                    Console.WriteLine($"Note {noteId} is already done.");
                }
                else
                {
                    notes[noteId - 1].IsDone = true;
                    notes[noteId - 1].UpdatedAt = DateTime.Now;
                    Console.WriteLine($"Note {noteId} marked as done.");
                }
            }
            else
            {
                Console.WriteLine("Invalid note ID. Please provide a valid integer ID.");
            }

            return notes;
        }

        public static List<Note> UndoneNote(this List<Note> notes, string idArgument)
        {
            int noteId = -1;
            bool isValidId = int.TryParse(idArgument, out noteId) && (noteId > 0 && noteId <= notes.Count);
            if (isValidId)
            {
                if (notes[noteId - 1].IsDone)
                {
                    notes[noteId - 1].IsDone = false;
                    notes[noteId - 1].UpdatedAt = DateTime.Now;
                    Console.WriteLine($"Note {noteId} marked as undone");

                }
                else
                {
                    Console.WriteLine($"Note {noteId} is already undone.");
                }
            }
            else
            {
                Console.WriteLine("Invalid note ID. Please provide a valid integer ID.");
            }

            return notes;
        }

        public static void ListNotes(this List<Note> notes, bool showAll)
        {
            if (notes.Count == 0)
            {
                Console.WriteLine("No notes available.");
            }
            else
            {
                if (showAll)
                    Console.WriteLine("{0,-5}{1,-13}{2,-8}", "ID", "CREATED", "DONE");
                else
                    Console.WriteLine("{0,-5}{1,-13}", "ID", "CREATED");

                notes.ForEach(note =>
                {
                    if (note.IsDone && !showAll)
                        return;

                    int index = notes.IndexOf(note) + 1;
                    string status = note.IsDone ? " X" : "";

                    if (showAll)
                        Console.WriteLine("{0,-5}{1,-13}{2,-8}{3,-10}", index, note.CreatedAt.ToString("dd/MM/yyyy"), status, note.Text);
                    else
                        Console.WriteLine("{0,-5}{1,-13}{2,-10}", index, note.CreatedAt.ToString("dd/MM/yyyy"), note.Text);
                });
            }
        }

        public static List<Note> ClearNotes(this List<Note> notes, bool? clearOnlyDoneNotes = false)
        {
            if (notes.Count == 0)
            {
                Console.WriteLine("No notes available.");
                return notes;
            }

            string noteType = clearOnlyDoneNotes == true ? "done notes" : "notes";

            Console.Write($"Are you sure you want to clear all your {noteType}. This action cannot be undone (Y/n): ");
            string? input = Console.ReadLine();

            if ( input == "Y")
            {
                if (clearOnlyDoneNotes == true)
                    notes.RemoveAll(note => note.IsDone);
                else
                    notes.Clear();

                Console.WriteLine($"Your {noteType} are deleted.");
            } else
            {
                Console.WriteLine("Your notes are safe.");
            }

            return notes;
        }

        public static void ShowVersion()
        {
            Version? version = Assembly.GetEntryAssembly()!.GetName().Version!;

            if (version is not null)
                Console.WriteLine("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            else
                Console.WriteLine("Version information not available.");
        }
    }
}
