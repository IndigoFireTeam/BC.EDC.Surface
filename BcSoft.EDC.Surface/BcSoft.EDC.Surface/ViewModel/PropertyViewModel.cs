using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BcSoft.EDC.Surface.Model;
using BcSoft.EDC.Surface.Domain;
using DevExpress.Mvvm;
using BcSoft.EDC.Surface.Commands;
using BcSoft.EDC.Surface.Helper;
using System.Collections.ObjectModel;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class PropertyViewModel : DevExpress.Mvvm.BindableBase
    {
        private string m_ModelGuid;

        public PropertyViewModel(string modelGuid)
        {
            m_ModelGuid = modelGuid;

            this.Init();
        }

        #region Properties
        public List<PropertyModel> Properties { get; set; }
        public string ModelName { get; set; }
        public ObservableCollection<DocumentModel> Documents { get; set; }
        #endregion

        #region Commands
        public DelegateCommand<ExCommandParameter> OpenDocumentCommand { get; set; }
        #endregion

        #region Private Methods
        private void Init()
        {
            this.InitCommand();
            this.InitModelProperty();
            this.InitModelFile();
        }

        private void InitModelProperty()
        {
            var querySql = string.Format("Select name, property_tree From model_catalog_tree Where Guid='{0}'", m_ModelGuid);
            var catalogTree = ApplicationContext.Instance.ProjectHelper.SelectSingle<CatalogTree>(querySql);

            if (catalogTree != null)
            {
                ModelName = catalogTree.Name;
                Properties = JsonHelpercs.Deserialize<List<PropertyModel>>(catalogTree.OriginPropertyTree);
            }
        }

        private void InitModelFile()
        {
            Documents = new ObservableCollection<DocumentModel>();

            var querySql = string.Format("Select * From Project_Doc Where Guid='{0}'", m_ModelGuid);
            var documents = ApplicationContext.Instance.ProjectHelper.Select<Document>(querySql);
            if (documents != null)
            {
                foreach (var document in documents)
                {
                    var documentModel = MapperHelper.Mapper<Document, DocumentModel>(document);
                    Documents.Add(documentModel);
                }
            }
        }

        private void InitCommand()
        {
            OpenDocumentCommand = new DelegateCommand<ExCommandParameter>(this.OpenDocumentCommandExecute);
        }
        #endregion

        #region Command Methods
        private void OpenDocumentCommandExecute(ExCommandParameter parameter)
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

            var documentModel = parameter.Parameter as DocumentModel;
            if (documentModel != null && eventArgs.ClickCount == 2)
            {
                var fileFullPath = PathUtils.GetDocumentPath(documentModel.Project_Id, string.Format("{0}.{1}", documentModel.File_Id, documentModel.Type));
                if (System.IO.File.Exists(fileFullPath))
                {
                    DocumentViewerHelper.Show(documentModel.Name, fileFullPath);
                }
            }
        }
        #endregion
    }
}
