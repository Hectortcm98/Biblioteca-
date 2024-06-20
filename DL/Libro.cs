using System;
using System.Collections.Generic;

namespace DL;

public partial class Libro
{
    public int IdLibro { get; set; }

    public string Autor { get; set; } = null!;

    public string TituloLibro { get; set; } = null!;

    public DateTime AñoPublicacion { get; set; }

    public string? Imagen { get; set; }

    public int? IdEditorial { get; set; }

    public virtual Editorial? IdEditorialNavigation { get; set; }
}
