using System;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Timers;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Principal;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Reflection;
using Resources;


namespace WebroomsSyncApp
{
    /// <summary>
    /// Summary description for clsBase.
    /// </summary>
    public class clsBase
    {
        /// <summary>
        /// Base class for every other class
        /// </summary>
        public clsBase()
        {

        }

        #region rhsRawFileType

        #region Type Name
        /// <summary>
        /// rhsRawFileTypeName that has been retrieved from the Database
        /// </summary>
        /// <param name="Status">status to rhsRawFileType, start in 1 to 6</param>
        /// <returns>Decrypted card number (note this may still be obfuscated)</returns>
        public string getrhsRawFileTypeName(int Status)
        {
            string Name = "";
            switch ((rhsRawFileType)Status)
            {
                case rhsRawFileType.rooms0Dat:
                    Name = "rooms0Dat";
                    break;
                case rhsRawFileType.rmtype1Dat:
                    Name = "rmtype1Dat";
                    break;
                case rhsRawFileType.statafftIdm:
                    Name = "statafftIdm";
                    break;
                case rhsRawFileType.statafftIds:
                    Name = "statafftIds";
                    break;
                case rhsRawFileType.statafftDat:
                    Name = "statafftDat";
                    break;
                default:
                case rhsRawFileType.rooms1Dat:
                    Name = "rooms1Dat";
                    break;
            }
            return Name;
        }

        #endregion

        /// <summary>
        /// This enumeration is used for rhsRawFileType
        /// </summary>
        public enum rhsRawFileType : int
        {
            /// <summary>
            /// rooms0Dat
            /// </summary>
            rooms0Dat = 0,
            /// <summary>
            /// rmtype1Dat 
            /// </summary>
            rmtype1Dat = 1,
            /// <summary>
            /// rooms1Dat 
            /// </summary>
            rooms1Dat = 2,
            /// <summary>
            /// statafftIdm 
            /// </summary>
            statafftIdm = 3,
            /// <summary>
            /// statafftIds 
            /// </summary>
            statafftIds = 4,
            /// <summary>
            /// statafftDat 
            /// </summary>
            statafftDat = 5
        }


        /// <summary>
        /// Externally available version of the enumeration
        /// <see cref="rhsRawFileType">rhsRawFileType</see> 
        /// </summary>
        /// <returns>value of 
        /// <see cref="rhsRawFileType.statafftIdm">statafftIdm</see> 
        /// </returns>
        public int rhsRawFileType_statafftIdm()
        {
            return Convert.ToInt32(rhsRawFileType.statafftIdm);
        }

        /// <summary>
        /// Externally available version of the enumeration
        /// <see cref="rhsRawFileType">rhsRawFileType</see> 
        /// </summary>
        /// <returns>value of 
        /// <see cref="rhsRawFileType.rooms0Dat">rooms0Dat</see> 
        /// </returns>
        public int rhsRawFileType_rooms0Dat()
        {
            return Convert.ToInt32(rhsRawFileType.rooms0Dat);
        }

        /// <summary>
        /// Externally available version of the enumeration
        /// <see cref="rhsRawFileType">rhsRawFileType</see> 
        /// </summary>
        /// <returns>value of 
        /// <see cref="rhsRawFileType.rmtype1Dat">rmtype1Dat</see> 
        /// </returns>
        public int rhsRawFileType_rmtype1Dat()
        {
            return Convert.ToInt32(rhsRawFileType.rmtype1Dat);
        }

        /// <summary>
        /// Externally available version of the enumeration
        /// <see cref="rhsRawFileType">rhsRawFileType</see> 
        /// </summary>
        /// <returns>value of 
        /// <see cref="rhsRawFileType.rooms1Dat">rooms1Dat</see> 
        /// </returns>
        public int rhsRawFileType_rooms1Dat()
        {
            return Convert.ToInt32(rhsRawFileType.rooms1Dat);
        }

        /// <summary>
        /// Externally available version of the enumeration
        /// <see cref="rhsRawFileType">rhsRawFileType</see> 
        /// </summary>
        /// <returns>value of 
        /// <see cref="rhsRawFileType.statafftIds">statafftIds</see> 
        /// </returns>
        public int rhsRawFileType_statafftIds()
        {
            return Convert.ToInt32(rhsRawFileType.statafftIds);
        }

        /// <summary>
        /// Externally available version of the enumeration
        /// <see cref="rhsRawFileType">rhsRawFileType</see> 
        /// </summary>
        /// <returns>value of 
        /// <see cref="rhsRawFileType.statafftDat">statafftDat</see> 
        /// </returns>
        public int rhsRawFileType_statafftDat()
        {
            return Convert.ToInt32(rhsRawFileType.statafftDat);
        }

        #endregion

        #region rhsFileTypes
        /// <summary>
        /// Structure containing information about the file that constitue the 
        /// RHS Databse 
        /// </summary>
        public struct rhsFileTypes
        {
            /// <summary>
            /// The file name
            /// </summary>
            public string fileName;
            /// <summary>
            /// Length of each record in the file
            /// </summary>
            public int recordLength;
            /// <summary>
            /// Whether data has been successfully retrieved from the file or not
            /// </summary>
            public bool currentDataRetrieved;
            /// <summary>
            /// Number of Records in the file
            /// </summary>
            public long numRecords;
            /// <summary>
            /// Largest index number in the file
            /// </summary>
            public long maxima;
            /// <summary>
            /// Smallest index number in the file
            /// </summary>
            public long minima;
            /// <summary>
            /// Last time the file was modified
            /// </summary>
            public DateTime lastModifiedTime;
        }

        #endregion

        #region RHS File Definitions and initiation

        /// <summary>
        /// Populates the RhsFiles Structure
        /// </summary>
        public void PopulateRhsFiles()
        {
            rhsFiles = new rhsFileTypes[numRhsFiles];

            //Initialise File type array

            rhsFiles[rhsRawFileType_rooms0Dat()].fileName = "ROOMS0.DAT";
            rhsFiles[rhsRawFileType_rooms0Dat()].recordLength = 2;
            rhsFiles[rhsRawFileType_rooms0Dat()].currentDataRetrieved = false;
            rhsFiles[rhsRawFileType_rooms0Dat()].numRecords = 0;
            rhsFiles[rhsRawFileType_rooms0Dat()].maxima = 0;
            rhsFiles[rhsRawFileType_rooms0Dat()].minima = 0;

            rhsFiles[rhsRawFileType_rmtype1Dat()].fileName = "RMTYPE1.DAT";
            rhsFiles[rhsRawFileType_rmtype1Dat()].recordLength = 60;
            rhsFiles[rhsRawFileType_rmtype1Dat()].currentDataRetrieved = false;
            rhsFiles[rhsRawFileType_rmtype1Dat()].numRecords = 0;
            rhsFiles[rhsRawFileType_rmtype1Dat()].maxima = 0;
            rhsFiles[rhsRawFileType_rmtype1Dat()].minima = 0;

            rhsFiles[rhsRawFileType_rooms1Dat()].fileName = "ROOMS1.DAT";
            rhsFiles[rhsRawFileType_rooms1Dat()].recordLength = 136;
            rhsFiles[rhsRawFileType_rooms1Dat()].currentDataRetrieved = false;
            rhsFiles[rhsRawFileType_rooms1Dat()].numRecords = 0;
            rhsFiles[rhsRawFileType_rooms1Dat()].maxima = 0;
            rhsFiles[rhsRawFileType_rooms1Dat()].minima = 0;

            rhsFiles[rhsRawFileType_statafftIdm()].fileName = "STATAFFT.IDM";
            rhsFiles[rhsRawFileType_statafftIdm()].recordLength = 16;
            rhsFiles[rhsRawFileType_statafftIdm()].currentDataRetrieved = false;
            rhsFiles[rhsRawFileType_statafftIdm()].numRecords = 0;
            rhsFiles[rhsRawFileType_statafftIdm()].maxima = 0;
            rhsFiles[rhsRawFileType_statafftIdm()].minima = 0;

            rhsFiles[rhsRawFileType_statafftIds()].fileName = "STATAFFT.IDS";
            rhsFiles[rhsRawFileType_statafftIds()].recordLength = 32;
            rhsFiles[rhsRawFileType_statafftIds()].currentDataRetrieved = false;
            rhsFiles[rhsRawFileType_statafftIds()].numRecords = 0;
            rhsFiles[rhsRawFileType_statafftIds()].maxima = 0;
            rhsFiles[rhsRawFileType_statafftIds()].minima = 0;

            rhsFiles[rhsRawFileType_statafftDat()].fileName = "STATAFFT.DAT";
            rhsFiles[rhsRawFileType_statafftDat()].recordLength = 64;
            rhsFiles[rhsRawFileType_statafftDat()].currentDataRetrieved = false;
            rhsFiles[rhsRawFileType_statafftDat()].numRecords = 0;
            rhsFiles[rhsRawFileType_statafftDat()].maxima = 0;
            rhsFiles[rhsRawFileType_statafftDat()].minima = 0;


        }
        #endregion

        /// <summary>
        /// Rhs File Type related variables
        /// </summary>
        public rhsFileTypes[] rhsFiles;

        #region CheckingAvailability / rhsProgress
        /// <summary>
        /// Update the Progress bar with the progress in reading the RHS datbase
        /// </summary>
        /// <param name="task">Task to display</param>
        /// <param name="progress">Progress (0-100)</param>
        /// <param name="time">Estimated time remaining (unused)</param>
        public void CheckingAvailability(string task, int progress, string time)
        {
            if (thisWebRoomsForm != null)
                thisWebRoomsForm.rhsProgress(task, progress, time);
        }

        #endregion

        #region UpdatingWebsite / webProgress
        /// <summary>
        /// Update the Progress bar with the progress in reading the RHS datbase
        /// </summary>
        /// <param name="task">Task to display</param>
        /// <param name="progress">Progress (0-100)</param>
        /// <param name="time">Estimated time remaining (unused)</param>
        public void UpdatingWebsite(string task, int progress, string time)
        {
            if (thisWebRoomsForm != null)
                thisWebRoomsForm.webProgress(task, progress, time);
        }

        #endregion

        #region SettingsChanges / wwrSettingsChange
        /// <summary>
        /// Update the Settings on the form
        /// </summary>
        public void SettingsChanges()
        {
            if (thisWebRoomsForm != null)
                thisWebRoomsForm.wwrSettingsChange();
        }

        #endregion

        #region rhsDate

        /// <summary>
        /// Emulates the rhsDate Structure. Total Struct Size: 6 bytes
        /// </summary>
        public struct rhsDate
        {
            /// <summary>
            /// Day of Year
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 0 - 1
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short day;
            /// <summary>
            /// Month of Year
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 2 - 3
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short month;
            /// <summary>
            /// Year
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 4 - 5
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short year;
        }

        #endregion

        #region RHS Account

        /// <summary>
        /// Emulates the rhsAccount Structure. Total Struct Size: 52 bytes
        /// <note type="implementnotes">Data in this type should always remain blank;
        /// there is no 'account' created until the guest leaves
        /// details</note>		
        /// </summary>
        public struct rhsAccount
        {
            /// <summary>
            /// Record No. of First Transaction
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 0 - 1
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short First;
            /// <summary>
            /// Record No. of Last Transaction
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 2 - 3
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short Last;
            /// <summary>
            /// Allocation No.
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 4 - 5
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short Allocated;
            /// <summary>
            /// Guest Allocated to
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 6 - 7
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short Guest;
            /// <summary>
            /// Number of Records in Transaction File	
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 8 - 9
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short Size;
            /// <summary>
            /// Record Number of Payment Type (Credit Trans)
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 10 - 11
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short Pay;
            /// <summary>
            /// Record Number of Debtor to Transfer to.	
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 12 - 13
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short Debtor;
            /// <summary>
            /// Invoice Number (when printed) 	
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 14 - 15
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short Invoice;
            /// <summary>
            /// -1 Printed, 0 Not Printed or changed since print		
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 16 - 17
            /// </description>
            /// <description>
            /// 2
            /// </description></item></list></summary>
            public short Printed;
            /// <summary>
            /// Account Balance	
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 18 - 21
            /// </description>
            /// <description>
            /// 4
            /// </description></item></list></summary>
            public float Balance;
            /// <summary>
            /// GST  - for GST Exclusive situations	
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 22 - 25
            /// </description>
            /// <description>
            /// 4
            /// </description></item></list></summary>
            public float Tax;
            /// <summary>
            /// Account Name	
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 26 - 35
            /// </description>
            /// <description>
            /// 10
            /// </description></item></list></summary>
            public string Name;
            /// <summary>
            /// Unused at this stage in the Account Structure	
            /// <list type="table"><listheader><term>
            /// Position in Structure
            /// </term><term>
            /// Length in Bytes
            /// </term></listheader>
            /// <item><description>
            /// 36 - 51
            /// </description>
            /// <description>
            /// 16
            /// </description></item></list></summary>
            public string Unused;
        }

        #endregion

        #region enums 

        #region confirmedBookingBehaviourType
        /// <summary>
        /// confirmed Booking Behaviour Type
        /// </summary>
        public enum confirmedBookingBehaviourType : int
        {
            /// <summary>
            /// Mark Confirmed Bookings as Confirmed in Roonsoft and vice verse (current and default) 
            /// </summary>
            markConfirmedAsConfirmedInRoonsoftAndViceVersa = 0,
            /// <summary>
            /// The opposite way around
            /// </summary>
            markUnconfirmedAsConfirmedInRoonsoftAndViceVersa = 1,
            /// <summary>
            /// Mark all bookings as confirmed in Roonsoft
            /// </summary>
            markAllConfirmed = 2,
            /// <summary>
            /// Mark all bookings as unconfirmed in Roonsoft
            /// </summary>
            markAllUnconfirmed = 3
        }

        #endregion

        #region rhsBookingType
        /// <summary>
        /// RHS Booking Type
        /// </summary>
        public enum rhsBookingType : int
        {
            /// <summary>
            /// This is not a Booking
            /// </summary>
            noBooking = 0,
            /// <summary>
            /// This Booking has been made, but not allocated to a Room
            /// </summary>
            bookedNotAllocated = 1,
            /// <summary>
            /// This Booking has been made and allocated to a Room
            /// </summary>
            bookedAndAllocated = 2
        }

