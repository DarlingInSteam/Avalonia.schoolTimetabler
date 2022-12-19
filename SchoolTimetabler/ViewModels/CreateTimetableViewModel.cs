using System.Collections.ObjectModel;
using System.Reactive;
using Data.FakeDataBase;
using Data.Models;
using Data.Repositories;
using Data.Repository;
using Domain.Entities;
using Domain.UseCases;
using ReactiveUI;

namespace SchoolTimetabler.ViewModels;

public class CreateTimetableViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private string _dayOfTheWeek = "Понедельник";
    private bool _isEnableBack;
    private bool _isEnableNext = true;
    private int _selectedIndexCabinetFri;
    private int _selectedIndexCabinetMon;
    private int _selectedIndexCabinetSat;
    private int _selectedIndexCabinetThurs;
    private int _selectedIndexCabinetTues;
    private int _selectedIndexCabinetWed;
    private int _selectedIndexClass;
    private int _selectedIndexDisciplineFri;
    private int _selectedIndexDisciplineMon;
    private int _selectedIndexDisciplineSat;
    private int _selectedIndexDisciplineThurs;
    private int _selectedIndexDisciplineTues;
    private int _selectedIndexDisciplineWed;
    private int _selectedIndexTeacherFri;

    private int _selectedIndexTeacherMon;
    private int _selectedIndexTeacherSat;
    private int _selectedIndexTeacherThurs;
    private int _selectedIndexTeacherTues;
    private int _selectedIndexTeacherWed;
    private readonly CabinetInteractor _cabinetInteractor;
    private readonly ClassInteractor _classInteractor;
    private readonly FDataBaseTeachers _storageTeachers;
    private readonly FDataBaseTimetable _storageTimetable;
    private int countDays;

    public CreateTimetableViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        _storageTimetable = FDataBaseTimetable.GetInstance();
        _storageTeachers = FDataBaseTeachers.GetInstance();
        _cabinetInteractor = new CabinetInteractor(CabinetsRepository.GetInstance());
        _classInteractor = new ClassInteractor(ClassesRepository.GetInstance());

        DisciplinesTeacherMon = new ObservableCollection<string>();
        DisciplinesTeacherSat = new ObservableCollection<string>();
        DisciplinesTeacherFri = new ObservableCollection<string>();
        DisciplinesTeacherThurs = new ObservableCollection<string>();
        DisciplinesTeacherTues = new ObservableCollection<string>();
        DisciplinesTeacherWed = new ObservableCollection<string>();

        TeachersName = new ObservableCollection<string>();
        Teachers = new ObservableCollection<SchoolTeachers>(_storageTeachers.SchoolTeachers);
        CabinetsNumbers = new ObservableCollection<string>();
        Cabinets = new ObservableCollection<Cabinet>(_cabinetInteractor.GetCabinets());
        ClassesNumber = new ObservableCollection<string>();
        Classes = new ObservableCollection<Class>(_classInteractor.GetClasses());

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
            countDays -= 1;

            ChangeIndexes();
            ChangeDayOfTheWeek();
        });

        NextDay = ReactiveCommand.Create(() =>
        {
            countDays += 1;

            ChangeIndexes();
            ChangeDayOfTheWeek();
        });

        SaveOneTimetable = ReactiveCommand.Create(() =>
        {
            var timetable = new SchoolTimetable();

            timetable.Day = DayOfTheWeek;

            timetable.TeacherOne = TeachersName[_selectedIndexTeacherMon];
            timetable.DisciplineOne = DisciplinesTeacherMon[_selectedIndexDisciplineMon];
            timetable.CabinetOne = CabinetsNumbers[_selectedIndexCabinetMon];
            timetable.ClassOne = ClassesNumber[_selectedIndexClass];

            timetable.TeacherTwo = TeachersName[_selectedIndexTeacherTues];
            timetable.DisciplineTwo = DisciplinesTeacherTues[_selectedIndexDisciplineTues];
            timetable.CabinetTwo = CabinetsNumbers[_selectedIndexCabinetTues];
            timetable.ClassTwo = ClassesNumber[_selectedIndexClass];

            timetable.TeacherThree = TeachersName[_selectedIndexTeacherWed];
            timetable.DisciplineThree = DisciplinesTeacherWed[_selectedIndexDisciplineWed];
            timetable.CabinetThree = CabinetsNumbers[_selectedIndexCabinetWed];
            timetable.ClassThree = ClassesNumber[_selectedIndexClass];

            timetable.TeacherFour = TeachersName[_selectedIndexTeacherThurs];
            timetable.DisciplineFour = DisciplinesTeacherThurs[_selectedIndexDisciplineThurs];
            timetable.CabinetFour = CabinetsNumbers[_selectedIndexCabinetThurs];
            timetable.ClassFour = ClassesNumber[_selectedIndexClass];

            timetable.TeacherFive = TeachersName[_selectedIndexTeacherFri];
            timetable.DisciplineFive = DisciplinesTeacherFri[_selectedIndexDisciplineFri];
            timetable.CabinetFive = CabinetsNumbers[_selectedIndexCabinetFri];
            timetable.ClassFive = ClassesNumber[_selectedIndexClass];

            timetable.TeacherSix = TeachersName[_selectedIndexTeacherSat];
            timetable.DisciplineSix = DisciplinesTeacherSat[_selectedIndexDisciplineSat];
            timetable.CabinetSix = CabinetsNumbers[_selectedIndexCabinetSat];
            timetable.ClassSix = ClassesNumber[_selectedIndexClass];

            _storageTimetable.AddTimetable(timetable);
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

    public ObservableCollection<SchoolTeachers> Teachers { get; }
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

    public int SelectedIndexClass
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexClass, value);
        get => _selectedIndexClass;
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
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherWed, value);
            DisciplinesTeacherWed.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherWed].TeacherDisciplines) DisciplinesTeacherWed.Add(t);
        }
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
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherThurs, value);
            DisciplinesTeacherThurs.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherThurs].TeacherDisciplines) DisciplinesTeacherThurs.Add(t);
        }
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
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherFri, value);
            DisciplinesTeacherFri.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherFri].TeacherDisciplines) DisciplinesTeacherFri.Add(t);
        }
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
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherSat, value);
            DisciplinesTeacherSat.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherSat].TeacherDisciplines) DisciplinesTeacherSat.Add(t);
        }
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
        switch (countDays)
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