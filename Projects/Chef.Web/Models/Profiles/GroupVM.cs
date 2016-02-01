using Chef.Collections;
using Chef.Profiles;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Chef.Web.Models.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupVM
    {
        /// <summary>
        /// A list of users in this group. TODO: should this be a list of ProfileVM instead of UserVM?
        /// </summary>
        public List<UserVM> Users { get; set; }
        /// <summary>
        /// The user this group belongs to, having an UserVM object so we can 
        /// display the basic info of this user on top of this collection page.
        /// </summary>
        public UserVM Owner { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsPublic { get; set; }
        /// <summary>
        /// Number shown next to each group of the number of users it has.
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// Used only when user tries to add/remove another user
        /// in the AddMe modal dialog.
        /// </summary>
        public bool TargetExists { get; set; }

        /// <summary>
        /// Url to this group.
        /// </summary>
        public string AbsoluteLink
        {
            get
            {
                // TODO: update route url
                return UrlHelper.RouteUrl("CollectionsUsers", new { userName = Owner.UserName, groupSlug = Slug });
            }
        }

        /// <summary>
        /// Maps a Group<Profile> object to GroupVM and also maps the Items which
        /// is List<Profile> inside Group<Profile> to List<UserVM>.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static GroupVM Map(Group<Profile> group)
        {
            var groupVM = new GroupVM();

            // fill users if there is any
            if (group.Items != null && group.Items.Count >0)
            {
                groupVM.Users = new List<UserVM>();

                foreach (var profile in group.Items)
                {
                    groupVM.Users.Add(UserVM.Map(profile));
                }
            }

            groupVM.Owner = UserVM.Map(group.Profile);
            groupVM.Id = group.Id;
            groupVM.Name = group.Name;
            groupVM.Slug = group.Slug;
            groupVM.IsPublic = group.IsPublic;
            groupVM.UserCount = group.ItemCount;
            groupVM.TargetExists = group.TargetExists;

            return groupVM;
        }


        protected UrlHelper UrlHelper
        {
            get { return new UrlHelper(HttpContext.Current.Request.RequestContext); }
        }

    }
}