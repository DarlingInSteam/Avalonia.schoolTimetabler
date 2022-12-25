using System.Collections;using System.Reactive.Linq;using System.Reactive.Subjects;using Domain.Entities;using Domain.Repositories;namespace Data.Repositories;public class TeacherRepository : SerializationRepository<Teacher>, ITeachersRepository<Teacher>{    private static TeacherRepository? _globalRepositoryInstance;    protected TeacherRepository(string path) : base(path)    {    }    public static TeacherRepository GetInstance()    {        return _globalRepositoryInstance ??=            new TeacherRepository("../../../../Data/DataSets/Teachers.json");    }    public void Delete(Teacher delEntity)    {        Remove(delEntity);    }    public void Add(Teacher newEntity)    {        Append(newEntity);    }    public List<Teacher> Read()    {        var t = DeserializationJson();        return t;    }    public void DelDiscipline(Teacher delTeacher, int indexTeacher)    {        Remove(DeserializationJson()[indexTeacher]);        Append(delTeacher);    }    public List<string> GetTeacherDiscipline(int index)    {        try        {            var teacherDisciplines = DeserializationJson()[index].TeacherDisciplines;            return teacherDisciplines;        }        catch (Exception e)        {            return new List<string>() { "Пусто" };        }    }    public void AddDiscipline(Teacher updTeacher)    {        Change(updTeacher);    }    public override bool CompareEntities(Teacher entity1, Teacher entity2)    {        return entity1.Id.Equals(entity2.Id);    }}