using Locked.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locked.Repositories
{
    public class InMemoryRepository : INoteRepository
    {
        private readonly List<MainPageViewModel.Note> _notes;

        public InMemoryRepository()
        {
            _notes = new List<MainPageViewModel.Note>()
            {
                new MainPageViewModel.Note() { Value = "alskjdfl kasjldfkj laskdjf alsk jdflkajs dlkfj aslkdjfalskdjflasjdlfkfjsdlkfjasldk jfalskdjf laskdjf laksjdfl kajsdlfkjasldkfjlkas jdklfjalksjd fas", CreatedAt = DateTime.Now },
                new MainPageViewModel.Note() { Value = "fkasjld fkjasldj fs", CreatedAt = DateTime.Now },
                new MainPageViewModel.Note() { Value = "kjf;laskjdf ;lasidfj lasidf ", CreatedAt = DateTime.Now },
                new MainPageViewModel.Note() { Value = "jklsadj; flkajsdl fasi", CreatedAt = DateTime.Now },
                new MainPageViewModel.Note() { Value = "Don't forget to RSVP to Sarah's birthday party by Friday.", CreatedAt = DateTime.Now },
            };
        }

        public List<MainPageViewModel.Note> All()
        {
            return _notes;
        }

        public void Insert(MainPageViewModel.Note note)
        {
            _notes.Add(note);
        }

        public void Remove(MainPageViewModel.Note note)
        {
            _notes.Remove(note);
        }

        public void Update(MainPageViewModel.Note note)
        {
            int index = _notes.IndexOf(note);

            MainPageViewModel.Note note1 = _notes[index];

            note1.Value = note.Value;
        }
    }
}
