using System.ComponentModel;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Enums;

namespace TaskManager.Core.Models;

[ModelMetadataType(typeof(TopicMetadata))]
public partial class Topic
{
    public string GetColorCode()
    {
        if (!Enum.TryParse<TopicColorOptionsEnum>(Color, out var color))
            throw new Exception("Código da cor não catalogado.");

        return color switch
        {
            TopicColorOptionsEnum.Azul => ColorTranslator.ToHtml(System.Drawing.Color.DodgerBlue),
            TopicColorOptionsEnum.Verde => ColorTranslator.ToHtml(System.Drawing.Color.LawnGreen),
            TopicColorOptionsEnum.Vermelho => ColorTranslator.ToHtml(System.Drawing.Color.IndianRed),
            TopicColorOptionsEnum.Amarelo => ColorTranslator.ToHtml(System.Drawing.Color.Yellow),
            TopicColorOptionsEnum.Roxo => ColorTranslator.ToHtml(System.Drawing.Color.MediumPurple),
            TopicColorOptionsEnum.Laranja => ColorTranslator.ToHtml(System.Drawing.Color.Orange),
            _ => throw new Exception("Código da cor não catalogado.")
        };
    }
}

public class TopicMetadata
{
    [DisplayName("Nome")]
    public string Name { get; set; }
        
    [DisplayName("Cor")]
    public string Color { get; set; }
}