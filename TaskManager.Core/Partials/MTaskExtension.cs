using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Enums;

namespace TaskManager.Core.Models;

[ModelMetadataType(typeof(MTaskMetadata))]
public partial class MTask
{
    public string GetStatusNameByCode(int code)
    {
        var taskAsEnum = (TaskStatusEnum)code;
        return taskAsEnum switch
        {
            TaskStatusEnum.Todo => "A Fazer",
            TaskStatusEnum.InProgress => "Em Andamento",
            TaskStatusEnum.Done => "Concluída",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public class MTaskMetadata
{
    [DisplayName("Título")]
    public string Title { get; set; }

    [DisplayName("Descrição")]
    public string TextContent { get; set; }
    
    [DisplayName("Criado em")]
    public DateTime CreatedAt { get; set; }
    
    [DisplayName("Alterado em")]
    public DateTime? UpdatedAt { get; set; }
    
    [DisplayName("Tópico")]
    public Topic Topic { get; set; }
    
    [DisplayName("Responsável")]
    public User User { get; set; }
}