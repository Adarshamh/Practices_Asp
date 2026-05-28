namespace StudentManagement.Api.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Semester { get; set; } = string.Empty;
    public decimal TotalFees { get; set; }
    public decimal FeesPaid { get; set; }
    public decimal FeesRemaining { get; set; }
    public string FeesStatus { get; set; } = string.Empty;
}