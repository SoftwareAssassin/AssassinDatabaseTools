using System;
using Assassin;
using System.Collections.Generic;
using System.Text;

namespace Assassin.Data
{
	public class InsertQueryBuilder : Base, IQueryBuilder
	{
		#region Members
		private List<string> m_fields = null;
		private List<string> m_values = null;
		#endregion
		#region Properties
		public string Table = null;
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

		public InsertQueryBuilder()
		{
			this.Init();
		}
		~InsertQueryBuilder()
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
			this.m_fields.Clear();
			this.m_fields = null;

			this.m_values.Clear();
			this.m_values = null;
		}

		#region Methods
		public void Add(string fieldName, string _value)
		{
			Fields.Add(fieldName);
			if (_value == null)
				Values.Add("NULL");
			else
				Values.Add(Assassin.Convert.ToTSqlValueFormat(_value));
		}
		public void Add(string fieldName, int _value)
		{
			Fields.Add(fieldName);
			Values.Add(Assassin.Convert.ToTSqlValueFormat(_value));
		}
		public void Add(string fieldName, decimal _value)
		{
			Fields.Add(fieldName);
			Values.Add(Assassin.Convert.ToTSqlValueFormat(_value));
		}
		public void Add(string fieldName, double _value)
		{
			Fields.Add(fieldName);
			Values.Add(Assassin.Convert.ToTSqlValueFormat(_value));
		}
		public void Add(string fieldName, bool _value)
		{
			Fields.Add(fieldName);
			Values.Add(Assassin.Convert.ToTSqlValueFormat(_value));
		}
		public void Add(string fieldName, DateTime _value)
		{
			Fields.Add(fieldName);
			Values.Add(Assassin.Convert.ToTSqlValueFormat(_value));
		}
		public void Add(string fieldName, long _value)
		{
			Fields.Add(fieldName);
			Values.Add(Assassin.Convert.ToTSqlValueFormat(_value));
		}
		public void Add(string fieldName, short _value)
		{
			Fields.Add(fieldName);
			Values.Add(Assassin.Convert.ToTSqlValueFormat(_value));
		}
		public void Add(string fieldName, object _value)
		{
			Fields.Add(fieldName);
			Values.Add(Assassin.Convert.ToTSqlValueFormat(_value));
		}
		#endregion

		public new string ToString()
		{
			StringBuilder sql = new StringBuilder();

			//insert into
			sql.Append(" INSERT INTO " + this.Table);

			//fields
			sql.Append(" (");
			for (int i = 0; i < this.Fields.Count; i++)
			{
				if (i != 0)
					sql.Append(",");
				sql.Append(String.Format("[{0}]", this.Fields[i]));
			}
			sql.Append(")");

			//values
			sql.Append(" VALUES (");
			for (int i = 0; i < this.Values.Count; i++)
			{
				if (i != 0)
					sql.Append(",");

				if (this.Values[i] == null)
					sql.Append("NULL");
				else
					sql.Append(this.Values[i]);
			}
			sql.Append(")");

			return sql.ToString();
		}
	}
}
