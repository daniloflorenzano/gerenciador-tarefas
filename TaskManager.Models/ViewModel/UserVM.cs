using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models.Models;
using Task = TaskManager.Models.Models.Task;

namespace TaskManager.Models.ViewModel
{
    public class UserVM
    {
        [Display(Name = "Código do Usuário")]
        public int Id { get; set; }
        
        [Display(Name = "Nome do Usuário")]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "O Tamanho não pode ser menor que 7")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        
        [Display(Name = "Comentários")]
        public List<Comment> comments { get; set; }
        public List<Task> Tasks { get; set; }

        public async static Task<List<UserVM>> GetUserVMs()
        {
            var db = new TaskManagerContext();
            var listaUsers = await db.Users.ToListAsync();
            return new List<UserVM>();
        }
    }
}
