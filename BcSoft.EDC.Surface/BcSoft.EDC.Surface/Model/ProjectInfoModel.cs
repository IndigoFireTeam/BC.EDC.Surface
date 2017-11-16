using BcSoft.EDC.Surface.Domain;
using BcSoft.EDC.Surface.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.Model
{
    public class ProjectInfoModel
    {
        public string Name { get; set; }
        public string Guid { get; set; }
        public string PreviewImage { get; set; }
        public bool IsLoaded { get; set; }

        private bool isFlying;
        public void LoadScene()
        {
            if(IsLoaded)
            {
                return;
            }

            var sceneInfos = ApplicationContext.Instance.ProjectHelper.Select<Scenes>("Select * From Scene");
            if (sceneInfos == null)
            {
                return;
            }

            var sceneModels = new List<SceneModel>();
            foreach (var scene in sceneInfos)
            {
                var sceneModel = MapperHelper.Mapper<Scenes, SceneModel>(scene);
                sceneModels.Add(sceneModel);
            }

            PathUtils.SetScenesPath(ApplicationContext.Instance.CurrentProject.Guid, sceneModels);
            foreach (var sceneModel in sceneModels)
            {
                if (!System.IO.File.Exists(sceneModel.ScenePath))
                {
                    continue;
                }

                EngineHelper.Instance.AddEarthScene(sceneModel.ScenePath,
                sceneModel.Longitude.RaiseFloat(),
                sceneModel.Latitude.RaiseFloat(),
                sceneModel.Altitude.RaiseFloat(),
                0, 0, 0, sceneModel.Rotate.RaiseFloat(), 1);

                EngineHelper.Instance.SetEarthSceneViewpoint(sceneModel.Longitude.RaiseFloat(),
                sceneModel.Latitude.RaiseFloat(),
                sceneModel.Altitude.RaiseFloat(),
                0, -30, 400, 2);
                //如果需要场地平整，则调用接口进行平整
                /*if(!string.IsNullOrEmpty(sceneModel.Relative_Height))
                {
                    EngineHelper.Instance.FlattenTerrain(sceneModel.Longitude.RaiseFloat(), sceneModel.Latitude.RaiseFloat(), sceneModel.Relative_Height.RaiseFloat(), sceneModel.X.RaiseFloat(), sceneModel.Y.RaiseFloat(), sceneModel.Z.RaiseFloat(), sceneModel.Rotate.RaiseFloat(), sceneModel.Terrain);
                }*/
            }

            IsLoaded = true;
            isFlying = false;
        }

        public void Flying()
        {
            if (!IsLoaded)
            {
                return;
            }
            if(isFlying)
            {
                EngineHelper.Instance.CancelFlyCruise();
                isFlying = false;
                return;
            }
            isFlying = true;
            
            EngineHelper.Instance.PlayFlyCruise(GetFlyingPath(), 70, "");  



        }

        private string GetFlyingPath()
        {
            /*
             var sceneInfos = ApplicationContext.Instance.ProjectHelper.Select<Scenes>("Select * From Scene");
            if (sceneInfos == null)
            {
                return;
            }
            List<Scenes> sceneList = new List<Scenes>(sceneInfos);
            StringBuilder flyPathDatas=new StringBuilder();
            for(int i=sceneList.Count-1;i>=0; i--)
            {
                if (sceneList[i].Rotate=="0")//临时用这个方法判断是否为线路，后续再修改
                {
                    flyPathDatas.Append(string.Format(@"{0},{1},{2};", sceneList[i].Longitude, sceneList[i].Latitude, sceneList[i].Altitude + 30));
                }
                
            }
            flyPathDatas =flyPathDatas.Remove(flyPathDatas.Length - 1, 1);
            return flyPathDatas.ToString();
            */
            return "119.9795837402344,30.09819984436035,236.0;119.9779815673828,30.09820556640625,171.0;119.9758758544922,30.0972785949707,156.0000610351563;119.9758224487305,30.09700965881348,138.00022888183588;119.9776992797852,30.09922409057617,190.0;119.9772491455078,30.09856605529785,161.0;119.9763641357422,30.09780311584473,151.0;119.9756622314453,30.09748458862305,166.0;119.9758224487305,30.09700965881348,138.00022888183588";
        }
    }
}
