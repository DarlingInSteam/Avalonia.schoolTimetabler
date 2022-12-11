using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using Avalonia.Controls;
using Data.FakeDataBase;
using ReactiveUI;

namespace SchoolTimetabler.ViewModels;

public class DisciplineEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    public ObservableCollection<Data.Models.SchoolDiscipline> Disciplines { get; set; }
    public ReactiveCommand<Unit, Unit> AddNewDiscipline { get; }
    public ReactiveCommand<Unit, Unit> DeleteDiscipline { get; }
    private FDataBaseDisciplines _storage;
    private FDataBaseDisciplines _storageDisciplines;
    private int _dataGridSelectedIndex;

    public DisciplineEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel,
        FDataBaseDisciplines storage)
    {
        _storage = storage;
        Disciplines = new ObservableCollection<Data.Models.SchoolDiscipline>(_storage.SchoolDisciplines);
        AddNewDiscipline = ReactiveCommand.Create(() =>
        {
            var schoolDiscipline = new Data.Models.SchoolDiscipline("Новая дисциплина");
            _storage.AddClass(schoolDiscipline);
            Disciplines.Add(schoolDiscipline);
        });

        DeleteDiscipline = ReactiveCommand.Create(() =>
        {
            _storage.DeleteDiscipline(_dataGridSelectedIndex);
            Disciplines.Remove(Disciplines[_dataGridSelectedIndex]);
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