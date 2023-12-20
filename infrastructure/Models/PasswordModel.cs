namespace infrastructure;

public class Password
{
    public int UserId { get; set; }
    public required string Hash { get; set; }
    public required string Salt { get; set; }
}