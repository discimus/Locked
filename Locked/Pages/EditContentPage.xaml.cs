using Locked.ViewModels;

namespace Locked.Pages;

public partial class EditContentPage : ContentPage
{
	public EditContentPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}