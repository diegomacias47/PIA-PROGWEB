using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG.Models.dbModels
{
    [Table("Factura")]
    public partial class Factura
    {
        [Key]
        [Column("idFactura")]
        public int IdFactura { get; set; }
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Column("precioTotal", TypeName = "decimal(18, 0)")]
        public decimal PrecioTotal { get; set; }
        [Column("fechaCompra", TypeName = "date")]
        public DateTime FechaCompra { get; set; }

        [ForeignKey(nameof(IdFactura))]
        [InverseProperty(nameof(Usuario.Factura))]
        public virtual Usuario IdFacturaNavigation { get; set; }
    }
}
