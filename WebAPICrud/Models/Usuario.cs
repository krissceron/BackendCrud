using System;
using System.Collections.Generic;

namespace WebAPICrud.Models;

public partial class Usuario
{
    public int UsuId { get; set; }

    public string? UsuNombre { get; set; }

    public string? UsuApellido { get; set; }

    public int? UsuCedula { get; set; }

    public string? UsuCorreo { get; set; }

    public string? UsuUsuario { get; set; }

    public string? UsuContrasenia { get; set; }
}
