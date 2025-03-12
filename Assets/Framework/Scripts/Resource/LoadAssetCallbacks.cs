namespace KitaFramework
{
    public class LoadAssetCallbacks
    {
        public LoadAssetSuccessCallback LoadAssetSuccessCallback { get; private set; }
        public LoadAssetFailureCallback LoadAssetFailureCallback { get; private set; }

        public LoadAssetCallbacks(LoadAssetSuccessCallback loadAssetSuccessCallback, LoadAssetFailureCallback loadAssetFailureCallback)
        {
            LoadAssetSuccessCallback = loadAssetSuccessCallback;
            LoadAssetFailureCallback = loadAssetFailureCallback;
        }
    }
}