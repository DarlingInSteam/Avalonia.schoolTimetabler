using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Data.Repositories;
using Domain.Entities;
using Domain.UseCases;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SchoolTimetabler.ViewModels;

public class CreateTimetableViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private string _dayOfTheWeek = "Понедельник";
    private bool _isEnableBack;
    private bool _isEnableNext = true;
    [Reactive] public int SelectedIndexCabinetFri { get; set; }
    [Reactive] public int SelectedIndexCabinetMon { get; set; }
    [Reactive] public int SelectedIndexCabinetSat { get; set; }
    [Reactive] public int SelectedIndexCabinetThurs { get; set; }
    [Reactive] public int SelectedIndexCabinetTues { get; set; }
    [Reactive] public int SelectedIndexCabinetWed { get; set; }
    [Reactive] public int SelectedIndexClass { get; set; }
    [Reactive] public int SelectedIndexDisciplineFri { get; set; }
    [Reactive] public int SelectedIndexDisciplineMon { get; set; }
    [Reactive] public int SelectedIndexDisciplineSat { get; set; }
    [Reactive] public int SelectedIndexDisciplineThurs { get; set; }
    [Reactive] public int SelectedIndexDisciplineTues { get; set; }
    [Reactive] public int SelectedIndexDisciplineWed { get; set; }

    private int _selectedIndexTeacherFri;
    private int _selectedIndexTeacherMon;
    private int _selectedIndexTeacherSat;
    private int _selectedIndexTeacherThurs;
    private int _selectedIndexTeacherTues;
    private int _selectedIndexTeacherWed;
    private int _countDays;

    public CreateTimetableViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        var timetableInteractor = new TimetableInteractor(TimetablesRepository.GetInstance());
        var teacherInteractor = new TeacherInteractor(TeacherRepository.GetInstance());
        var cabinetInteractor = new CabinetInteractor(CabinetsRepository.GetInstance());
        var classInteractor = new ClassInteractor(ClassesRepository.GetInstance());

        DisciplinesTeacherMon = new ObservableCollection<string>();
        DisciplinesTeacherSat = new ObservableCollection<string>();
        DisciplinesTeacherFri = new ObservableCollection<string>();
        DisciplinesTeacherThurs = new ObservableCollection<string>();
        DisciplinesTeacherTues = new ObservableCollection<string>();
        DisciplinesTeacherWed = new ObservableCollection<string>();

        TeachersMon = new ObservableCollection<string>();
        TeachersThurs = new ObservableCollection<string>();
        TeachersTues = new ObservableCollection<string>();
        TeachersFri = new ObservableCollection<string>();
        TeachersWed = new ObservableCollection<string>();
        TeachersSat = new ObservableCollection<string>();

        CabinetsThurs = new ObservableCollection<string>();
        CabinetsTues = new ObservableCollection<string>();
        CabinetsMon = new ObservableCollection<string>();
        CabinetsWed = new ObservableCollection<string>();
        CabinetsFri = new ObservableCollection<string>();
        CabinetsSat = new ObservableCollection<string>();
        
        TeachersName = new ObservableCollection<string>();
        Teachers = new ObservableCollection<Teacher>(teacherInteractor.GetTeachers());
        CabinetsNumbers = new ObservableCollection<string>();
        Cabinets = new ObservableCollection<Cabinet>(cabinetInteractor.GetCabinets());
        ClassesNumber = new ObservableCollection<string>();
        Classes = new ObservableCollection<Class>(classInteractor.GetClasses());

        foreach (var t in Classes) ClassesNumber.Add(t.Number + t.Symbol);
        

        BackDay = ReactiveCommand.Create(() =>
        {
            _countDays -= 1;

            ChangeIndexes();
            ChangeDayOfTheWeek();

            return;
        });

        NextDay = ReactiveCommand.Create(() =>
        {
            _countDays += 1;

            ChangeIndexes();
            ChangeDayOfTheWeek();
        });

        SaveOneTimetable = ReactiveCommand.Create(() =>
        {
            var timetable = new Timetable();

            timetable.Day = DayOfTheWeek;
            if (TeachersMon.Count != 0)
            {
                timetable.TeacherOne = TeachersMon[_selectedIndexTeacherMon];
                timetable.DisciplineOne = DisciplinesTeacherMon[SelectedIndexDisciplineMon];
                timetable.CabinetOne = CabinetsMon[SelectedIndexCabinetMon];
                timetable.ClassOne = ClassesNumber[SelectedIndexClass];
            }

            if (TeachersTues.Count != 0)
            {
                timetable.TeacherTwo = TeachersTues[_selectedIndexTeacherTues];
                timetable.DisciplineTwo = DisciplinesTeacherTues[SelectedIndexDisciplineTues];
                timetable.CabinetTwo = CabinetsTues[SelectedIndexCabinetTues];
                timetable.ClassTwo = ClassesNumber[SelectedIndexClass];
            }

            if (TeachersWed.Count != 0)
            {
                timetable.TeacherThree = TeachersWed[_selectedIndexTeacherWed];
                timetable.DisciplineThree = DisciplinesTeacherWed[SelectedIndexDisciplineWed];
                timetable.CabinetThree = CabinetsWed[SelectedIndexCabinetWed];
                timetable.ClassThree = ClassesNumber[SelectedIndexClass];
            }

            if (TeachersThurs.Count != 0)
            {
                timetable.TeacherFour = TeachersThurs[_selectedIndexTeacherThurs];
                timetable.DisciplineFour = DisciplinesTeacherThurs[SelectedIndexDisciplineThurs];
                timetable.CabinetFour = CabinetsThurs[SelectedIndexCabinetThurs];
                timetable.ClassFour = ClassesNumber[SelectedIndexClass];
            }

            if (TeachersFri.Count != 0)
            {
                timetable.TeacherFive = TeachersFri[_selectedIndexTeacherFri];
                timetable.DisciplineFive = DisciplinesTeacherFri[SelectedIndexDisciplineFri];
                timetable.CabinetFive = CabinetsFri[SelectedIndexCabinetFri];
                timetable.ClassFive = ClassesNumber[SelectedIndexClass];
            }

            if (TeachersSat.Count != 0)
            {
                timetable.TeacherSix = TeachersSat[_selectedIndexTeacherSat];
                timetable.DisciplineSix = DisciplinesTeacherSat[SelectedIndexDisciplineSat];
                timetable.CabinetSix = CabinetsSat[SelectedIndexCabinetSat];
                timetable.ClassSix = ClassesNumber[SelectedIndexClass];
            }

            timetableInteractor.AddTimetable(timetable);
        });
    }

    public string DayOfTheWeek
    {
        set => this.RaiseAndSetIfChanged(ref _dayOfTheWeek, value);
        get => _dayOfTheWeek;
    }

    public bool IsEnableNext
    {
        set => this.RaiseAndSetIfChanged(ref _isEnableNext, value);
        get => _isEnableNext;
    }

    public bool IsEnableBack
    {
        set => this.RaiseAndSetIfChanged(ref _isEnableBack, value);
        get => _isEnableBack;
    }

    public ObservableCollection<string> DisciplinesTeacherMon { get; }
    public ObservableCollection<string> DisciplinesTeacherTues { get; }
    public ObservableCollection<string> DisciplinesTeacherWed { get; }
    public ObservableCollection<string> DisciplinesTeacherThurs { get; }
    public ObservableCollection<string> DisciplinesTeacherFri { get; }
    public ObservableCollection<string> DisciplinesTeacherSat { get; }

    public ObservableCollection<Teacher> Teachers { get; }
    public ObservableCollection<string> TeachersName { get; set; }
    public ObservableCollection<string> TeachersMon { get; set; }
    public ObservableCollection<string> TeachersTues { get; set; }
    public ObservableCollection<string> TeachersWed { get; set; }
    public ObservableCollection<string> TeachersThurs { get; set; }
    public ObservableCollection<string> TeachersFri { get; set; }
    public ObservableCollection<string> TeachersSat { get; set; }
 
    public ObservableCollection<Cabinet> Cabinets { get; }
    public ObservableCollection<string> CabinetsNumbers { get; set; }
    public ObservableCollection<string> CabinetsMon { get; set; }
    public ObservableCollection<string> CabinetsTues { get; set; }
    public ObservableCollection<string> CabinetsWed { get; set; }
    public ObservableCollection<string> CabinetsThurs { get; set; }
    public ObservableCollection<string> CabinetsFri { get; set; }
    public ObservableCollection<string> CabinetsSat { get; set; }
    public ObservableCollection<Class> Classes { get; }
    public ObservableCollection<string> ClassesNumber { get; set; }

    public ReactiveCommand<Unit, Unit> SaveOneTimetable { get; }
    public ReactiveCommand<Unit, Unit> NextDay { get; }
    public ReactiveCommand<Unit, Unit> BackDay { get; }

    public int SelectedIndexTeacherMon
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherMon, value);
            DisciplinesTeacherMon.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherMon].TeacherDisciplines) DisciplinesTeacherMon.Add(t);
        }
        get => _selectedIndexTeacherMon;
    }

    public int SelectedIndexTeacherTues
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherTues, value);
            DisciplinesTeacherTues.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherTues].TeacherDisciplines) DisciplinesTeacherTues.Add(t);
        }
        get => _selectedIndexTeacherTues;
    }

    public int SelectedIndexTeacherWed
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherWed, value);
            DisciplinesTeacherWed.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherWed].TeacherDisciplines) DisciplinesTeacherWed.Add(t);
        }
        get => _selectedIndexTeacherWed;
    }


    public int SelectedIndexTeacherThurs
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherThurs, value);
            DisciplinesTeacherThurs.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherThurs].TeacherDisciplines) DisciplinesTeacherThurs.Add(t);
        }
        get => _selectedIndexTeacherThurs;
    }

    public int SelectedIndexTeacherFri
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherFri, value);
            DisciplinesTeacherFri.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherFri].TeacherDisciplines) DisciplinesTeacherFri.Add(t);
        }
        get => _selectedIndexTeacherFri;
    }

    public int SelectedIndexTeacherSat
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherSat, value);
            DisciplinesTeacherSat.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherSat].TeacherDisciplines) DisciplinesTeacherSat.Add(t);
        }
        get => _selectedIndexTeacherSat;
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }

    private void ChangeIndexes()
    {
        SelectedIndexCabinetMon = 0;
        SelectedIndexDisciplineMon = 0;
        SelectedIndexTeacherMon = 0;

        SelectedIndexCabinetTues = 0;
        SelectedIndexDisciplineTues = 0;
        SelectedIndexTeacherTues = 0;

        SelectedIndexCabinetWed = 0;
        SelectedIndexDisciplineWed = 0;
        SelectedIndexTeacherWed = 0;

        SelectedIndexCabinetThurs = 0;
        SelectedIndexDisciplineThurs = 0;
        SelectedIndexTeacherThurs = 0;

        SelectedIndexCabinetFri = 0;
        SelectedIndexDisciplineFri = 0;
        SelectedIndexTeacherFri = 0;

        SelectedIndexCabinetSat = 0;
        SelectedIndexDisciplineSat = 0;
        SelectedIndexTeacherSat = 0;
    }

    private void ChangeDayOfTheWeek()
    {
        switch (_countDays)
        {
            case 0:
            {
                DayOfTheWeek = "Понедельник";
                IsEnableBack = false;
                break;
            }

            case 1:
            {
                DayOfTheWeek = "Вторник";
                IsEnableBack = true;
                break;
            }

            case 2:
            {
                DayOfTheWeek = "Среда";
                break;
            }

            case 3:
            {
                DayOfTheWeek = "Четверг";
                break;
            }

            case 4:
            {
                DayOfTheWeek = "Пятница";
                IsEnableNext = true;
                break;
            }

            case 5:
            {
                DayOfTheWeek = "Суббота";
                IsEnableNext = false;
                break;
            }
        }
    }

    public void AddOne()
    {
        foreach (var t in Teachers) TeachersMon.Add(t.TeacherFullName);

        foreach (var t in Cabinets) CabinetsMon.Add(t.CabinetNumber);
    }

    public void AddTwo()
    {
        foreach (var t in Teachers) TeachersTues.Add(t.TeacherFullName);

        foreach (var t in Cabinets) CabinetsTues.Add(t.CabinetNumber);
    }

    public void AddThree()
    {
        foreach (var t in Teachers) TeachersWed.Add(t.TeacherFullName);

        foreach (var t in Cabinets) CabinetsWed.Add(t.CabinetNumber);
    }

    public void AddFour()
    {
        foreach (var t in Teachers) TeachersThurs.Add(t.TeacherFullName);

        foreach (var t in Cabinets) CabinetsThurs.Add(t.CabinetNumber);
    }

    public void AddFive()
    {
        foreach (var t in Teachers) TeachersFri.Add(t.TeacherFullName);

        foreach (var t in Cabinets) CabinetsFri.Add(t.CabinetNumber);
    }
    
    public void AddSix()
    {
        foreach (var t in Teachers) TeachersSat.Add(t.TeacherFullName);

        foreach (var t in Cabinets) CabinetsSat.Add(t.CabinetNumber);
    }
}