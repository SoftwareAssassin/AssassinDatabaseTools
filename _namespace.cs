namespace Assassin.Data
{
	public interface IQueryBuilder
	{
		string ToString();
	}
	public interface IDatabaseConnection
	{
		void Dispose();

		//TODO
	}

	public enum ConnectionStatus
	{
		Closed = 1
		, Open = 2
	}
	public enum Order
	{
		Ascending = 1
		, Descending = 2
		, Random = 3
	}
}
