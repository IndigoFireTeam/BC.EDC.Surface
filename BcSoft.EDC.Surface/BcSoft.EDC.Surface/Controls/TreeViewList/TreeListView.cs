using System.Windows;
using System.Windows.Controls;

namespace BcSoft.EDC.Surface.Controls
{
    public class TreeListView : TreeView
    {
        public TreeListView()
        {
            if (Columns == null)
                Columns = new GridViewColumnCollection();
            else
                Columns.Clear();
        }

        static TreeListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeListView), new FrameworkPropertyMetadata(typeof(TreeListView)));
        }

        //这两个默认的是TreeViewItem
        protected override DependencyObject GetContainerForItemOverride()//创建或标识用于显示指定项的元素。 
        {
            return new TreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)//确定指定项是否是（或可作为）其自己的 ItemContainer
        {
            //return item is TreeListViewItem;
            bool _isTreeLVI = item is TreeListViewItem;
            return _isTreeLVI;
        }

        public GridViewColumnCollection Columns
        {
            get { return (GridViewColumnCollection)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(GridViewColumnCollection), typeof(TreeListView), new PropertyMetadata(new GridViewColumnCollection()));

        public bool AllowsColumnReorder
        {
            get { return (bool)GetValue(AllowsColumnReorderProperty); }
            set { SetValue(AllowsColumnReorderProperty, value); }
        }
        // Using a DependencyProperty as the backing store for AllowsColumnReorder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowsColumnReorderProperty =
            DependencyProperty.Register("AllowsColumnReorder", typeof(bool), typeof(TreeListView), new UIPropertyMetadata(null));
    }
}
