///
/// Original Author: Software Assassin
/// 
/// GNU All-Permissive License:
/// Copying and distribution of this file, with or without modification,
/// are permitted in any medium without royalty provided the copyright
/// notice and this notice are preserved.  This file is offered as-is,
/// without any warranty.
/// 
/// Source code available at:
/// https://github.com/SoftwareAssassin/AssassinLibrary
/// 
/// Professional support available at:
/// http://www.softwareassassin.com
/// 

using System;
using Assassin;
using System.Collections.Generic;
using Assassin.Data.TSql;

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
