using KitaFramework;

namespace BabaIsYou
{
    public class DRScene : DataRowBase
    {
        public override int Id { get; protected set; }

        public string SceneName { get; private set; }

        public string SceneAssetName { get; private set; }

        public override bool ParseRow(string line)
        {
            string[] datas = line.Split(',');
            bool success = true;

            success &= int.TryParse(datas[1], out int id);
            Id = id;
            SceneName = datas[2];
            SceneAssetName = datas[3];

            return success;
        }
    }
}