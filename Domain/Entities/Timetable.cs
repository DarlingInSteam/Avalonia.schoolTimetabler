using System.Collections.ObjectModel;

namespace Domain.Entities;

[Serializable]
public class Timetable
{
    public string? Day { get; set; }
    public string? TeacherOne { get; set; }
    public string? DisciplineOne { get; set; }
    public string? ClassOne { get; set; }
    public string? CabinetOne { get; set; }
    public string? LessonNumberOne { get; set; }

    public string? TeacherTwo { get; set; }
    public string? DisciplineTwo { get; set; }
    public string? ClassTwo { get; set; }
    public string? CabinetTwo { get; set; }
    public string? LessonNumberTwo { get; set; }

    public string? TeacherThree { get; set; }
    public string? DisciplineThree { get; set; }
    public string? ClassThree { get; set; }
    public string? CabinetThree { get; set; }
    public string? LessonNumberThree { get; set; }

    public string? TeacherFour { get; set; }
    public string? DisciplineFour { get; set; }
    public string? ClassFour { get; set; }
    public string? CabinetFour { get; set; }
    public string? LessonNumberFour { get; set; }

    public string? TeacherFive { get; set; }
    public string? DisciplineFive { get; set; }
    public string? ClassFive { get; set; }
    public string? CabinetFive { get; set; }
    public string? LessonNumberFive { get; set; }

    public string? TeacherSix { get; set; }
    public string? DisciplineSix { get; set; }
    public string? ClassSix { get; set; }
    public string? CabinetSix { get; set; }
    public string? LessonNumberSix { get; set; }
}