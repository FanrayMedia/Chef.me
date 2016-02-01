using System.Collections.Generic;

namespace Chef.Collections.Data
{
    /// <summary>
    /// Collection repo.
    /// </summary>
    /// <remarks>
    /// Enables users to collect collectibles where collectibles are users/recipes/qna etc.
    /// Therefore some of these methods are passed in an targetId instead of say a userName.
    /// </remarks>
    public interface ICollectionRepository
    {
        // -------------------------------------------------------------------- Get

        /// <summary>
        /// Returns a specific group by its slug for the given user and group type.
        /// The returned group has in it Items data, if groupSlug is null Items 
        /// then contains all items for the given user.  Returns null
        /// if no such group found.
        /// </summary>
        GroupEntity GetGroup(string userName, string groupSlug, GroupType groupType);
        /// <summary>
        /// Returns a list of all groups for the given user and group type.
        /// The returned groups have in them Items data.
        /// Returns null if no groups are found.
        /// </summary>
        List<GroupEntity> GetGroupList(string userName, GroupType groupType);

        // -------------------------------------------------------------------- Create/Update/Delete

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <remarks>
        /// The caller of this method should check if group name exists already.
        /// </remarks>
        int CreateGroup(GroupEntity groupEnt);
        /// <summary>
        /// Deletes a group.
        /// </summary>
        void DeleteGroup(string userName, int groupId);
        /// <summary>
        /// Updates a group with a new name.
        /// </summary>
        void UpdateGroup(string userName, int groupId, string newGroupName, string newGroupSlug);

        // -------------------------------------------------------------------- AddItem/RemoveItem

        /// <summary>
        /// Adds an item to a group.
        /// </summary>
        /// <param name="targetId">
        /// Because target could be user, recipe or any collectiable, so here I
        /// pass an ID not a name + targetType kind thing.
        /// </param>
        void AddItemToGroup(string userName, int targetId, int groupId);
        /// <summary>
        /// Removes an item from a group.
        /// </summary>
        void RemoveItemFromGroup(string userName, int targetId, int groupId);

        // -------------------------------------------------------------------- bool

        /// <summary>
        /// Returns true if a group with the name exists already.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupName"></param>
        /// <param name="groupType"></param>
        /// <returns></returns>
        bool GroupNameExists(string userName, string groupName, GroupType groupType);
        /// <summary>
        /// Returns true if the item already exists in the group.
        /// </summary>
        /// <remarks>
        /// Used by AddRemoveContactToGroup
        /// </remarks>
        /// <param name="userName"></param>
        /// <param name="targetId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        bool ItemExistsInGroup(string userName, int targetId, int groupId);
    }
}