        #endregion

        #region webBookingParameterType


        /// <summary>
        /// Enumerated Type that enumerates the parameters as they arrive in the 
        /// Booking Message. 
        /// <note type="implementnotes">This is for the 'header'
        /// part of the Booking Message only, and doesn't enumerate the Room Night
        /// details</note>
        /// </summary>
        public enum webBookingParameterType : int
        {
            /// <summary>
            /// Booking Id (not visible to guest or motelier)
            /// </summary>
            bookingUID = 0,
            /// <summary>
            /// Confirmation Code (appears on guest/motelier emails)
            /// </summary>
            bookingConfirmationCode = 1,
            /// <summary>
            /// Name of the person who made the booking
            /// </summary>
            bookingContactName = 2,
            /// <summary>
            /// Name of the person for whoom the booking was made
            /// </summary>
            bookingGuestName = 3,
            /// <summary>
            /// Contact Fax Number
            /// </summary>
            bookingContactFax = 4,
            /// <summary>
            /// Contact Phone Number
            /// </summary>
            bookingContactPhone = 5,
            /// <summary>
            /// Alternative Phone Number
            /// </summary>
            bookingAlternativePhone = 6,
            /// <summary>
            /// Contact Mail Address
            /// </summary>
            bookingContactEmail = 7,
            /// <summary>
            /// Contact Mail Address
            /// </summary>
            bookingContactAddress = 8,
            /// <summary>
            /// Comments Added by the Contact
            /// </summary>
            bookingComments = 9,
            /// <summary>
            /// Payment Method Selected
            /// </summary>
            bookingPayment = 10,
            /// <summary>
            /// Customer New or Returning Web Customer
            /// </summary>
            bookingReturning = 11,
            /// <summary>
            /// Customer Stayed at this place before?
            /// </summary>
            bookingStayedBefore = 12,
            /// <summary>
            /// How the customer found the website
            /// </summary>
            bookingHowWebsiteFound = 13,
            /// <summary>
            /// Name on Credit Card used to secure the booking
            /// </summary>
            bookingCCName = 14,
            /// <summary>
            /// Type of Credit Card used to secure the booking
            /// </summary>
            bookingCCType = 15,
            /// <summary>
            /// Expiry Date of the Credit Card used to secure the booking
            /// </summary>
            bookingCCExpiry = 16,
            /// <summary>
            /// Number on Credit Card used to secure the booking
            /// </summary>
            bookingCCNumber = 17,
            /// <summary>
            /// Date and Time the reservation was made
            /// </summary>
            bookingDateTimeOfRes = 18,
            /// <summary>
            /// Whether the booking was tentative or not
            /// </summary>
            bookingTenatative = 19,
            /// <summary>
            /// First night of the booking
            /// </summary>
            bookingFirstNightDate = 20,
            /// <summary>
            /// Last night of the booking
            /// </summary>
            bookingLastNightDate = 21,
            /// <summary>
            /// Whether the booking is simple (true) or complex (false)
            /// </summary>
            bookingSimple = 22,
        }
        #endregion

        #region settingsHeaderParameterType


        /// <summary>
        /// Enumerated Type that enumerates the parameters as they arrive in the 
        /// Settings File. 
        /// </summary>
        public enum settingsHeaderParameterType : int
        {
            /// <summary>
            /// AddOnlineBookingsIntoRhs
            /// </summary>
            addOnlineBookingsIntoRhs = 0,
            /// <summary>
            /// AddOverbookingProtection
            /// </summary>
            addOverbookingProtection = 1,
            /// <summary>
            /// ConfirmedBookingBehaviour
            /// </summary>
            confirmedBookingBehaviour = 2,
            /// <summary>
            /// Curl
            /// </summary>
            curl = 3,
            /// <summary>
            /// LastAvailabilityUpdate
            /// </summary>
            lastAvailabilityUpdate = 4,
            /// <summary>
            /// LastBookingReceived
            /// </summary>
            lastBookingReceived = 5,
            /// <summary>
            /// LastRhsAvailabilityFileModify
            /// </summary>
            lastRhsAvailabilityFileModify = 6,
            /// <summary>
            /// LastSuccessfulCommunication
            /// </summary>
            lastSuccessfulCommunication = 7,
            /// <summary>
            /// LastSettingsChange
            /// </summary>
            lastSettingsChange = 8,
            /// <summary>
            /// LookAhead
            /// </summary>
            lookAhead = 9,
            /// <summary>
            /// MultiRoomBookingsInSameReservation
            /// </summary>
            multiRoomBookingsInSameReservation = 10,
            /// <summary>
            /// NumRhsRoomTypes
            /// </summary>
            numRhsRoomTypes = 11,
            /// <summary>
            /// NumWebRoomTypes
            /// </summary>
            numWebRoomTypes = 12,
            /// <summary>
            /// RefDate
            /// </summary>
            refDate = 13,
            /// <summary>
            /// RefDateLastUpdate
            /// </summary>
            refDateLastUpdate = 14,
            /// <summary>
            /// Password
            /// </summary>
            password = 15,
            /// <summary>
            /// ServerCommunicationInterval
            /// </summary>
            serverCommunicationInterval = 16,
            /// <summary>
            /// ServerSite
            /// </summary>
            serverSite = 17,
            /// <summary>
            /// UpdateWebsiteAvailability
            /// </summary>
            updateWebsiteAvailability = 18,
            /// <summary>
            /// RemoteSupportInvites
            /// </summary>
            remoteSupportInvites = 19,
            /// <summary>
            /// UpdateUnmapped
            /// </summary>
            updateUnmapped = 20,
            /// <summary>
            /// ListboxHandle
            /// </summary>
            listboxHandle = 21
        }
        #endregion

        #region settingsRhsRoomTypeParameterType

        /// <summary>
        /// Enumerated Type that enumerates the parameters as they arrive in the 
        /// Settings File. 
        /// </summary>
        public enum settingsRhsRoomTypeParameterType : int
        {
            /// <summary>
            /// rhsRoomTypeId
            /// </summary>
            rhsRoomTypeId = 0,
            /// <summary>
            /// rhsRoomTypeName
            /// </summary>
            rhsRoomTypeName = 1,
            /// <summary>
            /// rhsRoomTypeCode
            /// </summary>
            rhsRoomTypeCode = 2,
            /// <summary>
            /// numRooms
            /// </summary>
            numRooms = 3,
            /// <summary>
            /// affectOccupancy
            /// </summary>
            affectOccupancy = 4,
            /// <summary>
            /// Availability
            /// </summary>
            Availability = 5,
            /// <summary>
            /// AvailabilityAtTimeOfLastUpdate
            /// </summary>
            AvailabilityAtTimeOfLastUpdate = 6

        }
        #endregion

        #region settingsWebRoomTypeParameterType

        /// <summary>
        /// Enumerated Type that enumerates the parameters as they arrive in the 
        /// Settings File. 
        /// </summary>
        public enum settingsWebRoomTypeParameterType : int
        {
            /// <summary>
            /// webRoomTypeId
            /// </summary>
            webRoomTypeId = 0,
            /// <summary>
            /// webRoomTypeName
            /// </summary>
            webRoomTypeName = 1,
            /// <summary>
            /// IsInterconnecting
            /// </summary>
            isInterconnecting = 2,
            /// <summary>
            /// RhsRoomTypesMappedToGroup1
            /// </summary>
            rhsRoomTypesMappedToGroup1 = 3,
            /// <summary>
            /// RhsRoomTypesMappedToGroup2
            /// </summary>
            rhsRoomTypesMappedToGroup2 = 4,
            /// <summary>
            /// maxNumberOfRooms
            /// </summary>
            maxNumberOfRooms = 5,
            /// <summary>
            /// AvailabilityAsDelimitedList
            /// </summary>
            AvailabilityAsDelimitedList = 6,
            /// <summary>
            /// AvailabilityAtTimeOfLastUpdateAsDelimitedList
            /// </summary>
            AvailabilityAtTimeOfLastUpdateAsDelimitedList = 7
        }
        #endregion

        #endregion

        #region AppVersion

        /// <summary>
        /// AppVersion
        /// </summary>
        /// <returns></returns>
        public static string AppVersion()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            return fvi.FileVersion;
        }

        #endregion

        #region GetBaseAppFolder


