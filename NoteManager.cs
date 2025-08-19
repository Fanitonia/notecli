using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notecli
{
    public static class NoteManager
    {
        public static void PrintHelp()
        {
            Console.WriteLine("\nUsage:\n");
            Console.WriteLine("notecli [create | c] [YOUR NOTE]");
            Console.WriteLine("notecli [delete | d] [NOTE ID]");
            Console.WriteLine("notecli [update | u] [NOTE ID]");
            Console.WriteLine("notecli [finish | f] [NOTE ID]");
            Console.WriteLine("notecli [list | l]");
        }

        public static List<Note> AddNote(this List<Note> notes, string text)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please proivide a new note." +
                    "   notecli create [note]" +
                    "   notecli c [note]");
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
                notes.RemoveAt(noteId-1);
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

        public static List<Note> FinishNote(this List<Note> notes, string idArgument)
        {
            int noteId = -1;
            bool isValidId = int.TryParse(idArgument, out noteId) && (noteId > 0 && noteId <= notes.Count);
            if (isValidId)
            {
                notes[noteId - 1].IsFinished = true;
                Console.WriteLine($"Note {noteId} deleted");
            }
            else
            {
                Console.WriteLine("Invalid note ID. Please provide a valid integer ID.");
            }

            return notes;
        }

        public static void ListNotes(this List<Note> notes)
        {
            Console.WriteLine("ID\tCREATED");
            notes.ForEach(note =>
            {
                int index = notes.IndexOf(note) + 1;
                Console.WriteLine($"{index}\t{note.CreatedAt.ToString("dd/MM/yyyy")}\t{note.Text}");
            });
        }
    }
}
