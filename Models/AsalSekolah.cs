using System;
using System.Collections.Generic;

namespace SimpleCRUD.Models;

public partial class AsalSekolah
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Siswa> Siswas { get; set; } = new List<Siswa>();
}
