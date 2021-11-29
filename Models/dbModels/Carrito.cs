using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG.Models.dbModels
{
    [Table("Carrito")]
    public partial class Carrito
    {
        [Key]
        [Column("idDetalle")]
        public int IdDetalle { get; set; }
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Column("idArma")]
        public int IdArma { get; set; }
        [Column("idEstatus")]
        public bool IdEstatus { get; set; }

        [ForeignKey(nameof(IdArma))]
        [InverseProperty(nameof(Arma.Carritos))]
        public virtual Arma IdArmaNavigation { get; set; }
        [ForeignKey(nameof(IdDetalle))]
        [InverseProperty(nameof(Usuario.Carrito))]
        public virtual Usuario IdDetalleNavigation { get; set; }
    }
}
