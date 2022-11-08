using System;

namespace SchoolTimetabler.Models;

public class User
{
    public string FullName;
    public string Post;

    public User(string fullName, string post)
    {
        FullName = fullName;
        Post = post;
    }
}