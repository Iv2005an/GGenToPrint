using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.ViewModels;

namespace GGenToPrint.Resources.Views.EditPage;

[QueryProperty(nameof(CurrerntLetter), "Letter")]
public partial class EditPage : ContentPage
{
    readonly EditPageViewModel ViewModel;
    public Letter CurrerntLetter
    {
        set
        {
            ViewModel.CurrentLetter = value;
        }
    }
    public EditPage(EditPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        ViewModel = viewModel;
    }
}