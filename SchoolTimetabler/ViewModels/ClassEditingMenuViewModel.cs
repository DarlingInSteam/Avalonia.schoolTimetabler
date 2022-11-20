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
    public ObservableCollection<Data.Models.SchoolClass> Classes { get; set; }
    public ReactiveCommand<Unit, Unit> AddNewClass { get; }
    public ReactiveCommand<Unit, Unit> DeleteClass { get; }
    private FDataBaseClasses _storage;
    private int _dataGridSelectedIndex;
    
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

        DeleteClass = ReactiveCommand.Create(() =>
        {
            _storage.DeleteClass(_dataGridSelectedIndex);
            Classes.Remove(Classes[_dataGridSelectedIndex]);
        });
    }
    
    public int DataGridSelectedIndex
    { 
        set => this.RaiseAndSetIfChanged(ref _dataGridSelectedIndex, value); 
        get => _dataGridSelectedIndex; 
    }
    
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}