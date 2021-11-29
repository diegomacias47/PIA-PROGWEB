using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG.Models.dbModels
{
    [Table("Usuario")]
    [Index(nameof(Correo), Name = "UQ_Correo", IsUnique = true)]
    public partial class Usuario
    {
        [Key]
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(150)]
        public string Correo { get; set; }
        [Required]
        [StringLength(50)]
        public string Contrasena { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Fondos { get; set; }
        public int? Rol { get; set; }

        [ForeignKey(nameof(Rol))]
        [InverseProperty("Usuarios")]
        public virtual Rol RolNavigation { get; set; }
        [InverseProperty("IdDetalleNavigation")]
        public virtual Carrito Carrito { get; set; }
        [InverseProperty("IdFacturaNavigation")]
        public virtual Factura Factura { get; set; }
    }
}
