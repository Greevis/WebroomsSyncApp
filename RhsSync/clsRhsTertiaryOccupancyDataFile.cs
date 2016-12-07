using System;

namespace RhsSync
{
	/// <summary>
	/// Summary description for clsRhsTertiaryOccupancyDataFile.
	/// </summary>
	public class clsRhsTertiaryOccupancyDataFile : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsRhsTertiaryOccupancyDataFile()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Publicly Exposed Variables

//		public struct rhsTertiaryOccupancyDataFileStruct
//		{
			/// <summary>
			/// First Date of booking.
			/// Converted to a C# DateTime
			/// </summary>
			public DateTime startRefDate;			//Added after inital gathering of data
			/// <summary>
			/// Last Date of booking.
			/// Converted to a C# DateTime
			/// </summary>
			public DateTime endRefDate;				//Added after inital gathering of data
			/// <summary>
			/// First Date of booking.
			/// In initial form
			/// </summary>
			public int startDateValue;					//Max Length 4
			/// <summary>
			/// Last Date of booking.
			/// In initial form
			/// </summary>
			public int endDateValue;					//Max Length 4
			/// <summary>
			/// Status for this booking
			/// </summary>
			public rhsBookingType bookingStatus;			//New amalgam
			/// <summary>
			/// Type for this booking
			/// </summary>
			public int bookingType;					//Max Length 1
			/// <summary>
			/// 'Character' associated with this booking
			/// </summary>
			public string bookingChar;				//Max Length 1
			/// <summary>
			/// Room Number (not used)
			/// </summary>
			public short room;						//Max Length 2
			/// <summary>
			/// Number of rooms booked of each type for the given period
			/// </summary>
			public int[] roomTypesAffected;					//Max Length 32 (8 bit ints)
//		}

		#endregion

	}
}
