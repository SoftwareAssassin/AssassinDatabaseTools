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
using System.Data.SqlClient;
using System.Data;

namespace Assassin.Data.TSql
{
	public class StoredProcedure : Base
	{
		#region Members
		private Dictionary<string, SqlParameter> m_parameters = null;
		#endregion
		#region Properties
		public string ProcedureName = null;
		public int ParameterCount
		{
			get
			{
				return this.Parameters.Count;
			}
		}
		public Dictionary<string, SqlParameter> Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}
		#endregion

		#region Construction/Destruction
		public StoredProcedure()
			: this(null)
		{ }
		public StoredProcedure(string procedureName)
		{
			this.Init(procedureName);
		}
		~StoredProcedure()
		{
			this.Dispose();
		}

		private void Init(string procedureName)
		{
			//constructors
			this.m_parameters = new Dictionary<string, SqlParameter>();

			//blah...
			this.ProcedureName = procedureName;
		}
		private void Dispose()
		{
			this.m_parameters.Clear();
			this.m_parameters = null;
		}
		#endregion

		public object this[string key]
		{
			get
			{
				return this.Parameters[key];
			}
			set
			{
				this.Parameters[key].Value = value;
			}
		}
		public void AddParam(SqlParameter param)
		{
			this.Parameters.Add(param.ParameterName, param);
		}

		public SqlParameter[] Execute(string connectionName)
		{
			//prepare command for transaction
			SqlCommand com = new SqlCommand();

			com.CommandText = this.ProcedureName;
			com.CommandType = CommandType.StoredProcedure;

			foreach (KeyValuePair<string, SqlParameter> param in this.Parameters)
				com.Parameters.Add(param.Key, SqlDbType.BigInt).Value = param.Value;

			//execute transaction
			return ConnectionPool.Connections[connectionName].ExecuteStoredProcedure(com);
		}
		private SqlDbType GetTypeFromInput(object input)
		{
			SqlDbType? retval = null;

			if (input == null)
				throw new Exception("Cannot determine type from null");
			else if (input is string)
				retval = SqlDbType.NVarChar;
			else if (input is short)
				retval = SqlDbType.SmallInt;
			else if (input is int)
				retval = SqlDbType.Int;
			else if (input is long)
				retval = SqlDbType.BigInt;
			else if (input is decimal)
				retval = SqlDbType.Decimal;
			else if (input is double)
				retval = SqlDbType.Decimal;
			else if (input is DateTime)
				retval = SqlDbType.DateTime;
			else if (input is bool)
				retval = SqlDbType.Bit;
			else
				throw new Exception("Cannot determine type.");

			return retval.Value;
		}
	}
}
