namespace KitaFramework
{
	public abstract class DataRowBase
	{
		public abstract int Id { get; protected set; }

		public abstract bool ParseRow(string line);
	}
}