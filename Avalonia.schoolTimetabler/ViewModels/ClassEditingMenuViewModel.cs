using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using ReactiveUI;
using Avalonia.schoolTimetabler.Models;

namespace Avalonia.schoolTimetabler.ViewModels;

public class ClassEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
   
    public ObservableCollection<SchoolClasses> Classes { get; }
    public ReactiveCommand<Unit, Unit> AddNewClass { get; }

    public ClassEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel)
    {
        Classes = new ObservableCollection<SchoolClasses>(GenerateMockClassesTable());

        AddNewClass = ReactiveCommand.Create(() =>
        {
            Classes.Add(
                new SchoolClasses()
                {
                    Number = "New First Name",
                    Symbol = "New Last Name",
                    Classroom = "New Classroom"
                });
        });
    }
    
    private IEnumerable<SchoolClasses> GenerateMockClassesTable()
    {
        var defaultClasses = new List<SchoolClasses>();
        return defaultClasses;
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}