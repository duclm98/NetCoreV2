namespace Core.Models;

public class AuditLogCreateDto
{
    public int? CreatedBy { get; set; }
    public string Method { get; set; }
}