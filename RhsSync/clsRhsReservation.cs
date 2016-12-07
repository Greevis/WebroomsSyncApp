using System;
using System.IO;
using System.Text;
using System.Collections;

namespace RhsSync
{
	/// <summary>
	/// Summary description for clsRhsReservation.
	/// </summary>
	public class clsRhsReservation : clsBase
	{
		/// <summary>
		/// Initialiser
		/// </summary>
		public clsRhsReservation()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region Publicly Exposed Variables

		//		public struct rhsReservation
		//		{
		/// <summary>
		/// Date of Reservation (rhsDate type)
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 0 - 5
		/// </description>
		/// <description>
		/// 6
		/// </description></item></list></summary>
		public rhsDate ResDate;
		/// <summary>
		/// Reference to Guest 
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 6 - 11
		/// </description>
		/// <description>
		/// 6
		/// </description></item></list></summary>
		public string Number;
		/// <summary>
		/// Date Reservation was made
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 12 - 17
		/// </description>
		/// <description>
		/// 6
		/// </description></item></list></summary>
		public rhsDate MadDate;
		/// <summary>
		/// Date reservation confirmed
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 18 - 23
		/// </description>
		/// <description>
		/// 6
		/// </description></item></list></summary>
		public rhsDate ConDate;
		/// <summary>
		/// Number of nights the guest is staying
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 24 - 25
		/// </description>
		/// <description>
		/// 2
		/// </description></item></list></summary>
		public short Nights;
		/// <summary>
		/// Whether this reservation is waitlisted (Y or N)
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 26
		/// </description>
		/// <description>
		/// 1
		/// </description></item></list></summary>
		public string WaitList;
		/// <summary>
		/// Array of 2 Comments. Each Comment is 74 Bytes long
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 27 - 174
		/// </description>
		/// <description>
		/// 148
		/// </description></item></list></summary>
		public string[] Comment;
		/// <summary>
		/// Number of guests for this reservation
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 175 - 176	
		/// </description>
		/// <description>
		/// 2
		/// </description></item></list></summary>
		public short People;
		/// <summary>
		/// Reference to data in RESXTRA.DAT (0 = no extra data)
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 179 - 180
		/// </description>
		/// <description>
		/// 2
		/// </description></item></list></summary>
		public short XtraDat;
		/// <summary>
		/// Structure that indicates how many rooms of each type are reserved
		/// It is an array of 32 shorts; 32 representing the maximum number of
		/// Room Types
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 179 - 242	
		/// </description>
		/// <description>
		/// 64
		/// </description></item></list></summary>
		public short[] Rooms;
		/// <summary>
		/// Structure that indicates rates for each of the room types that the guest has booked.
		/// It is an array of 32 doubles.
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 243 - 498
		/// </description>
		/// <description>
		/// 256
		/// </description></item></list></summary>
		public double[] Rates;
		/// <summary>
		/// Type of Foreign Exchange that is used for Rates
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 499 - 500	
		/// </description>
		/// <description>
		/// 2
		/// </description></item></list></summary>
		public short ForexType;
		/// <summary>
		/// 'Dummy'; unused
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 501 - 511
		/// </description>
		/// <description>
		/// 11
		/// </description></item></list></summary>
		public string Dummy;
		/// <summary>
		/// 'WebRmSource'; The source of a booking in 
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 512 - 551
		/// </description>
		/// <description>
		///  40
		/// </description></item></list></summary>
		public string WebRmSource;
		/// <summary>
		/// 'Expanded'; unused
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 552 - 756
		/// </description>
		/// <description>
		///  205
		/// </description></item></list></summary>
		public string Expanded;
		/// <summary>
		/// If the Booking has been confirmed or not 
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 757
		/// </description>
		/// <description>
		/// 1
		/// </description></item></list></summary>
		public string Confirmed;
		/// <summary>
		/// Rate for the whole reservation, if listed
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 758 - 765
		/// </description>
		/// <description>
		/// 8
		/// </description></item></list></summary>
		public decimal GroupRate;
		//Total Rate per night
		/// <summary>
		/// 'R-TYPE'; ignored
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 766 - 767
		/// </description>
		/// <description>
		/// 2
		/// </description></item></list></summary>
		public short RTYPE;
		/// <summary>
		/// A second array of rates
		/// <list type="table"><listheader><term>
		/// Position in Structure
		/// </term><term>
		/// Length in Bytes
		/// </term></listheader>
		/// <item><description>
		/// 768 - 1023		
		/// </description>
		/// <description>
		/// 256
		/// </description></item></list></summary>
		public double[] Rates2;

