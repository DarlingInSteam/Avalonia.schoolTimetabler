using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.schoolTimetabler.Models;

namespace Avalonia.schoolTimetabler.ViewModels;

public class ClassEditingMenuViewModel 
{
    public ObservableCollection<SchoolClasses> Classes { get; }
    
    public ClassEditingMenuViewModel()
    {
        Classes = new ObservableCollection<SchoolClasses>(GenerateMockClassesTable());
    }
    
    private IEnumerable<SchoolClasses> GenerateMockClassesTable()
    {
        var defaultClasses = new List<SchoolClasses>()
        {
            new SchoolClasses()
            {
                Classroom = "1-1",
                Number = "3",
                Symbol = "В"
            },
                
            new SchoolClasses()
            {
                Classroom = "3-4",
                Number = "9",
                Symbol = "А"
            },
                
            new SchoolClasses()
            {
                Classroom = "2-6",
                Number = "5",
                Symbol = "Б"
            }
        };

        return defaultClasses;
    }
}