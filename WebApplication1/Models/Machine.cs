namespace WebApplication1.Models;

public class Machine
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public List<string> Muscles { get; set; }
    public decimal PoidsActuel { get; set; }
    public decimal PoidsPlaque { get; set; }
}

