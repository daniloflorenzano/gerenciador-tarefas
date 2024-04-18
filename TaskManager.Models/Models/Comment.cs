namespace TaskManager.Models.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string TextContent { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }

    public int? PreviousCommentId { get; set; }

    public virtual ICollection<Comment> InversePreviousComment { get; set; } = new List<Comment>();

    public virtual Comment? PreviousComment { get; set; }

    public virtual User User { get; set; } = null!;
}
