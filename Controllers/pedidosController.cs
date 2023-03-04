using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L012020MR602.Models;
using Microsoft.EntityFrameworkCore;

namespace L012020MR602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public pedidosController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get() 
        {
            List<pedidos>listadoPedidos = (from p in _restauranteContexto.pedidos select p).ToList();
            if (listadoPedidos.Count()==0)
            {
                return NotFound();

            }
            return Ok(listadoPedidos);
        }

       

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPedido([FromBody] pedidos pedidos) 
        {
            try
            {
                _restauranteContexto.pedidos.Add(pedidos);
                _restauranteContexto.SaveChanges();
                return Ok(pedidos);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidosModificar) 
        {
            pedidos? pedidosActual = (from p in _restauranteContexto.pedidos where p.pedidoId== id select p).FirstOrDefault();

            if (pedidosActual == null) { return NotFound(); }
            pedidosActual.motoristaId = pedidosModificar.motoristaId;
            pedidosActual.clienteId = pedidosModificar.clienteId;
            pedidosActual.platoId = pedidosModificar.platoId;
            pedidosActual.cantidad = pedidosModificar.cantidad;
            pedidosActual.precio = pedidosModificar.precio;

            _restauranteContexto.Entry(pedidosActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(pedidosModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarPedido(int id) 
        {
            pedidos? pedidos = (from p in _restauranteContexto.pedidos where p.pedidoId== id select p).FirstOrDefault();

            if (pedidos == null) 
            {
                return NotFound();
            }
            _restauranteContexto.pedidos.Attach(pedidos);
            _restauranteContexto.pedidos.Remove(pedidos);
            _restauranteContexto.SaveChanges();

            return Ok(pedidos);
        }

        [HttpGet]
        [Route("ObtenerInfo/{clienteId}/{motoristaId}")]
        public IActionResult listadodePedidos (int clienteId, int motoristaId)
        {
           
                List<pedidos> info = (from p in _restauranteContexto.pedidos where p.motoristaId == motoristaId && p.clienteId == clienteId select p).ToList();
                if (info == null) return NotFound();
            
            
           
                return Ok(info);
            
        }
    }
}
