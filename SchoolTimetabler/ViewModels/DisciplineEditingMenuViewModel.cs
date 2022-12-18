using System.Collections.ObjectModel;
using System.Reactive;
using Data.FakeDataBase;
using Data.Models;
using ReactiveUI;

namespace SchoolTimetabler.ViewModels;

public class DisciplineEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private int _dataGridSelectedIndex;
    private readonly FDataBaseDisciplines _storage;

    public DisciplineEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel,
        FDataBaseDisciplines storage)
    {
        _storage = FDataBaseDisciplines.GetInstance();
        Disciplines = new ObservableCollection<SchoolDiscipline>(_storage.SchoolDisciplines);
        AddNewDiscipline = ReactiveCommand.Create(() =>
        {
            var schoolDiscipline = new SchoolDiscipline("Новая дисциплина");
            _storage.AddClass(schoolDiscipline);
            Disciplines.Add(schoolDiscipline);
        });

        DeleteDiscipline = ReactiveCommand.Create(() =>
        {
            _storage.DeleteDiscipline(_dataGridSelectedIndex);
            Disciplines.Remove(Disciplines[_dataGridSelectedIndex]);
        });
    }

    public ObservableCollection<SchoolDiscipline> Disciplines { get; set; }
    public ReactiveCommand<Unit, Unit> AddNewDiscipline { get; }
    public ReactiveCommand<Unit, Unit> DeleteDiscipline { get; }

    public int DataGridSelectedIndex
    {
        set => this.RaiseAndSetIfChanged(ref _dataGridSelectedIndex, value);
        get => _dataGridSelectedIndex;
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}