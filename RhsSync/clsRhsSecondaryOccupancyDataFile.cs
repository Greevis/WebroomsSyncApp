using System;

namespace RhsSync
{
	/// <summary>
	/// Summary description for clsRhsSecondaryOccupancyDataFile.
	/// </summary>
	public class clsRhsSecondaryOccupancyDataFile : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsRhsSecondaryOccupancyDataFile()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Publicly Exposed Variables

		//		public struct rhsSecondaryOccupancyDataFileStruct
//		{
			/// <summary>
			/// Reference Date. 
			/// Converted to a C# DateTime
			/// </summary>
			public DateTime referenceDate;
			/// <summary>
			/// Reference Date as supplied by the file (Max Length 4)
			/// </summary>
			public int dateValue;
			/// <summary>
			/// Reference to the next Secondary Record
			/// (Max Length 4)
			/// </summary>
			public int nextSecondaryRef;
			/// <summary>
			/// Reference to the previous Secondary Record
			/// (not used, Max Length 4)
			/// </summary>
			public int prevSecondaryRef;
			/// <summary>
			/// Reference to the Tertiary Record that goes with this Secondary Record
			/// (Max Length 4)
			/// </summary>
			public int tertiaryRef;
//		}

		#endregion

	}
}
