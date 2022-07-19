using System.ComponentModel.DataAnnotations;

namespace jwt_aspnet_core.Models;

public class UserLogins
{
    [Required]
    public string UserName
    {
        get;
        set;
    }
    [Required]
    public string Password
    {
        get;
        set;
    }
    public UserLogins() { }
}