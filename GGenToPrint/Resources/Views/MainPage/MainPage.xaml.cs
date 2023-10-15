using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.Services;
using GGenToPrint.Resources.ViewModels;
using System.Collections.ObjectModel;

namespace GGenToPrint.Resources.Views.MainPage;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}