using System;
using System.Collections;

using System.Runtime.InteropServices;
using System.Data;
using System.Data.Odbc;
using System.Xml;

namespace RhsSync 
{
	/// <summary>
	/// Summary description for clsRecieveWebRoomsData.
	/// </summary>
	public class clsRecieveWebRoomsData : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsRecieveWebRoomsData()
		{
		}


		#region Publicly exposed variables

		/// <summary>
		/// Indicates if the success or otherwise of the operation
		/// </summary>
		public bool requestSucceeded;

		/// <summary>
        /// Supplies the details of any issues encountered while performing the operation
		/// </summary>
		public clsWelmanError errDescription = new clsWelmanError();

		/// <summary>
		/// Details on all Webrooms successfully returned
		/// </summary>
		public ArrayList theseWebRoomTypes;


		/// <summary>
		/// LastRemoteInviteChange
		/// </summary>
		public DateTime LastRemoteInviteChange = new DateTime(1,1,1);

		/// <summary>
		/// LastSuccessfulCommunication
		/// </summary>
		public DateTime LastSuccessfulCommunication = new DateTime(1,1,1);

		/// <summary>
		/// LastWebRoomTypeChange
		/// </summary>
		public DateTime LastWebRoomTypeChange = new DateTime(1,1,1);

		/// <summary>
		/// LastBookingReceived
		/// </summary>
		public DateTime LastBookingReceived = new DateTime(1,1,1);

		/// <summary>
		/// RefDate (local copy)
		/// </summary>
		public DateTime RefDate = new DateTime(1,1,1);

		/// <summary>
		/// Lookahead (local copy)
		/// </summary>
		public int Lookahead = 0;

		/// <summary>
		/// CompleteAvailabilityRequired (local copy)
		/// </summary>
		public bool CompleteAvailabilityRequired = true;

		#region WebBookingsToAcknowledge

		/// <summary>
		/// WebBookingsToAcknowledge of this Web room type
		/// </summary>
		public ArrayList WebBookingsToAcknowledge;

		/// <summary>
		/// A semi colon delimited list of the WebBookingsToAcknowledge of this Room Type
		/// </summary>
		/// <returns>A semi colon delimited list of the WebBookingsToAcknowledge of this Room Type</returns>
		public string WebBookingsToAcknowledgeAsDelimitedList
		{
			get 
			{
				string thisRetVal = "";
				if (WebBookingsToAcknowledge == null)
					return thisRetVal;

				for(int counter = 0; counter < WebBookingsToAcknowledge.Count; counter++)
					thisRetVal += ((int) WebBookingsToAcknowledge[counter]).ToString() + ";";

				return thisRetVal;
			}
			set
			{
				string thisList = value;
				WebBookingsToAcknowledge = new ArrayList();
				int semiColonIndex = thisList.IndexOf(";");
				while (semiColonIndex > 0)
				{
					string thisWebBookingsToAcknowledge = thisList.Substring(0, semiColonIndex);

					if (isNumerical(thisWebBookingsToAcknowledge))
						WebBookingsToAcknowledge.Add(Convert.ToInt32(thisWebBookingsToAcknowledge));
					thisList = thisList.Substring(semiColonIndex + 1);
					semiColonIndex = thisList.IndexOf(";");
				}
			}
		}

		#endregion

		/// <summary>
		/// NumWebRoomTypes
		/// </summary>
		public int NumWebRoomTypes;

//		/// <summary>
//		/// Picks up a change in Server Site
//		/// </summary>
//		public string ServerSite = "";

		/// <summary>
		/// Picks up a change in Remote Invites
		/// </summary>
		public string RemoteSupportInvites = "";
		
		/// <summary>
		/// Local List of Web Room Bookings
		/// </summary>
		public ArrayList theseWebRoomBookings = new ArrayList();


        #endregion

        #region GetDataFromServer (first communication)

        /// <summary>
        /// This method tries to contact the Welman Server.
        /// If successful, the server replies with:
        /// <list type="bullet">
        /// <item><description>
        /// Data on the Web Room Types for this customer
        /// </description></item>
        /// <item><description>		
        ///	Data on the Any Online Bookings made for this customer's premises.
        /// </description></item></list></summary>
        /// <returns>Webrooms Data as returned From the Server</returns>
        public bool GetDataFromServer(string thisRhsFolder, string Curl, string Password, bool AddOnlineBookingsIntoRhs)
		{
			string currentFunction = "GetDataFromServer";	
	
			requestSucceeded = false;
			errDescription.errNum = 0;
			errDescription.errorForUser = "";

            System.ServiceModel.EndpointAddress remoteAddress = new System.ServiceModel.EndpointAddress(ThirdPartyBaseUrl);
            Comms.WebRoomsServiceSoapClient thisService = new Comms.WebRoomsServiceSoapClient(
                new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.Transport), remoteAddress);

