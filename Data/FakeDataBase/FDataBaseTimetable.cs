using System.Collections.ObjectModel;
using Data.Models;

namespace Data.FakeDataBase;

public class FDataBaseTimetable
{
    private static FDataBaseTimetable? _instance;
    private readonly List<SchoolTimetable> _schoolTimetables;

    private FDataBaseTimetable()
    {
        _schoolTimetables = new List<SchoolTimetable>();
        SchoolTimetables = _schoolTimetables;
    }

    public IEnumerable<SchoolTimetable> SchoolTimetables { get; set; }

    public void AddTimetable(SchoolTimetable schoolTimetable)
    {
        _schoolTimetables.Add(schoolTimetable);
        SchoolTimetables = _schoolTimetables;
    }

    public ObservableCollection<string> GetDisciplines(string day, string classNumber)
    {
        var disciplines = new ObservableCollection<string>();
        foreach (var t in _schoolTimetables)
            if (t.Day == day && t.ClassOne == classNumber)
            {
                disciplines.Add(t.DisciplineOne);
                disciplines.Add(t.DisciplineTwo);
                disciplines.Add(t.DisciplineThree);
                disciplines.Add(t.DisciplineFour);
                disciplines.Add(t.DisciplineFive);
                disciplines.Add(t.DisciplineSix);
            }

        return disciplines;
    }

    public ObservableCollection<string> GetTeacher(string day, string classNumber)
    {
        var teachers = new ObservableCollection<string>();

        foreach (var t in _schoolTimetables)
            if (t.Day == day && t.ClassOne == classNumber)
            {
                teachers.Add(t.TeacherOne);
                teachers.Add(t.TeacherTwo);
                teachers.Add(t.TeacherThree);
                teachers.Add(t.TeacherFour);
                teachers.Add(t.TeacherFive);
                teachers.Add(t.TeacherSix);
            }

        return teachers;
    }

    public ObservableCollection<string> GetCabinet(string day, string classNumber)
    {
        var cabinets = new ObservableCollection<string>();

        foreach (var t in _schoolTimetables)
            if (t.Day == day && t.ClassOne == classNumber)
            {
                cabinets.Add(t.CabinetOne);
                cabinets.Add(t.CabinetTwo);
                cabinets.Add(t.CabinetThree);
                cabinets.Add(t.CabinetFour);
                cabinets.Add(t.CabinetFive);
                cabinets.Add(t.CabinetSix);
            }

        return cabinets;
    }

    public ObservableCollection<string> GetClass(string day, string classNumber)
    {
        var classes = new ObservableCollection<string>();

        foreach (var t in _schoolTimetables)
            if (t.Day == day && t.ClassOne == classNumber)
            {
                classes.Add(t.ClassOne);
                classes.Add(t.ClassTwo);
                classes.Add(t.ClassThree);
                classes.Add(t.ClassFour);
                classes.Add(t.ClassFive);
                classes.Add(t.ClassSix);
            }

        return classes;
    }

    public static FDataBaseTimetable GetInstance()
    {
        return _instance ??= new FDataBaseTimetable();
    }
}