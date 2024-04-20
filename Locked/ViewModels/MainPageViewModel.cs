using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Locked.BottomSheets;
using Locked.Pages;
using Locked.Repositories;

namespace Locked.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly INoteRepository _noteRepository;

    private int _currentPageIndex;

    [ObservableProperty] 
    private bool isLocked;
    
    [ObservableProperty]
    private string lockedIndicatorIcon;

    
    [ObservableProperty]
    private string noteDescription;
    
    public partial class Note : ObservableObject
    {
        public string Id { get; set; }

        [ObservableProperty]
        private string value;

        [ObservableProperty]
        private DateTime createdAt;
        public string CreatedAtParsed => this.CreatedAt.ToString("f");
    }

    public ObservableCollection<Note> Notes { get; set; }

    public MainPageViewModel(INoteRepository noteRepository)
    {
        this._noteRepository = noteRepository;

        this.LockedIndicatorIcon = Utils.IconFont.CellphoneKey;
        this.IsLocked = true;

        this.Notes = this._noteRepository.All()
            .ToObservableCollection();

        this._currentPageIndex = -1;
    }

    [RelayCommand]
    private async Task UpdateLockedState()
    {
        if (this.IsLocked)
        {
            Func<string, Task<bool>> unlockCallback = async (string passPhrase) =>
            {
                if (string.IsNullOrEmpty(passPhrase))
                {
                    await Toast.Make("You need to enter a passphrase").Show();
                    return false;
                }

                string pass = await SecureStorage.Default.GetAsync("passphrase") ?? string.Empty;

                if (pass == passPhrase)
                {
                    this.IsLocked = !this.IsLocked;

                    return true;
                }

                await Toast.Make("Invalid passphrase").Show();

                return false;
            };

            Func<string, Task<bool>> registerPasswordCallback = async (string passPhrase) =>
            {
                if (string.IsNullOrEmpty(passPhrase))
                {
                    await Toast.Make("You need to enter a passphrase").Show();
                    return false;
                }

                await SecureStorage.Default.SetAsync("passphrase", passPhrase);

                this.IsLocked = !this.IsLocked;

                return true;
            };

            string pass = await SecureStorage.Default.GetAsync("passphrase") ?? string.Empty;

            if (string.IsNullOrEmpty(pass))
            {
                var bottomSheet = new UnlockNotesBottomSheet(registerPasswordCallback, confirmMessage: "Register and unlock")
                {
                    HasBackdrop = true,
                    CornerRadius = 18
                };

                if (App.Current?.Windows?.Any() ?? false)
                    await bottomSheet.ShowAsync(App.Current.Windows.First());
            }
            else
            {
                var bottomSheet = new UnlockNotesBottomSheet(unlockCallback)
                {
                    HasBackdrop = true,
                    CornerRadius = 18
                };

                if (App.Current?.Windows?.Any() ?? false)
                    await bottomSheet.ShowAsync(App.Current.Windows.First());
            }

            return;
        }

        this.IsLocked = !this.IsLocked;

        if (this.IsLocked)
        {
            await Toast.Make("Locked").Show();
            this.LockedIndicatorIcon = Utils.IconFont.CellphoneKey;
        }
        else
        {
            this.LockedIndicatorIcon = Utils.IconFont.Lock;
        }
    }

    [RelayCommand]
    private async Task SaveNoteChanges()
    {
        if (string.IsNullOrEmpty(this.NoteDescription))
        {
            await Toast.Make("You need to type something...").Show();
            return;
        }

        if (this._currentPageIndex == -1)
        {
            var newNote = new Note()
            {
                Value = this.NoteDescription,
                CreatedAt = DateTime.Now,
            };

            this._noteRepository.Insert(newNote);
            this.Notes.Add(newNote);
        }
        else
        {
            Note note = this.Notes[this._currentPageIndex];
            note.Value = this.NoteDescription;

            this._noteRepository.Update(note);
        }

        this.NoteDescription = string.Empty;
        this._currentPageIndex = -1;

        await Shell.Current.GoToAsync("../");
    }

    [RelayCommand]
    private async Task AddNewNote()
    {
        this.NoteDescription = string.Empty;
        await Shell.Current.GoToAsync(nameof(EditContentPage));
    }

    [RelayCommand]
    private async Task EditNote(Note note)
    {
        this._currentPageIndex = this.Notes.IndexOf(note);
        this.NoteDescription = note.Value;

        await Shell.Current.GoToAsync(nameof(EditContentPage));
    }

    [RelayCommand]
    private async Task RemoveNote(Note note)
    {
        Func<Task> confirmCallback = () =>
        {
            this.Notes.Remove(note);
            _noteRepository.Remove(note);

            return Task.CompletedTask;
        };

        var bottomSheet = new RemoveNoteBottomSheet(confirmCallback)
        {
            HasBackdrop = true,
            CornerRadius = 18
        };

        if (App.Current?.Windows?.Any() ?? false)
            await bottomSheet.ShowAsync(App.Current.Windows.First());
    }
}