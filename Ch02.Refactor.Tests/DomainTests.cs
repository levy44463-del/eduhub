using EduHub.Ch02;
using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

public class DomainTests
{
    [Fact]
    public void CreateClub_WithEmptyName_ThrowsArgumentException()
    {
        var manager = new ClubManager();
        var act = () => manager.CreateClub("", "mô tả", "p@gmail.com");
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        var club = Club.Create("Tech Club", "Câu lạc bộ công nghệ");

        Assert.NotNull(club);
        Assert.Equal("Tech Club", club.Name);
        Assert.Equal("Câu lạc bộ công nghệ", club.Description);
        Assert.NotEqual(Guid.Empty, club.Id);
    }

    [Fact]
    public void Club_Create_WithEmptyName_ThrowsArgumentException()
    {
        var act = () => Club.Create("", "mô tả");
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_WithNameOver100Chars_ThrowsArgumentException()
    {
        var longName = new string('a', 101);
        var act = () => Club.Create(longName, "mô tả");
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_HasRoom_UnderMaxMembers_ReturnsTrue()
    {
        var club = Club.Create("Tech Club", "mô tả");
        Assert.True(club.HasRoom(Club.MaxMembers - 1));
    }

    [Fact]
    public void Club_HasRoom_AtMaxMembers_ReturnsFalse()
    {
        var club = Club.Create("Tech Club", "mô tả");
        Assert.False(club.HasRoom(Club.MaxMembers));
    }

    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        var evt = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop", Date = DateTime.Now };
        var memberId = Guid.NewGuid();

        var result = evt.Register(memberId);

        Assert.True(result);
        Assert.Contains(memberId, evt.RegisteredMemberIds);
    }

    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        var evt = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop", Date = DateTime.Now };
        var memberId = Guid.NewGuid();

        evt.Register(memberId);
        var result = evt.Register(memberId);

        Assert.False(result);
    }

    [Fact]
    public void Event_CheckIn_RegisteredMember_Success()
    {
        var evt = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop", Date = DateTime.Now };
        var memberId = Guid.NewGuid();
        evt.Register(memberId);

        evt.CheckIn(memberId);

        Assert.Contains(memberId, evt.CheckedInMemberIds);
    }

    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        var evt = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop", Date = DateTime.Now };
        var memberId = Guid.NewGuid();

        var act = () => evt.CheckIn(memberId);
        Assert.Throws<InvalidOperationException>(act);
    }
}
