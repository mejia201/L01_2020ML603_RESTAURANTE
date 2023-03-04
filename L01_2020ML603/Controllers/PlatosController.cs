using L01_2020ML603.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace L01_2020ML603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
       private readonly pedidosContext _platosContext;


        public PlatosController(pedidosContext platosContext)
        {
            this._platosContext = platosContext;
        }

        [HttpGet]
        [Route("getall_platos")]
        public IActionResult ObtenerPlatos()
        {
            List<platos> listadoPlatos = (from e in _platosContext.platos
                                            select e).ToList();

            if (listadoPlatos.Count == 0) { return NotFound(); }

            return Ok(listadoPlatos);
        }


        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult get(int id)
        {

            platos? unPlato = (from e in _platosContext.platos
                                 where e.platoId == id
                                 select e).FirstOrDefault();

            if (unPlato == null)
            { return NotFound(); }

            return Ok(unPlato);
        }

        [HttpGet]
        [Route("find_byPrecio")]

        public IActionResult buscar_por_Precio(decimal filtro)
        {
            List<platos> platosList = (from e in _platosContext.platos
                                         where (e.precio < filtro)
                                         select e).ToList();


            if (platosList.Any())
            {
                return Ok(platosList);
            }

            return NotFound();
        }


        [HttpPost]
        [Route("add_platos")]
        public IActionResult Crear([FromBody] platos platoNuevo)
        {
            try
            {
                
                _platosContext.platos.Add(platoNuevo);
                _platosContext.SaveChanges();

                return Ok(platoNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("actualizar_plato/{id}")]

        public IActionResult actualizarPlato(int id, [FromBody] platos platoModificar)
        {
            platos? platoExiste = (from e in _platosContext.platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();

            if (platoExiste == null)
                return NotFound();

            platoExiste.nombrePlato = platoModificar.nombrePlato;
            platoExiste.precio = platoModificar.precio;

            _platosContext.Entry(platoExiste).State = EntityState.Modified;
            _platosContext.SaveChanges();

            return Ok(platoExiste);

        }


        [HttpDelete]
        [Route("delete_plato/{id}")]

        public IActionResult eliminarPlatos(int id)
        {
            platos? platoExiste = (from e in _platosContext.platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();

            if (platoExiste == null)
                return NotFound();

            _platosContext.platos.Attach(platoExiste);
            _platosContext.platos.Remove(platoExiste);
            _platosContext.SaveChanges();

            return Ok();
        }
    }
}
