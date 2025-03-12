namespace KitaFramework
{
    public class UnloadSceneCompleteArgs : BaseEventArgs
    {
        public static int EventId { get; private set; } = typeof(UnloadSceneCompleteArgs).GetHashCode();

        public override int Id => EventId;

        public string UnloadedSceneAssetName { get; private set; }

        public UnloadSceneCompleteArgs(string unloadedSceneAssetName) 
        {
            UnloadedSceneAssetName = unloadedSceneAssetName;
        }
    }
}