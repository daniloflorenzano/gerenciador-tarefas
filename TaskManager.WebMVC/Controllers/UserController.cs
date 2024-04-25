using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core;
using TaskManager.Core.Models;



namespace TaskMaganer.Controllers
{
    public class UserController : DefaultController
    {
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var db = new TaskManagerContext();

            return View(await db.Users.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var db = new TaskManagerContext();
            if (ModelState.IsValid)
            {
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await db.SaveChangesAsync();
                ViewData["Mensagem"] = "Dados salvos com sucesso.";
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao salvar os dados.";
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var db = new TaskManagerContext();
            var user = await db.Users.FindAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            var db = new TaskManagerContext();
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                ViewData["Mensagem"] = "Dados alterados com sucesso.";
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao alterar os dados.";
            }
            return View(user);
        }

        public async Task<IActionResult> Details(int id)
        {
            var db = new TaskManagerContext();
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var db = new TaskManagerContext();
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(User user)
        {
            var db = new TaskManagerContext();
            db.Entry(user).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
