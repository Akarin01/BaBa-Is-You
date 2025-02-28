namespace KitaFramework
{
    public interface IUIManager
    {
        public void OpenUI<T>() where T : UIForm;

        public void CloseUI(UIForm uiForm);
    }
}