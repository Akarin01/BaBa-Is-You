using KitaFramework;
using System;
using UnityEngine;

namespace BabaIsYou
{
    public class ProcedureLauncher : ProcedureBase
    {
        private const string m_dataTableFileName = "UIFormDataTable";

        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            LoadDataTable();

            procedureOwner.SetData("NextScene", Config.MENU_SCENE_INDEX);
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }

        private void LoadDataTable()
        {
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.CreateDataTable<DRUIForm>(null);
            dtUIForm.ReadData(m_dataTableFileName);
        }
    }
}