using KitaFramework;
using System;

namespace BabaIsYou
{
    public static class UIExtension
    {
        public static void OpenUI(this UIManager uiManager, UIFormId uiFormId, object data = null)
        {
            uiManager.OpenUI((int)uiFormId, data);
        }

        public static void OpenUI(this UIManager uiManager, int uiFormId, object data = null)
        {
            // 读取 DataTable，根据 id 获取 uiForm 相关信息
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            if (dtUIForm == null)
            {
                throw new Exception($"Can't find {typeof(DRUIForm)} data table");
            }

            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
            {
                throw new ArgumentException($"Can't find {uiFormId} data row");
            }

            uiManager.OpenUI(drUIForm.Path, drUIForm.GroupName, data);
        }
    }
}