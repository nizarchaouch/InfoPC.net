using System;
using System.Collections.Generic;

namespace InfoPC.Models.InfoPC;

public partial class Ordinateur
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int Prix { get; set; }

    public int MarqueId { get; set; }

    public virtual Marque? Marque { get; set; } = null!;
}
