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
    private readonly ClassInteractor _classInteractor;

    public ClassEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel)
    {
        _classInteractor = new ClassInteractor(ClassesRepository.GetInstance());
        Classes = new ObservableCollection<Class>(_classInteractor.GetClasses());
        AddNewClass = ReactiveCommand.Create(() =>
        {
            var schoolClass = new Class("Новое число", "Новая буква");
            _classInteractor.AddClass(schoolClass);
            Classes.Add(schoolClass);
        });

        DeleteClass = ReactiveCommand.Create(() =>
        {
            _classInteractor.DelClass(Classes[_dataGridSelectedIndex]);
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

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}