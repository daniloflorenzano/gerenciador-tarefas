using Microsoft.AspNetCore.Mvc;
using TaskManager.Core;
using TaskManager.Core.Data.Repositories;
using TaskManager.Core.Models;

namespace TaskMaganer.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserController(TaskManagerContext context)
        {
            _userRepository = new UserRepository(context);
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.ListAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.CreateAsync(user);
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
            var user = await _userRepository.GetByKeyAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.UpdateAsync(user);
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
            var user = await _userRepository.GetByKeyAsync(id);
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetByKeyAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(User user)
        {
            await _userRepository.DeleteAsync(user);
            return RedirectToAction("Index");
        }

    }
}
