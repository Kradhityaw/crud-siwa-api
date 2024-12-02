using System;
using System.Collections.Generic;

namespace SimpleCRUD.Models;

public partial class Siswa
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Sex { get; set; } = null!;

    public int AsalSekolahId { get; set; }

    public virtual AsalSekolah AsalSekolah { get; set; } = null!;
}
