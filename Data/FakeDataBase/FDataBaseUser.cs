using Data.Models;

namespace Data.FakeDataBase;

public class FDataBaseUser
{
    private readonly SchoolUser _user;
    public SchoolUser UserPub { get; set; }

    private FDataBaseUser()
    {
        _user = new SchoolUser();
        UserPub = _user;
    }
    
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

    private static FDataBaseUser? _instance = null;
}