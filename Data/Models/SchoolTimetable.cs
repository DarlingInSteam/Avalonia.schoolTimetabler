namespace Data.Models;

public class SchoolTimetable
{
    public List<string> Teachers;
    public List<string> Disciplines;
    public List<string> Classes;

    public SchoolTimetable(List<string> teachers, List<string> disciplines, List<string> classes)
    {
        Teachers = teachers;
        Disciplines = disciplines;
        Classes = classes;
    }
}