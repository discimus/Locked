using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Locked.ViewModels;
using The49.Maui.BottomSheet;

namespace Locked.BottomSheets;

public partial class UnlockNotesBottomSheet : BottomSheet
{
    private readonly Func<string, Task<bool>> _confirmCallback;
    private readonly Func<Task>? _cancelCallback;

    public partial class UnlockNotesBottomSheetViewModel : ObservableObject
    {
        [ObservableProperty]
        private string pass;

        [ObservableProperty]
        private string confirmMessage;

        public UnlockNotesBottomSheetViewModel(string confirmMessage)
        {
            this.ConfirmMessage = confirmMessage;
        }
    }

    public UnlockNotesBottomSheet(
        Func<string, Task<bool>> confirmCallback, 
        string confirmMessage = "Unlock",
        Func<Task>? cancelCallback = null)
    {
        _confirmCallback = confirmCallback;
        _cancelCallback = cancelCallback;

        InitializeComponent();

        this.BindingContext = new UnlockNotesBottomSheetViewModel(confirmMessage);
    }
    
    private async void CloseModalClicked(object sender, EventArgs e)
    {
        if (_cancelCallback != null)
        {
            await _cancelCallback();
        }

        await this.DismissAsync();
    }
    
    private async void ValidatePassphraseClicked(object sender, EventArgs e)
    {
        var viewModel = this.BindingContext as UnlockNotesBottomSheetViewModel;
        bool shouldCloseModal = await _confirmCallback(viewModel?.Pass ?? string.Empty);

        if (shouldCloseModal)
        {
            await this.DismissAsync();
        }
    }
}