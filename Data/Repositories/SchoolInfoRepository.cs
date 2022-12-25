using System.Reactive.Linq;using System.Reactive.Subjects;using Domain.Entities;using Domain.Repositories;namespace Data.Repositories;public class SchoolRepository : SerializationRepository<SchoolInfo>, ISchoolInfoRepository<SchoolInfo>{    private static SchoolRepository? _globalRepositoryInstance;    protected SchoolRepository(string path) : base(path)    {    }    public static SchoolRepository GetInstance()    {        return _globalRepositoryInstance ??= new SchoolRepository("../../../../Data/DataSets/SchoolInfo.json");    }    public void Delete()    {    }    public void Add(SchoolInfo newSchoolInfo)    {        Append(newSchoolInfo);        if (DeserializationJson().Count >= 2) Remove(DeserializationJson()[0]);    }    public SchoolInfo Read()    {        if (DeserializationJson().Count == 1) return DeserializationJson()[0];        return new SchoolInfo("Не задано", "Не задано", "Не задано", "Не задано");    }    public override bool CompareEntities(SchoolInfo entity1, SchoolInfo entity2)    {        return entity1.CountClasses.Equals(entity2.CountClasses)               && entity1.CountTeachers.Equals(entity2.SchoolNumber)               && entity1.FullNameDirector.Equals(entity2.FullNameDirector);    }}