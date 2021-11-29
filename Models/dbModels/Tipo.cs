using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG.Models.dbModels
{
    [Table("Tipo")]
    public partial class Tipo
    {
        public Tipo()
        {
            Armas = new HashSet<Arma>();
        }

        [Key]
        [Column("idTipo")]
        public int IdTipo { get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [InverseProperty(nameof(Arma.TipoNavigation))]
        public virtual ICollection<Arma> Armas { get; set; }
    }
}
