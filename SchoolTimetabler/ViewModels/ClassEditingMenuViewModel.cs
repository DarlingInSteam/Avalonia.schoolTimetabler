using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using Data.FakeDataBase;
using System.Reactive.Linq;

namespace SchoolTimetabler.ViewModels;

public class ClassEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    public ObservableCollection<Data.Models.SchoolClass> Classes { get; }
    public ReactiveCommand<Unit, Unit> AddNewClass { get; }
    private FDataBaseClasses _storage;
    
    public ClassEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel, FDataBaseClasses storage)
    {
        _storage = storage;
        Classes = new ObservableCollection<Data.Models.SchoolClass>(_storage.SchoolClasses);
        AddNewClass = ReactiveCommand.Create(() =>
        {
            var schoolClass = new Data.Models.SchoolClass("Новое число", "Новая буква", "Новый классный кабинет");
            _storage.AddClass(schoolClass); 
            Classes.Add(schoolClass);
            
        });
    }
    
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}