			Comms.RWRServerParamsRequest thisRequest = new Comms.RWRServerParamsRequest();

//            Comms.WebRoomsService thisService = new Comms.WebRoomsService();
//			Comms.RWRServerParamsResponse thisResponse;
            Comms.RWRCheckServerResult thisResponse;


			thisRequest.PropertyAuthentication = new Comms.RWRServerParamsRequestPropertyAuthentication();
			thisRequest.PropertyAuthentication.PropertyId = Curl;
			thisRequest.PropertyAuthentication.PropertyPassword = Password;

			thisRequest.ThirdPartyAuthentication = new Comms.RWRServerParamsRequestThirdPartyAuthentication();
			
			thisRequest.ThirdPartyAuthentication.EnqUser = thisEnqUser;
			thisRequest.ThirdPartyAuthentication.EnqPass = thisEnqPass;
			thisRequest.ThirdPartyAuthentication.SoftwareVersion = webAgent;
            			
			try
			{

//				thisService.Timeout = webTimeOut;
				thisResponse = thisService.RWRCheckServer(thisRequest);

			}
			catch (Exception e)
			{
				LogThis(DateTime.Now, "Failure connecting to Webrooms Server when getting data from server ", 
					e.ToString() + " , " + e.Message + " , " + e.StackTrace + " , " + e.Source,
					currentFunction, e.StackTrace.ToString());

				return false;
			}
            
			LogThis(DateTime.Now, "Response from Server", "", currentFunction, RWRCheckServerRequestXml(thisResponse));

			if (!thisResponse.ResponseType.Success)
			{
				requestSucceeded = false;
				LogThis(DateTime.Now, "Failure attempting to check server parameters", thisResponse.ResponseType.Message, 
					currentFunction, thisResponse.ResponseType.Message);
				return requestSucceeded;

			}

			#region General Parameters

			Lookahead = thisResponse.LookAheadDays;
			RefDate = new DateTime(thisResponse.RefDayYear,
				thisResponse.RefDayMonth, 
				thisResponse.RefDayDay);

			CompleteAvailabilityRequired = thisResponse.CompleteAvailabilityRequired;

			#endregion

			#region Deal with Web Room Types

			DateTime thisLastWebRoomTypeChange = new DateTime(thisResponse.LastRoomTypeChangeYear,
				thisResponse.LastRoomTypeChangeMonth,
				thisResponse.LastRoomTypeChangeDay,
				thisResponse.LastRoomTypeChangeHour,
				thisResponse.LastRoomTypeChangeMinute,
				thisResponse.LastRoomTypeChangeSecond);

			if (thisLastWebRoomTypeChange != LastWebRoomTypeChange)
			{
				Comms.GetRoomTypesResponse thisRoomTypeResponse;

				try
				{
//					thisService.Timeout = webTimeOut;
					thisRoomTypeResponse = thisService.GetRoomTypes(thisRequest);
				}
				catch (Exception e)
				{
					LogThis(DateTime.Now, "Failure connecting to Webrooms Server when getting Room Types.", 
						e.ToString() + " , " + e.Message + " , " + e.StackTrace + " , " + e.Source,
						currentFunction, e.StackTrace.ToString());

					return false;
				}

				if (!thisRoomTypeResponse.ResponseType.Success)
				{
					requestSucceeded = false;
					LogThis(DateTime.Now, "Failure attempting to get room types", thisRoomTypeResponse.ResponseType.Message, 
						currentFunction, thisRoomTypeResponse.ResponseType.Message);
					return requestSucceeded;

				}

				#region Set Existing Room Types as not found

                int numTheseWebRoomTypes = theseWebRoomTypes.Count;

                for (int twrtCounter = 0; twrtCounter < numTheseWebRoomTypes; twrtCounter++)
                    //foreach (object thisObject in theseWebRoomTypes)
				{
//					clsWebRoomType thisWebRoomType = (clsWebRoomType) thisObject;
                    clsWebRoomType thisWebRoomType = (clsWebRoomType)theseWebRoomTypes[twrtCounter];
//                    int thisIndex = theseWebRoomTypes.IndexOf(thisObject);

					thisWebRoomType.foundInLatestList = false;
                    theseWebRoomTypes.RemoveAt(twrtCounter);
                    theseWebRoomTypes.Insert(twrtCounter, thisWebRoomType);

				}

				#endregion

				#region Go through recent list and compare and contrast

				int numWebRoomTypesFound = 0;

				if (thisRoomTypeResponse.RoomType != null)
					numWebRoomTypesFound = thisRoomTypeResponse.RoomType.GetUpperBound(0) + 1;

				for (int webRoomCounter = 0; webRoomCounter < numWebRoomTypesFound; webRoomCounter++)
				{

					clsWebRoomType thisWebRoomType = new clsWebRoomType();

					thisWebRoomType.webRoomTypeId = thisRoomTypeResponse.RoomType[webRoomCounter].RoomTypeId;

					thisWebRoomType.webRoomTypeName = thisRoomTypeResponse.RoomType[webRoomCounter].RoomTypeName;

					if (thisWebRoomType.webRoomTypeId != 0 && thisWebRoomType.webRoomTypeName != "")
					{

						thisWebRoomType.foundInLatestList = true;

						bool foundThisWebRoom = false;

						#region Try to find this Web Room Type in the existing list

						//Note a recent change which will result in the removal of duplicates web room types that show up 
						//in the 'existing' list
                        
                        numTheseWebRoomTypes = theseWebRoomTypes.Count;

                        for (int twrtCounter = 0; twrtCounter < numTheseWebRoomTypes && !foundThisWebRoom; twrtCounter++)
                        //foreach(object thisObject in theseWebRoomTypes)
						{

                            //if (!foundThisWebRoom)
                            //{
                                clsWebRoomType thisOldWebRoomType = (clsWebRoomType) theseWebRoomTypes[twrtCounter];
								if (thisOldWebRoomType.webRoomTypeId == thisWebRoomType.webRoomTypeId)
								{
									foundThisWebRoom = true;
									thisOldWebRoomType.foundInLatestList = true;
                                    if (thisOldWebRoomType.webRoomTypeName != thisWebRoomType.webRoomTypeName)
                                        thisOldWebRoomType.webRoomTypeName = thisWebRoomType.webRoomTypeName;

                                    theseWebRoomTypes.RemoveAt(twrtCounter);
                                    theseWebRoomTypes.Insert(twrtCounter, thisOldWebRoomType);


                                //}
							}

						}

						#endregion

						#region Add new additions

						if (!foundThisWebRoom)
							theseWebRoomTypes.Add(thisWebRoomType);

						#endregion

					}
				}
				#endregion

				#region Delete any WebRoom Types not found

				#region Delete if not found
				for(int counter = 0; counter < theseWebRoomTypes.Count; counter++)
				{
					clsWebRoomType thisWebRoomTypeToDelete = (clsWebRoomType) theseWebRoomTypes[counter];

					if (!thisWebRoomTypeToDelete.foundInLatestList)
					{
						theseWebRoomTypes.RemoveAt(counter);
						counter--;
					}
				}

				#endregion

				#endregion

				LogThis(DateTime.Now, "Successfully verified Web Room Types.", 
					"",	currentFunction, errDescription.logFileDescription);

				LastWebRoomTypeChange = thisLastWebRoomTypeChange;


			}

