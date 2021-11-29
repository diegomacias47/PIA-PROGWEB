using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG.Models.dbModels
{
    [Table("Estado")]
    public partial class Estado
    {
        public Estado()
        {
            Armas = new HashSet<Arma>();
        }

        [Key]
        [Column("idEstado")]
        public int IdEstado { get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [InverseProperty(nameof(Arma.EstadoNavigation))]
        public virtual ICollection<Arma> Armas { get; set; }
    }
}
