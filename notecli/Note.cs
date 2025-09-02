using System.Text.Json.Serialization;

namespace notecli
{
    public class Note
    {
        // fields
        private DateTime createdAt = DateTime.Now;
        private DateTime? updatedAt = null;
        private string text = string.Empty;
        private bool isDone = false;

        // properties
        public DateTime CreatedAt 
        {
            get => createdAt;
            set => createdAt = value;
        }
        public DateTime? UpdatedAt
        {
            get => updatedAt;
            set => updatedAt = value;
        }
        public string Text
        {
            get => text;
            set => text = value;
        }
        public bool IsDone
        {
            get => isDone;
            set => isDone = value;
        }

        // constructors
        public Note(string text)
        {
            this.text = text;
        }

        [JsonConstructor]
        public Note()
        {

        }
    }
}
