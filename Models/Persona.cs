using System.ComponentModel.DataAnnotations;

public class Persona
{
    public int Id { get; set; }  // campo de clave principal

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? NumeroDocumento { get; set; }

    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    public string? CorreoElectronico { get; set; }

    public string? Telefono { get; set; }

    [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento no es válida.")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? FechaNacimiento { get; set; }
}
