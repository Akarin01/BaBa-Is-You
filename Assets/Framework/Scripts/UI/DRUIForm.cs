namespace KitaFramework
{
    public class DRUIForm : DataRowBase
    {
        private int m_id;

        public override int ID => m_id;

        public string UIName { get; private set; }

        public string Path { get; private set; }

        public string GroupName { get; private set; }

        public override bool ParseRow(string line)
        {
            string[] datas = line.Split(',');
            bool success = true;

            success &= int.TryParse(datas[1], out m_id);
            UIName = datas[2];
            Path = datas[3];
            GroupName = datas[4];

            return success;
        }
    }
}