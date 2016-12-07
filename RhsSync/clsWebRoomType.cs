using System;
using System.Collections;

namespace RhsSync
{
	/// <summary>
	/// Summary description for clsWebRoomType.
	/// </summary>
	public class clsWebRoomType : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsWebRoomType()
		{
			RhsRoomTypesMappedToGroup1 = new ArrayList();
			RhsRoomTypesMappedToGroup2 = new ArrayList();
		}

        //		/// <summary>
        //		/// Structure that emulates the Webrooms table
        //		/// </summary>
        //		public struct webRoomStruct
        //		{

        /// <summary>
        /// Id of this Web Room
        /// </summary>
        public int webRoomTypeId;
			/// <summary>
			/// Name of this Web Room
			/// </summary>
			public string webRoomTypeName;
//		}

		/// <summary>
		/// Whether this Room Type is Interconnecting or not
		/// </summary>
		public int IsInterConnecting;

		/// <summary>
		/// Indicates whether this RHS Room has been found in the latest list
		/// </summary>
		public bool foundInLatestList;

		/// <summary>
		/// Maximum Number of Rooms of this type
		/// </summary>
		public int maxNumberOfRooms;

		#region RhsRoomTypesMappedToGroup1

		/// <summary>
		/// First Group of RHS Room Types that this WebRoomType is associated with
		/// </summary>
		public ArrayList RhsRoomTypesMappedToGroup1;

		/// <summary>
		/// A semi colon delimited list of the RHS Room Types Mapped to Group 1
		/// </summary>
		/// <returns></returns>
		public string RhsRoomTypesMappedToGroup1AsDelimitedList
		{
			get 
			{

				string thisRetVal = "";
				if (RhsRoomTypesMappedToGroup1 == null)
					return thisRetVal;

				for(int counter = 0; counter < RhsRoomTypesMappedToGroup1.Count; counter++)
					thisRetVal += ((int) RhsRoomTypesMappedToGroup1[counter]).ToString() + ";";

				return thisRetVal;
			}
			set
			{
				string thisList = value;
				RhsRoomTypesMappedToGroup1 = new ArrayList();
				int semiColonIndex = thisList.IndexOf(";");
				while (semiColonIndex > 0)
				{
					string thisRhsRoomType = thisList.Substring(0, semiColonIndex);

					if (isNumerical(thisRhsRoomType))
						RhsRoomTypesMappedToGroup1.Add(Convert.ToInt32(thisRhsRoomType));
					thisList = thisList.Substring(semiColonIndex + 1);
					semiColonIndex = thisList.IndexOf(";");
				}
			}
		}

		#endregion

		#region RhsRoomTypesMappedToGroup2

		/// <summary>
		/// Second Group of RHS Room Types that this WebRoomType is associated with
		/// </summary>
		public ArrayList RhsRoomTypesMappedToGroup2;

		/// <summary>
		/// A semi colon delimited list of the RHS Room Types Mapped to Group 2
		/// </summary>
		/// <returns></returns>
		public string RhsRoomTypesMappedToGroup2AsDelimitedList
		{
			get 
			{

				string thisRetVal = "";
				if (RhsRoomTypesMappedToGroup2 == null)
					return thisRetVal;

				for(int counter = 0; counter < RhsRoomTypesMappedToGroup2.Count; counter++)
					thisRetVal += ((int) RhsRoomTypesMappedToGroup2[counter]).ToString() + ";";

				return thisRetVal;
			}
			set
			{
				string thisList = value;
				RhsRoomTypesMappedToGroup2 = new ArrayList();
				int semiColonIndex = thisList.IndexOf(";");
				while (semiColonIndex > 0)
				{
					string thisRhsRoomType = thisList.Substring(0, semiColonIndex);

					if (isNumerical(thisRhsRoomType))
						RhsRoomTypesMappedToGroup2.Add(Convert.ToInt32(thisRhsRoomType));
					thisList = thisList.Substring(semiColonIndex + 1);
					semiColonIndex = thisList.IndexOf(";");
				}
			}
		}

		#endregion

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

		/// <summary>
		/// Arraylist containing indexes of differences between Availability and AvailabilityAtTimeOfLastUpdate arrays
		/// </summary>
		public ArrayList ChangedAvailabilityIndex;

		#region GetDifferences

		/// <summary>
		/// Returns the number of differences between AvailabilityAtTimeOfLastUpdateAsDelimitedList and Availability
		/// </summary>
		/// <returns></returns>
		public int GetDifferences()
		{
			ChangedAvailabilityIndex = new ArrayList();

			if (Availability != null)
			{
				for(int counter = 0; counter < Availability.Count; counter++)
				{
					if(counter > AvailabilityAtTimeOfLastUpdate.Count - 1 || Convert.ToInt32(Availability[counter]) != Convert.ToInt32(AvailabilityAtTimeOfLastUpdate[counter]))
						ChangedAvailabilityIndex.Add(counter);
				}
				return ChangedAvailabilityIndex.Count;
			}
			else
				return -1;


		}

		#endregion


	}
}
