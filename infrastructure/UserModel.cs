namespace infrastructure;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string Type { get; set; }
    public string MoreInfo { get; set; }
     
}