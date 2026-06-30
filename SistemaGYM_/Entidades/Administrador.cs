using System.ComponentModel.DataAnnotations;

public class Administrador
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Usuario { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string Contrasenia {get; set;} = string.Empty;

}