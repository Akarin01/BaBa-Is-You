using KitaFramework;

namespace BabaIsYou
{
    public class ProcedureLauncher : ProcedureBase
    {
        private const string UIFORM_DATA_TABLE_NAME = "UIForm";
        private const string SCENE_DATA_TABLE_NAME = "Scene";

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
            dtUIForm.ReadData(UIFORM_DATA_TABLE_NAME);

            IDataTable<DRScene> dtScene = GameEntry.DataTable.CreateDataTable<DRScene>(null);
            dtScene.ReadData(SCENE_DATA_TABLE_NAME);
        }
    }
}