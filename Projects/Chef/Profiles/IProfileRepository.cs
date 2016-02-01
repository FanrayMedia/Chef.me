using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace Chef.Profiles
{
    /// <summary>
    /// Profile operations contract.
    /// </summary>
    public interface IProfileRepository
    {
        /// <summary>
        /// Creates a new user and returns <see cref="Microsoft.AspNet.Identity.IdentityResult"/>
        /// with any error messages if operation fails.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        ProfileResult CreateProfile(Profile profile);

        /// <summary>
        /// Returns a user profile for the given username, optionally returns 
        /// user profile styles. Returns null if no profile found.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="includeStyles"></param>
        /// <returns></returns>
        /// <remarks>
        /// "/{userName}" would need styles while "/{userName}/contacts" for example may not.
        /// </remarks>
        Profile GetProfile(string userName, bool includeStyles = false);

        /// <summary>
        /// Returns a list of user profiles for the given list of profile ids.
        /// Returns an empty list if ids list is empty.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<Profile> GetProfiles(List<int> ids);

        /// <summary>
        /// Temp solution to serve up profiles.
        /// </summary>
        /// <returns></returns>
        List<Profile> GetAllProfiles();

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <remarks>
        /// This will delete a row from AspNetUsers, Profile and ProfileStyle tables.
        /// 
        /// Should be used with care, generally you don't need to delete a user, 
        /// because you have the option to ban a user.  Integration tests use 
        /// this method a lot for testing purpose.
        /// </remarks>
        /// <param name="userName"></param>
        IdentityResult DeleteProfile(string userName);

        void UpdateBgImg(string userName, string fileName);
        void UpdateCoordinates(string userName, int x, int y);
        void UpdateProfile(string userName, Profile profile);
        void UpdateDesign(string userName, ProfileStyle profileStyle);
    }
}
