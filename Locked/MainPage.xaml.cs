using CommunityToolkit.Maui.Alerts;
using Locked.BottomSheets;
using Locked.ViewModels;

namespace Locked;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}