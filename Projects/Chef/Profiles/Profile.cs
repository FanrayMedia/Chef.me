using Chef.Collections;
using System;
using Chef.Accounts;

namespace Chef.Profiles
{
    public class Profile : ICollectable
    {
        /// <summary>
        /// Init default values.
        /// </summary>
        public Profile()
        {
            BgImg = "A01.jpg";
            LastSeenOn = DateTime.UtcNow;
            JoinedOn = DateTime.UtcNow;
            AllowEmailMe = true;
            AccountStatus = EAccountStatus.Approved;
        }

        public virtual ProfileStyle ProfileStyle { get; set; }        


        // ------------------------------------------------ name/id
        /// <summary>
        /// Primary key
        /// </summary>
        //[Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// First Name
        /// </summary>
        /// <remarks>
        /// http://stackoverflow.com/questions/20958/list-of-standard-lengths-for-database-fields
        /// </remarks>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Background image filename with extension, default: A01.jpg.
        /// Only JPG, GIF and PNG allowed.
        /// </summary>
        public string BgImg { get; set; }

        // ------------------------------------------------ about

        public string Headline { get; set; }
        /// <summary>
        /// Bio, the easy way out is to have an about for now.
        /// </summary>
        public string Bio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Locations { get; set; }
        public bool EnableGravatar { get; set; }
        /// <summary>
        /// Email is always private, but user can choose to be emailed with Email Me button,
        /// so people can email them through our system.
        /// </summary>
        public bool AllowEmailMe { get; set; }

        // ------------------------------------------------ 

        /// <summary>
        /// Tells if an account is banned, visible to admin.
        /// </summary>
        public EAccountStatus AccountStatus { get; set; }
        /// <summary>
        /// Updated only through the <see cref="Chef.Account.AccountManager.GetUser"/> method.
        /// </summary>
        public DateTime? LastSeenOn { get; set; }
        /// <summary>
        /// Updated once at registration
        /// </summary>
        public DateTime? JoinedOn { get; set; }
        /// <summary>
        /// Updated only through the <see cref="Chef.Account.AccountManager.GetUser"/> method.
        /// </summary>
        public int ViewCount { get; set; }
       
    }
}