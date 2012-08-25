namespace Assassin.Data
{
	public class ConnectionConfiguration : Base
	{
		public ConnectionConfiguration(string name, string connectionString)
			: this(name, connectionString, null)
		{ }
		public ConnectionConfiguration(string name, string connectionString, string provider)
		{
			this.m_name = name;
			this.m_connectionString = connectionString;
			this.m_provider = provider;
		}

		private string m_name = null;
		private string m_connectionString = null;
		private string m_provider = null;

		public string Name
		{
			get
			{
				return this.m_name;
			}
		}
		public string ConnectionString
		{
			get
			{
				return this.m_connectionString;
			}
		}
		public string Provider
		{
			get
			{
				return this.m_provider;
			}
		}
	}
}
