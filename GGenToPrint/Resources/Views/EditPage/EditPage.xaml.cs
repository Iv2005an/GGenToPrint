using GGenToPrint.Resources.Drawables.SymbolSheet;
using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.ViewModels;

namespace GGenToPrint.Resources.Views.EditPage;

[QueryProperty(nameof(CurrentSymbol), "Symbol")]
public partial class EditPage : ContentPage
{
    readonly EditPageViewModel ViewModel;
    public Symbol CurrentSymbol
    {
        set
        {
            ViewModel.CurrentSymbol = value;
        }
    }
    public EditPage(EditPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        ViewModel = viewModel;
    }

    void SetSheetDrawableArgs(object sender)
    {
        SymbolSheetDrawable symbolSheet = (SymbolSheetDrawable)((SymbolSheetView)sender).Drawable;
        ViewModel.CellSize = symbolSheet.CellSize;
        ViewModel.Left = symbolSheet.Left;
        ViewModel.Top = symbolSheet.Top;
    }
    void StartInteraction(object sender, TouchEventArgs args)
    {
        SetSheetDrawableArgs(sender);
        ViewModel.StartGCodeChangingCommand.Execute(args.Touches[0]);
    }
    void DragInteraction(object sender, TouchEventArgs args)
    {
        SetSheetDrawableArgs(sender);
        ViewModel.GCodeChangingCommand.Execute(args.Touches[0]);
    }

    async void Cancel(object sender, EventArgs args) =>
        await Shell.Current.GoToAsync("///fontPage");

    async void SaveCommands(object sender, EventArgs args)
    {
        await ViewModel.SaveCommand.ExecuteAsync(null);
        await Shell.Current.GoToAsync("///fontPage");
    }
}