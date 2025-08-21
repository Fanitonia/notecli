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
            Console.WriteLine("note add | a [YOUR NOTE]");
            Console.WriteLine("note delete | d [NOTE ID]");
            Console.WriteLine("note update | u [NOTE ID]");
            Console.WriteLine("note finish | f [NOTE ID]");
            Console.WriteLine("note unfinish | uf [NOTE ID]");
            Console.WriteLine("note list | l [OPTIONS]");
            Console.WriteLine("\nOptions: --all | -a (Show all notes including finished)");
        }

        public static List<Note> AddNote(this List<Note> notes, string text)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please proivide a new note." +
                    "   note add | a [note]");
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

        public static List<Note> FinishNote(this List<Note> notes, string idArgument)
        {
            int noteId = -1;
            bool isValidId = int.TryParse(idArgument, out noteId) && (noteId > 0 && noteId <= notes.Count);
            if (isValidId)
            {
                if(notes[noteId - 1].IsFinished)
                {
                    Console.WriteLine($"Note {noteId} is already finished.");
                }
                else
                {
                    notes[noteId - 1].IsFinished = true;
                    notes[noteId - 1].UpdatedAt = DateTime.Now;
                    Console.WriteLine($"Note {noteId} finished");
                }
                    
            }
            else
            {
                Console.WriteLine("Invalid note ID. Please provide a valid integer ID.");
            }

            return notes;
        }

        public static List<Note> UnfinishNote(this List<Note> notes, string idArgument)
        {
            int noteId = -1;
            bool isValidId = int.TryParse(idArgument, out noteId) && (noteId > 0 && noteId <= notes.Count);
            if (isValidId)
            {
                if (notes[noteId - 1].IsFinished)
                {
                    notes[noteId - 1].IsFinished = false;
                    notes[noteId - 1].UpdatedAt = DateTime.Now;
                    Console.WriteLine($"Note {noteId} unfinished");
                    
                }
                else
                {
                    Console.WriteLine($"Note {noteId} is already unfinished.");
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
                if(showAll)
                    Console.WriteLine("{0,-5}{1,-13}{2,-8}", "ID", "CREATED", "DONE");
                else
                    Console.WriteLine("{0,-5}{1,-13}", "ID", "CREATED");

                notes.ForEach(note =>
                {
                    if (note.IsFinished && !showAll)
                        return;

                    int index = notes.IndexOf(note) + 1;
                    string status = note.IsFinished ? " X" : "";

                    if (showAll)
                        Console.WriteLine("{0,-5}{1,-13}{2,-8}{3,-10}", index, note.CreatedAt.ToString("dd/MM/yyyy"), status, note.Text);
                    else
                        Console.WriteLine("{0,-5}{1,-13}{2,-10}", index, note.CreatedAt.ToString("dd/MM/yyyy"), note.Text);
                });
            }
                
        }
    }
}
