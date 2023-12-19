using System.Collections;
using infrastructure;
using FluentAssertions;
using infrastructure.Models;
using infrastructure.Repositories;

namespace PlaywrightTests;

public class UserTests
{
    private UserRepository _repository;

    User retrievedUser = null;

    [SetUp]
    public void Setup()
    {
        _repository = new UserRepository();

    }

    [Test]
    public async Task ShouldSuccessfullyCreateUser()
    {
        User userToAdd = new User
        {
            UserName = "TestUser", IsAdmin = false, Email = "test@Email.com", MoreInfo = "nothing"
        };
        User addedUser = _repository.CreateUser(userToAdd);

        User retrievedUser = _repository.GetUserById(addedUser.UserId);

        retrievedUser.Should().BeEquivalentTo(addedUser, "it should be the same");
        _repository.DeleteUserById(retrievedUser.UserId);
        Assert.Pass("We did it!");
    }
    
    [Test]
    public async Task ShouldSuccessfullyUpdateUser()
    {
        User userToAdd = new User
        {
            UserName = "TestUser", IsAdmin = false, Email = "test@Email.com", MoreInfo = "nothing"
        };
        User addedUser = _repository.CreateUser(userToAdd);
        addedUser.UserName = "Updated Test User";
        User updatedUser = _repository.UpdateUser(addedUser);
        updatedUser.Should().BeEquivalentTo(addedUser, "it should be the same");
        _repository.DeleteUserById(updatedUser.UserId);
        Assert.Pass("We did it!");
    }
}