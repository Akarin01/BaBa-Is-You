namespace KitaFramework
{
    public class UnloadAssetCallbacks
    {
        public UnloadAssetSuccessCallback UnloadAssetSuccessCallback { get; private set; }
        public UnloadAssetFailureCallback UnloadAssetFailureCallback { get; private set; }

        public UnloadAssetCallbacks(UnloadAssetSuccessCallback unloadAssetSuccessCallback, UnloadAssetFailureCallback unloadAssetFailureCallback)
        {
            UnloadAssetSuccessCallback = unloadAssetSuccessCallback;
            UnloadAssetFailureCallback = unloadAssetFailureCallback;
        }
    }
}