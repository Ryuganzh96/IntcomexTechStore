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

    // Método que retorna un listado de articulosbasado en la categoría o subcategoría, junto con la paginación.
    [Authorize]  // Este atributo asegura que solo los usuarios autenticados puedan acceder a este método.
    [HttpPost("listado")]
    public IActionResult GetArticulos([FromBody] ArticulosRequestModel request)
    {
        try
        {
            // Validación inicial
            if (request == null || string.IsNullOrEmpty(request.Categoria) || request.Cantidad <= 0 || request.Pagina <= 0)
            {
                return BadRequest("Request inválido. Asegúrate de que todos los parámetros son correctos.");
            }

            // Calcula cuántos elementos se deben omitir para la paginación.
            var skip = (request.Pagina - 1) * request.Cantidad;

            // Prepara una consulta base que obtiene todos los artículos.
            var query = _context.Articulos.AsQueryable();

            // Filtra los articulossegún la subcategoría o categoría proporcionada.
            var subcategorias = _context.Subcategorias
                .Where(s => s.NombreSubcategoria.ToLower() == request.Categoria.ToLower())
                .Select(s => s.IdSubcategoria)
                .ToList();

            if (subcategorias.Any())
            {
                // Buscar articulosrelacionados con las subcategorías encontradas.
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
                     return NotFound("No se encontraron articulospara la subcategoría proporcionada.");
                 }
            }
            else
            {
                // Buscar por categoría si no se encontraron subcategorías.
                var categoria = _context.Categorias
                    .FirstOrDefault(c => c.NombreCategoria.ToLower() == request.Categoria.ToLower());
                

                if (categoria != null)
                {
                    // Buscar articulosrelacionados con la categoría encontrada.
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
                        return NotFound("No se encontraron articulospara la categoría proporcionada.");
                    }
                }
                else
                {
                    return NotFound("No se encontraron articulosni en subcategoría ni en categoría.");
                }

                
            }

            // Calcula el total de articulosque cumplen con los criterios.
            var totalItems = query.Count();

            // Calcula el total de páginas basándose en la cantidad de articulospor página.
            var totalPages = (int)Math.Ceiling(totalItems / (double)request.Cantidad);

            // Selecciona los articulosespecíficos para la página solicitada y los convierte en ArticuloDto.
            var articulos = query.Skip(skip).Take(request.Cantidad)
                .Select(a => new ArticuloDto
                {
                    IdArticulo = a.IdArticulo,
                    SKU = a.Sku,
                    Mpn = a.Mpn,
                    Nombre = a.Nombre
                }).ToList();

            // Crea un objeto PaginacionDto para encapsular la respuesta.
            var resultado = new PaginacionDto<ArticuloDto>
            {
                TotalArticulos = totalItems,
                PaginaActual = request.Pagina,
                TotalPaginas = totalPages,
                Articulos = articulos
            };

            // Retorna el resultado en formato JSON.
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            // Captura y maneja cualquier excepción que pueda ocurrir.
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}