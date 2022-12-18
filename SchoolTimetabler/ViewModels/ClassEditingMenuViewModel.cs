using System.Collections.ObjectModel;
using System.Reactive;
using Data.FakeDataBase;
using Data.Models;
using ReactiveUI;

namespace SchoolTimetabler.ViewModels;

public class ClassEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private int _dataGridSelectedIndex;
    private readonly FDataBaseClasses _storage;

    public ClassEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel,
        FDataBaseClasses storage)
    {
        _storage = storage;
        Classes = new ObservableCollection<SchoolClass>(_storage.SchoolClasses);
        AddNewClass = ReactiveCommand.Create(() =>
        {
            var schoolClass = new SchoolClass("Новое число", "Новая буква");
            _storage.AddClass(schoolClass);
            Classes.Add(schoolClass);
        });

        DeleteClass = ReactiveCommand.Create(() =>
        {
            _storage.DeleteClass(_dataGridSelectedIndex);
            Classes.Remove(Classes[_dataGridSelectedIndex]);
        });
    }

    public ObservableCollection<SchoolClass> Classes { get; set; }
    public ReactiveCommand<Unit, Unit> AddNewClass { get; }
    public ReactiveCommand<Unit, Unit> DeleteClass { get; }

    public int DataGridSelectedIndex
    {
        set => this.RaiseAndSetIfChanged(ref _dataGridSelectedIndex, value);
        get => _dataGridSelectedIndex;
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}