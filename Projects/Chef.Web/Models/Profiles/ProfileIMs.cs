using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chef.Web.Models.Profiles
{
    #region UpdateBgImgIM

    /// <summary>
    /// The authenticated user is creating a new group.
    /// </summary>
    public class UpdateBgImgIM
    {
        /// <summary>
        /// The new group the logged-on user is creating.
        /// </summary>
        public string ImgFileName { get; set; }
    }

    #endregion

    public class UpdateCoordinatesIM
    {
        /// <summary>
        /// The new group the logged-on user is creating.
        /// </summary>
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }

    public class UpdateProfileIM
    {
        public string ProfileField { get; set; }
    }

    public class UpdateDesignIM
    {
        public string DesignField { get; set; }
    }
}