namespace KitaFramework
{
	public abstract class DataRowBase
	{
		public abstract int ID { get; }

		public abstract bool ParseRow(string line);
	}
}