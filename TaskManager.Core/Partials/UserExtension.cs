﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models
{
    [ModelMetadataType(typeof(MD_User))]
    public partial class User
    {
        class MD_User
        {
            [Display(Name = "Nome")]
            public string Name { get; set; }

            [Display(Name ="Criado Em")]
            public DateTime CreatedAt { get; set; } = DateTime.Now;

            [Display(Name = "Alterado Em")]
            public DateTime? UpdatedAt { get; set; }
        }
    }

    
}