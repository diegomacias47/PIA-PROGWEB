using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PROG.Models;
using PROG.Models.dbModels;
using PROG.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PROG.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly P141121Context _dbcontext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public Usuario usuario = null;


        public HomeController(ILogger<HomeController> logger, P141121Context dbcontext, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _dbcontext = dbcontext;
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult ArmaModificar()
        {
            var query2 = _dbcontext.Armas.Join(_dbcontext.Tipos, x => x.Tipo, y => y.IdTipo, (x, y) => new
            {
                x,
                y,

            }).Join(_dbcontext.Estados, x => x.x.Estado, h => h.IdEstado, (x, h) => new
            {
                x,
                h

            });

            List<ModeloArma> lstMA = new List<ModeloArma>();

            foreach (var qry in query2)
            {

                lstMA.Add(new ModeloArma
                {
                    idArma = qry.x.x.IdArma,
                    Nombre = qry.x.x.Nombre,
                    Tipo = qry.x.x.Tipo,
                    Estado = qry.x.x.Estado,
                    Precio = qry.x.x.Precio,
                    Float = qry.x.x.Float,
                    dTipo = qry.x.y.Descripcion,
                    dEstado = qry.h.Descripcion,
                    dirFoto = qry.x.x.Foto
                });
            }

            ModeloAdministrador ma = new ModeloAdministrador
            {
                lstMA = lstMA
            };

            return View("ArmaModificar",ma);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Reedireccion(ModeloUsuario us)
        {
            usuario = _dbcontext.Usuarios.Where(x => x.Correo.Equals(us.Correo) && x.Contrasena.Equals(us.Contrasena)).FirstOrDefault();
            List<Arma> armas = _dbcontext.Armas.OrderBy(x => x.IdArma).ToList();


            if (usuario == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                us = new ModeloUsuario
                {
                    idUsuario = usuario.IdUsuario,
                    Nombre = usuario.Nombre,
                    Correo = usuario.Correo,
                    Contrasena = usuario.Contrasena,
                    Fondos = usuario.Fondos,
                    Rol = usuario.Rol
                };

                if(us.Rol == 1)
                {
                    
                    return Administrador();
                }
                else
                {
                    ModeloTienda tienda = new ModeloTienda
                    {
                        usuario = us,
                        lstA = armas
                    };
                    return View("Tienda", tienda);
                }
            }        
        }


        public IActionResult ArmaDetalles(int id)
        {
            var ar = _dbcontext.Armas.Join(_dbcontext.Tipos, x => x.Tipo, y => y.IdTipo, (x, y) => new
            {
                x,
                y,

            }).Join(_dbcontext.Estados, x => x.x.Estado, h => h.IdEstado, (x, h) => new
            {
                x,
                h

            }).Where(z => z.x.x.IdArma == id).FirstOrDefault();



            ModeloArma avm = new ModeloArma
            {
                idArma = ar.x.x.IdArma,
                Nombre = ar.x.x.Nombre,
                Tipo = ar.x.x.Tipo,
                Estado = ar.x.x.Estado,
                Precio = ar.x.x.Precio,
                Float = ar.x.x.Float,
                dTipo = ar.x.y.Descripcion,
                dEstado = ar.h.Descripcion,
                dirFoto = ar.x.x.Foto

            };

            return View(avm);
        }

        public IActionResult Administrador()
        {            
            List<Arma> armas = _dbcontext.Armas.OrderBy(x => x.IdArma).ToList();

            var query = _dbcontext.Armas.Join(_dbcontext.Tipos, x => x.Tipo, y => y.IdTipo, (x, y) => new
            {
                x,
                y
            }).GroupBy(tipo => tipo.y.Descripcion).Select(grupo => new
            {
                tipo = grupo.Key,
                Count = grupo.Count()

            });

            List<Almacen> alm = new List<Almacen>();

            foreach (var qry in query)
            {

                alm.Add(new Almacen
                {
                    des = qry.tipo,
                    tam = qry.Count
                });
            }

            var query2 = _dbcontext.Armas.Join(_dbcontext.Tipos, x => x.Tipo, y => y.IdTipo, (x, y) => new
            {
                x,
                y,

            }).Join(_dbcontext.Estados, x => x.x.Estado, h => h.IdEstado, (x, h) => new
            {
                x,
                h

            });

            List<ModeloArma> lstMA = new List<ModeloArma>();

            foreach (var qry in query2)
            {

                lstMA.Add(new ModeloArma
                {
                    idArma = qry.x.x.IdArma,
                    Nombre = qry.x.x.Nombre,
                    Tipo = qry.x.x.Tipo,
                    Estado = qry.x.x.Estado,
                    Precio = qry.x.x.Precio,
                    Float = qry.x.x.Float,
                    dTipo = qry.x.y.Descripcion,
                    dEstado = qry.h.Descripcion,
                    dirFoto = qry.x.x.Foto
                });
            }

            ModeloAdministrador adm = new ModeloAdministrador
            {
                lstA = armas,
                lstal = alm,
                lstMA = lstMA
            };
            return View("Administrador",adm);
        }

        [HttpGet]
        public  IActionResult Eliminar(int id)
        {  
           
           Arma arma = _dbcontext.Armas.Find(id);
           try
            {
                _dbcontext.Armas.Remove(arma);
                _dbcontext.SaveChanges();
            } 
            catch(Exception e)
            {

            }
            return ArmaModificar();          
        }




        public IActionResult Tienda()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Tienda(ModeloTienda mt)
        {
            if (ModelState.IsValid)
            {
                Usuario user = _dbcontext.Usuarios.Find(mt.usuario.idUsuario);


                user = new Usuario()
                {
                    Nombre = mt.usuario.Nombre,
                    Correo = mt.usuario.Correo,
                    Contrasena = mt.usuario.Contrasena,
                    Fondos = mt.usuario.Fondos

                };


                Usuario us = new Usuario()
                {
                    IdUsuario = mt.usuario.idUsuario,
                    Nombre = mt.usuario.Nombre,
                    Correo = mt.usuario.Correo,
                    Contrasena = mt.usuario.Contrasena,
                    Fondos = mt.usuario.Fondos,
                    Rol = 2
                };

                try
                {
                    _dbcontext.Usuarios.Update(user);
                    _dbcontext.SaveChanges();
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                    
                }
                return RedirectToAction("Tienda", mt);
            }


            return View();
        }



        public IActionResult ArmaRegistro()
        {
            List<Tipo> lsT = _dbcontext.Tipos.OrderBy(x => x.IdTipo).ToList();
            List<Estado> lstE = _dbcontext.Estados.OrderBy(x => x.IdEstado).ToList();

            ModeloArma avm = new ModeloArma
            {
                lsT = lsT,
                lstE = lstE
            };

            return View(avm);
        }

        [HttpPost]
        public async Task<IActionResult> ArmaRegistro(ModeloArma avm)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                string nombreArchivo = Path.GetFileNameWithoutExtension(avm.Foto.FileName);
                string extensionArchivo = Path.GetExtension(avm.Foto.FileName);
                nombreArchivo = nombreArchivo + DateTime.Now.ToString("yymmssfff") + extensionArchivo;

                string path = Path.Combine(wwwRootPath + "/img/imagenesSubidas", nombreArchivo);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await avm.Foto.CopyToAsync(fileStream);
                }


                Arma a = new Arma
                {
                    Nombre = avm.Nombre,
                    Tipo = avm.Tipo,
                    Estado = avm.Estado,
                    Precio = avm.Precio,
                    Float = avm.Float,
                    Foto = "/img/imagenesSubidas/" + nombreArchivo,
                    Estatus = false
                };

                try
                {
                    _dbcontext.Armas.Add(a);
                    _dbcontext.SaveChanges();
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
                return RedirectToAction("ArmaRegistro");
            }


            return View(avm);
        }

        public IActionResult UsuarioRegistro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UsuarioRegistro(ModeloUsuario rvm)
        {
            if (ModelState.IsValid)
            {
                Usuario u = new Usuario
                {
                    Nombre = rvm.Nombre,
                    Correo = rvm.Correo,
                    Contrasena = rvm.Contrasena,
                    Fondos = 0,
                    Rol = 2
                };

                try
                {
                    _dbcontext.Usuarios.Add(u);
                    _dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
                return RedirectToAction("Login");
            }


            return View(rvm);
        }
    
        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
