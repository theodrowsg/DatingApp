using System;
using API.Extensions;

namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public  byte[] PasswordHash { get; set; } = [];
    public  byte[] PasswordSalt { get; set; } = [];
    public required string KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public DateOnly DateOfBirth { get; set; }
    public required string Gender { get; set; }
    public string? Introduction { get; set; }
    public string? LookingFor { get; set; }
    public string? Interests { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
     public List<Photo> Photos { get; set; } = [];
    // public ICollection<UserLike> LikedByUsers { get; set; } = new List<UserLike>();
    // public ICollection<UserLike> LikedUsers { get; set; } = new List<UserLike>();
    // public ICollection<Message> MessagesSent { get; set; } = new List<Message>();   
    // public ICollection<Message> MessagesReceived { get; set; } = new List<Message>();
    // public ICollection<Connection> Connections { get; set; } = new List<Connection>();

  // public int GetAge()
  //   {
  //       return DateOfBirth.CalculateAge();
  //   }
}
