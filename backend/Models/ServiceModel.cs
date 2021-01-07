using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubWatchApi.Models
{
    public class ServiceInfo
    {
        [Key]
        public string Name { get; set; }
        public float Price { get; set; }

        public string Url { get; set; }

        public string IconUrl { get; set; }
    }
}