using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace Chef.Profiles
{
    public class ProfileResult : IdentityResult
    {
        public ProfileResult()
        {
        }
        public ProfileResult(IEnumerable<string> errors) : base(errors)
        {

        }

        /// <summary>
        /// Returns true if profile was created successfully.
        /// </summary>
        public new bool Succeeded { get;set; }
        /// <summary>
        /// New profile id after ProfileService.CreateProfile().
        /// </summary>
        public int ProfileId { get; set; }
    }
}
