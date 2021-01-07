using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubWatchApi.Models
{
    public class ServiceInfo
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }
        
        public string Url { get; set; }

        public string IconUrl { get; set; }
    }
}