		/// <summary>
		/// Max number of Adults that are a part of this reservation
		/// </summary>
		public int MaxAdults;

		/// <summary>
		/// Max number of Children that are a part of this reservation
		/// </summary>
		public int MaxChildren;

		/// <summary>
		/// Max number of Infants that are a part of this reservation
		/// </summary>
		public int MaxInfants;

		/// <summary>
		/// Max number of People that are a part of this reservation
		/// </summary>
		public int MaxPeople;
		
		//		}

		/// <summary>
		/// WebRoomTypeAllocations (from clsWebRoomTypeAllocations)
		/// </summary>
		public ArrayList WebRoomTypeAllocations = new ArrayList();

		#endregion

		#region readReservationFile

		/// <summary>
		/// Read the Reservation file and output information from it
		/// </summary>
		/// <param name="ReservationFileName">Reservation File Name</param>
		/// <returns>A populated Rhs Reservation Structure (if successful)</returns>
		public void readReservationFile(string ReservationFileName)
		{
			
			int lengthOfreservationRecord = 1024;
			string currentFunction = "readReservationFile";

			Decoder d = Encoding.UTF8.GetDecoder();

			FileStream dataFileStream;
			StreamReader sr;
			BinaryReader binReader;
			string totalRecord;


			if (!File.Exists(ReservationFileName))
				return;

			//See if we can access the file
			try
			{
				dataFileStream = new FileStream(ReservationFileName, FileMode.Open, FileAccess.Read);
				sr = new StreamReader(dataFileStream);
				binReader = new BinaryReader(dataFileStream);
			}
			catch (System.UnauthorizedAccessException e)
			{
				LogThis(DateTime.Now, "Error Opening File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return;
			}
			catch (System.Exception e)
			{
				LogThis(DateTime.Now, "Error Opening File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return;
			}

			//we can; lets get the data out of it

			byte[] tempBytes = new byte[lengthOfreservationRecord];

			//Try to go to the start
			try
			{
				dataFileStream.Seek(0, SeekOrigin.Begin);
			}
			catch (System.UnauthorizedAccessException e)
			{
				LogThis(DateTime.Now, "Error Opening File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return;
			}
			catch (System.Exception e)
			{
				LogThis(DateTime.Now, "Error Opening File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return;
			}
					
			//Attempt to read from the file
			try
			{
				if (dataFileStream.Read(tempBytes, 0, lengthOfreservationRecord) != lengthOfreservationRecord)
				{
					LogThis(DateTime.Now, "Error Reading File",
						"File incorrect Length", currentFunction, ReservationFileName);
				}
					
			}
			catch (System.UnauthorizedAccessException e)
			{
				LogThis(DateTime.Now, "Error Reading File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return;
			}
			catch (System.Exception e)
			{
				LogThis(DateTime.Now, "Error Reading File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return;
			}
								
			byte[] unicodeBytes = Encoding.Convert( ascii, unicode, tempBytes);
			totalRecord = unicode.GetString(unicodeBytes);

			ResDate.day = bufferToShort(tempBytes, 0);
			ResDate.month = bufferToShort(tempBytes, 2);
			ResDate.year = bufferToShort(tempBytes, 4);

			Number = totalRecord.Substring(6,6);
			
			MadDate.day = bufferToShort(tempBytes, 12);
			MadDate.month = bufferToShort(tempBytes, 14);
			MadDate.year = bufferToShort(tempBytes, 16);

			ConDate.day = bufferToShort(tempBytes, 18);
			ConDate.month = bufferToShort(tempBytes, 20);
			ConDate.year = bufferToShort(tempBytes, 22);

			Nights = bufferToShort(tempBytes, 24);
			WaitList = totalRecord.Substring(26,1);
			
			Comment = new string[2];
			for (int counter = 0; counter < 2; counter++)
				Comment[counter] = totalRecord.Substring(27 + counter * 74,74);

			People = bufferToShort(tempBytes, 175);
			XtraDat = bufferToShort(tempBytes, 177);

			Rooms = new short[33];

			for (int counter = 1; counter < 33; counter++)
				Rooms[counter] = bufferToShort(tempBytes, 179 + 2 * (counter - 1));

			Rates = new double[33];

			for (int counter = 1; counter < 33; counter++)
				Rates[counter] = bufferToCurrency(tempBytes, 243 + 8 * (counter - 1));

			ForexType = bufferToShort(tempBytes, 499);

			Dummy = totalRecord.Substring(501,11);
			WebRmSource = totalRecord.Substring(512,40);
			Expanded = totalRecord.Substring(552,205);
			Confirmed = totalRecord.Substring(757,1);

			GroupRate = Convert.ToDecimal(bufferToCurrency(tempBytes, 758));
			RTYPE = bufferToShort(tempBytes, 766);

			Rates2 = new double[33];

			for (int counter = 1; counter < 33; counter++)
				Rates2[counter] = bufferToCurrency(tempBytes, 768 + 8 * (counter - 1));


			dataFileStream.Close();
			sr.Close();
			binReader.Close();
		}

		#endregion

		#region writeRhsReservationFile

		/// <summary>
		/// Write data to the the Reservation file
		/// </summary>
		/// <param name="ReservationFileName">File to write data to</param>
		public bool writeRhsReservationFile(string ReservationFileName)
		{
			
			int lengthOfReservationRecord = 1024;
			string currentFunction = "writeReservationFile";

			//			
			
			Decoder d = Encoding.UTF8.GetDecoder();

			FileStream dataFileStream;

			BinaryWriter binWriter;


			//See if we can access the file
			try
			{
				dataFileStream = new FileStream(ReservationFileName, FileMode.Create, FileAccess.Write);
				binWriter = new BinaryWriter(dataFileStream, ascii);
			}
			catch (System.UnauthorizedAccessException e)
			{
				LogThis(DateTime.Now, "Error Opening File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return false;
			}
			catch (System.Exception e)
			{
				LogThis(DateTime.Now, "Error Opening File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return false;
			}


			//we can; lets get the data out of it

			byte[] tempBytes = new byte[lengthOfReservationRecord];

			//Try to go to the start
			try
			{
				//				binWriter.Seek(0, SeekOrigin.Begin);
				
				dataFileStream.Seek(0, SeekOrigin.Begin);
			}
			catch (System.UnauthorizedAccessException e)
			{
				LogThis(DateTime.Now, "Error Opening File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return false;
			}
			catch (System.Exception e)
			{
				LogThis(DateTime.Now, "Error Opening File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return false;
			}

			//Attempt to write the file
			try
			{

				binWriter.Write(ResDate.day);
				binWriter.Write(ResDate.month);
				binWriter.Write(ResDate.year);
				
				binWriter.Write(toByteArrayFromString(Number, 6));

				binWriter.Write(MadDate.day);
				binWriter.Write(MadDate.month);
				binWriter.Write(MadDate.year);
				
				binWriter.Write(ConDate.day);
				binWriter.Write(ConDate.month);
				binWriter.Write(ConDate.year);
				
				binWriter.Write(Nights); 
				binWriter.Write(toByteArrayFromString(WaitList, 1)); 
								
				binWriter.Write(toByteArrayFromString(Comment[0], 74));
				binWriter.Write(toByteArrayFromString(Comment[1], 74));
				binWriter.Write(People); 
				binWriter.Write(XtraDat); 

				for (int counter = 1; counter < 33; counter++)
					binWriter.Write(Rooms[counter]); 
				
				for (int counter = 1; counter < 33; counter++)
					binWriter.Write(Convert.ToInt64(Rates[counter] * 10000));

				binWriter.Write(ForexType); 
				binWriter.Write(toByteArrayFromString(Dummy, 11));
				binWriter.Write(toByteArrayFromString(WebRmSource, 40));
				binWriter.Write(toByteArrayFromString(Expanded, 205));
				binWriter.Write(toByteArrayFromString(Confirmed, 1));
				binWriter.Write(Convert.ToInt64(GroupRate * 10000));
				binWriter.Write(RTYPE); 

				for (int counter = 1; counter < 33; counter++)
					binWriter.Write(Convert.ToInt64(Rates2[counter] * 10000));
				
			}
			catch (System.UnauthorizedAccessException e)
			{
				LogThis(DateTime.Now, "Error Reading File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return false;
			}
			catch (System.Exception e)
			{
				LogThis(DateTime.Now, "Error Reading File",
					e.ToString() + e.Message + e.StackTrace, currentFunction, ReservationFileName);
				return false;
			}

			binWriter.Close();
			dataFileStream.Close();
			return true;
		}
		#endregion

	}
}
