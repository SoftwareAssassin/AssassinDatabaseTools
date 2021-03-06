﻿///
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
