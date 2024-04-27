using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Data;
using TaskManager.Core.Models;

namespace TaskMaganer.Controllers
{
    public class CommentController : Controller
    {
        private readonly TaskManagerContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(TaskManagerContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comment
        public async Task<IActionResult> Index()
        {
            var taskManagerContext = _context.Comments.Include(c => c.PreviousComment).Include(c => c.Task)
                .Include(c => c.User);
            return View(await taskManagerContext.ToListAsync());
        }

        // GET: Comment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.PreviousComment)
                .Include(c => c.Task)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comment/Create
        public IActionResult Create(int taskId)
        {
            ViewData["PreviousCommentId"] = new SelectList(_context.Comments, "Id", "TextContent");
            ViewData["UserId"] = new SelectList(_userManager.Users, "Id", "Name");
            ViewData["TaskId"] = taskId.ToString();
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,TextContent,CreatedAt,UpdatedAt,UserId,TaskId,PreviousCommentId")] Comment comment)
        {
            var novoUsuario = _userManager.GetUserAsync(User);

            var user = new User()
            {
                Name = User.Identity.Name,
                Id = 2
            };
            var commenta = new Comment()
            {
                User = user,
                UserId = user.Id,
                TextContent = comment.TextContent,
                CreatedAt = DateTime.Now,
                PreviousCommentId = comment.PreviousCommentId,
                TaskId = comment.TaskId

            };
            //novoUsuario = User.Identity.Name;
            _context.Comments.Add(commenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["PreviousCommentId"] =
                new SelectList(_context.Comments, "Id", "TextContent", comment.PreviousCommentId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "TextContent", comment.TaskId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", comment.UserId);
            return View(comment);
        }

        // GET: Comment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            ViewData["PreviousCommentId"] =
                new SelectList(_context.Comments, "Id", "TextContent", comment.PreviousCommentId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "TextContent", comment.TaskId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", comment.UserId);
            return View(comment);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,TextContent,CreatedAt,UpdatedAt,UserId,TaskId,PreviousCommentId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["PreviousCommentId"] =
                new SelectList(_context.Comments, "Id", "TextContent", comment.PreviousCommentId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "TextContent", comment.TaskId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", comment.UserId);
            return View(comment);
        }

        // GET: Comment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.PreviousComment)
                .Include(c => c.Task)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}