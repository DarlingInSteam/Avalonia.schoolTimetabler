namespace Avalonia.schoolTimetabler.Models;

public class School
{
    public string SchoolNumber;
    public string FullNameDirector;
    public string CountClasses;

    public School(string schoolNumber, string fullNameDirector, string countClasses)
    {
        SchoolNumber = schoolNumber;
        FullNameDirector = fullNameDirector;
        CountClasses = countClasses;
    }
}