using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Collections.Data
{
    public class ItemEntity
    {
        [Key, Column(Order = 0)]
        public int GroupId { get; set; }
        [Key, Column(Order = 1)]
        public int ItemId { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
