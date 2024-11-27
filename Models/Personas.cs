using Ej3Personas.ViewModels;
using System;
using System.Collections.Generic;

namespace Ej3Personas.Models;

public partial class Personas
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public Sexo Genero { get; set; }
}
