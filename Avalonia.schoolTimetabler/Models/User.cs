using System;

namespace Avalonia.schoolTimetabler.Models;

public class User
{
    public string Password;
    public string UserName;

    public User(string password, string userName)
    {
        Password = password;
        UserName = userName;
    }
}