﻿using ConstructionApp.DataServices.Interface;

namespace ConstructionApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new AppShell();
    }
}
