using Chef.Collections.Data;
using System.Collections.Generic;

namespace Chef.Collections
{
    /// <summary>
    /// For any service that needs to impl collecting item work, such as a user
    /// can collect other users into group, or a user can collect things like 
    /// recipes into groups etc.
    /// </summary>
    public abstract class CollectableService<T> where T: ICollectable
    {
        #region Constructor

        protected readonly ICollectionRepository _collRepo;

        public CollectableService()
        {
            _collRepo = new SqlCollectionRepository();
        }

        public CollectableService(ICollectionRepository collRepo)
        {
            _collRepo = collRepo;
        }

        #endregion

        public abstract Group<T> GetGroup(string userName, string groupSlug);
        public abstract List<Group<T>> GetGroupList(string userName);
        public abstract List<Group<T>> GetGroupListWithTarget(string userName, int targetId);
        public abstract Group<T> CreateGroup(string userName, string groupName);
        public abstract Group<T> CreateGroupWithTarget(string userName, string groupName, int targetId);

        #region UpdateGroup

        /// <summary>
        /// Updates an existing group with a new name.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupId"></param>
        /// <param name="newGroupName"></param>
        public string UpdateGroup(string userName, int groupId, string newGroupName)
        {
            if (groupId == 0) return ""; // this is the All group case

            string newGroupSlug = WebHelper.FormatSlug(newGroupName);
            _collRepo.UpdateGroup(userName, groupId, newGroupName,
                newGroupSlug);

            return newGroupSlug; // hacky?
        }

        #endregion

        #region DeleteGroup

        /// <summary>
        /// Deletes group and all items the group has. TODO should I return something 
        /// to signal delete failed?
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupId"></param>
        public void DeleteGroup(string userName, int groupId)
        {
            _collRepo.DeleteGroup(userName, groupId);
        }

        #endregion

        #region AddRemoveItemToGroup

        /// <summary>
        /// If target item is not yet in the group for the given user, add it,
        /// else remove the item from the group. 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="targetId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Should I return targetId back to signal success and -1 for failed? Does js need this info? 
        /// </remarks>
        public void AddRemoveItemToGroup(string userName, int targetId, int groupId)
        {
            if (_collRepo.ItemExistsInGroup(userName, targetId, groupId))
                _collRepo.RemoveItemFromGroup(userName, targetId, groupId);
            else
                _collRepo.AddItemToGroup(userName, targetId, groupId);
        }

        #endregion
    }
}
