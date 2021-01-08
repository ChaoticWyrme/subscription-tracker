using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SubWatchApi.Models
{
    public class Subscription
    {
        //[Key]
        public string Username { get; set; }

        //[Key]
        public long ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual ServiceInfo Service { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
