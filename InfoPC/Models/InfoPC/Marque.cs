using System;
using System.Collections.Generic;

namespace InfoPC.Models.InfoPC;

public partial class Marque
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Ordinateur> Ordinateurs { get; set; } = new List<Ordinateur>();
}
