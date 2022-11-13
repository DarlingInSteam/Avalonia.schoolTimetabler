using System.Collections.ObjectModel;
using System.Reactive;
using Data.FakeDataBase;
using ReactiveUI;

namespace SchoolTimetabler.ViewModels;

public class DisciplineEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    public ObservableCollection<Data.Models.SchoolDiscipline> Disciplines { get; }
    public ReactiveCommand<Unit, Unit> AddNewDiscipline { get; }
    private FDataBaseDisciplines _storage;
    
    public DisciplineEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel)
    {
        _storage = FDataBaseDisciplines.GetInstance();
        Disciplines = new ObservableCollection<Data.Models.SchoolDiscipline>(_storage.SchoolDisciplines);
        AddNewDiscipline = ReactiveCommand.Create(() =>
        {
            var schoolDiscipline = new Data.Models.SchoolDiscipline( "Новая дисциплина");
            _storage.AddClass(schoolDiscipline); 
            Disciplines.Add(schoolDiscipline);
            
        });
    }
    
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}
