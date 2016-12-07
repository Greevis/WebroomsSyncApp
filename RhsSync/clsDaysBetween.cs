using System;

namespace RhsSync
{
	/// <summary>
	/// Summary description for clsDaysBetween.
	/// </summary>
	public class clsDaysBetween
	{
		/// <summary>
		/// Calculates the days between two dates
		/// </summary>
		public clsDaysBetween()
		{
			//
			// TODO: Add constructor logic here
			//
		}

//		#region daysBetweenStruct
//
//		/// <summary>
//		/// Structure that is returned when a duration between two dates is required.
//		/// </summary>
//		public struct daysBetweenStruct
//		{
			/// <summary>
			/// If the two dates were both of legitimate types, 
			/// daysBetween contains the number of days between the two dates
			/// </summary>
			public long daysBetween;
			/// <summary>
			/// Supplies the details of any issues encounterd while performing the operation 
			/// </summary>
			public clsWelmanError errDescription;
//		}	
//
//		#endregion

	}
}
