using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BcSoft.EDC.Surface.Helper;
using BcSoft.EDC.Surface.Model;
using BcSoft.EDC.Surface.Domain;
using DevExpress.Mvvm;
using BcSoft.EDC.Surface.Commands;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class CruiseViewModel : DevExpress.Mvvm.BindableBase
    {
        public CruiseViewModel()
        {
            this.InitCommand();
            this.LoadCruiseDatas();
        }

        #region Properties
        public ObservableCollection<CruiseModel> CruiseDatas { get; set; }
        #endregion

        #region Command
        public DelegateCommand<ExCommandParameter> PlayCruiseCommand { get; set; }
        #endregion

        #region Private Methods
        private void LoadCruiseDatas()
        {
            var cruiseDatas = ApplicationContext.Instance.ProjectHelper.Select<Cruise>("Select * From Cruise");
            if (cruiseDatas == null)
            {
                return;
            }

            CruiseDatas = new ObservableCollection<CruiseModel>();
            foreach(var cruiseData in cruiseDatas)
            {
                var cruiseDataModel = MapperHelper.Mapper<Cruise, CruiseModel>(cruiseData);

                CruiseDatas.Add(cruiseDataModel);
            }
        }

        private void InitCommand()
        {
            PlayCruiseCommand = new DelegateCommand<ExCommandParameter>(this.PlayCruiseCommandExecute);
        }
        #endregion

        #region Command Methods
        private void PlayCruiseCommandExecute(ExCommandParameter parameter)
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

            var cruise = parameter.Parameter as CruiseModel;
            if (cruise != null && eventArgs.ClickCount == 2)
            {
                EngineHelper.Instance.PlayCruiseData(cruise.Cruise_Data, cruise.Cruise_Viewing.PaiseInt(), cruise.Cruise_Time.RaiseFloat(), (short)cruise.Cruise_Type.PaiseInt());
            }
        }
        #endregion
    }
}
