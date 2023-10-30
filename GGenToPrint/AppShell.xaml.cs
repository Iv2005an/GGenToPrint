﻿using GGenToPrint.Resources.Views.EditPage;

namespace GGenToPrint;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("editCharacter", typeof(EditPage));
    }
}