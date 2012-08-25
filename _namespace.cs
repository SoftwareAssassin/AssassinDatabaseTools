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
