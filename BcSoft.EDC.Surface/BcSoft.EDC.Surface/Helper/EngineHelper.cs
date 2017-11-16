using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.Helper
{
    public class EngineHelper
    {
        public static EngineHelper Instance;

        private bool m_IsLoaded;
        private Action<string> SelectionChangedCallback;
        private int m_CurrentPickModel = 0;

        static EngineHelper()
        {
            Instance = new EngineHelper();
        }

        private EngineHelper()
        {
            EngineView = new AxBcUgOcxLib.AxBcUgOcx();
        }

        #region Properties
        internal AxBcUgOcxLib.AxBcUgOcx EngineView { get; private set; }
        #endregion

        #region Public Methods
        public void CreateEarthScene()
        {
            if(!m_IsLoaded)
            {
                m_IsLoaded = true;

                EngineView.CreateEarthScene(1, 0, ApplicationContext.Instance.SystemConfig.GisServerAddress);
                EngineView.ShowEarthSceneCompass(0);
                EngineView.Run();
                EngineView.FireCurrentSelectionSetChanged += EngineView_FireCurrentSelectionSetChanged;

                this.InitEarthViewPoint();
            }

            //重置引擎拾取模式
            m_CurrentPickModel = 0;
            EngineView.SetPickMode(0);
        }

        public string AddEarthScene(string sceneFile, float lon, float lat, float height, float xOffset, float yOffset, float zOffset, float angle, short light)
        {
            if (m_IsLoaded)
            {
                return EngineView.AddEarthScene(sceneFile, lon, lat, height, xOffset, yOffset, zOffset, angle, light);
            }

            return null;
        }

        public void SetEarthSceneViewpoint(float lon, float lat, float z, float heading, float pitch, float range, float duration)
        {
            if (m_IsLoaded)
            {
                EngineView.SetEarthSceneViewpoint(lon, lat, z, heading, pitch, range, duration);
            }
        }

        public void ClearScene()
        {
            if(m_IsLoaded)
            {
                EngineView.CancelCruise();
                EngineView.ClearScene();
                EngineView.ClearCurrentSelectionSet();
            }
        }

        public void InitEarthViewPoint()
        {
            if (m_IsLoaded)
            {
                EngineView.SetEarthSceneViewpoint(116.3499985f, 39.93999863f, 0, 0, -90, 38268820, 0);
            }
        }
         
        public void LocateEntity(string tags)
        {
            if(m_IsLoaded)
            {
                EngineView.LocateEntity(tags, 1, 0.0f);
            }
        }

        public void SetCameraMatrix(string matrixOriginData)
        {
            if (m_IsLoaded)
            {
                if (string.IsNullOrEmpty(matrixOriginData))
                {
                    return;
                }

                float a00, a01, a02, a03, a10, a11, a12, a13, a20, a21, a22, a23, a30, a31, a32, a33;
                var matrixData = matrixOriginData.Split(new char[] { ';' });
                if (matrixData.Length != 16)
                {
                    return;
                }

                a00 = matrixData[0].RaiseFloat();
                a01 = matrixData[1].RaiseFloat();
                a02 = matrixData[2].RaiseFloat();
                a03 = matrixData[3].RaiseFloat();
                a10 = matrixData[4].RaiseFloat();
                a11 = matrixData[5].RaiseFloat();
                a12 = matrixData[6].RaiseFloat();
                a13 = matrixData[7].RaiseFloat();
                a20 = matrixData[8].RaiseFloat();
                a21 = matrixData[9].RaiseFloat();
                a22 = matrixData[10].RaiseFloat();
                a23 = matrixData[11].RaiseFloat();
                a30 = matrixData[12].RaiseFloat();
                a31 = matrixData[13].RaiseFloat();
                a32 = matrixData[14].RaiseFloat();
                a33 = matrixData[15].RaiseFloat();

                EngineView.SetCameraMatrix(a00, a01, a02, a03, a10, a11, a12, a13, a20, a21, a22, a23, a30, a31, a32, a33);
            }
        }

        public void PlayCruiseData(string data, int overlook, float duration, short flag)
        {
            if (m_IsLoaded)
            {
                if(string.IsNullOrEmpty(data))
                {
                    return;
                }

                EngineView.PlayCruiseData(data, overlook, duration, flag);
            }
        }

        public void Move(double x,double y)
        {
            if(m_IsLoaded)
            {
                EngineView.Move(x, y);
            }
        }

        public void RegisterSelectionSetChangedCallback(Action<string> callback)
        {
            SelectionChangedCallback = null;
            SelectionChangedCallback += callback;
        }

        /// <summary>
        /// //0-不支持拾取（默认），1-单选，2-多选
        /// </summary>
        public void SetPickMode()
        {
            if(m_IsLoaded)
            {
                if (m_CurrentPickModel == 1)
                {
                    m_CurrentPickModel = 0;
                    EngineView.SetPickMode(0);
                }
                else if (m_CurrentPickModel == 0)
                {
                    m_CurrentPickModel = 1;
                    EngineView.SetPickMode(1);
                }
            }
        }

        //场地平整
        public void FlattenTerrain(float sceneLon, float sceneLat, float sceneHeight, float xOffset, float yOffset, float zOffset, double angle, string pnts)
        {
            if (m_IsLoaded)
            {
                EngineView.FlattenTerrain(sceneLon, sceneLat, sceneHeight, xOffset, yOffset, zOffset, angle, pnts);
            }
        }
        /// <summary>
        /// 飞行
        /// </summary>
        /// <param name="pnts">飞行的路径，由经纬度和海拔组成</param>
        /// <param name="duration">飞行的时间（秒）</param>
        /// <param name="modelPath">无人机模型，如为空则使用默认无人机模型</param>
        public void PlayFlyCruise(string pnts, float duration, string modelPath)
        {
            if(m_IsLoaded)
            {
                EngineView.PlayFlyCruise(pnts, duration, modelPath);
            }
        }
        /// <summary>
        /// 取消飞行
        /// </summary>
        public void CancelFlyCruise()
        {
            if(m_IsLoaded)
            {
                EngineView.CancelFlyCruise();
            }
        }
        #endregion

        #region Event Methods
        private void EngineView_FireCurrentSelectionSetChanged(object sender, AxBcUgOcxLib._DBcUgOcxEvents_FireCurrentSelectionSetChangedEvent e)
        {
            if (SelectionChangedCallback != null)
            {
                this.SelectionChangedCallback(e.newTags);
            }
        }
        #endregion
    }
}
