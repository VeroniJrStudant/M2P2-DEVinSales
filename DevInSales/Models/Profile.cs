using System.ComponentModel.DataAnnotations.Schema;

namespace DevInSales.Models;

public class Profile
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name{ get; set; }
    [Column("email")]
    public int Email { get; set; }

    public Profile()
    {
    }

    public Profile(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}