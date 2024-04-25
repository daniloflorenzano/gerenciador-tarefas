namespace TaskManager.Core.Models;

public partial class MTask
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string TextContent { get; set; } = null!;

    public int UserId { get; set; }

    public int TopicId { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual Topic Topic { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual List<Comment> Comments { get; set; } = new();
}
