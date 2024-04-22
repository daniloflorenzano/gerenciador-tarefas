using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Models.Models;

[ModelMetadataType(typeof(TopicMetadata))]
public partial class Topic;

public class TopicMetadata
{
    [DisplayName("Nome")]
    public string Name { get; set; }
        
    [DisplayName("Cor")]
    public string Color { get; set; }
}