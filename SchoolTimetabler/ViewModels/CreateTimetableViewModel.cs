using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
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
    private string _dayOfTheWeek = "Понедельник";
    private bool _isEnableNext = true;
    private bool _isEnableBack = false;
    private int countDays = 0;

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

    public ObservableCollection<Data.Models.SchoolTeachers> Teachers { get; }
    public ObservableCollection<string> TeachersName { get; set; }
    public ObservableCollection<Data.Models.SchoolCabinet> Cabinets { get; }
    public ObservableCollection<string> CabinetsNumbers { get; set; }
    public ObservableCollection<Data.Models.SchoolClass> Classes { get; }
    public ObservableCollection<string> ClassesNumber { get; set; }

    public ReactiveCommand<Unit, Unit> SaveOneTimetable { get; }
    public ReactiveCommand<Unit, Unit> NextDay { get; }
    public ReactiveCommand<Unit, Unit> BackDay { get; }

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

        if (Teachers.Count != 0)
        {
            foreach (var t in Teachers[0].TeacherDisciplines)
            {
                DisciplinesTeacherMon.Add(t);
                DisciplinesTeacherFri.Add(t);
                DisciplinesTeacherSat.Add(t);
                DisciplinesTeacherThurs.Add(t);
                DisciplinesTeacherTues.Add(t);
                DisciplinesTeacherWed.Add(t);

            }
        }
        else
        {
            IsEnableNext = false;
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
            var timetable = new Data.Models.SchoolTimetable();

            timetable.Day = DayOfTheWeek;

            timetable.TeacherOne = TeachersName[_selectedIndexTeacherMon];
            timetable.DisciplineOne = DisciplinesTeacherMon[_selectedIndexDisciplineMon];
            timetable.CabinetOne = CabinetsNumbers[_selectedIndexCabinetMon];
            timetable.ClassOne = ClassesNumber[_selectedIndexClassMon];

            timetable.TeacherTwo = TeachersName[_selectedIndexTeacherTues];
            timetable.DisciplineTwo = DisciplinesTeacherTues[_selectedIndexDisciplineTues];
            timetable.CabinetTwo = CabinetsNumbers[_selectedIndexCabinetTues];
            timetable.ClassTwo = ClassesNumber[_selectedIndexClassTues];

            timetable.TeacherThree = TeachersName[_selectedIndexTeacherWed];
            timetable.DisciplineThree = DisciplinesTeacherWed[_selectedIndexDisciplineWed];
            timetable.CabinetThree = CabinetsNumbers[_selectedIndexCabinetWed];
            timetable.ClassThree = ClassesNumber[_selectedIndexClassWed];

            timetable.TeacherFour = TeachersName[_selectedIndexTeacherThurs];
            timetable.DisciplineFour = DisciplinesTeacherThurs[_selectedIndexDisciplineThurs];
            timetable.CabinetFour = CabinetsNumbers[_selectedIndexCabinetThurs];
            timetable.ClassFour = ClassesNumber[_selectedIndexClassThurs];

            timetable.TeacherFive = TeachersName[_selectedIndexTeacherFri];
            timetable.DisciplineFive = DisciplinesTeacherFri[_selectedIndexDisciplineFri];
            timetable.CabinetFive = CabinetsNumbers[_selectedIndexCabinetFri];
            timetable.ClassFive = ClassesNumber[_selectedIndexClassFri];

            timetable.TeacherSix = TeachersName[_selectedIndexTeacherSat];
            timetable.DisciplineSix = DisciplinesTeacherSat[_selectedIndexDisciplineSat];
            timetable.CabinetSix = CabinetsNumbers[_selectedIndexCabinetSat];
            timetable.ClassSix = ClassesNumber[_selectedIndexClassSat];

            _storageTimetable.AddTimetable(timetable);
        });
    }

    private void ChangeIndexes()
    {
        SelectedIndexCabinetMon = 0;
        SelectedIndexClassMon = 0;
        SelectedIndexDisciplineMon = 0;
        SelectedIndexTeacherMon = 0;

        SelectedIndexCabinetTues = 0;
        SelectedIndexClassTues = 0;
        SelectedIndexDisciplineTues = 0;
        SelectedIndexTeacherTues = 0;

        SelectedIndexCabinetWed = 0;
        SelectedIndexClassWed = 0;
        SelectedIndexDisciplineWed = 0;
        SelectedIndexTeacherWed = 0;

        SelectedIndexCabinetThurs = 0;
        SelectedIndexClassThurs = 0;
        SelectedIndexDisciplineThurs = 0;
        SelectedIndexTeacherThurs = 0;

        SelectedIndexCabinetFri = 0;
        SelectedIndexClassFri = 0;
        SelectedIndexDisciplineFri = 0;
        SelectedIndexTeacherFri = 0;

        SelectedIndexCabinetSat = 0;
        SelectedIndexClassSat = 0;
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

    public int SelectedIndexTeacherMon
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherMon, value);
            DisciplinesTeacherMon.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherMon].TeacherDisciplines)
            {
                DisciplinesTeacherMon.Add(t);
            }
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

    public int SelectedIndexClassMon
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexClassMon, value);
        get => _selectedIndexClassMon;
    }

    public int SelectedIndexTeacherTues
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherTues, value);
            DisciplinesTeacherTues.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherTues].TeacherDisciplines)
            {
                DisciplinesTeacherTues.Add(t);
            }
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

    public int SelectedIndexClassTues
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexClassTues, value);
        get => _selectedIndexClassTues;
    }

    public int SelectedIndexTeacherWed
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherWed, value);
            DisciplinesTeacherWed.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherWed].TeacherDisciplines)
            {
                DisciplinesTeacherWed.Add(t);
            }
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

    public int SelectedIndexClassWed
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexClassWed, value);
        get => _selectedIndexClassWed;
    }

    public int SelectedIndexTeacherThurs
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherThurs, value);
            DisciplinesTeacherThurs.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherThurs].TeacherDisciplines)
            {
                DisciplinesTeacherThurs.Add(t);
            }
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

    public int SelectedIndexClassThurs
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexClassThurs, value);
        get => _selectedIndexClassThurs;
    }

    public int SelectedIndexTeacherFri
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherFri, value);
            DisciplinesTeacherFri.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherFri].TeacherDisciplines)
            {
                DisciplinesTeacherFri.Add(t);
            }
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

    public int SelectedIndexClassFri
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexClassFri, value);
        get => _selectedIndexClassFri;
    }

    public int SelectedIndexTeacherSat
    {
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedIndexTeacherSat, value);
            DisciplinesTeacherSat.Clear();
            foreach (var t in Teachers[_selectedIndexTeacherSat].TeacherDisciplines)
            {
                DisciplinesTeacherSat.Add(t);
            }
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

    public int SelectedIndexClassSat
    {
        set => this.RaiseAndSetIfChanged(ref _selectedIndexClassSat, value);
        get => _selectedIndexClassSat;
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }

    private int _selectedIndexTeacherMon;
    private int _selectedIndexDisciplineMon;
    private int _selectedIndexCabinetMon;
    private int _selectedIndexClassMon;
    private int _selectedIndexTeacherTues;
    private int _selectedIndexDisciplineTues;
    private int _selectedIndexCabinetTues;
    private int _selectedIndexClassTues;
    private int _selectedIndexTeacherWed;
    private int _selectedIndexDisciplineWed;
    private int _selectedIndexCabinetWed;
    private int _selectedIndexClassWed;
    private int _selectedIndexTeacherThurs;
    private int _selectedIndexDisciplineThurs;
    private int _selectedIndexCabinetThurs;
    private int _selectedIndexClassThurs;
    private int _selectedIndexTeacherFri;
    private int _selectedIndexDisciplineFri;
    private int _selectedIndexCabinetFri;
    private int _selectedIndexClassFri;
    private int _selectedIndexTeacherSat;
    private int _selectedIndexDisciplineSat;
    private int _selectedIndexCabinetSat;
    private int _selectedIndexClassSat;
}