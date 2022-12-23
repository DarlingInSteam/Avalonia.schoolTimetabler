using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using Data.Repositories;
using Domain.Entities;
using Domain.UseCases;
using ReactiveUI;

namespace SchoolTimetabler.ViewModels;

public class DisciplineEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private int _dataGridSelectedIndex;
    private string _disciplineName;

    public DisciplineEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel)
    {
        var disciplineInteractor = new DisciplineInteractor(DisciplineRepository.GetInstance());
        Disciplines = new ObservableCollection<Discipline>(disciplineInteractor.GetDisciplines());
        AddNewDiscipline = ReactiveCommand.Create(() =>
        {
            disciplineInteractor.AddDiscipline(DisciplineName);
            Disciplines.Clear();

            foreach (var t in disciplineInteractor.GetDisciplines())
            {
                Disciplines.Add(t);
            }

            DisciplineName = "";
        });
        DeleteDiscipline = ReactiveCommand.Create(() =>
        {
            disciplineInteractor.DelDiscipline(Disciplines[_dataGridSelectedIndex]);
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

    public string DisciplineName
    {
        set => this.RaiseAndSetIfChanged(ref _disciplineName, value);
        get => _disciplineName;
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}