using BcSoft.EDC.Surface.Domain;
using BcSoft.EDC.Surface.Helper;
using BcSoft.EDC.Surface.Model;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BcSoft.EDC.Surface.Commands;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class ViewPointViewModel : DevExpress.Mvvm.ViewModelBase
    {
        public ViewPointViewModel()
        {
            this.InitCommand();
            this.LoadViewPoints();
        }

        #region Command
        public DelegateCommand<ExCommandParameter> ViewPointCommand { get; set; }
        #endregion

        #region Private Methods
        private void InitCommand()
        {
            ViewPointCommand = new DelegateCommand<ExCommandParameter>(this.ViewPointCommandExecute);
        }

        private void LoadViewPoints()
        {
            var originViewPoints = new Dictionary<string, ViewPointModel>();
            var viewPoints = ApplicationContext.Instance.ProjectHelper.Select<ViewPoint>("Select * From ViewPoint");
            if (viewPoints != null)
            {
                string rootGuid = string.Empty;
                foreach (var viewPoint in viewPoints)
                {
                    if (string.IsNullOrEmpty(viewPoint.Parent_Id)
                        || string.Equals("0", viewPoint.Parent_Id))
                    {
                        rootGuid = viewPoint.Id;
                    }

                    var viewPointModel = MapperHelper.Mapper<ViewPoint, ViewPointModel>(viewPoint);
                    originViewPoints.Add(viewPoint.Id, viewPointModel);
                }

                this.GenerateViewPointTree(rootGuid, originViewPoints);
            }
        }

        private void GenerateViewPointTree(string rootGuid, Dictionary<string, ViewPointModel> originViewPoints)
        {
            ViewPoints = new ObservableCollection<ViewPointModel>();

            foreach (var originViewPoint in originViewPoints)
            {
                var parentGuid = originViewPoint.Value.Parent_Id;
                if (string.IsNullOrEmpty(parentGuid)
                    || string.Equals("0", parentGuid))
                {
                    continue;
                }

                if (originViewPoints.ContainsKey(parentGuid))
                {
                    var parentViewPoint= originViewPoints[parentGuid];

                    originViewPoint.Value.Parent = parentViewPoint;
                    parentViewPoint.Children.Add(originViewPoint.Value);
                }
                else
                {
                    ViewPoints.Add(originViewPoint.Value);
                }
            }

            ViewPointModel root;
            if (originViewPoints.TryGetValue(rootGuid, out root))
            {
                ViewPoints.Insert(0, root);
            }
        }

        private void ViewPointCommandExecute(ExCommandParameter parameter)
        {
            #region 参数有效性验证
            if (parameter == null)
            {
                return;
            }

            var eventArgs = parameter.EventArgs as System.Windows.Input.MouseButtonEventArgs;
            if (eventArgs == null)
            {
                return;
            }
            #endregion

            var viewPoint = parameter.Parameter as ViewPointModel;
            if (viewPoint != null && eventArgs.ClickCount == 2)
            {
                EngineHelper.Instance.SetCameraMatrix(viewPoint.Type);
            }
        }
        #endregion

        #region Properties
        public ObservableCollection<ViewPointModel> ViewPoints { get; set; }
        #endregion
    }
}
