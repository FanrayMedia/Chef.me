using Chef.Accounts;
using Chef.Collections;
using Chef.Collections.Data;
using Chef.Emails;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Chef.Profiles
{
    public class ProfileService : CollectableService<Profile>
    {
        #region Constructor

        private readonly IProfileRepository _repo;
        private UserManager<ApplicationUser> _userManager;

        public ProfileService()
        {
            _repo = new SqlProfileRepository();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        }

        public ProfileService(IProfileRepository repo)
        {
            _repo = repo;
        }

        #endregion

        // -------------------------------------------------------------------- Events

        #region UserCreated

        /// <summary>
        /// Occurs when a user is registered.
        /// </summary>
        public static event EventHandler<EventArgs> UserCreated;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        static void OnUserCreated(Profile profile)
        {
            if (UserCreated != null)
            {
                UserCreated(profile, new EventArgs());
            }
        }

        #endregion

        // -------------------------------------------------------------------- Profile

        #region GetProfile

        /// <summary>
        /// Returns a user profile by userName, optionally include the profile 
        /// styles. Returns null if is userName is null or empty or no profile 
        /// found.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="includeStyles">styles may or may not be needed</param>
        /// <returns></returns>
        /// <remarks>
        /// Called when:
        /// 1. a user's profile is visited PeopleApi.GetProfile, in this situation 
        /// styles are needed;
        /// 2. a user's collection is visited, in this case no styles needed
        /// see _MapGroup.
        /// </remarks>
        public Profile GetProfile(string userName, bool includeStyles = false)
        {
            return string.IsNullOrWhiteSpace(userName) ? null :
                _repo.GetProfile(userName, includeStyles);
        }

        #endregion

        #region GetProfiles

        /// <summary>
        /// Returns a list of profile for the given profile ids, returns null
        /// if profileIds is null.
        /// </summary>
        /// <param name="profileIds"></param>
        /// <returns></returns>
        public List<Profile> GetProfiles(List<int> profileIds)
        {
            return (profileIds == null) ? null : _repo.GetProfiles(profileIds);
        }

        #endregion

        #region CreateUserProfile
        public IdentityResult CreateUserProfile(Profile profile)
        {
            var user = new ApplicationUser { UserName = profile.UserName, Email = profile.Email };
            return CreateUserProfile(profile, user, null);
        }
        /// <summary>
        /// Creates a new user and profile and assign default groups.
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="user">ApplicationUser</param>
        /// <param name="password">null if called from ExternalLoginConfirmation</param>
        /// <returns></returns>
        public IdentityResult CreateUserProfile(Profile profile, ApplicationUser user, string password)
        {
            // username must be within certain length
            if (profile.UserName.IsNullOrEmpty() || profile.UserName.Length > Config.USERNAME_MAXLENGTH ||
                profile.UserName.Length < Config.USERNAME_MINLENGTH)
            {
                return new IdentityResult(new List<string>() {
                       string.Format(Msg.USERNAME_MUST_BE_WITHIN_LENGTH, Config.USERNAME_MINLENGTH, Config.USERNAME_MAXLENGTH)
                    });
            }

            // username must be alpha-numeric
            if (!Regex.IsMatch(profile.UserName, Config.USERNAME_REGEX, RegexOptions.IgnoreCase))
            {
                return new IdentityResult(new List<string>() {
                        Msg.USERNAME_MUST_BE_ALPHANUMERICA
                    });
            }

            // username cannot be among reserved words
            if (Config.USERNAME_RESERVED.Any(n=>n.Equals(profile.UserName, StringComparison.InvariantCultureIgnoreCase)))
            {
                return new IdentityResult(new List<string>() {
                        string.Format(Msg.USERNAME_IS_TAKEN, profile.UserName)
                    });
            }

            // create profile which checks duplicate email and username
            ProfileResult profileResult = _repo.CreateProfile(profile);
            if (profileResult.Succeeded)
            {
                // create user which checks username but not email
                IdentityResult identityResult = password.IsNullOrEmpty() ?
                        _userManager.Create(user) : _userManager.Create(user, password);

                if (identityResult.Succeeded)
                {
                    // create default groups
                    foreach (var groupName in Config.DEFAULT_GROUPS)
                        this.CreateGroup(profile.UserName, groupName);
                }

                // raise event that the user has been created, right now no one is listening
                //OnUserCreated(profile);

                return identityResult;
            }

            return new IdentityResult(profileResult.Errors);
        }

        #endregion

        #region DeleteProfile

        /// <summary>
        /// Currently only used for testing purpose. It deletes user, profile and groups.
        /// </summary>
        /// <param name="userName"></param>
        public void DeleteProfile(string userName)
        {
           var result = _repo.DeleteProfile(userName);
            if (result.Succeeded)
            {
                // deletes all groups this user has
                var groupList = this.GetGroupList(userName);
                foreach (var group in groupList)
                    this.DeleteGroup(userName, group.Id);
            }
        }

        #endregion

        #region GetAllProfiles

        public List<Profile> GetAllProfiles()
        {
            return _repo.GetAllProfiles();
        }

        #endregion

        // -------------------------------------------------------------------- UpdateProfile

        #region UpdateBgImg

        public void UpdateBgImg(string userName, string imgFileName)
        {
            _repo.UpdateBgImg(userName, imgFileName); //  + ".jpg"
        }

        #endregion
        
        #region UpdateCoordinates

        public void UpdateCoordinates(string userName, decimal x, decimal y)
        {
            _repo.UpdateCoordinates(userName,
                Convert.ToInt32(x),
                Convert.ToInt32(y));
        }

        #endregion

        #region UpdateProfile

        public void UpdateProfile(string userName, Profile profile)
        {
            _repo.UpdateProfile(userName, profile);
        }

        #endregion

        #region UpdateDesign

        public void UpdateDesign(string userName, ProfileStyle profileStyle)
        {
            _repo.UpdateDesign(userName, profileStyle);
        }

        #endregion

        // -------------------------------------------------------------------- email

        #region EmailMe

        /// <summary>
        /// https://github.com/sendgrid/sendgrid-csharp
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="targetUserName"></param>
        /// <param name="message"></param>
        public void EmailMe(string userName, string targetUserName, string message)
        {
            var senderProfile = _repo.GetProfile(userName);
            EmailUser sender = new EmailUser { 
                UserName = userName,
                FirstName = senderProfile.FirstName,
                LastName = senderProfile.LastName,
                Email = senderProfile.Email,
                BgImgUrl = senderProfile.BgImg, // 
            };
            
            var recipientProfile = _repo.GetProfile(targetUserName);
            EmailUser recipient = new EmailUser
            {
                UserName = userName,
                FirstName = recipientProfile.FirstName,
                LastName = recipientProfile.LastName,
                Email = recipientProfile.Email,
                BgImgUrl = recipientProfile.BgImg, // 
            };

            EmailSender.SendEmailMe(sender, recipient, message);
        }

        #endregion

        // -------------------------------------------------------------------- CollectableService<T>

        #region GetGroup

        /// <summary>
        /// Returns a group of user profiles for the given userName and groupSlug, 
        /// returns all user profiles for the given userName if groupSlug is null,
        /// returns null if no such a group is found.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupSlug"></param>
        /// <returns></returns>
        public override Group<Profile> GetGroup(string userName, string groupSlug)
        {
            var groupEnt = _collRepo.GetGroup(userName, groupSlug, GroupType.User);
            return _MapGroup(groupEnt, userName, includeItems: true);
        }

        #endregion

        #region GetGroupList

        /// <summary>
        /// Returns all groups the given userName has created for collecting other 
        /// user profiles, it's used to display a list of group, thus the groups 
        /// contain no item data.  Returns an empty list if no groups are found.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public override List<Group<Profile>> GetGroupList(string userName)
        {
            List<Group<Profile>> groupList = new List<Group<Profile>>();
            var entList = _collRepo.GetGroupList(userName, GroupType.User);
            if (entList == null) return groupList;

            foreach (var ent in entList)
            {
                groupList.Add(_MapGroup(ent, userName));
            }

            return groupList;
        }

        #endregion

        #region GetGroupListWithTarget

        /// <summary>
        /// Returns all groups the given userName has created for collecting other 
        /// user profiles, it's used to display a list of group, thus the groups 
        /// contain no item data.  Returns an empty list if no groups are found.
        /// 
        /// It sets the TargetExists property based on if targetId is found in a
        /// group.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public override List<Group<Profile>> GetGroupListWithTarget(string userName, int targetId)
        {
            List<Group<Profile>> groupList = new List<Group<Profile>>();
            var entList = _collRepo.GetGroupList(userName, GroupType.User);
            if (entList == null) return groupList;

            foreach (var ent in entList)
            {
                groupList.Add(_MapGroupWithTarget(ent, userName,targetId));
            }

            return groupList;
        }

        #endregion

        #region CreateGroup

        /// <summary>
        /// Creates and returns a new group used to collect other user profiles.
        /// If groupName already exists or validation fails it returns null.
        /// The returned group does not have Items data.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public override Group<Profile> CreateGroup(string userName, string groupName)
        {
            // TODO validation on username, groupname
            // ? should I throw exception when validation fails

            if (_collRepo.GroupNameExists(userName, groupName, GroupType.User))
                return null;

            GroupEntity groupEnt = new GroupEntity {
                UserName = userName,
                Name = groupName,
                Slug = WebHelper.FormatSlug(groupName),
                GroupType = GroupType.User,
                IsPublic = true,
            };

            int id = _collRepo.CreateGroup(groupEnt);
            groupEnt.Id = id;

            return _MapGroup(groupEnt, userName);
        }

        #endregion

        #region CreateGroupWithTarget

        /// <summary>
        /// Creates and returns a new group used to collect other user profiles.
        /// If groupName already exists or validation fails it returns null.
        /// And adds an item to the new group. The returned group does not have
        /// items data.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupName"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public override Group<Profile> CreateGroupWithTarget(string userName, string groupName, int targetId)
        {
            // create group
            Group<Profile> newGroup = this.CreateGroup(userName, groupName);
            if (newGroup == null) return null;

            // add item to that group
            _collRepo.AddItemToGroup(userName, targetId, newGroup.Id); // should I return a value to signal if success?
            newGroup.TargetExists = true;
            newGroup.ItemCount++;

            return newGroup;
        }

        #endregion

        // -------------------------------------------------------------------- Helpers

        #region _MapGroup

        /// <summary>
        /// Returns a Group<Profile> object given groupEnt and userName, 
        /// returns null if groupEnt or userName is null.
        /// </summary>
        /// <remarks>
        /// Items are always included with the group.
        /// </remarks>
        /// <param name="groupEnt"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private Group<Profile> _MapGroup(GroupEntity groupEnt, string userName, bool includeItems = false)
        {
            if (groupEnt == null || string.IsNullOrWhiteSpace(userName)) return null;

            Profile profile = this.GetProfile(userName);
            List<Profile> profileList = null;
            
            if (/*groupEnt.ItemEntities != null &&*/ includeItems)
            {
                List<int> ids = new List<int>();
                foreach (var item in groupEnt.ItemEntities)
                {
                    ids.Add(item.ItemId);
                }
                profileList = this.GetProfiles(ids);
            }

            return new Group<Profile>
            {
                Id = groupEnt.Id,
                Profile = profile,
                Name = groupEnt.Name,
                Slug = groupEnt.Slug,
                IsPublic = groupEnt.IsPublic,
                ItemCount = groupEnt.ItemEntities.Count,
                Items = profileList,
            };
        }

        /// <summary>
        /// Used only by 
        /// </summary>
        /// <param name="groupEnt"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private Group<Profile> _MapGroupWithTarget(GroupEntity groupEnt, string userName, int targetId)
        {
            var group = _MapGroup(groupEnt, userName); // get group without items
            group.TargetExists = groupEnt.ItemEntities.Any(item => item.ItemId == targetId);
            return group;
        }

        #endregion
    }
}
