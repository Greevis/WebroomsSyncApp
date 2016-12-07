using System;

namespace RhsSync
{
	/// <summary>
	/// Summary description for clsRhsPrimaryOccupancyDataFile.
	/// </summary>
	public class clsRhsPrimaryOccupancyDataFile : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsRhsPrimaryOccupancyDataFile()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Publicly Exposed Variables

//		public struct rhsPrimaryOccupancyDataFileStruct Data as presented by the RHS Primary Link file STATAFFT.IDM
//		{
			/// <summary>
			/// Reference Date: Added from aux file. 
			/// Converted to a C# DateTime
			/// </summary>
			public DateTime referenceDate;
			/// <summary>
			/// Reference Date as supplied by the file 
			/// (Max Length 4)
			/// </summary>
			public int dateValue;
			/// <summary>
			/// Initial Reference to the Secondary file (followed to get initial 
			/// secondary record, Max Length 4)
			/// </summary>
			public int firstSecondaryRef;
			/// <summary>
			/// Final Reference to the Secondary file 
			/// (not used, Max Length 4)
			/// </summary>
			public int lastSecondaryRef;
			/// <summary>
			/// Total number of References to the Secondary file
			/// (used as a counter, Max Length 4)
			/// </summary>
			public int numOfSecondaryRefs;
//		}

		#endregion
	}
}