        /// <summary>
        /// GetBaseAppFolder
        /// </summary>
        /// <returns>Base Program Files Folder</returns>
        public string GetBaseAppFolder()
        {
            string thisBaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (!thisBaseFolder.EndsWith(@"\"))
                thisBaseFolder += @"\";

            thisBaseFolder += Application.ProductName + @"\";

            return thisBaseFolder;
        }

        #endregion

        #region RhsFolders

        /// <summary>
        /// Default Path where RHSFolders and StatusAndLogHandles reside
        /// </summary>
        public const string RHSFolders = @"RHSFolders.csv";

        /// <summary>
        /// rhsFolderStruct
        /// </summary>
        public struct rhsFolderStruct
        {
            /// <summary>
            /// FolderPath
            /// </summary>
            public string FolderPath;

            /// <summary>
            /// ListboxHandle
            /// </summary>
            public int ListboxHandle;
        }


        /// <summary>
        /// Get RhsFolders from file
        /// </summary>
        /// <returns></returns>
        public ArrayList GetRhsFolders()
        {
            string currentFunction = "GetRhsFolders";

            ArrayList theseRhsFolders = new ArrayList();


            string thisRhsFolderFile = GetBaseAppFolder() + RHSFolders;

            logIt(DateTime.Now,
                "Getting Rhs Folders",
                "GetBaseAppFolder : " + GetBaseAppFolder()
                + " RHSFolders: " + RHSFolders,
                currentFunction,
                "");

            bool fileExists = System.IO.File.Exists(thisRhsFolderFile);

            if (fileExists)
            {
                File.SetAttributes(thisRhsFolderFile, FileAttributes.Normal);
                clsCsvReader fileReader = new clsCsvReader(thisRhsFolderFile);
                string[] thisLine = fileReader.GetCsvLine();

                while (thisLine != null)
                {

                    int numParams = thisLine.GetUpperBound(0) + 1;

                    rhsFolderStruct thisFolder = new rhsFolderStruct();
                    thisFolder.FolderPath = "";
                    thisFolder.ListboxHandle = 0;


                    if (numParams > 0)
                    {
                        thisFolder.FolderPath = thisLine[0].Trim();

                        if (thisFolder.FolderPath.IndexOf(" ") > -1)
                            thisFolder.FolderPath = @"""" + thisFolder.FolderPath + @"""";

                        thisFolder.FolderPath = thisFolder.FolderPath.Trim();

                        if (!thisFolder.FolderPath.EndsWith(@"\"))
                            thisFolder.FolderPath = (thisFolder.FolderPath + @"\").Trim();

                    }

                    if (numParams > 1)
                    {
                        string thisHandle = thisLine[1].Trim();
                        if (isNumerical(thisHandle))
                        {
                            try
                            {
                                thisFolder.ListboxHandle = Convert.ToInt32(thisHandle);
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }

                    if (thisFolder.FolderPath != @"\" && thisFolder.FolderPath != "")
                    {
                        bool alreadyFoundFolder = false;
                        for (int counter = 0; counter < theseRhsFolders.Count; counter++)
                        {
                            rhsFolderStruct thisFolderFromList = (rhsFolderStruct)theseRhsFolders[counter];
                            if (thisFolderFromList.FolderPath.ToLower() == thisFolder.FolderPath.ToLower())
                                alreadyFoundFolder = true;
                        }

                        if (!alreadyFoundFolder)
                            theseRhsFolders.Add(thisFolder);

                    }


                    thisLine = fileReader.GetCsvLine();

                }


                fileReader.Dispose();
            }


            logIt(DateTime.Now,
                "Getting Rhs Folders",
                "fileExists: " + fileExists.ToString()
                + " theseRhsFolders.Count: " + theseRhsFolders.Count.ToString(),
                currentFunction,
                "");

            return theseRhsFolders;
        }


        /// <summary>
        /// EnsureRhsFolder
        /// </summary>
        /// <param name="FolderPath">FolderPath</param>
        /// <param name="ListboxHandle">ListboxHandle</param>
        public void EnsureRhsFolder(string FolderPath, int ListboxHandle)
        {

            FolderPath = FolderPath.Trim();
            string currentFunction = "EnsureRhsFolder";

            ArrayList theseRhsFolders = GetRhsFolders();

            int foundIndex = -1;
            bool requiresAnUpdate = false;


            if (FolderPath != "" && FolderPath != @"/" && theseRhsFolders != null)
            {
                int numRhsFolders = theseRhsFolders.Count;
                for (int counter = 0; counter < numRhsFolders; counter++)
                {
                    rhsFolderStruct thisFolder = (rhsFolderStruct)theseRhsFolders[counter];

                    if (thisFolder.FolderPath.ToLower() == FolderPath.ToLower().Trim())
                    {
                        foundIndex = counter;

                        if (thisFolder.ListboxHandle != ListboxHandle)
                        {
                            requiresAnUpdate = true;
                            theseRhsFolders.RemoveAt(counter);

                            thisFolder.ListboxHandle = ListboxHandle;
                            theseRhsFolders.Insert(counter, thisFolder);
                        }
                    }
                }
            }

            logIt(DateTime.Now,
                "EnsureRhsFolder",
                "requiresAnUpdate: " + requiresAnUpdate.ToString()
                + " foundIndex: " + foundIndex.ToString()
                + " theseRhsFolders.Count: " + theseRhsFolders.Count.ToString(),
                currentFunction,
                "");


            if (foundIndex == -1)
            {
                rhsFolderStruct thisFolder = new rhsFolderStruct();

                thisFolder.FolderPath = FolderPath;
                thisFolder.ListboxHandle = ListboxHandle;

                theseRhsFolders.Add(thisFolder);
                requiresAnUpdate = true;
            }

            logIt(DateTime.Now,
                "EnsureRhsFolder",
                "requiresAnUpdate: " + requiresAnUpdate.ToString()
                + " FolderPath: " + FolderPath
                + " ListboxHandle: " + ListboxHandle.ToString()
                + " theseRhsFolders.Count: " + theseRhsFolders.Count.ToString(),
                currentFunction,
                "");

            if (requiresAnUpdate)
                WriteRhsFolders(theseRhsFolders);

        }

        /// <summary>
        /// RemoveRhsFolder
        /// </summary>
        /// <param name="FolderPath">FolderPath</param>
        public ArrayList RemoveRhsFolder(string FolderPath)
        {
            string currentFunction = "RemoveRhsFolder";

            ArrayList theseRhsFolders = GetRhsFolders();

            int foundIndex = -1;
            bool requiresAnUpdate = false;

            int numRhsFolders = theseRhsFolders.Count;

            for (int counter = 0; counter < numRhsFolders; counter++)
            {
                rhsFolderStruct thisFolder = (rhsFolderStruct)theseRhsFolders[counter];

                if (thisFolder.FolderPath.ToLower() == FolderPath.ToLower().Trim())
                {
                    foundIndex = counter;

                    theseRhsFolders.RemoveAt(counter);
                    numRhsFolders--;
                    requiresAnUpdate = true;
                }
            }

            logIt(DateTime.Now,
                currentFunction,
                "requiresAnUpdate: " + requiresAnUpdate.ToString()
                + " foundIndex: " + foundIndex.ToString()
                + " theseRhsFolders.Count: " + theseRhsFolders.Count.ToString(),
                currentFunction,
                "");

            if (requiresAnUpdate)
                WriteRhsFolders(theseRhsFolders);

            return theseRhsFolders;
        }

        /// <summary>
        /// WriteRhsFolders
        /// </summary>
        public void WriteRhsFolders(ArrayList theseRhsFolders)
        {
            string currentFunction = "WriteRhsFolders";
            string thisRhsFolderFile = GetBaseAppFolder() + RHSFolders;

            bool fileExists = System.IO.File.Exists(thisRhsFolderFile);
            bool deleted = false;

            if (fileExists)
            {
                try
                {
                    File.SetAttributes(thisRhsFolderFile, FileAttributes.Normal);
                    System.IO.File.Delete(thisRhsFolderFile);
                    deleted = true;
                }
                catch (Exception e)
                {
                    LogThis(DateTime.Now, "Deleting RHS Folder File: " + thisRhsFolderFile,
                        e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolderFile);

                }
            }

            logIt(DateTime.Now,
                currentFunction,
                "fileExists: " + fileExists.ToString()
                + " deleted: " + deleted.ToString(),
                currentFunction,
                "");


            try
            {
                clsCsvWriter thisCsvWriter = new clsCsvWriter(thisRhsFolderFile, false);

                int numRhsFolders = theseRhsFolders.Count;

                for (int counter = 0; counter < numRhsFolders; counter++)
                {
                    rhsFolderStruct thisFolder = (rhsFolderStruct)theseRhsFolders[counter];

                    thisCsvWriter.WriteFields(new object[] {
                        thisFolder.FolderPath,
                        thisFolder.ListboxHandle
                    });
                }


                thisCsvWriter.Close();
            }
            catch (Exception e)
            {
                LogThis(DateTime.Now, "Writing RHS Folder File: " + thisRhsFolderFile,
                    e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolderFile);
            }
        }


        #endregion

        #region Logging

        /// <summary>
        /// Delegate that alerts an application when something has been logged
        /// </summary>
        public delegate void DelLogThis(DateTime entryDate, string userTitle, string userDescription, string functionName, string nonPublicText);

        /// <summary>
        /// Path and File Name where the Application's Log file is stored
        /// </summary>
        public const string logFileName = "RHSWebroomsLog";

        ///// <summary>
        ///// Path and File Name where the Application's Old Log file is stored
        ///// </summary>
        //      public string oldLogPathFile = GetBaseAppFolder() + "old" + logFileName;

        /// <summary>
        /// Date when the Log files were last purged
        /// </summary>
        public string lastDateLogFileUpdated = "";

        /// <summary>
        /// Remote Application Executable Name
        /// </summary>
        public string remoteApplicationExeName = "winvnc4";

        /// <summary>
        /// Quick Support Application Executable Name
        /// </summary>
        public string quickSupportApplicationExeName = "TeamViewerQS_en";

        /// <summary>
        /// Quick Support Application Executable Name
        /// </summary>
        public string webroomsUpdaterApplicationExeName = "WebroomsUpdate";


        /// <summary>
        /// LB_INSERTSTRING
        /// </summary>
        public const int LB_INSERTSTRING = 0x181;

        /// <summary>
        /// LogThis
        /// </summary>
        /// <param name="entryDate">entryDate</param>
        /// <param name="userTitle">userTitle</param>
        /// <param name="userDescription">userDescription</param>
        /// <param name="functionName">functionName</param>
        /// <param name="nonPublicText">nonPublicText</param>
        public void LogThis(DateTime entryDate,
            string userTitle,
            string userDescription,
            string functionName,
            string nonPublicText)
        {
            int listEventLogHandle = thisListboxHandle;
            LogThis(entryDate, userTitle, userDescription, functionName, nonPublicText, listEventLogHandle);

        }


        /// <summary>
        /// LogViaHandle
        /// </summary>
        /// <param name="thisHandle">thisHandle</param>
        /// <param name="thisEntry">thisEntry</param>
        public void LogViaHandle(int thisHandle, string thisEntry)
        {
            int wParam = 0;
            IntPtr appListBox = new IntPtr(thisHandle);

            IntPtr thisPointer = Marshal.StringToCoTaskMemAnsi(thisEntry);
            SendMessage(appListBox, LB_INSERTSTRING, wParam, thisPointer);

        }


        /// <summary>
        /// This event is fired when an Entry needs to be added to the Log
        /// </summary>
        /// <param name="entryDate">Date and time of the entry</param>
        /// <param name="userTitle">A user friendly title for the entry</param>
        /// <param name="userDescription">A user friendly description for the entry</param>
        /// <param name="functionName">Name of the function from which the event was fired</param>
        /// <param name="nonPublicText">Technical description for the entry</param>
        /// <param name="listEventLogHandle">listEventLogHandle</param>
        public void LogThis(DateTime entryDate,
            string userTitle,
            string userDescription,
            string functionName,
            string nonPublicText,
            int listEventLogHandle)
        {
            logIt(entryDate, userTitle, userDescription, functionName, nonPublicText);

            string thisEntry = entryDate + ": " + userTitle;

            if (userDescription.Trim().Length > 0)
                thisEntry += ", " + userDescription;

            //Limit the log length to 100 records
            if (thisWebRoomsForm == null)
            {
                if (listEventLogHandle != 0)
                {
                    LogViaHandle(listEventLogHandle, thisEntry);
                }
            }
            else
            {
                for (int deleteCounter = 100; deleteCounter < thisWebRoomsForm.listEventLog.Items.Count; deleteCounter++)
                    thisWebRoomsForm.listEventLog.Items.RemoveAt(deleteCounter);

                thisWebRoomsForm.listEventLog.Items.Insert(0, thisEntry);

                #region Handle the Handle

                int thisHandle = thisWebRoomsForm.listEventLog.Handle.ToInt32();

                if (thisHandle != thisWebRoomsForm.thisDbi.thisSetting.ListboxHandle)
                {
                    thisWebRoomsForm.thisDbi.thisSetting.ListboxHandle = thisHandle;
                    LogThis(entryDate, "ListBoxHandle Update", "Handle now: " + thisHandle.ToString(), "LogThis", "");
                }
                #endregion

            }
        }

        #endregion

        #region roomTypeStatusStruct

        /// <summary>
        /// Indicates the number of occupied and available rooms 
        /// for each RHS room type for a given night
        /// </summary>
        public struct roomTypeStatusStruct
        {
            /// <summary>
            /// Number occupied
            /// </summary>
            public int occupied;
            /// <summary>
            /// Number available
            /// </summary>
            public int available;
        }

        #endregion

        #region ASCII character constants

        /// <summary>
        /// ASCII Encoding
        /// </summary>
        public Encoding ascii = Encoding.ASCII;

        /// <summary>
        /// UNICODE Encoding
        /// </summary>
        public Encoding unicode = Encoding.Unicode;

        /// <summary>
        /// asciiDecimalPoint
        /// </summary>
        public const int asciiDecimalPoint = 46;

        /// <summary>
        /// asciiSlash
        /// </summary>
        public const int asciiSlash = 47;

        /// <summary>
        /// asciiZero
        /// </summary>
        public const int asciiZero = 48;

        /// <summary>
        /// asciiOne
        /// </summary>
        public const int asciiOne = 49;

        /// <summary>
        /// asciiTwo
        /// </summary>
        public const int asciiTwo = 50;

        /// <summary>
        /// asciiThree
        /// </summary>
        public const int asciiThree = 51;

        /// <summary>
        /// asciiFour
        /// </summary>
        public const int asciiFour = 52;

        /// <summary>
        /// asciiFive
        /// </summary>
        public const int asciiFive = 53;

        /// <summary>
        /// asciiNine
        /// </summary>
        public const int asciiNine = 57;

        /// <summary>
        /// aCrLf
        /// </summary>
        public const string aCrLf = "\r\n";

        /// <summary>
        /// aSpace
        /// </summary>
        public const string aSpace = " ";

        /// <summary>
        /// aComma
        /// </summary>
        public const string aComma = ",";

        /// <summary>
        /// aTild
        /// </summary>
        public const string aTild = "~";

        /// <summary>
        /// webCrLf
        /// </summary>
        public const string webCrLf = "~L";

        /// <summary>
        /// webComma
        /// </summary>
        public const string webComma = "~C";

        /// <summary>
        /// webTild
        /// </summary>
        public const string webTild = "~T";

        /// <summary>
        /// sLookAhead
        /// </summary>
        public const string sLookAhead = "lookAhead:";

        /// <summary>
        /// sRefDate
        /// </summary>
        public const string sRefDate = "refDate:";

        /// <summary>
        /// sNumRoomTypes
        /// </summary>
        public const string sNumRoomTypes = "numRoomTypes:";

        /// <summary>
        /// sNumBookings
        /// </summary>
        public const string sNumBookings = "numBookings:";

        /// <summary>
        /// sNumRooms
        /// </summary>
        public const string sNumRooms = "numRooms:";

        /// <summary>
        /// sNumRoomNights
        /// </summary>
        public const string sNumRoomNights = "numRoomNights:";

        #endregion

        #region Web Constants

        /// <summary>
        /// ThirdPartyBaseUrl
        /// </summary>
        public string ThirdPartyBaseUrl = @"https://ws.web-rooms.co.nz/rhswebrooms/webroomsservice.asmx";


        /// <summary>
        /// RHS Webrooms Updater Agent Name
        /// </summary>
        public string webAgent = "WWRv" + AppVersion();

        /// <summary>
        /// Web Site Timeout
        /// </summary>
        public int webTimeOut = 600000;

        /// <summary>
        /// numWebBookingParameters
        /// </summary>
        public const int numWebBookingParameters = 22;


        #endregion

        #region Constants

        /// <summary>
        /// EnqUser
        /// </summary>
        public const string thisEnqUser = @"RHSWebRooms";

        /// <summary>
        /// Default Path where RHS Resides
        /// </summary>
        public const string thisEnqPass = @"mince22";

        /// <summary>
        /// Default Rhs Poll interval, 1 minute
        /// </summary>
        public const double defaultRhsPollInterval = (1 * 60 * 1000);

        /// <summary>
        /// Default Path where RHS Resides
        /// </summary>
        public const string defaultPath = @"C:\RHS";



        /// <summary>
        /// Number of Rhs Files we are using to get RHS availability from
        /// </summary>
        public const int numRhsFiles = 6;

        /// <summary>
        /// Name of the Application
        /// </summary>
        public const string applicationName = "RHS Webrooms Updater";


        /// <summary>
        /// RHS Webrooms Updater General File Extenstion
        /// </summary>
        public const string rhsWebRoomsExtension = ".rwr";

        /// <summary>
        /// RHS General Data File Extenstion
        /// </summary>
        public const string rhsExtension = ".DAT";

        /// <summary>
        /// File Extension for 'Guest' Files (First half of the online booking file)
        /// </summary>
        public const string rhsGuestFileExtension = ".GUE";

        /// <summary>
        /// File Extension for 'Reservation' Files (Second half of the online booking file)
        /// </summary>
        public const string rhsReservationFileExtension = ".RES";

        /// <summary>
        /// File to check before reading RHS files
        /// </summary>
        public const string noRWRFile = "Norwr.dat";

        /// <summary>
        /// File Name of the Availability File
        /// </summary>
        public string availabilityFile = "RWRavail" + rhsWebRoomsExtension;

        /// <summary>
        /// File Name of the Last Communications File
        /// </summary>
        public string lastCommunicationsFile = "LASTRWR" + rhsExtension;

        /// <summary>
        /// progressInterval
        /// </summary>
        public const int progressInterval = 5;

        /// <summary>
        /// progressInterval
        /// </summary>
        public const int timeoutRetryTime = 6;

        #region Special Web Room Booking Parameters

        //		/// <summary>
        //		/// WebRmSourceSourceParameterIndicator
        //		/// </summary>
        //		public const string WebRmSourceSourceParameterIndicator = "<WRSRC=";
        //
        //		/// <summary>
        //		/// ETASourceParameterIndicator
        //		/// </summary>
        //		public const string ETASourceParameterIndicator = "<ETA=";
        //
        //		/// <summary>
        //		/// ServerSiteSourceParameterIndicator
        //		/// </summary>
        //		public const string ServerSiteSourceParameterIndicator = "<SS=";
        //
        //		/// <summary>
        //		/// RemoteInvitePatameterIndicator
        //		/// </summary>
        //		public const string RemoteInvitePatameterIndicator = "<RI=";

        #endregion


        #region Registry Key names

        ///// <summary>
        ///// Local Machine Registry Key
        ///// </summary>
        //public RegistryKey applicationRegistryKey = Registry.LocalMachine;

        /// <summary>
        /// Local Machine Registry Key
        /// </summary>
        public RegistryKey applicationRegistryKey = Registry.CurrentUser;

        /// <summary>
        /// Application Product Name
		/// </summary>
        public const string ApplicationProductName = "RHS Webrooms Updater";

        /// <summary>
        /// Application Registry Key
        /// </summary>
        public string applicationRegistrySubKey = @"SOFTWARE\Welman Technologies\";

        /// <summary>
        /// RhsRoomType Registry Sub-Key Stem
        /// </summary>
        public string rhsRoomTypeRegistrySubKeyStem = "RhsRoomTypeIndex";

        /// <summary>
        /// RhsRoomTypeId Registry Value Name
        /// </summary>
        public string rhsRoomTypeIdRegistryValueName = "RhsRoomTypeId";

        /// <summary>
        /// RhsRoomTypeName Registry Value Name
        /// </summary>
        public string rhsRoomTypeNameRegistryValueName = "RhsRoomTypeName";

        /// <summary>
        /// RhsRoomTypeCode Registry Value Name
        /// </summary>
        public string rhsRoomTypeCodeRegistryValueName = "RhsRoomTypeCode";

        /// <summary>
        /// RhsRoomTypeNumberOfRooms Registry Value Name
        /// </summary>
        public string rhsRoomTypeNumberOfRoomsRegistryValueName = "RhsRoomTypeNumberOfRooms";

        /// <summary>
        /// RhsRoomTypeOccupancy Registry Value Name
        /// </summary>
        public string rhsRoomTypeAffectOccupancyRegistryValueName = "RhsRoomTypeAffectOccupancy";

        /// <summary>
        ///  rhsRoomTypeAvailabilityAsDelimitedList Registry Value Name
        /// </summary>
        public string rhsRoomTypeAvailabilityAsDelimitedListRegistryValueName = "RhsRoomTypeAvailabilityAsDelimitedList";

        /// <summary>
        ///  rhsRoomTypeAvailabilityAtTimeOfLastUpdateAsDelimitedList Registry Value Name
        /// </summary>
        public string rhsRoomTypeAvailabilityAtTimeOfLastUpdateAsDelimitedListRegistryValueName = "RhsRoomTypeAvailabilityAtTimeOfLastUpdateAsDelimitedList";


        /// <summary>
        /// WebRoomType Registry Sub-Key Stem
        /// </summary>
        public string webRoomTypeRegistrySubKeyStem = "WebRoomTypeIndex";

        /// <summary>
        /// WebRoomTypeId Registry Value Name
        /// </summary>
        public string webRoomTypeIdRegistryValueName = "WebRoomTypeId";

        /// <summary>
        /// WebRoomTypeName Registry Value Name
        /// </summary>
        public string webRoomTypeNameRegistryValueName = "WebRoomTypeName";

        /// <summary>
        /// WebRoomTypeIsInterConnecting Registry Value Name
        /// </summary>
        public string webRoomTypeIsInterConnectingRegistryValueName = "WebRoomTypeIsInterConnecting";

        /// <summary>
        /// WebRoomTypeRhsRoomTypesMappedToGroup1AsDelimitedList Registry Value Name
        /// </summary>
        public string webRoomTypeRhsRoomTypesMappedToGroup1AsDelimitedListRegistryValueName = "WebRoomTypeRhsRoomTypesMappedToGroup1AsDelimitedList";

        /// <summary>
        /// WebRoomTypeRhsRoomTypesMappedToGroup2AsDelimitedList Registry Value Name
        /// </summary>
        public string webRoomTypeRhsRoomTypesMappedToGroup2AsDelimitedListRegistryValueName = "WebRoomTypeRhsRoomTypesMappedToGroup2AsDelimitedList";

        /// <summary>
        ///  webRoomTypeMaxNumberOfRooms Registry Value Name
        /// </summary>
        public string webRoomTypeMaxNumberOfRoomsRegistryValueName = "WebRoomTypeMaxNumberOfRooms";

        /// <summary>
        ///  webRoomTypeAvailabilityAsDelimitedList Registry Value Name
        /// </summary>
        public string webRoomTypeAvailabilityAsDelimitedListRegistryValueName = "WebRoomTypeAvailabilityAsDelimitedList";

        /// <summary>
        ///  webRoomTypeAvailabilityAtTimeOfLastUpdateAsDelimitedList Registry Value Name
        /// </summary>
        public string webRoomTypeAvailabilityAtTimeOfLastUpdateAsDelimitedListRegistryValueName = "WebRoomTypeAvailabilityAtTimeOfLastUpdateAsDelimitedList";


        #endregion

        #endregion

        #region Check Server Responses for Legitimacy

        #region parseServerResponseForGeneralError

        /// <summary>
        /// Parse Server Response For General Error
        /// </summary>
        /// <param name="response">Server Repons</param>
        /// <returns>Struct indicating Error or not</returns>
        public clsWelmanError parseServerResponseForGeneralError(string response)
        {
            clsWelmanError errDescription = new clsWelmanError();

            string failure = "err:";
            string errorVBS = "VBScript";
            string errorOdbc = "Microsoft OLE DB Provider for ODBC Drivers";

            if (response == null)
            {
                errDescription.errorForUser = "Failed to connect to remote Server. Check your internet connection is working.";
                errDescription.logFileDescription = "Null Response from httpPost";
                errDescription.success = false;
                return errDescription;
            }


            //Check the response and log it:
            int errorVBSPos = response.IndexOf(errorVBS);

            if (errorVBSPos > -1)
            {
                errDescription.errorForUser = "Server VB Script error; application will try again later.";
                errDescription.logFileDescription = response;
                errDescription.success = false;
                return errDescription;
            }

            int errorOdbcPos = response.IndexOf(errorOdbc);

            if (errorOdbcPos > -1)
            {
                errDescription.errorForUser = "Server ODBC Database error; application will try again later.";
                errDescription.logFileDescription = response;
                errDescription.success = false;
                return errDescription;
            }

            int failurePos = response.IndexOf(failure);

            if (failurePos > -1)
            {
                //Error number could be up to 3 figures.
                bool foundErrorNum = false;
                for (int counter = 3; (counter > 0) && !foundErrorNum; counter--)
                {
                    if (response.Length + 1 > failurePos + failure.Length + counter)
                    {
                        string param = response.Substring(failurePos + failure.Length, counter);
                        if (checkParam(param, "Positive Non Zero Integer", 0))
                        {
                            errDescription.errNum = getIntFromString(param);
                            foundErrorNum = true;
                        }
                    }
                }

                if (foundErrorNum)
                {
                    errDescription = webErrorTitle(errDescription.errNum);
                    errDescription.errorForUser = "Update Error: " + errDescription.errNum.ToString() + " " + errDescription.errorForUser;
                    errDescription.logFileDescription = response;
                    errDescription.success = false;
                    return errDescription;
                }

            }

            errDescription.success = true;
            return errDescription;

        }

        #endregion

        #region Check Server Response Parameters for Legitimacy

        /// <summary>
        /// This is an auxillary Parsing function. It checks a string for adherance to a datatype
        /// subject to a 'maximum length' for strings or 'number of decimal places' for Doubles
        /// </summary>
        /// <param name="paramString">The string the parameter is stored in</param>
        /// <param name="dataType">The datatype to check for</param>
        /// <param name="length">The 'maximum length' for strings or 'number of decimal places' for Doubles</param>
        /// <returns>True or false depending on whether the string adheres to the specified dataType</returns>
        public bool checkParam(string paramString, string dataType, int length)
        {
            byte[] testChar;
            bool noError = true;

            switch (dataType)
            {
                case "Double":
                    #region Double Precision variable e.g. xxxxx.yyy
                    //Length must be at least 1
                    if (paramString.Length < 1)
                        return false;

                    int pointPlace = paramString.IndexOf(".");

                    //The decimal point can not be at the start or end 
                    if (pointPlace == paramString.Length || pointPlace == 0)
                        return false;

                    //and there can only be a maximum of one one decimal point
                    if (pointPlace > -1)
                        if (paramString.IndexOf(".", pointPlace + 1) != -1)
                            return false;

                    testChar = ascii.GetBytes(paramString);

                    for (int index = 0; index < paramString.Length; index++)
                    {
                        if ((testChar[index] > asciiNine) || (testChar[index] < asciiZero)
                            && (testChar[index] != asciiDecimalPoint))
                            return false;
                        //Zero is allowed if 
                        // o Its not the first character or
                        // o It is the first character, but is 
                        //		o followed by a decimal point or
                        //		o The only character
                        if ((testChar[index] == asciiZero) && (index == 0) && ((pointPlace != 1) || (paramString.Length == 1)))
                            return false;
                    }
                    #endregion
                    break;
                case "Positive Non Zero Integer":
                    #region must be numerical and bigger than 0
                    for (int index = 0; index < paramString.Length; index++)
                    {
                        testChar = ascii.GetBytes(paramString.Substring(index, 1));
                        if ((testChar[0] > asciiNine) || (testChar[0] < asciiZero))
                            return false;
                        if ((testChar[0] == asciiZero) && (index == 0))
                            return false;
                    }
                    #endregion
                    break;
                case "Positive Integer or Zero":
                    #region must be numerical and bigger than -1
                    for (int index = 0; index < paramString.Length; index++)
                    {
                        testChar = ascii.GetBytes(paramString.Substring(index, 1));
                        if ((testChar[0] > asciiNine) || (testChar[0] < asciiZero))
                            return false;
                        if ((testChar[0] == asciiZero) && (index == 0) && (paramString.Length > 1))
                            return false;
                    }
                    #endregion
                    break;
                case "Boolean":
                    #region Must be 'true' or 'false'
                    string testParam = paramString.ToLower().Trim();
                    if ((testParam != "true") && (testParam != "false"))
                        return false;
                    #endregion
                    break;
                case "String":
                    #region check length, if this is not '0' (which means no restriction)
                    int thisActualLength = paramString.Length;
                    int nextTilde = paramString.IndexOf("~");
                    while (nextTilde > -1)
                    {
                        thisActualLength--;
                        nextTilde = paramString.IndexOf("~", nextTilde + 1);
                    }
                    if (length != 0 && thisActualLength > length + 1)
                        return false;
                    #endregion
                    break;
                case "CCExpiry":
                    #region mm/yyyy w.g 05/2006
                    //Length must be 7 characters
                    if (paramString.Length != 7)
                        return false;

                    //First two numbers are the month
                    testChar = ascii.GetBytes(paramString.Substring(0, 7));

                    //First number of month must be either zero or one
                    if ((testChar[0] < asciiZero) || (testChar[0] > asciiOne))
                        return false;

                    //Second number of month must be 1-9 if first number was 0, 
                    // or 0-2 if the first number was 1.

                    if (testChar[0] == asciiZero) //First Char '0'
                        if ((testChar[1] < asciiOne) || (testChar[1] > asciiNine)) // 2nd Char 1-9
                            return false;

                    if (testChar[0] == asciiOne) //First Char '1'
                        if ((testChar[1] < asciiZero) || (testChar[1] > asciiTwo)) // 2nd Char 0-2
                            return false;

                    //Third character must be a slash
                    if (testChar[2] != asciiSlash)
                        return false;

                    //4th Char should be at least 2
                    if ((testChar[3] < asciiTwo) || (testChar[3] > asciiNine))
                        return false;

                    //5th through 7th Chars can be any number
                    for (int charCounter = 4; charCounter < 7; charCounter++)
                    {
                        if ((testChar[charCounter] < asciiZero) || (testChar[charCounter] > asciiNine))
                            return false;
                    }
                    #endregion
                    break;
                case "Date":
                    #region yyyymmdd e.g. 20050103
                    //Length must be 8 characters
                    if (paramString.Length != 8)
                        return false;
                    testChar = ascii.GetBytes(paramString.Substring(0, 8));

                    //1st character must be at least 2
                    if ((testChar[0] < asciiTwo) || (testChar[0] > asciiNine))
                        return false;

                    //2nd through 4th characters can be any number
                    for (int charCounter = 1; charCounter < 4; charCounter++)
                    {
                        if ((testChar[charCounter] < asciiZero) || (testChar[charCounter] > asciiNine))
                            return false;
                    }

                    //First number of month must be either zero or one
                    if ((testChar[4] < asciiZero) || (testChar[4] > asciiOne))
                        return false;

                    //Second number of month must be 1-9 if first number was 0, 
                    // or 0-2 if the first number was 1.

                    if (testChar[4] == asciiZero) //First Char '0'
                        if ((testChar[5] < asciiOne) || (testChar[5] > asciiNine)) // 2nd Char 1-9
                            return false;

                    if (testChar[4] == asciiOne) //First Char '1'
                        if ((testChar[5] < asciiZero) || (testChar[5] > asciiTwo)) // 2nd Char 0-2
                            return false;

                    //First number of dat must be either zero, one, two or three
                    if ((testChar[6] < asciiZero) || (testChar[6] > asciiThree))
                        return false;

                    //Second number of month must be:
                    //	1-9 if first number was 0
                    //	0-9 if first number was 1 or 2 
                    //  0-1 if  first number was 3.
                    if (testChar[6] == asciiZero)                               // First Char '0'
                        if ((testChar[7] < asciiOne) || (testChar[7] > asciiNine))  // 2nd Char 1-9
                            return false;

                    if ((testChar[6] == asciiOne) || (testChar[6] == asciiTwo))     // First Char '1' or '2'
                        if ((testChar[7] < asciiZero) || (testChar[7] > asciiNine)) // 2nd Char 0-9
                            return false;

                    if (testChar[6] == asciiThree)                              // First Char '3'
                        if ((testChar[7] < asciiZero) || (testChar[7] > asciiOne))  // 2nd Char 0-1
                            return false;
                    #endregion
                    break;
                case "DateTime":
                    #region yyyymmddhhmmss e.g. 20051103012334 = 1:23am and 24 seconds on 3/11/2005
                    //Length must be 14 characters
                    if (paramString.Length != 14)
                        return false;

                    testChar = ascii.GetBytes(paramString.Substring(0, 14));

                    //1st character of year must be at least 2
                    if ((testChar[0] < asciiTwo) || (testChar[0] > asciiNine))
                        return false;

                    //2nd through 4th characters can be any number
                    for (int charCounter = 1; charCounter < 4; charCounter++)
                    {
                        if ((testChar[charCounter] < asciiZero) || (testChar[charCounter] > asciiNine))
                            return false;
                    }

                    //First number of month must be either zero or one
                    if ((testChar[4] < asciiZero) || (testChar[4] > asciiOne))
                        return false;

                    //Second number of month must be 1-9 if first number was 0, 
                    // or 0-2 if the first number was 1.

                    if (testChar[4] == asciiZero) //First Char '0'
                        if ((testChar[5] < asciiOne) || (testChar[5] > asciiNine)) // 2nd Char 1-9
                            return false;

                    if (testChar[4] == asciiOne) //First Char '1'
                        if ((testChar[5] < asciiZero) || (testChar[5] > asciiTwo)) // 2nd Char 0-2
                            return false;

                    //First number of dat must be either zero, one, two or three
                    if ((testChar[6] < asciiZero) || (testChar[6] > asciiThree))
                        return false;

                    //Second number of month must be:
                    //	1-9 if first number was 0
                    //	0-9 if first number was 1 or 2 
                    //  0-1 if  first number was 3.
                    if (testChar[6] == asciiZero)                               // First Char '0'
                        if ((testChar[7] < asciiOne) || (testChar[7] > asciiNine))  // 2nd Char 1-9
                            return false;

                    if ((testChar[6] == asciiOne) || (testChar[6] == asciiTwo))     // First Char '1' or '2'
                        if ((testChar[7] < asciiZero) || (testChar[7] > asciiNine)) // 2nd Char 0-9
                            return false;

                    if (testChar[6] == asciiThree)                              // First Char '3'
                        if ((testChar[7] < asciiZero) || (testChar[7] > asciiOne))  // 2nd Char 0-1
                            return false;

                    //First number of hour must be either 0,1 or 2
                    if ((testChar[8] < asciiZero) || (testChar[8] > asciiTwo))
                        return false;

                    //Second number of hour must be:
                    //	0-9 if first number was 0 or 1
                    //  0-3 if  first number was 2.

                    if ((testChar[8] == asciiZero) || (testChar[8] == asciiOne))        //First Char 0 or 1
                        if ((testChar[9] < asciiZero) || (testChar[9] > asciiNine)) // 2nd Char 0-9
                            return false;

                    if (testChar[8] == asciiTwo)                                //First Char '2'
                        if ((testChar[9] < asciiZero) || (testChar[9] > asciiThree))    // 2nd Char 0-3
                            return false;

                    //First number of minute must be one of 0,1,2,3,4,5
                    if ((testChar[10] < asciiZero) || (testChar[10] > asciiFive))
                        return false;

                    //Second number of minute can be any number
                    if ((testChar[11] < asciiZero) || (testChar[11] > asciiNine))
                        return false;

                    //First number of second must be one of 0,1,2,3,4,5
                    if ((testChar[12] < asciiZero) || (testChar[12] > asciiFive))
                        return false;

                    //Second number of second can be any number
                    if ((testChar[13] < asciiZero) || (testChar[13] > asciiNine))
                        return false;
                    #endregion
                    break;
                default:
                    noError = false;
                    break;
            }
            return noError; //Finished and passed!

        }

        #endregion

        #endregion

        #region Date Manipulation

        #region .Net Date to Welman (string) Date 

        /// <summary>
        /// Converts from .Net Date to Welman (string) Date 
        /// </summary>
        /// <param name="dateToFormat">.Net Date</param>
        /// <returns>Welman Date</returns>
        public string formatDateForWeb(DateTime dateToFormat)
        {
            string year = dateToFormat.Year.ToString().Trim();
            string month = dateToFormat.Month.ToString().Trim();
            string day = dateToFormat.Day.ToString().Trim();

            if (month.Length == 1)
                month = "0" + month;

            if (day.Length == 1)
                day = "0" + day;

            return year + month + day;
        }


        /// <summary>
        /// Converts from Welman (string) Date to .Net Date  
        /// </summary>
        /// <param name="dateToFormat">Welman Date</param>
        /// <returns>.Net Date</returns>
        public DateTime unformatDateForWeb(string dateToFormat)
        {

            System.Globalization.CultureInfo info =
                new System.Globalization.CultureInfo("en-US", false);

            System.Globalization.Calendar calendar = info.Calendar;

            int tempYear = getIntFromString(dateToFormat.Substring(0, 4));
            int tempMonth = getIntFromString(dateToFormat.Substring(4, 2));
            int tempDay = getIntFromString(dateToFormat.Substring(6, 2));


            DateTime subsequent = new DateTime(tempYear, tempMonth, tempDay,
                0, 0, 0, 0, calendar);

            return subsequent;
        }

        /// <summary>
        /// Converts a C# DateTime to a Welman Little-Endian DateTime string
        /// </summary>
        /// <param name="dateToConvert">C# DateTime</param>
        /// <param name="dateOnly">Whether to return just the date or include the time too</param>
        /// <returns>Welman DateTime string</returns>
        public string toWelmanDate(DateTime dateToConvert, bool dateOnly)
        {
            if (!dateOnly)
                return toWelmanDate(dateToConvert);
            else
            {
                string welmanDateTime = "";
                string temp;

                temp = dateToConvert.Year.ToString().Trim();
                while (temp.Length < 4)
                {
                    temp = "0" + temp;
                }
                welmanDateTime += temp;

                temp = dateToConvert.Month.ToString().Trim();
                while (temp.Length < 2)
                {
                    temp = "0" + temp;
                }
                welmanDateTime += temp;

                temp = dateToConvert.Day.ToString().Trim();
                while (temp.Length < 2)
                {
                    temp = "0" + temp;
                }
                welmanDateTime += temp;
                return welmanDateTime;
            }
        }


        /// <summary>
        /// Converts a C# DateTime to a Welman DateTime string
        /// </summary>
        /// <param name="dateToConvert">C# DateTime</param>
        /// <returns>Welman DateTime string</returns>
        public string toWelmanDate(DateTime dateToConvert)
        {
            string welmanDateTime = "";
            string temp;

            temp = dateToConvert.Year.ToString().Trim();
            while (temp.Length < 4)
            {
                temp = "0" + temp;
            }
            welmanDateTime += temp;

            temp = dateToConvert.Month.ToString().Trim();
            while (temp.Length < 2)
            {
                temp = "0" + temp;
            }
            welmanDateTime += temp;

            temp = dateToConvert.Day.ToString().Trim();
            while (temp.Length < 2)
            {
                temp = "0" + temp;
            }
            welmanDateTime += temp;

            temp = dateToConvert.Hour.ToString().Trim();
            while (temp.Length < 2)
            {
                temp = "0" + temp;
            }
            welmanDateTime += temp;

            temp = dateToConvert.Minute.ToString().Trim();
            while (temp.Length < 2)
            {
                temp = "0" + temp;
            }
            welmanDateTime += temp;
            temp = dateToConvert.Second.ToString().Trim();

            while (temp.Length < 2)
            {
                temp = "0" + temp;
            }
            welmanDateTime += temp;

            return welmanDateTime;
        }


        /// <summary>
        /// Takes a Welman Date/Time and converts it to a .Net Date/Time
        /// </summary>
        /// <param name="welmanDateTime">a Welman Date/Time</param>
        /// <returns>.Net Date/Time</returns>
        public DateTime fromWelmanDate(string welmanDateTime)
        {
            System.Globalization.CultureInfo info =
                new System.Globalization.CultureInfo("en-US", false);

            System.Globalization.Calendar calendar = info.Calendar;

            DateTime newDate;

            if (welmanDateTime.Length != 14)
                newDate = new DateTime(1, 1, 1, 0, 0, 0, 0, calendar);
            else
            {
                //Check that the string is numeric
                newDate = new DateTime(getIntFromString(welmanDateTime.Substring(0, 4)),
                    getIntFromString(welmanDateTime.Substring(4, 2)),
                    getIntFromString(welmanDateTime.Substring(6, 2)),
                    getIntFromString(welmanDateTime.Substring(8, 2)),
                    getIntFromString(welmanDateTime.Substring(10, 2)),
                    getIntFromString(welmanDateTime.Substring(12, 2)),
                    0, calendar);
            }

            return newDate;

        }

        #endregion

        /// <summary>
        /// Returns a MasterValue from a DateTime
        /// </summary>
        /// <param name="thisDate">thisDate</param>
        /// <returns>MasterValue</returns>
        public int FromDateToMasterValue(DateTime thisDate)
        {
            int MasterValue = thisDate.Year * 1300 + thisDate.Month * 100 + thisDate.Day;

            return MasterValue;
        }

        /// <summary>
        /// FromMasterValueToDateTime
        /// </summary>
        /// <param name="thisMasterValue">thisMasterValue</param>
        /// <returns>DateTime</returns>
        public DateTime FromMasterValueToDateTime(long thisMasterValue)
        {
            DateTime initial = new DateTime(1, 1, 1);

            int tempDay = (int)(thisMasterValue % 100);
            int tempYear = (int)(thisMasterValue / 1300);
            int tempMonth = (int)(thisMasterValue / 100) % 13;

            try
            {
                initial = new DateTime(tempYear, tempMonth, tempDay);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                initial = new DateTime(1, 1, 1);
            }

            return initial;

        }


        #region Get Days Between two dates

        /// <summary>
        /// Get the days between two dates, regardless of if they are in .Net or RHS format
        /// </summary>
        /// <param name="dateInitial">Initial Date</param>
        /// <param name="dateSubsequent">Subsequent Date</param>
        /// <returns>Number of Days</returns>
        public clsDaysBetween daysBetween(DateTime dateInitial, DateTime dateSubsequent)
        {
            string currentFunction = "daysBetween(DateTime dateInitial, DateTime dateSubsequent)";

            clsDaysBetween result = new clsDaysBetween();
            result.errDescription = new clsWelmanError();
            result.errDescription.success = false; //Starting off pessimistic...
            result.errDescription.errNum = 0;
            result.errDescription.errorForUser = "";
            result.errDescription.logFileDescription = "";

            TimeSpan duration = new TimeSpan();

            try
            {
                duration = dateSubsequent - dateInitial;
            }
            catch (System.Exception e)
            {
                result.errDescription.errorForUser = "General Exception getting duration between two dates";
                result.errDescription.logFileDescription = "dateInitial = " + dateInitial.ToString()
                    + " dateSubsequent = " + dateSubsequent.ToString();

                LogThis(DateTime.Now, result.errDescription.errorForUser,
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    result.errDescription.logFileDescription);

                return result;
            }

            //Otherwise, we have succeeded
            result.daysBetween = Convert.ToInt64(duration.TotalDays + 1);
            result.errDescription.success = true;

            return result;
        }

        /// <summary>
        /// Get the days between two dates, regardless of if they are in .Net or RHS format
        /// </summary>
        /// <param name="dateInitial">Initial Date</param>
        /// <param name="dateSubsequent">Subsequent Date</param>
        /// <returns>Number of Days</returns>
        public clsDaysBetween daysBetween(long dateInitial, DateTime dateSubsequent)
        {
            string currentFunction = "daysBetween(long dateInitial, DateTime dateSubsequent)";

            clsDaysBetween result = new clsDaysBetween();
            result.errDescription = new clsWelmanError();

            result.errDescription.success = false; //Starting off pessimistic...
            result.errDescription.errNum = 0;
            result.errDescription.errorForUser = "";
            result.errDescription.logFileDescription = "";

            //1) Convert the 'MasterValue' and 'startDate' to dates
            //2) Find the difference between them in days
            // return this
            System.Globalization.CultureInfo info =
                new System.Globalization.CultureInfo("en-US", false);

            System.Globalization.Calendar calendar = info.Calendar;
            DateTime initial = new DateTime();

            int tempDay = (int)(dateInitial % 100);
            int tempYear = (int)(dateInitial / 1300);
            int tempMonth = (int)(dateInitial / 100) % 13;

            try
            {
                initial = new DateTime(tempYear, tempMonth, tempDay,
                    0, 0, 0, 0, calendar);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                result.errDescription.errorForUser = "ArgumentOutOfRangeException creating a date";
                result.errDescription.logFileDescription = "dateInitial = " + dateInitial.ToString()
                    + " tempDay = " + tempDay.ToString()
                    + " tempMonth = " + tempMonth.ToString()
                    + " tempYear = " + tempYear.ToString();


                LogThis(DateTime.Now, result.errDescription.errorForUser,
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    result.errDescription.logFileDescription);

                return result;
            }
            //Otherwise, we should succeed
            return daysBetween(initial, dateSubsequent);
        }



        /// <summary>
        /// Get the days between two dates, regardless of if they are in .Net or RHS format
        /// </summary>
        /// <param name="dateInitial">Initial Date</param>
        /// <param name="dateSubsequent">Subsequent Date</param>
        /// <returns>Number of Days</returns>
        public clsDaysBetween daysBetween(DateTime dateInitial, long dateSubsequent)
        {
            string currentFunction = "daysBetween(DateTime dateInitial, long dateSubsequent)";

            clsDaysBetween result = new clsDaysBetween();
            result.errDescription = new clsWelmanError();
            result.errDescription.success = false; //Starting off pessimistic...
            result.errDescription.errNum = 0;
            result.errDescription.errorForUser = "";
            result.errDescription.logFileDescription = "";

            //1) Convert the 'MasterValue' and 'startDate to dates
            //2) Find the difference between them in days
            // return this
            System.Globalization.CultureInfo info =
                new System.Globalization.CultureInfo("en-US", false);

            System.Globalization.Calendar calendar = info.Calendar;
            DateTime subsequent = new DateTime();

            int tempDay = (int)(dateSubsequent % 100);
            int tempYear = (int)(dateSubsequent / 1300);
            int tempMonth = (int)(dateSubsequent / 100) % 13;

            try
            {
                subsequent = new DateTime(tempYear, tempMonth, tempDay,
                    0, 0, 0, 0, calendar);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                result.errDescription.errorForUser = "ArgumentOutOfRangeException creating a date";
                result.errDescription.logFileDescription = "dateSubsequent = " + dateSubsequent.ToString()
                    + " tempDay = " + tempDay.ToString()
                    + " tempMonth = " + tempMonth.ToString()
                    + " tempYear = " + tempYear.ToString();


                LogThis(DateTime.Now, result.errDescription.errorForUser,
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    result.errDescription.logFileDescription);

                return result;
            }

            return daysBetween(dateInitial, subsequent);
        }

        /// <summary>
        /// Get the days between two dates, regardless of if they are in .Net or RHS format
        /// </summary>
        /// <param name="dateInitial">Initial Date</param>
        /// <param name="dateSubsequent">Subsequent Date</param>
        /// <returns>Number of Days</returns>
        public clsDaysBetween daysBetween(long dateInitial, long dateSubsequent)
        {
            string currentFunction = "daysBetween(long dateInitial, long dateSubsequent)";

            clsDaysBetween result = new clsDaysBetween();
            result.errDescription = new clsWelmanError();
            result.errDescription.success = false; //Starting off pessimistic...
            result.errDescription.errNum = 0;
            result.errDescription.errorForUser = "";
            result.errDescription.logFileDescription = "";

            //1) Convert the 'MasterValue' and 'startDate to dates
            //2) Find the difference between them in days
            // return this
            System.Globalization.CultureInfo info =
                new System.Globalization.CultureInfo("en-US", false);

            System.Globalization.Calendar calendar = info.Calendar;

            DateTime subsequent = new DateTime();

            int tempDay = (int)(dateSubsequent % 100);
            int tempYear = (int)(dateSubsequent / 1300);
            int tempMonth = (int)(dateSubsequent / 100) % 13;

            try
            {
                subsequent = new DateTime(tempYear, tempMonth, tempDay,
                    0, 0, 0, 0, calendar);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                result.errDescription.errorForUser = "ArgumentOutOfRangeException creating a date";
                result.errDescription.logFileDescription = "dateSubsequent = " + dateSubsequent.ToString()
                    + " tempDay = " + tempDay.ToString()
                    + " tempMonth = " + tempMonth.ToString()
                    + " tempYear = " + tempYear.ToString();


                LogThis(DateTime.Now, result.errDescription.errorForUser,
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    result.errDescription.logFileDescription);
                return result;
            }

            return daysBetween(dateInitial, subsequent);
        }

        #endregion

        #endregion

        #region Log data

        /// <summary>
        /// Add information to the log file
        /// </summary>
        /// <param name="entryDate">entry Date for the item</param>
        /// <param name="userTitle">Title for the item (this will display in the 'Status and Log' Page log)</param>
        /// <param name="userDescription">User Description for the item (this will display in the 'Status and Log' Page log</param>
        /// <param name="functionName">Name of the function from which this item is added (this will not display in the 'Status and Log' Page log</param>
        /// <param name="nonPublicText">Detailed Technical information about this event (this will not display in the 'Status and Log' Page log</param>
        public void logIt(DateTime entryDate,
            string userTitle,
            string userDescription,
            string functionName,
            string nonPublicText)
        {

            DateTime thisDate = DateTime.Now;

            //            string thisLogFileName = AppDomain.CurrentDomain.BaseDirectory;

            //string thisBaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            //if (!thisBaseFolder.EndsWith(@"\"))
            //    thisBaseFolder += @"\";

            //thisBaseFolder += Application.ProductName + @"\";

            string thisLogFileName = GetBaseAppFolder() + logFileName + toWelmanDate(thisDate, true) + rhsWebRoomsExtension;

            //Delete Any log file for a day that is in the month 2 prior to this one

            if (!Directory.Exists(GetBaseAppFolder()))
                Directory.CreateDirectory(GetBaseAppFolder());

            string[] fileList = Directory.GetFiles(GetBaseAppFolder(), logFileName + toWelmanDate(thisDate.AddMonths(-1), true).Substring(0, 6) + "*");
            for (int fileCounter = 0; fileCounter < fileList.GetUpperBound(0) + 1; fileCounter++)
            {
                try
                {
                    File.SetAttributes(fileList[fileCounter], FileAttributes.Normal);
                    File.Delete(fileList[fileCounter]);
                }
                catch (System.UnauthorizedAccessException)
                {
                }
            }

            //Add new data to Log (Create file if necessary)
            StreamWriter log;

            try
            {
                log = new StreamWriter(thisLogFileName, true);
            }
            catch (System.UnauthorizedAccessException)
            {
                return;
            }
            catch (System.Exception)
            {
                return;
            }

            try
            {
                log.Write("{0},{1},{2},{3},{4}" + aCrLf, entryDate, userTitle, userDescription,
                    functionName, nonPublicText);
            }
            catch (System.UnauthorizedAccessException)
            {
                return;
            }
            catch (System.Exception)
            {
                return;
            }

            log.Close();
        }
        #endregion

        #region Auxillary

        #region SanitiseString

        /// <summary>
        /// Sanitise a string
        /// </summary>
        /// <param name="original">original</param>
        /// <returns>Sanitised String</returns>
        public string SanitiseString(string original)
        {
            string newVersion = original;

            for (int counter = 0; counter < 35; counter++)
            {
                char thisChar = Convert.ToChar(counter);
                newVersion = newVersion.Replace(thisChar.ToString(), " ");
            }
            newVersion = newVersion.Replace(",", "+");

            for (int counter = 123; counter < 256; counter++)
            {
                char thisChar = Convert.ToChar(counter);
                newVersion = newVersion.Replace(thisChar.ToString(), " ");
            }

            return newVersion.Trim();

        }

        #endregion

        #region isNumerical
        /// <summary>
        /// Returns whether a string is Numerical or not
        /// </summary>
        /// <param name="potentialNumber">potentialNumber</param>
        /// <returns>whether a string is Numerical or not</returns>
        public bool isNumerical(string potentialNumber)
        {
            int numLen = potentialNumber.Length;
            bool isNumeric = true;

            if (potentialNumber.Trim() == "")
                isNumeric = false;

            for (int counter = 0; counter < numLen; counter++)
            {
                char thisChar = Convert.ToChar(potentialNumber.Substring(counter, 1));
                int thisVal = (int)thisChar;
                if (thisVal < (int)'0' || thisVal > (int)'9')
                    isNumeric = false;

            }
            return isNumeric;
        }

        #endregion

        #region Replace instances of a string within a string

        /// <summary>
        /// Find / Replace string
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="findThis">string to find</param>
        /// <param name="replaceWithThis">string to replace found string with</param>
        /// <returns></returns>
        public string replace(string original, string findThis, string replaceWithThis)
        {
            string result = "";

            int currentIndex = 0;
            int nextOccurrance = original.IndexOf(findThis, currentIndex);

            while (nextOccurrance > -1)
            {
                result += original.Substring(currentIndex, nextOccurrance) + replaceWithThis;
                currentIndex = nextOccurrance + 1;
                nextOccurrance = original.IndexOf(findThis, currentIndex);
            }

            //Pick up the last bit
            result += original.Substring(currentIndex + 2);


            return result;
        }

        #endregion

        #region toByteArrayFromString

        /// <summary>
        /// Convert to Byte Array from String
        /// </summary>
        /// <param name="original"></param>
        /// <param name="lengthOfByteArray"></param>
        /// <returns></returns>
        public byte[] toByteArrayFromString(string original, int lengthOfByteArray)
        {
            byte[] originalToBytes = ascii.GetBytes(original.TrimEnd().PadRight(lengthOfByteArray, ' '));

            if (originalToBytes.GetUpperBound(0) + 1 > lengthOfByteArray)
            {
                byte[] returnArray = new byte[lengthOfByteArray];
                for (int byteCounter = 0; byteCounter < lengthOfByteArray; byteCounter++)
                    returnArray[byteCounter] = originalToBytes[byteCounter];
                return returnArray;
            }
            else

                return originalToBytes;
        }

        #endregion

        #region read/write Registry Keys (Removed as Registry centric)

        //        #region writeRegKey


        //        /// <summary>
        //        /// Writes a Registry Key Entry
        //        /// </summary>
        //        /// <param name="baseRegistryKey">baseRegistryKey</param>
        //        /// <param name="subKey">subKey</param>
        //        /// <param name="KeyName">KeyName</param>
        //        /// <param name="Value">Value</param>
        //        /// <returns>Success or Failure</returns>
        //        public bool writeRegKey(RegistryKey baseRegistryKey, string subKey, string KeyName, object Value)
        //        {
        //            string subName = "writeRegKey";

        //            //Ignore if not sensible
        //            if (subKey == "" || KeyName == "")
        //                return false;

        //            string user = "";

        //            RegistryKey baseKey = GetBaseKey();
        //            System.Security.AccessControl.RegistrySecurity thisSecurity;

        //            string thisbaseRegistryKeyName = "";

        //            int thisbaseRegistryKeyNameLength = thisbaseRegistryKeyName.Length;
        //            int thisbaseKeyNameLength = baseKey.Name.Length;

        //            #region Security Access Control. Not working, try to bypass

        //            //try
        //            //{

        //            //    thisSecurity = new System.Security.AccessControl.RegistrySecurity();

        //            //}
        //            //catch (Exception e)
        //            //{
        //            //    thisWebRoomsForm.addToLog(DateTime.Now,
        //            //        "Getting thisSecurity",
        //            //        e.ToString() + aCrLf
        //            //        + e.Message + aCrLf
        //            //        + e.StackTrace,
        //            //        subName, 
        //            //        "");

        //            //    LogThis(DateTime.Now,
        //            //        "Getting thisSecurity",
        //            //        e.ToString() + aCrLf
        //            //        + e.Message + aCrLf
        //            //        + e.StackTrace,
        //            //        subName, 
        //            //        "");

        //            //    return false;
        //            //}

        //            //try
        //            //{
        //            //    user = Environment.UserDomainName + "\\" + Environment.UserName;

        //            //}
        //            //catch (Exception e)
        //            //{
        //            //    thisWebRoomsForm.addToLog(DateTime.Now, 
        //            //        "Getting User",
        //            //        e.ToString() + aCrLf
        //            //        + e.Message + aCrLf
        //            //        + e.StackTrace,
        //            //        subName, 
        //            //        "");

        //            //    LogThis(DateTime.Now,
        //            //        "Getting User",
        //            //        e.ToString() + aCrLf
        //            //        + e.Message + aCrLf
        //            //        + e.StackTrace,
        //            //        subName, 
        //            //        "");

        //            //    return false;
        //            //}

        //            //try
        //            //{
        //            //    thisSecurity.AddAccessRule(new RegistryAccessRule(user, 
        //            //        RegistryRights.FullControl,
        //            //        InheritanceFlags.None, 
        //            //        PropagationFlags.None, 
        //            //        AccessControlType.Allow));

        //            //}
        //            //catch (Exception e)
        //            //{
        //            //    thisWebRoomsForm.addToLog(DateTime.Now, 
        //            //        "Getting Security Access Rule",
        //            //        "user: " + user + aCrLf
        //            //        + e.ToString() + aCrLf
        //            //        + e.Message + aCrLf
        //            //        + e.StackTrace,
        //            //        subName, 
        //            //        "");

        //            //    LogThis(DateTime.Now, 
        //            //        "Getting Security Access Rule",
        //            //        "user: " + user + aCrLf
        //            //        + e.ToString() + aCrLf
        //            //        + e.Message + aCrLf
        //            //        + e.StackTrace,
        //            //        subName, 
        //            //        "");

        //            //    return false;
        //            //}


        //            #endregion

        //            try
        //            {
        //                thisbaseRegistryKeyName = baseRegistryKey.Name;

        //                thisbaseRegistryKeyNameLength = thisbaseRegistryKeyName.Length;
        //                thisbaseKeyNameLength = baseKey.Name.Length;

        //                if (thisbaseRegistryKeyNameLength > thisbaseKeyNameLength)
        //                    subKey = thisbaseRegistryKeyName.Substring(thisbaseKeyNameLength, thisbaseRegistryKeyNameLength - thisbaseKeyNameLength)
        //                        + @"\" + subKey;


        ////				RegistryKey sk1 = baseRegistryKey.CreateSubKey(subKey, RegistryKeyPermissionCheck.Default, thisSecurity);
        ////                RegistryKey sk1 = baseKey.CreateSubKey(subKey, RegistryKeyPermissionCheck.Default, thisSecurity);
        //                RegistryKey sk1 = baseKey.CreateSubKey(subKey);

        //                // Save the value
        //                sk1.SetValue(KeyName.ToUpper(), Value.ToString());

        //                return true;
        //            }
        //            catch (Exception e)
        //            {
        //                thisWebRoomsForm.addToLog(DateTime.Now,
        //                    "Writing to the registry",
        //                    "thisbaseRegistryKeyName: " + thisbaseRegistryKeyName + aCrLf
        //                    + "thisbaseRegistryKeyNameLength: " + thisbaseRegistryKeyNameLength.ToString() + aCrLf
        //                    + "thisbaseKeyNameLength: " + thisbaseKeyNameLength.ToString() + aCrLf
        //                    + "subKey: " + subKey + aCrLf
        //                    + "KeyName: " + subKey + aCrLf
        //                    + "Value: " + Value.ToString() + aCrLf
        //                    + e.ToString() + aCrLf
        //                    + e.Message + aCrLf
        //                    + e.StackTrace,
        //                    subName,
        //                    "");

        //                LogThis(DateTime.Now,
        //                    "Writing to the registry",
        //                    "thisbaseRegistryKeyName: " + thisbaseRegistryKeyName + aCrLf
        //                    + "thisbaseRegistryKeyNameLength: " + thisbaseRegistryKeyNameLength.ToString() + aCrLf
        //                    + "thisbaseKeyNameLength: " + thisbaseKeyNameLength.ToString() + aCrLf
        //                    + "subKey: " + subKey + aCrLf
        //                    + "KeyName: " + subKey + aCrLf
        //                    + "Value: " + Value.ToString() + aCrLf
        //                    + e.ToString() + aCrLf
        //                    + e.Message + aCrLf
        //                    + e.StackTrace,
        //                    subName,
        //                    "");

        //                return false;
        //            }
        //        }

        //#endregion

        #region Determine Bit-ness

        static bool is64BitProcess = (IntPtr.Size == 8);
        static bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] out bool wow64Process
        );

        /// <summary>
        /// InternalCheckIsWow64
        /// </summary>
        /// <returns>True; 64 bit, False, 32 bit</returns>
        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// GetBaseKey
        /// </summary>
        /// <returns></returns>
        public static RegistryKey GetBaseKey()
        {
            //RegistryKey sk1 = Registry.LocalMachine;

            //if (is64BitOperatingSystem)
            //    sk1 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            //else
            //    sk1 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);

            RegistryKey sk1 = Registry.CurrentUser;

            if (is64BitOperatingSystem)
                sk1 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry64);
            else
                sk1 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry32);


            return sk1;

        }

        #endregion

        #region readRegKey (Removed as Registry centric)

        ///// <summary>
        ///// Read a Registry
        ///// </summary>
        ///// <param name="baseRegistryKey">baseRegistryKey</param>
        ///// <param name="subKey">subKey</param>
        ///// <param name="KeyName">KeyName</param>
        ///// <returns>Presence or null</returns>
        //public string readRegKey(RegistryKey baseRegistryKey, string subKey, string KeyName)
        //{
        //    //Ignore if not sensible
        //    if (KeyName == "")
        //        return null;

        //    bool success = false;
        //    string retVal = "";

        //    RegistryKey sk1 = GetBaseKey();

        //    string thisbaseRegistryKeyName = baseRegistryKey.Name;

        //    int thisbaseRegistryKeyNameLength = thisbaseRegistryKeyName.Length;
        //    int thissk1NameLength = sk1.Name.Length;

        //    if (thisbaseRegistryKeyNameLength > thissk1NameLength)
        //        subKey = thisbaseRegistryKeyName.Substring(thissk1NameLength, thisbaseRegistryKeyNameLength - thissk1NameLength)
        //            + @"\" + subKey;

        //    if (subKey != "")
        //    {
        //        try
        //        {
        //            sk1 = sk1.OpenSubKey(subKey);
        //        }
        //        catch (Exception e)
        //        {
        //            logIt(DateTime.Now, "Registry Key Open Fail", "Can't Access Subkey",
        //                "readRegKey",
        //                "is64BitProcess: " + is64BitProcess.ToString() + " "
        //                + "InternalCheckIsWow64(): " + InternalCheckIsWow64().ToString() + " "
        //                + "is64BitOperatingSystem: " + is64BitOperatingSystem.ToString() + " "
        //                + "subKey: " + subKey.ToString() + " "
        //                + "KeyName: " + KeyName.ToString() + " "
        //                + e.StackTrace +
        //                e.Message + baseRegistryKey.ToString() + subKey + KeyName);

        //            //ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
        //        }
        //    }
        //    // If the RegistrySubKey doesn't exist -> (null)
        //    if ( sk1 == null )
        //    {
        //        logIt(DateTime.Now, "Registry Key Read Fail", "General Non Issue",
        //            "readRegKey", "64 bit Registry Key does not exist: "
        //            + "baseRegistryKey: " + baseRegistryKey.ToString()
        //            + " subKey: " + subKey
        //            + " KeyName: " + KeyName);
        //    }
        //    else
        //    {
        //        try 
        //        {
        //            // If the RegistryKey exists I get its value
        //            // or null is returned.
        //            retVal =  (sk1.GetValue(KeyName.ToUpper()).ToString());
        //            success = true;
        //        }
        //        catch (Exception e)
        //        {
        //            logIt(DateTime.Now, "Registry Key Read Fail", "General Non Issue",
        //                "readRegKey",
        //                e.StackTrace +
        //                e.Message + aCrLf
        //                + "baseRegistryKey: " + baseRegistryKey.ToString()
        //                + " subKey: " + subKey
        //                + " KeyName: " + KeyName);

        //            //ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
        //        }
        //    }

        //    if (!success)
        //        return null;
        //    else
        //        return retVal;


        //}

        #endregion

        #endregion

        #region unWebFormatString (Take Escape sequences out of strings recovered from Server)

        /// <summary>
        /// Take Escape sequences out of strings recovered from Server
        /// </summary>
        /// <param name="webFormated">String from Web Server</param>
        /// <returns>Originl String</returns>
        public string unWebFormatString(string webFormated)
        {

            string result = "";

            int currentIndex = 0;
            int nextTild = webFormated.IndexOf(aTild, currentIndex);

            while (nextTild > -1)
            {
                //Examine this tild
                switch (webFormated.Substring(nextTild, 2))
                {
                    case webCrLf:
                        //Add balance of string to result, and then add crlf
                        result += webFormated.Substring(currentIndex, nextTild - currentIndex) + aCrLf;
                        break;
                    case webComma:
                        result += webFormated.Substring(currentIndex, nextTild - currentIndex) + aComma;
                        break;
                    case webTild:
                        result += webFormated.Substring(currentIndex, nextTild - currentIndex) + aTild;
                        break;
                    default:
                        result += webFormated.Substring(currentIndex, nextTild + 2 - currentIndex);
                        break;
                }


                currentIndex = nextTild + 2;
                nextTild = webFormated.IndexOf(aTild, currentIndex);
            }

            //Pick up the last bit
            result += webFormated.Substring(currentIndex);


            return result;
        }

        #endregion

        #region Get Data from Parameters

        /// <summary>
        /// Returns an integer from a string parameter
        /// </summary>
        /// <param name="stringContainingInt">string parameter</param>
        /// <returns>integer within or 0 if empty string</returns>
        public int getIntFromString(string stringContainingInt)
        {
            int retVal = 0;

            string currentFunction = "getIntFromString";

            string thisString = stringContainingInt.Trim();

            try
            {
                retVal = Convert.ToInt32(thisString);

            }
            catch (Exception e)
            {
                LogThis(DateTime.Now, "Error Converting Int",
                    e.ToString() + e.Message + e.StackTrace + e.StackTrace
                    , currentFunction,
                    "stringContainingInt: " + thisString);
                retVal = 0;

                try
                {
                    thisString = Regex.Match(thisString, @"\d+").Value;
                    retVal = Convert.ToInt32(thisString);
                }
                catch
                {
                    LogThis(DateTime.Now, "Error Converting Int even using Regex",
                        e.ToString() + e.Message + e.StackTrace + e.StackTrace
                        , currentFunction,
                        "stringContainingInt: " + thisString);
                    retVal = 0;
                }
            }

            return retVal;
            //
            //
            //			if ( stringContainingInt == "" || !isNumerical(stringContainingInt))
            //				return 0;
            //			else
            //				return Convert.ToInt32(stringContainingInt);
        }

        /// <summary>
        /// Returns a Double from a string parameter
        /// </summary>
        /// <param name="stringContainingDouble">string parameter</param>
        /// <returns>double within</returns>
        public Double getDoubleFromString(string stringContainingDouble)
        {
            double retVal = 0;
            string currentFunction = "getDoubleFromString";

            try
            {
                retVal = Convert.ToDouble(stringContainingDouble);

            }
            catch (Exception e)
            {
                LogThis(DateTime.Now, "Error Converting Double",
                    e.ToString() + e.Message + e.StackTrace, currentFunction,
                    "stringContainingDouble: " + stringContainingDouble);
                retVal = 0;
            }

            return retVal;
            //
            //			return Convert.ToDouble(stringContainingDouble);
        }

        /// <summary>
        /// Returns a Boolean from a string parameter
        /// </summary>
        /// <param name="stringContainingBool">string parameter</param>
        /// <returns>Bool within</returns>
        public bool getBoolFromString(string stringContainingBool)
        {
            if (stringContainingBool.ToLower().Trim() == "true")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns a Date from a string parameter
        /// </summary>
        /// <param name="stringContainingDate">string parameter</param>
        /// <returns>Date within</returns>
        public DateTime getDateFromString(string stringContainingDate)
        {
            string currentFunction = "getDateFromString";
            System.Globalization.CultureInfo info =
                new System.Globalization.CultureInfo("en-US", false);

            System.Globalization.Calendar calendar = info.Calendar;

            DateTime newDate;

            switch (stringContainingDate.Length)
            {
                case 0:
                    //default date; 1st January 0001
                    newDate = new DateTime(1, 1, 1, 0, 0, 0, 0, calendar);
                    break;
                case 8:     // yyyyMMdd
                    newDate = new DateTime(getIntFromString(stringContainingDate.Substring(0, 4)),
                        getIntFromString(stringContainingDate.Substring(4, 2)),
                        getIntFromString(stringContainingDate.Substring(6, 2)),
                        0, 0, 0, 0, calendar);
                    // Covered by default
                    break;
                case 14:    // yyyyMMddHHmmSS
                    newDate = fromWelmanDate(stringContainingDate);
                    break;
                default: // dd/mm/yyyy hh:mm:ss a.m. (or similar)
                    try
                    {
                        newDate = Convert.ToDateTime(stringContainingDate);
                    }
                    catch (System.FormatException e)
                    {
                        LogThis(DateTime.Now, "Error Converting Date",
                            e.ToString() + e.Message + e.StackTrace, currentFunction,
                            "stringContainingDate: " + stringContainingDate);
                        newDate = new DateTime(1, 1, 1, 0, 0, 0, 0, calendar);
                    }
                    break;
            }
            return newDate;
        }

        #endregion

        #region httpPost

        /// <summary>
        /// httpPost
        /// </summary>
        /// <param name="URI">URI</param>
        /// <param name="Parameters">Parameters</param>
        /// <returns>response</returns>
        public string httpPost(string URI, string Parameters)
        {
            #region Initialise

            string currentFunction = "httpPost";

            string thisContentType = "application/x-www-form-urlencoded";

            System.Net.WebResponse resp = null;
            //			System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };


            int numAvailableWorkerThreads = 0;
            int numAvailableCompletionPortThreads = 0;

            System.Threading.ThreadPool.GetAvailableThreads(out numAvailableWorkerThreads, out numAvailableCompletionPortThreads);

            LogThis(DateTime.Now, "About to Create HttpWebRequest Thread.",
                "", currentFunction,
                "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                + " and numAvailableCompletionPortThreads = "
                + numAvailableCompletionPortThreads.ToString());

            System.Net.HttpWebRequest req;
            byte[] bytes;

            #endregion

            #region WebRequest.Create

            try
            {
                req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URI);
            }
            catch (System.Net.WebException e)
            {
                LogThis(DateTime.Now, "Failed Server Connection Attempt creating HttpWebRequest",
                    e.ToString() + " , " + e.Message + " , " + e.Response + " , " + e.Source,
                    currentFunction, e.Status.ToString());
                return null;
            }
            catch (System.InvalidOperationException e)
            {
                LogThis(DateTime.Now, "Insufficient Free Threads in the ThreadPool Object creating HttpWebRequest",
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                    + " and numAvailableCompletionPortThreads = "
                    + numAvailableCompletionPortThreads.ToString());
                return null;
            }
            catch (System.SystemException e)
            {
                LogThis(DateTime.Now, "General Exception creating HttpWebRequest",
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                    + " and numAvailableCompletionPortThreads = "
                    + numAvailableCompletionPortThreads.ToString());
                return null;
            }

            #endregion

            #region Convert Parameters into bytes

            try
            {
                req.ContentType = thisContentType;
                req.Method = "POST";
                req.UserAgent = webAgent;
                req.Timeout = webTimeOut;

                bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);

                while (bytes.Length > 0 && bytes[0] != 60)
                {
                    byte[] newbytes = new byte[bytes.Length - 1];

                    for (int counter = 0; counter < bytes.Length - 1; counter++)
                        newbytes[counter] = bytes[counter + 1];

                    bytes = newbytes;
                }

                req.ContentLength = bytes.Length;
            }
            catch (System.Net.WebException e)
            {
                LogThis(DateTime.Now, "Failed Server Connection Attempt while setting HttpWebRequest parameters",
                    e.ToString() + " , " + e.Message + " , " + e.Response + " , " + e.Source,
                    currentFunction, e.Status.ToString());
                return null;
            }
            catch (System.InvalidOperationException e)
            {
                LogThis(DateTime.Now, "Insufficient Free Threads in the ThreadPool Object while setting HttpWebRequest parameters",
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                    + " and numAvailableCompletionPortThreads = "
                    + numAvailableCompletionPortThreads.ToString());
                return null;
            }
            catch (System.SystemException e)
            {
                LogThis(DateTime.Now, "General Exception while setting HttpWebRequest parameters",
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                    + " and numAvailableCompletionPortThreads = "
                    + numAvailableCompletionPortThreads.ToString());
                return null;
            }

            #endregion

            #region GetRequestStream

            try
            {
                System.IO.Stream os = req.GetRequestStream();
                os.Write(bytes, 0, bytes.Length);
                os.Close();
            }
            catch (System.Net.WebException e)
            {
                LogThis(DateTime.Now, "Failed Server Connection Attempt while performing GetRequestStream",
                    e.ToString() + " , " + e.Message + " , " + e.Response + " , " + e.Source,
                    currentFunction, e.Status.ToString());
                return null;
            }
            catch (System.InvalidOperationException e)
            {
                LogThis(DateTime.Now, "Insufficient Free Threads in the ThreadPool Object while performing GetRequestStream",
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                    + " and numAvailableCompletionPortThreads = "
                    + numAvailableCompletionPortThreads.ToString());
                return null;
            }
            catch (System.SystemException e)
            {
                LogThis(DateTime.Now, "General Exception while performing GetRequestStream",
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                    + " and numAvailableCompletionPortThreads = "
                    + numAvailableCompletionPortThreads.ToString());
                return null;
            }

            #endregion

            #region GetResponse

            try
            {
                resp = req.GetResponse();
            }
            catch (System.Net.WebException e)
            {
                LogThis(DateTime.Now, "Failed Server Connection Attempt while performing GetResponse",
                    e.ToString() + " , " + e.Message + " , " + e.Response + " , " + e.Source,
                    currentFunction, e.Status.ToString());
                return null;
            }
            catch (System.InvalidOperationException e)
            {
                LogThis(DateTime.Now, "Insufficient Free Threads in the ThreadPool Object while performing GetResponse",
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                    + " and numAvailableCompletionPortThreads = "
                    + numAvailableCompletionPortThreads.ToString());
                return null;
            }
            catch (System.SystemException e)
            {
                LogThis(DateTime.Now, "General Exception while performing GetResponse",
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                    + " and numAvailableCompletionPortThreads = "
                    + numAvailableCompletionPortThreads.ToString());
                return null;
            }

            #endregion

            #region Check for null response

            if (resp == null) return null;

            #endregion

            #region GetResponseStream

            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                string returnData = sr.ReadToEnd().Trim();
                LogThis(DateTime.Now, "Successful Communiction with Web Server",
                    "",
                    currentFunction, returnData);
                return returnData;
            }
            catch (System.Net.WebException e)
            {
                LogThis(DateTime.Now, "Failed Server Connection Attempt while performing StreamReader(GetResponseStream)",
                    e.ToString() + " , " + e.Message + " , " + e.Response + " , " + e.Source,
                    currentFunction, e.Status.ToString());
                return null;
            }
            catch (System.InvalidOperationException e)
            {
                LogThis(DateTime.Now, "Insufficient Free Threads in the ThreadPool Object while performing StreamReader(GetResponseStream)",
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                    + " and numAvailableCompletionPortThreads = "
                    + numAvailableCompletionPortThreads.ToString());
                return null;
            }
            catch (System.SystemException e)
            {
                LogThis(DateTime.Now, "General Exception while performing StreamReader(GetResponseStream)",
                    e.ToString() + " , " + e.Message + " , " + e.Source,
                    currentFunction,
                    "numAvailableWorkerThreads = " + numAvailableWorkerThreads.ToString()
                    + " and numAvailableCompletionPortThreads = "
                    + numAvailableCompletionPortThreads.ToString());
                return null;
            }


            #endregion

        }

        #endregion

        #region webErrorTitle (Returns Error Name from number)

        /// <summary>
        /// Returns Web Error Name from number
        /// </summary>
        /// <param name="webErrorNumber">Error Number, from Web Server</param>
        /// <returns>Web Error Name</returns>
        public clsWelmanError webErrorTitle(int webErrorNumber)
        {
            clsWelmanError errorTitle = new clsWelmanError();
            errorTitle.errNum = webErrorNumber;

            switch (webErrorNumber)
            {
                case 6:
                    errorTitle.logFileDescription = "";
                    errorTitle.errorForUser = "Website is set to ignore RHS Availability Updates";
                    break;
                case 41:
                    errorTitle.logFileDescription = "Invalid reference date";
                    errorTitle.errorForUser = "System date invalid; it is likely that this attempt happened close to midnight";
                    break;
                case 42:
                    errorTitle.logFileDescription = "Credentials database unavailable";
                    errorTitle.errorForUser = "Server temporarily unavailable";
                    break;
                case 43:
                    errorTitle.logFileDescription = "Invalid Credentials";
                    errorTitle.errorForUser = "Invalid User name / Password combination";
                    break;
                case 44:
                    errorTitle.logFileDescription = "di < 0 submitted";
                    errorTitle.errorForUser = "Server temporarily unavailable";
                    break;
                case 45:
                    errorTitle.logFileDescription = "Could not connect to the customer bookings web database";
                    errorTitle.errorForUser = "Server temporarily unavailable";
                    break;
                case 46:
                    errorTitle.logFileDescription = "Could not connect to the customer bookings web database";
                    errorTitle.errorForUser = "Your website is configured to not accept updates from this program. Until you log in to the website and change this setting you will not be able to automatically upload your availability";
                    break;
                case 47:
                    errorTitle.logFileDescription = "At least one rt value submitted do not exist in the roomtypes table";
                    errorTitle.errorForUser = "Incorrect room type submitted. Verify your room types";
                    break;
                case 48:
                    errorTitle.logFileDescription = "At least one oc value submitted exceeds the totalRooms value for that room type";
                    errorTitle.errorForUser = "Incoherent room type data submitted. Verify your room types";
                    break;
                case 49:
                    errorTitle.logFileDescription = "Too much data submitted";
                    errorTitle.errorForUser = "Over 10 years worth of data is being submitted with every update; contact Welman Technologies";
                    break;
                case 50:
                    errorTitle.logFileDescription = "Valid Credentials Supplied";
                    errorTitle.errorForUser = "Not submitting data; just checking credentials";
                    break;
                case 51:
                    errorTitle.logFileDescription = "Change Updated Only";
                    errorTitle.errorForUser = "Please contact Welman Technologies";
                    break;
                case 52:
                    errorTitle.logFileDescription = "Site Update already in progress";
                    errorTitle.errorForUser = "Service temporarily unavailable; please contact Welman Technologies if this problem persists";
                    break;
                case 56:
                    errorTitle.logFileDescription = "A connection was attempted that did not use SSL/HTTPS";
                    errorTitle.errorForUser = "Please contact Welman Technologies";
                    break;
                case 57:
                    errorTitle.logFileDescription = "Maximum log in attempts exceeded";
                    errorTitle.errorForUser = "Your maximum number of failed log in attempts has been exceeded and your account has been locked; please contact Welman Technologies";
                    break;
                case 58:
                    errorTitle.logFileDescription = "Agent Not Allowed";
                    errorTitle.errorForUser = "Please contact Welman Technologies";
                    break;
                case 59:
                    errorTitle.logFileDescription = "Incoherent data recieved; Non-numeric or empty di/rt/oc values";
                    errorTitle.errorForUser = "Service temporarily unavailable; please contact Welman Technologies if this problem persists";
                    break;
                case 60:
                    errorTitle.logFileDescription = "Incoherent data recieved; di/rt/oc submitted value counts to not match";
                    errorTitle.errorForUser = "Service temporarily unavailable; please contact Welman Technologies if this problem persists";
                    break;
                case 61:
                    errorTitle.logFileDescription = "Incoherent data recieved; blank rtuid or tr value submitted";
                    errorTitle.errorForUser = "Service temporarily unavailable; please contact Welman Technologies if this problem persists";
                    break;
                case 62:
                    errorTitle.logFileDescription = "Incoherent data recieved; Non numeric rtuid or tr value submitted";
                    errorTitle.errorForUser = "Service temporarily unavailable; please contact Welman Technologies if this problem persists";
                    break;
                case 63:
                    errorTitle.logFileDescription = "Submitted rtuid does not exist in room types database";
                    errorTitle.errorForUser = "Incorrect room type submitted. Verify your room types";
                    break;
                case 64:
                    errorTitle.logFileDescription = "Incoherent data recieved; Number of rtuid and/or tr values does not match number of configured web room types";
                    errorTitle.errorForUser = "Service temporarily unavailable; please contact Welman Technologies if this problem persists";
                    break;
                case 65:
                    errorTitle.logFileDescription = "Incoherent data recieved; No room types submitted";
                    errorTitle.errorForUser = "All online availability wiped; no room types are mapped. Go to the Room Mappings tab to rectify this.";
                    break;

                default:
                    errorTitle.logFileDescription = "Unspecified Error";
                    errorTitle.errorForUser = "Unspecified Error";
                    break;
            }

            return errorTitle;
        }

        #endregion

        #endregion

        #region Functions for Converting Between Buffered Bytes to their Values in VB

        /// <summary>
        /// Takes a byte buffer containing a VB Currency value and returns the value of this
        /// </summary>
        /// <param name="buffer">Byte buffer containing a VB Currency value</param>
        /// <param name="startByte">Position in the buffer to start</param>
        /// <returns>Equivalent Value to the VB currency</returns>
        public double bufferToCurrency(byte[] buffer, int startByte)
        {
            return ((double)(long)(
                (long)(buffer[startByte + 7] << 56) +
                (long)(buffer[startByte + 6] << 48) +
                (long)(buffer[startByte + 5] << 40) +
                (long)(buffer[startByte + 4] << 32) +
                (long)(buffer[startByte + 3] << 24) +
                (long)(buffer[startByte + 2] << 16) +
                (long)(buffer[startByte + 1] << 8) +
                (long)(buffer[startByte]))
                / 10000);
        }


        /// <summary>
        /// Takes a byte buffer containing a VB Int value and returns the value of this
        /// </summary>
        /// <param name="buffer">Byte buffer containing a VB Currency value</param>
        /// <param name="startByte">Position in the buffer to start</param>
        /// <returns>Equivalent Value to the VB int</returns>
        public int bufferToInt(byte[] buffer, int startByte)
        {
            return (int)((int)(buffer[startByte + 3] << 24) + (int)(buffer[startByte + 2] << 16) + (int)(buffer[startByte + 1] << 8) + (int)(buffer[startByte]));
        }

        /// <summary>
        /// Takes a byte buffer containing a VB Short value and returns the value of this
        /// </summary>
        /// <param name="buffer">Byte buffer containing a VB Short value</param>
        /// <param name="startByte">Position in the buffer to start</param>
        /// <returns>Equivalent Value to the VB Short</returns>
        public short bufferToShort(byte[] buffer, int startByte)
        {
            return (short)((short)(buffer[startByte + 1] << 8) + (short)(buffer[startByte]));
        }

        /// <summary>
        /// Takes a byte buffer containing a VB Extra Short value and returns the value of this
        /// </summary>
        /// <param name="buffer">Byte buffer containing a VB Extra Short value</param>
        /// <returns>Equivalent Value to the VB Extra Short</returns>
        public short bufferToExtraShort(byte buffer)
        {
            return (short)(buffer);
        }

        //End of Helper functions for collecting raw RHS data

        /// <summary>
        /// Take a short and convert it to a VB Int buffer
        /// </summary>
        /// <param name="numToConvert">numToConvert</param>
        /// <returns> VB Int buffer Equivalent</returns>
        public byte[] shortToVbIntBuffer(short numToConvert)
        {
            byte[] buffer = new byte[2];
            buffer[0] = Convert.ToByte(numToConvert / (2 ^ 8));
            buffer[1] = Convert.ToByte(numToConvert % (2 ^ 8));
            return buffer;
        }

        /// <summary>
        /// Take an int and convert it to a VB Long buffer
        /// </summary>
        /// <param name="numToConvert">numToConvert</param>
        /// <returns> VB Int buffer Equivalent</returns>
        public byte[] intToVbLongBuffer(int numToConvert)
        {
            byte[] buffer = new byte[4];

            buffer[0] = Convert.ToByte(numToConvert / (2 ^ 8));
            buffer[1] = Convert.ToByte(numToConvert % (2 ^ 8));
            buffer[2] = Convert.ToByte((numToConvert % (2 ^ 24)) / (2 ^ 16));
            buffer[3] = Convert.ToByte(numToConvert / (2 ^ 24));

            return buffer;
        }

        #endregion

        #region Web Rooms XML Serialiser



        #region Old

        ///// <summary>
        ///// Deserialise a WebRooms RwrCheckServer Message into a string
        ///// </summary>
        ///// <param name="thisRWRCheckServerRequest">WebRooms SetAvailability</param>
        ///// <returns>Deserialised XML</returns>
        //public string RWRCheckServerRequestXml(Comms.RWRServerParamsResponse thisRWRCheckServerRequest)
        //{
        //    MemoryStream stream = null;
        //    TextWriter writer = null;
        //    try
        //    {
        //        stream = new MemoryStream(); // read xml in memory

        //        writer = new StreamWriter(stream, Encoding.Unicode) ;
        //        // get serialise object

        //        XmlSerializer serializer = new XmlSerializer(typeof(Comms.RWRServerParamsResponse));
        //        serializer.Serialize(writer, thisRWRCheckServerRequest); // read object

        //        int count = (int) stream.Length; // saves object in memory stream

        //        byte[] arr = new byte[count];
        //        stream.Seek(0, SeekOrigin.Begin);
        //        // copy stream contents in byte array

        //        stream.Read(arr, 0, count);
        //        UnicodeEncoding utf = new UnicodeEncoding(); // convert byte array to string

        //        return utf.GetString(arr).Trim();
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //    finally
        //    {
        //        if(stream != null) stream.Close();
        //        if(writer != null) writer.Close();
        //    }
        //}

        #endregion

        /// <summary>
        /// Deserialise a WebRooms RwrCheckServer Message into a string
        /// </summary>
        /// <param name="thisRWRCheckServerRequest">WebRooms SetAvailability</param>
        /// <returns>Deserialised XML</returns>
        public string RWRCheckServerRequestXml(Comms.RWRCheckServerResult thisRWRCheckServerRequest)
        {
            MemoryStream stream = null;
            TextWriter writer = null;
            try
            {
                stream = new MemoryStream(); // read xml in memory

                writer = new StreamWriter(stream, Encoding.Unicode);
                // get serialise object

                XmlSerializer serializer = new XmlSerializer(typeof(Comms.RWRCheckServerResult));
                serializer.Serialize(writer, thisRWRCheckServerRequest); // read object

                int count = (int)stream.Length; // saves object in memory stream

                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                // copy stream contents in byte array

                stream.Read(arr, 0, count);
                UnicodeEncoding utf = new UnicodeEncoding(); // convert byte array to string

                return utf.GetString(arr).Trim();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (stream != null) stream.Close();
                if (writer != null) writer.Close();
            }
        }


        /// <summary>
		/// Deserialise a WebRooms RwrCheckServer Message into a string
		/// </summary>
		/// <param name="thisSetAvailabilityRequest">WebRooms SetAvailability</param>
		/// <returns>Deserialised XML</returns>
		public string SetAvailabilityRequestXml(Comms.SetAvailabilityRequest thisSetAvailabilityRequest)
        {
            MemoryStream stream = null;
            TextWriter writer = null;
            try
            {
                stream = new MemoryStream(); // read xml in memory

                writer = new StreamWriter(stream, Encoding.Unicode);
                // get serialise object

                XmlSerializer serializer = new XmlSerializer(typeof(Comms.SetAvailabilityRequest));
                serializer.Serialize(writer, thisSetAvailabilityRequest); // read object

                int count = (int)stream.Length; // saves object in memory stream

                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                // copy stream contents in byte array

                stream.Read(arr, 0, count);
                UnicodeEncoding utf = new UnicodeEncoding(); // convert byte array to string

                return utf.GetString(arr).Trim();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (stream != null) stream.Close();
                if (writer != null) writer.Close();
            }
        }


        #endregion

    }
}
