using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HD_Wallet_Recoverer_GUI.ViewModels;
using HD_Wallet_Recoverer_GUI.Views;

namespace HD_Wallet_Recoverer_GUI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var viewModel = new MainWindowViewModel();
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel
            };
            
            viewModel.ThisWindow = desktop.MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
