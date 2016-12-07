using System;
using System.Collections;

namespace RhsSync
{
	/// <summary>
	/// Summary description for clsRhsRoomType.
	/// </summary>
	public class clsRhsRoomType : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsRhsRoomType()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region Publicly exposed variables

//		public struct rhsRoomStruct
//		{
			/// <summary>
			/// Room Type Name
			/// </summary>
			public string roomTypeName;
			/// <summary>
			/// Room Type Code
			/// </summary>
			public string roomTypeCode;
			/// <summary>
			/// Room Type Id (Ordinal)
			/// </summary>
			public int roomTypeId;
			/// <summary>
			/// Number of Rooms of this type in the database
			/// </summary>
			public int numberOfRooms;
			/// <summary>
			/// If Rooms of this type 'are allowed' to be occupied.
			/// </summary>
			public int affectOccupancy;
//		}

		/// <summary>
		/// Indicates whether this RHS Room has been found in the latest list
		/// </summary>
		public bool foundInLatestList;
		
		#region Availability

		/// <summary>
		/// Availability of this Web room type
		/// </summary>
		public ArrayList Availability;

		/// <summary>
		/// A semi colon delimited list of the Availability of this Room Type
		/// </summary>
		/// <returns>A semi colon delimited list of the Availability of this Room Type</returns>
		public string AvailabilityAsDelimitedList
		{
			get 
			{

				string thisRetVal = "";
				if (Availability == null)
					return thisRetVal;

				for(int counter = 0; counter < Availability.Count; counter++)
					thisRetVal += ((int) Availability[counter]).ToString() + ";";

				return thisRetVal;
			}
			set
			{
				string thisList = value;
				Availability = new ArrayList();
				int semiColonIndex = thisList.IndexOf(";");
				while (semiColonIndex > 0)
				{
					string thisRhsRoomType = thisList.Substring(0, semiColonIndex);

					if (isNumerical(thisRhsRoomType))
						Availability.Add(Convert.ToInt32(thisRhsRoomType));
					thisList = thisList.Substring(semiColonIndex + 1);
					semiColonIndex = thisList.IndexOf(";");
				}
			}
		}

		#endregion

		#region AvailabilityAtTimeOfLastUpdate

		/// <summary>
		/// AvailabilityAtTimeOfLastUpdate of this Web room type
		/// </summary>
		public ArrayList AvailabilityAtTimeOfLastUpdate;

		/// <summary>
		/// A semi colon delimited list of the AvailabilityAtTimeOfLastUpdate of this Room Type
		/// </summary>
		/// <returns>A semi colon delimited list of the AvailabilityAtTimeOfLastUpdate of this Room Type</returns>
		public string AvailabilityAtTimeOfLastUpdateAsDelimitedList
		{
			get 
			{

				string thisRetVal = "";
				if (AvailabilityAtTimeOfLastUpdate == null)
					return thisRetVal;

				for(int counter = 0; counter < AvailabilityAtTimeOfLastUpdate.Count; counter++)
					thisRetVal += ((int) AvailabilityAtTimeOfLastUpdate[counter]).ToString() + ";";

				return thisRetVal;
			}
			set
			{
				string thisList = value;
				AvailabilityAtTimeOfLastUpdate = new ArrayList();
				int semiColonIndex = thisList.IndexOf(";");
				while (semiColonIndex > 0)
				{
					string thisRhsRoomType = thisList.Substring(0, semiColonIndex);

					if (isNumerical(thisRhsRoomType))
						AvailabilityAtTimeOfLastUpdate.Add(Convert.ToInt32(thisRhsRoomType));
					thisList = thisList.Substring(semiColonIndex + 1);
					semiColonIndex = thisList.IndexOf(";");
				}
			}
		}

		#endregion

		#endregion

		#region ShiftAvailability

		/// <summary>
		/// Alters Availability and AvailabilityAtTimeOfLastUpdateAsDelimitedList by the number of nights supplied
		/// </summary>
		/// <param name="numNights">numNights</param>
		public void ShiftAvailability(int numNights)
		{
			if (numNights > 0)
			{
				#region Take days off the start
				for(int counter = 0; counter < numNights; counter++)
				{
					if (Availability.Count > 0)
						Availability.RemoveAt(0);
				}

				for(int counter = 0; counter < numNights; counter++)
				{
					if (AvailabilityAtTimeOfLastUpdate.Count > 0)
						AvailabilityAtTimeOfLastUpdate.RemoveAt(0);
				}

				#endregion

			}
			else
			{ 
				if (numNights < 0)
				{
					#region Add -1 availabilities to the start
					for(int counter = 0; counter < numNights; counter++)
					{
						Availability.Insert(0, -1);
					}

					for(int counter = 0; counter < numNights; counter++)
					{
						AvailabilityAtTimeOfLastUpdate.Insert(0, -1);
					}

					#endregion
				}
			}

		}

		#endregion


	}
}
