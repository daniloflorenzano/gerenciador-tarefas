using System.ComponentModel;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.Enums;

namespace TaskManager.Models.Models;

[ModelMetadataType(typeof(TopicMetadata))]
public partial class Topic
{
    // public TopicColorOptionsEnum ColorAsEnum
    // {
    //     get => (TopicColorOptionsEnum)Enum.Parse(typeof(TopicColorOptionsEnum), Color);
    //     set => Color = value.ToString();
    // }

    public string GetColorCode()
    {
        if (!Enum.TryParse<TopicColorOptionsEnum>(Color, out var color))
            throw new Exception("Código da cor não catalogado.");

        return color switch
        {
            TopicColorOptionsEnum.Azul => ColorTranslator.ToHtml(System.Drawing.Color.Blue),
            TopicColorOptionsEnum.Verde => ColorTranslator.ToHtml(System.Drawing.Color.Green),
            TopicColorOptionsEnum.Vermelho => ColorTranslator.ToHtml(System.Drawing.Color.Red),
            TopicColorOptionsEnum.Amarelo => ColorTranslator.ToHtml(System.Drawing.Color.Yellow),
            TopicColorOptionsEnum.Roxo => ColorTranslator.ToHtml(System.Drawing.Color.Purple),
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