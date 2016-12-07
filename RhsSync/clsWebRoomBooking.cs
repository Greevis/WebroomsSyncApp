using System;
using System.IO;
using System.Collections;

namespace RhsSync
{
	/// <summary>
	/// Summary description for clsWebRoomBooking.
	/// </summary>
	public class clsWebRoomBooking : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsWebRoomBooking()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region Publicly Exposed Variables

		/// <summary>
		/// Structure for a Web Booking
		/// </summary>
//		public struct webRoomBooking
//		{

		public int numBookingsFound = 0;
			/// <summary>
			/// Booking Id (not visible to guest or motelier)
			/// </summary>
			public string uid;
			/// <summary>
			/// Confirmation Code (appears on guest/motelier emails)
			/// </summary>
			public string confirmationCode;
			/// <summary>
			/// Name of the person who made the booking
			/// </summary>
			public string contactName;
			/// <summary>
			/// Name of the person for whom the booking was made
			/// </summary>
			public string guestName;
			/// <summary>
			/// Contact Fax Number
			/// </summary>
			public ArrayList contactFax = new ArrayList();
			/// <summary>
			/// Contact Phone Number
			/// </summary>
            public ArrayList contactPhone = new ArrayList();
			/// <summary>
			/// Alternative Phone Number
			/// </summary>
            public ArrayList alternativePhone = new ArrayList();
			/// <summary>
			/// Contact Email Address
			/// </summary>
            public ArrayList contactEmail = new ArrayList();
			/// <summary>
			/// Contact Mail Address
			/// </summary>
			public string contactAddresss;
			/// <summary>
			/// Comments Added by the Contact
			/// </summary>
			public string comments;
			/// <summary>
			/// Payment Method Selected
			/// </summary>
			public int payment;
			/// <summary>
			/// Customer New or Returning Web Customer
			/// </summary>
			public bool returning;
			/// <summary>
			/// Customer Stayed at this place before?
			/// </summary>
			public bool stayedBefore;
			/// <summary>
			/// How the customer found the website
			/// </summary>
			public string howWebsiteFound;
			/// <summary>
			/// Name on Credit Card used to secure the booking
			/// </summary>
			public string ccName;
			/// <summary>
			/// Type of Credit Card used to secure the booking
			/// </summary>
			public string ccType;
			/// <summary>
			/// Expiry Date of the Credit Card used to secure the booking
			/// </summary>
			public string ccExpiry;
			/// <summary>
			/// Number on Credit Card used to secure the booking
			/// </summary>
			public string ccNumber;
			/// <summary>
			/// Date and Time the reservation was made
			/// </summary>
			public DateTime dateTimeResMade;
			/// <summary>
			/// Whether the booking was tentative or not
			/// </summary>
			public bool tentative;
			/// <summary>
			/// First night of the booking
			/// </summary>
			public DateTime firstNight;
			/// <summary>
			/// Last night of the booking
			/// </summary>
			public DateTime lastNight;
			/// <summary>
			/// Whether the booking is simple (true) or complex (false)
			/// </summary>
			public bool simple;
			/// <summary>
			/// Construction that contains details of the booking, if it was Simple
			/// </summary>
			public ArrayList roomsBooked;
			/// <summary>
			/// Construction that contains details of the booking, if it was Complex
			/// </summary>
			public ArrayList roomNightsBooked;

		#region Newly added

		/// <summary>
		/// Source of the booking
		/// </summary>
		public string WebRmSource;

		/// <summary>
		/// Guests Estimated Time of Arrival
		/// </summary>
		public string ETA;

		#endregion


//		}

//		#region bookingSummaryStruct

//		public struct bookingSummaryStruct
//		{
			/// <summary>
			/// Indicates if Costs and room configuration (number of Adults / Children)
			/// are consistant throughout this booking
			/// </summary>
			public bool costAndRoomsSameForEachNight;
			/// <summary>
			/// Maximum number of Adults staying at any point during this booking
			/// </summary>
			public int maxAdults;
			/// <summary>
			/// Maximum number of Children staying at any point during this booking
			/// </summary>
			public int maxChildren;
			/// <summary>
			/// Maximum number of Infants staying at any point during this booking
			/// </summary>
			public int maxInfants;
			/// <summary>
			/// Maximum number of Children plus Infants staying at any point during this booking
			/// </summary>
			public int maxChildrenAndInfants;
			/// <summary>
			/// Maximum number of People staying at any point during this booking
			/// </summary>
			public int maxPeople;
			/// <summary>
			/// Maximum cost of a single night during this booking
			/// </summary>
			public decimal maxCost;
			/// <summary>
			/// Total cost of this booking
			/// </summary>
			public decimal totalCost;
			/// <summary>
			/// Number of each web room type included in this booking.
			/// <note>At this stage each RHS booking is for a maximum of one room</note>
			/// </summary>
			public clsWebRoomsBookedIn[] maxWebRoomsBooked;
			/// <summary>
			/// First night of this booking
			/// </summary>
			public DateTime overallFirstNight;
			/// <summary>
			/// Last night of this booking
			/// </summary>
			public DateTime overallLastNight;
			/// <summary>
			/// Web Room Type for this booking 
			/// </summary>
			public int webRoomType;

		#endregion

		/// <summary>
		/// Local Guest instance
		/// </summary>
		public clsRhsGuest thisRhsGuest;

		private clsSetting thisSetting;

		#region getRhsGuestFromWebBooking

        #region GetPhoneNumberFromNumberProvided

        /// <summary>
        /// GetPhoneNumberFromNumberProvided
        /// </summary>
        /// <param name="NumberProvided">NumberProvided</param>
        /// <returns>Phone Number</returns>
        public string[] GetPhoneNumberFromNumberProvided(string NumberProvided)
        {
            int MaxPhoneLength = 14;
            int InitialGapLimit = 4;
            string PhonePrefix = "";
            string Phone = "";

            string tempPhone = NumberProvided.Replace(aCrLf, aSpace).Replace("-", aSpace).Trim();
            int phoneLength = tempPhone.Length;

            if (phoneLength > MaxPhoneLength)
            {
                tempPhone = tempPhone.Replace(aSpace, "").Trim();
                phoneLength = tempPhone.Length;
            }


            if (InitialGapLimit > phoneLength)
                InitialGapLimit = phoneLength;


            int indexForGap = tempPhone.Substring(0, InitialGapLimit).LastIndexOf(" ");

            switch (phoneLength)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    PhonePrefix = tempPhone.Trim();
                    Phone = ""; //No phone provided
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    if (indexForGap == -1)
                    {
                        //No Gap, just use first 3 digits
                        PhonePrefix = tempPhone.Substring(0, 3).Trim();
                        Phone = tempPhone.Substring(3).Trim();
                    }
                    else
                    {
                        PhonePrefix = tempPhone.Substring(0, indexForGap).Trim();
                        Phone = tempPhone.Substring(indexForGap + 1).Trim();
                    }

                    break;
                default: //Use the last 14 digits
                        Phone = tempPhone.Substring(tempPhone.Length - 11, 11);
                        PhonePrefix = tempPhone.Substring(tempPhone.Length - 14, 3);
                    break;

            }


            string[] thisPhone = new string[2];

            thisPhone[0] = PhonePrefix;
            thisPhone[1] = Phone;

