using System.Windows;
using System.Windows.Controls;

namespace BcSoft.EDC.Surface.Controls
{
    public class TreeListViewItem : TreeViewItem
    {
        static TreeListViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeListViewItem), new FrameworkPropertyMetadata(typeof(TreeListViewItem)));
        }

        private int _level = -1;
        public int Level
        {
            get
            {
                if (_level == -1)
                {
                    TreeListViewItem parent =
                        ItemsControl.ItemsControlFromItemContainer(this) as TreeListViewItem;
                    _level = (parent != null) ? parent.Level + 1 : 0;
                }
                return _level;
            }
        }

        public bool IsExpandedOnce
        {
            get;
            internal set;
        }

        public bool IsExpandable
        {
            get
            {
                return (this.HasItems && this.Items.Count > 0);
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            bool _isITV = item is TreeListViewItem;
            return _isITV;
        }
    }
}
