using System.Collections.ObjectModel;
using System.Reactive;
using Data.FakeDataBase;
using DynamicData;
using ReactiveUI;

namespace SchoolTimetabler.ViewModels;

public class CreateTimetableViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private FDataBaseTimetable _storageTimetable;
    private FDataBaseCabinets _storageCabinets;
    private FDataBaseTeachers _storageTeachers;
    private FDataBaseClasses _storageClasses;
    
    public ObservableCollection<string> DisciplinesTeacherMon { get; }
    public ObservableCollection<string> DisciplinesTeacherTues { get; }
    public ObservableCollection<string> DisciplinesTeacherWed { get; }
    public ObservableCollection<string> DisciplinesTeacherThurs { get; }
    public ObservableCollection<string> DisciplinesTeacherFri { get; }
    public ObservableCollection<string> DisciplinesTeacherSat { get; }
    
    public ObservableCollection<Data.Models.SchoolTeachers> Teachers { get; }
    public ObservableCollection<string> TeachersName { get; set; }
    public ObservableCollection<Data.Models.SchoolCabinet> Cabinets { get; }
    public ObservableCollection<string> CabinetsNumbers { get; set; }
    public ObservableCollection<Data.Models.SchoolClass> Classes { get; }
    public ObservableCollection<string> ClassesNumber { get; set; }

    public ReactiveCommand<Unit, Unit> AddOneTimetable { get; }
    public CreateTimetableViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        _storageTimetable = FDataBaseTimetable.GetInstance();
        _storageTeachers = FDataBaseTeachers.GetInstance();
        _storageCabinets = FDataBaseCabinets.GetInstance();
        _storageClasses = FDataBaseClasses.GetInstance();
        
        DisciplinesTeacherMon = new ObservableCollection<string>();
        DisciplinesTeacherSat = new ObservableCollection<string>();
        DisciplinesTeacherFri = new ObservableCollection<string>();
        DisciplinesTeacherThurs = new ObservableCollection<string>();
        DisciplinesTeacherTues = new ObservableCollection<string>();
        DisciplinesTeacherWed = new ObservableCollection<string>();
        
        TeachersName = new ObservableCollection<string>();
        Teachers = new ObservableCollection<Data.Models.SchoolTeachers>(_storageTeachers.SchoolTeachers);
        CabinetsNumbers = new ObservableCollection<string>();
        Cabinets = new ObservableCollection<Data.Models.SchoolCabinet>(_storageCabinets.SchoolCabinets);
        ClassesNumber = new ObservableCollection<string>();
        Classes = new ObservableCollection<Data.Models.SchoolClass>(_storageClasses.SchoolClasses);
        
        foreach (var t in Teachers[0].TeacherDisciplines)
        {
            DisciplinesTeacherMon.Add(t);
            DisciplinesTeacherFri.Add(t);
            DisciplinesTeacherSat.Add(t);
            DisciplinesTeacherThurs.Add(t);
            DisciplinesTeacherTues.Add(t);
            DisciplinesTeacherWed.Add(t);
        }
        
        foreach (var t in Classes)
        {
            ClassesNumber.Add(t.Number + t.Symbol);
        }
        foreach (var t in Teachers)
        {
            TeachersName.Add(t.TeacherFullName);
        }
        foreach (var t in Cabinets)
        {
            CabinetsNumbers.Add(t.CabinetNumber);
        }
    }

    public int SelectedIndexTeacherMon
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexTeacherMon, value);
        get => _selectedIndexTeacherMon;
    }
    
    public int SelectedIndexDisciplineMon
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexDisciplineMon, value);
        get => _selectedIndexDisciplineMon;
    }
    
    public int SelectedIndexCabinetMon
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexCabinetMon, value);
        get => _selectedIndexCabinetMon;
    }
    
    public int SelectedIndexTeacherTues
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexTeacherTues, value);
        get => _selectedIndexTeacherTues;
    }
    
    public int SelectedIndexDisciplineTues
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexDisciplineTues, value);
        get => _selectedIndexDisciplineTues;
    }
    
    public int SelectedIndexCabinetTues
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexCabinetTues, value);
        get => _selectedIndexCabinetTues;
    }
    
    public int SelectedIndexTeacherWed
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexTeacherWed, value);
        get => _selectedIndexTeacherWed;
    }
    
    public int SelectedIndexDisciplineWed
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexDisciplineWed, value);
        get => _selectedIndexDisciplineWed;
    }
    
    public int SelectedIndexCabinetWed
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexCabinetWed, value);
        get => _selectedIndexCabinetWed;
    }
    
    public int SelectedIndexTeacherThurs
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexTeacherThurs, value);
        get => _selectedIndexTeacherThurs;
    }
    
    public int SelectedIndexDisciplineThurs
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexDisciplineThurs, value);
        get => _selectedIndexDisciplineThurs;
    }
    
    public int SelectedIndexCabinetThurs
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexCabinetThurs, value);
        get => _selectedIndexCabinetThurs;
    }
    
    public int SelectedIndexTeacherFri
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexTeacherFri, value);
        get => _selectedIndexTeacherFri;
    }
    
    public int SelectedIndexDisciplineFri
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexDisciplineFri, value);
        get => _selectedIndexDisciplineFri;
    }
    
    public int SelectedIndexCabinetFri
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexCabinetFri, value);
        get => _selectedIndexCabinetFri;
    }
    
    public int SelectedIndexTeacherSat
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexTeacherSat, value);
        get => _selectedIndexTeacherSat;
    }
    
    public int SelectedIndexDisciplineSat
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexDisciplineSat, value);
        get => _selectedIndexDisciplineSat;
    }
    
    public int SelectedIndexCabinetSat
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexCabinetSat, value);
        get => _selectedIndexCabinetSat;
    }
    
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
    
    private int _selectedIndexTeacherMon;
    private int _selectedIndexDisciplineMon;
    private int _selectedIndexCabinetMon;
    private int _selectedIndexTeacherTues;
    private int _selectedIndexDisciplineTues;
    private int _selectedIndexCabinetTues;
    private int _selectedIndexTeacherWed;
    private int _selectedIndexDisciplineWed;
    private int _selectedIndexCabinetWed;
    private int _selectedIndexTeacherThurs;
    private int _selectedIndexDisciplineThurs;
    private int _selectedIndexCabinetThurs;
    private int _selectedIndexTeacherFri;
    private int _selectedIndexDisciplineFri;
    private int _selectedIndexCabinetFri;
    private int _selectedIndexTeacherSat;
    private int _selectedIndexDisciplineSat;
    private int _selectedIndexCabinetSat;
}