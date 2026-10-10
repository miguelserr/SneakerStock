namespace SneakerStock;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("AddSneakerPage", typeof(Views.AddSneakerPage));
        Routing.RegisterRoute("RegisterPage", typeof(Views.RegisterPage));
    }
}