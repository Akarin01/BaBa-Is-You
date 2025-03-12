namespace KitaFramework
{
    public class LoadSceneCallbacks
    {
        public LoadSceneSuccessCallback LoadSceneSuccessCallback { get; private set; }
        public LoadSceneFailureCallback LoadSceneFailureCallback { get; private set; }

        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback, 
            LoadSceneFailureCallback loadSceneFailureCallback)
        {
            LoadSceneSuccessCallback = loadSceneSuccessCallback;
            LoadSceneFailureCallback = loadSceneFailureCallback;
        }
    }
}