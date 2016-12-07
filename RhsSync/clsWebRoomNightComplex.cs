using System;

namespace RhsSync
{
	/// <summary>
	/// Summary description for clsWebRoomNightComplex.
	/// </summary>
	public class clsWebRoomNightComplex : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsWebRoomNightComplex()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Publicly Exposed Variables

//		public struct webRoomNightComplex
//		{
			/// <summary>
			/// Date of first night of stay
			/// </summary>
			public DateTime date;
			/// <summary>
			/// Web Room Type Booked
			/// </summary>
			public int roomType;
			/// <summary>
			/// Number of Adults staying
			/// </summary>
			public int numAdults;
			/// <summary>
			/// Number of Children staying
			/// </summary>
			public int numChildren;
			/// <summary>
			/// Number of Infants staying
			/// </summary>
			public int numInfants;
			/// <summary>
			/// Total Cost of the Room for a Single Night
			/// </summary>
			public decimal costOfRoomPerNight;
//		}
		
		#endregion

	}
}
