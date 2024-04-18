namespace TaskManager.Models.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string TextContent { get; set; } = null!;

    public int UserId { get; set; }

    public int TopicId { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Topic Topic { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
