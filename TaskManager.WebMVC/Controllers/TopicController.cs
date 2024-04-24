using Microsoft.AspNetCore.Mvc;
using TaskManager.Core;
using TaskManager.Core.Data;
using TaskManager.Core.Data.Repositories;
using TaskManager.Core.Enums;
using TaskManager.Core.Models;

namespace TaskMaganer.Controllers
{
    public class TopicController : Controller
    {
        private readonly TopicRepository _topicRepository;

        public TopicController(TaskManagerContext context)
        {
            _topicRepository = new TopicRepository(context);
        }

        // GET: Topic
        public async Task<IActionResult> Index()
        {
            return View(await _topicRepository.ListAllAsync());
        }

        // GET: Topic/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var topic = await _topicRepository.GetByKeyAsync(id);
            if (topic == null)
                return NotFound();

            return View(topic);
        }

        // GET: Topic/Create
        public IActionResult Create()
        {
            ViewBag.ColorOptions = Enum.GetValues(typeof(TopicColorOptionsEnum)).Cast<TopicColorOptionsEnum>();
            return View();
        }

        // POST: Topic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Color")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                await _topicRepository.CreateAsync(topic);
                return RedirectToAction(nameof(Index));
            }

            return View(topic);
        }

        // GET: Topic/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            
            var topic = await _topicRepository.GetByKeyAsync(id);
            if (topic == null)
                return NotFound();

            return View(topic);
        }

        // POST: Topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Color")] Topic topic)
        {
            if (id != topic.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _topicRepository.UpdateAsync(topic);
                return RedirectToAction(nameof(Index));
            }

            return View(topic);
        }

        // GET: Topic/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            
            var topic = await _topicRepository.GetByKeyAsync(id);
            if (topic == null)
                return NotFound();

            return View(topic);
        }

        // POST: Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topic = await _topicRepository.GetByKeyAsync(id);
            if (topic != null)
                await _topicRepository.DeleteAsync(topic);
            
            return RedirectToAction(nameof(Index));
        }
    }
}