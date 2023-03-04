using Microsoft.AspNetCore.Mvc;
using L01_2020ML603.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace L01_2020ML603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristasController : ControllerBase
    {
        private readonly pedidosContext _motoristaContext;

        public MotoristasController(pedidosContext motoristasContext)
        {
            this._motoristaContext = motoristasContext;
        }

        [HttpGet]
        [Route("getall_motoristas")]
        public IActionResult ObtenerMotoristas()
        {
            List<motoristas> listadoMotoristas = (from e in _motoristaContext.motoristas
                                          select e).ToList();

            if (listadoMotoristas.Count == 0) { return NotFound(); }

            return Ok(listadoMotoristas);
        }


        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult get(int id)
        {

            motoristas? unMotorista = (from e in _motoristaContext.motoristas
                               where e.motoristaId == id
                               select e).FirstOrDefault();

            if (unMotorista == null)
            { return NotFound(); }

            return Ok(unMotorista);
        }


        [HttpGet]
        [Route("find_motoristas")]

        public IActionResult buscar(string filtro)
        {
            List<motoristas> motoristasList = (from e in _motoristaContext.motoristas
                                         where (e.nombreMotorista.Contains(filtro))
                                         select e).ToList();
  

            if (motoristasList.Any())
            {
                return Ok(motoristasList);
            }

            return NotFound();
        }



        [HttpPost]
        [Route("add_motoristas")]
        public IActionResult Crear([FromBody] motoristas motoristaNuevo)
        {
            try
            {

                _motoristaContext.motoristas.Add(motoristaNuevo);
                _motoristaContext.SaveChanges();

                return Ok(motoristaNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        [Route("actualizar_motorista/{id}")]

        public IActionResult actualizarMotorista(int id, [FromBody] motoristas motoristaModificar)
        {
            motoristas? motoristasExiste = (from e in _motoristaContext.motoristas
                                   where e.motoristaId == id
                                   select e).FirstOrDefault();

            if (motoristasExiste == null)
                return NotFound();

            motoristasExiste.nombreMotorista = motoristaModificar.nombreMotorista;

            _motoristaContext.Entry(motoristasExiste).State = EntityState.Modified;
            _motoristaContext.SaveChanges();

            return Ok(motoristasExiste);

        }


        [HttpDelete]
        [Route("delete_motorista/{id}")]

        public IActionResult eliminarPlatos(int id)
        {
            motoristas? motoristaExiste = (from e in _motoristaContext.motoristas
                                   where e.motoristaId == id
                                   select e).FirstOrDefault();

            if (motoristaExiste == null)
                return NotFound();

            _motoristaContext.motoristas.Attach(motoristaExiste);
            _motoristaContext.motoristas.Remove(motoristaExiste);
            _motoristaContext.SaveChanges();

            return Ok();
        }




    }
}
