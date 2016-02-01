using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Chef.Collections.Data
{
    public class SqlCollectionRepository : ICollectionRepository
    {
        // -------------------------------------------------------------------- Get

        public GroupEntity GetGroup(string userName, string groupSlug, GroupType groupType)
        {
            using (var db = new CollectionDbContext())
            {
                // return all contacts across all groups since groupSlug is empty
                if (groupSlug.IsNullOrEmpty())
                {
                    // TODO paging
                    // get all items across all groups
                    var itemEnts = (from g in db.GroupEntities
                                from i in g.ItemEntities
                                where g.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase) &&
                                      g.Id == i.GroupId &&
                                      g.GroupType == groupType
                                select i ).ToList();
                    if (itemEnts == null) return null;

                    var groupEnt = new GroupEntity();
                    foreach (var item in itemEnts)
                    {
                        if (!groupEnt.ItemEntities.Any(i=>i.ItemId == item.ItemId)) // filter out existing for "all" group
                            groupEnt.ItemEntities.Add(item);
                    }
                    groupEnt.UserName = userName;
                    return groupEnt;
                }

                // return contacts for a specific group
                return db.GroupEntities.Include(g => g.ItemEntities)
                                    .FirstOrDefault(g => g.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase)
                                        && g.Slug.Equals(groupSlug, StringComparison.InvariantCultureIgnoreCase)
                                        && g.GroupType == groupType);
            }
        }

        public List<GroupEntity> GetGroupList(string userName, GroupType groupType)
        {
            using (var db = new CollectionDbContext())
            {
                var groupEnts = db.GroupEntities.Include(g => g.ItemEntities)
                    .Where(g => g.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase) 
                        && g.GroupType == groupType);

                return (groupEnts == null) ? null : groupEnts.ToList();
            }
        }

        // -------------------------------------------------------------------- Create/Update/Delete

        public int CreateGroup(GroupEntity groupEnt)
        {
            using (var db = new CollectionDbContext())
            {
                db.GroupEntities.Add(groupEnt);
                db.SaveChanges();
                return groupEnt.Id;
            }
        }

        public void DeleteGroup(string userName, int groupId)
        {
            using (var db = new CollectionDbContext())
            {
                var groupEnt = db.GroupEntities.FirstOrDefault(g => g.Id == groupId);
                db.GroupEntities.Remove(groupEnt);
                db.SaveChanges();
            }
        }

        public void UpdateGroup(string userName, int groupId, string newGroupName, string newGroupSlug)
        {
            using (var db = new CollectionDbContext())
            {
                // make sure the groupId belongs to the authenticatedUserName
                var groupEnt = db.GroupEntities.FirstOrDefault(g => g.Id == groupId 
                    && g.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
                if (groupEnt == null) return;

                groupEnt.Name = newGroupName;
                groupEnt.Slug = newGroupSlug;
                db.SaveChanges();
            }
        }

        // -------------------------------------------------------------------- AddItem / RemoveItem

        public void AddItemToGroup(string userName, int targetId, int groupId)
        {
            using (var db = new CollectionDbContext())
            {
                // TODO: should I check if targetId exists?
                var groupEnt = db.GroupEntities.FirstOrDefault(g => g.Id == groupId
                    && g.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
                if (groupEnt == null) return;

                var itemEnt = new ItemEntity();
                itemEnt.GroupId = groupId;
                itemEnt.ItemId = targetId;
                itemEnt.AddedOn = DateTime.UtcNow;

                groupEnt.ItemEntities.Add(itemEnt);
                db.SaveChanges();
            }
        }

        public void RemoveItemFromGroup(string userName, int targetId, int groupId)
        {
            using (var db = new CollectionDbContext())
            {
                var groupEnt = db.GroupEntities.Include(g=>g.ItemEntities).FirstOrDefault(g => g.Id == groupId
                   && g.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
                if (groupEnt == null) return;

                var itemEnt = groupEnt.ItemEntities.FirstOrDefault(i => i.GroupId == groupId
                    && i.ItemId == targetId);
                if (itemEnt == null) return;

                groupEnt.ItemEntities.Remove(itemEnt);
                db.SaveChanges();
            }
        }

        // -------------------------------------------------------------------- bool

        public bool GroupNameExists(string userName, string groupName, GroupType groupType)
        {
            using (var db = new CollectionDbContext())
            {
                return db.GroupEntities.FirstOrDefault(g => g.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase)
                    && g.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase)
                    && g.GroupType == groupType) != null;
            }
        }

        public bool ItemExistsInGroup(string userName, int targetId, int groupId)
        {
            using (var db = new CollectionDbContext())
            {
                int count = (from g in db.GroupEntities
                             from i in g.ItemEntities
                             where g.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase) &&
                                   g.Id == groupId && 
                                   i.GroupId == groupId &&
                                   i.ItemId == targetId
                             select i).Count();

                return count != 0;
            }
        }
    }
}