			#endregion

			#region Deal with Remote Invites

			DateTime thisLastRemoteInviteChange = new DateTime(thisResponse.LastRemoteInviteChangeYear,
				thisResponse.LastRemoteInviteChangeMonth,
				thisResponse.LastRemoteInviteChangeDay,
				thisResponse.LastRemoteInviteChangeHour,
				thisResponse.LastRemoteInviteChangeMinute,
				thisResponse.LastRemoteInviteChangeSecond);

			if (thisLastRemoteInviteChange != LastRemoteInviteChange)
			{
				Comms.GetRemoteInvitesResponse thisRemoteInviteResponse;

				try
				{
//					thisService.Timeout = webTimeOut;
					thisRemoteInviteResponse = thisService.GetRemoteInvites(thisRequest);
				}
				catch (Exception e)
				{
					LogThis(DateTime.Now, "Failure connecting to Webrooms Server when getting Remote Invites.", 
						e.ToString() + " , " + e.Message + " , " + e.StackTrace + " , " + e.Source,
						currentFunction, e.StackTrace.ToString());

					return false;
				}

				if (!thisRemoteInviteResponse.ResponseType.Success)
				{
					requestSucceeded = false;
					LogThis(DateTime.Now, "Failure attempting to get room types", thisRemoteInviteResponse.ResponseType.Message, 
						currentFunction, thisRemoteInviteResponse.ResponseType.Message);
					return requestSucceeded;

				}

				#region Add Remote Invites as found

				RemoteSupportInvites = "";

				if (thisRemoteInviteResponse.RemoteInvite != null)
				{

					for(int counter = 0; counter < thisRemoteInviteResponse.RemoteInvite.GetUpperBound(0)+1; counter++)
					{
						RemoteSupportInvites += thisRemoteInviteResponse.RemoteInvite[counter].RemoteInviteName
							+ "~" + thisRemoteInviteResponse.RemoteInvite[counter].RemoteInviteUrl
							+ ";";
					}

				}

				#endregion

				LogThis(DateTime.Now, "Successfully retrieved Remote Invites.", 
					"",	currentFunction, errDescription.logFileDescription);

				LastRemoteInviteChange = thisLastRemoteInviteChange;

			}
			#endregion
				
			#region Add Bookings

