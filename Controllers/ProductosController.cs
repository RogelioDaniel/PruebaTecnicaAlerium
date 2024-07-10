using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaAlerium.Models;
using PruebaTecnicaAlerium.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace PruebaTecnicaAlerium.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos/Create
        public IActionResult Create(int idProveedor)
        {
            ViewData["IdProveedores"] = new SelectList(_context.Proveedores, "Id", "Nombre");
            return View(new Producto { IdProveedor = idProveedor });
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,idProveedor,Codigo,Descripcion,Unidad,Costo")] Producto product)
        {
            if (ModelState.IsValid)
            {
                var existingSupplier = await _context.Productos
                   .FirstOrDefaultAsync(s => s.Codigo == product.Codigo);
                if (existingSupplier != null)
                {
                    ModelState.AddModelError("Codigo", "Ya existe un producto con este código.");
                    TempData["ErrorMessage"] = "Error: Ya existe un producto con este código.";
                    return View(product);
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Proveedores", new { id = product.IdProveedor });
            }
            ViewData["idProveedor"] = new SelectList(_context.Proveedores, "idProveedor", "RazonSocial", product.IdProveedor);
            return View(product);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Productos
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }

}
