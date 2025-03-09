namespace KitaFramework
{
	public abstract class DataRowBase
	{
		public abstract int Id { get; }

		public abstract bool ParseRow(string line);
	}
}