			if (AddOnlineBookingsIntoRhs && thisResponse.Booking != null)
			{

				int numBookings = thisResponse.Booking.GetUpperBound(0) + 1;

				for(int bkCounter = 0; bkCounter < numBookings; bkCounter++)
				{
					clsWebRoomBooking thisWebRoomBooking = new clsWebRoomBooking();

					thisWebRoomBooking.uid = thisResponse.Booking[bkCounter].BookingId.ToString();

					#region Check if this Booking needs to be added

					bool newBooking = true;

					foreach(object thisObject in WebBookingsToAcknowledge)
					{
						string thisId = (string) thisObject;
						if (thisId == thisWebRoomBooking.uid)
							newBooking = false;
					}

					#endregion

					if (newBooking)
					{
						#region Receive the Booking

						thisWebRoomBooking.WebRmSource = thisResponse.Booking[bkCounter].BookChannel;
						thisWebRoomBooking.confirmationCode = thisResponse.Booking[bkCounter].ConfirmationCode;

						int numContacts = 0;
						string thisContact = "";
						string thisFax = "";
						string thisContactPhone = "";
						string thisAltPhone = "";
						string thisContactEmail = "";
						string thisContactAddress = "";
						string thisComments = "";

						thisWebRoomBooking.contactName = "";

						#region Contact Information

						if (thisResponse.Booking[bkCounter].Contact != null)
						{

							numContacts = thisResponse.Booking[bkCounter].Contact.GetUpperBound(0) + 1;
							

							for(int ctCounter = 0; ctCounter < numContacts; ctCounter++)
							{
								thisContact += thisResponse.Booking[bkCounter].Contact[ctCounter].FirstNames + " "
									+ thisResponse.Booking[bkCounter].Contact[ctCounter].LastName + " ";

                                thisFax = thisResponse.Booking[bkCounter].Contact[ctCounter].Fax;

                                if (thisFax != "")
                                    thisWebRoomBooking.contactFax.Add(thisFax);

								thisContactPhone = thisResponse.Booking[bkCounter].Contact[ctCounter].MobilePhone;

                                if (thisContactPhone != "")
                                    thisWebRoomBooking.contactPhone.Add(thisContactPhone);

								thisAltPhone = thisResponse.Booking[bkCounter].Contact[ctCounter].AltPhone;

                                if (thisAltPhone != "")
                                    thisWebRoomBooking.alternativePhone.Add(thisAltPhone);

								thisContactEmail = thisResponse.Booking[bkCounter].Contact[ctCounter].Email;

                                if (thisContactEmail != "")
                                    thisWebRoomBooking.contactEmail.Add(thisContactEmail);
								
								string thisSubAddress = thisResponse.Booking[bkCounter].Contact[ctCounter].Address.Replace("\n", aCrLf);

								thisContactAddress += thisSubAddress + aCrLf
									+ thisResponse.Booking[bkCounter].Contact[ctCounter].City + " " + thisResponse.Booking[bkCounter].Contact[ctCounter].PostCode + aCrLf
									//								+ thisResponse.Booking[bkCounter].Contact[ctCounter].State + aCrLf
									+ thisResponse.Booking[bkCounter].Contact[ctCounter].Country + aCrLf;

								thisComments += thisResponse.Booking[bkCounter].Contact[ctCounter].Comments + " ";

							}

							thisWebRoomBooking.contactName = thisContact.Trim();

						}

						#endregion

						#region Guest Information

						int numGuests = 0;
						string thisGuest = "";

						if (thisResponse.Booking[bkCounter].Guest != null)
						{

							numGuests = thisResponse.Booking[bkCounter].Guest.GetUpperBound(0) + 1;
							for(int gtCounter = 0; gtCounter < numGuests; gtCounter++)
							{
								thisGuest += thisResponse.Booking[bkCounter].Guest[gtCounter].FirstNames + " "
									+ thisResponse.Booking[bkCounter].Guest[gtCounter].LastName + " ";



                                thisFax = thisResponse.Booking[bkCounter].Guest[gtCounter].Fax;

                                if (thisFax != "")
                                    thisWebRoomBooking.contactFax.Add(thisFax);

                                thisContactPhone = thisResponse.Booking[bkCounter].Guest[gtCounter].MobilePhone;

                                if (thisContactPhone != "")
                                    thisWebRoomBooking.contactPhone.Add(thisContactPhone);

                                thisAltPhone = thisResponse.Booking[bkCounter].Guest[gtCounter].AltPhone;

                                if (thisAltPhone != "")
                                    thisWebRoomBooking.alternativePhone.Add(thisAltPhone);

                                thisContactEmail = thisResponse.Booking[bkCounter].Guest[gtCounter].Email;

                                if (thisContactEmail != "")
                                    thisWebRoomBooking.contactEmail.Add(thisContactEmail);

                                //thisFax += thisResponse.Booking[bkCounter].Guest[gtCounter].Fax + " ";
                                //thisContactPhone += thisResponse.Booking[bkCounter].Guest[gtCounter].MobilePhone + " ";
                                //thisAltPhone += thisResponse.Booking[bkCounter].Guest[gtCounter].AltPhone + " ";
                                //thisContactEmail += thisResponse.Booking[bkCounter].Guest[gtCounter].Email + " ";

								string thisSubAddress = thisResponse.Booking[bkCounter].Guest[gtCounter].Address.Replace("\n", aCrLf);
								thisContactAddress += thisSubAddress + aCrLf
									+ thisResponse.Booking[bkCounter].Guest[gtCounter].City + " " + thisResponse.Booking[bkCounter].Guest[gtCounter].PostCode + aCrLf
									//								+ thisResponse.Booking[bkCounter].Guest[ctCounter].State + aCrLf
									+ thisResponse.Booking[bkCounter].Guest[gtCounter].Country + aCrLf;
								thisComments += thisResponse.Booking[bkCounter].Guest[gtCounter].Comments + " ";

							}
						}

						thisWebRoomBooking.guestName = thisGuest.Trim();
                        //thisWebRoomBooking.contactFax = thisFax.Trim();
                        //thisWebRoomBooking.contactPhone = thisContactPhone.Trim();
                        //thisWebRoomBooking.alternativePhone = thisAltPhone.Trim();
                        //thisWebRoomBooking.contactPhone = thisContactPhone.Trim();
						//thisWebRoomBooking.contactEmail = thisContactEmail.Trim();
						thisWebRoomBooking.contactAddresss = thisContactAddress.Trim();
						thisWebRoomBooking.comments = thisComments.Trim();
						thisWebRoomBooking.payment = 0;
						thisWebRoomBooking.returning = thisResponse.Booking[bkCounter].GuestStayedBefore;
						thisWebRoomBooking.stayedBefore = thisResponse.Booking[bkCounter].GuestStayedBefore;
						thisWebRoomBooking.howWebsiteFound = thisResponse.Booking[bkCounter].HowWebsiteFound;

						#endregion

						#region Credit Card Information

						if (thisResponse.Booking[bkCounter].CreditCard != null)
						{

							thisWebRoomBooking.ccExpiry = thisResponse.Booking[bkCounter].CreditCard.ExpiryMonth.ToString()
								+ "/" + thisResponse.Booking[bkCounter].CreditCard.ExpiryYear.ToString();
							thisWebRoomBooking.ccName = thisResponse.Booking[bkCounter].CreditCard.Name;
							thisWebRoomBooking.ccType = ""; 
							thisWebRoomBooking.ccNumber = thisResponse.Booking[bkCounter].CreditCard.Number;
						}
						else
						{
							thisWebRoomBooking.ccExpiry = "";
							thisWebRoomBooking.ccName = "";
							thisWebRoomBooking.ccType = "";
							thisWebRoomBooking.ccNumber = "";
						}

						#endregion

						#region Submitted Date/Time

						thisWebRoomBooking.dateTimeResMade = new DateTime(1,1,1);

						if (thisResponse.Booking[bkCounter].SubmittedYear != 0
							&& thisResponse.Booking[bkCounter].SubmittedMonth != 0
							&& thisResponse.Booking[bkCounter].SubmittedDay != 0
							&& thisResponse.Booking[bkCounter].SubmittedHour != 0
							&& thisResponse.Booking[bkCounter].SubmittedMinute != 0
							&& thisResponse.Booking[bkCounter].SubmittedSecond != 0)
						{
							thisWebRoomBooking.dateTimeResMade = new DateTime(thisResponse.Booking[bkCounter].SubmittedYear,
								thisResponse.Booking[bkCounter].SubmittedMonth,
								thisResponse.Booking[bkCounter].SubmittedDay,
								thisResponse.Booking[bkCounter].SubmittedHour,
								thisResponse.Booking[bkCounter].SubmittedMinute,
								thisResponse.Booking[bkCounter].SubmittedSecond);
						}

						#endregion

						#region Booking Summary Information

						thisWebRoomBooking.tentative = thisResponse.Booking[bkCounter].IsTentative;

						thisWebRoomBooking.firstNight = new DateTime(1,1,1);
						thisWebRoomBooking.lastNight = new DateTime(1,1,1);

						thisWebRoomBooking.simple = false;

						thisWebRoomBooking.ETA = thisResponse.Booking[bkCounter].ETA;

						#endregion

						if (thisResponse.Booking[bkCounter].RoomNight == null)
						{
							LogThis(DateTime.Now, "Error:Booking " + thisWebRoomBooking.uid +  " has no Room Nights.", 
								"",	currentFunction, errDescription.logFileDescription);

                            WebBookingsToAcknowledge.Add(thisWebRoomBooking.uid); //Make sure this booking is acknowledged 

						}
						else
						{

							int numRoomNights = thisResponse.Booking[bkCounter].RoomNight.GetUpperBound(0) + 1;

							thisWebRoomBooking.roomNightsBooked = new ArrayList();

							#region Add Room Nights

							for(int rnCounter = 0; rnCounter < numRoomNights; rnCounter++)
							{


								clsWebRoomNightComplex thisWebRoomNightComplex = new clsWebRoomNightComplex();

								thisWebRoomNightComplex.roomType = thisResponse.Booking[bkCounter].RoomNight[rnCounter].RoomId;
								thisWebRoomNightComplex.numAdults = thisResponse.Booking[bkCounter].RoomNight[rnCounter].NumAdults;
								thisWebRoomNightComplex.numChildren = thisResponse.Booking[bkCounter].RoomNight[rnCounter].NumChildren;
								thisWebRoomNightComplex.numInfants = thisResponse.Booking[bkCounter].RoomNight[rnCounter].NumInfantNotRequiringCot
									+ thisResponse.Booking[bkCounter].RoomNight[rnCounter].NumInfantRequiringCot;
								thisWebRoomNightComplex.costOfRoomPerNight = thisResponse.Booking[bkCounter].RoomNight[rnCounter].TotalCostPerNight;

								thisWebRoomNightComplex.date = new DateTime(
									thisResponse.Booking[bkCounter].RoomNight[rnCounter].FirstDayYear,
									thisResponse.Booking[bkCounter].RoomNight[rnCounter].FirstDayMonth,
									thisResponse.Booking[bkCounter].RoomNight[rnCounter].FirstDayDay);

								DateTime lastDay = new DateTime(
									thisResponse.Booking[bkCounter].RoomNight[rnCounter].LastDayYear,
									thisResponse.Booking[bkCounter].RoomNight[rnCounter].LastDayMonth,
									thisResponse.Booking[bkCounter].RoomNight[rnCounter].LastDayDay);

								TimeSpan thisTs = lastDay.Subtract(thisWebRoomNightComplex.date);

								if (thisWebRoomBooking.firstNight == new DateTime(1,1,1)
									|| thisWebRoomBooking.firstNight > thisWebRoomNightComplex.date)
									thisWebRoomBooking.firstNight = thisWebRoomNightComplex.date;

								int numNights = Convert.ToInt32(thisTs.TotalDays);

								thisWebRoomBooking.roomNightsBooked.Add(thisWebRoomNightComplex);


								for(int nightCounter = 1; nightCounter < numNights; nightCounter++)
								{
									clsWebRoomNightComplex thisNewWebRoomNightComplex = new clsWebRoomNightComplex();
									thisNewWebRoomNightComplex.roomType = thisWebRoomNightComplex.roomType;
									thisNewWebRoomNightComplex.numAdults = thisWebRoomNightComplex.numAdults;
									thisNewWebRoomNightComplex.numChildren = thisWebRoomNightComplex.numChildren;
									thisNewWebRoomNightComplex.numInfants = thisWebRoomNightComplex.numInfants;
									thisNewWebRoomNightComplex.costOfRoomPerNight = thisWebRoomNightComplex.costOfRoomPerNight;
									thisNewWebRoomNightComplex.date = thisWebRoomNightComplex.date.AddDays(nightCounter);

									if (thisWebRoomBooking.lastNight < thisNewWebRoomNightComplex.date)
										thisWebRoomBooking.firstNight = thisNewWebRoomNightComplex.date;

									thisWebRoomBooking.roomNightsBooked.Add(thisNewWebRoomNightComplex);

								}

							}

							#endregion

							#region Add summary details for this booking

							#region For each Booking

							bool consistantCosts = true;
							bool consistantRooms = true;

							thisWebRoomBooking.maxWebRoomsBooked = new clsWebRoomsBookedIn[theseWebRoomTypes.Count];

							#region Make the IDs consistant with the webRoomIDs
							for (int counter = 0; counter < theseWebRoomTypes.Count; counter++)
							{
								thisWebRoomBooking.maxWebRoomsBooked[counter] = new clsWebRoomsBookedIn();
								thisWebRoomBooking.maxWebRoomsBooked[counter].webRoomTypeId = ((clsWebRoomType) theseWebRoomTypes[counter]).webRoomTypeId;
							}
							#endregion

							#region Deal with  Complex booking

							DateTime lastDate = new DateTime(2001,1,1);
							int totAdults = 0;
							int totChildren = 0;
							int totInfants = 0;
							decimal totCost = 0;
							decimal lastDaysCost = 0;

							clsWebRoomsBookedIn[] dayResult = new clsWebRoomsBookedIn[theseWebRoomTypes.Count];
							clsWebRoomsBookedIn[] lastDaysRooms = new clsWebRoomsBookedIn[theseWebRoomTypes.Count];

							for (int roomNightcounter = 0; roomNightcounter < thisWebRoomBooking.roomNightsBooked.Count; roomNightcounter ++)
							{
								#region for Each Room Night

								if (((clsWebRoomNightComplex)thisWebRoomBooking.roomNightsBooked[roomNightcounter]).date != lastDate)
								{
									if (!((lastDate.Year == 2001) && (lastDate.Month == 1) && (lastDate.Day == 1)))
									{
										//Check for consistancy between the days
										consistantCosts = consistantCosts && (lastDaysCost == totCost);
										//Costs are consistant if:
										// a) They already were when we started, AND
										// b) The cost for the last day was the same as for this day
										consistantRooms = consistantRooms && (lastDaysRooms == dayResult);
									}
						
									lastDaysRooms = dayResult;
									lastDaysCost = totCost;
									totAdults = 0;
									totChildren = 0;
									totInfants = 0;
									totCost = 0;

									lastDate = ((clsWebRoomNightComplex) thisWebRoomBooking.roomNightsBooked[roomNightcounter]).date;
									//New day; reset counters
									for (int webRoomCounter = 0; webRoomCounter < theseWebRoomTypes.Count; webRoomCounter++)
									{
										dayResult[webRoomCounter] = new clsWebRoomsBookedIn();
										dayResult[webRoomCounter].webRoomTypeId = ((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).webRoomTypeId;
										dayResult[webRoomCounter].maxRoomsBooked = 0;
									}
					
					
								}

								totAdults += ((clsWebRoomNightComplex) thisWebRoomBooking.roomNightsBooked[roomNightcounter]).numAdults;
								totChildren += ((clsWebRoomNightComplex) thisWebRoomBooking.roomNightsBooked[roomNightcounter]).numChildren;
								totInfants += ((clsWebRoomNightComplex) thisWebRoomBooking.roomNightsBooked[roomNightcounter]).numInfants;
								totCost += ((clsWebRoomNightComplex) thisWebRoomBooking.roomNightsBooked[roomNightcounter]).costOfRoomPerNight;
					
								if (totAdults > thisWebRoomBooking.maxAdults)
									thisWebRoomBooking.maxAdults = totAdults;

								if (totChildren + totInfants > thisWebRoomBooking.maxChildrenAndInfants)
									thisWebRoomBooking.maxChildrenAndInfants = totChildren + totInfants;

								if (totAdults + totChildren + totInfants > thisWebRoomBooking.maxPeople)
									thisWebRoomBooking.maxPeople = totAdults + totChildren + totInfants;

								if (totCost > thisWebRoomBooking.maxCost)
									thisWebRoomBooking.maxCost = totCost;

								for (int webRoomCounter = 0; webRoomCounter < theseWebRoomTypes.Count; webRoomCounter++)
								{
									if (dayResult[webRoomCounter].webRoomTypeId == ((clsWebRoomNightComplex) thisWebRoomBooking.roomNightsBooked[roomNightcounter]).roomType)
									{
										dayResult[webRoomCounter].maxRoomsBooked++;
										if (dayResult[webRoomCounter].maxRoomsBooked > thisWebRoomBooking.maxWebRoomsBooked[webRoomCounter].maxRoomsBooked)
											thisWebRoomBooking.maxWebRoomsBooked[webRoomCounter].maxRoomsBooked = dayResult[webRoomCounter].maxRoomsBooked;
									}					
								}
								#endregion
							}

							#endregion

							thisWebRoomBooking.costAndRoomsSameForEachNight = consistantCosts && consistantRooms;

							#endregion


							#endregion

							#region Add this Web Booking

							theseWebRoomBookings.Add(thisWebRoomBooking);
					
							#endregion

						}

						#endregion

						LastBookingReceived = DateTime.Now;

                    }


				}

                #region Add Bookings into RHS, if this is desired

                if (AddOnlineBookingsIntoRhs && theseWebRoomBookings != null && theseWebRoomBookings.Count > 0)
                {
                    sendWebBookingsToRhs(thisRhsFolder);
                }

                #endregion


				LogThis(DateTime.Now, "Successfully checked for Online Bookings.", 
					"",	currentFunction, errDescription.logFileDescription);

			}

			#endregion

			LogThis(DateTime.Now, "Successfully updated settings from Server.", 
				"",	currentFunction, errDescription.logFileDescription);

			requestSucceeded = true;

			LastSuccessfulCommunication = DateTime.Now;


			return requestSucceeded;

		}
        #endregion

