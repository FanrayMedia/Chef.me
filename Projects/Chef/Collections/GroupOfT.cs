using Chef.Profiles;
using System.Collections.Generic;

namespace Chef.Collections
{
    public class Group<T> where T: ICollectable
    {
        public int Id { get; set; }
        /// <summary>
        /// The profile of the user this group belongs to.
        /// </summary>
        public Profile Profile { get; set; }
        /// <summary>
        /// Group name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Group slug.
        /// </summary>
        public string Slug { get; set; }
        /// <summary>
        /// True if the group is visible to public.
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// Number of items in this group.
        /// </summary>
        /// <remarks>
        /// Though we have Items property but it's null in the case of only returning
        /// group data, so we have a count property here.
        /// </remarks>
        public int ItemCount { get; set; }

        public List<T> Items { get; set; }

        /// <summary>
        /// Whether a target exists in this group, used exclusively by the UI,
        /// it's here to assist transfer this info out.
        /// </summary>
        public bool TargetExists { get; set; }

    }
}
