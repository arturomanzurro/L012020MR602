using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L012020MR602.Models;
using Microsoft.EntityFrameworkCore;

namespace L012020MR602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public platosController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<platos> listadoPlatos = (from pl in _restauranteContexto.platos select pl).ToList();
            if (listadoPlatos.Count() == 0)
            {
                return NotFound();

            }
            return Ok(listadoPlatos);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPlatos([FromBody] platos platos)
        {
            try
            {
                _restauranteContexto.platos.Add(platos);
                _restauranteContexto.SaveChanges();
                return Ok(platos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPlato(int id, [FromBody] platos platosModificar)
        {
            platos? platosActual = (from pl in _restauranteContexto.platos where pl.platoId== id select pl).FirstOrDefault();

            if (platosActual == null) { return NotFound(); }
            platosActual.platoId= platosModificar.platoId;
            platosActual.nombrePlato = platosModificar.nombrePlato;
            platosActual.precio = platosModificar.precio;

            _restauranteContexto.Entry(platosActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(platosModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarPlatos(int id)
        {
            platos? platos = (from pl in _restauranteContexto.platos where pl.platoId == id select pl).FirstOrDefault();

            if (platos == null)
            {
                return NotFound();
            }
            _restauranteContexto.platos.Attach(platos);
            _restauranteContexto.platos.Remove(platos);
            _restauranteContexto.SaveChanges();

            return Ok(platos);
        }

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult EncontrarporInicial (string filtro) 
        {
            List<platos> platos = (from pl in _restauranteContexto.platos where pl.nombrePlato.Contains(filtro) select pl).ToList();
            if (platos == null) 
            {
                return NotFound();
            }
            return Ok(platos);
        }
    }
}
