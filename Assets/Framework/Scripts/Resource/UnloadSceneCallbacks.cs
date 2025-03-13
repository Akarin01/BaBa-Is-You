namespace KitaFramework
{
    public class UnloadSceneCallbacks
    {
        public UnloadSceneSuccessCallback UnloadSceneSuccessCallback { get; private set; }
        public UnloadSceneFailureCallback UnloadSceneFailureCallback { get; private set; }

        public UnloadSceneCallbacks(UnloadSceneSuccessCallback unloadSceneSuccessCallback,
            UnloadSceneFailureCallback unloadSceneFailureCallback)
        {
            UnloadSceneSuccessCallback = unloadSceneSuccessCallback;
            UnloadSceneFailureCallback = unloadSceneFailureCallback;
        }
    }
}