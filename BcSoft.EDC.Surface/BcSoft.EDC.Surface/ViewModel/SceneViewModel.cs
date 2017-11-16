using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BcSoft.EDC.Surface.Domain;
using BcSoft.EDC.Surface.Model;
using BcSoft.EDC.Surface.Helper;
using DevExpress.Mvvm;
using BcSoft.EDC.Surface.Commands;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class SceneViewModel : DevExpress.Mvvm.ViewModelBase
    {
        public SceneViewModel()
        {
            this.InitCommand();
            this.LoadSceneDatas();
        }

        #region Properties
        public ObservableCollection<CatalogTreeModel> CatalogDataTrees { get; set; }
        public ObservableCollection<CatalogTreeModel> HideCatalogDatas { get; set; }
        #endregion

        #region Command
        public DelegateCommand<ExCommandParameter> CatalogCommand { get; set; }
        #endregion

        #region Private Methods
        private void InitCommand()
        {
            CatalogCommand = new DelegateCommand<ExCommandParameter>(this.CatalogCommandExecute);
        }

        private void LoadSceneDatas()
        {
            var originCatalogDatas = new Dictionary<string,CatalogTreeModel>();

            var catalogDatas = ApplicationContext.Instance.ProjectHelper.Select<CatalogTree>("Select * From model_catalog_tree");
            if (catalogDatas != null)
            {
                string rootGuid = string.Empty;

                foreach (var catalogTree in catalogDatas)
                {
                    if(string.IsNullOrEmpty(catalogTree.Parent_Guid))
                    {
                        rootGuid = catalogTree.Guid;
                    }

                    var catalogtreeModel = MapperHelper.Mapper<CatalogTree, CatalogTreeModel>(catalogTree);
                    originCatalogDatas.Add(catalogTree.Guid, catalogtreeModel);
                }

                this.GenerateCatalogDataTree(rootGuid, originCatalogDatas);
            }
        }

        private void GenerateCatalogDataTree(string rootGuid, Dictionary<string, CatalogTreeModel> originCatalogDatas)
        {
            foreach(var originCatalogData in originCatalogDatas)
            {
                var parentGuid = originCatalogData.Value.Parent_Guid;
                if (string.IsNullOrEmpty(parentGuid))
                {
                    continue;
                }

                if(originCatalogDatas.ContainsKey(parentGuid))
                {
                    var parentCatalogData = originCatalogDatas[parentGuid];

                    originCatalogData.Value.Parent = parentCatalogData;
                    parentCatalogData.Children.Add(originCatalogData.Value);
                }
            }

            CatalogTreeModel root;
            if(originCatalogDatas.TryGetValue(rootGuid, out root))
            {
                CatalogDataTrees = new ObservableCollection<CatalogTreeModel>(root.Children.ToList<CatalogTreeModel>());
            }
        }

        private void CatalogCommandExecute(ExCommandParameter parameter)
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

            var catalog = parameter.Parameter as CatalogTreeModel;
            if (catalog != null && eventArgs.ClickCount == 2)
            {
                EngineHelper.Instance.LocateEntity(catalog.Guid);
            }
        }
        #endregion
    }
}
