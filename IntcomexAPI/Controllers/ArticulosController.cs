using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class ArticulosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    // Constructor que recibe el contexto de la base de datos.
    public ArticulosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Metodo que retorna un listado de articulos basado en la categoria o subcategoria, junto con la paginacion.
    [Authorize]  // nos aseguramos que solo los usuarios autenticados puedan acceder a este metodo.
    [HttpPost("listado")]
    public IActionResult GetArticulos([FromBody] ArticulosRequestModel request)
    {
        try
        {

            if (request == null || string.IsNullOrEmpty(request.Categoria) || request.Cantidad <= 0 || request.Pagina <= 0)
            {
                return BadRequest("Request inválido. Asegurate de que todos los parámetros son correctos.");
            }

            // calculamos cuantos elementos se deben omitir para la paginacion.
            var skip = (request.Pagina - 1) * request.Cantidad;

            // consulta base que obtiene todos los articulos.
            var query = _context.Articulos.AsQueryable();

            // Filtramos los articulos
            var subcategorias = _context.Subcategorias
                .Where(s => s.NombreSubcategoria.ToLower() == request.Categoria.ToLower())
                .Select(s => s.IdSubcategoria)
                .ToList();

            if (subcategorias.Any())
            {
                // Buscamos con las subcategoraas encontradas.
                var articulosId = _context.ArticulosSubcategorias
                     .Where(asc => subcategorias.Contains(asc.IdSubcategoria))
                     .Select(asc => asc.IdArticulo)
                     .ToList();

                if (articulosId.Any())
                {
                    query = query.Where(a => articulosId.Contains(a.IdArticulo));
                }
                else
                {
                    return NotFound("No se encontraron articulospara la subcategoria proporcionada.");
                }
            }
            else
            {
                // Buscamos por categoria si no se encontraron subcategorias.
                var categoria = _context.Categorias
                    .FirstOrDefault(c => c.NombreCategoria.ToLower() == request.Categoria.ToLower());


                if (categoria != null)
                {
                    // Buscamos con la categoria encontrada.
                    var articulosId = _context.ArticulosCategorias
                        .Where(ac => ac.IdCategoria == categoria.IdCategoria)
                        .Select(ac => ac.IdArticulo)
                        .ToList();

                    if (articulosId.Any())
                    {
                        query = query.Where(a => articulosId.Contains(a.IdArticulo));
                    }
                    else
                    {
                        return NotFound("No se encontraron articulos para la categoria proporcionada.");
                    }
                }
                else
                {
                    return NotFound("No se encontraron articulos ni en subcategoria ni en categoria.");
                }


            }

            // total de articulos que cumplen con los criterios.
            var totalItems = query.Count();

            // total de paginas basandose en la cantidad de articulos por pagina.
            var totalPages = (int)Math.Ceiling(totalItems / (double)request.Cantidad);

            // articulos especificos para la pagina solicitada.
            var articulos = query.Skip(skip).Take(request.Cantidad)
                .Select(a => new ArticuloDto
                {
                    IdArticulo = a.IdArticulo,
                    SKU = a.Sku,
                    Mpn = a.Mpn,
                    Nombre = a.Nombre
                }).ToList();

            // objeto PaginacionDto para encapsular la respuesta.
            var resultado = new PaginacionDto<ArticuloDto>
            {
                TotalArticulos = totalItems,
                PaginaActual = request.Pagina,
                TotalPaginas = totalPages,
                Articulos = articulos
            };

            // Retorna en formato JSON.
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}