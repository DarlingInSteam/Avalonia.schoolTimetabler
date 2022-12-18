using Data.Models;

namespace Data.FakeDataBase;

public class FDataBaseUser
{
    private static FDataBaseUser? _instance;
    private readonly SchoolUser _user;

    private FDataBaseUser()
    {
        _user = new SchoolUser("Не задано", "Не задано");
        UserPub = _user;
    }

    public SchoolUser UserPub { get; set; }

    public void AddUser(SchoolUser user)
    {
        _user.Post = user.Post;
        _user.FullName = user.FullName;
        UserPub = _user;
    }

    public void DeleteUser()
    {
        _user.Post = "";
        _user.FullName = "";
        UserPub = _user;
    }

    public static FDataBaseUser GetInstance()
    {
        return _instance ??= new FDataBaseUser();
    }
}