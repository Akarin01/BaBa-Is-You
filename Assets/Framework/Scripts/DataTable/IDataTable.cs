namespace KitaFramework
{
    public interface IDataTable<T> where T : DataRowBase
    {
        public string Name { get; }

        public T GetDataRow(int id);

        public void ReadData(string file);
    }
}