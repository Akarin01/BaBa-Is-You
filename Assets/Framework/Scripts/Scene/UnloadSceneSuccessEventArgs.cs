namespace KitaFramework
{
    public class UnloadSceneSuccessEventArgs : BaseEventArgs
    {
        public static int EventId { get; private set; } = typeof(UnloadSceneSuccessEventArgs).GetHashCode();

        public override int Id => EventId;

        public string UnloadedSceneAssetName { get; private set; }

        public object UserData { get; private set; }

        public UnloadSceneSuccessEventArgs(string unloadedSceneAssetName, object userData) 
        {
            UnloadedSceneAssetName = unloadedSceneAssetName;
            UserData = userData;
        }
    }
}