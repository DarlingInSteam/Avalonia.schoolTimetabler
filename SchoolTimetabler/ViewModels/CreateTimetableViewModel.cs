using System.Collections.ObjectModel;
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
    [Reactive]
    public int SelectedIndexCabinetFri { get; set; }
    [Reactive]
    public int SelectedIndexCabinetMon { get; set; }
    [Reactive]
    public int SelectedIndexCabinetSat { get; set; }
    [Reactive]
    public int SelectedIndexCabinetThurs { get; set; }
    [Reactive]
    public int SelectedIndexCabinetTues { get; set; }
    [Reactive]
    public int SelectedIndexCabinetWed { get; set; }
    [Reactive]
    public int SelectedIndexClass { get; set; }
    [Reactive]
    public int SelectedIndexDisciplineFri { get; set; }
    [Reactive]
    public int SelectedIndexDisciplineMon { get; set; }
    [Reactive]
    public int SelectedIndexDisciplineSat { get; set; }
    [Reactive]
    public int SelectedIndexDisciplineThurs { get; set; }
    [Reactive]
    public int SelectedIndexDisciplineTues { get; set; }
    [Reactive]
    public int SelectedIndexDisciplineWed { get; set; }

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

        TeachersName = new ObservableCollection<string>();
        Teachers = new ObservableCollection<Teacher>(teacherInteractor.GetTeachers());
        CabinetsNumbers = new ObservableCollection<string>();
        Cabinets = new ObservableCollection<Cabinet>(cabinetInteractor.GetCabinets());
        ClassesNumber = new ObservableCollection<string>();
        Classes = new ObservableCollection<Class>(classInteractor.GetClasses());

        if (Teachers.Count != 0)
            foreach (var t in Teachers[0].TeacherDisciplines)
            {
                DisciplinesTeacherMon.Add(t);
                DisciplinesTeacherFri.Add(t);
                DisciplinesTeacherSat.Add(t);
                DisciplinesTeacherThurs.Add(t);
                DisciplinesTeacherTues.Add(t);
                DisciplinesTeacherWed.Add(t);
            }
        else
            IsEnableNext = false;

        foreach (var t in Classes) ClassesNumber.Add(t.Number + t.Symbol);

        foreach (var t in Teachers) TeachersName.Add(t.TeacherFullName);

        foreach (var t in Cabinets) CabinetsNumbers.Add(t.CabinetNumber);

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

            timetable.TeacherOne = TeachersName[_selectedIndexTeacherMon];
            timetable.DisciplineOne = DisciplinesTeacherMon[SelectedIndexDisciplineMon];
            timetable.CabinetOne = CabinetsNumbers[SelectedIndexCabinetMon];
            timetable.ClassOne = ClassesNumber[SelectedIndexClass];

            timetable.TeacherTwo = TeachersName[_selectedIndexTeacherTues];
            timetable.DisciplineTwo = DisciplinesTeacherTues[SelectedIndexDisciplineTues];
            timetable.CabinetTwo = CabinetsNumbers[SelectedIndexCabinetTues];
            timetable.ClassTwo = ClassesNumber[SelectedIndexClass];

            timetable.TeacherThree = TeachersName[_selectedIndexTeacherWed];
            timetable.DisciplineThree = DisciplinesTeacherWed[SelectedIndexDisciplineWed];
            timetable.CabinetThree = CabinetsNumbers[SelectedIndexCabinetWed];
            timetable.ClassThree = ClassesNumber[SelectedIndexClass];

            timetable.TeacherFour = TeachersName[_selectedIndexTeacherThurs];
            timetable.DisciplineFour = DisciplinesTeacherThurs[SelectedIndexDisciplineThurs];
            timetable.CabinetFour = CabinetsNumbers[SelectedIndexCabinetThurs];
            timetable.ClassFour = ClassesNumber[SelectedIndexClass];

            timetable.TeacherFive = TeachersName[_selectedIndexTeacherFri];
            timetable.DisciplineFive = DisciplinesTeacherFri[SelectedIndexDisciplineFri];
            timetable.CabinetFive = CabinetsNumbers[SelectedIndexCabinetFri];
            timetable.ClassFive = ClassesNumber[SelectedIndexClass];

            timetable.TeacherSix = TeachersName[_selectedIndexTeacherSat];
            timetable.DisciplineSix = DisciplinesTeacherSat[SelectedIndexDisciplineSat];
            timetable.CabinetSix = CabinetsNumbers[SelectedIndexCabinetSat];
            timetable.ClassSix = ClassesNumber[SelectedIndexClass];

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
    public ObservableCollection<Cabinet> Cabinets { get; }
    public ObservableCollection<string> CabinetsNumbers { get; set; }
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
}