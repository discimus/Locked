using Locked.Pages;

namespace Locked;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(EditContentPage), typeof(EditContentPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}