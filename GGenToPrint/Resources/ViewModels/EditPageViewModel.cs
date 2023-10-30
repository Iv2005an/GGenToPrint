using CommunityToolkit.Mvvm.ComponentModel;
using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.ViewModels;

public partial class EditPageViewModel : ObservableObject
{
    [ObservableProperty]
    Letter currentLetter;

    [ObservableProperty]
    string character;

    partial void OnCurrentLetterChanged(Letter value)
    {
        Character = value.Character;
    }
}