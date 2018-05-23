using System.ComponentModel.DataAnnotations;

namespace Upload.ViewModel
{
    public class Presign 
    {
        [Required]
        public string FileName { get; set; }
    }
}