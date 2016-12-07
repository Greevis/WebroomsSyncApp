using System;
using System.IO;
using System.Text;


namespace RhsSync
{
	/// <summary>
	/// Summary description for clsRhsGuest.
	/// </summary>
	public class clsRhsGuest : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsRhsGuest()
		{
		}

		#region Publicly Exposed Variables
//		public struct rhsGuest
//		{
			/// <summary>
			/// Name of Guest
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 0 - 27	
			/// </description>
			/// <description>
			/// 28
			/// </description></item></list></summary>
			public string GuestName;
			/// <summary>
			/// Whether the Guest is In House at the moment or Not (Y or N)
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 28
			/// </description>
			/// <description>
			/// 1
			/// </description></item></list></summary>
			public string GuestInHouse;
			/// <summary>
			/// Guest's favoured room, if they have one
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 29 - 30
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short GuestRoom;
			/// <summary>
			/// Number of Adults staying
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			///  31
			/// </description>
			/// <description>
			/// 1
			/// </description></item></list></summary>
			public string Adult;
			/// <summary>
			/// Number of Children staying
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 32	
			/// </description>
			/// <description>
			/// 1
			/// </description></item></list></summary>
			public string Child;
			/// <summary>
			/// First Address Field
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 33 - 57	
			/// </description>
			/// <description>
			/// 25
			/// </description></item></list></summary>
			public string Address1;
			/// <summary>
			/// Second Address Field
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 33 - 57	
			/// </description>
			/// <description>
			/// 25
			/// </description></item></list></summary>
			public string Address2;		//							Pos: 58 - 82	Length: 25
			/// <summary>
			/// Third Address Field
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 83 - 107
			/// </description>
			/// <description>
			/// 25
			/// </description></item></list></summary>
			public string Address3;
			/// <summary>
			/// Fourth Address Field
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 108 - 132
			/// </description>
			/// <description>
			/// 25
			/// </description></item></list></summary>
			public string Address4;
			/// <summary>
			/// Car registration number
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 133 - 138
			/// </description>
			/// <description>
			/// 6
			/// </description></item></list></summary>
			public string Rego;
			/// <summary>
			/// Contact Phone Number
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 139 - 148
			/// </description>
			/// <description>
			/// 10
			/// </description></item></list></summary>
			public string Phone;
			/// <summary>
			/// Class of Guest
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 149 - 150
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short GuestClass;
			/// <summary>
			/// Rate that this Guest Pays (Basic type: Currency)
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 151 - 158
			/// </description>
			/// <description>
			/// 8
			/// </description></item></list></summary>
			public double GuestRate;
			/// <summary>
			/// Number of the Nights the Guest is staying for this reservation
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 159 - 160	
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short Nights;
			/// <summary>
			/// First Night of Guest Booking (Basic type: rhsDate)
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 161 - 166
			/// </description>
			/// <description>
			/// 6
			/// </description></item></list></summary>
			public rhsDate DateIn;
			/// <summary>
			/// Comments added by the Guest
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 167 - 195
			/// </description>
			/// <description>
			/// 29
			/// </description></item></list></summary>
			public string Comment;
			/// <summary>
			/// Name of the Voucher, should the guest have one
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 196 - 215
			/// </description>
			/// <description>
			/// 20
			/// </description></item></list></summary>
			public string Voucher;
			/// <summary>
			/// If the Guest is a regular.
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 216 - 217
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short Regular;
			/// <summary>
			/// A reference to extra data regarding this Guest
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 218 - 219
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short ExtRec;
			/// <summary>
			/// Reference to a Note file added by the Proprietor (0 = No notes)
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 220 - 221
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short Notes;
			/// <summary>
			/// Member Number (for Frequent Reward programmes)
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 222 - 223	
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short MemberNo;
			/// <summary>
			/// Number of Associated Group (0 = no group)
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 224 - 225
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short Group;
			/// <summary>
			/// Transaction Record (of base type rhsAccount)
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 226 - 329
			/// </description>
			/// <description>
			/// 104
			/// </description></item></list></summary>
			public rhsAccount[] Trans;
			/// <summary>
			/// Name of Contact 
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 330 - 358
			/// </description>
			/// <description>
			/// 29
			/// </description></item></list></summary>
			public string Contact;
			/// <summary>
			/// Contact Mobile Phone Number
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 359 - 372	
			/// </description>
			/// <description>
			/// 14
			/// </description></item></list></summary>
			public string Mobile;
			/// <summary>
			/// Unallocated Member
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 373 - 381
			/// </description>
			/// <description>
			/// 9
			/// </description></item></list></summary>
			public string Spare;
			/// <summary>
			/// Number of Guest's Company, if any
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 382 - 383	
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short Company;
			/// <summary>
			/// Comments Regarding Arrival (e.g. associated travel arrangement) 
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 384 - 400	
			/// </description>
			/// <description>
			/// 17
			/// </description></item></list></summary>
			public string ArrivalDesc;
			/// <summary>
			/// Comments Regarding Departure (e.g. associated travel arrangement) 
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 401 - 417
			/// </description>
			/// <description>
			/// 17
			/// </description></item></list></summary>
			public string DepartDesc;
			/// <summary>
			/// Where this guest came from
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 418 - 419	
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short Origin;
			/// <summary>
			/// Whether this Guest is GST Exclusive
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 420 - 421	
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short GSTExcl;
			/// <summary>
			/// Reference to how the Guest found the establishment
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 422 - 423	
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short Source;
			/// <summary>
			/// Credit Card Number
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 424 - 443
			/// </description>
			/// <description>
			/// 20
			/// </description></item></list></summary>
			public string CreditCard;
			/// <summary>
			/// Credit Card Expiry Date
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 444 - 448
			/// </description>
			/// <description>
			/// 5
			/// </description></item></list></summary>
			public string ExpiryDate;
			/// <summary>
			/// Contact Fax Number
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 449 - 458	
			/// </description>
			/// <description>
			/// 10
			/// </description></item></list></summary>
			public string FaxNumber;
			/// <summary>
			/// Comments added by the user
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 459 - 487	
			/// </description>
			/// <description>
			/// 29
			/// </description></item></list></summary>
			public string UserComment;
			/// <summary>
			/// If this Guest has left or not
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 488
			/// </description>
			/// <description>
			/// 1
			/// </description></item></list></summary>
			public string Departed;
			/// <summary>
			/// Guest's time of Departure
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 489 - 493
			/// </description>
			/// <description>
			/// 5
			/// </description></item></list></summary>
			public string DepartTime;
			/// <summary>
			/// Contact Phone Number Prefix
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 494 - 497
			/// </description>
			/// <description>
			/// 4
			/// </description></item></list></summary>
			public string PhonePrefix;
			/// <summary>
			/// Contact Fax Number Prefix
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 498 - 501	
			/// </description>
			/// <description>
			/// 4
			/// </description></item></list></summary>
			public string FaxPrefix;
			/// <summary>
			/// 'RegularNum', ignored
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 502 - 509
			/// </description>
			/// <description>
			/// 8
			/// </description></item></list></summary>
			public string RegularNum;
			/// <summary>
			/// Type of Invoice
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 510
			/// </description>
			/// <description>
			/// 1
			/// </description></item></list></summary>
			public string InvoiceType;
			/// <summary>
			/// Alternative Guest Rate (Basic type: Currency)
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			///  511 - 518
			/// </description>
			/// <description>
			/// 8
			/// </description></item></list></summary>
			public double GuestRate2;
			/// <summary>
			/// Reference to Type of Foreign Exchange the guest has been quoted in
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 519 - 520
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short ForexType;
			/// <summary>
			/// Details of any Package the guest has booked useing.
			/// This is an RHS user defined type (UDT), the details of which are:
			/// Length of each structure: 36 bytes
			/// Number of Structures: 5
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			///  521 - 700
			/// </description>
			/// <description>
			/// 180
			/// </description></item></list></summary>
			public string[] Package;
			/// <summary>
			/// Type of Room the Guest prefers to stay in.
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 701 - 702
			/// </description>
			/// <description>
			/// 2
			/// </description></item></list></summary>
			public short RoomType;
			/// <summary>
			/// 'Extenstion' Details .
			/// This is an RHS user defined type (UDT), the details of which are:
			/// Length of each structure: 2 bytes
			/// Number of Structures: 4
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 703 - 710	
			/// </description>
			/// <description>
			/// 8
			/// </description></item></list></summary>
			public short[] Extension;
			/// <summary>
			/// 'LockExt'
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 711	
			/// </description>
			/// <description>
			/// 1
			/// </description></item></list></summary>
			public string LockExt;
			/// <summary>
			/// Whether the guest has Checked In or Not
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 712	
			/// </description>
			/// <description>
			/// 1
			/// </description></item></list></summary>
			public string CheckedIn;
			/// <summary>
			/// Type of Guest
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 713	
			/// </description>
			/// <description>
			/// 1
			/// </description></item></list></summary>
			public string GuestType;
			/// <summary>
			/// Reservation Number
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 714 - 719
			/// </description>
			/// <description>
			/// 6
			/// </description></item></list></summary>
			public string ResNumber;
			/// <summary>
			/// PPV
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 720
			/// </description>
			/// <description>
			/// 1
			/// </description></item></list></summary>
			public string PPV;
		/// <summary>
		/// ADContent
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 721
		/// </description>
		/// <description>
		/// 1
		/// </description></item></list></summary>
		public string ADContent;

