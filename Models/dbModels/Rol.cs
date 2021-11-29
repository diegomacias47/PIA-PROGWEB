using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG.Models.dbModels
{
    [Table("Rol")]
    public partial class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<Usuario>();
        }

        [Key]
        [Column("idRol")]
        public int IdRol { get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [InverseProperty(nameof(Usuario.RolNavigation))]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
