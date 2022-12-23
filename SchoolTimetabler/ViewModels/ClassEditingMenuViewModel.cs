using System.Collections.ObjectModel;
using System.Reactive;
using Data.Repository;
using Domain.Entities;
using Domain.UseCases;
using ReactiveUI;

namespace SchoolTimetabler.ViewModels;

public class ClassEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private int _dataGridSelectedIndex;
    private string _classNumber;
    private string _classSymbol;

    public ClassEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel)
    {
        var classInteractor = new ClassInteractor(ClassesRepository.GetInstance());
        Classes = new ObservableCollection<Class>(classInteractor.GetClasses());
        AddNewClass = ReactiveCommand.Create(() =>
        {
            classInteractor.AddClass(ClassNumber, ClassSymbol);
            Classes.Clear();

            foreach (var t in classInteractor.GetClasses())
            {
                Classes.Add(t);
            }
        });

        DeleteClass = ReactiveCommand.Create(() =>
        {
            classInteractor.DelClass(Classes[_dataGridSelectedIndex]);
            Classes.Remove(Classes[_dataGridSelectedIndex]);
        });
    }

    public ObservableCollection<Class> Classes { get; set; }
    public ReactiveCommand<Unit, Unit> AddNewClass { get; }
    public ReactiveCommand<Unit, Unit> DeleteClass { get; }

    public int DataGridSelectedIndex
    {
        set => this.RaiseAndSetIfChanged(ref _dataGridSelectedIndex, value);
        get => _dataGridSelectedIndex;
    }

    public string ClassNumber
    {
        set => this.RaiseAndSetIfChanged(ref _classNumber, value);
        get => _classNumber;
    }

    public string ClassSymbol
    {
        set => this.RaiseAndSetIfChanged(ref _classSymbol, value);
        get => _classSymbol;
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}