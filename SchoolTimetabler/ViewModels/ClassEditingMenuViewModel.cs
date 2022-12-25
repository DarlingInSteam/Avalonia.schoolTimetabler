using System.Collections.ObjectModel;
using System.Reactive;
using Data.Repositories;
using Domain.Entities;
using Domain.UseCases;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SchoolTimetabler.ViewModels;

public class ClassEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    public ClassEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel)
    {
        var classInteractor = new ClassInteractor(ClassesRepository.GetInstance());
        Classes = new ObservableCollection<Class>(classInteractor.GetClasses());
        AddNewClass = ReactiveCommand.Create(() =>
        {
            classInteractor.AddClass(ClassNumber, ClassSymbol);
            Classes.Clear();

            foreach (var t in classInteractor.GetClasses()) Classes.Add(t);
        });

        DeleteClass = ReactiveCommand.Create(() =>
        {
            classInteractor.DelClass(Classes[DataGridSelectedIndex]);
            Classes.Remove(Classes[DataGridSelectedIndex]);
        });
    }

    [Reactive] public int DataGridSelectedIndex { get; set; }
    [Reactive] public string ClassNumber { get; set; }
    [Reactive] public string ClassSymbol { get; set; }

    public ObservableCollection<Class> Classes { get; set; }
    public ReactiveCommand<Unit, Unit> AddNewClass { get; }
    public ReactiveCommand<Unit, Unit> DeleteClass { get; }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}