		/// <summary>
		/// Wireless
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 722 - 731
		/// </description>
		/// <description>
		/// 10
		/// </description></item></list></summary>
		public string Wireless;
		/// <summary>
		/// BBUsage
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 732 - 739
		/// </description>
		/// <description>
		/// 8
		/// </description></item></list></summary>
		public string BBUsage;
		/// <summary>
		/// email
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 740 - 783
		/// </description>
		/// <description>
		/// 44
		/// </description></item></list></summary>
		public string email;
		/// <summary>
		/// HSIPortStatus
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 784
		/// </description>
		/// <description>
		/// 1
		/// </description></item></list></summary>
		public string HSIPortStatus;
		/// <summary>
		/// GuestHSIPort
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 785 - 787
		/// </description>
		/// <description>
		/// 3
		/// </description></item></list></summary>
		public string GuestHSIPort;
		/// <summary>
		/// VIPGuest
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 788
		/// </description>
		/// <description>
		/// 1
		/// </description></item></list></summary>
		public string VIPGuest;
		/// <summary>
		/// 'HSICodes' Details .
		/// This is an RHS user defined type (UDT), the details of which are:
		/// Length of each structure: 2 bytes
		/// Number of Structures: 4
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 789 - 800	
		/// </description>
		/// <description>
		/// 12
		/// </description></item></list></summary>
		public string[] HSICodes;
		/// <summary>
		/// IPTraxStep
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 801
		/// </description>
		/// <description>
		/// 1
		/// </description></item></list></summary>
		public string IPTraxStep;
		/// <summary>
		/// RoomAlias
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 802 - 809
		/// </description>
		/// <description>
		/// 8
		/// </description></item></list></summary>
		public string RoomAlias;
			/// <summary>
			/// Unused Data (Makes the structure up to 1024 Bytes)
			/// <list type="table"><listheader><term>
			/// Position in Structure
			/// </term><term>
			/// Length in Bytes
			/// </term></listheader>
			/// <item><description>
			/// 810 - 1009
			/// </description>
			/// <description>
			/// 200
			/// </description></item></list></summary>
			public string Expanded;
		/// <summary>
		/// WebRmNumber
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 1010 - 1023
		/// </description>
		/// <description>
		/// 14
		/// </description></item></list></summary>
		public string WebRmNumber;

//		}

