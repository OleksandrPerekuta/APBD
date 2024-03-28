using System;
using System.Runtime.InteropServices.JavaScript;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Test;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{
    [Fact]
    public void AddUser_Should_Return_True_When_All_Data_Is_Correct()
    {
        var userService = new UserService();
        var addResult = userService.AddUser("David", "Kwiatkowski", "email@gmail.com", DateTime.Parse("01.01.2000"), 1);
        Assert.True(addResult);
    }

    [Fact]
    public void AddUser_Sould_Return_False_When_Firstname_Is_Missing()
    {
        var userService = new UserService();
        var addResult = userService.AddUser("", "Doe", "email@gmail.com", DateTime.Parse("01.01.2000"), 1);
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Sould_Return_False_When_Lastname_Is_Missing()
    {
        var userService = new UserService();
        var addResult = userService.AddUser("Dan", "", "email@gmail.com", DateTime.Parse("01.01.2000"), 1);
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_Invalid_Email_1()
    {
        var userService = new UserService();
        var addResult = userService.AddUser("Dan", "Kwiatkowski", "emailgmailcom", DateTime.Parse("01.01.2000"), 1);
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_False_Underage()
    {
        var userService = new UserService();
        var addResult = userService.AddUser("Dan", "Kwiatkowski", "email@gmailcom", DateTime.Parse("01.01.2020"), 1);
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_False_Email_Empty()
    {
        var userService = new UserService();
        var addResult = userService.AddUser("Dan", "Kwiatkowski", "", DateTime.Parse("01.01.2000"), 1);
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_Throw_Error_Invalid_Id()
    {
        var userService = new UserService();
        Assert.Throws<ArgumentException>(() =>
        {
            userService.AddUser("Dan", "Kwiatkowski", "email@gmail.com", DateTime.Parse("01.01.2000"), -1);
        });
    }
}