using System;
using System.Collections.Generic;

namespace InventarioAPI.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ContraseniaHash { get; set; } = null!;

    public string ContraseniaSalt { get; set; } = null!;
}
