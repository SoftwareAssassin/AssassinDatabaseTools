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
using System.Text;
using System.Collections.Generic;

namespace Assassin.Data
{
	public class UpdateQueryBuilder : Base, IQueryBuilder
	{
		#region Members
		private long m_pk = 0;
		private List<string> m_fields = null;
		private List<string> m_values = null;
		#endregion
		#region Properties
		public string Table = null;
		public string PrimaryKeyField = null;

		public long PrimaryKey
		{
			get
			{
				return this.m_pk;
			}
			set
			{
				this.m_pk = value;
			}
		}
		public long Record
		{
			get
			{
				return this.m_pk;
			}
			set
			{
				this.m_pk = value;
			}
		}
		public long Row
		{
			get
			{
				return this.m_pk;
			}
			set
			{
				this.m_pk = value;
			}
		}

		public List<string> Fields
		{
			get
			{
				return this.m_fields;
			}
		}
		public List<string> Values
		{
			get
			{
				return this.m_values;
			}
		}

		#endregion

		public UpdateQueryBuilder()
		{
			this.Init();
		}
		~UpdateQueryBuilder()
		{
			this.Dispose();
		}

		private void Init()
		{
			this.m_fields = new List<string>();
			this.m_values = new List<string>();
		}
		private void Dispose()
		{
		}

		#region Methods
		public void Add(string fieldName, object _value)
		{
			Fields.Add(fieldName);
			Values.Add(Assassin.Convert.ToTSqlValueFormat(_value));
		}
		#endregion

		public new string ToString()
		{
			StringBuilder sql = new StringBuilder();

			//update
			sql.Append(String.Format(" UPDATE {0}", this.Table));

			//set
			sql.Append(" SET ");
			for (int i = 0; i < this.Fields.Count; i++)
			{
				if (i != 0)
					sql.Append(",");
				sql.Append(String.Format("[{0}] = {1}", this.Fields[i], this.Values[i]));
			}

			//where
			sql.Append(String.Format(" WHERE ( [{0}] = {1} )", this.PrimaryKeyField, this.PrimaryKey));

			return sql.ToString();
		}
	}
}
