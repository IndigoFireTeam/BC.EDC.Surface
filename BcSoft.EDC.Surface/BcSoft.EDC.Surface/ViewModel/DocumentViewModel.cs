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
    public class DocumentViewModel: DevExpress.Mvvm.BindableBase
    {
        public DocumentViewModel()
        {
            this.InitCommand();
            this.LoadDocuments();
        }

        #region Commands
        public DelegateCommand<ExCommandParameter> OpenDocumentCommand { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<DocumentModel> Documents { get; set; }
        #endregion

        #region Private Methods
        private void InitCommand()
        {
            OpenDocumentCommand = new DelegateCommand<ExCommandParameter>(this.OpenDocumentCommandExecute);
        }

        private void LoadDocuments()
        {
            Documents = new ObservableCollection<DocumentModel>();
            var documents = ApplicationContext.Instance.ProjectHelper.Select<Document>("Select * From Project_Doc");
            if (documents != null)
            {
                foreach (var document in documents)
                {
                    var documentModel = MapperHelper.Mapper<Document, DocumentModel>(document);

                    Documents.Add(documentModel);
                }
            }
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
