using BabaIsYou;
using UnityEngine;

namespace KitaFramework
{
    public class ProcedureChangeScene : ProcedureBase
    {
        private bool m_gotoMenu;

        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            int nextSceneId = (int)procedureOwner.GetData("NextScene");
            m_gotoMenu = nextSceneId == Config.MENU_SCENE_INDEX;

            string[] loadedSceneNames = GameEntry.Scene.GetLoadedSceneAssetNames();
            foreach (string loadedSceneName in loadedSceneNames)
            {
                GameEntry.Scene.UnloadScene(loadedSceneName);
            }

            IDataTable<DRScene> dtScene = GameEntry.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow(nextSceneId);
            GameEntry.Scene.LoadScene(drScene.SceneAssetName);
            SceneInfo.CurrentSceneId = nextSceneId;
        }

        protected internal override void OnUpdate(IFsm<ProcedureManager> procedureOwner, float deltaTime, float realDeltaTime)
        {
            base.OnUpdate(procedureOwner, deltaTime, realDeltaTime);

            if (m_gotoMenu)
            {
                ChangeState<ProcedureMenu>(procedureOwner);
            }
            else
            {
                ChangeState<ProcedureMain>(procedureOwner);
            }
        }
    }
}