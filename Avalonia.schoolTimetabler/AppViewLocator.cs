using System;using Avalonia.schoolTimetabler.ViewModels;using Avalonia.schoolTimetabler.Views;using ReactiveUI;namespace Avalonia.schoolTimetabler{    public class AppViewLocator : IViewLocator    {        public IViewFor ResolveView<T>(T viewModel, string contract = null) => viewModel switch        {            CreateSchoolProfileViewModel context => new CreateSchoolProfile { DataContext = context },        };    }}    