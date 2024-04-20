using Locked.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Locked.Repositories
{
    internal class JsonNoteRepository : INoteRepository
    {
        private readonly List<MainPageViewModel.Note> _notes;

        public JsonNoteRepository()
        {
            string value = SecureStorage.Default.GetAsync("notes")
                .ConfigureAwait(true)
                .GetAwaiter().GetResult() ?? "[]";

            _notes = JsonSerializer.Deserialize<List<MainPageViewModel.Note>>(value) ?? new List<MainPageViewModel.Note>();
        }

        public List<MainPageViewModel.Note> All()
        {
            return _notes;
        }

        public void Insert(MainPageViewModel.Note note)
        {
            _notes.Add(note);

            string content = JsonSerializer.Serialize(_notes);
            SecureStorage.Default.SetAsync("notes", content);
        }

        public void Remove(MainPageViewModel.Note note)
        {
            _notes.Remove(note);

            string content = JsonSerializer.Serialize(_notes);
            SecureStorage.Default.SetAsync("notes", content);
        }

        public void Update(MainPageViewModel.Note note)
        {
            int index = _notes.IndexOf(note);

            MainPageViewModel.Note note1 = _notes[index];

            note1.Value = note.Value;

            string content = JsonSerializer.Serialize(_notes);
            SecureStorage.Default.SetAsync("notes", content);
        }
    }
}
