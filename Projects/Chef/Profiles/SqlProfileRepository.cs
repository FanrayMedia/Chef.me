using Chef.Accounts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Chef.Profiles
{
    /// <summary>
    /// Sqlserver implementation of the <see cref="Chef.Profiles.IProfileRepository"/>.
    /// </summary>
    /// <remarks>
    /// This implementation partly used <see cref="Microsoft.AspNet.Identity.UserManager<T>"/> so I don't
    /// re-invent the wheel for those user centric operations, and should I decide to swap out those
    /// I can easily do so.
    /// </remarks>
    public class SqlProfileRepository : IProfileRepository
    {
        #region Constructor

        private UserManager<ApplicationUser> _userManager;

        public SqlProfileRepository()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        #endregion

        #region CreateProfile

        /// <summary>
        /// Creates a new user.
        /// </summary>
        public ProfileResult CreateProfile(Profile profile)
        {
            using (var db = new ProfileDbContext())
            {
                if (db.Profiles.FirstOrDefault(u => u.Email.ToLower() == profile.Email.ToLower()) != null)
                {
                    return new ProfileResult(new List<string>() {
                        "Email " + profile.Email + " exists already."
                    });
                }

                // wouldn't hurt if we check username here, even though _userManager checks it again
                if (db.Profiles.FirstOrDefault(u => u.UserName.ToLower() == profile.UserName.ToLower()) != null)
                {
                    return new ProfileResult(new List<string>() {
                        "Username " + profile.UserName + " is already taken."
                    });
                }

                db.Profiles.Add(profile);
                db.SaveChanges();

                return new ProfileResult
                    {
                        Succeeded = true,
                        ProfileId = profile.Id
                    };
            }

        }

        #endregion

        #region GetProfile

        public Profile GetProfile(string userName, bool includeStyles = false)
        {
            using (var db = new ProfileDbContext())
            {
                return includeStyles ?
                    db.Profiles.Include(p => p.ProfileStyle).FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase)) :
                    db.Profiles.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        #endregion

        #region GetProfiles

        public List<Profile> GetProfiles(List<int> ids)
        {
            using (var db = new ProfileDbContext())
            {
                return (from profile in db.Profiles
                        where ids.Contains(profile.Id)
                        select profile).ToList();
            }
        }

        #endregion

        #region GetAllProfiles

        public List<Profile> GetAllProfiles()
        {
            using (var db = new ProfileDbContext())
            {
                return db.Profiles.ToList();
            }
        }

        #endregion

        #region DeleteProfile

        public IdentityResult DeleteProfile(string userName)
        {
            ApplicationUser user = _userManager.FindByName(userName);
            IdentityResult result = null;
            if (user != null)
            {
                result = _userManager.Delete(user);
                if (!result.Succeeded) return result;
            }

            using (var db = new ProfileDbContext())
            {
                var profile = db.Profiles.FirstOrDefault(p => p.UserName.Equals(userName,
                    StringComparison.InvariantCultureIgnoreCase));

                if (profile != null)
                {
                    var profileStyle = db.ProfileStyles.FirstOrDefault(p => p.Id == profile.Id);

                    db.Profiles.Remove(profile);
                    db.ProfileStyles.Remove(profileStyle);
                    db.SaveChanges();
                }
            }

            return result;
        }

        #endregion

        #region UpdateBgImg

        public void UpdateBgImg(string userName, string fileName)
        {
            using (var db = new ProfileDbContext())
            {
                var profile = db.Profiles.FirstOrDefault(p => p.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
                if (profile == null) return;

                profile.BgImg = fileName;
                db.SaveChanges();
            }
        }

        #endregion

        #region UpdateCoordinates

        public void UpdateCoordinates(string userName, int x, int y)
        {
            using (var db = new ProfileDbContext())
            {
                var entPro = db.Profiles.FirstOrDefault(p => p.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
                if (entPro == null) return;

                var ent = db.ProfileStyles.FirstOrDefault(p => p.Id == entPro.Id); // find the ProfileStyles entity

                ent.Profbox_x = x;
                ent.Profbox_y = y;
                db.SaveChanges();
            }
        }

        #endregion

        #region UpdateProfile

        public void UpdateProfile(string userName, Profile profile)
        {
            using (var db = new ProfileDbContext())
            {
                var ent = db.Profiles.FirstOrDefault(p => p.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
                if (ent == null) return;
                bool changed = false;

                if (!profile.FirstName.IsNullOrEmpty() && ent.FirstName != profile.FirstName)
                {
                    ent.FirstName = profile.FirstName;
                    changed = true;
                }
                if (!profile.LastName.IsNullOrEmpty() && ent.LastName != profile.LastName)
                {
                    ent.LastName = profile.LastName;
                    changed = true;
                }
                if (!profile.Headline.IsNullOrEmpty() && ent.Headline != profile.Headline)
                {
                    ent.Headline = profile.Headline;
                    changed = true;
                }
                if (!profile.Bio.IsNullOrEmpty() && ent.Bio != profile.Bio)
                {
                    ent.Bio = profile.Bio;
                    changed = true;
                }
                if (!profile.Locations.IsNullOrEmpty() && ent.Locations != profile.Locations)
                {
                    ent.Locations = profile.Locations;
                    changed = true;
                }

                if (changed)
                    db.SaveChanges();
            }
        }

        #endregion

        #region UpdateDesign

        public void UpdateDesign(string userName, ProfileStyle profileStyle)
        {
            using (var db = new ProfileDbContext())
            {
                var entPro = db.Profiles.FirstOrDefault(p => p.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
                if (entPro == null) return;

                var ent = db.ProfileStyles.FirstOrDefault(p => p.Id == entPro.Id); // find the ProfileStyles entity
                bool changed = false;

                // name
                if (!profileStyle.Name_color.IsNullOrEmpty() && ent.Name_color != profileStyle.Name_color)
                {
                    ent.Name_color = profileStyle.Name_color;
                    changed = true;
                }
                if (!profileStyle.Name_font.IsNullOrEmpty() && ent.Name_font != profileStyle.Name_font)
                {
                    ent.Name_font = profileStyle.Name_font;
                    changed = true;
                }
                if (profileStyle.Name_size > 0 && ent.Name_size != profileStyle.Name_size)
                {
                    ent.Name_size = profileStyle.Name_size;
                    changed = true;
                }

                // headline
                if (!profileStyle.Headline_color.IsNullOrEmpty() && ent.Headline_color != profileStyle.Headline_color)
                {
                    ent.Headline_color = profileStyle.Headline_color;
                    changed = true;
                }
                if (!profileStyle.Headline_font.IsNullOrEmpty() && ent.Headline_font != profileStyle.Headline_font)
                {
                    ent.Headline_font = profileStyle.Headline_font;
                    changed = true;
                }
                if (profileStyle.Headline_size > 0 && ent.Headline_size != profileStyle.Headline_size)
                {
                    ent.Headline_size = profileStyle.Headline_size;
                    changed = true;
                }

                // bio
                if (!profileStyle.Bio_color.IsNullOrEmpty() && ent.Bio_color != profileStyle.Bio_color)
                {
                    ent.Bio_color = profileStyle.Bio_color;
                    changed = true;
                }
                if (!profileStyle.Bio_font.IsNullOrEmpty() && ent.Bio_font != profileStyle.Bio_font)
                {
                    ent.Bio_font = profileStyle.Bio_font;
                    changed = true;
                }
                if (profileStyle.Bio_size > 0 && ent.Bio_size != profileStyle.Bio_size)
                {
                    ent.Bio_size = profileStyle.Bio_size;
                    changed = true;
                }

                // links
                if (!profileStyle.Links_color.IsNullOrEmpty() && ent.Links_color != profileStyle.Links_color)
                {
                    ent.Links_color = profileStyle.Links_color;
                    changed = true;
                }
                if (!profileStyle.Links_font.IsNullOrEmpty() && ent.Links_font != profileStyle.Links_font)
                {
                    ent.Links_font = profileStyle.Links_font;
                    changed = true;
                }
                if (profileStyle.Links_size > 0 && ent.Links_size != profileStyle.Links_size)
                {
                    ent.Links_size = profileStyle.Links_size;
                    changed = true;
                }

                // box bg and opacity
                if (!profileStyle.Profbox_bgcolor.IsNullOrEmpty() && ent.Profbox_bgcolor != profileStyle.Profbox_bgcolor)
                {
                    ent.Profbox_bgcolor = profileStyle.Profbox_bgcolor;
                    changed = true;
                }
                if (profileStyle.Profbox_opacity > 0 && ent.Profbox_opacity != profileStyle.Profbox_opacity)
                {
                    ent.Profbox_opacity = profileStyle.Profbox_opacity;
                    changed = true;
                }

                if (changed)
                    db.SaveChanges();
            }
        } 

        #endregion
    }
}
