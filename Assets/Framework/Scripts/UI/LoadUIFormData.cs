namespace KitaFramework
{
    public class LoadUIFormData
    {
        public string GroupName { get; private set; }
        public object Data { get; private set; }

        public LoadUIFormData(string groupName, object data)
        {
            GroupName = groupName;
            Data = data;
        }
    }
}