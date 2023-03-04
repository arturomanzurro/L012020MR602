using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L012020MR602.Models;
using Microsoft.EntityFrameworkCore;

namespace L012020MR602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientesController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public clientesController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<clientes> listadoClientes = (from c in _restauranteContexto.clientes select c).ToList();
            if (listadoClientes.Count() == 0)
            {
                return NotFound();

            }
            return Ok(listadoClientes);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarclientes([FromBody] clientes clientes)
        {
            try
            {
                _restauranteContexto.clientes.Add(clientes);
                _restauranteContexto.SaveChanges();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarClientes(int id, [FromBody] clientes clientesModificar)
        {
            clientes? clientesActual = (from c in _restauranteContexto.clientes where c.clienteId == id select c).FirstOrDefault();

            if (clientesActual == null) { return NotFound(); }
            clientesActual.nombreCliente = clientesActual.nombreCliente;
            clientesActual.direccion = clientesModificar.direccion;
           

            _restauranteContexto.Entry(clientesActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(clientesModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarCliente(int id)
        {
            clientes? clientes = (from c in _restauranteContexto.clientes where c.clienteId == id select c).FirstOrDefault();

            if (clientes == null)
            {
                return NotFound();
            }
            _restauranteContexto.clientes.Attach(clientes);
            _restauranteContexto.clientes.Remove(clientes);
            _restauranteContexto.SaveChanges();

            return Ok(clientes);
        }

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult EncontrarporInicial(string filtro)
        {
            List<clientes> clientes = (from c in _restauranteContexto.clientes where c.direccion.Contains(filtro) select c).ToList();
            if (clientes == null)
            {
                return NotFound();
            }
            return Ok(clientes);
        }

    }
}