		#endregion

		#region writeRhsGuestFile

		/// <summary>
		/// Write data to the the guest file
		/// </summary>
		/// <param name="guestFileName">File to write data to</param>
		public bool writeRhsGuestFile(string guestFileName)
		{
			
			int lengthOfGuestRecord = 1024;
			string currentFunction = "readGuestFile";

			
			Decoder d = Encoding.UTF8.GetDecoder();

			FileStream dataFileStream;



			
			BinaryWriter binWriter;


			//See if we can access the file
			try
			{
				dataFileStream = new FileStream(guestFileName, FileMode.Create, FileAccess.Write);
				binWriter = new BinaryWriter(dataFileStream, ascii);
			}
			catch (System.UnauthorizedAccessException e)
			{
				LogThis(DateTime.Now, "Error Opening File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, guestFileName);
				return false;
			}
			catch (System.Exception e)
			{
				LogThis(DateTime.Now, "Error Opening File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, guestFileName);
				return false;
			}


			//we can; lets get the data out of it

			byte[] tempBytes = new byte[lengthOfGuestRecord];


			//Attempt to write the file
			try
			{
				binWriter.Write(toByteArrayFromString(GuestName, 28));
				binWriter.Write(toByteArrayFromString(GuestInHouse, 1)); 
				binWriter.Write(GuestRoom); 
				binWriter.Write(toByteArrayFromString(Adult, 1));
				binWriter.Write(toByteArrayFromString(Child, 1));
				binWriter.Write(toByteArrayFromString(Address1, 25));
				binWriter.Write(toByteArrayFromString(Address2, 25));
				binWriter.Write(toByteArrayFromString(Address3, 25));
				binWriter.Write(toByteArrayFromString(Address4, 25));
				binWriter.Write(toByteArrayFromString(Rego, 6));

				binWriter.Write(toByteArrayFromString(Phone, 10));
				binWriter.Write(GuestClass);

				binWriter.Write(Convert.ToInt64(GuestRate * 10000));

				binWriter.Write(Nights);
				binWriter.Write(DateIn.day);
				binWriter.Write(DateIn.month);
				binWriter.Write(DateIn.year);
				binWriter.Write(toByteArrayFromString(Comment, 29));
				binWriter.Write(toByteArrayFromString(Voucher, 20));
				binWriter.Write(Regular);
				binWriter.Write(ExtRec);
				binWriter.Write(Notes);
				binWriter.Write(MemberNo);
				binWriter.Write(Group);
				for (int counter = 0; counter < 2; counter++)
				{

					binWriter.Write(Trans[counter].First);
					binWriter.Write(Trans[counter].Last);
					binWriter.Write(Trans[counter].Allocated);
					binWriter.Write(Trans[counter].Guest);
					binWriter.Write(Trans[counter].Size);
					binWriter.Write(Trans[counter].Pay);
					binWriter.Write(Trans[counter].Debtor);
					binWriter.Write(Trans[counter].Invoice);
					binWriter.Write(Trans[counter].Printed);
					binWriter.Write(Trans[counter].Balance);
					binWriter.Write(Trans[counter].Tax );
					binWriter.Write(toByteArrayFromString(Trans[counter].Name, 10));
					binWriter.Write(toByteArrayFromString(Trans[counter].Unused, 16));
				}

				binWriter.Write(toByteArrayFromString(Contact, 29));
				binWriter.Write(toByteArrayFromString(Mobile, 14));
				binWriter.Write(toByteArrayFromString(Spare, 9));
				binWriter.Write(Company);
				binWriter.Write(toByteArrayFromString(ArrivalDesc, 17));
				binWriter.Write(toByteArrayFromString(DepartDesc, 17));
				binWriter.Write(Origin);
				binWriter.Write(GSTExcl);
				binWriter.Write(Source);
				binWriter.Write(toByteArrayFromString(CreditCard, 20));
				binWriter.Write(toByteArrayFromString(ExpiryDate, 5));
				binWriter.Write(toByteArrayFromString(FaxNumber, 10));
				binWriter.Write(toByteArrayFromString(UserComment, 29));
				binWriter.Write(toByteArrayFromString(Departed, 1));
				binWriter.Write(toByteArrayFromString(DepartTime, 5));
				binWriter.Write(toByteArrayFromString(PhonePrefix, 4));
				binWriter.Write(toByteArrayFromString(FaxPrefix, 4));
				binWriter.Write(toByteArrayFromString(RegularNum, 8));
				binWriter.Write(toByteArrayFromString(InvoiceType, 1));
				binWriter.Write(Convert.ToInt64(GuestRate2 * 10000));
				binWriter.Write(ForexType);

				for (int counter = 0; counter < 5; counter++)
					binWriter.Write(toByteArrayFromString(Package[counter], 36));
				
				binWriter.Write(RoomType);

				for (int counter = 0; counter < 4; counter++)
					binWriter.Write(Extension[counter]);

				binWriter.Write(toByteArrayFromString(LockExt, 1));
				binWriter.Write(toByteArrayFromString(CheckedIn, 1));
				binWriter.Write(toByteArrayFromString(GuestType, 1));
				binWriter.Write(toByteArrayFromString(ResNumber, 6));
				binWriter.Write(toByteArrayFromString(PPV, 1));
				binWriter.Write(toByteArrayFromString(ADContent, 1));
				binWriter.Write(toByteArrayFromString(Wireless, 10));
				binWriter.Write(toByteArrayFromString(BBUsage, 8));
				binWriter.Write(toByteArrayFromString(email, 44));
				binWriter.Write(toByteArrayFromString(HSIPortStatus, 1));
				binWriter.Write(toByteArrayFromString(GuestHSIPort, 3));
				binWriter.Write(toByteArrayFromString(VIPGuest, 1));

				for (int counter = 0; counter < 2; counter++)
					binWriter.Write(toByteArrayFromString(HSICodes[counter], 6));

				binWriter.Write(toByteArrayFromString(IPTraxStep, 1));
				binWriter.Write(toByteArrayFromString(RoomAlias, 8));
				binWriter.Write(toByteArrayFromString(Expanded, 200));
				binWriter.Write(toByteArrayFromString(WebRmNumber, 14));

			}
			catch (System.UnauthorizedAccessException e)
			{
				LogThis(DateTime.Now, "Error Reading File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, guestFileName);
				return false;
			}
			catch (System.Exception e)
			{
				LogThis(DateTime.Now, "Error Reading File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, guestFileName);
				return false;
			}

			binWriter.Close();
			dataFileStream.Close();
			return true;
		}

		#endregion

	}
}
