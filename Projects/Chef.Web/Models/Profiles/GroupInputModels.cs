namespace Chef.Web.Models.Profiles
{
    // InputModels (IM) for PeopleApi

    #region CreateGroupIM

    /// <summary>
    /// The authenticated user is creating a new group.
    /// </summary>
    public class CreateGroupIM
    {
        /// <summary>
        /// The new group the logged-on user is creating.
        /// </summary>
        public string NewGroupName { get; set; }
    }

    #endregion

    #region CreateGroupWithTargetIM

    /// <summary>
    /// The authenticated user is creating a new group while adding a target 
    /// user to the group.
    /// </summary>
    public class CreateGroupWithTargetIM
    {
        /// <summary>
        /// The user who is being added by the logged-on user.
        /// </summary>
        public int TargetId { get; set; }
        /// <summary>
        /// The new group the logged-on user is creating.
        /// </summary>
        public string NewGroupName { get; set; }
    }

    #endregion

    /// <summary>
    /// Updates an existing group's name.
    /// </summary>
    public class UpdateGroupIM
    {
        /// <summary>
        /// Id of an existing group.
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// The new group the logged-on user is creating.
        /// </summary>
        public string NewGroupName { get; set; }
    }

    /// <summary>
    /// Deletes an existing group.
    /// </summary>
    public class DestroyGroupIM
    {
        /// <summary>
        /// Id of an existing group.
        /// </summary>
        public int GroupId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AddRemoveUserToGroupIM
    {
        /// <summary>
        /// The user to add or remove.
        /// </summary>
        public int TargetId { get; set; }
        /// <summary>
        /// Id of an existing group.
        /// </summary>
        public int GroupId { get; set; }
    }

    public class EmailMeIM
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}