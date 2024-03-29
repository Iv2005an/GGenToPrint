using GGenToPrint.Resources.Drawables.Editor;
using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.ViewModels;

namespace GGenToPrint.Resources.Views.EditPage;

[QueryProperty(nameof(CurrentLetter), "Letter")]
public partial class EditPage : ContentPage
{
    readonly EditPageViewModel ViewModel;
    public Letter CurrentLetter
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

    void StartInteraction(object sender, TouchEventArgs args)
    {
        ViewModel.CellSize = ((EditorDrawable)((EditorView)sender).Drawable).CellSize;
        ViewModel.Top = ((EditorDrawable)((EditorView)sender).Drawable).Top;
        ViewModel.StartGCodeChangingCommand.Execute(args.Touches[0]);
    }

    void DragInteraction(object sender, TouchEventArgs args)
    {
        ViewModel.CellSize = ((EditorDrawable)((EditorView)sender).Drawable).CellSize;
        ViewModel.Top = ((EditorDrawable)((EditorView)sender).Drawable).Top;
        ViewModel.GCodeChangingCommand.Execute(args.Touches[0]);
    }

    async void SaveCommands(object sender, EventArgs args)
    {
        await DisplayAlert("Сохранение", "Вид символа сохранён", "ОК");
        await ViewModel.SaveCommand.ExecuteAsync(null);
    }
}