using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using Data.Repositories;
using Domain.Entities;
using Domain.UseCases;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SchoolTimetabler.ViewModels;

public class DisciplineEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    [Reactive]
    public int DataGridSelectedIndex { get; set; }
    [Reactive]
    public string DisciplineName { get; set; }

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
            disciplineInteractor.DelDiscipline(Disciplines[DataGridSelectedIndex]);
            Disciplines.Remove(Disciplines[DataGridSelectedIndex]);
        });
    }

    public ObservableCollection<Discipline> Disciplines { get; set; }
    public ReactiveCommand<Unit, Unit> AddNewDiscipline { get; }
    public ReactiveCommand<Unit, Unit> DeleteDiscipline { get; }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}