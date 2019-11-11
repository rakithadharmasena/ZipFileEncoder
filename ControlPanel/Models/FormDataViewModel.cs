using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

//view model used to bind to the main form of control panel
namespace ControlPanel.ViewModels
{
    public class FormDataViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
