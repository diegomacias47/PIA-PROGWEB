using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG.Models.dbModels
{
    [Table("Arma")]
    public partial class Arma
    {
        public Arma()
        {
            Carritos = new HashSet<Carrito>();
        }

        [Key]
        [Column("idArma")]
        public int IdArma { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int Estado { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Precio { get; set; }
        public double? Float { get; set; }
        public string Foto { get; set; }
        public bool? Estatus { get; set; }

        [ForeignKey(nameof(Estado))]
        [InverseProperty("Armas")]
        public virtual Estado EstadoNavigation { get; set; }
        [ForeignKey(nameof(Tipo))]
        [InverseProperty("Armas")]
        public virtual Tipo TipoNavigation { get; set; }
        [InverseProperty(nameof(Carrito.IdArmaNavigation))]
        public virtual ICollection<Carrito> Carritos { get; set; }
    }
}
