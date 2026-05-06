using FinanzaNova.Web.Data;
using FinanzaNova.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanzaNova.Web.Controllers;

public class CategoriasController(FinanzaNovaDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var categorias = await context.Categorias
            .AsNoTracking()
            .OrderByDescending(c => c.Activa)
            .ThenBy(c => c.Tipo)
            .ThenBy(c => c.Nombre)
            .ToListAsync();

        return View(categorias);
    }

    public IActionResult Create()
    {
        return View(new Categoria());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Categoria categoria)
    {
        if (!ModelState.IsValid)
        {
            return View(categoria);
        }

        context.Add(categoria);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await context.Categorias.FindAsync(id);
        return categoria is null ? NotFound() : View(categoria);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Categoria categoria)
    {
        if (id != categoria.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(categoria);
        }

        context.Update(categoria);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActiva(int id)
    {
        var categoria = await context.Categorias.FindAsync(id);
        if (categoria is null)
        {
            return NotFound();
        }

        categoria.Activa = !categoria.Activa;
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