        #region getDetailWebRooms (Web Room Types from Server Response)

        //		/// <summary>
        //		/// This method returns Webrooms in the web rooms structure from a response by the server to a
        //		/// request for data from the website.  
        //		/// </summary>
        //		/// <param name="response">Server Response to be parsed</param>
        //		/// <returns>clsWebRoomType containing the web rooms details</returns>
        //		public bool getDetailWebRooms(string response)
        //		{
        //			int currentIndex = 0;
        //			//int currentParamIndex = 0;
        //			int numWebRoomTypesFound = 0;
        //			int nextEol;
        //			int nextComma;
        //			byte[] testChar;
        //			testChar = new byte[2];
        //			string paramstring; //Test String
        //
        //
        //			currentIndex = response.IndexOf(sNumRoomTypes, 0);
        //			nextEol = response.IndexOf(aCrLf, currentIndex);
        //			nextComma = response.IndexOf(aComma, currentIndex);
        //
        //			paramstring = response.Substring(sNumRoomTypes.Length,nextEol - sNumRoomTypes.Length);
        //
        //			//Lets get it
        //			numWebRoomTypesFound = getIntFromString(paramstring);
        //
        //			if (numWebRoomTypesFound == 0)
        //				return false;
        //
        //			#region Lets clean up the web room types
        //
        //			#region Set them as not found
        //			foreach(object thisObject in theseWebRoomTypes)
        //			{
        //				clsWebRoomType thisWebRoomType = (clsWebRoomType) thisObject;
        //				int thisIndex = theseWebRoomTypes.IndexOf(thisObject);
        //
        //				thisWebRoomType.foundInLatestList = false;
        //			}
        //
        //			#endregion
        //
        //			#region Go through recent list and compare and contrast
        //
        //			for (int webRoomCounter = 0; webRoomCounter < numWebRoomTypesFound; webRoomCounter++)
        //			{
        //				currentIndex = nextEol + 2;
        //				nextEol = response.IndexOf(aCrLf, currentIndex);
        //				nextComma = response.IndexOf(aComma, currentIndex);
        //				//	lineToReadFrom = response.Substring(currentIndex, nextEol - currentIndex);
        //				clsWebRoomType thisWebRoomType = new clsWebRoomType();
        //
        //				thisWebRoomType.webRoomTypeId = 
        //					getIntFromString(response.Substring(currentIndex, nextComma - currentIndex));
        //
        //				thisWebRoomType.webRoomTypeName = 
        //					response.Substring(nextComma + 1, nextEol - 1 - nextComma);
        //
        //				thisWebRoomType.foundInLatestList = true;
        //
        //				bool foundThisWebRoom = false;
        //
        //				#region Try to find this Web Room Type in the existing list
        //				foreach(object thisObject in theseWebRoomTypes)
        //				{
        //					clsWebRoomType thisOldWebRoomType = (clsWebRoomType) thisObject;
        //					if (thisOldWebRoomType.webRoomTypeId == thisWebRoomType.webRoomTypeId)
        //					{
        //						foundThisWebRoom = true;
        //						thisOldWebRoomType.foundInLatestList = true;
        //						if (thisOldWebRoomType.webRoomTypeName != thisWebRoomType.webRoomTypeName)
        //							thisOldWebRoomType.webRoomTypeName = thisWebRoomType.webRoomTypeName;
        //
        //					}
        //
        //				}
        //
        //				#endregion
        //
        //				#region Add new additions
        //
        //				if (!foundThisWebRoom)
        //					theseWebRoomTypes.Add(thisWebRoomType);
        //
        //				#endregion
        //			}
        //			#endregion
        //
        //			#region Delete any WebRoom Types not found
        //
        //			#region Delete if not found
        //			for(int counter = 0; counter < theseWebRoomTypes.Count; counter++)
        //			{
        //				clsWebRoomType thisWebRoomTypeToDelete = (clsWebRoomType) theseWebRoomTypes[counter];
        //
        //				if (!thisWebRoomTypeToDelete.foundInLatestList)
        //				{
        //					theseWebRoomTypes.RemoveAt(counter);
        //					counter--;
        //				}
        //			}
        //
        //			#endregion
        //
        //			#endregion
        //
        //			#endregion
        //			
        //			return true;
        //		}

