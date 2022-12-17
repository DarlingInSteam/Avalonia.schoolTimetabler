using Data.Models;

namespace Data.FakeDataBase;

public class FDataBaseTimetable
{
    private readonly List<SchoolTimetable> _schoolTimetables;
    public IEnumerable<SchoolTimetable> SchoolTimetables { get; set; }

    private FDataBaseTimetable()
    {
        _schoolTimetables = new List<SchoolTimetable>();
        SchoolTimetables = _schoolTimetables;
    }
    
    public void AddTimetable(SchoolTimetable schoolTimetable)
    {
        _schoolTimetables.Add(schoolTimetable);
        SchoolTimetables = _schoolTimetables;
    }

    public static FDataBaseTimetable GetInstance()
    {
        return _instance ??= new FDataBaseTimetable();
    }

    private static FDataBaseTimetable? _instance = null;
}