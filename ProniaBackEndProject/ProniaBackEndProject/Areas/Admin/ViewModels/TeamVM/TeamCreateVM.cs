﻿using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.TeamVM
{
    public class TeamCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }


        [Required]
        public IFormFile Photo { get; set; }
    }
}
