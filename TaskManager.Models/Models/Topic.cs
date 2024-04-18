namespace TaskManager.Models.Models;

public partial class Topic
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Color { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
