using Avalgame.Views;
using Avalonia;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.ViewModels
{
    public partial class HomePageViewModel : ViewModelBase
    {
        [RelayCommand]
        private void Start() => MainWindowViewModel.Instance.ChangePage(Page.Game);
        [RelayCommand]
        private void Exit() => MainWindow.Instance.Close();
    }
}
