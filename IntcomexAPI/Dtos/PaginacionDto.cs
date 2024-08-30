public class PaginacionDto<T>
{
    public int TotalArticulos { get; set; }
    public int PaginaActual { get; set; }
    public int TotalPaginas { get; set; }
    public required List<T> Articulos { get; set; }
}
