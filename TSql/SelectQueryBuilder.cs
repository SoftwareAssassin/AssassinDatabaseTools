using System;
using Assassin;
using System.Text;
using System.Collections.Generic;

namespace Assassin.Data
{
	public class SelectQueryBuilder : Base, IQueryBuilder
	{
		#region Members
		//table info
		private List<string> m_tables = null;
		private List<string> m_joinTables = null;
		private List<string> m_tableFields = null;
		private List<string> m_joinFields = null;
		private List<string> m_joinTypes = null;
		private List<string> m_tableNames = null;

		//field info
		private List<string> m_fields = null;
		private List<string> m_fieldTables = null;

		//sorting info
		private List<string> m_sortOrders = null;
		private List<string> m_sortOrdersOrder = null;

		//criteria info
		private List<string> m_andArguments = null;
		private List<string> m_orArguments = null;
		#endregion
		#region Properties
		public long? MaxResults = null;
		public string GroupBy = null;
		#endregion

		public SelectQueryBuilder()
		{
			this.Init();
		}
		~SelectQueryBuilder()
		{
			this.Dispose();
		}

		private void Init()
		{
			this.m_tables = new List<string>();
			this.m_joinTables = new List<string>();
			this.m_tableFields = new List<string>();
			this.m_joinFields = new List<string>();
			this.m_joinTypes = new List<string>();
			this.m_fields = new List<string>();
			this.m_fieldTables = new List<string>();
			this.m_sortOrders = new List<string>();
			this.m_sortOrdersOrder = new List<string>();
			this.m_andArguments = new List<string>();
			this.m_orArguments = new List<string>();
			this.m_tableNames = new List<string>();
		}
		private void Dispose()
		{
			//TODO
		}

		#region Methods
		public void AddTable(string tableName)
		{
			this.AddTable(tableName, null, null, null);
		}
		public void AddTable(string tableName, string joinTable, string tableField, string joinField)
		{
			this.AddTable(tableName, joinTable, tableField, joinField, "INNER JOIN");
		}
		public void AddTable(string tableName, string joinTable, string tableField, string joinField, string joinType)
		{
			this.AddTable(tableName, joinTable, tableField, joinField, joinType, tableName);
		}
		public void AddTable(string tableName, string joinTable, string tableField, string joinField, string joinType, string tbName)
		{
			this.m_tables.Add(tableName);
			this.m_joinTables.Add(joinTable);
			this.m_tableFields.Add(tableField);
			this.m_joinFields.Add(joinField);
			this.m_joinTypes.Add(joinType);
			this.m_tableNames.Add(tbName);
		}

		public void AddField(string fieldName)
		{
			this.AddField(fieldName, null);
		}
		public void AddField(string fieldName, string tableName)
		{
			this.AddField(fieldName, fieldName, tableName);
		}
		public void AddField(string fieldName, string knownAs, string tableName)
		{
			if (fieldName != knownAs)
				this.m_fields.Add(fieldName + " AS [" + knownAs + "]");
			else
				this.m_fields.Add(fieldName);
			this.m_fieldTables.Add(tableName);
		}
		public void AddCustomField(string fieldName, string valueFormula)
		{
			this.m_fields.Add("(" + valueFormula + " AS [" + fieldName + "])");
			this.m_fieldTables.Add(null);
		}

		public void AddSortOrder(string sortField)
		{
			this.AddSortOrder(sortField, null);
		}
		public void AddSortOrder(string sortField, string sortTable)
		{
			this.AddSortOrder(sortField, sortTable, Order.Ascending);
		}
		public void AddSortOrder(string sortField, Order order)
		{
			this.AddSortOrder(sortField, null, order);
		}
		public void AddSortOrder(string sortField, string sortTable, Order order)
		{
			string x = null;
			switch (order)
			{
				case Order.Ascending:
					x = "ASC";
					break;
				case Order.Descending:
					x = "DESC";
					break;
			}
			if (sortTable == null)
				this.m_sortOrders.Add("[" + sortField + "]");
			else
				this.m_sortOrders.Add("[" + sortTable + "].[" + sortField + "]");
			this.m_sortOrdersOrder.Add(x);
		}
		public void ClearSortOrders()
		{
			this.m_sortOrders.Clear();
			this.m_sortOrdersOrder.Clear();
		}

		public void AddAndCriteria(string argument)
		{
			this.m_andArguments.Add(argument);
		}
		public void AddOrCriteria(string argument)
		{
			this.m_orArguments.Add(argument);
		}
		#endregion

		public new string ToString()
		{
			StringBuilder sql = new StringBuilder();

			//start select
			sql.Append("SELECT");

			//max results
			if (this.MaxResults.HasValue)
				sql.Append(String.Format(" TOP {0}", this.MaxResults.ToString()));

			//select
			if (this.m_fields.Count == 0)
				sql.Append(" *");
			else
				for (int i = 0; i < this.m_fields.Count; i++)
				{
					if (i == 0)
						sql.Append(" ");
					else
						sql.Append(",");
					if (this.m_fieldTables[i] == null)
						if (this.m_fields[i].StartsWith("(") && this.m_fields[i].EndsWith(")"))
							sql.Append(this.m_fields[i].Substring(1, this.m_fields[i].Length - 2));
						else
							sql.Append(this.m_fields[i]);
					else
						sql.Append(String.Format("{0}.{1}", this.m_fieldTables[i], this.m_fields[i]));
				}

			//from
			for (int i = 0; i < this.m_tables.Count; i++)
				if (i == 0)
					sql.Append(String.Format(" FROM [{0}] AS [{1}]", this.m_tables[i], this.m_tableNames[i]));
				else
					sql.Append(String.Format(" {0} [{1}] AS [{2}] ON [{3}].[{4}] = [{5}].[{6}]", this.m_joinTypes[i], this.m_tables[i], this.m_tableNames[i], this.m_joinTables[i], this.m_joinFields[i], this.m_tableNames[i], this.m_tableFields[i]));

			//where
			bool whereAdded = false;
			if (this.m_andArguments.Count != 0 || this.m_orArguments.Count != 0)
				sql.Append(" WHERE ");
			for (int i = 0; i < this.m_andArguments.Count; i++)
			{
				if (whereAdded)
					sql.Append(" AND");
				sql.Append(String.Format(" ({0})", this.m_andArguments[i]));
				whereAdded = true;
			}
			for (int i = 0; i < this.m_orArguments.Count; i++)
			{
				if (whereAdded)
					sql.Append(" OR");
				sql.Append(String.Format(" ({0})", this.m_orArguments[i]));
				whereAdded = true;
			}

			//group by
			if (this.GroupBy != null)
				sql.Append(String.Format(" GROUP BY {0}", this.GroupBy));

			//order
			for (int i = 0; i < this.m_sortOrders.Count; i++)
				if (i == 0)
					sql.Append(String.Format(" ORDER BY {0} {1}", this.m_sortOrders[i], this.m_sortOrdersOrder[i]));
				else
					sql.Append(String.Format(", {0} {1}", this.m_sortOrders[i].ToString(), this.m_sortOrdersOrder[i].ToString()));

			return sql.ToString();
		}
	}
}
