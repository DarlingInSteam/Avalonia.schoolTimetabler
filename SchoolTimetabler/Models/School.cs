namespace SchoolTimetabler.Models;

public class School
{
    public string SchoolNumber;
    public string FullNameDirector;
    public string CountClasses;
    public string CountTeachers;
    
    public School(string schoolNumber, string fullNameDirector, string countClasses, string countTeachers)
    {
        SchoolNumber = schoolNumber;
        FullNameDirector = fullNameDirector;
        CountClasses = countClasses;
        CountTeachers = countTeachers;
    }
}