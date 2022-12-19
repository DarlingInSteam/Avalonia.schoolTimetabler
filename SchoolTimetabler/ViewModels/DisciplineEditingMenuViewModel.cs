using System.Collections.ObjectModel;
using System.Reactive;
using Data.FakeDataBase;
using Data.Models;
using Data.Repositories;
using Domain.Entities;
using Domain.UseCases;
using ReactiveUI;

namespace SchoolTimetabler.ViewModels;

public class DisciplineEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private int _dataGridSelectedIndex;
    private readonly DisciplineInteractor _disciplineInteractor;

    public DisciplineEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel)
    {
        _disciplineInteractor = new DisciplineInteractor(DisciplineRepository.GetInstance());
        Disciplines = new ObservableCollection<Discipline>(_disciplineInteractor.GetDisciplines());
        AddNewDiscipline = ReactiveCommand.Create(() =>
        {
            var schoolDiscipline = new Discipline("Новая дисциплина");
            _disciplineInteractor.AddDiscipline(schoolDiscipline);
            Disciplines.Add(schoolDiscipline);
        });

        DeleteDiscipline = ReactiveCommand.Create(() =>
        {
            _disciplineInteractor.DelDiscipline(Disciplines[_dataGridSelectedIndex]);
            Disciplines.Remove(Disciplines[_dataGridSelectedIndex]);
        });
    }

    public ObservableCollection<Discipline> Disciplines { get; set; }
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