using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using Data.FakeDataBase;
using System.Reactive.Linq;
using SchoolClass = Avalonia.schoolTimetabler.Models.SchoolClass;

namespace Avalonia.schoolTimetabler.ViewModels;

public class ClassEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    public ObservableCollection<SchoolClass> Classes { get; }
    public ReactiveCommand<Unit, Unit> AddNewClass { get; }
    private FDataBase _storage;
    
    public ClassEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel)
    {
        _storage = FDataBase.GetInstance();
        Classes = new ObservableCollection<SchoolClass>(_storage.SchoolClasses.Select(dbSchoolClass => new SchoolClass(dbSchoolClass)));
        AddNewClass = ReactiveCommand.Create(() =>
        {
            var schoolClass = new SchoolClass("Новое число", "Новая буква", "Новый классный кабинет");
            _storage.AddClass(schoolClass.MapToDbSchoolClass());
            Classes.Add(schoolClass);
        });
        
        
    }

    
    
    public void Save()
    {
        
    }
    
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}