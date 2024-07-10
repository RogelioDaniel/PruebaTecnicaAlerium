using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaAlerium.Models;
using PruebaTecnicaAlerium.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
namespace Alerium.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProveedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proveedores.ToListAsync());
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idProveedor,Codigo,RazonSocial,RFC")] Proveedor proveedores)
        {
            if (ModelState.IsValid)
            {
                var existingSupplier = await _context.Proveedores
                    .FirstOrDefaultAsync(s => s.Codigo == proveedores.Codigo);
                if (existingSupplier != null)
                {  ModelState.AddModelError("Codigo", "Ya existe un producto con este código.");
                    TempData["ErrorMessage"] = "Error: Ya existe un producto con este código.";
                    ModelState.AddModelError("Codigo", "Ya existe un proveedor con este código.");
                    TempData["ErrorMessage"] = "Error: Ya existe un proveedor con este código.";
                    return View(proveedores);
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(proveedores.RFC, @"^[A-Z]{4}\d{6}[A-Z]\d{2}$"))
                {
                    ModelState.AddModelError("RFC", "El RFC debe tener el formato de 13 caracteres (4 letras + 6 dígitos + 1 letra + 2 dígitos).");
                    TempData["ErrorMessage"] = "Error: El RFC debe tener el formato correcto.";
                    return View(proveedores);
                }

                _context.Add(proveedores);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Proveedor creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Error: No se pudo crear el proveedor.";
            return View(proveedores);
        }

        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await _context.Proveedores.FindAsync(id);
            if (proveedores == null)
            {
                return NotFound();
            }
            return View(proveedores);
        }
        // POST: Proveedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idProveedor,Codigo,RazonSocial,RFC")] Proveedor proveedores)
        {
            if (id != proveedores.idProveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Validar el RFC usando expresión regular
                    if (!System.Text.RegularExpressions.Regex.IsMatch(proveedores.RFC, @"^[A-Z]{4}\d{6}[A-Z]\d{2}$"))
                    {
                        ModelState.AddModelError("RFC", "El RFC debe tener el formato de 13 caracteres (4 letras + 6 dígitos + 1 letra + 2 dígitos).");
                        TempData["ErrorMessage"] = "Error: El RFC debe tener el formato correcto.";
                        return View(proveedores);
                    }

                    _context.Update(proveedores);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Proveedor actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedores.idProveedor))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(proveedores);
        }


        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await _context.Proveedores
                .Include(s => s.Productos)
                .FirstOrDefaultAsync(m => m.idProveedor == id);
            if (proveedores == null)
            {
                return NotFound();
            }

            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, bool confirm = false)
        {
            var proveedores = await _context.Proveedores
                .Include(s => s.Productos)
                .FirstOrDefaultAsync(m => m.idProveedor == id);
            if (proveedores == null)
            {
                return NotFound();
            }

            if (proveedores.Productos.Any() && !confirm)
            {
                TempData["ConfirmDelete"] = true;
                _context.Proveedores.Remove(proveedores);
                TempData["SuccessMessage"] = "Correcto: Se elimino el proveedor con codigo: " + proveedores.Codigo;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            _context.Proveedores.Remove(proveedores);
            TempData["SuccessMessage"] = "Correcto: Se elimino el proveedor con codigo: " + proveedores.Codigo;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.idProveedor == id);
        }

        // GET: Productos/Create
        public IActionResult CreateProduct(int idProveedor)
        {
            var producto = new Producto { IdProveedor = idProveedor };
            return View(producto);
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([Bind("IdProducto,IdProveedor,Codigo,Descripcion,Unidad,Costo")] Producto product)
        {
            var existingSupplier = await _context.Productos
                   .FirstOrDefaultAsync(s => s.Codigo == product.Codigo);
            if (ModelState.IsValid)
            {
                if (existingSupplier != null)
                {
                    ModelState.AddModelError("Codigo", "Ya existe un producto con este código.");
                    TempData["ErrorMessage"] = "Error: Ya existe un producto con este código.";
                    return View(product);
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Correcto: Se creó el producto.";
                return RedirectToAction("Details", "Proveedores", new { id = product.IdProveedor });

            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "idProveedor", "RazonSocial", product.IdProveedor);
            return View(product);
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var proveedor = await _context.Proveedores
                .Include(p => p.Productos)
                .FirstOrDefaultAsync(m => m.idProveedor == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }
    }
}
