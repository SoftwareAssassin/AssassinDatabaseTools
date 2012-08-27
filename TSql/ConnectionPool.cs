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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace Assassin.Data.TSql
{
	public class ConnectionPool : Base, IDatabaseConnection
	{
		#region Static State
		private static ConnectionCollection m_databases = null;
		public static ConnectionCollection Connections
		{
			get
			{
				if (ConnectionPool.m_databases == null)
					ConnectionPool.InitDatabases();

				return ConnectionPool.m_databases;
			}
		}

		private static void InitDatabases()
		{
			//construct collection
			ConnectionPool.m_databases = new ConnectionCollection();

			//populate collection
			for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
			{
				ConnectionConfiguration config = new ConnectionConfiguration(ConfigurationManager.ConnectionStrings[i].Name
					, ConfigurationManager.ConnectionStrings[i].ConnectionString
					, ConfigurationManager.ConnectionStrings[i].ProviderName
					);
				ConnectionPool.Connections.Add(new ConnectionPool(config));
			}
		}
		#endregion

		#region Members
		private ConnectionConfiguration m_configuration = null;
		private ConnectionStatus m_status = ConnectionStatus.Closed;

		private bool disposed = false;
		#endregion
		#region Properties
		public ConnectionConfiguration Configuration
		{
			get
			{
				return this.m_configuration;
			}
		}
		public ConnectionStatus Status
		{
			get
			{
				return this.m_status;
			}
		}
		#endregion

		private void Init(ConnectionConfiguration config)
		{
			this.m_configuration = config;

			//TODO
		}
		public void Dispose()
		{
			if (!this.disposed)
			{
				//TODO
				this.disposed = true;
			}
		}

		#region Methods
		public int ExecuteNonQuery(string sql)
		{
			SqlConnection con = new SqlConnection(this.Configuration.ConnectionString);
			con.Open();
			this.m_status = ConnectionStatus.Open;

			SqlCommand command = new SqlCommand();
			command.CommandText = sql;
			command.CommandType = System.Data.CommandType.Text;
			command.Connection = con;

			int retval = command.ExecuteNonQuery();

			command.Dispose();
			command = null;

			con.Close();
			con.Dispose();
			con = null;

			return retval;
		}
		public object ExecuteScalar(string sql)
		{
			object retval = null;

			SqlConnection con = new SqlConnection(this.Configuration.ConnectionString);
			con.Open();
			this.m_status = ConnectionStatus.Open;

			SqlCommand command = new SqlCommand(sql, con);
			command.CommandText = sql;

			retval = command.ExecuteScalar();

			command.Dispose();
			command = null;

			con.Close();
			con.Dispose();
			con = null;

			return retval;
		}
		public DataSet RetrieveDataSet(string sql)
		{
			return this.RetrieveDataSet(sql, -1);
		}
		public DataSet RetrieveDataSet(string sql, int maxRows)
		{
			DataSet retval = new DataSet();

			SqlConnection con = new SqlConnection(this.Configuration.ConnectionString);
			con.Open();
			this.m_status = ConnectionStatus.Open;

			SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
			if (maxRows > 0)
				adapter.Fill(retval, 0, maxRows, null);
			else
				adapter.Fill(retval);

			adapter.Dispose();
			adapter = null;

			con.Close();
			con.Dispose();
			con = null;

			return retval;
		}
		public SqlParameter[] ExecuteStoredProcedure(SqlCommand command)
		{
			SqlParameter[] retval = null;

			SqlConnection con = new SqlConnection(this.Configuration.ConnectionString);
			con.Open();
			this.m_status = ConnectionStatus.Open;

			//prepare command
			command.Connection = con;
			command.CommandType = CommandType.StoredProcedure;

			//execute
			command.ExecuteReader();
			command.Parameters.CopyTo(retval, 0);

			//dispose
			con.Close();
			con.Dispose();
			con = null;

			return retval;
		}
		#endregion

		#region Event Handlers
		public ConnectionPool(ConnectionConfiguration config)
		{
			this.Init(config);
		}
		~ConnectionPool()
		{
			this.Dispose();
		}
		#endregion
	}
}
