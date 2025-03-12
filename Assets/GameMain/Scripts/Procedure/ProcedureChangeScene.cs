﻿using BabaIsYou;
using System;
using System.Collections.Generic;

namespace KitaFramework
{
    public class ProcedureChangeScene : ProcedureBase
    {
        private bool m_gotoMenu;
        private Dictionary<string, bool> m_unloadFlags = new();
        private int nextSceneId;

        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(UnloadSceneCompleteArgs.EventId, OnSceneUnloaded);

            nextSceneId = (int)procedureOwner.GetData("NextScene");
            m_gotoMenu = nextSceneId == Config.MENU_SCENE_INDEX;

            string[] loadedSceneNames = GameEntry.Scene.GetLoadedSceneAssetNames();
            m_unloadFlags.Clear();
            foreach (string loadedSceneName in loadedSceneNames)
            {
                GameEntry.Scene.UnloadScene(loadedSceneName);
                m_unloadFlags.Add(loadedSceneName, false);
            }
        }

        protected internal override void OnUpdate(IFsm<ProcedureManager> procedureOwner, float deltaTime, float realDeltaTime)
        {
            base.OnUpdate(procedureOwner, deltaTime, realDeltaTime);

            foreach (var unloadFlag in m_unloadFlags)
            {
                if (!unloadFlag.Value)
                {
                    return;
                }
            }
            // 场景全部卸载完毕，加载新场景
            IDataTable<DRScene> dtScene = GameEntry.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow(nextSceneId);
            GameEntry.Scene.LoadScene(drScene.SceneAssetName, true);
            SceneInfo.CurrentSceneId = nextSceneId;

            if (m_gotoMenu)
            {
                ChangeState<ProcedureMenu>(procedureOwner);
            }
            else
            {
                ChangeState<ProcedureMain>(procedureOwner);
            }
        }

        protected internal override void OnExit(IFsm<ProcedureManager> procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(UnloadSceneCompleteArgs.EventId, OnSceneUnloaded);

            base.OnExit(procedureOwner, isShutdown);
        }

        private void OnSceneUnloaded(object sender, EventArgs e)
        {
            UnloadSceneCompleteArgs ue = (UnloadSceneCompleteArgs)e;

            if (m_unloadFlags.ContainsKey(ue.UnloadedSceneAssetName))
            {
                m_unloadFlags[ue.UnloadedSceneAssetName] = true;
            }
        }
    }
}