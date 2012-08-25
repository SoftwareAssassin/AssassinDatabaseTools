using System;
using Assassin;
using System.Collections.Generic;

namespace Assassin.Data
{
	public class ConnectionCollection : Base
	{
		#region Members
		private List<ConnectionPool> cons = null;
		#endregion
		#region Properties
		public int Count
		{
			get
			{
				return this.cons.Count;
			}
		}
		#endregion

		public ConnectionCollection()
		{
			this.Init();
		}
		~ConnectionCollection()
		{
			this.Dispose();
		}

		private void Init()
		{
			this.cons = new List<ConnectionPool>();
		}
		private void Dispose()
		{
			//dispose connections
			for (int i = 0; i < this.Count; i++)
				this[i].Dispose();

			this.cons.Clear();
			this.cons = null;

			//whatever else...
			//TODO
		}

		public ConnectionPool this[int index]
		{
			get
			{
				return this.cons[index];
			}
		}
		public ConnectionPool this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
					if (this[i].Configuration.Name == name)
						return this[i];
				return null;
			}
		}

		public void Add(ConnectionPool c)
		{
			this.cons.Add(c);
		}
	}
}
