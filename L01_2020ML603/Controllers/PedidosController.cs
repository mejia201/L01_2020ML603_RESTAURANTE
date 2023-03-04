using L01_2020ML603.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace L01_2020ML603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly pedidosContext _pedidosContext;


        public PedidosController(pedidosContext pedidosContext)
        {
            this._pedidosContext = pedidosContext;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerPedidos()
        {
            List<pedidos> listadoPedidos = (from e in _pedidosContext.pedidos
                                           select e).ToList();

            if (listadoPedidos.Count == 0) { return NotFound(); }

            return Ok(listadoPedidos);
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult get(int id)
        {
            
            pedidos? unPedido = (from e in _pedidosContext.pedidos
                                 where e.pedidoId == id 
                                 select e).FirstOrDefault();

            if (unPedido == null)
            { return NotFound(); }

            return Ok(unPedido);
        }


        [HttpGet]
        [Route("find_byIDcliente")]

        public IActionResult buscar_por_IDcliente(int filtro)
        {
            List<pedidos> pedidosList = (from e in _pedidosContext.pedidos
                                         where (e.clienteId == filtro)
                                         select e).ToList();

  
            if (pedidosList.Any())
            {
                return Ok(pedidosList);
            }

            return NotFound();
        }


        [HttpGet]
        [Route("find_IDMotorista")]

        public IActionResult buscar_por_IDmotorista(int filtro)
        {
            List<pedidos> pedidosList = (from e in _pedidosContext.pedidos
                                         where (e.motoristaId == filtro)
                                         select e).ToList();


            if (pedidosList.Any())
            {
                return Ok(pedidosList);
            }

            return NotFound();
        }


        [HttpPost]
        [Route("add_pedidos")]
        public IActionResult Crear([FromBody] pedidos pedidoNuevo)
        {
            try
            {
                
                _pedidosContext.pedidos.Add(pedidoNuevo);
                _pedidosContext.SaveChanges();

                return Ok(pedidoNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        [Route("actualizar_pedidos/{id}")]

        public IActionResult actualizarPedido(int id, [FromBody] pedidos pedidoModificar)
        {
            pedidos? pedidoExiste = (from e in _pedidosContext.pedidos
                                     where e.pedidoId == id
                                     select e).FirstOrDefault();

            if (pedidoExiste == null)
                return NotFound();

            pedidoExiste.motoristaId = pedidoModificar.motoristaId;
            pedidoExiste.clienteId = pedidoModificar.clienteId;
            pedidoExiste.platoId = pedidoModificar.platoId;
            pedidoExiste.cantidad = pedidoModificar.cantidad;
            pedidoExiste.precio = pedidoModificar.precio;

            _pedidosContext.Entry(pedidoExiste).State = EntityState.Modified;
            _pedidosContext.SaveChanges();

            return Ok(pedidoExiste);

        }



        [HttpDelete]
        [Route("delete_pedido/{id}")]

        public IActionResult eliminarPedido(int id)
        {
            pedidos? pedidoExiste = (from e in _pedidosContext.pedidos
                                     where e.pedidoId == id
                                     select e).FirstOrDefault();

            if (pedidoExiste == null)
                return NotFound();

            _pedidosContext.pedidos.Attach(pedidoExiste);
            _pedidosContext.pedidos.Remove(pedidoExiste);
            _pedidosContext.SaveChanges();

            return Ok();
        }

    }
}