            return thisPhone;
        }

        #endregion

        /// <summary>
		/// Returns Guest data as established from a Webrooms Booking
		/// </summary>
		/// <returns>Guest Data</returns>
		private clsRhsGuest getRhsGuestFromWebBooking()
		{
			string currentFunction = "getRhsGuestFromWebBooking";
			
			#region Constant Text Fields

			string overSizeAddressText = ("Ref:" + confirmationCode).ToUpper();
			string overSizePhoneText = ("Ref:" + confirmationCode).ToUpper();
			string commentText = comments;

			#endregion
			
			#region RhsGuest Data

			thisRhsGuest = new clsRhsGuest();

			if (guestName == "")
				thisRhsGuest.GuestName = contactName;
			else
				thisRhsGuest.GuestName = guestName;

			thisRhsGuest.GuestInHouse = "N";
			thisRhsGuest.GuestRoom = 0;

			thisRhsGuest.Child = maxChildrenAndInfants.ToString();			
			thisRhsGuest.Adult = maxAdults.ToString();

			#region Deal with Addresses
			//Divide the address into 'crlf' portions
			
			//Count number of CRLFs in address
			//string address = booking.contactAddresss.Trim();

			//Initialise the address string
			thisRhsGuest.Address1 = "";
			thisRhsGuest.Address2 = "";
			thisRhsGuest.Address3 = "";
			thisRhsGuest.Address4 = "";
			
			#region Count EOLs

			int currentIndex = 0;
			int nextEol = contactAddresss.IndexOf(aCrLf, currentIndex);
			int numEols = 0;
			while (nextEol > -1)
			{
				numEols++;
				currentIndex = nextEol + 1;
				nextEol = contactAddresss.IndexOf(aCrLf, currentIndex);
			}

			#endregion

			#region Get each line in the address supplied

			string[] adresss = new string[numEols + 1];
			int[] fieldLengths = new int[numEols + 1];
			bool[] lengthAcceptable = new bool[numEols + 1];
			int totalLength = 0;
			//Put data in the members
			currentIndex = 0;
			nextEol = contactAddresss.IndexOf(aCrLf, currentIndex);

			for (int counter = 0; counter < numEols + 1; counter++)
			{
				if (nextEol == -1)
					nextEol = contactAddresss.Length;
				adresss[counter] = contactAddresss.Substring(currentIndex, nextEol - currentIndex).Trim();
				fieldLengths[counter] = adresss[counter].Length;
				totalLength += fieldLengths[counter];
				lengthAcceptable[counter] = (fieldLengths[counter] < 26);
				currentIndex = nextEol + 2;
				if (currentIndex >= contactAddresss.Length)
					nextEol = contactAddresss.Length;
				else
					nextEol = contactAddresss.IndexOf(aCrLf, currentIndex);


				if (fieldLengths[counter] == 0)
				{
					//If this field is empty, get rid of it
					counter--;
					numEols--;
				}
			}

			#endregion
			
			bool allAcceptableLength = true;
			for (int counter = 0; counter < numEols; counter++)
				allAcceptableLength &= lengthAcceptable[counter];

			if ((numEols + 1 < 4) && (allAcceptableLength))
			{
				#region apply the settings
				for (int counter = 0; counter < numEols + 1; counter++)
				{
					switch (counter)
					{
						case 0:
							thisRhsGuest.Address1 = adresss[counter].Replace(aCrLf, aSpace);
							break;
						case 1:
							thisRhsGuest.Address2 = adresss[counter].Replace(aCrLf, aSpace);
							break;
						case 2:
							thisRhsGuest.Address3 = adresss[counter].Replace(aCrLf, aSpace);
							break;
						case 3:
							thisRhsGuest.Address4 = adresss[counter].Replace(aCrLf, aSpace);
							break;
						default:
							break;
					}
				}
				#endregion
			}
			else //At least one bad length
			{
//				thisRhsGuest.Address4 = "Ref:" + confirmationCode;
					
				#region Restrict each line to 25 characters
				for (int counter = 0; counter < numEols + 1; counter++)
				{

					if (adresss[counter].Length > 25)
						adresss[counter] = adresss[counter].Substring(0, 25).Replace(aCrLf, aSpace);

					if (adresss[counter].Length > 25)
						adresss[counter] = adresss[counter].Substring(0, 25).Replace(aSpace, "");

					if (adresss[counter].Length > 25)
					{
						if (counter < 3) 
						{
							adresss[counter + 1] = adresss[counter].Substring(25) + " " + adresss[counter + 1];
						}
						adresss[counter] = adresss[counter].Substring(0, 25);
					}

					int thisAddressLength = adresss[counter].Length;
					if (thisAddressLength > 25)
						thisAddressLength = 25;

					
					switch (counter)
					{
						case 0:
							thisRhsGuest.Address1 = adresss[counter].Substring(0, thisAddressLength);
							break;
						case 1:
							thisRhsGuest.Address2 = adresss[counter].Substring(0, thisAddressLength);
							break;
						case 2:
							thisRhsGuest.Address3 = adresss[counter].Substring(0, thisAddressLength);
							break;
						case 3:
							thisRhsGuest.Address4 = adresss[counter].Substring(0, thisAddressLength);
							break;
						default:
							break;
					}
				}
				#endregion
			}

			#endregion

			thisRhsGuest.Rego = "";

			#region Phone Numbers

//            string[] thisResult = GetPhoneNumberFromNumberProvided(contactPhone);


            string thisPhoneToUse = "";

            if (thisPhoneToUse == "" && alternativePhone.Count > 0 && ((string) alternativePhone[0]) != "")
                thisPhoneToUse = (string) alternativePhone[0];

            string[] thisResult = GetPhoneNumberFromNumberProvided(thisPhoneToUse);
 //            string[] thisResult = GetPhoneNumberFromNumberProvided(alternativePhone);
            thisRhsGuest.PhonePrefix = thisResult[0];
            thisRhsGuest.Phone = thisResult[1];

			#endregion

			thisRhsGuest.GuestClass = 1;
			thisRhsGuest.GuestRate = 0;

			#region Guest Nights and Days Is
			
			clsDaysBetween tempDaysBetween = daysBetween(firstNight, lastNight);
			if (tempDaysBetween.errDescription.success)
				thisRhsGuest.Nights = (short) tempDaysBetween.daysBetween;
			else
			{
				LogThis(DateTime.Now, "thisRhsGuest.Nights out of Range",
					tempDaysBetween.errDescription.errorForUser, 
					currentFunction,
					"firstNight = " + firstNight.ToString()
					+ " lastNight = " + lastNight.ToString());
				
				thisRhsGuest.Nights = 0;
			}

			thisRhsGuest.DateIn.day = (short) firstNight.Day;
			thisRhsGuest.DateIn.month = (short) firstNight.Month;
			thisRhsGuest.DateIn.year = (short) firstNight.Year;

			#endregion

			thisRhsGuest.Comment = commentText.Replace(aCrLf, aSpace);

			#region Voucher

			thisRhsGuest.Voucher = confirmationCode;

			#endregion

			thisRhsGuest.Regular = 0;

			thisRhsGuest.ExtRec = 0;
			thisRhsGuest.Notes = 0;
			thisRhsGuest.MemberNo = 0;
			thisRhsGuest.Group = 0;

			#region Guest Accounts
			thisRhsGuest.Trans = new rhsAccount[2];
			for (int counter = 0; counter < 2; counter++)
			{
				thisRhsGuest.Trans[counter].First = 0;
				thisRhsGuest.Trans[counter].Last = 0;
				thisRhsGuest.Trans[counter].Allocated = 0;
				thisRhsGuest.Trans[counter].Guest = 0;
				thisRhsGuest.Trans[counter].Size = 0;
				thisRhsGuest.Trans[counter].Pay = 0;
				thisRhsGuest.Trans[counter].Debtor = 0;
				thisRhsGuest.Trans[counter].Invoice = 0;
				thisRhsGuest.Trans[counter].Printed = 0;

				thisRhsGuest.Trans[counter].Balance = 0;
				thisRhsGuest.Trans[counter].Tax = 0;

				thisRhsGuest.Trans[counter].Name = "";
				thisRhsGuest.Trans[counter].Unused = "";			
			}

			//The first account always has the name "MAIN".
			thisRhsGuest.Trans[0].Name = "MAIN";
			#endregion

			thisRhsGuest.Contact = contactName;

			#region Mobile Phone
//            thisResult = GetPhoneNumberFromNumberProvided(alternativePhone);
            thisPhoneToUse = "";
            if (thisPhoneToUse == "" && contactPhone.Count > 0 && ((string)contactPhone[0]) != "")
                thisPhoneToUse = (string)contactPhone[0];

            thisResult = GetPhoneNumberFromNumberProvided(thisPhoneToUse);
            thisRhsGuest.Mobile = thisResult[0] + " " + thisResult[1];

            int MaxLength = 14;

            if (thisRhsGuest.Mobile.Length > MaxLength)
                thisRhsGuest.Mobile.Replace(" ", "");

            if (thisRhsGuest.Mobile.Length > MaxLength)
                thisRhsGuest.Mobile = thisRhsGuest.Mobile.Substring(0, MaxLength);

			#endregion

			thisRhsGuest.Spare = "";

			thisRhsGuest.Company = 0;

			#region ETA (Arrival Desc)

			thisRhsGuest.ArrivalDesc = ETA;

			#endregion

			thisRhsGuest.DepartDesc = "";

			thisRhsGuest.Origin = 0;
			thisRhsGuest.GSTExcl = 0;
			thisRhsGuest.Source = 0;

			#region Credit Card; Add spaces to make this readable
			thisRhsGuest.CreditCard = ccNumber;
			int thisLength = thisRhsGuest.CreditCard.Length;
			if (thisLength > 4)
				thisRhsGuest.CreditCard = thisRhsGuest.CreditCard.Substring(0, thisLength - 4) + " "
					+ thisRhsGuest.CreditCard.Substring(thisLength - 4);
			if (thisLength > 8)
				thisRhsGuest.CreditCard = thisRhsGuest.CreditCard.Substring(0, thisLength - 8) + " "
					+ thisRhsGuest.CreditCard.Substring(thisLength - 8);
			if (thisLength > 12)
				thisRhsGuest.CreditCard = thisRhsGuest.CreditCard.Substring(0, thisLength - 12) + " "
					+ thisRhsGuest.CreditCard.Substring(thisLength - 12);

			#endregion

			//Expiry Date comes in format mm/yyyy. 
			//We need to change this to format mm/yy (i.e get rid of the first two year fields)
			if (ccExpiry.Trim() == "")
				thisRhsGuest.ExpiryDate = "";
			else
			{
				int slashPos = ccExpiry.IndexOf(@"/");
				if (slashPos != -1 && slashPos != 0 && slashPos != ccExpiry.Length)
					thisRhsGuest.ExpiryDate = ccExpiry.Substring(0,slashPos + 1) + ccExpiry.Substring(ccExpiry.Length-2);
			}

			#region fax

            thisPhoneToUse = "";
            if (thisPhoneToUse == "" && contactFax.Count > 0 && ((string)contactFax[0]) != "")
                thisPhoneToUse = (string)contactFax[0];

            thisResult = GetPhoneNumberFromNumberProvided(thisPhoneToUse);
            thisRhsGuest.FaxPrefix = thisResult[0];
            thisRhsGuest.FaxNumber = thisResult[1];


            //thisRhsGuest.FaxPrefix= "";//TODO: Pending document Change
            //thisRhsGuest.FaxNumber = ""; //TODO: Pending document Change

            ////Deal with FaxPrefix (Max length: 4) and Fax (Max length: 10)
            //string tempFax = contactFax.Replace(aCrLf, aSpace).Trim();
            //int FaxLength = tempFax.Length;
            //indexForGap = -1;

            //if (FaxLength < 14)
            //{
            //    //We can easily fit everything in
            //    //Can we find a space in the first 4 digits?
            //    if (FaxLength < 4)
            //    {
            //        thisRhsGuest.FaxPrefix = tempFax.Trim();
            //        thisRhsGuest.FaxNumber = ""; //No Fax provided
            //    }
            //    else
            //    {
            //        indexForGap = tempFax.Substring(0, 4).LastIndexOf(" ");
            //        if (indexForGap == -1)
            //        {
            //            //No Gap, just use first 4 digits
            //            thisRhsGuest.FaxPrefix = tempFax.Substring(0,4).Trim();
            //            thisRhsGuest.FaxNumber = tempFax.Substring(4).Trim();
            //        }
            //        else
            //        {
            //            thisRhsGuest.FaxPrefix = tempFax.Substring(0,indexForGap).Trim();
            //            thisRhsGuest.FaxNumber = tempFax.Substring(indexForGap + 1).Trim();
            //        }
            //    }			
            //}
            //else
            //{
            //    //Take all the spaces out and see if everything will fit
            //    tempFax = tempFax.Replace(" ", "").Trim();
            //    if (tempFax.Length < 14)
            //    {
            //        //We can easily fit everything in
            //        //Can we find a space in the first 4 digits?
            //        if (FaxLength < 4)
            //        {
            //            thisRhsGuest.FaxPrefix = tempFax.Trim();
            //            thisRhsGuest.FaxNumber = ""; //No Fax provided
            //        }
            //        else
            //        {
            //            indexForGap = tempFax.Substring(0, 4).LastIndexOf(" ");
            //            if (indexForGap == -1)
            //            {
            //                //No Gap, just use first 4 digits
            //                thisRhsGuest.FaxPrefix = tempFax.Substring(0,4).Trim();
            //                thisRhsGuest.FaxNumber = tempFax.Substring(4).Trim();
            //            }
            //            else
            //            {
            //                thisRhsGuest.FaxPrefix = tempFax.Substring(0,indexForGap).Trim();
            //                thisRhsGuest.FaxNumber = tempFax.Substring(indexForGap + 1).Trim();
            //            }
            //        }			

            //    }
            //    else	
            //    {
            //        thisRhsGuest.FaxPrefix = "";
            //        thisRhsGuest.FaxNumber = overSizePhoneText;
            //    }
            //}
			#endregion

            thisRhsGuest.UserComment = "";
            if (contactEmail.Count > 0 && ((string) contactEmail[0]) != "")
                thisRhsGuest.UserComment = ((string) contactEmail[0]).Replace(aCrLf, aSpace).Trim();

			thisRhsGuest.Departed = "N";
			thisRhsGuest.DepartTime = "";
			thisRhsGuest.RegularNum = "";
			thisRhsGuest.InvoiceType = "";

			thisRhsGuest.GuestRate2 = 0;

			thisRhsGuest.ForexType = 0;

			#region Packages
			thisRhsGuest.Package = new string[5];

			for (int counter = 0; counter < 5; counter ++)
				thisRhsGuest.Package[counter] = "\0\0\0\0\0\0\0\0\0\0\0None";

			#endregion

			thisRhsGuest.RoomType = 0;

			#region Extension

			thisRhsGuest.Extension = new short[4];
			for (int counter = 0; counter < 4; counter ++)
				thisRhsGuest.Extension[counter] = 0;

			#endregion

			thisRhsGuest.LockExt = "";
			thisRhsGuest.CheckedIn = "";
			thisRhsGuest.GuestType = "";
			thisRhsGuest.ResNumber = "";
			thisRhsGuest.PPV = "";
			thisRhsGuest.ADContent = "";
			thisRhsGuest.Wireless = "";
			thisRhsGuest.BBUsage = "";

            thisRhsGuest.email = "";
            if (contactEmail.Count > 0 && ((string)contactEmail[0]) != "")
                thisRhsGuest.email = ((string)contactEmail[0]).Replace(aCrLf, aSpace).Trim();

			thisRhsGuest.HSIPortStatus = "";
			thisRhsGuest.GuestHSIPort = "";
			thisRhsGuest.VIPGuest = "";

			#region HSICodes

			thisRhsGuest.HSICodes = new string[2];
			for (int counter = 0; counter < 2; counter ++)
				thisRhsGuest.HSICodes[counter] = "";

			#endregion

			thisRhsGuest.IPTraxStep = "";
			thisRhsGuest.RoomAlias = "";

			#region Expanded

			thisRhsGuest.Expanded = confirmationCode.Trim();

			if (thisRhsGuest.Expanded.Length < 200)
				thisRhsGuest.Expanded = thisRhsGuest.Expanded.PadLeft(200 - thisRhsGuest.Expanded.Length, ' ');

			#endregion

			thisRhsGuest.WebRmNumber = confirmationCode;

			#endregion

			return thisRhsGuest;
		}

		#endregion

		#region writeReservations to file


		/// <summary>
		/// Gets a Template for each new reservation
		/// </summary>
		/// <returns>A template for a new Reservation</returns>
		public clsRhsReservation GenerateReservationTemplate()
		{
			#region Assemble and Initialise all data that will be common to all rhs Reservations

			clsRhsReservation thisRhsReservation = new clsRhsReservation();

			thisRhsReservation.Number = "";

			thisRhsReservation.MadDate.day = (short) dateTimeResMade.Day;
			thisRhsReservation.MadDate.month = (short) dateTimeResMade.Month;
			thisRhsReservation.MadDate.year = (short) dateTimeResMade.Year;

			#region Deal with confirmed booking behaviour

			bool showAsUnconfirmed = true;

			switch((confirmedBookingBehaviourType) thisSetting.ConfirmedBookingBehaviour)
			{
				case confirmedBookingBehaviourType.markAllConfirmed:
					showAsUnconfirmed = false;
					break;				
				case confirmedBookingBehaviourType.markAllUnconfirmed:
					showAsUnconfirmed = true;
					break;
				case confirmedBookingBehaviourType.markUnconfirmedAsConfirmedInRoonsoftAndViceVersa:
					showAsUnconfirmed = !tentative;
					break;
				case confirmedBookingBehaviourType.markConfirmedAsConfirmedInRoonsoftAndViceVersa:
				default:
					showAsUnconfirmed = tentative;
					break;

			}

			if (showAsUnconfirmed)
			{
				thisRhsReservation.Confirmed = "N";
				thisRhsReservation.ConDate.day = 0;
				thisRhsReservation.ConDate.month = 0;
				thisRhsReservation.ConDate.year = 0;
			}
			else
			{
				thisRhsReservation.Confirmed = "Y";
				thisRhsReservation.ConDate.day = (short) dateTimeResMade.Day;
				thisRhsReservation.ConDate.month = (short) dateTimeResMade.Month;
				thisRhsReservation.ConDate.year = (short) dateTimeResMade.Year;
			}

			#endregion


			thisRhsReservation.WaitList = "N";
			thisRhsReservation.XtraDat = 0;
			thisRhsReservation.ForexType = 0;
			thisRhsReservation.Dummy = "";
			thisRhsReservation.RTYPE = 0;
			thisRhsReservation.Comment = new string[2];
			
			thisRhsReservation.Comment[0] = "";
			thisRhsReservation.Comment[1] = "";

			thisRhsReservation.WebRmSource = WebRmSource.Trim();
			if (thisRhsReservation.WebRmSource.Length < 40)
				thisRhsReservation.WebRmSource = thisRhsReservation.WebRmSource.PadLeft(40 - thisRhsReservation.WebRmSource.Length, ' ');

			thisRhsReservation.Expanded = confirmationCode.Trim();
			if (thisRhsReservation.Expanded.Length < 205)
				thisRhsReservation.Expanded = thisRhsReservation.Expanded.PadLeft(205 - thisRhsReservation.Expanded.Length, ' ');


			thisRhsReservation.Rates = new double[33];
			thisRhsReservation.Rates2 = new double[33];

			for (int rhsRoomTypeCounter = 0; rhsRoomTypeCounter < 33; rhsRoomTypeCounter++)
			{
				thisRhsReservation.Rates[rhsRoomTypeCounter] = 0;
				thisRhsReservation.Rates2[rhsRoomTypeCounter] = 0;
			}

			#endregion

			return thisRhsReservation;

		}


		#region allocateSingleWebRoomToRhsRooms

		/// <summary>
		/// Allocate Single Web Rooms Booking To Rhs Rooms
		/// </summary>
		/// <param name="webRoomTypeId">Id of Web Room</param>
		/// <param name="dtFirstNight">Firstnight of booking</param>
		/// <param name="numNights">Number of Nights for the booking</param>
		/// <returns>Array of Room Types where arooms have been allocated</returns>
		public short[] allocateSingleWebRoomToRhsRooms(
			int webRoomTypeId, 
			DateTime dtFirstNight, 
			int numNights)
		{

			string currentFunction = "allocateSingleWebRoomToRhsRooms";

			#region Initialise return value and local variables
			short[] retRooomTypeBookings = new short[33];

			for(int counter = 0; counter < 32; counter++)
			{
				retRooomTypeBookings[counter] = 0;
			}

			int webRoomNumber = -1;
			int defaultRhsRoomTypeIndexGroup1 = 0;
			int rhsRoomTypeIndexGroup1 = 0;
			int defaultRhsRoomTypeIndexGroup2 = 0;
			int rhsRoomTypeIndexGroup2 = 0;

			#endregion

			#region initiate First and Last night

			int lFirstNight;
			clsDaysBetween  tempDaysBetween = daysBetween(thisSetting.RefDate, 
				dtFirstNight);
			if (tempDaysBetween.errDescription.success)
				lFirstNight = (int) tempDaysBetween.daysBetween - 1;
			else
			{
				LogThis(DateTime.Now, "lFirstNight out of Range",
					tempDaysBetween.errDescription.errorForUser, 
					currentFunction,
					"thisSetting.RefDate = " + thisSetting.RefDate.ToString()
					+ " dtFirstNight = " 
					+ dtFirstNight.ToString());
				
				lFirstNight = 0;
			}

			int lLastNight = lFirstNight + numNights;
			#endregion

			#region Deal with Allocations

			if (thisSetting.NumWebRoomTypes > 0)
			{

				#region Find the Web Room type
				for (int counter = 0 ; counter < thisSetting.theseWebRoomTypes.Count; counter++)
				{
					if (((clsWebRoomType) thisSetting.theseWebRoomTypes[counter]).webRoomTypeId == webRoomTypeId)
						webRoomNumber = counter;
				}
                #endregion

                #region Can't find Web Room Id in Webrooms, try the first one
                if (webRoomNumber == -1)
				{
					LogThis(DateTime.Now, "Unable to allocate Web Booking to RHS Room Type", 
						"Web Room Id: " + webRoomTypeId.ToString() + " not found",	currentFunction, "");	
					webRoomNumber = 0;
				}
				#endregion

				#region Obtain Web Room Type
				bool WebRoomTypePresent = true;

				clsWebRoomType thisWebRoomType = new clsWebRoomType();

				if (thisSetting.theseWebRoomTypes.Count < webRoomNumber)
					WebRoomTypePresent = false; 
				else
					thisWebRoomType = (clsWebRoomType) thisSetting.theseWebRoomTypes[webRoomNumber];


				#endregion

				#region Are there any associated RHS Room Types?
				int numAssRhsRoomTypes = 0;

				numAssRhsRoomTypes = thisWebRoomType.RhsRoomTypesMappedToGroup1.Count;

				if (numAssRhsRoomTypes == 0)
				{
					LogThis(DateTime.Now, "Unable to allocate Web Booking to RHS Room Type", 
						"Web Room Type does not exist in current settings: " + webRoomNumber.ToString(),	
						currentFunction, 
						"webRoomNumber: " + webRoomNumber.ToString()
						+ "webRoomTypeId: " + webRoomTypeId.ToString());
				}
				else
					defaultRhsRoomTypeIndexGroup1 = (int) thisWebRoomType.RhsRoomTypesMappedToGroup1[0];

				#endregion

				bool firstRoomAllocated = false;
				bool secondRoomAllocated = false;
				int priorityRhsRoomCounter = 0;

				#region Attempt to find an Rhs Room To Allocated to for Group 1
			
				while ((!firstRoomAllocated) &&
					(priorityRhsRoomCounter < numAssRhsRoomTypes))
				{
					#region Get the Rhs Room Type
					rhsRoomTypeIndexGroup1 = (int) thisWebRoomType.RhsRoomTypesMappedToGroup1[priorityRhsRoomCounter];

					clsRhsRoomType thisRhsRoomType = new clsRhsRoomType();

					if (thisSetting.theseRhsRoomTypes.Count >= rhsRoomTypeIndexGroup1)
						thisRhsRoomType = (clsRhsRoomType) thisSetting.theseRhsRoomTypes[rhsRoomTypeIndexGroup1];

					#endregion

					#region Check we have availability for this Room Type

					bool availabilityIsPresent = false;
					
					if (thisRhsRoomType.Availability == null 
						|| thisRhsRoomType.Availability.Count < lLastNight + 1
						|| lFirstNight < 0
						)
					{
						if (thisRhsRoomType.Availability == null)
							LogThis(DateTime.Now, "Can not check full duration of the booking", 
								" Booking goes out of bounds of availability database",	
								currentFunction, 
								"thisRhsRoomType.Availability.Count: Null"
								+ ", thisRhsRoomType.roomTypeId: " + (thisRhsRoomType.roomTypeId).ToString()
								+ ", lFirstNight: " + lFirstNight.ToString()
								+ ", lLastNight: " + lLastNight.ToString()
								);	
						else
							LogThis(DateTime.Now, "Can not check full duration of the booking", 
								" Booking goes out of bounds of availability database",	
								currentFunction, 
								"thisRhsRoomType.Availability.Count:" + thisRhsRoomType.Availability.Count.ToString()
								+ ", thisRhsRoomType.roomTypeId: " + (thisRhsRoomType.roomTypeId).ToString()
								+ ", lFirstNight: " + lFirstNight.ToString()
								+ ", lLastNight: " + lLastNight.ToString()
								);	

					}
					else
						availabilityIsPresent = true;

					#endregion

					#region Check the room type's Availability
					bool roomTypeHasRoomsAvailable = false;
					if (availabilityIsPresent)
					{
						roomTypeHasRoomsAvailable = true;
						for (int dayCounter = lFirstNight; dayCounter < lLastNight + 1; dayCounter++)
						{
							if ((int) thisRhsRoomType.Availability[dayCounter] < 1)
								roomTypeHasRoomsAvailable = false;
						}
					}
					#endregion

					#region Decrement Availability if this is our room type
					if (roomTypeHasRoomsAvailable)
					{
						for (int dayCounter = lFirstNight; dayCounter < lLastNight + 1; dayCounter++)
							thisRhsRoomType.Availability[dayCounter] = (int) thisRhsRoomType.Availability[dayCounter] - 1;

						//Save this Availability Change
						thisSetting.Save();
					}
					#endregion

					firstRoomAllocated = roomTypeHasRoomsAvailable;

					priorityRhsRoomCounter++;
				}

				#endregion

				#region Add this to the Array (Regardless if position is actually found or not)
				if (rhsRoomTypeIndexGroup1 == 0)
					rhsRoomTypeIndexGroup1 = defaultRhsRoomTypeIndexGroup1;

				//Offset by one for the array
				rhsRoomTypeIndexGroup1++; 
				retRooomTypeBookings[rhsRoomTypeIndexGroup1]++;

				#endregion

				#region Deal with Interconnecting Rooms
				if (WebRoomTypePresent && thisWebRoomType.IsInterConnecting != 0 && thisWebRoomType.RhsRoomTypesMappedToGroup2.Count > 0)
				{
					//We have a second room to allocate
					#region Attempt to find an Rhs Room To Allocated to for Group 2
					numAssRhsRoomTypes = thisWebRoomType.RhsRoomTypesMappedToGroup2.Count;

					priorityRhsRoomCounter = 0;
			
					while ((!secondRoomAllocated) &&
						(priorityRhsRoomCounter < numAssRhsRoomTypes))
					{
						#region Get the Rhs Room Type
						rhsRoomTypeIndexGroup2 = (int) thisWebRoomType.RhsRoomTypesMappedToGroup2[priorityRhsRoomCounter];

						clsRhsRoomType thisRhsRoomType = new clsRhsRoomType();

						if (thisSetting.theseRhsRoomTypes.Count >= rhsRoomTypeIndexGroup2)
							thisRhsRoomType = (clsRhsRoomType) thisSetting.theseRhsRoomTypes[rhsRoomTypeIndexGroup2];

						#endregion

						#region Check we have availability for this Room Type

						bool availabilityIsPresent = false;
					
						if (thisRhsRoomType.Availability == null 
							|| thisRhsRoomType.Availability.Count < lLastNight
							|| lFirstNight < 0
							)
						{
							if (thisRhsRoomType.Availability == null)
								LogThis(DateTime.Now, "Can not check full duration of the booking", 
									" Booking goes out of bounds of availability database",	
									currentFunction, 
									"thisRhsRoomType.Availability.Count: Null"
									+ ", thisRhsRoomType.roomTypeId: " + (thisRhsRoomType.roomTypeId).ToString()
									+ ", lFirstNight: " + lFirstNight.ToString()
									+ ", lLastNight: " + lLastNight.ToString()
									);	
							else
								LogThis(DateTime.Now, "Can not check full duration of the booking", 
									" Booking goes out of bounds of availability database",	
									currentFunction, 
									"thisRhsRoomType.Availability.Count:" + thisRhsRoomType.Availability.Count.ToString()
									+ ", thisRhsRoomType.roomTypeId: " + (thisRhsRoomType.roomTypeId).ToString()
									+ ", lFirstNight: " + lFirstNight.ToString()
									+ ", lLastNight: " + lLastNight.ToString()
									);	
						}
						else
							availabilityIsPresent = true;

						#endregion

						#region Check the room type's Availability
						bool roomTypeHasRoomsAvailable = false;
						if (availabilityIsPresent)
						{
							roomTypeHasRoomsAvailable = true;
							for (int dayCounter = lFirstNight; dayCounter < lLastNight + 1; dayCounter++)
							{
								if ((int) thisRhsRoomType.Availability[dayCounter] < 1)
									roomTypeHasRoomsAvailable = false;
							}
						}
						#endregion

						#region Decrement Availability if this is our room type
						if (roomTypeHasRoomsAvailable)
						{
							for (int dayCounter = lFirstNight; dayCounter < lLastNight + 1; dayCounter++)
								thisRhsRoomType.Availability[dayCounter] = (int) thisRhsRoomType.Availability[dayCounter] - 1;

							//Save this Availability Change
							thisSetting.Save();
						}
						#endregion

						secondRoomAllocated = roomTypeHasRoomsAvailable;

						priorityRhsRoomCounter++;
					}

					#endregion

					#region Add this to the Array (Regardless if position is actually found or not)
					if (rhsRoomTypeIndexGroup2 == 0)
						rhsRoomTypeIndexGroup2 = defaultRhsRoomTypeIndexGroup2;

					//Offset by one for the array
					rhsRoomTypeIndexGroup2++; 
					retRooomTypeBookings[rhsRoomTypeIndexGroup2]++;

					#endregion

				}

				#endregion

			}
			else
			{
				#region Add this to the Array (Regardless if position is actually found or not)
				if (rhsRoomTypeIndexGroup1 == 0)
					rhsRoomTypeIndexGroup1 = defaultRhsRoomTypeIndexGroup1;

				//Offset by one for the array
				rhsRoomTypeIndexGroup1++; 
				retRooomTypeBookings[rhsRoomTypeIndexGroup1]++;

				#endregion

			}

			#endregion

			return retRooomTypeBookings;
		}

		#endregion


		/// <summary>
		/// Take the data in from the clsWebRoomBooking format and return it 
		/// in an array of clsRhsReservation contructs 
		/// </summary>
		/// <returns>Success or Failure</returns>
		public bool writeReservations(string thisRhsFolder)
		{
			#region Load Settings and Initialise constants
			string currentFunction = "writeReservations";

            //2016: Why do we need settings here? Can we just load the ones we need?
			//thisSetting = new clsSetting();
			//thisSetting.LogThis += new clsSetting.DelLogThis(LogThis);
   //         //thisSetting.GetSettings(thisWebRoomsForm);
   //         thisSetting.LoadSettingsFromFile(thisRhsFolder);


			string overSizeCommentText = ("Guest Comment too long; Ref: " + confirmationCode).ToUpper();
			string peopleCostsChangeDuringBooking = (@"Num People and/or cost of room change over booking; see ref: " + confirmationCode).ToUpper();

			#endregion

			#region Create the Bookings folder if it does not exist

			DirectoryInfo di = new DirectoryInfo(thisSetting.OnlineBookingsFolder);
			if (!di.Exists)
				di.Create();

			#endregion

			#region Get thisRhsGuest information

            clsRhsGuest thisRhsGuest = getRhsGuestFromWebBooking();

			#endregion

			#region Get the maximum number of Rooms Booked of each Web Room Type

			maxWebRoomsBooked = new clsWebRoomsBookedIn[thisSetting.theseWebRoomTypes.Count];
			
			for (int counter = 0; counter < thisSetting.theseWebRoomTypes.Count; counter++)
			{
				maxWebRoomsBooked[counter] = new clsWebRoomsBookedIn();
				maxWebRoomsBooked[counter].webRoomTypeId = ((clsWebRoomType) thisSetting.theseWebRoomTypes[counter]).webRoomTypeId;
			}
			#endregion

			#region Initialise data and check for Room Nights

			int totalRoomNights = roomNightsBooked.Count;

			int numRhsReservations = 0;

			if (totalRoomNights == -1) 
			{
				LogThis(DateTime.Now, "Error: Booking has no Room Nights: " + confirmationCode,
					ToString(), currentFunction, "");
				return false;
			}

			#endregion

			#region Move the Room Nights into a collection of RHS Reservations

			ArrayList ListOfReservations = new ArrayList();

	
			while (roomNightsBooked.Count > 0)
			{
				#region Instantiate a new Reservation

				clsRhsReservation thisRhsReservation = GenerateReservationTemplate();

				ArrayList WebRoomTypes = new ArrayList();

				#region Obtain the first and take it out of the list

				clsWebRoomNightComplex thisWebRoomNightComplex = (clsWebRoomNightComplex) roomNightsBooked[0];
				roomNightsBooked.Remove(thisWebRoomNightComplex);

				#endregion

				#region Set Reservation Day, Duration and People

				firstNight = thisWebRoomNightComplex.date;
				webRoomType = thisWebRoomNightComplex.roomType;	
				costAndRoomsSameForEachNight = true;

				thisRhsReservation.ResDate.day = (short) thisWebRoomNightComplex.date.Day;
				thisRhsReservation.ResDate.month = (short) thisWebRoomNightComplex.date.Month;
				thisRhsReservation.ResDate.year = (short) thisWebRoomNightComplex.date.Year;
				thisRhsReservation.Nights = 1;
				thisRhsReservation.GroupRate = thisWebRoomNightComplex.costOfRoomPerNight;

				thisRhsReservation.MaxAdults = thisWebRoomNightComplex.numAdults;
				thisRhsReservation.MaxChildren= thisWebRoomNightComplex.numChildren;
				thisRhsReservation.MaxInfants = thisWebRoomNightComplex.numInfants;

				thisRhsReservation.People = (short) (thisWebRoomNightComplex.numChildren 
					+ thisWebRoomNightComplex.numInfants
					+ thisWebRoomNightComplex.numAdults);
                
				#endregion

				#region Get Adjoining RoomNights for the reservation

				bool foundAnotherRoomNight = true;

				while (foundAnotherRoomNight)
				{
					foundAnotherRoomNight = false;

					for(int nwrncCounter = 0; nwrncCounter < roomNightsBooked.Count; nwrncCounter++)
					{
						clsWebRoomNightComplex thisNewWebRoomNightComplex = (clsWebRoomNightComplex) roomNightsBooked[nwrncCounter];

						if (!foundAnotherRoomNight && thisNewWebRoomNightComplex.roomType == webRoomType
							&& thisNewWebRoomNightComplex.date == firstNight.AddDays(thisRhsReservation.Nights))
						{
							#region We have a subsequent night. Note the last night remove from roomNightsBooked

							foundAnotherRoomNight = true;
							thisRhsReservation.Nights++;

							#region Deal with NumPeople
							short thisRoomNightNumPeople = (short) (thisNewWebRoomNightComplex.numChildren 
								+ thisNewWebRoomNightComplex.numInfants
								+ thisNewWebRoomNightComplex.numAdults);

							if(thisRhsReservation.People != thisRoomNightNumPeople)
							{
								costAndRoomsSameForEachNight = false;

								if (thisRhsReservation.People < thisRoomNightNumPeople)
									thisRhsReservation.People = thisRoomNightNumPeople;
							}
							#endregion

							#region Deal with Costs

							if (thisRhsReservation.GroupRate != thisNewWebRoomNightComplex.costOfRoomPerNight)
							{
								thisRhsReservation.GroupRate = (thisNewWebRoomNightComplex.costOfRoomPerNight 
									+ (thisRhsReservation.GroupRate * (thisRhsReservation.Nights - 1))) / thisRhsReservation.Nights;
								costAndRoomsSameForEachNight = false;
							}

							#endregion

							roomNightsBooked.Remove(thisNewWebRoomNightComplex);
							nwrncCounter--;

							#endregion
						}
					}
				}

				#endregion

				#region Lets get the room type

				#region Get the appropriate RHS Room Type(s) to allocate the room to

				LogThis(DateTime.Now, "Allocating Web Room Type to available RHS Room Type(s)",
					"", currentFunction, webRoomType.ToString());
				
				short[] rhsRoomTypeToAllocateTo = allocateSingleWebRoomToRhsRooms(webRoomType,
					firstNight, thisRhsReservation.Nights);


				#region Find the Web Room type
				for (int counter = 0 ; counter < thisSetting.theseWebRoomTypes.Count; counter++)
				{
					if (((clsWebRoomType) thisSetting.theseWebRoomTypes[counter]).webRoomTypeId == webRoomType)
					{
						clsWebRoomTypeAllocation thisWebRoomTypeAllocation = new clsWebRoomTypeAllocation();
						thisWebRoomTypeAllocation.WebRoomTypeId = webRoomType;
						thisWebRoomTypeAllocation.WebRoomTypeName = ((clsWebRoomType) thisSetting.theseWebRoomTypes[counter]).webRoomTypeName;
						thisWebRoomTypeAllocation.NumberOfThisType = 1;

						thisRhsReservation.WebRoomTypeAllocations.Add(thisWebRoomTypeAllocation);

					}
				}
				#endregion

				string Allocations = "";
				for (int counter = 0; counter < rhsRoomTypeToAllocateTo.GetUpperBound(0) + 1; counter++)
				{
					Allocations += rhsRoomTypeToAllocateTo[counter].ToString() + ";";
				}
				LogThis(DateTime.Now, "Web Room Type allocated",
					"", currentFunction, Allocations);

				thisRhsReservation.Rooms = new short[33];

				for (int rhsRoomTypeCounter = 1; rhsRoomTypeCounter < 33; rhsRoomTypeCounter++)
				{
					thisRhsReservation.Rooms[rhsRoomTypeCounter] += rhsRoomTypeToAllocateTo[rhsRoomTypeCounter];
				}

				#endregion

				#region Sort out comments

				string tempComment = comments;

				thisRhsReservation.Comment[0] = "";

				if (costAndRoomsSameForEachNight)
				{
					if (tempComment.Length > 74)
					{
						thisRhsReservation.Comment[1] = overSizeCommentText;
					}
					else
						thisRhsReservation.Comment[1] = tempComment.Replace(aCrLf, aSpace).Trim();
				}
				else //People in this room and/or cost of the room change over the course of this booking
				{
					thisRhsReservation.Comment[1] = peopleCostsChangeDuringBooking;
				}
				#endregion

				#endregion

				#region Add the Chain to the List of Reservations
				ListOfReservations.Add(thisRhsReservation);
				#endregion

				#endregion
			}

			#region See if any of the chains can be merged

			while (ListOfReservations.Count > 0)
			{
				#region Obtain the first and take it out of the list

				clsRhsReservation thisRhsReservation  = (clsRhsReservation) ListOfReservations[0];

				ListOfReservations.Remove(thisRhsReservation);

				#endregion

				if (thisSetting.MultiRoomBookingsInSameReservation)
				{
					#region Try to find other reservations we can merge with this one
					bool foundAnotherReservation = true;

					while (foundAnotherReservation)
					{
						foundAnotherReservation = false;


						for(int resCounter = 0; resCounter < ListOfReservations.Count; resCounter++)
						{
                            clsRhsReservation thisNewRhsReservation = (clsRhsReservation)ListOfReservations[resCounter];

							if (!foundAnotherReservation 
								&& thisNewRhsReservation.ResDate.day == thisRhsReservation.ResDate.day
								&& thisNewRhsReservation.ResDate.month == thisRhsReservation.ResDate.month
								&& thisNewRhsReservation.ResDate.year == thisRhsReservation.ResDate.year
								&& thisNewRhsReservation.Nights == thisRhsReservation.Nights)
							{
								#region We have a Multi Room Booking. Note the additions and remove from ListOfReservations

								foundAnotherReservation = true;
	
								#region Deal with Room Types, People and Group Rate

								for(int counter = 1; counter < 33; counter++)
									thisRhsReservation.Rooms[counter] += thisNewRhsReservation.Rooms[counter];
								
								
								if (thisRhsReservation.MaxAdults < thisNewRhsReservation.MaxAdults)
									thisRhsReservation.MaxAdults = thisNewRhsReservation.MaxAdults; 

								if (thisRhsReservation.MaxChildren< thisNewRhsReservation.MaxChildren)
									thisRhsReservation.MaxChildren = thisNewRhsReservation.MaxChildren; 

								if (thisRhsReservation.MaxInfants < thisNewRhsReservation.MaxInfants)
									thisRhsReservation.MaxInfants = thisNewRhsReservation.MaxInfants; 

								if (thisRhsReservation.People < thisNewRhsReservation.People)
									thisRhsReservation.People = thisNewRhsReservation.People; 

								thisRhsReservation.GroupRate += thisNewRhsReservation.GroupRate;

								for(int newWRTAcounter = 0; newWRTAcounter < thisNewRhsReservation.WebRoomTypeAllocations.Count; newWRTAcounter++)
								{
									clsWebRoomTypeAllocation thisWebRoomTypeAllocation = (clsWebRoomTypeAllocation) thisNewRhsReservation.WebRoomTypeAllocations[newWRTAcounter];
									int thisWebRoomTypeId = thisWebRoomTypeAllocation.WebRoomTypeId;
									bool foundThisWebRoomTypeId = false;
									//Attempt Find the Web Room Type Id in the list for current WebRoomTypeAllocation.
									for(int counter = 0; counter < thisRhsReservation.WebRoomTypeAllocations.Count && foundThisWebRoomTypeId == false; counter++)
										if ( ((clsWebRoomTypeAllocation) thisRhsReservation.WebRoomTypeAllocations[counter]).WebRoomTypeId == thisWebRoomTypeId)
										{
											((clsWebRoomTypeAllocation) thisRhsReservation.WebRoomTypeAllocations[counter]).NumberOfThisType +=
												((clsWebRoomTypeAllocation) thisNewRhsReservation.WebRoomTypeAllocations[newWRTAcounter]).NumberOfThisType;
											foundThisWebRoomTypeId = true;
										}

									if (!foundThisWebRoomTypeId)
										thisRhsReservation.WebRoomTypeAllocations.Add((clsWebRoomTypeAllocation) thisNewRhsReservation.WebRoomTypeAllocations[newWRTAcounter]);
								}

								#endregion

								ListOfReservations.Remove(thisNewRhsReservation);

								#endregion
							}
						}
					}

					#endregion
				}

				#region Overwrite the People Numbers so they are actually right

				thisRhsGuest.Adult = thisRhsReservation.MaxAdults.ToString();
				thisRhsGuest.Child = (thisRhsReservation.MaxChildren + thisRhsReservation.MaxInfants).ToString();

				#endregion

				#region Write the reservation




				#region Write the files

				string fileStem = getBookingFileStem(uid, numRhsReservations);


				if (!thisRhsGuest.writeRhsGuestFile(thisSetting.OnlineBookingsFolder + fileStem + rhsGuestFileExtension))
				{
					LogThis(DateTime.Now, "Error Writing Guest File",
						thisRhsGuest.ToString(), currentFunction, thisSetting.OnlineBookingsFolder + fileStem + rhsGuestFileExtension);
					return false;
				}

				#region Finally Deal with the Web Room Types as text

				string CommentText = "";
				for(int counter = 0; counter < thisRhsReservation.WebRoomTypeAllocations.Count; counter++)
				{
					clsWebRoomTypeAllocation thisWebRoomTypeAllocation = (clsWebRoomTypeAllocation) thisRhsReservation.WebRoomTypeAllocations[counter];

					CommentText += thisWebRoomTypeAllocation.WebRoomTypeName.ToUpper();

					if (thisWebRoomTypeAllocation.NumberOfThisType > 1)
						CommentText += "x" + thisWebRoomTypeAllocation.NumberOfThisType.ToString().Trim();

					CommentText += ";";
				}

				if (CommentText.Length > 74)
				{
					CommentText.Replace(" ", "");
					CommentText.Replace(";", "");
					CommentText.Replace("!", "");
					CommentText.Replace(".", "");
					CommentText.Replace("0", "");
					CommentText.Replace("1", "");
					CommentText.Replace("2", "");
					CommentText.Replace("3", "");
					CommentText.Replace("4", "");
					CommentText.Replace("5", "");
					CommentText.Replace("6", "");
					CommentText.Replace("7", "");
					CommentText.Replace("8", "");
					CommentText.Replace("9", "");
					CommentText.Replace("'", "");
					CommentText.Replace("@", "");
					CommentText.Replace("THE", "");
					CommentText.Replace("OF", "");
					CommentText.Replace("AND", "");
				}

				if (CommentText.Length > 74)
					CommentText = CommentText.Substring(0, 74);

				thisRhsReservation.Comment[0] = CommentText;


				#endregion

				if (!thisRhsReservation.writeRhsReservationFile(thisSetting.OnlineBookingsFolder + fileStem + rhsReservationFileExtension))
				{
					LogThis(DateTime.Now, "Error Writing Reservation File",
						ToString(), currentFunction, thisSetting.OnlineBookingsFolder + fileStem + rhsReservationFileExtension);
					return false;
				} 


				LogThis(DateTime.Now, "Reservation Added to RHS from Web Booking: " + fileStem,
					"", currentFunction, fileStem);

				#endregion

				numRhsReservations++;
				#endregion

			}

			#endregion

			#endregion
			
			return true;
		}

		#endregion

		#region getMaxWebRoomsBooked

		/// <summary>
		/// Takes a web Booking and returns the maximum number of rooms of each web room type
		/// that have been booked
		/// </summary>
		/// <param name="index"></param>
		/// <returns>WebRoomsBookedIn instance</returns>
		public clsWebRoomsBookedIn[] getMaxWebRoomsBooked(int index)
		{
			clsSetting currentSettings = new clsSetting();
			//currentSettings.GetSettings(); - TODO 2016, what settings are required here? They should be passed as params.
			
			clsWebRoomsBookedIn[] result = new clsWebRoomsBookedIn[currentSettings.theseWebRoomTypes.Count];
			

			//Make the IDs consistant with the webRoomIDs
			for (int counter = 0; counter < currentSettings.theseWebRoomTypes.Count; counter++)
			{
				result[counter].webRoomTypeId = ((clsWebRoomType) currentSettings.theseWebRoomTypes[counter]).webRoomTypeId;
			}

			//Go through the booking and get the max number for a given night
			if (simple)
			{
				//The bookings are the same for each night
				for (int counter = 0; counter < roomsBooked.Count; counter ++)
				{
					for (int webRoomCounter = 0; webRoomCounter < currentSettings.theseWebRoomTypes.Count; webRoomCounter++)
						if (result[webRoomCounter].webRoomTypeId == ((clsWebRoomSimple) roomsBooked[counter]).roomType)
							result[webRoomCounter].maxRoomsBooked++;
				}

			}
			else //RoomNight by RoomNight
			{
				DateTime lastDate = new DateTime(2001,1,1);
				clsWebRoomsBookedIn[] dayResult = new clsWebRoomsBookedIn[currentSettings.theseWebRoomTypes.Count];

				for (int roomNightcounter = 0; roomNightcounter < roomNightsBooked.Count; roomNightcounter ++)
				{
					if (((clsWebRoomNightComplex) roomNightsBooked[roomNightcounter]).date != lastDate)
					{
						lastDate = ((clsWebRoomNightComplex) roomNightsBooked[roomNightcounter]).date;
						//New day; reset counters
						for (int webRoomCounter = 0; webRoomCounter < currentSettings.theseWebRoomTypes.Count; webRoomCounter++)
						{
							dayResult[webRoomCounter].webRoomTypeId = ((clsWebRoomType) currentSettings.theseWebRoomTypes[webRoomCounter]).webRoomTypeId;
							dayResult[webRoomCounter].maxRoomsBooked = 0;
						}
					}

					for (int webRoomCounter = 0; webRoomCounter < currentSettings.theseWebRoomTypes.Count; webRoomCounter++)
					{
						if (dayResult[webRoomCounter].webRoomTypeId == ((clsWebRoomNightComplex) roomNightsBooked[roomNightcounter]).roomType)
						{
							dayResult[webRoomCounter].maxRoomsBooked++;
							if (dayResult[webRoomCounter].maxRoomsBooked > result[webRoomCounter].maxRoomsBooked)
								result[webRoomCounter].maxRoomsBooked = dayResult[webRoomCounter].maxRoomsBooked;
						}					
					}

				}

			}
			return result;
		}

		#endregion

		#region getBookingFileStem

		/// <summary>
		/// Returns the file stem for the Bookings file
		/// </summary>
		/// <param name="uniqueStem">The Booking UID; the base of the file stem</param>
		/// <param name="fileNumber">The Rhs Reservation number from this booking</param>
		/// <returns>File stem for the rhs Reservation and rhs Guest files</returns>
		public string getBookingFileStem(string uniqueStem, int fileNumber)
		{
			string fileStem = uniqueStem;
			string suffix = "";
			int uniqueSubFile = fileNumber;
			bool atLeastOneMoreChar = true; 

			while (atLeastOneMoreChar)
			{
				int remainder = uniqueSubFile % 26;
				
				string newSuffix = ascii.GetString(
					new byte[] {Convert.ToByte(remainder + 65)});

				suffix = newSuffix + suffix;

				atLeastOneMoreChar = (uniqueSubFile > 25);
				uniqueSubFile = Convert.ToInt32((uniqueSubFile - remainder) / 26);
			}
		
			int maxLengthOfFileStem = 8 - suffix.Length;

			//If our stem is too long, just take the last few characters from it.
			if (fileStem.Length > maxLengthOfFileStem)
				fileStem = fileStem.Substring(fileStem.Length - maxLengthOfFileStem); 

			return fileStem + suffix;
		}

		#endregion



	}
}
