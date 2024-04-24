namespace TaskManager.Core.Models;

public partial class Topic
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Color { get; set; }

    public virtual ICollection<MTask> Tasks { get; set; } = new List<MTask>();
}
