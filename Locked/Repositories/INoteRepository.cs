using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locked.Repositories
{
    public interface INoteRepository
    {
        public List<ViewModels.MainPageViewModel.Note> All();
        public void Insert(ViewModels.MainPageViewModel.Note note);

        public void Update(ViewModels.MainPageViewModel.Note note);

        public void Remove(ViewModels.MainPageViewModel.Note note);
    }
}
