using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chef.Collections.Data
{
    public class GroupEntity
    {
        public GroupEntity()
        {
            this.ItemEntities = new HashSet<ItemEntity>();
        }

        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public GroupType GroupType { get; set; }
        public bool IsPublic { get; set; }

        public virtual ICollection<ItemEntity> ItemEntities { get; private set; }
    }
}
