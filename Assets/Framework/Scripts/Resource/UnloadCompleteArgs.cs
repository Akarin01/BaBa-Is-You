namespace KitaFramework
{
    public class UnloadCompleteArgs : BaseEventArgs
    {
        public static int EventId { get; private set; } = typeof(UnloadCompleteArgs).GetHashCode();

        public override int Id => EventId;

        public string UnloadedSceneAssetName { get; private set; }

        public UnloadCompleteArgs(string unloadedSceneAssetName) 
        {
            UnloadedSceneAssetName = unloadedSceneAssetName;
        }
    }
}