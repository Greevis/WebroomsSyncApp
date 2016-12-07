using System;

namespace RhsSync
{
	/// <summary>
	/// Summary description for clsRhsRoomDataFile.
	/// </summary>
	public class clsRhsRoomDataFile : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsRhsRoomDataFile()
		{
		}

		#region Publicly Exposed Variables	

			/// <summary>
			/// Index of Room Type
			/// </summary>
			public int roomType;				//Max Length 2 (assuming 16 bit int)
			/// <summary>
			/// Room Number (e.g. what is used by the reservation file)
			/// </summary>
			public int number;					//Max Length 2 (assuming 16 bit int)
            /// <summary>
            /// Room Alias (e.g. what the RHS worksheet shows)
            /// </summary>
            public int alias;					//Max Length 2 (assuming 16 bit int)
            /// <summary>
            /// Room Alias (e.g. what the RHS worksheet shows)
            /// </summary>
            public string aliasSt;					//Max Length 2 (assuming 16 bit int)
            /// <summary>
            /// Whether this room is allowed to be allocated
            /// </summary>
            public bool affectOccupancy;				//Max Length 1
            /// <summary>
            /// affectOccupancyFromRooms0Dat
            /// </summary>
            public bool affectOccupancyFromRooms0Dat;				//Max Length 1
            /// <summary>
            /// affectOccupancyFromRooms1Dat
			/// </summary>
            public string affectOccupancyFromRooms1Dat;				//Max Length 1

		#endregion

	}
}