        #endregion

        #region getDetailServerParams (Gets Server Parameters from Server Response)

        //		private bool getDetailServerParams(string response)
        //		{
        //
        //			string lineToParse;
        //			int currentIndex = response.IndexOf(sLookAhead);
        //			int nextEol = response.IndexOf(aCrLf, currentIndex + 1);
        //			string paramstring;
        //
        //			lineToParse = response.Substring(currentIndex, nextEol - currentIndex);
        //			paramstring = lineToParse.Substring(sLookAhead.Length, lineToParse.Length - sLookAhead.Length);
        //			Lookahead = getIntFromString(paramstring);
        //
        //			currentIndex = response.IndexOf(sRefDate);
        //			lineToParse = response.Substring(currentIndex, response.Length - currentIndex);
        //			paramstring = lineToParse.Substring(sRefDate.Length, lineToParse.Length - sRefDate.Length);
        //			RefDate = getDateFromString(paramstring);
        //
        //			return true;
        //		}

        #endregion

        #region sendWebBookingsToRhs

        /// <summary>
        /// The purpose of this function is to take web bookings data recieved
        /// from the server and use this to create files that can be imported into RHS
        /// </summary>
        /// <returns>Success or failure of the function</returns>
        public bool sendWebBookingsToRhs(string thisRhsFolder)
		{
			string currentFunction = "sendWebBookingsToRhs";

			foreach(object thisObject in theseWebRoomBookings)
			{
				clsWebRoomBooking thisWebRoomBooking = (clsWebRoomBooking) thisObject;

				LogThis(DateTime.Now, "Booking Arrived to be Added to RHS: " + thisWebRoomBooking.confirmationCode.ToString(),
					"", currentFunction, "");

                if (thisWebRoomBooking.writeReservations(thisRhsFolder))
				{

					LogThis(DateTime.Now, "Booking Completely Added to RHS: " + thisWebRoomBooking.confirmationCode.ToString(),
						"", currentFunction, "");

					WebBookingsToAcknowledge.Add(thisWebRoomBooking.uid);
				}
			}

			return true;
		}
		#endregion


	}
}
