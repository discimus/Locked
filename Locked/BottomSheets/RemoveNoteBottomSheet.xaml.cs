using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using The49.Maui.BottomSheet;

namespace Locked.BottomSheets;

public partial class RemoveNoteBottomSheet : BottomSheet
{
    private readonly Func<Task> _confirmCallback;
    private readonly Func<Task>? _cancelCallback;

    public RemoveNoteBottomSheet(Func<Task> confirmCallback, Func<Task>? cancelCallback = null)
    {
        _confirmCallback = confirmCallback;
        _cancelCallback = cancelCallback;

        InitializeComponent();
    }
    
    private async void CloseModalClicked(object sender, EventArgs e)
    {
        if (_cancelCallback != null)
        {
            await _cancelCallback();
        }

        await this.DismissAsync();
    }
    
    private async void ConfirmRemoveNoteClicked(object sender, EventArgs e)
    {
        await _confirmCallback();

        await this.DismissAsync();
    }
}