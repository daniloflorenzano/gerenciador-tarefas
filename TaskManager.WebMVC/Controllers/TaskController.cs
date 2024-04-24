using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Core;
using TaskManager.Core.Data;
using TaskManager.Core.Data.Repositories;
using TaskManager.Core.Models;

namespace TaskMaganer.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskRepository _taskRepository;
        private readonly TopicRepository _topicRepository;
        private readonly UserRepository _userRepository;

        public TaskController(TaskManagerContext context)
        {
            _taskRepository = new TaskRepository(context);
            _topicRepository = new TopicRepository(context);
            _userRepository = new UserRepository(context);
        }

        // GET: Task
        public async Task<IActionResult> Index()
        {
            return View(await _taskRepository.ListAllAsync());
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var task = await _taskRepository.GetByKeyAsync(id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        // GET: Task/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TopicId"] = new SelectList(await _topicRepository.ListAllAsync(), "Id", "Name");
            ViewData["UserId"] = new SelectList(await _userRepository.ListAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,TextContent,UserId,TopicId,Status,CreatedAt,UpdatedAt")]
            MTask task)
        {
            var user = await _userRepository.GetByKeyAsync(task.UserId);
            var topic = await _topicRepository.GetByKeyAsync(task.TopicId);

            if (user is null || topic is null)
                return NotFound();

            task.User = user;
            task.Topic = topic;

            try
            {
                await _taskRepository.CreateAsync(task);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewData["TopicId"] = new SelectList(await _topicRepository.ListAllAsync(), "Id", "Name", task.TopicId);
                ViewData["UserId"] = new SelectList(await _userRepository.ListAllAsync(), "Id", "Name", task.UserId);
                return View(task);
            }
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var task = await _taskRepository.GetByKeyAsync(id);
            if (task == null)
                return NotFound();

            ViewData["TopicId"] = new SelectList(await _topicRepository.ListAllAsync(), "Id", "Name", task.TopicId);
            ViewData["UserId"] = new SelectList(await _userRepository.ListAllAsync(), "Id", "Name", task.UserId);
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Title,TextContent,UserId,TopicId,Status,CreatedAt,UpdatedAt")]
            MTask task)
        {
            if (id != task.Id)
                return NotFound();

            var user = await _userRepository.GetByKeyAsync(task.UserId);
            var topic = await _topicRepository.GetByKeyAsync(task.TopicId);

            if (user is null || topic is null)
                return NotFound();

            task.User = user;
            task.Topic = topic;

            try
            {
                var existingTask = await _taskRepository.GetByKeyAsync(task.Id);

                if (existingTask == null)
                {
                    return NotFound();
                }
                
                existingTask.Title = task.Title;
                existingTask.TextContent = task.TextContent;
                existingTask.UserId = task.UserId;
                existingTask.TopicId = task.TopicId;
                existingTask.Status = task.Status; 
                existingTask.UpdatedAt = DateTime.Now; 

                await _taskRepository.UpdateAsync(existingTask);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewData["TopicId"] = new SelectList(await _topicRepository.ListAllAsync(), "Id", "Name", task.TopicId);
                ViewData["UserId"] = new SelectList(await _userRepository.ListAllAsync(), "Id", "Name", task.UserId);
                return View(task);
            }
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var task = await _taskRepository.GetByKeyAsync(id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _taskRepository.GetByKeyAsync(id);
            if (task != null)
                await _taskRepository.DeleteAsync(task);

            return RedirectToAction(nameof(Index));
        }
    }
}