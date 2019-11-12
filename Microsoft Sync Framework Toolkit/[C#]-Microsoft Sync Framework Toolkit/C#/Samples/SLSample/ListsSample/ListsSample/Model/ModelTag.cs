// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using DefaultScope;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ListsSample.Model
{
    /// <summary>
    /// Represents a conceptualized model for a tag.
    /// </summary>
    public class ModelTag
    {
        int tagId;
        Tag tag = null;
        int count;
        DynamicQuery<Item> items;
        
        /// <summary>
        /// Takes the tag id and the number of items associated with the tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="count"></param>
        public ModelTag(int tagId, int count)
        {
            this.tagId = tagId;
            this.count = count;
            items = null;
        }

        /// <summary>
        /// Returns the number of items associated with the tag
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }

        /// <summary>
        /// This property is databound to the size of text, and is dependent on the count
        /// </summary>
        public int Size
        {
            get
            {
                return 10 + (count - 1);
            }
        }

        /// <summary>
        /// Returns the name of the associated tag.
        /// </summary>
        public string Tag
        {
            get
            {
                if (tag == null)
                {
                    tag = ContextModel.Instance.GetTag(tagId);
                }

                return tag.Name;
            }
        }

        /// <summary>
        /// Returns the tag id
        /// </summary>
        public int TagID
        {
            get
            {
                return tagId;
            }
        }

        /// <summary>
        /// Returns the items for the tag
        /// </summary>
        public IEnumerable<Item> Items
        {
            get
            {
                if (items == null)
                {
                    var query = from t in ContextModel.Instance.TagItemMappings
                                where t.TagID == tagId
                                join i in ContextModel.Instance.Items on t.ItemID equals i.ID
                                select i;

                    items = new DynamicQuery<Item>(
                        query,
                        (INotifyCollectionChanged)ContextModel.Instance.TagItemMappings,
                        (INotifyCollectionChanged)ContextModel.Instance.Items);
                }

                return items;
            }
        }

        /// <summary>
        /// This is overridden to allow the ListBox selection to work when navigating the ui
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                return true;
            }

            if (obj is ModelTag)
            {
                return ((ModelTag)obj).TagID == this.tagId;
            }

            return false;
        }

        /// <summary>
        /// This is likely incorrect, but it is need to quiet compiler warnings
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
