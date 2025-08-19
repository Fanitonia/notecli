using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notecli
{
    public static class NoteManager
    {
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
            return notes;
        }

    }
}
