using System;
using System.IO;
using Resources;
using Microsoft.Win32;
using System.Collections;
using System.Text;
using System.Security.Permissions;

namespace RhsSync
{
    /// <summary>
    /// Summary description for clsSetting.
    /// </summary>
    public class clsSetting : clsBase
    {

        #region Initialise Class

        /// <summary>
        /// clsSetting Initialiser
        /// </summary>
        public clsSetting()
        {
        }

        public int GetSettings(string thisProvidedRhsFolder)
        {
            int retVal = 0;
            errDescription = new clsWelmanError();
            thisRhsFolder = thisProvidedRhsFolder;

            bool haveAnRhsFolder = false;
            bool haveCredentials = false;
            string thisCurrentRhsFolderId = "";
            string thisCurrentRhsFolder = "";

            if (thisProvidedRhsFolder != "")
                haveAnRhsFolder = true;

            if (thisProvidedRhsFolder == "")
                return -1;

            #region Step 2) See if we can find a New Settings file in this folder

            haveCredentials = LoadSettingsFromFile(thisCurrentRhsFolder);
            if (haveCredentials)
            {
                if (Curl.Trim() == "" || Password.Trim() == "")
                    haveCredentials = false;
            }

            if (!haveCredentials)
                return -2;



            #endregion

            #region Get Room Types

            if (theseRhsRoomTypes == null)
                theseRhsRoomTypes = new ArrayList();

            NumRhsRoomTypes = theseRhsRoomTypes.Count;

            if (theseWebRoomTypes == null)
                theseWebRoomTypes = new ArrayList();

            NumWebRoomTypes = theseWebRoomTypes.Count;

            #endregion

            Save();

            return 1;
        }


        #endregion

        #region Publicly Exposed variables


        /// <summary>
        /// Indicates that the rhsDataFolder exists (but not weather it contains RHS Files)
        /// </summary>
        public bool rhsDataFolderExists = false;

        /// <summary>
        /// Indicates that the RhsFolder exists and contains RHS Files
        /// </summary>
        public bool rhsFolderIsValid = false;

        /// <summary>
        /// Indicates that the RHS Settings File exists
        /// </summary>
        public bool rhsSettingsFileExists = false;

        /// <summary>
        /// Indicates that the RHS Settings File exists
        /// </summary>
        public bool credentialsAreValid = false;

        /// <summary>
        /// The RHS Room Types 
        /// </summary>
        public ArrayList theseRhsRoomTypes;

        /// <summary>
        /// The Web Room Types 
        /// </summary>
        public ArrayList theseWebRoomTypes;

        /// <summary>
        /// Internal version of the 'Base RHS Folder'
        /// </summary>
        public string thisRhsFolder = "";

        /// <summary>
        /// Internal version of the 'OnlineBookingsFolder'
        /// </summary>
        public string thisOnlineBookingsFolder = "";

        /// <summary>
        /// Whether to add a booking into RHS or not
        /// </summary>
        public bool thisAddOnlineBookingsIntoRhs;

        /// <summary>
        /// Whether to update Website Availability or not
        /// </summary>
        public bool thisAddOverbookingProtection;

        /// <summary>
        /// Whether complete availability is required or not
        /// </summary>
        public bool thisCompleteAvailabilityRequired = true;

        /// <summary>
        /// Behaviour of Confirmed/Unconfirmed Bookings
        /// </summary>
        public int thisConfirmedBookingBehaviour = 0;

        /// <summary>
		/// Webrooms URL
		/// </summary>
		public string thisCurl;

        /// <summary>
        /// Remote Support Invites
        /// </summary>
        public string thisRemoteSupportInvites;

        /// <summary>
        /// The LastRemoteInviteChange
        /// </summary>
        public DateTime thisLastRemoteInviteChange;

        /// <summary>
        /// The settings the application should to use
        /// </summary>
        public DateTime thisLastSettingsChange;

        /// <summary>
        /// Last Communication of any type
        /// </summary>
        public DateTime thisLastSuccessfulCommunication;

        /// <summary>
        /// Last availability Update
        /// </summary>
        public DateTime thisLastAvailabilityUpdate;

        /// <summary>
        /// Last Rhs Availability File Modify
        /// </summary>
        public DateTime thisLastRhsAvailabilityFileModify;

        /// <summary>
        /// The LastWebRoomTypeChange
        /// </summary>
        public DateTime thisLastWebRoomTypeChange;

        /// <summary>
        /// Last Booking Received
        /// </summary>
        public DateTime thisLastBookingReceived;

        /// <summary>
        /// Lookahead Days for Updates
        /// </summary>
        public int thisLookahead = 0;

        /// <summary>
        /// Whether to put bookings with multiple rooms in the same reservation or not
        /// </summary>
        public bool thisMultiRoomBookingsInSameReservation;

        /// <summary>
        /// Number of RHS Room Types
        /// </summary>
        public int thisNumRhsRoomTypes = 0;

        /// <summary>
        /// Number of Web Room Types
        /// </summary>
        public int thisNumWebRoomTypes = 0;

        /// <summary>
        /// Webrooms Updates Password 
        /// </summary>
        public string thisPassword;

        /// <summary>
        /// Reference Date
        /// </summary>
        public DateTime thisRefDate;

        /// <summary>
        /// Reference Date at the lLast Update
        /// </summary>
        public DateTime thisRefDateLastUpdate;

        /// <summary>
        /// Indicates the number of availabilities updated
        /// </summary>
        public int availabilitiesUpdated;

        /// <summary>
        /// Minimum Interval between updating the Website
        /// </summary>
        public long thisServerCommunicationInterval; //interval in milli seconds

        /// <summary>
        /// Whether to put bookings with multiple rooms in the same reservation or not
        /// </summary>
        public bool thisUpdateUnmapped;

        /// <summary>
        /// Whether to update Website Availability or not
        /// </summary>
        public bool thisUpdateWebsiteAvailability;

        /// <summary>
        /// URL from which Data is received from the Server
        /// </summary>
        public string thisWebsiteRecieveUrl = "";

        /// <summary>
        /// URL to which Data is sent to the Server
        /// </summary>
        public string thisWebsiteSendUrl = "";

        /// <summary>
        /// Indicates if the success or otherwise of the operation
        /// </summary>
        public bool requestSucceeded;

        /// <summary>
        /// Supplies the details of any issues encountered while performing the 
        /// operation 
        /// </summary>
        public clsWelmanError errDescription = new clsWelmanError();

        #endregion

        #region Availability variables
        //		#region clsRhsAvailability

        //		/// <summary>
        //		/// Holds all the data captured from the RHS Database
        //		/// </summary>
        //		private struct rhsDataInstanceStruct
        //		{
        /// <summary>
        /// Array that holds availability and occupancy for each roomtype for each day.
        /// First index is roomTypeCounter, second is dayCounter
        /// </summary>
        public roomTypeStatusStruct[,] availabilityAndOccupancy;

        /// <summary>
		/// Number of RHS Room Types
		/// </summary>
		private long numRoomTypes;
        /// <summary>
        /// Number of RHS Rooms
        /// </summary>
        public long numRooms;
        /// <summary>
        /// Number of 'Tertiary Records'
        /// </summary>
        private long numTertiaryRecords;
        /// <summary>
        /// Highest Room Number
        /// </summary>
        private long highestRoomNumber;
        /// <summary>
        /// RHS Value of first day
        /// </summary>
        public long firstDayValue;
        /// <summary>
        /// Number of days that the RHS databse spans
        /// </summary>
        public long numDays;

        /// <summary>
        /// The time of the last availability change
        /// </summary>
        public DateTime lastAvailabilityChange;

        /// <summary>
        /// RHS Room Type Data
        /// </summary>
        public ArrayList roomTypeData;
        /// <summary>
        /// Data about each RHS Room
        /// </summary>
        public ArrayList roomData;
        /// <summary>
        /// Data from the Primary Booking File
        /// </summary>
        public ArrayList primaryBookingData;
        /// <summary>
        /// Data from the Secondary Booking File
        /// </summary>
        public ArrayList secondaryBookingData;
        /// <summary>
        /// Data from the Tertiary Booking File
        /// </summary>
        public ArrayList tertiaryBookingData;
        /// <summary>
        /// Data about each RHS Room0
        /// </summary>
        public ArrayList roomData0;
        //		}


        #endregion

        #region Constants

        /// <summary>
        /// The Maximum number of RHS or Web Room types the system can handle
        /// </summary>
        public const int MaxRoomTypes = 100;


        /// <summary>
        /// Default RHS Folder
        /// </summary>
        public const string defaultRhsFolder = @"C:\RHS\";

        /// <summary>
        /// Sub Folder of RHS where RHS Databse Files are found 
        /// </summary>
        public const string dataSubFolder = @"DATA\";

        /// <summary>
        /// Sub Folder of RHS where Web Bookings are kept
        /// </summary>
        public const string onlineBookingsFolder = @"WEB\";


        /// <summary>
        /// File where RHS Webrooms Updater Settings are stored
        /// </summary>
        public string OldSettingsFile = "RHSWebRoomsSettings" + rhsWebRoomsExtension;

        /// <summary>
        /// File where RHS Webrooms Updater Settings are stored
        /// </summary>
        public string NewSettingsFile = "RWRSettings" + rhsWebRoomsExtension;

        /// <summary>
        /// Registry Key where the time of the Last Successful Communication of any kind with the Welman Server is stored
        /// </summary>
        public const string CurrentRhsFolderIdKeyName = "CurrentRhsFolderId";

        /// <summary>
        /// Registry Key where the RHS Folder is stored
        /// </summary>
        public const string RhsFolderKeyName = "RhsFolder";

        /// <summary>
        /// Registry Key where the AddOnlineBookingsIntoRhs value is stored
        /// </summary>
        public const string AddOnlineBookingsIntoRhsKeyName = "AddOnlineBookingsIntoRhs";

        /// <summary>
        /// Registry Key where the AddOverbookingProtection value is stored
        /// </summary>
        public const string AddOverbookingProtectionKeyName = "AddOverbookingProtection";

        /// <summary>
        /// Registry Key where the CompleteAvailabilityRequired value is stored
        /// </summary>
        public const string CompleteAvailabilityRequiredKeyName = "CompleteAvailabilityRequired";


        /// <summary>
        /// Registry Key where the ConfirmedBookingBehaviour value is stored
        /// </summary>
        public const string ConfirmedBookingBehaviourKeyName = "ConfirmedBookingBehaviour";

        /// <summary>
        /// Registry Key where the Curl value is stored
        /// </summary>
        public const string CurlKeyName = "Curl";

        /// <summary>
        /// Registry Key where the RemoteSupportInvites value is stored
        /// </summary>
        public const string RemoteSupportInvitesKeyName = "RemoteSupportInvites";

        /// <summary>
        /// Registry Key where the RHS Webrooms Updater Settings are stored
        /// </summary>
        public const string OldSettingsKeyName = "settings";

        //		/// <summary>
        //		/// Registry Key where the Welman Server to connect to is kept.
        //		/// <note type="implementnotes">This key is only set used for development and Beta versions</note>
        //		/// </summary>
        //		public const string ServerSiteKeyName = "serversite";

        /// <summary>
        /// Registry Key where the time of the Last Availability Update is stored
        /// </summary>
        public const string LastAvailabilityUpdateKeyName = "LastAvailabilityUpdate";

        /// <summary>
        /// Registry Key where the time of the Last Booking Received is stored
        /// </summary>
        public const string LastBookingReceivedKeyName = "LastBookingReceived";

        /// <summary>
        /// Registry Key where the time of the LastRemoteInviteChangeKeyName of any kind with the Welman Server is stored
        /// </summary>
        public const string LastRemoteInviteChangeKeyName = "LastRemoteInviteChange";

        /// <summary>
        /// Registry Key where the time of the Last RHS Availability File Modify is stored
        /// </summary>
        public const string LastRhsAvailabilityFileModifyKeyName = "LastRhsAvailabilityFileModify";

        /// <summary>
        /// Registry Key where the time of the Last Successful Communication of any kind with the Welman Server is stored
        /// </summary>
        public const string LastSuccessfulCommunicationKeyName = "LastSuccessfulCommunication";

        /// <summary>
        /// Registry Key where the time of the Last Settings Change is stored
        /// </summary>
        public const string LastSettingsChangeKeyName = "LastSettingsChange";

        /// <summary>
        /// Registry Key where the time of the LastWebRoomTypeChange of any kind with the Welman Server is stored
        /// </summary>
        public const string LastWebRoomTypeChangeKeyName = "LastWebRoomTypeChange";

        /// <summary>
        /// Registry Key where the ListboxHandle is stored
        /// </summary>
        public const string ListboxHandleKeyName = "ListboxHandle";

        /// <summary>
        /// Registry Key where the LookAhead is stored
        /// </summary>
        public const string LookAheadKeyName = "LookAhead";

        /// <summary>
        /// Registry Key where the MultiRoomBookingsInSameReservation value is stored
        /// </summary>
        public const string MultiRoomBookingsInSameReservationKeyName = "MultiRoomBookingsInSameReservation";

        /// <summary>
        /// Registry Key where the NumRhsRoomTypes is stored
        /// </summary>
        public const string NumRhsRoomTypesKeyName = "NumRhsRoomTypes";

        /// <summary>
        /// Registry Key where the NumWebRoomTypes is stored
        /// </summary>
        public const string NumWebRoomTypesKeyName = "NumWebRoomTypes";

        /// <summary>
        /// Registry Key where the RefDate is stored
        /// </summary>
        public const string RefDateKeyName = "RefDate";

        /// <summary>
        /// Registry Key where the RefDateLastUpdate is stored
        /// </summary>
        public const string RefDateLastUpdateKeyName = "RefDateLastUpdate";

        /// <summary>
        /// Registry Key where the SiteId is stored
        /// </summary>
        public const string SiteIdKeyName = "SiteId";

        /// <summary>
        /// Registry Key where the Password is stored
        /// </summary>
        public const string PasswordKeyName = "Password";

        /// <summary>
        /// Registry Key where the Server Communication Interval is stored
        /// </summary>
        public const string ServerCommunicationIntervalKeyName = "ServerCommunicationInterval";

        /// <summary>
        /// Registry Key where the Update Website Availability is stored
        /// </summary>
        public const string UpdateWebsiteAvailabilityKeyName = "UpdateWebsiteAvailability";

        /// <summary>
        /// Registry Key where the UpdateUnmapped value is stored
        /// </summary>
        public const string UpdateUnmappedKeyName = "UpdateUnmapped";

        #endregion

        #region New Get/Sets that are not Registry centric

        #region Get RhsDataFolder

        /// <summary>
        /// Returns the RHS folder of the current computer.
        /// If the key is not found, the function returns null.
        /// </summary>
        /// <returns>The name of the RHS folder or Null if unable to get RHS folder</returns>
        public string RhsDataFolder
        {
            get
            {
                return thisRhsFolder + dataSubFolder;
            }
        }

        #endregion

        #region Get OnlineBookingsFolder

        /// <summary>
        /// Returns the RHS folderof the current computer.
        /// If the key is not found, the function returns null.
        /// </summary>
        /// <returns>The name of the RHS folder or Null if unable to get RHS folder</returns>
        public string OnlineBookingsFolder
        {
            get
            {
                //string temp = defaultRhsFolder; //Assume This folder By default
                //thisOnlineBookingsFolder = temp;

                //if (!thisOnlineBookingsFolder.EndsWith(@"\"))
                //    thisOnlineBookingsFolder += @"\";

                //if (thisOnlineBookingsFolder.EndsWith(@"Data\"))
                //    thisOnlineBookingsFolder = thisOnlineBookingsFolder.Substring(thisOnlineBookingsFolder.Length - 5);

                //thisOnlineBookingsFolder += onlineBookingsFolder;

                //return thisOnlineBookingsFolder;

                return thisRhsFolder + onlineBookingsFolder;
            }
        }

        #endregion

        #region Get/Set AddOnlineBookingsIntoRhs

        /// <summary>
        /// Returns the Number of AddOnlineBookingsIntoRhs Days
        /// </summary>
        /// <returns>The AddOnlineBookingsIntoRhs or Empty String if unable to get RHS folder</returns>
        public bool AddOnlineBookingsIntoRhs
        {
            get
            {
                thisAddOnlineBookingsIntoRhs = true;
                return thisAddOnlineBookingsIntoRhs;
            }
            set
            {
                if (value != true
                    || thisAddOnlineBookingsIntoRhs != true)
                {
                    thisAddOnlineBookingsIntoRhs = true;
                    LastSettingsChange = DateTime.Now;
                }
                thisAddOnlineBookingsIntoRhs = true;
            }

        }

        #endregion

        #region Get/Set AddOverbookingProtection

        /// <summary>
        /// Returns the Number of AddOverbookingProtection Days
        /// </summary>
        /// <returns>The AddOverbookingProtection or Empty String if unable to get RHS folder</returns>
        public bool AddOverbookingProtection
        {
            get
            {
                return thisAddOverbookingProtection;
            }
            set
            {
                if (value != thisAddOverbookingProtection)
                {
                    thisAddOverbookingProtection = value;
                    LastSettingsChange = DateTime.Now;
                }

            }

        }

        #endregion

        #region Get/Set CompleteAvailabilityRequired

        /// <summary>
        /// Returns the Number of CompleteAvailabilityRequired Days
        /// </summary>
        /// <returns>The CompleteAvailabilityRequired or Empty String if unable to get RHS folder</returns>
        public bool CompleteAvailabilityRequired
        {
            get
            {
                return thisCompleteAvailabilityRequired;
            }
            set
            {
                if (value != thisCompleteAvailabilityRequired)
                {
                    thisCompleteAvailabilityRequired = value;
                }

            }

        }

        #endregion

        #region Get/Set ConfirmedBookingBehaviour

        /// <summary>
        /// Returns the Number of ConfirmedBookingBehaviour Days
        /// </summary>
        /// <returns>The ConfirmedBookingBehaviour or Empty String if unable to get RHS folder</returns>
        public int ConfirmedBookingBehaviour
        {
            get
            {
                return thisConfirmedBookingBehaviour;
            }
            set
            {
                if (value != thisConfirmedBookingBehaviour)
                {
                    thisConfirmedBookingBehaviour = value;
                }

            }

        }

        #endregion

        #region Get/Set Curl

        /// <summary>
        /// Returns the Curl
        /// </summary>
        /// <returns>The Curl or Empty String if no Curl</returns>
        public string Curl
        {
            get
            {
                return thisCurl;
            }
            set
            {
                if (value != thisCurl)
                {
                    thisCurl = value;
                    LastSettingsChange = DateTime.Now;
                }

            }

        }

        #endregion

        #region Get/Set LastAvailabilityUpdate

        /// <summary>
        /// Returns the LastAvailabilityUpdate
        /// </summary>
        /// <returns>The LastAvailabilityUpdate or 1st January 1AD if unable to retrieve</returns>
        public DateTime LastAvailabilityUpdate
        {
            get
            {
                return thisLastAvailabilityUpdate;
            }
            set
            {
                if (value != thisLastAvailabilityUpdate)
                {
                    thisLastAvailabilityUpdate = value;
                }
            }

        }

        #endregion

        #region Get/Set LastBookingReceived

        /// <summary>
        /// Returns the Number of LastBookingReceived Days
        /// </summary>
        /// <returns>The LastBookingReceived or Empty String if unable to get RHS folder</returns>
        public DateTime LastBookingReceived
        {
            get
            {
                return thisLastBookingReceived;
            }
            set
            {
                if (value != thisLastBookingReceived)
                {
                    thisLastBookingReceived = value;
                }
            }

        }

        #endregion

        #region Get/Set LastRhsAvailabilityFileModify

        /// <summary>
        /// Returns the Number of LastRhsAvailabilityFileModify Days
        /// </summary>
        /// <returns>The LastRhsAvailabilityFileModify or Empty String if unable to get RHS folder</returns>
        public DateTime LastRhsAvailabilityFileModify
        {
            get
            {
                return thisLastRhsAvailabilityFileModify;
            }
            set
            {
                if (value != thisLastRhsAvailabilityFileModify)
                {
                    thisLastRhsAvailabilityFileModify = value;
                }
            }

        }

        #endregion

        #region Get/Set LastRemoteInviteChange

        /// <summary>
        /// Returns the Number of LastRemoteInviteChange Days
        /// </summary>
        /// <returns>The LastRemoteInviteChange or Empty String if unable to get RHS folder</returns>
        public DateTime LastRemoteInviteChange
        {
            get
            {
                return thisLastRemoteInviteChange;
            }
            set
            {
                //                string currentFunction = "Set LastRemoteInviteChange";

                if (value != thisLastRemoteInviteChange)
                {
                    thisLastRemoteInviteChange = value;

                    WriteLastCommunicationFile(value);
                }
            }

        }

        #endregion

        #region Get/Set LastSettingsChange

        /// <summary>
        /// Returns the Number of LastSettingsChange Days
        /// </summary>
        /// <returns>The LastSettingsChange or Empty String if unable to get RHS folder</returns>
        public DateTime LastSettingsChange
        {
            get
            {
                return thisLastSettingsChange;
            }
            set
            {
                thisLastSettingsChange = value;
            }

        }

        #endregion

        #region Get/Set LastSuccessfulCommunication

        /// <summary>
        /// Returns the Number of LastSuccessfulCommunication Days
        /// </summary>
        /// <returns>The LastSuccessfulCommunication or Empty String if unable to get RHS folder</returns>
        public DateTime LastSuccessfulCommunication
        {
            get
            {
                return thisLastSuccessfulCommunication;
            }
            set
            {
                //                string currentFunction = "Set LastSuccessfulCommunication";

                if (value != thisLastSuccessfulCommunication)
                {
                    thisLastSuccessfulCommunication = value;

                    WriteLastCommunicationFile(value);

                }
            }

        }

        #endregion

        #region Get/Set LastWebRoomTypeChange

        /// <summary>
        /// Returns the Number of LastWebRoomTypeChange Days
        /// </summary>
        /// <returns>The LastWebRoomTypeChange or Empty String if unable to get RHS folder</returns>
        public DateTime LastWebRoomTypeChange
        {
            get
            {
                return thisLastWebRoomTypeChange;
            }
            set
            {
                //                string currentFunction = "Set LastWebRoomTypeChange";

                if (value != thisLastWebRoomTypeChange)
                {
                    thisLastWebRoomTypeChange = value;

                    WriteLastCommunicationFile(value);

                }
            }

        }

        #endregion

        #region Get/Set Lookahead

        /// <summary>
        /// Returns the Number of Lookahead Days
        /// </summary>
        /// <returns>The Lookahead or Empty String if unable to get RHS folder</returns>
        public int Lookahead
        {
            get
            {
                return thisLookahead;
            }
            set
            {
                if (value != thisLookahead)
                {
                    thisLookahead = value;

                    LastSettingsChange = DateTime.Now;

                }

            }

        }

        #endregion

        #region Get/Set MultiRoomBookingsInSameReservation

        /// <summary>
        /// Returns the MultiRoomBookingsInSameReservation
        /// </summary>
        /// <returns>The MultiRoomBookingsInSameReservation or Empty String if unable to get RHS folder</returns>
        public bool MultiRoomBookingsInSameReservation
        {
            get
            {
                return thisMultiRoomBookingsInSameReservation;
            }
            set
            {
                if (value != thisMultiRoomBookingsInSameReservation)
                {
                    thisMultiRoomBookingsInSameReservation = value;
                }

            }

        }

        #endregion

        #region Get/Set NumRhsRoomTypes

        /// <summary>
        /// Returns the Number of NumRhsRoomTypes Days
        /// </summary>
        /// <returns>The NumRhsRoomTypes or Empty String if unable to get RHS folder</returns>
        public int NumRhsRoomTypes
        {
            get
            {
                return thisNumRhsRoomTypes;
            }
            set
            {
                if (value != thisNumRhsRoomTypes)
                {
                    thisNumRhsRoomTypes = value;
                    LastSettingsChange = DateTime.Now;
                }

            }

        }

        #endregion

        #region Get/Set NumWebRoomTypes

        /// <summary>
        /// Returns the Number of NumWebRoomTypes Days
        /// </summary>
        /// <returns>The NumWebRoomTypes or Empty String if unable to get RHS folder</returns>
        public int NumWebRoomTypes
        {
            get
            {
                return thisNumWebRoomTypes;
            }
            set
            {
                if (value != thisNumWebRoomTypes)
                {
                    thisNumWebRoomTypes = value;
                    LastSettingsChange = DateTime.Now;
                }

            }

        }

        #endregion

        #region Get/Set Password

        /// <summary>
        /// Returns the Number of Password Days
        /// </summary>
        /// <returns>The Password or Empty String if unable to get RHS folder</returns>
        public string Password
        {
            get
            {
                return thisPassword;
            }
            set
            {
                if (value != thisPassword)
                {
                    thisPassword = value;
                    LastSettingsChange = DateTime.Now;
                }

            }

        }

        #endregion

        #region Get/Set RefDate

        /// <summary>
        /// Returns the RefDate
        /// </summary>
        /// <returns>The RefDate or 1st January 1AD if unable to return anything</returns>
        public DateTime RefDate
        {
            get
            {
                return thisRefDate;
            }
            set
            {
                if (value != thisRefDate)
                {
                    thisRefDate = value;
                    LastSettingsChange = DateTime.Now;
                }
            }

        }

        #endregion

        #region Get/Set RefDateLastUpdate

        /// <summary>
        /// Returns the RefDateLastUpdate
        /// </summary>
        /// <returns>The RefDateLastUpdate or 1st January 1AD if unable to return anything</returns>
        public DateTime RefDateLastUpdate
        {
            get
            {
                return thisRefDateLastUpdate;
            }
            set
            {
                if (value != thisRefDateLastUpdate)
                {
                    thisRefDateLastUpdate = value;
                    LastSettingsChange = DateTime.Now;
                }
            }

        }

        #endregion

        #region Get/Set RemoteSupportInvites

        /// <summary>
        /// Returns the Number of RemoteSupportInvites Days
        /// </summary>
        /// <returns>The RemoteSupportInvites or Empty String if unable to get RHS folder</returns>
        public string RemoteSupportInvites
        {
            get
            {
                return thisRemoteSupportInvites;
            }
            set
            {
                if (value != thisRemoteSupportInvites)
                {
                    thisRemoteSupportInvites = value;
                    LastSettingsChange = DateTime.Now;
                }

            }

        }

        #endregion

        #region Get/Set ServerCommunicationInterval

        /// <summary>
        /// Returns the ServerCommunicationInterval in milliseconds
        /// </summary>
        /// <returns>The ServerCommunicationInterval</returns>
        public long ServerCommunicationInterval
        {
            get
            {
                int minimum = 1 * 60 * 1000;
                if (thisServerCommunicationInterval < minimum)
                    thisServerCommunicationInterval = minimum;

                return thisServerCommunicationInterval;
            }
            set
            {
                if (value != thisServerCommunicationInterval)
                {
                    thisServerCommunicationInterval = value;
                    LastSettingsChange = DateTime.Now;
                }

            }

        }

        #endregion

        #region Get/Set UpdateWebsiteAvailability

        /// <summary>
        /// Returns the Number of UpdateWebsiteAvailability Days
        /// </summary>
        /// <returns>The UpdateWebsiteAvailability or Empty String if unable to get RHS folder</returns>
        public bool UpdateWebsiteAvailability
        {
            get
            {
                return thisUpdateWebsiteAvailability;
            }
            set
            {
                if (value != thisUpdateWebsiteAvailability)
                {
                    thisUpdateWebsiteAvailability = value;
                }

            }

        }

        #endregion

        #region Get/Set UpdateUnmapped

        /// <summary>
        /// Returns the Number of UpdateUnmapped Days
        /// </summary>
        /// <returns>The UpdateUnmapped or Empty String if unable to get RHS folder</returns>
        public bool UpdateUnmapped
        {
            get
            {
                return thisUpdateUnmapped;
            }
            set
            {
                thisUpdateUnmapped = value;
                LastSettingsChange = DateTime.Now;
            }

        }

        #endregion

        #region Get/Set WebBookingsToAcknowledge

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

                for (int counter = 0; counter < WebBookingsToAcknowledge.Count; counter++)
                    thisRetVal += ((int)WebBookingsToAcknowledge[counter]).ToString() + ";";

                return thisRetVal;
            }
            set
            {
                string thisList = value;
                WebBookingsToAcknowledge = new ArrayList();
                int semiColonIndex = thisList.IndexOf(";");
                while (semiColonIndex > 0)
                {
                    string thisRhsRoomType = thisList.Substring(0, semiColonIndex);

                    if (isNumerical(thisRhsRoomType))
                        WebBookingsToAcknowledge.Add(Convert.ToInt32(thisRhsRoomType));
                    thisList = thisList.Substring(semiColonIndex + 1);
                    semiColonIndex = thisList.IndexOf(";");
                }
            }
        }

        #endregion

        #region SetRhsRoomType

        /// <summary>
        /// Sets a RhsRoomType. Includes updating the appropriate Values in the Registry
        /// </summary>
        /// <param name="roomTypeId">roomTypeId</param>
        /// <param name="roomTypeName">roomTypeName</param>
        /// <param name="roomTypeCode">roomTypeCode</param>
        /// <param name="numberOfRooms">numberOfRooms</param>
        /// <param name="affectOccupancy">affectOccupancy</param>
        /// <param name="AvailabilityAsDelimitedList">AvailabilityAsDelimitedList</param>
        /// <param name="AvailabilityAtTimeOfLastUpdateAsDelimitedList">AvailabilityAtTimeOfLastUpdateAsDelimitedList</param>
        public bool SetRhsRoomType(int roomTypeId,
            string roomTypeName,
            string roomTypeCode,
            int numberOfRooms,
            int affectOccupancy,
            string AvailabilityAsDelimitedList,
            string AvailabilityAtTimeOfLastUpdateAsDelimitedList)
        {
            //Sets and Rhs Room Type in the Registry
            clsRhsRoomType thisRhsRoomType = new clsRhsRoomType();
            thisRhsRoomType.roomTypeId = roomTypeId;
            thisRhsRoomType.roomTypeName = roomTypeName;
            thisRhsRoomType.roomTypeCode = roomTypeCode;
            thisRhsRoomType.numberOfRooms = numberOfRooms;
            thisRhsRoomType.affectOccupancy = affectOccupancy;
            thisRhsRoomType.AvailabilityAsDelimitedList = AvailabilityAsDelimitedList;
            thisRhsRoomType.AvailabilityAtTimeOfLastUpdateAsDelimitedList = AvailabilityAtTimeOfLastUpdateAsDelimitedList;

            theseRhsRoomTypes.Add(thisRhsRoomType);
            int Index = theseRhsRoomTypes.Count - 1;

            return true;
        }

        #endregion

        #region SetWebRoomType

        /// <summary>
        /// Sets a WebRoomType. Includes updating the appropriate Values in the Registry
        /// </summary>
        /// <param name="webRoomTypeId">webRoomTypeId</param>
        /// <param name="webRoomTypeName">webRoomTypeName</param>
        /// <param name="IsInterConnecting">IsInterConnecting</param>
        /// <param name="RhsRoomTypesMappedToGroup1AsDelimitedList">RhsRoomTypesMappedToGroup1AsDelimitedList</param>
        /// <param name="RhsRoomTypesMappedToGroup2AsDelimitedList">RhsRoomTypesMappedToGroup2AsDelimitedList</param>
        /// <param name="maxNumberOfRooms">maxNumberOfRooms</param>
        /// <param name="AvailabilityAsDelimitedList">AvailabilityAsDelimitedList</param>
        /// <param name="AvailabilityAtTimeOfLastUpdateAsDelimitedList">AvailabilityAtTimeOfLastUpdateAsDelimitedList</param>
        public bool SetWebRoomType(int webRoomTypeId,
            string webRoomTypeName,
            int IsInterConnecting,
            string RhsRoomTypesMappedToGroup1AsDelimitedList,
            string RhsRoomTypesMappedToGroup2AsDelimitedList,
            int maxNumberOfRooms,
            string AvailabilityAsDelimitedList,
            string AvailabilityAtTimeOfLastUpdateAsDelimitedList)
        {
            //Sets and Web Room Type in the Registry
            clsWebRoomType thisWebRoomType = new clsWebRoomType();
            thisWebRoomType.webRoomTypeId = webRoomTypeId;

            if (webRoomTypeId == 0)
                return false;

            thisWebRoomType.webRoomTypeName = webRoomTypeName;
            thisWebRoomType.IsInterConnecting = IsInterConnecting;
            thisWebRoomType.RhsRoomTypesMappedToGroup1AsDelimitedList = RhsRoomTypesMappedToGroup1AsDelimitedList;
            thisWebRoomType.RhsRoomTypesMappedToGroup2AsDelimitedList = RhsRoomTypesMappedToGroup2AsDelimitedList;
            thisWebRoomType.maxNumberOfRooms = maxNumberOfRooms;
            thisWebRoomType.AvailabilityAsDelimitedList = AvailabilityAsDelimitedList;
            thisWebRoomType.AvailabilityAtTimeOfLastUpdateAsDelimitedList = AvailabilityAtTimeOfLastUpdateAsDelimitedList;

            theseWebRoomTypes.Add(thisWebRoomType);
            int Index = theseWebRoomTypes.Count - 1;

            return true;
        }


        #endregion

        #region Write to the Last Communication File

        /// <summary>
        /// WriteLastCommunicationFile
        /// </summary>
        /// <param name="thisDateTimeToWrite">thisDateTimeToWrite</param>
        public void WriteLastCommunicationFile(DateTime thisDateTimeToWrite)
        {
            if (thisDateTimeToWrite > new DateTime(2016, 1, 1))
            {
                #region Write to the Last Communication File, So RHS knows we've communicated

                string currentFunction = "WriteLastCommunicationFile";

                StreamWriter lastCommsFileWriter;

                try
                {
                    lastCommsFileWriter = new StreamWriter(RhsDataFolder + lastCommunicationsFile, false);
                }
                catch (System.UnauthorizedAccessException e)
                {
                    LogThis(DateTime.Now, "Error Opening Last Communications File",
                        e.ToString() + e.Message + e.StackTrace, currentFunction, RhsDataFolder + lastCommunicationsFile);

                    try
                    {
                        lastCommsFileWriter = null;
                        System.IO.File.Delete(RhsDataFolder + lastCommunicationsFile);
                    }
                    catch (System.Exception e2)
                    {
                        LogThis(DateTime.Now, "Error Deleting Last Communications File",
                            e2.ToString() + e2.Message + e2.StackTrace, currentFunction, RhsDataFolder + lastCommunicationsFile);

                    }

                    return;
                }
                catch (System.Exception e)
                {
                    LogThis(DateTime.Now, "Error Opening Last Communications File",
                        e.ToString() + e.Message + e.StackTrace, currentFunction, RhsDataFolder + lastCommunicationsFile);

                    try
                    {
                        lastCommsFileWriter = null;
                        System.IO.File.Delete(RhsDataFolder + lastCommunicationsFile);
                    }
                    catch (System.Exception e2)
                    {
                        LogThis(DateTime.Now, "Error Deleting Last Communications File",
                            e2.ToString() + e2.Message + e2.StackTrace, currentFunction, RhsDataFolder + lastCommunicationsFile);

                    }

                    return;
                }

                string temp = thisDateTimeToWrite.Hour.ToString().Trim();
                if (temp.Length == 1)
                    temp = "0" + temp;

                string lastCommunication = temp + ":";

                temp = thisDateTimeToWrite.Minute.ToString().Trim();
                if (temp.Length == 1)
                    temp = "0" + temp;

                lastCommunication += temp + " ";

                temp = thisDateTimeToWrite.Day.ToString().Trim();
                if (temp.Length == 1)
                    temp = "0" + temp;

                lastCommunication += temp + "/";

                temp = thisDateTimeToWrite.Month.ToString().Trim();
                if (temp.Length == 1)
                    temp = "0" + temp;

                lastCommunication += temp + "/";

                temp = (thisDateTimeToWrite.Year - 2000).ToString().Trim();
                if (temp.Length == 1)
                    temp = "0" + temp;

                lastCommunication += temp + aCrLf;

                #region Actually Write file here

                try
                {
                    lastCommsFileWriter.Write(lastCommunication);
                }
                catch (System.UnauthorizedAccessException e)
                {
                    LogThis(DateTime.Now, "Error Writing to Last Communications File (System.UnauthorizedAccessException)",
                        e.ToString() + e.Message + e.StackTrace, currentFunction, RhsDataFolder + lastCommunicationsFile + " Data: " + lastCommunication);
                    return;
                }
                catch (System.Exception e)
                {
                    LogThis(DateTime.Now, "Error Writing to Settings file",
                        e.ToString() + e.Message + e.StackTrace, currentFunction, RhsDataFolder + lastCommunicationsFile + " Data: " + lastCommunication);
                    return;
                }

                lastCommsFileWriter.Close();

                #endregion

                #endregion
            }
        }

        #endregion

        #endregion

        #region Logging

        /// <summary>
        /// Delegate that alerts an application when something has been logged
        /// </summary>
        public new delegate void DelLogThis(DateTime entryDate, string userTitle, string userDescription, string functionName, string nonPublicText);

        /// <summary>
        /// Event thrown for the Delegate <see cref="DelLogThis">DelLogThis</see>
        /// </summary>		
        public new event DelLogThis LogThis;

        #endregion

        #region Auxillary Mathods for Obtaining Settings

        #region containsSettingsFile (Checks for Settings file in the folder)

        /// <summary>
        /// Determines if the folder contains a valid Welamn Webrooms settings file or not
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        private bool containsSettingsFile(string folderName)
        {
            string folderToCheck = folderName;
            if (folderToCheck.EndsWith(clsSetting.dataSubFolder))
                folderToCheck = folderToCheck.Substring(0, folderToCheck.Length - clsSetting.dataSubFolder.Length - 1);

            if (File.Exists(folderToCheck + @"\" + OldSettingsFile))
                return true;
            else
                return false;
        }

        #endregion

        #region Check Presence of RHS Database files

        /// <summary>
        /// This method tries to establish the presence of database files 
        /// in the specified thisSetting.internalthisSetting.RhsFolder, and establish a connection to them. 
        /// If this is not possible, this method ascertains why not.
        /// This method follows the procedure:
        /// <list type="bullet">
        /// <item><description>
        /// Establish if thisSetting.internalthisSetting.RhsFolder is a well formed windows path. 
        /// If not it will return the error code pathNotWellFormed with 
        /// thisSetting.internalthisSetting.RhsFolder as the moreInfo parameter
        /// </description></item>
        /// <item><description>
        /// Establish if thisSetting.internalthisSetting.RhsFolder is on a local computer 
        /// (using a “\\” vs. “drive:\” start). Note that if a mapped network 
        /// drive is used the system will not necessarily pick this up.
        /// </description></item>
        /// <item><description>
        /// If thisSetting.internalthisSetting.RhsFolder is not ‘local’ attempt to connect to the 
        /// computer. If unsuccessful, it will return the error code 
        /// computerNotFound with the computer name as the moreInfo parameter.
        /// </description></item>
        /// <item><description>
        /// Establish if the folder exists. If it does not, it will return 
        /// the error code folderNotFound with the folder name as the moreInfo 
        /// parameter.
        /// </description></item>
        /// <item><description>
        /// Establish if all the component RHS files that make up a database 
        /// exist. If any do not, it will return the error code fileNotFound 
        /// with each unfound file name aComma separated in the moreInfo parameter.
        /// </description></item>
        /// <item><description>
        /// Connect to each of the files individually. If any connection 
        /// attempts are unsuccessful, it will return the error code cantReadfile 
        /// with the file name as the moreInfo parameter.
        /// </description></item>
        /// <item><description>
        /// Disconnect from each of the files individually.
        /// </description></item>
        /// <item><description>
        /// If this has been successfully negotiated, the method returns with 
        /// the code SuccessNoError and the empty string as the moreInfo parameter.
        /// </description></item></list></summary>
        /// <returns>Stucture indicating success or failure and reason for failure</returns>
        private bool CheckRhsFilesPresent(string thisFolder)
        {
            if (thisFolder == "")
                thisFolder = thisRhsFolder;

            PopulateRhsFiles();

            rhsDataFolderExists = false;
            rhsFolderIsValid = false;

            //Add a trailing slash if there is not one already
            if (!thisFolder.EndsWith(@"\"))
                thisFolder += @"\";

            if (!thisFolder.EndsWith(dataSubFolder))
                thisFolder += dataSubFolder;


            bool result = false;

            #region Attempt to find this path

            if (!Directory.Exists(thisFolder))
            {
                rhsDataFolderExists = false;
                errDescription.errorForUser = thisFolder + " not found";
                return result;
            }
            else
                rhsDataFolderExists = true;

            #endregion

            result = true;  //Start optimistic

            #region Determine if the required RHS files are there 

            for (int counter = 1; counter < 3; counter++)
            {
                if (!File.Exists(thisFolder + rhsFiles[counter].fileName))
                {
                    rhsFolderIsValid = false;
                    result = false;
                    errDescription.errorForUser += (", " + thisFolder + rhsFiles[counter].fileName);
                }
            }

            #endregion

            if (result)
                rhsFolderIsValid = true;
            else
                errDescription.errorForUser = "File(s) not found: " + errDescription.errorForUser;

            return result;
        }

        private bool CheckRhsFilesPresent()
        {
            return CheckRhsFilesPresent("");
        }

        #endregion

        #endregion

        #region Test Website Credentials

        /// <summary>
        /// Tests whether the supplied credentials work or not
        /// </summary>
        /// <returns></returns>
        public bool websiteCredentialsWork()
        {
            return websiteCredentialsWork(thisCurl, thisPassword);

        }


        /// <summary>
        /// Method that verifies if the specified Web Credentials are valid or not
        /// </summary>
        /// <param name="Curl">Curl</param>
        /// <param name="Password">Password</param>
        /// <returns>Boolean indicating Success or Failure.
        /// <note type="implementnotes"> In case of failure this class will also return the fact that
        /// there was an error  communicating with the Welman Server</note></returns>
        public bool websiteCredentialsWork(string Curl, string Password)
        {
            string currentFunction = "websiteCredentialsWork";

            // Set up default response

            requestSucceeded = false; //Starting off pessimistic...
            if (errDescription == null)
                errDescription = new clsWelmanError();

            errDescription.errNum = 0;
            errDescription.errorForUser = "";
            errDescription.logFileDescription = "";
            errDescription.errorForUser = "";

            System.ServiceModel.EndpointAddress remoteAddress = new System.ServiceModel.EndpointAddress(ThirdPartyBaseUrl);
            Comms.WebRoomsServiceSoapClient thisService = new Comms.WebRoomsServiceSoapClient(new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.Transport), remoteAddress);

            Comms.RWRServerParamsRequest thisRequest = new Comms.RWRServerParamsRequest();

            //			Comms.WebRoomsService thisService = new Comms.WebRoomsService();
            //			Comms.RWRServerParamsResponse thisResponse;

            Comms.RWRCheckServerResult thisResponse;

            thisRequest.PropertyAuthentication = new Comms.RWRServerParamsRequestPropertyAuthentication();
            thisRequest.PropertyAuthentication.PropertyId = Curl;
            thisRequest.PropertyAuthentication.PropertyPassword = Password;

            thisRequest.ThirdPartyAuthentication = new Comms.RWRServerParamsRequestThirdPartyAuthentication();

            thisRequest.ThirdPartyAuthentication.EnqUser = thisEnqUser;
            thisRequest.ThirdPartyAuthentication.EnqPass = thisEnqPass;

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
            
            if (!thisResponse.ResponseType.Success)
            {
                requestSucceeded = false;

                LogThis(DateTime.Now, "Failure attempting to check credentials",
                    thisResponse.ResponseType.Message, currentFunction, thisResponse.ResponseType.Message);

                return requestSucceeded;

            }


            requestSucceeded = true;
            LogThis(DateTime.Now, "Successfully Verified Credentials.", "",
                currentFunction,
                errDescription.logFileDescription);
            return requestSucceeded;



            //			string postParams = "Curl=" + Curl 
            //				+ "&pass=" + Password 
            //				+ "&credcheck=True";
            //
            //			UpdatingWebsite("", 0, "");
            //			
            //			string response = httpPost(WebsiteRecieveUrl, postParams);
            //
            //			UpdatingWebsite("", 100, "");
            //
            //			errDescription = parseServerResponseForGeneralError(response);
            //
            //			if (!errDescription.success)
            //			{
            //				if (errDescription.errNum == 50)
            //				{
            //					requestSucceeded = true;
            //					LogThis(DateTime.Now, "Successfully Verified Credentials.", "",
            //						currentFunction, 
            //						errDescription.logFileDescription);
            //					return requestSucceeded;
            //				}
            //				else
            //				{
            //					requestSucceeded = false;
            //					LogThis(DateTime.Now, "Failure attempting to check credentials", 
            //						errDescription.errorForUser, currentFunction, errDescription.logFileDescription);
            //					return requestSucceeded;
            //				}				
            //			}
            //
            //			return requestSucceeded;

        }

        #endregion

        #region WebsiteRecieveUrl

        //		/// <summary>
        //		/// Trys to retrieve the URL to use from the registry
        //		/// </summary>
        //		/// <returns>The name of the RHS folder or Null if unable to get RHS folder</returns>
        //		public string WebsiteRecieveUrl
        //		{
        //			get
        //			{
        //				string temp = readRegKey(applicationRegistryKey, applicationRegistrySubKey, ServerSiteKeyName);
        //				if ( temp == null || temp == "") //Nothing in the Registry
        //				{
        //					thisServerSite = @"www.securezone.co.nz";
        //				}
        //				else
        //				{
        //					thisServerSite = temp;
        //				}
        //				
        //				thisWebsiteRecieveUrl = @"https://" + thisServerSite.Trim() + @"/~welman/obsReceive.asp?"; 	
        //				return thisWebsiteRecieveUrl;
        //			}
        //
        //		}

        #endregion

        #region WebsiteSendUrl

        //		/// <summary>
        //		/// Trys to retrieve the URL to use from the registry
        //		/// </summary>
        //		/// <returns>The name of the RHS folder or Null if unable to get RHS folder</returns>
        //		public string WebsiteSendUrl
        //		{
        //			get
        //			{
        //				string temp = readRegKey(applicationRegistryKey, applicationRegistrySubKey, ServerSiteKeyName);
        //				if ( temp == null || temp == "") //Nothing in the Registry
        //				{
        //					thisServerSite = @"www.securezone.co.nz";
        //				}
        //				else
        //				{
        //					thisServerSite = temp;
        //				}
        //				
        //				thisWebsiteSendUrl = @"https://" + thisServerSite.Trim() + @"/~welman/obsSend.asp?";	
        //				return thisWebsiteSendUrl;
        //			}
        //
        //		}

        #endregion

        #region Load (New) Settings from file

        /// <summary>
        /// Saves the current settings in the settings file
        /// </summary>
        /// <param name="RhsFolderToLoadSettingsFrom">RhsFolderToLoadSettingsFrom</param>
        /// <returns>Success or Failure of loading of settings</returns>
        public bool LoadSettingsFromFile(string RhsFolderToLoadSettingsFrom)
        {
            bool response = false;

            string currentFunction = "LoadSettingsFromFile";
            DateTime thisDateTime = DateTime.Now;

            clsCsvReader thisCsvReader;

            #region See if the file exists
            if (!File.Exists(RhsFolderToLoadSettingsFrom + @"\" + NewSettingsFile))
            {
                requestSucceeded = false;
                errDescription.errNum = 1;
                errDescription.errorForUser = "Error Opening Settings file";
                errDescription.logFileDescription = "Settings File Not Found";

                LogThis(DateTime.Now, errDescription.errorForUser,
                    errDescription.logFileDescription, currentFunction, RhsFolderToLoadSettingsFrom + @"\" + NewSettingsFile);
                return response;
            }
            else
                rhsSettingsFileExists = true;

            #endregion

            #region Try to open the settings file

            try
            {
                File.SetAttributes(RhsFolderToLoadSettingsFrom + @"\" + NewSettingsFile, FileAttributes.Normal);
                thisCsvReader = new clsCsvReader(RhsFolderToLoadSettingsFrom + @"\" + NewSettingsFile);
            }
            catch (System.UnauthorizedAccessException e)
            {
                requestSucceeded = false;
                errDescription.errNum = 1;
                errDescription.errorForUser = "Error Opening Settings file";
                errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;

                LogThis(DateTime.Now, errDescription.errorForUser,
                    errDescription.logFileDescription, currentFunction, RhsFolderToLoadSettingsFrom + NewSettingsFile);
                return response;

            }
            catch (System.Exception e)
            {
                requestSucceeded = false;
                errDescription.errNum = 1;
                errDescription.errorForUser = "Error Opening Settings file";
                errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;

                LogThis(DateTime.Now, errDescription.errorForUser,
                    errDescription.logFileDescription, currentFunction, RhsFolderToLoadSettingsFrom + NewSettingsFile);
                return response;
            }

            #endregion

            #region Try to read the header

            string[] headingParams;

            try
            {
                headingParams = thisCsvReader.GetCsvLine();
            }
            catch (System.UnauthorizedAccessException e)
            {
                requestSucceeded = false;
                errDescription.errNum = 1;
                errDescription.errorForUser = "Error Reading Settings file";
                errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;

                LogThis(DateTime.Now, errDescription.errorForUser,
                    errDescription.logFileDescription, currentFunction, RhsFolderToLoadSettingsFrom + NewSettingsFile);
                return response;
            }
            catch (System.Exception e)
            {
                requestSucceeded = false;
                errDescription.errNum = 1;
                errDescription.errorForUser = "Error Reading Settings file";
                errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;

                LogThis(DateTime.Now, errDescription.errorForUser,
                    errDescription.logFileDescription, currentFunction, RhsFolderToLoadSettingsFrom + NewSettingsFile);
                return response;
            }

            #endregion

            #region Gather data for the header row

            if (headingParams != null)
            {
                for (int counter = 0; counter < headingParams.GetUpperBound(0) + 1; counter++)
                {

                    switch ((settingsHeaderParameterType)counter)
                    {
                        case settingsHeaderParameterType.addOnlineBookingsIntoRhs:
                            AddOnlineBookingsIntoRhs = getBoolFromString(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.addOverbookingProtection:
                            AddOverbookingProtection = getBoolFromString(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.confirmedBookingBehaviour:
                            ConfirmedBookingBehaviour = getIntFromString(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.curl:
                            Curl = headingParams[counter];
                            break;
                        case settingsHeaderParameterType.lastAvailabilityUpdate:
                            LastAvailabilityUpdate = fromWelmanDate(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.lastBookingReceived:
                            LastBookingReceived = fromWelmanDate(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.lastRhsAvailabilityFileModify:
                            LastRhsAvailabilityFileModify = fromWelmanDate(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.lastSettingsChange:
                            LastSettingsChange = fromWelmanDate(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.lastSuccessfulCommunication:
                            thisLastSuccessfulCommunication = fromWelmanDate(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.listboxHandle:
                            //ListboxHandle = getIntFromString(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.lookAhead:
                            Lookahead = getIntFromString(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.multiRoomBookingsInSameReservation:
                            MultiRoomBookingsInSameReservation = getBoolFromString(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.numRhsRoomTypes:
                            NumRhsRoomTypes = getIntFromString(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.numWebRoomTypes:
                            NumWebRoomTypes = getIntFromString(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.refDate:
                            RefDate = fromWelmanDate(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.refDateLastUpdate:
                            RefDateLastUpdate = fromWelmanDate(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.password:
                            try
                            {
                                clsStringEncryption thisEncryption = new clsStringEncryption();
                                Password = thisEncryption.Decrypt(headingParams[counter]);
                            }
                            catch (System.Exception e)
                            {
                                string errorForUser = "Error Decrypting Password";
                                string logFileDescription = e.ToString() + e.Message + e.StackTrace;

                                LogThis(DateTime.Now, errorForUser, logFileDescription, currentFunction, RhsFolderToLoadSettingsFrom + NewSettingsFile);
                                return response;
                            }
                            break;
                        case settingsHeaderParameterType.remoteSupportInvites:
                            RemoteSupportInvites = headingParams[counter];
                            break;
                        case settingsHeaderParameterType.updateWebsiteAvailability:
                            UpdateWebsiteAvailability = getBoolFromString(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.serverCommunicationInterval:
                            ServerCommunicationInterval = getIntFromString(headingParams[counter]);
                            break;
                        case settingsHeaderParameterType.updateUnmapped:
                            UpdateUnmapped = getBoolFromString(headingParams[counter]);
                            break;
                        default:
                            break;
                    }
                }
            }

            #endregion //End of header row

            #region Try to read the RHS Room Types

            theseRhsRoomTypes = new ArrayList();

            for (int rhsRoomCounter = 0; rhsRoomCounter < thisNumRhsRoomTypes; rhsRoomCounter++)
            {

                #region Attempt To Read
                string[] rhsRoomTypeParams;

                try
                {
                    rhsRoomTypeParams = thisCsvReader.GetCsvLine();
                }
                catch (System.UnauthorizedAccessException e)
                {
                    requestSucceeded = false;
                    errDescription.errNum = 1;
                    errDescription.errorForUser = "Error Reading Settings file";
                    errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;

                    LogThis(DateTime.Now, errDescription.errorForUser,
                        errDescription.logFileDescription, currentFunction, RhsFolderToLoadSettingsFrom + NewSettingsFile);
                    return response;
                }
                catch (System.Exception e)
                {
                    requestSucceeded = false;
                    errDescription.errNum = 1;
                    errDescription.errorForUser = "Error Reading Settings file";
                    errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;

                    LogThis(DateTime.Now, errDescription.errorForUser,
                        errDescription.logFileDescription, currentFunction, RhsFolderToLoadSettingsFrom + NewSettingsFile);
                    return response;
                }

                #endregion

                #region Gather RHS Room Type Data

                if (rhsRoomTypeParams != null)
                {
                    clsRhsRoomType thisRhsRoomType = new clsRhsRoomType();

                    for (int paramCounter = 0; paramCounter < rhsRoomTypeParams.GetUpperBound(0) + 1; paramCounter++)
                    {

                        switch ((settingsRhsRoomTypeParameterType)paramCounter)
                        {
                            //Strings of up to 30 characters
                            case settingsRhsRoomTypeParameterType.rhsRoomTypeId:
                                thisRhsRoomType.roomTypeId = getIntFromString(rhsRoomTypeParams[paramCounter]);
                                if (thisRhsRoomType.roomTypeId == 0)
                                    thisRhsRoomType.roomTypeId = rhsRoomCounter;

                                break;
                            case settingsRhsRoomTypeParameterType.rhsRoomTypeName:
                                thisRhsRoomType.roomTypeName = rhsRoomTypeParams[paramCounter];
                                break;
                            case settingsRhsRoomTypeParameterType.rhsRoomTypeCode:
                                thisRhsRoomType.roomTypeCode = rhsRoomTypeParams[paramCounter];
                                break;
                            case settingsRhsRoomTypeParameterType.affectOccupancy:
                                thisRhsRoomType.affectOccupancy = getIntFromString(rhsRoomTypeParams[paramCounter]);
                                break;
                            case settingsRhsRoomTypeParameterType.numRooms:
                                thisRhsRoomType.numberOfRooms = getIntFromString(rhsRoomTypeParams[paramCounter]);
                                break;
                            case settingsRhsRoomTypeParameterType.Availability:
                                thisRhsRoomType.AvailabilityAsDelimitedList = rhsRoomTypeParams[paramCounter];
                                break;
                            case settingsRhsRoomTypeParameterType.AvailabilityAtTimeOfLastUpdate:
                                thisRhsRoomType.AvailabilityAtTimeOfLastUpdateAsDelimitedList = rhsRoomTypeParams[paramCounter];
                                break;
                            default:
                                break;
                        }

                    }

                    #region Confirm that we don't already have this room type Id; if we do then ignore

                    bool foundThisRhsRoomType = false;

                    foreach (object thisObject in theseRhsRoomTypes)
                    {
                        clsRhsRoomType thisCheckRhsRoomType = (clsRhsRoomType)thisObject;
                        if (thisCheckRhsRoomType.roomTypeId == thisRhsRoomType.roomTypeId)
                            foundThisRhsRoomType = true;
                    }

                    #endregion

                    if (!foundThisRhsRoomType)
                    {
                        theseRhsRoomTypes.Add(thisRhsRoomType);
                        //Not Required						SetRhsRoomType();
                    }

                }
                #endregion
            }

            #endregion

            #region Try to read the Web Room Types

            theseWebRoomTypes = new ArrayList();

            for (int webRoomCounter = 0; webRoomCounter < thisNumWebRoomTypes; webRoomCounter++)
            {
                #region Attempt To Read

                string[] webRoomTypeParams;

                try
                {
                    webRoomTypeParams = thisCsvReader.GetCsvLine();
                }
                catch (System.UnauthorizedAccessException e)
                {
                    requestSucceeded = false;
                    errDescription.errNum = 1;
                    errDescription.errorForUser = "Error Reading Settings file";
                    errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;

                    LogThis(DateTime.Now, errDescription.errorForUser,
                        errDescription.logFileDescription, currentFunction, RhsFolderToLoadSettingsFrom + NewSettingsFile);
                    return response;
                }
                catch (System.Exception e)
                {
                    requestSucceeded = false;
                    errDescription.errNum = 1;
                    errDescription.errorForUser = "Error Reading Settings file";
                    errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;

                    LogThis(DateTime.Now, errDescription.errorForUser,
                        errDescription.logFileDescription, currentFunction, RhsFolderToLoadSettingsFrom + NewSettingsFile);
                    return response;
                }

                #endregion

                #region Gather web Room Type Data

                if (webRoomTypeParams != null)
                {

                    clsWebRoomType thisWebRoomType = new clsWebRoomType();

                    for (int paramCounter = 0; paramCounter < webRoomTypeParams.GetUpperBound(0) + 1; paramCounter++)
                    {

                        switch ((settingsWebRoomTypeParameterType)paramCounter)
                        {
                            //Strings of up to 30 characters
                            case settingsWebRoomTypeParameterType.webRoomTypeId:
                                thisWebRoomType.webRoomTypeId = getIntFromString(webRoomTypeParams[paramCounter]);
                                break;
                            case settingsWebRoomTypeParameterType.webRoomTypeName:
                                thisWebRoomType.webRoomTypeName = webRoomTypeParams[paramCounter];
                                break;
                            case settingsWebRoomTypeParameterType.isInterconnecting:
                                thisWebRoomType.IsInterConnecting = getIntFromString(webRoomTypeParams[paramCounter]);
                                break;
                            case settingsWebRoomTypeParameterType.rhsRoomTypesMappedToGroup1:
                                thisWebRoomType.RhsRoomTypesMappedToGroup1AsDelimitedList = webRoomTypeParams[paramCounter];
                                break;
                            case settingsWebRoomTypeParameterType.rhsRoomTypesMappedToGroup2:
                                thisWebRoomType.RhsRoomTypesMappedToGroup2AsDelimitedList = webRoomTypeParams[paramCounter];
                                break;
                            case settingsWebRoomTypeParameterType.maxNumberOfRooms:
                                thisWebRoomType.maxNumberOfRooms = getIntFromString(webRoomTypeParams[paramCounter]);
                                break;
                            case settingsWebRoomTypeParameterType.AvailabilityAsDelimitedList:
                                thisWebRoomType.AvailabilityAsDelimitedList = webRoomTypeParams[paramCounter];
                                break;
                            case settingsWebRoomTypeParameterType.AvailabilityAtTimeOfLastUpdateAsDelimitedList:
                                thisWebRoomType.AvailabilityAtTimeOfLastUpdateAsDelimitedList = webRoomTypeParams[paramCounter];
                                break;
                            default:
                                break;
                        }

                    }

                    if (thisWebRoomType.webRoomTypeId != 0)
                    {

                        #region Confirm that we don't already have this room type Id; if we do then ignore

                        bool foundThisWebRoomType = false;

                        foreach (object thisObject in theseWebRoomTypes)
                        {
                            clsWebRoomType thisCheckWebRoomType = (clsWebRoomType)thisObject;
                            if (thisCheckWebRoomType.webRoomTypeId == thisWebRoomType.webRoomTypeId)
                                foundThisWebRoomType = true;
                        }

                        #endregion

                        if (!foundThisWebRoomType)
                        {
                            theseWebRoomTypes.Add(thisWebRoomType);
                            //Not Required							SetWebRoomType();
                        }

                    }

                }
                #endregion
            }

            #endregion

            #region Set the Rhs Folder

            thisRhsFolder = RhsFolderToLoadSettingsFrom;

            #endregion


            #region Close and Log

            //Success!
            thisCsvReader.Dispose();

            if (Curl != "" && Password != "")
                requestSucceeded = true;
            else
                requestSucceeded = false;

            LogThis(DateTime.Now, "Loaded Settings From: " + RhsFolderToLoadSettingsFrom, "", currentFunction, "");

            return requestSucceeded;

            #endregion

        }

        #endregion

        #region LoadOldSettings

        #region LoadOldSettingsFromFile 
        //
        //		/// <summary>
        //		/// Loads settings from the settings file in the defined RhsDataFolder. 
        //		/// Requires that the folder RhsDataFolder exists, and conatins
        //		/// a file called OldSettingsFile which is a valid RHS Webrooms Updater settings file
        //		/// </summary>
        //		/// <returns>Response indicating Success or Failure with a description of Failure</returns>
        //		private bool LoadOldSettingsFromFile()
        //		{
        //			string currentFunction = "LoadOldSettingsFromFile";
        //			
        //			StreamReader settingsFileReader;
        //			string folderToLoadFrom = thisRhsFolder;
        //			
        //			if (folderToLoadFrom.EndsWith(dataSubFolder))
        //				folderToLoadFrom = folderToLoadFrom.Substring(0, folderToLoadFrom.Length - dataSubFolder.Length);
        //			
        //			string fileName = folderToLoadFrom + @"\" + OldSettingsFile;
        //
        //			string settingsFileData = "";
        //			
        //			bool response = false;
        //			rhsSettingsFileExists = false;
        //			credentialsAreValid = false;
        //
        //			#region See if the file exists
        //			if (!File.Exists(fileName))
        //			{
        //				requestSucceeded = false;
        //				errDescription.errNum = 1;
        //				errDescription.errorForUser = "Error Opening Settings file";
        //				errDescription.logFileDescription = "Settings File Not Found";
        //
        //				LogThis(DateTime.Now, errDescription.errorForUser,
        //					errDescription.logFileDescription, currentFunction, fileName);
        //				return response;
        //			}
        //			else
        //				rhsSettingsFileExists = true;
        //
        //			#endregion
        //
        //			#region Try to open the settings file
        //
        //			try
        //			{
        //				settingsFileReader = new StreamReader(fileName);
        //			}
        //			catch (System.UnauthorizedAccessException e)
        //			{
        //				requestSucceeded = false;
        //				errDescription.errNum = 1;
        //				errDescription.errorForUser = "Error Opening Settings file";
        //				errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;
        //
        //				LogThis(DateTime.Now, errDescription.errorForUser,
        //					errDescription.logFileDescription, currentFunction, fileName);
        //				return response;
        //				
        //			}
        //			catch (System.Exception e)
        //			{
        //				requestSucceeded = false;
        //				errDescription.errNum = 1;
        //				errDescription.errorForUser = "Error Opening Settings file";
        //				errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;
        //
        //				LogThis(DateTime.Now, errDescription.errorForUser,
        //					errDescription.logFileDescription, currentFunction, fileName);
        //				return response;
        //			}
        //
        //			#endregion
        //			
        //			#region Try to read file
        //			try
        //			{
        //				settingsFileData = settingsFileReader.ReadToEnd(); 
        //			}
        //			catch (System.UnauthorizedAccessException e)
        //			{
        //				requestSucceeded = false;
        //				errDescription.errNum = 1;
        //				errDescription.errorForUser = "Error Reading Settings file";
        //				errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;
        //
        //				LogThis(DateTime.Now, errDescription.errorForUser,
        //					errDescription.logFileDescription, currentFunction, fileName);
        //				return response;
        //			}
        //			catch (System.Exception e)
        //			{
        //				requestSucceeded = false;
        //				errDescription.errNum = 1;
        //				errDescription.errorForUser = "Error Reading Settings file";
        //				errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;
        //
        //				LogThis(DateTime.Now, errDescription.errorForUser,
        //					errDescription.logFileDescription, currentFunction, fileName);
        //				return response;
        //			}
        //
        //			#endregion
        //
        //			rhsFolderIsValid = true;
        //			
        //			#region Decrypt the file here
        //
        //			stringEncryption encryption = new stringEncryption();
        //			
        //			string unencryptedData = encryption.Decrypt(settingsFileData);
        //
        //			//Use the data in the settings file
        //			int currentIndex = 0;
        //			int currentParamIndex = 0;
        //			int nextEol;
        //			int nextComma;
        //			byte[] testChar;
        //			testChar = new byte[2];
        //			string lineToParse; //Test Line
        //			string paramstring; //Test String
        //
        //			currentIndex = 0;
        //			nextEol = unencryptedData.IndexOf(aCrLf, currentIndex);
        //			nextComma = unencryptedData.IndexOf(aComma, currentIndex);
        //
        //
        //			if (nextEol == -1)
        //			{
        //				requestSucceeded = false;
        //				errDescription.errNum = 1;
        //				errDescription.errorForUser = "Error Reading Settings file";
        //				errDescription.logFileDescription = "Premature End of line; incorrect format";
        //
        //				LogThis(DateTime.Now, errDescription.errorForUser,
        //					errDescription.logFileDescription, currentFunction, fileName);
        //				return response;
        //			}
        //
        //			#endregion
        //
        //			#region Gather data for the header row
        //			lineToParse = unencryptedData.Substring(currentIndex, nextEol - currentIndex);
        //			currentParamIndex = 0;
        //			nextComma = -1;
        //			int thisLineLength = lineToParse.Length;
        //
        //			for (int headerParamCounter = 0; headerParamCounter < 10; headerParamCounter++)
        //			{
        //				currentParamIndex = nextComma + 1;
        //				if (thisLineLength >= currentParamIndex)
        //				{
        //
        //					nextComma = lineToParse.IndexOf(aComma, currentParamIndex);
        //					if (nextComma == -1) //If we've reached the last aComma
        //						nextComma = thisLineLength; //The end of this param is the end of the line
        //					
        //					paramstring = lineToParse.Substring(currentParamIndex, nextComma - currentParamIndex);
        //				}
        //				else
        //					paramstring = "";
        //
        //				switch (headerParamCounter) 
        //				{
        //						//Strings of up to 30 characters
        //					case 0:
        //						NumWebRoomTypes = getIntFromString(paramstring);
        //						break;
        //					case 1:
        //						NumRhsRoomTypes = getIntFromString(paramstring);
        //						break;
        //					case 2:
        //						Lookahead = getIntFromString(paramstring);
        //						break;
        //					case 3:
        //						RefDate = getDateFromString(paramstring);
        //						break;
        //					case 4:
        //						Curl = paramstring;
        //						break;
        //					case 5:
        //						Password = paramstring;
        //						break;
        //					case 6:
        //						AddOnlineBookingsIntoRhs = getBoolFromString(paramstring);
        //						break;
        //					case 7:
        //						UpdateWebsiteAvailability = getBoolFromString(paramstring);
        //						break;
        //					case 8:
        //						ServerCommunicationInterval = getIntFromString(paramstring);
        //						break;
        //					case 9:
        //						if (paramstring == "")
        //							AddOverbookingProtection = false;
        //						else
        //							AddOverbookingProtection = getBoolFromString(paramstring);
        //						break;
        //					default:
        //						break;
        //				}
        //			} 
        //			
        //			#endregion //End of header row
        //
        //			#region Get Room Types
        //
        //			credentialsAreValid = true;
        //
        //			//Declare variables based on header information
        //			theseWebRoomTypes = new ArrayList();
        //			theseRhsRoomTypes = new ArrayList();
        //			
        //			//Gather Web Rooms Data
        //			for (int webRoomCounter = 0; webRoomCounter < NumWebRoomTypes; webRoomCounter++)
        //			{
        //
        //				clsWebRoomType thisWebRoomType = new clsWebRoomType();
        //				
        //
        //				int numAssoications = 0;
        //
        //				//Get First line
        //				currentIndex = nextEol + 2;
        //				nextEol = unencryptedData.IndexOf(aCrLf, currentIndex);
        //				
        //				if (nextEol == -1)
        //				{
        //					requestSucceeded = false;
        //					errDescription.errNum = 1;
        //					errDescription.errorForUser = "Error Reading Settings file";
        //					errDescription.logFileDescription = "Premature End of line; incorrect format";
        //
        //					LogThis(DateTime.Now, errDescription.errorForUser,
        //						errDescription.logFileDescription, currentFunction, fileName);
        //					return response;
        //				}
        //
        //				lineToParse = unencryptedData.Substring(currentIndex, nextEol - currentIndex);
        //				currentParamIndex = 0;
        //				nextComma = -1;
        //
        //				for (int headerParamCounter = 0; headerParamCounter < 3; headerParamCounter++)
        //				{
        //					currentParamIndex = nextComma + 1;
        //					nextComma = lineToParse.IndexOf(aComma, currentParamIndex);
        //					if (nextComma == -1) //If we've reached the last aComma
        //						nextComma = lineToParse.Length; //The end of this param is the end of the line
        //					
        //					paramstring = lineToParse.Substring(currentParamIndex, nextComma - currentParamIndex);
        //				
        //					switch (headerParamCounter) 
        //					{
        //						case 0:
        //							thisWebRoomType.webRoomTypeId = getIntFromString(paramstring);
        //							break;
        //						case 1:
        //							thisWebRoomType.webRoomTypeName = paramstring;
        //							break;
        //						case 2:
        //							numAssoications = getIntFromString(paramstring);
        //							break;
        //						default:
        //							break;
        //					}
        //				}
        //
        //				if (numAssoications > 0)
        //				{
        //					//Get Second line (associations)
        //					currentIndex = nextEol + 2;
        //					nextEol = unencryptedData.IndexOf(aCrLf, currentIndex);
        //
        //					if (nextEol == -1)
        //					{
        //						LogThis(DateTime.Now, "Error Reading Settings file",
        //							"File incorrect Format", currentFunction, fileName);	
        //
        //						NumWebRoomTypes = 0;
        //						return response;
        //					}
        //				
        //					lineToParse = unencryptedData.Substring(currentIndex, nextEol - currentIndex);
        //					currentParamIndex = 0;
        //					nextComma = -1;
        //
        //					for (int associatedRhsRoomTypesCounter = 0; 
        //						associatedRhsRoomTypesCounter < numAssoications; 
        //						associatedRhsRoomTypesCounter++)
        //					{
        //						currentParamIndex = nextComma + 1;
        //						nextComma = lineToParse.IndexOf(aComma, currentParamIndex);
        //						if (nextComma == -1) //If we've reached the last aComma
        //							nextComma = lineToParse.Length; //The end of this param is the end of the line
        //					
        //						paramstring = lineToParse.Substring(currentParamIndex, nextComma - currentParamIndex);
        //					
        //						thisWebRoomType.RhsRoomTypesMappedToGroup1.Add(getIntFromString(paramstring));
        //
        //					}
        //
        //				}
        //				theseWebRoomTypes.Add(thisWebRoomType);
        //				SetWebRoomType();
        //
        //			}
        //			//Gather RHS Rooms Data
        //			for (int rhsRoomCounter = 0; rhsRoomCounter < NumRhsRoomTypes; rhsRoomCounter++)
        //			{
        //				//Get First line
        //				currentIndex = nextEol + 2;
        //				nextEol = unencryptedData.IndexOf(aCrLf, currentIndex);
        //
        //				if (nextEol == -1)
        //				{
        //					requestSucceeded = false;
        //					errDescription.errNum = 1;
        //					errDescription.errorForUser = "Error Reading Settings file";
        //					errDescription.logFileDescription = "Premature End of line; incorrect format";
        //
        //					LogThis(DateTime.Now, errDescription.errorForUser,
        //						errDescription.logFileDescription, currentFunction, fileName);
        //					return response;
        //
        //				}
        //
        //				lineToParse = unencryptedData.Substring(currentIndex, nextEol - currentIndex);
        //				currentParamIndex = 0;
        //				nextComma = - 1;
        //
        //				clsRhsRoomType thisRhsRoomType = new clsRhsRoomType();
        //
        //				for (int headerParamCounter = 0; headerParamCounter < 5; headerParamCounter++)
        //				{
        //					currentParamIndex = nextComma + 1;
        //					nextComma = lineToParse.IndexOf(aComma, currentParamIndex);
        //					if (nextComma == -1) //If we've reached the last aComma
        //						nextComma = lineToParse.Length; //The end of this param is the end of the line
        //					
        //					paramstring = lineToParse.Substring(currentParamIndex, nextComma - currentParamIndex);
        //				
        //					switch (headerParamCounter) 
        //					{
        //							//Strings of up to 30 characters
        //						case 0:
        //							thisRhsRoomType.roomTypeId = getIntFromString(paramstring);
        //							break;
        //						case 1:
        //							thisRhsRoomType.roomTypeName = paramstring;
        //							break;
        //						case 2:
        //							thisRhsRoomType.roomTypeCode = paramstring;
        //							break;
        //						case 3:
        //							if (getBoolFromString(paramstring))
        //								thisRhsRoomType.affectOccupancy = 1;
        //							else
        //								thisRhsRoomType.affectOccupancy = 0;
        //							break;
        //						case 4:
        //							thisRhsRoomType.numberOfRooms = getIntFromString(paramstring);
        //							break;
        //						default:
        //							break;
        //					}
        //
        //				}
        //				theseRhsRoomTypes.Add(thisRhsRoomType);
        //				SetRhsRoomType();
        //
        //
        //			}
        //	
        //			settingsFileReader.Close();
        //
        //			#endregion
        //
        //			//Success!
        //			requestSucceeded = true;
        //
        //			LogThis(DateTime.Now, "Loaded Settings From Old File.", "", currentFunction, "");
        //
        //			#region Load Availability from file
        ////
        ////			StreamReader availabilityFileReader;
        ////			if (folderToLoadFrom.EndsWith(clsSetting.dataSubFolder))
        ////				folderToLoadFrom = folderToLoadFrom.Substring(0, folderToLoadFrom.Length - clsSetting.dataSubFolder.Length - 1);
        ////			
        ////			fileName = folderToLoadFrom + @"\" + availabilityFile;
        ////
        ////			string strLine = "";
        ////			string[] strArray;
        ////			char[] charArray = new char[] {','};
        ////
        ////			requestSucceeded = false;
        ////			
        ////			firstDayValue = 0;
        ////			numDays = 0;
        ////			numRoomTypes = 0;
        ////
        ////
        ////			if (!File.Exists(fileName))
        ////			{
        ////				errDescription.errNum = 1010;
        ////				errDescription.errorForUser = "Availability File not found";
        ////				LogThis(DateTime.Now, "Availability file does not exist",
        ////					errDescription.errorForUser, currentFunction, 
        ////					errDescription.errNum.ToString() + " " + fileName);
        ////				return requestSucceeded;
        ////			}
        ////
        ////			//Read Availability
        ////			try
        ////			{
        ////				availabilityFileReader = new StreamReader(fileName);
        ////			}
        ////			catch (System.UnauthorizedAccessException e)
        ////			{
        ////				errDescription.errNum = 1011;
        ////				errDescription.errorForUser = "Error Opening Availability file";
        ////				errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;
        ////
        ////				LogThis(DateTime.Now, errDescription.errorForUser,
        ////					errDescription.logFileDescription, currentFunction, 
        ////					errDescription.errNum.ToString() + " " + fileName);
        ////				return requestSucceeded;
        ////			}
        ////			catch (System.Exception e)
        ////			{
        ////				errDescription.errNum = 1012;
        ////				errDescription.errorForUser = "Error Opening Availability file";
        ////				errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;
        ////
        ////				LogThis(DateTime.Now, errDescription.errorForUser,
        ////					errDescription.logFileDescription, currentFunction, 
        ////					errDescription.errNum.ToString() + " " + fileName);
        ////				return requestSucceeded;
        ////			}
        ////
        ////
        ////			//General Header data
        ////			try
        ////			{
        ////				strLine = availabilityFileReader.ReadLine();
        ////			}
        ////			catch (System.UnauthorizedAccessException e)
        ////			{
        ////				availabilityFileReader.Close();
        ////
        ////				errDescription.errNum = 1013;
        ////				errDescription.errorForUser = "Error Reading Availability file";
        ////				errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;
        ////
        ////				LogThis(DateTime.Now, errDescription.errorForUser,
        ////					errDescription.logFileDescription, currentFunction, 
        ////					errDescription.errNum.ToString() + " " + fileName);
        ////
        ////				return requestSucceeded;
        ////			}
        ////			catch (System.Exception e)
        ////			{
        ////				availabilityFileReader.Close();
        ////				errDescription.errNum = 1014;
        ////				errDescription.errorForUser = "Error Reading Availability file";
        ////				errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;
        ////
        ////				LogThis(DateTime.Now, errDescription.errorForUser,
        ////					errDescription.logFileDescription, currentFunction, 
        ////					errDescription.errNum.ToString() + " " + fileName);
        ////				return requestSucceeded;
        ////			}
        ////
        ////			strArray = strLine.Split(charArray);
        ////			if(strArray.GetUpperBound(0)==3-1)
        ////			{
        ////				firstDayValue = Convert.ToInt64(strArray[0].Trim());
        ////				numDays = Convert.ToInt64(strArray[1].Trim());
        ////				numRoomTypes = Convert.ToInt64(strArray[2].Trim());
        ////			}
        ////			else
        ////			{
        ////				availabilityFileReader.Close();
        ////				errDescription.errNum = 1015;
        ////				errDescription.errorForUser = "Availability file format error";
        ////				errDescription.logFileDescription = "Incorrect Number of Parameters: " + strLine;
        ////
        ////				LogThis(DateTime.Now, errDescription.errorForUser,
        ////					errDescription.logFileDescription, currentFunction, 
        ////					errDescription.errNum.ToString() + " " + fileName);
        ////
        ////				return requestSucceeded;
        ////			}
        ////
        ////			availabilityAndOccupancy = new 
        ////				roomTypeStatusStruct[numRoomTypes, numDays];
        ////
        ////			for (long dayCounter = 0; dayCounter < numDays; 
        ////				dayCounter++)
        ////			{
        ////				try
        ////				{
        ////					strLine = availabilityFileReader.ReadLine();
        ////				}
        ////				catch (System.UnauthorizedAccessException e)
        ////				{
        ////					availabilityFileReader.Close();
        ////					errDescription.errNum = 1016;
        ////					errDescription.errorForUser = "Error Reading from Availability file";
        ////					errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;
        ////
        ////					LogThis(DateTime.Now, errDescription.errorForUser,
        ////						errDescription.logFileDescription, currentFunction, 
        ////						errDescription.errNum.ToString() + " " + fileName);
        ////
        ////					return requestSucceeded;
        ////				}
        ////				catch (System.Exception e)
        ////				{
        ////					availabilityFileReader.Close();
        ////					errDescription.errNum = 1017;
        ////					errDescription.errorForUser = "Error Reading from Availability file";
        ////					errDescription.logFileDescription = e.ToString() + e.Message + e.StackTrace;
        ////
        ////					LogThis(DateTime.Now, errDescription.errorForUser,
        ////						errDescription.logFileDescription, currentFunction, 
        ////						errDescription.errNum.ToString() + " " + fileName);
        ////					return requestSucceeded;
        ////				}
        ////				strArray = strLine.Split(charArray);
        ////
        ////				if(strArray.GetUpperBound(0)== 2 * numRoomTypes - 1)
        ////				{
        ////						
        ////					for (long roomTypeCounter = 0; roomTypeCounter < 
        ////						numRoomTypes; roomTypeCounter++)
        ////					{
        ////						availabilityAndOccupancy[roomTypeCounter, 
        ////							dayCounter].available = getIntFromString(strArray[2 * roomTypeCounter].Trim());
        ////						availabilityAndOccupancy[roomTypeCounter, 
        ////							dayCounter].occupied = getIntFromString(strArray[2 * roomTypeCounter + 1].Trim());
        ////					}
        ////				}
        ////				else //Possible corruption in the file
        ////				{
        ////					availabilityFileReader.Close();
        ////					errDescription.errNum = 1018;
        ////					errDescription.errorForUser = "Availability file format error";
        ////					errDescription.logFileDescription = "Incorrect Number of Parameters: " + strLine;
        ////
        ////					LogThis(DateTime.Now, errDescription.errorForUser,
        ////						errDescription.logFileDescription, currentFunction, 
        ////						errDescription.errNum.ToString() + " " + fileName);
        ////					
        ////					return requestSucceeded;
        ////				}
        ////
        ////			}
        ////
        ////			availabilityFileReader.Close();
        ////			//Success!
        ////			requestSucceeded = true;
        ////
        ////			return requestSucceeded;
        ////
        //			#endregion
        //
        //			return requestSucceeded;
        //		}

        #endregion

        #region LoadOldSettingsFromRegistry

        //		/// <summary>
        //		/// Load settings from Registry. This is only used if we can not currently
        //		/// Get settings from the Settings file due to the settings file being undfindable
        //		/// (e.g. Mapped Network Drive unreachable)
        //		/// </summary>
        //		/// <returns>Response indicating the success or Failure + description of Failure</returns>
        //		private bool LoadOldSettingsFromRegistry()
        //		{
        //			string currentFunction = "LoadOldSettingsFromRegistry";
        //			string settingsFileData = "";
        //			string fileName = "Registry";
        //
        //			requestSucceeded = false;
        //			
        //			#region Get settings from the registry
        //
        //			string temp = readRegKey(applicationRegistryKey, applicationRegistrySubKey, OldSettingsKeyName);
        //			if ( temp == null) //Nothing in the Registry
        //			{
        //				//No settings in the Registry
        //				requestSucceeded = false;
        //				errDescription.errNum = 1;
        //				errDescription.errorForUser = "No Settings Found in Registry";
        //				errDescription.logFileDescription = "Null returned";
        //
        //				LogThis(DateTime.Now, errDescription.errorForUser,
        //					errDescription.logFileDescription, currentFunction, fileName);
        //				return requestSucceeded;
        //			}
        //			else
        //			{
        //				settingsFileData = temp; // We have settings!
        //			}
        //			
        //			#endregion
        //
        //			#region Decrypt the settings
        //			stringEncryption encryption = new stringEncryption();
        //			
        //			string unencryptedData = encryption.Decrypt(settingsFileData);
        //
        //			//Use the data in the settings file
        //			int currentIndex = 0;
        //			int currentParamIndex = 0;
        //			int nextEol;
        //			int nextComma;
        //			byte[] testChar;
        //			testChar = new byte[2];
        //			string lineToParse; //Test Line
        //			string paramstring; //Test String
        //
        //			currentIndex = 0;
        //			nextEol = unencryptedData.IndexOf(aCrLf, currentIndex);
        //			nextComma = unencryptedData.IndexOf(aComma, currentIndex);
        //
        //			if (nextEol == -1)
        //			{
        //				requestSucceeded = false;
        //				errDescription.errNum = 1;
        //				errDescription.errorForUser = "Error Reading Settings file";
        //				errDescription.logFileDescription = "Premature End of line; incorrect format";
        //
        //				LogThis(DateTime.Now, errDescription.errorForUser,
        //					errDescription.logFileDescription, currentFunction, fileName);
        //				return requestSucceeded;
        //			}
        //			#endregion
        //
        //			#region Gather data for the header row
        //			lineToParse = unencryptedData.Substring(currentIndex, nextEol - currentIndex);
        //			currentParamIndex = 0;
        //			nextComma = -1;
        //			int thisLineLength = lineToParse.Length;
        //
        //			for (int headerParamCounter = 0; headerParamCounter < 10; headerParamCounter++)
        //			{
        //				currentParamIndex = nextComma + 1;
        //				if (thisLineLength >= currentParamIndex)
        //				{
        //
        //					nextComma = lineToParse.IndexOf(aComma, currentParamIndex);
        //					if (nextComma == -1) //If we've reached the last aComma
        //						nextComma = thisLineLength; //The end of this param is the end of the line
        //					
        //					paramstring = lineToParse.Substring(currentParamIndex, nextComma - currentParamIndex);
        //				}
        //				else
        //					paramstring = "";
        //
        //				switch (headerParamCounter) 
        //				{
        //						//Strings of up to 30 characters
        //					case 0:
        //						NumWebRoomTypes = getIntFromString(paramstring);
        //						break;
        //					case 1:
        //						NumRhsRoomTypes = getIntFromString(paramstring);
        //						break;
        //					case 2:
        //						Lookahead = getIntFromString(paramstring);
        //						break;
        //					case 3:
        //						RefDate = getDateFromString(paramstring);
        //						break;
        //					case 4:
        //						Curl = paramstring;
        //						break;
        //					case 5:
        //						Password = paramstring;
        //						break;
        //					case 6:
        //						AddOnlineBookingsIntoRhs = getBoolFromString(paramstring);
        //						break;
        //					case 7:
        //						UpdateWebsiteAvailability = getBoolFromString(paramstring);
        //						break;
        //					case 8:
        //						ServerCommunicationInterval = getIntFromString(paramstring);
        //						break;
        //					case 9:
        //						if (paramstring == "")
        //							AddOverbookingProtection = false;
        //						else
        //							AddOverbookingProtection = getBoolFromString(paramstring);
        //						break;
        //					default:
        //						break;
        //				}
        //			} // End of header row
        //
        //			#endregion
        //
        //			//Declare variables based on header information
        //			theseWebRoomTypes = new ArrayList();
        //			theseRhsRoomTypes = new ArrayList();
        //
        //
        //			#region Gather Web Rooms Data
        //			for (int webRoomCounter = 0; webRoomCounter < NumWebRoomTypes; webRoomCounter++)
        //			{
        //				#region Get First line
        //				currentIndex = nextEol + 2;
        //				nextEol = unencryptedData.IndexOf(aCrLf, currentIndex);
        //				
        //				if (nextEol == -1)
        //				{
        //					requestSucceeded = false;
        //					errDescription.errNum = 1;
        //					errDescription.errorForUser = "Error Reading Settings file";
        //					errDescription.logFileDescription = "Premature End of line; incorrect format";
        //
        //					LogThis(DateTime.Now, errDescription.errorForUser,
        //						errDescription.logFileDescription, currentFunction, fileName);
        //					return requestSucceeded;
        //				}
        //
        //				lineToParse = unencryptedData.Substring(currentIndex, nextEol - currentIndex);
        //				currentParamIndex = 0;
        //				nextComma = -1;
        //
        //				clsWebRoomType thisWebRoomType = new clsWebRoomType();
        //				int numAssoications = 0;
        //
        //
        //				for (int headerParamCounter = 0; headerParamCounter < 3; headerParamCounter++)
        //				{
        //					currentParamIndex = nextComma + 1;
        //					nextComma = lineToParse.IndexOf(aComma, currentParamIndex);
        //					if (nextComma == -1) //If we've reached the last aComma
        //						nextComma = lineToParse.Length; //The end of this param is the end of the line
        //					
        //					paramstring = lineToParse.Substring(currentParamIndex, nextComma - currentParamIndex);
        //				
        //
        //					switch (headerParamCounter) 
        //					{
        //						case 0:
        //							thisWebRoomType.webRoomTypeId = getIntFromString(paramstring);
        //							break;
        //						case 1:
        //							thisWebRoomType.webRoomTypeName = paramstring;
        //							break;
        //						case 2:
        //							numAssoications = getIntFromString(paramstring);
        //							break;
        //						default:
        //							break;
        //					}
        //				}
        //
        //				#endregion
        //
        //				#region Get Associations
        //
        //				if (numAssoications > 0)
        //				{
        //					#region Get Second line (associations)
        //					currentIndex = nextEol + 2;
        //					nextEol = unencryptedData.IndexOf(aCrLf, currentIndex);
        //
        //					if (nextEol == -1)
        //					{
        //						LogThis(DateTime.Now, "Error Reading Settings file",
        //							"File incorrect Format", currentFunction, fileName);	
        //
        //						NumRhsRoomTypes = 0;
        //						NumWebRoomTypes = 0;
        //						return requestSucceeded;
        //					}
        //				
        //					lineToParse = unencryptedData.Substring(currentIndex, nextEol - currentIndex);
        //					currentParamIndex = 0;
        //					nextComma = -1;
        //
        //					thisWebRoomType.RhsRoomTypesMappedToGroup1 = new ArrayList();
        //
        //					for (int associatedRhsRoomTypesCounter = 0; 
        //						associatedRhsRoomTypesCounter < numAssoications; 
        //						associatedRhsRoomTypesCounter++)
        //					{
        //						currentParamIndex = nextComma + 1;
        //						nextComma = lineToParse.IndexOf(aComma, currentParamIndex);
        //						if (nextComma == -1) //If we've reached the last aComma
        //							nextComma = lineToParse.Length; //The end of this param is the end of the line
        //					
        //						paramstring = lineToParse.Substring(currentParamIndex, nextComma - currentParamIndex);
        //					
        //						thisWebRoomType.RhsRoomTypesMappedToGroup1.Add(getIntFromString(paramstring));
        //
        //					}
        //					#endregion
        //				}
        //				theseWebRoomTypes.Add(thisWebRoomType);
        //				SetWebRoomType();
        //
        //				#endregion
        //			}
        //			#endregion
        //
        //			#region Gather RHS Rooms Data
        //			for (int rhsRoomCounter = 0; rhsRoomCounter < NumRhsRoomTypes; rhsRoomCounter++)
        //			{
        //				clsRhsRoomType thisRhsRoomType = new clsRhsRoomType();
        //
        //				//Get First line
        //				currentIndex = nextEol + 2;
        //				nextEol = unencryptedData.IndexOf(aCrLf, currentIndex);
        //
        //				if (nextEol == -1)
        //				{
        //					requestSucceeded = false;
        //					errDescription.errNum = 1;
        //					errDescription.errorForUser = "Error Reading Settings file";
        //					errDescription.logFileDescription = "Premature End of line; incorrect format";
        //
        //					LogThis(DateTime.Now, errDescription.errorForUser,
        //						errDescription.logFileDescription, currentFunction, fileName);
        //					return requestSucceeded;
        //
        //				}
        //
        //				lineToParse = unencryptedData.Substring(currentIndex, nextEol - currentIndex);
        //				currentParamIndex = 0;
        //				nextComma = - 1;
        //
        //				for (int headerParamCounter = 0; headerParamCounter < 5; headerParamCounter++)
        //				{
        //					currentParamIndex = nextComma + 1;
        //					nextComma = lineToParse.IndexOf(aComma, currentParamIndex);
        //					if (nextComma == -1) //If we've reached the last aComma
        //						nextComma = lineToParse.Length; //The end of this param is the end of the line
        //					
        //					paramstring = lineToParse.Substring(currentParamIndex, nextComma - currentParamIndex);
        //				
        //					switch (headerParamCounter) 
        //					{
        //							//Strings of up to 30 characters
        //						case 0:
        //							thisRhsRoomType.roomTypeId = getIntFromString(paramstring);
        //							break;
        //						case 1:
        //							thisRhsRoomType.roomTypeName = paramstring;
        //							break;
        //						case 2:
        //							thisRhsRoomType.roomTypeCode = paramstring;
        //							break;
        //						case 3:
        //							if (getBoolFromString(paramstring))
        //								thisRhsRoomType.affectOccupancy = 1;
        //							else
        //								thisRhsRoomType.affectOccupancy = 0;
        //							break;
        //						case 4:
        //							thisRhsRoomType.numberOfRooms = getIntFromString(paramstring);
        //							break;
        //						default:
        //							break;
        //					}
        //
        //				}
        //				theseRhsRoomTypes.Add(thisRhsRoomType);
        //				SetRhsRoomType();
        //
        //
        //			}
        //			#endregion
        //	
        //			//Success!
        //			requestSucceeded = true;
        //
        //			LogThis(DateTime.Now, "Loaded Settings From Registry.", "", currentFunction, "");
        //
        //			return requestSucceeded;
        //		}


        #endregion

        #endregion

        #region Save

        /// <summary>
        /// Saves the current settings in the settings file
        /// </summary>
        public void Save()
        {
            string currentFunction = "Save";
            DateTime thisDateTime = DateTime.Now;

            clsStringEncryption encryption = new clsStringEncryption();

            #region Ensure each Room Type is 'Set' Removed as Registry centric
            //for (int counter = 0; counter < theseWebRoomTypes.Count; counter++)
            //    SetWebRoomType(counter);

            //for(int counter = 0; counter < theseRhsRoomTypes.Count; counter++)
            //    SetRhsRoomType(counter);


            NumWebRoomTypes = theseWebRoomTypes.Count;
            NumRhsRoomTypes = theseRhsRoomTypes.Count;

            #endregion

            #region Save the other Settings

            AddOnlineBookingsIntoRhs = AddOnlineBookingsIntoRhs;
            AddOverbookingProtection = AddOverbookingProtection;
            ConfirmedBookingBehaviour = ConfirmedBookingBehaviour;
            LastBookingReceived = LastBookingReceived;
            MultiRoomBookingsInSameReservation = MultiRoomBookingsInSameReservation;
            RefDateLastUpdate = RefDateLastUpdate;
            RemoteSupportInvites = RemoteSupportInvites;
            ServerCommunicationInterval = ServerCommunicationInterval;
            UpdateWebsiteAvailability = UpdateWebsiteAvailability;
            UpdateUnmapped = UpdateUnmapped;

            #endregion

            #region Save to RhsFolders File

            EnsureRhsFolder(thisRhsFolder);

            #endregion

            #region Save to RwrSettings File

            clsCsvWriter thisCsvWriter;

            #region If the file exists, Set Attributes on it


            try
            {
                if (File.Exists(thisRhsFolder + @"\" + NewSettingsFile))
                    File.SetAttributes(thisRhsFolder + @"\" + NewSettingsFile, FileAttributes.Normal);
            }
            catch (System.UnauthorizedAccessException e)
            {
                LogThis(DateTime.Now, "Error Opening Settings file in: " + thisRhsFolder,
                    e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolder + NewSettingsFile);
                return;
            }
            catch (System.Exception e)
            {
                LogThis(DateTime.Now, "Error Opening Settings file in: " + thisRhsFolder,
                    e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolder + NewSettingsFile);
                return;
            }

            #endregion

            #region Create the file (if it is not already created) and write to it

            try
            {
                thisCsvWriter = new clsCsvWriter(thisRhsFolder + @"\" + NewSettingsFile, false);
            }
            catch (System.UnauthorizedAccessException e)
            {
                LogThis(DateTime.Now, "Error Opening Settings file in: " + thisRhsFolder,
                    e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolder + NewSettingsFile);
                return;
            }
            catch (System.Exception e)
            {
                LogThis(DateTime.Now, "Error Opening Settings file in: " + thisRhsFolder,
                    e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolder + NewSettingsFile);
                return;
            }

            #endregion

            #region Write the header variables

            try
            {
                thisCsvWriter.WriteFields(new
                    object[] {
                                 AddOnlineBookingsIntoRhs.ToString(),
                                 AddOverbookingProtection.ToString(),
                                 ConfirmedBookingBehaviour.ToString(),
                                 Curl,
                                 toWelmanDate(LastAvailabilityUpdate, false),
                                 toWelmanDate(LastBookingReceived, false),
                                 toWelmanDate(LastRhsAvailabilityFileModify, false),
                                 toWelmanDate(LastSuccessfulCommunication, false),
                                 toWelmanDate(LastSettingsChange, false),
                                 Lookahead.ToString(),
                                 MultiRoomBookingsInSameReservation.ToString(),
                                 NumRhsRoomTypes.ToString(),
                                 NumWebRoomTypes.ToString(),
                                 toWelmanDate(RefDate, false),
                                 toWelmanDate(RefDateLastUpdate, false),
                                 encryption.Encrypt(Password),
                                 ServerCommunicationInterval.ToString(),
                                 "",
                                 UpdateWebsiteAvailability.ToString(),
                                 RemoteSupportInvites,
                                 UpdateUnmapped,
                                 "0"
                             });
            }
            catch (System.UnauthorizedAccessException e)
            {
                LogThis(DateTime.Now, "Error Writing to Settings file (System.UnauthorizedAccessException)",
                    e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolder + NewSettingsFile);
                thisCsvWriter.Close();
                return;
            }
            catch (System.Exception e)
            {
                LogThis(DateTime.Now, "Error Writing to Settings file",
                    e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolder + NewSettingsFile);
                thisCsvWriter.Close();
                return;
            }

            #endregion

            #region Write the RHS Room Types
            if (theseRhsRoomTypes == null)
                theseRhsRoomTypes = new ArrayList();


            for (int rhsRoomCounter = 0; rhsRoomCounter < theseRhsRoomTypes.Count; rhsRoomCounter++)
            {

                try
                {
                    thisCsvWriter.WriteFields(new
                        object[] {
                                     ((clsRhsRoomType) theseRhsRoomTypes[rhsRoomCounter]).roomTypeId.ToString(),
                                     ((clsRhsRoomType) theseRhsRoomTypes[rhsRoomCounter]).roomTypeName,
                                     ((clsRhsRoomType) theseRhsRoomTypes[rhsRoomCounter]).roomTypeCode,
                                     ((clsRhsRoomType) theseRhsRoomTypes[rhsRoomCounter]).numberOfRooms.ToString(),
                                     ((clsRhsRoomType) theseRhsRoomTypes[rhsRoomCounter]).affectOccupancy.ToString(),
                                     ((clsRhsRoomType) theseRhsRoomTypes[rhsRoomCounter]).AvailabilityAsDelimitedList.ToString(),
                                     ((clsRhsRoomType) theseRhsRoomTypes[rhsRoomCounter]).AvailabilityAtTimeOfLastUpdateAsDelimitedList.ToString()
                                 });
                }
                catch (System.UnauthorizedAccessException e)
                {
                    LogThis(DateTime.Now, "Error Writing to Settings file (System.UnauthorizedAccessException)",
                        e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolder + NewSettingsFile);
                    thisCsvWriter.Close();
                    return;
                }
                catch (System.Exception e)
                {
                    LogThis(DateTime.Now, "Error Writing to Settings file",
                        e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolder + NewSettingsFile);
                    thisCsvWriter.Close();
                    return;
                }
            }

            #endregion

            #region Write the Web Room Types

            if (theseWebRoomTypes == null)
                theseWebRoomTypes = new ArrayList();

            for (int webRoomCounter = 0; webRoomCounter < theseWebRoomTypes.Count; webRoomCounter++)
            {

                try
                {
                    thisCsvWriter.WriteFields(new
                        object[] {
                                     ((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).webRoomTypeId.ToString(),
                                     ((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).webRoomTypeName,
                                     ((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).IsInterConnecting.ToString(),
                                     ((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).RhsRoomTypesMappedToGroup1AsDelimitedList,
                                     ((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).RhsRoomTypesMappedToGroup2AsDelimitedList,
                                     ((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).maxNumberOfRooms,
                                     ((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).AvailabilityAsDelimitedList,
                                     ((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).AvailabilityAtTimeOfLastUpdateAsDelimitedList,
                    });
                }
                catch (System.UnauthorizedAccessException e)
                {
                    LogThis(DateTime.Now, "Error Writing to Settings file (System.UnauthorizedAccessException)",
                        e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolder + NewSettingsFile);
                    thisCsvWriter.Close();
                    return;
                }
                catch (System.Exception e)
                {
                    LogThis(DateTime.Now, "Error Writing to Settings file",
                        e.ToString() + e.Message + e.StackTrace, currentFunction, thisRhsFolder + NewSettingsFile);
                    thisCsvWriter.Close();
                    return;
                }
            }

            #endregion

            #region Close and Log

            thisCsvWriter.Close();

            LogThis(DateTime.Now, "Settings Saved", "", currentFunction, "");

            #endregion

            #endregion

            #region (old) Save Availability
            //
            //			StreamWriter availabilityFileWriter;
            //			string folderToSaveTo = RhsDataFolder;
            //			if (folderToSaveTo.EndsWith(clsSetting.dataSubFolder))
            //				folderToSaveTo = folderToSaveTo.Substring(0, folderToSaveTo.Length - clsSetting.dataSubFolder.Length - 1);
            //			
            //			#region Write Settings file
            //
            //			try
            //			{
            //				availabilityFileWriter = new StreamWriter(folderToSaveTo + @"\" + availabilityFile, false);
            //			}
            //			catch (System.UnauthorizedAccessException e)
            //			{
            //				LogThis(DateTime.Now, "Error Creating Availability file",
            //					e.ToString() + e.Message + e.StackTrace, currentFunction, folderToSaveTo + availabilityFile);
            //				return;
            //			}
            //			catch (System.Exception e)
            //			{
            //				LogThis(DateTime.Now, "Error Creating Availability file",
            //					e.ToString() + e.Message + e.StackTrace, currentFunction, folderToSaveTo + availabilityFile);
            //				return;
            //			}
            //			
            //			#endregion
            //
            //			General Header data
            //			try
            //			{
            //				availabilityFileWriter.Write("{0},{1},{2}" + aCrLf , 
            //					firstDayValue, numDays,
            //					numRoomTypes);
            //			}
            //			catch (System.UnauthorizedAccessException e)
            //			{
            //				LogThis(DateTime.Now, "Error Writing to Availability file",
            //					e.ToString() + e.Message + e.StackTrace, currentFunction, folderToSaveTo + availabilityFile);
            //				return;
            //			}
            //			catch (System.Exception e)
            //			{
            //				LogThis(DateTime.Now, "Error Writing to Availability file",
            //					e.ToString() + e.Message + e.StackTrace, currentFunction, folderToSaveTo + availabilityFile);
            //				return;
            //			}
            //
            //			for (long dayCounter = 0; dayCounter < numDays; 
            //				dayCounter++)
            //			{
            //				string stringToWrite = "";
            //
            //				for (long roomTypeCounter = 0; roomTypeCounter < 
            //					numRoomTypes; roomTypeCounter++)
            //				{
            //					stringToWrite += 
            //						availabilityAndOccupancy[
            //						roomTypeCounter, dayCounter].available.ToString() + aComma +
            //						availabilityAndOccupancy[
            //						roomTypeCounter, dayCounter].occupied.ToString();				
            //					
            //					if (roomTypeCounter + 1 < numRoomTypes)
            //						stringToWrite += aComma;
            //				}
            //
            //				try
            //				{
            //					availabilityFileWriter.Write(stringToWrite + aCrLf);
            //				}
            //				catch (System.UnauthorizedAccessException e)
            //				{
            //					LogThis(DateTime.Now, "Error Writing to Availability file",
            //						e.ToString() + e.Message + e.StackTrace, currentFunction, folderToSaveTo + availabilityFile);
            //					return;
            //				}
            //				catch (System.Exception e)
            //				{
            //					LogThis(DateTime.Now, "Error Writing to Availability file",
            //						e.ToString() + e.Message + e.StackTrace, currentFunction, folderToSaveTo + availabilityFile);
            //					return;
            //				}
            //			}
            //			availabilityFileWriter.Close();
            //
            #endregion

        }

        #endregion

        #region updateRoomAssociations

        /// <summary>
        ///  Updates Room Associations
        /// </summary>
        /// <returns>Whether this update results in any changes</returns>
        public bool updateRoomAssociations()
        {

            bool changes = false;

            //Check that the room types in the Web Room Type Association lists are associated
            //If not remove them
            if (theseWebRoomTypes == null)
                return changes;

            for (int webRoomTypeCounter = 0; webRoomTypeCounter < theseWebRoomTypes.Count; webRoomTypeCounter++)
            {

                bool changesThisRoomType = false;

                #region Search for Rhs Room Types used in RhsRoomTypesMappedToGroup1; remove those no longer in the system
                if (((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup1 != null)
                {
                    for (int roomAssociationCounter = 0; roomAssociationCounter <
                        ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup1.Count;
                        roomAssociationCounter++)
                    {
                        int thisRhsRoomTypeId = (int)((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup1[roomAssociationCounter];

                        #region Search for this RhsRoomTypeId in list of RhsRoomTypes; remove it if it is not found

                        bool foundRhsRoomType = true;
                        //						if (theseRhsRoomTypes != null)
                        //						{
                        //							for(int RhsRoomTypeCounter = 0; RhsRoomTypeCounter < theseRhsRoomTypes.Count && foundRhsRoomType == false; RhsRoomTypeCounter++)
                        //							{
                        //								if (((clsRhsRoomType) theseRhsRoomTypes[RhsRoomTypeCounter]).roomTypeId == thisRhsRoomTypeId)
                        //									foundRhsRoomType = true;
                        //							}
                        //						}
                        if (thisRhsRoomTypeId > theseRhsRoomTypes.Count)
                            foundRhsRoomType = false;


                        if (!foundRhsRoomType)
                        {
                            ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup1.Remove(
                                ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup1[roomAssociationCounter]);
                            changesThisRoomType = true;
                        }
                        #endregion

                    }

                }
                #endregion

                #region Search for Rhs Room Types used in RhsRoomTypesMappedToGroup2; remove those no longer in the system
                if (((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup2 != null)
                {
                    for (int roomAssociationCounter = 0; roomAssociationCounter <
                        ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup2.Count;
                        roomAssociationCounter++)
                    {
                        int thisRhsRoomTypeId = (int)((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup2[roomAssociationCounter];

                        #region Search for this RhsRoomTypeId in list of RhsRoomTypes; remove it if it is not found

                        bool foundRhsRoomType = false;
                        if (theseRhsRoomTypes != null)
                        {
                            for (int RhsRoomTypeCounter = 0; RhsRoomTypeCounter < theseRhsRoomTypes.Count && foundRhsRoomType == false; RhsRoomTypeCounter++)
                            {
                                if (((clsRhsRoomType)theseRhsRoomTypes[RhsRoomTypeCounter]).roomTypeId == thisRhsRoomTypeId)
                                    foundRhsRoomType = true;
                            }
                        }


                        if (!foundRhsRoomType)
                        {
                            ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup2.Remove(
                                ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup2[roomAssociationCounter]);
                            changesThisRoomType = true;
                        }
                        #endregion

                    }

                }
                #endregion

                #region Update Max number of Rooms
                int totalNumberOfRooms = 0;
                int Group1Total = 0;
                int Group2Total = 0;
                int thisRoomTypeIsInterconnecting = ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).IsInterConnecting;

                #region Deal with Group 1
                foreach (int RhsRoomTypeId in ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup1)
                {
                    bool appearsInGroup2 = false;
                    if (thisRoomTypeIsInterconnecting > 0)
                    {
                        //Check if this Room Type appears in the other Group List
                        foreach (int Group2RhsRoomTypeId in ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup2)
                            if (RhsRoomTypeId == Group2RhsRoomTypeId)
                                appearsInGroup2 = true;
                    }

                    if (appearsInGroup2)
                        Group1Total += (int)(((clsRhsRoomType)theseRhsRoomTypes[RhsRoomTypeId]).numberOfRooms / 2);
                    else
                        Group1Total += ((clsRhsRoomType)theseRhsRoomTypes[RhsRoomTypeId]).numberOfRooms;

                }

                totalNumberOfRooms = Group1Total;

                #endregion

                #region Deal with Group 2
                if (thisRoomTypeIsInterconnecting > 0)
                {
                    foreach (int RhsRoomTypeId in ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup2)
                    {
                        bool appearsInGroup1 = false;
                        //Check if this Room Type appears in the other Group List
                        foreach (int Group1RhsRoomTypeId in ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).RhsRoomTypesMappedToGroup1)
                            if (RhsRoomTypeId == Group1RhsRoomTypeId)
                                appearsInGroup1 = true;

                        if (appearsInGroup1)
                            Group2Total += (int)(((clsRhsRoomType)theseRhsRoomTypes[RhsRoomTypeId]).numberOfRooms / 2);
                        else
                            Group2Total += ((clsRhsRoomType)theseRhsRoomTypes[RhsRoomTypeId]).numberOfRooms;

                    }
                    if (Group2Total < Group1Total)
                        totalNumberOfRooms = Group2Total;


                }
                #endregion

                if (totalNumberOfRooms != ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).maxNumberOfRooms)
                {
                    ((clsWebRoomType)theseWebRoomTypes[webRoomTypeCounter]).maxNumberOfRooms = totalNumberOfRooms;
                    changesThisRoomType = true;
                }

                #endregion

                #region Save Changes

                if (changesThisRoomType)
                {
                    //Not Required					SetWebRoomType(webRoomTypeCounter);
                    changes = true;
                }

                #endregion

            }
            return changes;
        }

        #endregion

        #region Check for changesToSettingsAndAvailability

        /// <summary>
        /// Indicated whether there are changesInRhsOrWebRoomTypes
        /// </summary>
        public bool changesInRhsOrWebRoomTypes = false;

        /// <summary>
        /// Indicates if there are recent changes 
        /// </summary>
        public bool recentChanges = false;

        /// <summary>
        /// Indicates if there are RHS Availability changes
        /// </summary>
        public bool rhsRoomTypeAvailabilityChanges = false;

        /// <summary>
        /// Indicates if there are Web Availability changes
        /// </summary>
        public bool webRoomTypeAvailabilityChanges = false;


        /// <summary>
        /// Checks if there have been any changes to Setings and Availability.
        /// </summary>
        /// <returns>A structure indicating if there have been changes,
        /// and if so what the new settings are</returns>
        public bool changesToSettingsAndAvailability()
        {
            string currentFunction = "changesToSettingsAndAvailability";

            changesInRhsOrWebRoomTypes = false;
            rhsRoomTypeAvailabilityChanges = false;

            //If these are not the same as the existing settings, notify the container
            if (LastSettingsChange > LastAvailabilityUpdate)
            {
                changesInRhsOrWebRoomTypes = true;
            }

            //Check if availabiity is available!
            if (!getRhsAvailabilityData())
            {
                return false;
            }

            //Deal with Room Types first:
            changesInRhsOrWebRoomTypes = !updateRoomTypesSettings() && changesInRhsOrWebRoomTypes;

            changesInRhsOrWebRoomTypes = !updateRoomAssociations() && changesInRhsOrWebRoomTypes;

            if (lastAvailabilityChange > LastAvailabilityUpdate)
                rhsRoomTypeAvailabilityChanges = true;

            #region Ensure Availability Arrays are initialised

            foreach (object thisObject in theseRhsRoomTypes)
            {
                clsRhsRoomType thisRhsRoomType = (clsRhsRoomType)thisObject;
                if (thisRhsRoomType.Availability == null)
                    thisRhsRoomType.Availability = new ArrayList();
            }


            foreach (object thisObject in theseWebRoomTypes)
            {
                clsWebRoomType thisWebRoomType = (clsWebRoomType)thisObject;
                if (thisWebRoomType.Availability == null)
                    thisWebRoomType.Availability = new ArrayList();
            }

            #endregion

            //CheckingAvailability("Check Complete", 100, "");

            #region Calculate Start and End Dates

            int startVal = 0;

            clsDaysBetween tempDaysBetween = daysBetween(RefDate,
                firstDayValue);

            if (tempDaysBetween.errDescription.success)
                startVal = (int)tempDaysBetween.daysBetween - 1;
            else
            {
                LogThis(DateTime.Now, "startVal out of Range",
                    tempDaysBetween.errDescription.errorForUser,
                    currentFunction,
                    "RefDate = " + RefDate.ToString()
                    + " firstDayValue= " + firstDayValue.ToString());

                startVal = 0;
            }

            //endVal is the number of days worth of data to send. This is either
            //the lookahead value specified or
            //the number of days worth of data left in the database subsequent to the reference date
            //whichever is the smallest.
            long endVal = Lookahead + 1;
            //			if (endVal > (numDays + startVal))
            //				endVal = numDays + startVal;

            #endregion

            #region Initialise Logging
            DateTime thisDate = DateTime.Now;

            //			string thisLogFileName = GetBaseAppFolder() + logFileName + toWelmanDate(thisDate, true) + "Availability" + rhsWebRoomsExtension;
            //
            //			//Add new data to Log (Create file if necessary)
            //			StreamWriter log;
            //
            //			DateTime thisNewDateTime = new DateTime(2001,1,1);
            //
            //			if (firstDayValue != 0)
            //				thisNewDateTime = new DateTime((int) (firstDayValue / 1300),
            //					(int) (firstDayValue / 100) % 13,
            //					(int) (firstDayValue % 100));
            //			
            //			try
            //			{
            //				log = new StreamWriter(thisLogFileName, true);
            //				log.Write("changesToSettingsAndAvailability" + aCrLf);
            //				log.Write("Now, firstDayValue,firstDayAsDate,startVal,endVal" + aCrLf);
            //
            //				log.Write("{0},{1},{2},{3},{4}" + aCrLf, 
            //					DateTime.Now.ToString(), 
            //					firstDayValue, 
            //					thisNewDateTime.ToString(),
            //					startVal.ToString(),
            //					endVal.ToString());
            //				log.Close();
            //			}
            //			catch (System.UnauthorizedAccessException)
            //			{
            //			}
            //			catch (System.Exception)
            //			{
            //			}

            #endregion

            if (firstDayValue == 0)
                rhsRoomTypeAvailabilityChanges = true;
            else
            {

                #region Sort out availability for whole inventory
                for (int dateIndex = 0; dateIndex < endVal; dateIndex++)
                {
                    int roomTypeCounter = 1;

                    #region Establish availabiity for RHS Room Types from availabilityAndOccupancy array

                    if (dateIndex < startVal || dateIndex - startVal > availabilityAndOccupancy.GetUpperBound(1))
                    {
                        #region no data for this day: assume full occupancy
                        foreach (object thisObject in theseRhsRoomTypes)
                        {
                            clsRhsRoomType thisRhsRoomType = (clsRhsRoomType)thisObject;
                            if (thisRhsRoomType.Availability.Count <= dateIndex)
                            {
                                //Adding
                                thisRhsRoomType.Availability.Add(thisRhsRoomType.numberOfRooms);
                                rhsRoomTypeAvailabilityChanges = true;

                                #region Log

                                //								string thisChange = "Addition with Full Availability,"
                                //									+ roomTypeCounter.ToString() + ","
                                //									+ dateIndex.ToString() + ","
                                //									+ startVal.ToString() + ","
                                //									+ thisRhsRoomType.numberOfRooms.ToString() 
                                //									+ aCrLf;
                                //
                                //								try
                                //								{
                                //									log = new StreamWriter(thisLogFileName, true);
                                //									log.Write(thisChange);
                                //									log.Close();
                                //								}
                                //								catch (System.UnauthorizedAccessException)
                                //								{
                                //								}
                                //								catch (System.Exception)
                                //								{
                                //								}

                                #endregion

                            }
                            else
                            {
                                //Editing
                                if ((int)thisRhsRoomType.Availability[dateIndex] !=
                                    thisRhsRoomType.numberOfRooms)
                                {
                                    //A change

                                    #region Log

                                    //									string thisChange = "Edition with Full Availability,"
                                    //										+ roomTypeCounter.ToString() + ","
                                    //										+ dateIndex.ToString() + ","
                                    //										+ startVal.ToString() + ","
                                    //										+ thisRhsRoomType.Availability[dateIndex].ToString() + ","
                                    //										+ thisRhsRoomType.numberOfRooms.ToString() 
                                    //										+ aCrLf;
                                    //
                                    //									try
                                    //									{
                                    //										log = new StreamWriter(thisLogFileName, true);
                                    //										log.Write(thisChange);
                                    //										log.Close();
                                    //									}
                                    //									catch (System.UnauthorizedAccessException)
                                    //									{
                                    //									}
                                    //									catch (System.Exception)
                                    //									{
                                    //									}

                                    #endregion

                                    thisRhsRoomType.Availability[dateIndex] = thisRhsRoomType.numberOfRooms;
                                    rhsRoomTypeAvailabilityChanges = true;


                                }
                            }
                            roomTypeCounter++;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Sort Availability for each RHS Room Type

                        foreach (object thisObject in theseRhsRoomTypes)
                        {
                            clsRhsRoomType thisRhsRoomType = (clsRhsRoomType)thisObject;
                            if (thisRhsRoomType.Availability.Count <= dateIndex)
                            {
                                //Adding
                                if (roomTypeCounter <= availabilityAndOccupancy.GetUpperBound(0))
                                {
                                    thisRhsRoomType.Availability.Add(
                                        availabilityAndOccupancy[roomTypeCounter, dateIndex - startVal].available);
                                    rhsRoomTypeAvailabilityChanges = true;

                                    #region Log

                                    string thisChange = "Addition with Availability from Raw,"
                                        + roomTypeCounter.ToString() + ","
                                        + dateIndex.ToString() + ","
                                        + startVal.ToString() + ","
                                        + availabilityAndOccupancy[roomTypeCounter, dateIndex - startVal].available.ToString()
                                        + aCrLf;

                                    //									try
                                    //									{
                                    //										log = new StreamWriter(thisLogFileName, true);
                                    //										log.Write(thisChange);
                                    //										log.Close();
                                    //									}
                                    //									catch (System.UnauthorizedAccessException)
                                    //									{
                                    //									}
                                    //									catch (System.Exception)
                                    //									{
                                    //									}

                                    #endregion

                                }
                            }
                            else
                            {
                                //Editing
                                if (roomTypeCounter <= availabilityAndOccupancy.GetUpperBound(0)
                                    && dateIndex - startVal >= availabilityAndOccupancy.GetLowerBound(1)
                                    )
                                {
                                    //									//When within range, update from availabilityAndOccupancy. Otherwise, set to maximum
                                    //									if (dateIndex - startVal <= availabilityAndOccupancy.GetUpperBound(1))
                                    //									{

                                    if ((int)thisRhsRoomType.Availability[dateIndex] !=
                                        availabilityAndOccupancy[roomTypeCounter, dateIndex - startVal].available)
                                    {
                                        //A change
                                        #region Log

                                        string thisChange = "Edition with Availability from Raw,"
                                            + roomTypeCounter.ToString() + ","
                                            + dateIndex.ToString() + ","
                                            + startVal.ToString() + ","
                                            + thisRhsRoomType.Availability[dateIndex].ToString() + ","
                                            + availabilityAndOccupancy[roomTypeCounter, dateIndex - startVal].available.ToString()
                                            + aCrLf;

                                        //											try
                                        //											{
                                        //												log = new StreamWriter(thisLogFileName, true);
                                        //												log.Write(thisChange);
                                        //												log.Close();
                                        //											}
                                        //											catch (System.UnauthorizedAccessException)
                                        //											{
                                        //											}
                                        //											catch (System.Exception)
                                        //											{
                                        //											}

                                        #endregion

                                        thisRhsRoomType.Availability[dateIndex] =
                                            availabilityAndOccupancy[roomTypeCounter, dateIndex - startVal].available;
                                        rhsRoomTypeAvailabilityChanges = true;


                                    }
                                    //									}
                                    //									else
                                    //									{
                                    //										if ((int) thisRhsRoomType.Availability[dateIndex] != 
                                    //											thisRhsRoomType.numberOfRooms)
                                    //										{
                                    //											//A change
                                    //											thisRhsRoomType.Availability[dateIndex] = 
                                    //												thisRhsRoomType.numberOfRooms;
                                    //											rhsRoomTypeAvailabilityChanges = true;
                                    //										}
                                    //									}
                                }
                            }
                            roomTypeCounter++;
                        }
                        #endregion
                    }

                    #endregion

                    #region Sort out Availability for Web Room Types from RHS Room Types

                    #region Get total number of rooms Availabile for this night at the property
                    int totalNumberOfRoomsAvailableThisNight = 0;
                    //Newly added; if we have enabled Overbooking Protection, ensure the 
                    // total number of rooms we make bookable is less than or equal to the TOTAL
                    // number of rooms available in the whole establishment
                    if (AddOverbookingProtection)
                    {
                        foreach (object thisObject in theseRhsRoomTypes)
                        {
                            clsRhsRoomType thisRhsRoomType = (clsRhsRoomType)thisObject;
                            if (thisRhsRoomType.affectOccupancy != 0 && thisRhsRoomType.Availability.Count > dateIndex)
                                totalNumberOfRoomsAvailableThisNight += (int)thisRhsRoomType.Availability[dateIndex];
                        }
                    }

                    #endregion

                    #region Get number of rooms of this Web Room Type Available for this night
                    foreach (object thisObject in theseWebRoomTypes)
                    {
                        clsWebRoomType thisWebRoomType = (clsWebRoomType)thisObject;

                        #region Get Acutal Availability

                        int totalNumberOfRoomsAvailableTheWebRoomType = 0;
                        int Group1Total = 0;
                        int Group2Total = 0;
                        int thisRoomTypeIsInterconnecting = thisWebRoomType.IsInterConnecting;

                        #region Deal with Group 1
                        foreach (int RhsRoomTypeId in thisWebRoomType.RhsRoomTypesMappedToGroup1)
                        {
                            bool appearsInGroup2 = false;
                            if (thisRoomTypeIsInterconnecting > 0)
                            {
                                //Check if this Room Type appears in the other Group List
                                foreach (int Group2RhsRoomTypeId in thisWebRoomType.RhsRoomTypesMappedToGroup2)
                                    if (RhsRoomTypeId == Group2RhsRoomTypeId)
                                        appearsInGroup2 = true;


                            }

                            #region Find the Rhs Room Type

                            clsRhsRoomType thisRhsRoomType = new clsRhsRoomType();
                            bool foundRhsRoomType = true;

                            if (RhsRoomTypeId < theseRhsRoomTypes.Count)
                                thisRhsRoomType = (clsRhsRoomType)theseRhsRoomTypes[RhsRoomTypeId];
                            else
                                foundRhsRoomType = false;



                            #endregion

                            #region Set availability

                            if (foundRhsRoomType)
                            {
                                if (appearsInGroup2)
                                    Group1Total += (int)((int)thisRhsRoomType.Availability[dateIndex] / 2);
                                else
                                    Group1Total += (int)thisRhsRoomType.Availability[dateIndex];
                            }
                            #endregion
                        }

                        totalNumberOfRoomsAvailableTheWebRoomType = Group1Total;

                        #endregion

                        #region Deal with Group 2
                        if (thisRoomTypeIsInterconnecting > 0)
                        {
                            foreach (int RhsRoomTypeId in thisWebRoomType.RhsRoomTypesMappedToGroup2)
                            {
                                bool appearsInGroup1 = false;
                                //Check if this Room Type appears in the other Group List
                                foreach (int Group1RhsRoomTypeId in thisWebRoomType.RhsRoomTypesMappedToGroup1)
                                    if (RhsRoomTypeId == Group1RhsRoomTypeId)
                                        appearsInGroup1 = true;

                                clsRhsRoomType thisRhsRoomType = new clsRhsRoomType();

                                if (!appearsInGroup1)
                                {

                                    if (RhsRoomTypeId < theseRhsRoomTypes.Count)
                                    {
                                        thisRhsRoomType = (clsRhsRoomType)theseRhsRoomTypes[RhsRoomTypeId];
                                        Group2Total += (int)((clsRhsRoomType)theseRhsRoomTypes[RhsRoomTypeId]).Availability[dateIndex];
                                    }
                                }
                                else
                                {
                                    if (RhsRoomTypeId < theseRhsRoomTypes.Count)
                                    {
                                        thisRhsRoomType = (clsRhsRoomType)theseRhsRoomTypes[RhsRoomTypeId];
                                        Group2Total += ((int)((clsRhsRoomType)theseRhsRoomTypes[RhsRoomTypeId]).Availability[dateIndex] / 2);
                                    }
                                }
                            }

                            if (Group2Total < Group1Total)
                                totalNumberOfRoomsAvailableTheWebRoomType = Group2Total;


                        }
                        #endregion

                        #endregion

                        #region Modify this with the Overbooking Auto Protection

                        //If we've got overbooking protection, check if the occupancy we're going to show is high enough
                        if (AddOverbookingProtection && totalNumberOfRoomsAvailableTheWebRoomType > totalNumberOfRoomsAvailableThisNight)
                            totalNumberOfRoomsAvailableTheWebRoomType = totalNumberOfRoomsAvailableThisNight;


                        //Don't allow overbooking of Rooms (RHS allows this), and there probably aren't 
                        //too many reasons why we shouldn't allow it, but the site as its set up will
                        //give us Error 48 if we do. Send 0 availability instead.
                        if (totalNumberOfRoomsAvailableTheWebRoomType < 0)
                            totalNumberOfRoomsAvailableTheWebRoomType = 0;


                        #endregion

                        #region Add to Web Room Type Availability
                        if (thisWebRoomType.Availability.Count <= dateIndex)
                        {
                            //Adding
                            thisWebRoomType.Availability.Add(totalNumberOfRoomsAvailableTheWebRoomType);
                            webRoomTypeAvailabilityChanges = true;
                        }
                        else
                        {
                            //Editing
                            if ((int)thisWebRoomType.Availability[dateIndex] != totalNumberOfRoomsAvailableTheWebRoomType)
                            {
                                //A change
                                thisWebRoomType.Availability[dateIndex] = totalNumberOfRoomsAvailableTheWebRoomType;
                                webRoomTypeAvailabilityChanges = true;
                            }
                        }

                        #endregion

                    }
                    #endregion


                    #endregion

                }
                #endregion
            }

            #region Logging

            //			try
            //			{
            //				log = new StreamWriter(thisLogFileName, true);
            //				
            //				if (changesInRhsOrWebRoomTypes)
            //					log.Write("There were changesInRhsOrWebRoomTypes" + aCrLf);
            //				else
            //					log.Write("There were NO changesInRhsOrWebRoomTypes" + aCrLf);
            //
            //				log.Write("Current Availability: RHS Room Types" + aCrLf);
            //
            //				foreach(object thisObject in theseRhsRoomTypes)
            //				{
            //					clsRhsRoomType thisRhsRoomType = (clsRhsRoomType) thisObject;
            //					log.Write("RHS Room Type: " + thisRhsRoomType.roomTypeId.ToString() +  aCrLf);
            //					log.Write(thisRhsRoomType.AvailabilityAsDelimitedList + aCrLf);
            //				}
            //
            //				log.Write("Current Availability: Web Room Types" + aCrLf);
            //
            //				foreach(object thisObject in theseWebRoomTypes)
            //				{
            //					clsWebRoomType thisWebRoomType = (clsWebRoomType) thisObject;
            //					log.Write("Web Room Type: " + thisWebRoomType.webRoomTypeId.ToString() +  aCrLf);
            //					log.Write(thisWebRoomType.AvailabilityAsDelimitedList + aCrLf);
            //				}
            //
            //				log.Close();
            //			}
            //			catch (System.UnauthorizedAccessException)
            //			{
            //			}
            //			catch (System.Exception)
            //			{
            //			}


            #endregion

            Save();

            return (changesInRhsOrWebRoomTypes || recentChanges || rhsRoomTypeAvailabilityChanges);
        }

        #endregion

        #region Availability things

        #region getRawRHSData

        /// <summary>
        /// As quickly as possible, get data from data files
        /// </summary>
        /// <param name="dataToGet">files to get data from</param>
        /// <returns>Data structure containing the unprocessed information</returns>
        public bool getRawRHSData(rhsFileTypes[] dataToGet)
        {
            string currentFunction = "getRawRHSData";

            DateTime dtValidationDate = DateTime.Now.AddDays(-7);
            int masterValueValidationDate = FromDateToMasterValue(dtValidationDate);

            bool success = false;

            rhsFiles = dataToGet;

            Decoder d = Encoding.UTF8.GetDecoder();

            //FileStream dataFileStream;
            //StreamReader sr;
            string totalRecord;
            byte[] unicodeBytes;

            int lastProgressUpdate = 0;
            //CheckingAvailability("", lastProgressUpdate, "");

            int recordOffset = 0;


            byte[] fullFile;

            for (int fileCounter = 0; fileCounter < numRhsFiles; fileCounter++)
            {
                //See if we need to access the file
                if (!rhsFiles[fileCounter].currentDataRetrieved)
                {
                    #region See if the file exists

                    if (!File.Exists(RhsDataFolder + (rhsFiles[fileCounter].fileName).ToString()))
                    {
                        LogThis(DateTime.Now, "File Not Found",
                            rhsFiles[fileCounter].fileName, currentFunction, rhsFiles[fileCounter].fileName);
                        rhsFiles[fileCounter].currentDataRetrieved = false;
                        return success;
                    }

                    #endregion

                    #region See if we can access the file

                    try
                    {

                        //dataFileStream = new FileStream(RhsDataFolder + (rhsFiles[fileCounter].fileName).ToString(), 
                        //    FileMode.Open, FileAccess.Read, FileShare.None);
                        //sr = new StreamReader(dataFileStream);

                        #region Read the file here, quickly

                        fullFile = File.ReadAllBytes(RhsDataFolder + (rhsFiles[fileCounter].fileName));

                        unicodeBytes = Encoding.Convert(ascii, unicode, fullFile);
                        totalRecord = unicode.GetString(unicodeBytes);

                        #endregion
                    }
                    catch (System.UnauthorizedAccessException e)
                    {
                        LogThis(DateTime.Now, "Error Opening File",
                            e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                        rhsFiles[fileCounter].currentDataRetrieved = false;
                        return success;
                    }
                    catch (System.Exception e)
                    {
                        LogThis(DateTime.Now, "Error Opening File",
                            e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                        rhsFiles[fileCounter].currentDataRetrieved = false;
                        return success;
                    }

                    #endregion

                    #region we can; lets get the data out of it

                    //rhsFiles[fileCounter].numRecords = Convert.ToInt64(dataFileStream.Length / rhsFiles[fileCounter].recordLength);
                    //byte[] tempBytes = new byte[rhsFiles[fileCounter].recordLength];

                    //                    byte[] tempBytes = new byte[rhsFiles[fileCounter].recordLength];

                    rhsFiles[fileCounter].numRecords = Convert.ToInt64(fullFile.Length / rhsFiles[fileCounter].recordLength);


                    #endregion

                    #region Try to go to the start

                    //try
                    //{
                    //    dataFileStream.Seek(0, SeekOrigin.Begin);
                    //}
                    //catch (System.UnauthorizedAccessException e)
                    //{
                    //    LogThis(DateTime.Now, "Error Opening File",
                    //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                    //    dataFileStream.Close();
                    //    sr.Close();
                    //    rhsFiles[fileCounter].currentDataRetrieved = false;
                    //    return success;
                    //}
                    //catch (System.Exception e)
                    //{
                    //    LogThis(DateTime.Now, "Error Opening File",
                    //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                    //    dataFileStream.Close();
                    //    sr.Close();
                    //    rhsFiles[fileCounter].currentDataRetrieved = false;
                    //    return success;
                    //}

                    #endregion

                    #region Do work for each file

                    switch ((rhsRawFileType)fileCounter)
                    {
                        case rhsRawFileType.rooms0Dat:

                            #region roomData0

                            roomData0 = new ArrayList();
                            rhsFiles[fileCounter].maxima = 0;

                            for (int recordCounter = 0; recordCounter < rhsFiles[fileCounter].numRecords; recordCounter++)
                            {
                                #region Attempt to read from the file

                                //try
                                //{
                                //    dataFileStream.Read(tempBytes, 0, rhsFiles[fileCounter].recordLength);

                                //}
                                //catch (System.UnauthorizedAccessException e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}
                                //catch (System.Exception e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}

                                #endregion

                                //byte[] unicodeBytes = Encoding.Convert(ascii, unicode, tempBytes); 

                                recordOffset = recordCounter * rhsFiles[fileCounter].recordLength;

                                clsRooms0Dat thisRooms0Dat = new clsRooms0Dat();

                                thisRooms0Dat.number = bufferToShort(fullFile, recordOffset + 0);
                                thisRooms0Dat.Index = recordCounter + 1;

                                thisRooms0Dat.inUse = false;

                                if (thisRooms0Dat.number > 0)
                                    thisRooms0Dat.inUse = true;

                                if (thisRooms0Dat.inUse && thisRooms0Dat.number > rhsFiles[fileCounter].maxima)
                                    rhsFiles[fileCounter].maxima = thisRooms0Dat.number;

                                roomData0.Add(thisRooms0Dat);

                            }

                            //lastProgressUpdate = updateProgress(16, lastProgressUpdate);

                            rhsFiles[fileCounter].minima = 0;

                            rhsFiles[fileCounter].currentDataRetrieved = true;

                            #endregion

                            break;
                        case rhsRawFileType.rmtype1Dat:

                            #region roomTypeData
                            roomTypeData = new ArrayList();

                            for (int recordCounter = 0; recordCounter < rhsFiles[fileCounter].numRecords; recordCounter++)
                            {
                                #region Attempt to read from the file

                                //try
                                //{
                                //    dataFileStream.Read(tempBytes, 0, rhsFiles[fileCounter].recordLength);
                                //}
                                //catch (System.UnauthorizedAccessException e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}
                                //catch (System.Exception e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}

                                #endregion

                                //byte[] unicodeBytes = Encoding.Convert(ascii, unicode, tempBytes);
                                //totalRecord = unicode.GetString(unicodeBytes);

                                recordOffset = recordCounter * rhsFiles[fileCounter].recordLength;

                                clsRhsRoomType thisRhsRoomType = new clsRhsRoomType();

                                thisRhsRoomType.roomTypeName = SanitiseString(totalRecord.Substring(recordOffset + 0, 20));
                                thisRhsRoomType.roomTypeCode = SanitiseString(totalRecord.Substring(recordOffset + 52, 4));


                                if (totalRecord.Substring(recordOffset + 56, 1).Trim().ToUpper() == "Y")
                                    thisRhsRoomType.affectOccupancy = 1;
                                else
                                    thisRhsRoomType.affectOccupancy = 0;

                                thisRhsRoomType.roomTypeId = recordCounter;
                                roomTypeData.Add(thisRhsRoomType);

                            }
                            rhsFiles[fileCounter].minima = 0;
                            rhsFiles[fileCounter].maxima = rhsFiles[fileCounter].numRecords;
                            
                            rhsFiles[fileCounter].currentDataRetrieved = true;
                            #endregion

                            break;
                        case rhsRawFileType.rooms1Dat:

                            #region roomData

                            roomData = new ArrayList();
                            rhsFiles[fileCounter].maxima = 0;

                            //byte[] unicodeBytes = Encoding.Convert( ascii, unicode, fullFile);
                            //totalRecord = unicode.GetString(unicodeBytes);

                            for (int recordCounter = 0; recordCounter < rhsFiles[fileCounter].numRecords; recordCounter++)
                            {

                                #region Attempt to read from the file

                                //try
                                //{
                                //    dataFileStream.Read(tempBytes, 0, rhsFiles[fileCounter].recordLength);
                                //}
                                //catch (System.UnauthorizedAccessException e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}
                                //catch (System.Exception e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}

                                #endregion

                                //byte[] unicodeBytes = Encoding.Convert( ascii, unicode, tempBytes);

                                recordOffset = recordCounter * rhsFiles[fileCounter].recordLength;

                                clsRhsRoomDataFile thisRhsRoomDataFile = new clsRhsRoomDataFile();

                                thisRhsRoomDataFile.number = bufferToShort(fullFile, recordOffset + 87);

                                thisRhsRoomDataFile.aliasSt = SanitiseString(totalRecord.Substring(recordOffset + 119, 8)).Trim();

                                if (isNumerical(thisRhsRoomDataFile.aliasSt))
                                    thisRhsRoomDataFile.alias = Convert.ToInt32(thisRhsRoomDataFile.aliasSt);
                                else
                                    thisRhsRoomDataFile.alias = thisRhsRoomDataFile.number;


                                //thisRhsRoomDataFile.alias = unicode.GetString(unicodeBytes).Substring(119, 3).Trim();
                                thisRhsRoomDataFile.roomType = bufferToShort(fullFile, recordOffset + 0);

                                //Check Occupancy byte if this is "Y" then set this to true
                                //                                thisRhsRoomDataFile.affectOccupancyFromRooms1Dat = unicode.GetString(unicodeBytes).Substring(127, 1).Trim().ToUpper();
                                thisRhsRoomDataFile.affectOccupancyFromRooms1Dat = SanitiseString(totalRecord.Substring(recordOffset + 127, 1));

                                //                                string Occupancy = totalRecord.Substring(recordOffset + 86, 1).Trim().ToUpper();

                                #region Old "fixing" of Number and Room Types

                                //if (thisRhsRoomDataFile.number == 0)
                                //    thisRhsRoomDataFile.number = 1;

                                //if (thisRhsRoomDataFile.roomType == 0)
                                //    thisRhsRoomDataFile.roomType = 1;

                                #endregion

                                #region New "fixing" of Number and Room Types

                                thisRhsRoomDataFile.affectOccupancy = true;


                                if (thisRhsRoomDataFile.number == 0 || thisRhsRoomDataFile.roomType == 0)
                                {
                                    thisRhsRoomDataFile.affectOccupancy = false;
                                }

                                #endregion

                                //Previously using recordCounter. Changed to RoomNumber instead!

                                //if (roomData0.Count < recordCounter + 1
                                //    || (roomData0.Count > recordCounter 
                                //    && ((clsRooms0Dat)roomData0[recordCounter]).inUse))
                                //{


                                if (thisRhsRoomDataFile.affectOccupancy
                                    && thisRhsRoomDataFile.number + 1 < roomData0.Count
                                    && thisRhsRoomDataFile.number > -1
                                    && ((clsRooms0Dat)roomData0[thisRhsRoomDataFile.number - 1]).inUse)
                                {
                                    thisRhsRoomDataFile.affectOccupancy = true;
                                    thisRhsRoomDataFile.affectOccupancyFromRooms0Dat = true;

                                    #region If RoomType is in range, add up number of rooms of each type

                                    if (thisRhsRoomDataFile.roomType - 1 < roomTypeData.Count)
                                        ((clsRhsRoomType)roomTypeData[thisRhsRoomDataFile.roomType - 1]).numberOfRooms++;
                                    else
                                    {
                                        thisRhsRoomDataFile.affectOccupancy = false;
                                        thisRhsRoomDataFile.affectOccupancyFromRooms0Dat = false;
                                    }

                                    #endregion
                                }
                                else
                                {
                                    thisRhsRoomDataFile.affectOccupancy = false;
                                    thisRhsRoomDataFile.affectOccupancyFromRooms0Dat = false;
                                }


                                ////if (Occupancy == "U")
                                ////{
                                //    if (roomData0.Count < thisRhsRoomDataFile.number + 1
                                //        || (roomData0.Count > thisRhsRoomDataFile.number
                                //        && ((clsRooms0Dat)roomData0[thisRhsRoomDataFile.number - 1]).inUse))
                                //    {
                                //        thisRhsRoomDataFile.affectOccupancy = true;
                                //        //Count number of rooms of each type
                                //        ((clsRhsRoomType)roomTypeData[thisRhsRoomDataFile.roomType - 1]).numberOfRooms++;
                                //    }
                                //    else
                                //        thisRhsRoomDataFile.affectOccupancy = false;
                                ////}
                                ////else
                                ////    thisRhsRoomDataFile.affectOccupancy = false;

                                // Check both alias and number
                                if (thisRhsRoomDataFile.number > rhsFiles[fileCounter].maxima)
                                    rhsFiles[fileCounter].maxima = thisRhsRoomDataFile.number;

                                if (thisRhsRoomDataFile.alias > rhsFiles[fileCounter].maxima)
                                    rhsFiles[fileCounter].maxima = thisRhsRoomDataFile.alias;


                                roomData.Add(thisRhsRoomDataFile);

                            }

                            //lastProgressUpdate = updateProgress(16, lastProgressUpdate);

                            rhsFiles[fileCounter].minima = 0;

                            rhsFiles[fileCounter].currentDataRetrieved = true;

                            #endregion

                            break;
                        case rhsRawFileType.statafftIdm:

                            #region primaryBookingData

                            primaryBookingData = new ArrayList();

                            for (int recordCounter = 0; recordCounter < rhsFiles[fileCounter].numRecords; recordCounter++)
                            {
                                #region Attempt to read from the file

                                //try
                                //{
                                //    dataFileStream.Read(tempBytes, 0, rhsFiles[fileCounter].recordLength);
                                //}
                                //catch (System.UnauthorizedAccessException e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}
                                //catch (System.Exception e)
                                //{
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}

                                #endregion

                                recordOffset = recordCounter * rhsFiles[fileCounter].recordLength;

                                clsRhsPrimaryOccupancyDataFile thisRhsPrimaryOccupancyDataFile = new clsRhsPrimaryOccupancyDataFile();

                                int thisDateValue = bufferToInt(fullFile, recordOffset + 0);

                                if (FromMasterValueToDateTime(thisDateValue) != new DateTime(1, 1, 1))
                                    thisRhsPrimaryOccupancyDataFile.dateValue = thisDateValue;
                                else
                                    thisRhsPrimaryOccupancyDataFile.dateValue = masterValueValidationDate;

                                thisRhsPrimaryOccupancyDataFile.firstSecondaryRef = bufferToInt(fullFile, recordOffset + 4);
                                thisRhsPrimaryOccupancyDataFile.lastSecondaryRef = bufferToInt(fullFile, recordOffset + 8);
                                thisRhsPrimaryOccupancyDataFile.numOfSecondaryRefs = bufferToInt(fullFile, recordOffset + 12);
                                primaryBookingData.Add(thisRhsPrimaryOccupancyDataFile);

                            }

                            //lastProgressUpdate = updateProgress(24, lastProgressUpdate);

                            rhsFiles[fileCounter].minima = ((clsRhsPrimaryOccupancyDataFile)primaryBookingData[0]).dateValue;
                            rhsFiles[fileCounter].maxima =
                                ((clsRhsPrimaryOccupancyDataFile)primaryBookingData[(int)rhsFiles[fileCounter].numRecords - 1]).dateValue;

                            rhsFiles[fileCounter].currentDataRetrieved = true;

                            #endregion

                            break;
                        case rhsRawFileType.statafftIds:

                            #region secondaryBookingData
                            secondaryBookingData = new ArrayList();

                            for (int recordCounter = 0; recordCounter < rhsFiles[fileCounter].numRecords; recordCounter++)
                            {
                                #region Attempt to read from the file

                                //try
                                //{
                                //    dataFileStream.Read(tempBytes, 0, rhsFiles[fileCounter].recordLength);
                                //}
                                //catch (System.UnauthorizedAccessException e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}
                                //catch (System.Exception e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}

                                #endregion

                                recordOffset = recordCounter * rhsFiles[fileCounter].recordLength;

                                clsRhsSecondaryOccupancyDataFile thisRhsSecondaryOccupancyDataFile = new clsRhsSecondaryOccupancyDataFile();

                                int thisDateValue = bufferToInt(fullFile, recordOffset + 0);

                                if (FromMasterValueToDateTime(thisDateValue) != new DateTime(1, 1, 1))
                                    thisRhsSecondaryOccupancyDataFile.dateValue = thisDateValue;
                                else
                                    thisRhsSecondaryOccupancyDataFile.dateValue = masterValueValidationDate;

                                //thisRhsSecondaryOccupancyDataFile.dateValue = bufferToInt(fullFile, recordOffset + 0);
                                thisRhsSecondaryOccupancyDataFile.nextSecondaryRef = bufferToInt(fullFile, recordOffset + 4);
                                thisRhsSecondaryOccupancyDataFile.prevSecondaryRef = bufferToInt(fullFile, recordOffset + 8);
                                thisRhsSecondaryOccupancyDataFile.tertiaryRef = bufferToInt(fullFile, recordOffset + 12);
                                secondaryBookingData.Add(thisRhsSecondaryOccupancyDataFile);
                            }

                            rhsFiles[fileCounter].minima = ((clsRhsSecondaryOccupancyDataFile)secondaryBookingData[0]).dateValue;
                            rhsFiles[fileCounter].maxima =
                                ((clsRhsSecondaryOccupancyDataFile)secondaryBookingData[(int)rhsFiles[fileCounter].numRecords - 1]).dateValue;


                            rhsFiles[fileCounter].currentDataRetrieved = true;

                            #endregion

                            break;
                        case rhsRawFileType.statafftDat:

                            #region tertiaryBookingData

                            tertiaryBookingData = new ArrayList();

                            rhsFiles[fileCounter].minima = 0;
                            for (int recordCounter = 0; recordCounter < rhsFiles[fileCounter].numRecords; recordCounter++)
                            {
                                #region Attempt to read from the file

                                //try
                                //{
                                //    dataFileStream.Read(tempBytes, 0, rhsFiles[fileCounter].recordLength);
                                //}
                                //catch (System.UnauthorizedAccessException e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}
                                //catch (System.Exception e)
                                //{
                                //    LogThis(DateTime.Now, "Error Reading File",
                                //        e.ToString() + e.Message + e.StackTrace, currentFunction, rhsFiles[fileCounter].fileName);
                                //    dataFileStream.Close();
                                //    sr.Close();
                                //    rhsFiles[fileCounter].currentDataRetrieved = false;
                                //    return success;
                                //}

                                #endregion

                                //byte[] unicodeBytes = Encoding.Convert( ascii, unicode, tempBytes);
                                //totalRecord = unicode.GetString(unicodeBytes);

                                recordOffset = recordCounter * rhsFiles[fileCounter].recordLength;

                                clsRhsTertiaryOccupancyDataFile thisRhsTertiaryOccupancyDataFile = new clsRhsTertiaryOccupancyDataFile();

                                int thisDateValue = bufferToInt(fullFile, recordOffset + 0);

                                if (FromMasterValueToDateTime(thisDateValue) != new DateTime(1, 1, 1))
                                    thisRhsTertiaryOccupancyDataFile.startDateValue = thisDateValue;
                                else
                                    thisRhsTertiaryOccupancyDataFile.startDateValue = masterValueValidationDate;

                                thisDateValue = bufferToInt(fullFile, recordOffset + 4);

                                if (FromMasterValueToDateTime(thisDateValue) != new DateTime(1, 1, 1))
                                    thisRhsTertiaryOccupancyDataFile.endDateValue = thisDateValue;
                                else
                                    thisRhsTertiaryOccupancyDataFile.endDateValue = masterValueValidationDate;

                                //thisRhsTertiaryOccupancyDataFile.startDateValue = bufferToInt(fullFile, recordOffset + 0);
                                //thisRhsTertiaryOccupancyDataFile.endDateValue = bufferToInt(fullFile, recordOffset + 4);

                                thisRhsTertiaryOccupancyDataFile.room = bufferToShort(fullFile, recordOffset + 13);

                                string type = totalRecord.Substring(recordOffset + 12, 1).ToUpper().Trim();

                                thisRhsTertiaryOccupancyDataFile.bookingChar = type;

                                switch (type)
                                {
                                    case "X":
                                    case "R":
                                    case "r":
                                        thisRhsTertiaryOccupancyDataFile.bookingType = 1;
                                        break;
                                    case "G":
                                        thisRhsTertiaryOccupancyDataFile.bookingType = 4;
                                        break;
                                    case "A":
                                        thisRhsTertiaryOccupancyDataFile.bookingType = 5;
                                        break;
                                    case "L":
                                        thisRhsTertiaryOccupancyDataFile.bookingType = 2;
                                        break;
                                    default:
                                        break;
                                }

                                if (thisRhsTertiaryOccupancyDataFile.room == 0)
                                {
                                    //Type = "R" and Extra = "N" with room = 0 make bookingStatus true
                                    string extra = totalRecord.Substring(recordOffset + 16, 1).ToUpper().Trim();

                                    if ((type == "r" || type == "R") &&
                                        (extra == "N"))
                                    {
                                        thisRhsTertiaryOccupancyDataFile.bookingStatus = rhsBookingType.bookedNotAllocated;
                                        thisRhsTertiaryOccupancyDataFile.roomTypesAffected = new int[38];
                                        for (int counter = 0; counter < 32; counter++)
                                            thisRhsTertiaryOccupancyDataFile.roomTypesAffected[counter] =
                                                bufferToExtraShort(fullFile[recordOffset + 31 + counter]);
                                    }
                                    else
                                    {
                                        thisRhsTertiaryOccupancyDataFile.bookingStatus = rhsBookingType.noBooking;
                                    }
                                }
                                else
                                {
                                    //Type = "X" or "R" or "G" or "L" with room > 0 bookedAndAllocated true
                                    if ((type == "r") || (type == "R") || (type == "X") || (type == "G") || (type == "L"))
                                    {
                                        thisRhsTertiaryOccupancyDataFile.bookingStatus = rhsBookingType.bookedAndAllocated;
                                    }
                                    else
                                    {
                                        thisRhsTertiaryOccupancyDataFile.bookingStatus = rhsBookingType.noBooking;
                                    }
                                }

                                if (thisRhsTertiaryOccupancyDataFile.endDateValue > rhsFiles[fileCounter].maxima)
                                    rhsFiles[fileCounter].maxima = thisRhsTertiaryOccupancyDataFile.endDateValue;

                                if ((thisRhsTertiaryOccupancyDataFile.startDateValue < rhsFiles[fileCounter].minima) ||
                                    (rhsFiles[fileCounter].minima == 0))
                                    rhsFiles[fileCounter].minima = thisRhsTertiaryOccupancyDataFile.startDateValue;

                                tertiaryBookingData.Add(thisRhsTertiaryOccupancyDataFile);
                            }

                            

                            rhsFiles[fileCounter].currentDataRetrieved = true;

                            #endregion

                            break;

                        default:
                            break;
                    }

                    #endregion

                    #region Close the file

                    //dataFileStream.Close();
                    //sr.Close();

                    #endregion
                }
            }
            success = true;
            return success;
        }



        #endregion

        #region updateRoomTypesSettings


        /// <summary>
        /// Get information from both the Web Room Types and the RHS Room Types
        /// Note any changes between this data and the supplied 'existingSettings'
        /// Update this settings costruction to reflect this
        /// </summary>
        /// <returns>Whether there were changes or not</returns>
        public bool updateRoomTypesSettings()
        {
            string currentFunction = "updateRoomTypesSettings";

            bool changes = false;

            #region ensure that variables are initialised

            if (roomTypeData == null)
                roomTypeData = new ArrayList();

            if (theseRhsRoomTypes == null)
                theseRhsRoomTypes = new ArrayList();

            #endregion

            #region Get deletions and editions

            //Look for changes
            //First, check if all old rhs room types are in the new structure

            int thisOldNumRhsRoomTypes = theseRhsRoomTypes.Count;

            #region Rhs Room Types

            #region Delete all the Rhs Room Types that are out of range


            NumRhsRoomTypes = roomTypeData.Count;

            if (thisOldNumRhsRoomTypes != NumRhsRoomTypes)
                changes = true;

            #endregion

            #region Perform additions and editions of Rhs Room Types

            for (int newRhsCounter = 0; newRhsCounter < roomTypeData.Count;
                newRhsCounter++)
            {
                //Note if it is an eddition or an addition
                if (newRhsCounter < thisOldNumRhsRoomTypes)
                {

                    #region Editing
                    //Look for editions:

                    bool changesThisRoomType = false;

                    if (((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).roomTypeName !=
                        ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeName)
                    {
                        //Note the change:
                        LogThis(DateTime.Now, "Change of Rhs Room Type Name",
                            "From: " + ((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).roomTypeName + " To: " + ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeName,
                            currentFunction, "");

                        //Perform the change:
                        ((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).roomTypeName =
                            ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeName;

                        changesThisRoomType = true;
                    }
                    if (((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).roomTypeCode !=
                        ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeCode)
                    {
                        //Note the change:
                        LogThis(DateTime.Now, "Change of Rhs Room Type Code",
                            "From: " + ((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).roomTypeCode + " To: " + ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeCode,
                            currentFunction, "");

                        //Perform the change:
                        ((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).roomTypeCode =
                            ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeCode;

                        changesThisRoomType = true;

                    }

                    if (((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).affectOccupancy !=
                        ((clsRhsRoomType)((clsRhsRoomType)roomTypeData[newRhsCounter])).affectOccupancy)
                    {
                        //Note the change:
                        LogThis(DateTime.Now, "Change of Rhs Room Type Affect Occupancy",
                            "From: " + ((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).affectOccupancy + " To: " + ((clsRhsRoomType)roomTypeData[newRhsCounter]).affectOccupancy,
                            currentFunction, "");

                        //Perform the change:
                        ((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).affectOccupancy =
                            ((clsRhsRoomType)roomTypeData[newRhsCounter]).affectOccupancy;

                        changesThisRoomType = true;
                    }

                    if (((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).numberOfRooms !=
                        ((clsRhsRoomType)roomTypeData[newRhsCounter]).numberOfRooms)
                    {
                        //Note the change:
                        LogThis(DateTime.Now, "Change of Rhs Room Type Number Of Rooms",
                            "From: " + ((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).numberOfRooms + " To: " + ((clsRhsRoomType)roomTypeData[newRhsCounter]).numberOfRooms,
                            currentFunction, "");

                        //Perform the change:
                        ((clsRhsRoomType)theseRhsRoomTypes[newRhsCounter]).numberOfRooms =
                            ((clsRhsRoomType)roomTypeData[newRhsCounter]).numberOfRooms;

                        changesThisRoomType = true;
                    }

                    if (changesThisRoomType)
                    {
                        //Not Required						SetRhsRoomType(newRhsCounter);
                        changes = true;

                    }

                    #endregion

                }
                else
                {
                    #region Adding

                    clsRhsRoomType thisRhsRoomType = new clsRhsRoomType();

                    thisRhsRoomType.roomTypeId = ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeId;
                    thisRhsRoomType.roomTypeCode = ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeCode;
                    thisRhsRoomType.roomTypeName = ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeName;
                    thisRhsRoomType.numberOfRooms = ((clsRhsRoomType)roomTypeData[newRhsCounter]).numberOfRooms;
                    thisRhsRoomType.affectOccupancy = ((clsRhsRoomType)roomTypeData[newRhsCounter]).affectOccupancy;

                    LogThis(DateTime.Now, "Adding New RhsRoomType",
                        "Room Type Id: " + ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeId.ToString() + ","
                        + "Room Type Code: " + ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeCode + ","
                        + "Room Type Name: " + ((clsRhsRoomType)roomTypeData[newRhsCounter]).roomTypeName + ","
                        + "Number of Rooms: " + ((clsRhsRoomType)roomTypeData[newRhsCounter]).numberOfRooms.ToString() + ","
                        + "Affect Occupancy: " + ((clsRhsRoomType)roomTypeData[newRhsCounter]).affectOccupancy.ToString(),
                        currentFunction,
                        "");

                    theseRhsRoomTypes.Add(thisRhsRoomType);
                    //Not Required					SetRhsRoomType();

                    changes = true;

                    #endregion

                }
            }

            #endregion

            #endregion

            #region Web Room Types

            clsRecieveWebRoomsData thisRecieveWebRoomsData = new clsRecieveWebRoomsData();

            if (WebBookingsToAcknowledge == null)
                WebBookingsToAcknowledge = new ArrayList();

            thisRecieveWebRoomsData.WebBookingsToAcknowledge = WebBookingsToAcknowledge;
            thisRecieveWebRoomsData.NumWebRoomTypes = NumWebRoomTypes;

            LogThis(DateTime.Now, "About to Obtain Data from Server",
                "", currentFunction, "");

            thisRecieveWebRoomsData.theseWebRoomTypes = theseWebRoomTypes;

            thisRecieveWebRoomsData.LastWebRoomTypeChange = LastWebRoomTypeChange;
            thisRecieveWebRoomsData.LastRemoteInviteChange = LastRemoteInviteChange;

            thisRecieveWebRoomsData.GetDataFromServer(thisRhsFolder, Curl, Password, AddOnlineBookingsIntoRhs);
            LogThis(DateTime.Now, "Obtained Data from Server",
                "", currentFunction, "");

            if (thisRecieveWebRoomsData.requestSucceeded)
            {

                #region Update the WebBookingsToAcknowledge

                WebBookingsToAcknowledge = thisRecieveWebRoomsData.WebBookingsToAcknowledge;

                #endregion

                #region Check for a change to RemoteInvites

                if (thisRecieveWebRoomsData.RemoteSupportInvites != "")
                {
                    RemoteSupportInvites = thisRecieveWebRoomsData.RemoteSupportInvites;
                    LastRemoteInviteChange = thisRecieveWebRoomsData.LastRemoteInviteChange;
                }

                #endregion

                if (thisRecieveWebRoomsData.LastBookingReceived != new DateTime(1, 1, 1))
                    LastBookingReceived = thisRecieveWebRoomsData.LastBookingReceived;

                if (thisRecieveWebRoomsData.LastSuccessfulCommunication != new DateTime(1, 1, 1))
                    LastSuccessfulCommunication = thisRecieveWebRoomsData.LastSuccessfulCommunication;


                #region If there is a change to RefDate, adjust the availability caches accordingly

                if (thisRecieveWebRoomsData.RefDate != new DateTime(1, 1, 1)
                    && RefDate != thisRecieveWebRoomsData.RefDate
                    )
                {
                    if (RefDate != new DateTime(1, 1, 1))
                    {
                        int numDaysToShift = Convert.ToInt32(thisRecieveWebRoomsData.RefDate.Subtract(RefDate).TotalDays);

                        #region Initialise Logging
                        DateTime thisDate = DateTime.Now;

                        //						string thisLogFileName = GetBaseAppFolder() + logFileName + toWelmanDate(thisDate, true) + "Availability" + rhsWebRoomsExtension;
                        //
                        //						//Add new data to Log (Create file if necessary)
                        //						StreamWriter log;
                        //
                        //						try
                        //						{
                        //							log = new StreamWriter(thisLogFileName, true);
                        //							log.Write("Change To Ref Date Requiring Shifting" + aCrLf);
                        //							log.Write("Now, OldDate, NewDate, numDaysToShift" + aCrLf);
                        //
                        //							log.Write("{0},{1},{2},{3}" + aCrLf, 
                        //								DateTime.Now.ToString(), 
                        //								RefDate.ToString(), 
                        //								thisRecieveWebRoomsData.RefDate.ToString(),
                        //								numDaysToShift.ToString()
                        //								);
                        //							log.Close();
                        //
                        //						}
                        //						catch (System.UnauthorizedAccessException)
                        //						{
                        //						}
                        //						catch (System.Exception)
                        //						{
                        //						}

                        #endregion

                        if (numDaysToShift != 0)
                        {
                            //At least one day further on, move on the caches by this amount

                            #region Update RHS Room Type Availability Caches

                            for (int counter = 0; counter < theseRhsRoomTypes.Count; counter++)
                            {
                                clsRhsRoomType thisRhsRoomType = (clsRhsRoomType)theseRhsRoomTypes[counter];

                                #region Log

                                //								try
                                //								{
                                //									log = new StreamWriter(thisLogFileName, true);
                                //									log.Write("thisRhsRoomType.roomTypeId, old availability, new availability" + aCrLf);
                                //									log.Write(thisRhsRoomType.roomTypeId.ToString() + aCrLf);
                                //									log.Write(thisRhsRoomType.AvailabilityAsDelimitedList + aCrLf);
                                //									log.Close();
                                //								}
                                //								catch (System.UnauthorizedAccessException)
                                //								{
                                //								}
                                //								catch (System.Exception)
                                //								{
                                //								}

                                #endregion

                                if (thisRhsRoomType.AvailabilityAsDelimitedList != "" ||
                                    thisRhsRoomType.AvailabilityAtTimeOfLastUpdateAsDelimitedList != "")
                                {
                                    thisRhsRoomType.ShiftAvailability(numDaysToShift);
                                    theseRhsRoomTypes.RemoveAt(counter);
                                    theseRhsRoomTypes.Insert(counter, thisRhsRoomType);
                                }

                                #region Log
                                //
                                //								try
                                //								{
                                //									log = new StreamWriter(thisLogFileName, true);
                                //									log.Write(thisRhsRoomType.AvailabilityAsDelimitedList + aCrLf);
                                //									log.Close();
                                //								}
                                //								catch (System.UnauthorizedAccessException)
                                //								{
                                //								}
                                //								catch (System.Exception)
                                //								{
                                //								}

                                #endregion


                            }

                            #endregion

                            #region Update Web Room Type Availability Caches

                            for (int counter = 0; counter < theseWebRoomTypes.Count; counter++)
                            {
                                clsWebRoomType thisWebRoomType = (clsWebRoomType)theseWebRoomTypes[counter];

                                #region Log
                                //
                                //								try
                                //								{
                                //									log = new StreamWriter(thisLogFileName, true);
                                //									log.Write("thisWebRoomType.roomTypeId, old availability, new availability" + aCrLf);
                                //									log.Write(thisWebRoomType.webRoomTypeId.ToString() + aCrLf);
                                //									log.Write(thisWebRoomType.AvailabilityAsDelimitedList + aCrLf);
                                //									log.Close();
                                //
                                //								}
                                //								catch (System.UnauthorizedAccessException)
                                //								{
                                //								}
                                //								catch (System.Exception)
                                //								{
                                //								}

                                #endregion

                                if (thisWebRoomType.AvailabilityAsDelimitedList != "" ||
                                    thisWebRoomType.AvailabilityAtTimeOfLastUpdateAsDelimitedList != "")
                                {
                                    thisWebRoomType.ShiftAvailability(numDaysToShift);
                                    theseWebRoomTypes.RemoveAt(counter);
                                    theseWebRoomTypes.Insert(counter, thisWebRoomType);
                                }

                                #region Log

                                //								try
                                //								{
                                //									log = new StreamWriter(thisLogFileName, true);
                                //									log.Write(thisWebRoomType.AvailabilityAsDelimitedList + aCrLf);
                                //									log.Close();
                                //								}
                                //								catch (System.UnauthorizedAccessException)
                                //								{
                                //								}
                                //								catch (System.Exception)
                                //								{
                                //								}

                                #endregion

                            }

                            #endregion

                        }

                    }
                    RefDate = thisRecieveWebRoomsData.RefDate;

                }

                #endregion

                CompleteAvailabilityRequired = thisRecieveWebRoomsData.CompleteAvailabilityRequired;


                if (thisRecieveWebRoomsData.Lookahead != 0)
                    Lookahead = thisRecieveWebRoomsData.Lookahead;



                if (thisRecieveWebRoomsData.LastWebRoomTypeChange != LastWebRoomTypeChange) // Only do things if server communications were Successful
                {

                    #region Set the Web Room Types (Clean up of these is performed in clsRecieveWebRoomsData) (Removed as Registry centric)

                    //for (int counter = 0; 
                    //    counter < theseWebRoomTypes.Count;
                    //    counter++)
                    //{
                    //    SetWebRoomType(counter);
                    //} 

                    #endregion

                    LastWebRoomTypeChange = thisRecieveWebRoomsData.LastWebRoomTypeChange;
                }
            }
            #endregion

            #endregion

            Save();

            return changes;
        }

        #endregion

        #region getAvailabilityFromRaw

        /// <summary>
        /// This is public so we can show availability in the UI
        /// </summary>
        public int[,] bookingsByRoomIndex = new int[1, 1];

        /// <summary>
        /// Analyses the contents of raw RHS files and outputs an availability matrix
        /// </summary>
        /// <returns>Processed RHS Data</returns>
        public void getAvailabilityFromRaw()
        {
            #region Initialise

            string currentFunction = "getAvailabilityFromRaw";

            //initialise variables
            int numberOfOccupancies = 0;
            numRoomTypes = rhsFiles[rhsRawFileType_rmtype1Dat()].numRecords;

            numRooms = rhsFiles[rhsRawFileType_rooms1Dat()].numRecords;
            numTertiaryRecords = rhsFiles[rhsRawFileType_statafftDat()].numRecords;
            highestRoomNumber = rhsFiles[rhsRawFileType_rooms1Dat()].maxima;
            firstDayValue = rhsFiles[rhsRawFileType_statafftDat()].minima;

            clsDaysBetween tempDaysBetween = daysBetween(firstDayValue, rhsFiles[rhsRawFileType_statafftDat()].maxima);
            if (tempDaysBetween.errDescription.success)
                numDays = tempDaysBetween.daysBetween;
            else
            {
                LogThis(DateTime.Now, "numDays out of Range",
                    tempDaysBetween.errDescription.errorForUser,
                    currentFunction,
                    "firstDayValue = " + firstDayValue.ToString()
                    + " rhsFiles[rhsRawFileType_statafftDat()].maxima = " + rhsFiles[rhsRawFileType_statafftDat()].maxima.ToString());

                numDays = 0;
            }

            //			numDays = daysBetween(firstDayValue, rhsFiles[rhsRawFileType_statafftDat()].maxima);

            //Need to get bookings by Room Index before adding these to the
            //Bookings by Room Type system
            bookingsByRoomIndex = new int[numRooms, numDays];

            //Update visual progress
            int lastProgressUpdate = 50;

            availabilityAndOccupancy = new roomTypeStatusStruct[numRoomTypes + 1, numDays];

            //Used to reference a room from its number to its index
            int[] roomIndexFromNumber = new int[highestRoomNumber + 1];

            //Initialise the roomIndexFromNumber Array. 
            for (int counter = 0; counter < highestRoomNumber; counter++)
                roomIndexFromNumber[counter] = -1;

            for (int counter = 0; counter < numRooms; counter++)
            {
                //Ensure we have indexes for every room number in the roomIndexFromNumber array
                clsRhsRoomDataFile thisRhsRoomDataFile = (clsRhsRoomDataFile)roomData[counter];
                int thisRhsRoomDataFileNumber = thisRhsRoomDataFile.number;
                bool thisRhsRoomDataFileAffectOccupancy = thisRhsRoomDataFile.affectOccupancy;

                //                if (((clsRhsRoomDataFile)roomData[counter]).number > -1 && ((clsRhsRoomDataFile)roomData[counter]).affectOccupancy)
                //                    roomIndexFromNumber[((clsRhsRoomDataFile)roomData[counter]).number] = counter;
                if (thisRhsRoomDataFileNumber > -1 && thisRhsRoomDataFileAffectOccupancy)
                {
                    if (thisRhsRoomDataFileNumber < roomIndexFromNumber.GetUpperBound(0) + 1)
                        roomIndexFromNumber[thisRhsRoomDataFileNumber] = counter;
                    else
                        roomIndexFromNumber[thisRhsRoomDataFileNumber] = counter;

                }

                //if (((clsRhsRoomDataFile)roomData[counter]).alias > -1 && ((clsRhsRoomDataFile)roomData[counter]).affectOccupancy)
                //    roomIndexFromNumber[((clsRhsRoomDataFile)roomData[counter]).alias] = counter;
            }

            //Set the number of Rooms of each type as the 'base availability' i.e #of rooms
            for (int roomTypeCounter = 0; roomTypeCounter < numRoomTypes; roomTypeCounter++)
                for (int dayCounter = 0; dayCounter < numDays; dayCounter++)
                {
                    availabilityAndOccupancy[roomTypeCounter + 1, dayCounter].available = Convert.ToInt32(((clsRhsRoomType)roomTypeData[roomTypeCounter]).numberOfRooms);
                    //currentRhsAvailability[roomTypeCounter, dayCounter].occupied = 0;
                }

            bool[] tetiaryRecordDone = new bool[tertiaryBookingData.Count + 1];

            for (int counter = 0; counter < tertiaryBookingData.Count + 1; counter++)
                tetiaryRecordDone[counter] = false;

            //Get booked and allocated data
            //This data is viewable in the worksheet under RHS


            //StreamWriter debug = new StreamWriter("debugNew.txt", false);
            //int curSec = 0;
            //int curTer = 0;


            #endregion

            #region Iterate through the primary, secondary and tertiary records

            for (int priRecordCounter = 0; priRecordCounter < primaryBookingData.Count; priRecordCounter++)
            {

                clsRhsPrimaryOccupancyDataFile currentPrimary = new clsRhsPrimaryOccupancyDataFile();
                clsRhsSecondaryOccupancyDataFile currentSecondary = new clsRhsSecondaryOccupancyDataFile();
                clsRhsTertiaryOccupancyDataFile currentTertiary = new clsRhsTertiaryOccupancyDataFile();

                currentPrimary = (clsRhsPrimaryOccupancyDataFile)primaryBookingData[priRecordCounter];
                if (priRecordCounter == 0 || (daysBetween(DateTime.Now, currentPrimary.dateValue).daysBetween > -1))
                {

                    //				nextSec = primaryBookingData[priRecordCounter].firstSecondaryRef;
                    currentSecondary.nextSecondaryRef = currentPrimary.firstSecondaryRef;
                    //+1 added because of the index offset.
                    if (currentSecondary.nextSecondaryRef < secondaryBookingData.Count + 1)
                    {
                        while (currentSecondary.nextSecondaryRef > 0)
                        {
                            //curSec = currentSecondary.nextSecondaryRef - 1;
                            currentSecondary = (clsRhsSecondaryOccupancyDataFile)secondaryBookingData[currentSecondary.nextSecondaryRef - 1];


                            if (!tetiaryRecordDone[currentSecondary.tertiaryRef])
                            {
                                //							tetiaryRecordDone[currentSecondary.tertiaryRef] = true;
                                //curTer = currentSecondary.tertiaryRef - 1;
                                currentTertiary = (clsRhsTertiaryOccupancyDataFile)tertiaryBookingData[currentSecondary.tertiaryRef - 1];

                                //debug.Write("{0},{1},{2} Try\r\n", priRecordCounter, curSec, curTer);	
                                switch (currentTertiary.bookingStatus)
                                {
                                    case rhsBookingType.bookedAndAllocated:

                                        #region bookedAndAllocated

                                        #region Sort Start Day

                                        long startDay;
                                        tempDaysBetween = daysBetween(firstDayValue, currentTertiary.startDateValue);
                                        if (tempDaysBetween.errDescription.success)
                                            startDay = tempDaysBetween.daysBetween - 1;
                                        else
                                        {
                                            LogThis(DateTime.Now, "startDay out of Range",
                                                tempDaysBetween.errDescription.errorForUser,
                                                currentFunction,
                                                "firstDayValue = " + firstDayValue.ToString()
                                                + " currentTertiary.startDateValue = " + currentTertiary.startDateValue.ToString());

                                            startDay = 0;
                                        }

                                        #endregion

                                        #region Sort End Day

                                        long endDay;

                                        if (currentTertiary.endDateValue == 0)
                                        {
                                            //An 'indefinte stay'; go to the edge of the array
                                            endDay = bookingsByRoomIndex.GetUpperBound(1);
                                        }
                                        else
                                        {
                                            tempDaysBetween = daysBetween(firstDayValue,
                                                currentTertiary.endDateValue);
                                            if (tempDaysBetween.errDescription.success)
                                                endDay = tempDaysBetween.daysBetween;
                                            else
                                            {
                                                LogThis(DateTime.Now, "endDay out of Range",
                                                    tempDaysBetween.errDescription.errorForUser,
                                                    currentFunction,
                                                    "firstDayValue = "
                                                    + firstDayValue.ToString()
                                                    + " currentTertiary.endDateValue = "
                                                    + currentTertiary.endDateValue.ToString());
                                                endDay = startDay;
                                            }
                                        }

                                        #endregion

                                        #region Sort roomNum

                                        int roomNum = -1;

                                        if (currentTertiary.room < roomIndexFromNumber.GetUpperBound(0) + 1)
                                            roomNum = roomIndexFromNumber[currentTertiary.room];
                                        else
                                        {
                                            LogThis(DateTime.Now, "currentTertiary.room out of Range",
                                                "Ignoring this room",
                                                currentFunction,
                                                "currentTertiary.room = "
                                                + currentTertiary.room.ToString()
                                                + " roomIndexFromNumber.GetUpperBound(0) = "
                                                + roomIndexFromNumber.GetUpperBound(0).ToString());
                                        }

                                        //										if (roomNum > -1 && roomNum < bookingsByRoomIndex.GetUpperBound(0) +1)
                                        if (roomNum > -1
                                            && roomNum < bookingsByRoomIndex.GetUpperBound(0) + 1
                                            && ((clsRhsRoomDataFile)roomData[roomNum]).affectOccupancy
                                            )
                                            for (long dayCounter = startDay; dayCounter < endDay; dayCounter++)
                                            {
                                                bookingsByRoomIndex[roomNum, dayCounter] = currentTertiary.bookingType;

                                            } // for dayCounter...

                                        #endregion

                                        #endregion

                                        break;
                                    case rhsBookingType.bookedNotAllocated:

                                        #region bookedNotAllocated

                                        for (int roomTypeCounter = 0; roomTypeCounter < numRoomTypes;
                                            roomTypeCounter++)
                                        {
                                            numberOfOccupancies =
                                                currentTertiary.roomTypesAffected[roomTypeCounter];

                                            // Note this start day is NOT the same as the day given by
                                            // daysBetween(firstDayValue, currentTertiary.startDateValue);

                                            //long occupancyDay = priRecordCounter;
                                            long occupancyDay;

                                            #region Sort Start Day (note this is different to StartDay for bookedAndAllocated)

                                            tempDaysBetween = daysBetween(firstDayValue,
                                                currentPrimary.dateValue);
                                            if (tempDaysBetween.errDescription.success)
                                                occupancyDay = tempDaysBetween.daysBetween - 1;
                                            else
                                            {
                                                LogThis(DateTime.Now, "firstNightIndex out of Range",
                                                    tempDaysBetween.errDescription.errorForUser,
                                                    currentFunction,
                                                    "firstDayValue = " + firstDayValue.ToString()
                                                    + " currentPrimary.dateValue = "
                                                    + currentPrimary.dateValue.ToString());

                                                occupancyDay = 0;
                                            }

                                            #endregion

                                            //										long occupancyDay = daysBetween(firstDayValue, currentPrimary.dateValue) - 1;

                                            availabilityAndOccupancy[roomTypeCounter + 1, occupancyDay].available -=
                                                numberOfOccupancies;
                                            availabilityAndOccupancy[roomTypeCounter + 1, occupancyDay].occupied +=
                                                numberOfOccupancies;
                                            //if (numberOfOccupancies > 0)
                                            //debug.Write("{0},{1},{2}. ", occupancyDay, roomTypeCounter, numberOfOccupancies);


                                        } // end of for
                                        //debug.Write(aCrLf);

                                        #endregion

                                        break;
                                    default:
                                        break;
                                } // end of switch
                            }
                        }
                    }
                }
            }

            #endregion

            #region Go through the bookingsByRoomIndex array and get room type availability from that


            #region Now go through the bookingsByRoomIndex array and get room type availability from that

            for (long dayCounter = 0; dayCounter < numDays; dayCounter++)
            {
                for (long roomIndexCounter = 0; roomIndexCounter < numRooms;
                    roomIndexCounter++)
                {
                    int roomType = ((clsRhsRoomDataFile)roomData[(int)roomIndexCounter]).roomType - 1;
                    switch (bookingsByRoomIndex[roomIndexCounter, dayCounter])
                    {
                        case 1:
                        case 4:
                            //For cases 1 and 4 increment occupancy and decrement availability
                            availabilityAndOccupancy[roomType + 1, dayCounter].available--;
                            availabilityAndOccupancy[roomType + 1, dayCounter].occupied++;
                            break;
                        case 2:
                        case 3:
                            //For cases 2 and 3 just decrement availability
                            availabilityAndOccupancy[roomType + 1, dayCounter].available--;
                            break;
                        case 5:
                            //For case 5, just decrement occupancy
                            availabilityAndOccupancy[roomType + 1, dayCounter].occupied--;
                            break;
                        default:
                            break;
                    }  // end of tertiaryBookingData[terRecordCounter].bookingStatus switch
                }
            }
            #endregion

            #endregion

            #region Take away any outstanding Webrooms reservations

            //Now need to take away any outstanding reservations 
            if (AddOnlineBookingsIntoRhs)
            {

                //Create the folder if it does not exist
                DirectoryInfo di = new DirectoryInfo(OnlineBookingsFolder);
                if (di.Exists)
                {
                    //Find any files with the .RES extension in it
                    FileInfo[] fi = di.GetFiles("*" + rhsReservationFileExtension);
                    foreach (FileInfo fiTemp in fi)
                    {
                        clsRhsReservation thisRhsReservation = new clsRhsReservation();

                        thisRhsReservation.readReservationFile(OnlineBookingsFolder + @"\" + fiTemp.Name);
                        //Iterate through the reservation

                        if (thisRhsReservation.ResDate.year > DateTime.Now.Year - 2)
                        {

                            long firstNightIndex;

                            tempDaysBetween = daysBetween(firstDayValue,
                                new DateTime(thisRhsReservation.ResDate.year, thisRhsReservation.ResDate.month, thisRhsReservation.ResDate.day));
                            if (tempDaysBetween.errDescription.success)
                                firstNightIndex = tempDaysBetween.daysBetween - 1;
                            else
                            {
                                LogThis(DateTime.Now, "firstNightIndex out of Range",
                                    tempDaysBetween.errDescription.errorForUser,
                                    currentFunction,
                                    "firstDayValue = " + firstDayValue.ToString()
                                    + " new DateTime(thisRhsReservation.ResDate.year, thisRhsReservation.ResDate.month, thisRhsReservation.ResDate.day) = "
                                    + new DateTime(thisRhsReservation.ResDate.year, thisRhsReservation.ResDate.month, thisRhsReservation.ResDate.day).ToString());

                                firstNightIndex = 0;
                            }

                            //						long firstNightIndex = daysBetween(firstDayValue, new DateTime(thisRhsReservation.ResDate.year, thisRhsReservation.ResDate.month, thisRhsReservation.ResDate.day)) - 1;
                            long lastNightIndex = firstNightIndex + thisRhsReservation.Nights;

                            //Check for out of bounds conditions
                            if (firstNightIndex < 0)
                                firstNightIndex = 0;

                            if (firstNightIndex > availabilityAndOccupancy.GetUpperBound(1))
                                firstNightIndex = availabilityAndOccupancy.GetUpperBound(1);

                            if (lastNightIndex < 0)
                                lastNightIndex = 0;

                            if (lastNightIndex > availabilityAndOccupancy.GetUpperBound(1))
                                lastNightIndex = availabilityAndOccupancy.GetUpperBound(1);

                            for (long nightCounter = firstNightIndex; nightCounter < lastNightIndex; nightCounter++)
                            {
                                for (int roomTypeCounter = 1; roomTypeCounter < numRoomTypes + 1; roomTypeCounter++)
                                {
                                    availabilityAndOccupancy[roomTypeCounter, nightCounter].available
                                        -= thisRhsReservation.Rooms[roomTypeCounter];
                                    availabilityAndOccupancy[roomTypeCounter, nightCounter].occupied
                                        += thisRhsReservation.Rooms[roomTypeCounter];
                                }
                            }
                        }

                    }
                }

            }

            #endregion

            #region Logging

            DateTime thisDate = DateTime.Now;

            //			string thisLogFileName = GetBaseAppFolder() + logFileName + toWelmanDate(thisDate, true) + "Availability" + rhsWebRoomsExtension;
            //
            //			//Add new data to Log (Create file if necessary)
            //			StreamWriter log;
            //
            //			DateTime thisNewDateTime = new DateTime((int) (firstDayValue / 1300),
            //				(int) (firstDayValue / 100) % 13,
            //				(int) (firstDayValue % 100));
            //
            //			try
            //			{
            //				log = new StreamWriter(thisLogFileName, true);
            //				log.Write("getAvailabilityFromRaw" + aCrLf);
            //				log.Write("Now, firstDayValue,firstDayAsDate,numDays,numRoomTypes" + aCrLf);
            //
            //				log.Write("{0},{1},{2},{3},{4}" + aCrLf, 
            //					DateTime.Now.ToString(), 
            //					firstDayValue, 
            //					thisNewDateTime.ToString(),
            //					numDays,
            //					numRoomTypes);
            //
            //				for(int rtCounter = 0; rtCounter < numRoomTypes; rtCounter++)
            //				{
            //					log.Write("RoomTypeid: " + (rtCounter - 1).ToString() + aCrLf);
            //
            //					string availability = "";
            //					for(int avCounter = 0; avCounter < numDays; avCounter++)
            //					{
            //						availability += availabilityAndOccupancy[rtCounter,avCounter].available.ToString() + ",";
            //					}
            //
            //					log.Write(availability + aCrLf);
            //
            //				}
            //				log.Close();
            //			}
            //			catch (System.UnauthorizedAccessException)
            //			{
            //				return;
            //			}
            //			catch (System.Exception)
            //			{
            //				return;
            //			}

            #endregion

            //			lastProgressUpdate = updateProgress(100,
            //				lastProgressUpdate);
            //			return currentRhsAvailability;
        }

        #endregion

        #region getRhsAvailabilityData

        /// <summary>
        /// Gets Availability Data from RHS Database Files
        /// </summary>
        /// <returns>RHS Availability</returns>
        public bool getRhsAvailabilityData()
        {
            requestSucceeded = false;
            errDescription = new clsWelmanError();

            string currentFunction = "getRhsAvailabilityData";

            PopulateRhsFiles();

            #region Check the dates of the RHS files; do we need to update?

            bool updateFromRawRequired = false;

            DateTime previousLastModifiedDate = LastRhsAvailabilityFileModify;

            DateTime tempLastRhsAvailabilityFileModify = LastRhsAvailabilityFileModify;

            for (int counter = 0; counter < numRhsFiles; counter++)
            {
                string thisfile = RhsDataFolder + (rhsFiles[counter].fileName).ToString();
                if (File.Exists(thisfile))
                {
                    DateTime thisDateModified = File.GetLastWriteTime(thisfile);
                    if (thisDateModified > tempLastRhsAvailabilityFileModify)
                        tempLastRhsAvailabilityFileModify = thisDateModified;
                }
            }

            DateTime defaultTime = new DateTime(1, 1, 1);

            if (thisLastRhsAvailabilityFileModify == defaultTime
                || previousLastModifiedDate == defaultTime
                || RefDate == defaultTime
                || RefDateLastUpdate == defaultTime
                || thisLastRhsAvailabilityFileModify > previousLastModifiedDate
                || RefDate != RefDateLastUpdate)
            {
                //Update required
                updateFromRawRequired = true;
            }

            #endregion

            if (updateFromRawRequired)
            {

                #region Read RHS files: Firstly see if we can get data

                for (int counter = 0; counter < numRhsFiles; counter++)
                {
                    rhsFiles[counter].currentDataRetrieved = false;
                }

                //Read RHS files
                getRawRHSData(rhsFiles);

                #endregion

                #region Confirm we actually read them

                bool bAnRhsFileNotFound = false;
                for (int counter = 0; counter < numRhsFiles; counter++)
                {
                    if (!rhsFiles[counter].currentDataRetrieved)
                        bAnRhsFileNotFound = true;
                }

                if (bAnRhsFileNotFound)
                {
                    string messageToSend = "";

                    for (int counter = 0; counter < numRhsFiles; counter++)
                    {
                        if (!rhsFiles[counter].currentDataRetrieved)
                            messageToSend += rhsFiles[counter].fileName + ", ";
                    }

                    errDescription.success = false;
                    errDescription.errNum = 97;
                    errDescription.errorForUser = "Update Failed: Unable to find RhsFiles";
                    errDescription.logFileDescription = messageToSend;

                    LogThis(DateTime.Now, errDescription.errorForUser,
                        "", currentFunction, errDescription.logFileDescription);

                    return requestSucceeded;
                }
                #endregion

                #region Get Availability from them

                getAvailabilityFromRaw();

                #endregion

                #region Mark the LastRhsAvailabilityFileModify

                LastRhsAvailabilityFileModify = tempLastRhsAvailabilityFileModify;

                #endregion

            }

            requestSucceeded = true;
            return requestSucceeded;
        }

        #endregion


        #endregion

        #region Send Data to Web Server


        /// <summary>
        /// SendDataToServer
        /// </summary>
        public bool SendDataToServer(bool forceUpdate)
        {
            string currentFunction = "SendDataToServer";

            requestSucceeded = false;
            errDescription.errNum = 0;
            errDescription.errorForUser = "";
            availabilitiesUpdated = 0;

            #region Null Credentials / First Day check

            if ((Curl == null) ||
                (Password == null) ||
                (Curl == "") ||
                (Password == ""))
            {
                //Failure due to null or empty credentials
                errDescription.errNum = 1101;
                errDescription.errorForUser = "Credentials null or Empty";
                errDescription.logFileDescription = "";
                errDescription.errorForUser = "Supplied Credentials were null or empty";

                LogThis(DateTime.Now, "Failure attempting to upload availability: ",
                    errDescription.errorForUser, currentFunction,
                    errDescription.logFileDescription);

                requestSucceeded = false;
                return requestSucceeded;
            }

            if (RefDate == new DateTime(1, 1, 1))
            {
                //Failure due to null data
                errDescription.errNum = 1201;
                errDescription.errorForUser = "No Data to send; RefDate = " + RefDate.ToString();
                errDescription.logFileDescription = "";
                errDescription.errorForUser = "No Data to send";

                LogThis(DateTime.Now, "Failure attempting to upload availability: ",
                    errDescription.errorForUser, currentFunction,
                    errDescription.logFileDescription);

                requestSucceeded = false;
                return requestSucceeded;
            }
            #endregion

            #region Set Availability

            System.ServiceModel.EndpointAddress remoteAddress = new System.ServiceModel.EndpointAddress(ThirdPartyBaseUrl);
            Comms.WebRoomsServiceSoapClient thisService = new Comms.WebRoomsServiceSoapClient(
                new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.Transport), remoteAddress);


            //Comms.WebRoomsService thisService = new Comms.WebRoomsService();
            Comms.SetAvailabilityRequest thisRequest = new Comms.SetAvailabilityRequest();
            Comms.ResponseType thisResponse;


            thisRequest.PropertyAuthentication = new Comms.SetAvailabilityRequestPropertyAuthentication();
            thisRequest.PropertyAuthentication.PropertyId = Curl;
            thisRequest.PropertyAuthentication.PropertyPassword = Password;

            thisRequest.ThirdPartyAuthentication = new Comms.SetAvailabilityRequestThirdPartyAuthentication();

            thisRequest.ThirdPartyAuthentication.EnqUser = thisEnqUser;
            thisRequest.ThirdPartyAuthentication.EnqPass = thisEnqPass;

            DateTime thisDate = RefDate;


            thisRequest.RefDayYear = thisDate.Year;
            thisRequest.RefDayMonth = thisDate.Month;
            thisRequest.RefDayDay = thisDate.Day;

            ArrayList theseRoomTypesWithAvailabilities = new ArrayList();

            #region Add availabilities

            for (int rtCounter = 0; rtCounter < theseWebRoomTypes.Count; rtCounter++)
            {
                clsWebRoomType thisWebRoomType = (clsWebRoomType)theseWebRoomTypes[rtCounter];

                if (thisWebRoomType.maxNumberOfRooms > 0 || UpdateUnmapped)
                {

                    Comms.SetAvailabilityRequestRoomType thisRoomType = new Comms.SetAvailabilityRequestRoomType();
                    thisRoomType.RoomTypeId = thisWebRoomType.webRoomTypeId;
                    thisRoomType.NumberOfRooms = thisWebRoomType.maxNumberOfRooms;
                    thisRoomType.NumberOfRoomsSpecified = true;

                    int numChanges = 0;
                    thisRequest.CompleteAvailabilitySentSpecified = true;

                    if (CompleteAvailabilityRequired || forceUpdate)
                    {
                        thisRequest.CompleteAvailabilitySent = true;

                        numChanges = thisWebRoomType.Availability.Count;
                        thisRoomType.Availabilities = new Comms.SetAvailabilityRequestRoomTypeAvailabilities[numChanges];

                        for (int avCounter = 0; avCounter < numChanges; avCounter++)
                        {
                            thisRoomType.Availabilities[avCounter] = new Comms.SetAvailabilityRequestRoomTypeAvailabilities();
                            thisRoomType.Availabilities[avCounter].StartDayOffset = avCounter;
                            thisRoomType.Availabilities[avCounter].RoomsAvailable = Convert.ToInt32(thisWebRoomType.Availability[avCounter]);
                            thisRoomType.Availabilities[avCounter].RoomsAvailableSpecified = true;
                        }

                        theseRoomTypesWithAvailabilities.Add(thisRoomType);

                    }
                    else
                    {
                        thisRequest.CompleteAvailabilitySent = false;
                        numChanges = thisWebRoomType.GetDifferences();

                        if (numChanges > 0)
                        {
                            thisRoomType.Availabilities = new Comms.SetAvailabilityRequestRoomTypeAvailabilities[numChanges];

                            for (int avCounter = 0; avCounter < numChanges; avCounter++)
                            {
                                thisRoomType.Availabilities[avCounter] = new Comms.SetAvailabilityRequestRoomTypeAvailabilities();
                                int thisIndex = Convert.ToInt32(thisWebRoomType.ChangedAvailabilityIndex[avCounter]);
                                thisRoomType.Availabilities[avCounter].StartDayOffset = thisIndex;
                                thisRoomType.Availabilities[avCounter].EndDayOffset = thisIndex;
                                thisRoomType.Availabilities[avCounter].EndDayOffsetSpecified = true;
                                thisRoomType.Availabilities[avCounter].RoomsAvailable = Convert.ToInt32(thisWebRoomType.Availability[thisIndex]);
                                thisRoomType.Availabilities[avCounter].RoomsAvailableSpecified = true;
                            }

                            theseRoomTypesWithAvailabilities.Add(thisRoomType);

                        }
                    }
                }
            }

            #endregion

            int numRoomTypes = theseRoomTypesWithAvailabilities.Count;
            thisRequest.RoomType = new Comms.SetAvailabilityRequestRoomType[numRoomTypes];

            for (int counter = 0; counter < numRoomTypes; counter++)
            {
                thisRequest.RoomType[counter] = (Comms.SetAvailabilityRequestRoomType)theseRoomTypesWithAvailabilities[counter];
            }


            DateTime finished = new DateTime(1, 1, 1);

            if (numRoomTypes > 0)
            {

                LogThis(DateTime.Now, "Sending Data to Server", "", currentFunction, SetAvailabilityRequestXml(thisRequest));

                try
                {
                    //					thisService.Timeout = webTimeOut;

                    thisResponse = thisService.SetAvailabilities(thisRequest);

                    if (!thisResponse.Success)
                    {
                        requestSucceeded = false;
                        LogThis(DateTime.Now, "Failure attempting to Set Availability", thisResponse.Message,
                            currentFunction, thisResponse.Message);
                        return requestSucceeded;

                    }

                }
                catch (Exception e)
                {
                    LogThis(DateTime.Now, "Failure connecting to Webrooms Server when Setting Availability.",
                        e.ToString() + " , " + e.Message + " , " + e.StackTrace + " , " + e.Source,
                        currentFunction, e.StackTrace.ToString());

                    return false;
                }

                finished = DateTime.Now;
                LastSuccessfulCommunication = finished;
                LastAvailabilityUpdate = finished;

                LogThis(DateTime.Now, thisResponse.Message,
                    "", currentFunction, "");

            }
            #endregion

            #region Acknowledge Bookings

            int numWebBookingsToAcknowledge = 0;
            if (WebBookingsToAcknowledge != null)
                numWebBookingsToAcknowledge = WebBookingsToAcknowledge.Count;

            if (numWebBookingsToAcknowledge > 0)
            {
                Comms.AcknowledgeBookings thisAckRequest = new Comms.AcknowledgeBookings();

                thisAckRequest.PropertyAuthentication = new Comms.AcknowledgeBookingsPropertyAuthentication();
                thisAckRequest.PropertyAuthentication.PropertyId = Curl;
                thisAckRequest.PropertyAuthentication.PropertyPassword = Password;

                thisAckRequest.ThirdPartyAuthentication = new Comms.AcknowledgeBookingsThirdPartyAuthentication();

                thisAckRequest.ThirdPartyAuthentication.EnqUser = thisEnqUser;
                thisAckRequest.ThirdPartyAuthentication.EnqPass = thisEnqPass;

                thisAckRequest.Booking = new Comms.AcknowledgeBookingsBooking[numWebBookingsToAcknowledge];

                for (int counter = 0; counter < numWebBookingsToAcknowledge; counter++)
                {
                    int thisBookingId = Convert.ToInt32(WebBookingsToAcknowledge[counter]);

                    thisAckRequest.Booking[counter] = new Comms.AcknowledgeBookingsBooking();
                    thisAckRequest.Booking[counter].BookingId = thisBookingId;
                }

                try
                {

                    //					thisService.Timeout = webTimeOut;
                    thisResponse = thisService.AcknowledgeBookings(thisAckRequest);

                    if (!thisResponse.Success)
                    {
                        requestSucceeded = false;
                        LogThis(DateTime.Now, "Failure attempting to Acknowledge Bookings", thisResponse.Message,
                            currentFunction, thisResponse.Message);
                        return requestSucceeded;

                    }

                }
                catch (Exception e)
                {
                    LogThis(DateTime.Now, "Failure connecting to Webrooms Server when Acknowledging Bookings.",
                        e.ToString() + " , " + e.Message + " , " + e.StackTrace + " , " + e.Source,
                        currentFunction, e.StackTrace.ToString());

                    return false;
                }

                finished = DateTime.Now;
                LastSuccessfulCommunication = finished;

                LogThis(DateTime.Now, thisResponse.Message,
                    "", currentFunction, "");

            }


            #endregion

            #region Old School

            #region Add Room numbers for each roomType
            //			string roomTypes = "";
            //			for (int webRoomCounter = 0; webRoomCounter < theseWebRoomTypes.Count;
            //				webRoomCounter++)
            //			{
            //				roomTypes += "&rtuid=" + ((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).webRoomTypeId.ToString().Trim();
            //				roomTypes += "&tr=" +((clsWebRoomType) theseWebRoomTypes[webRoomCounter]).maxNumberOfRooms.ToString().Trim();
            //			}
            #endregion

            #region Add Web Booking Acknowledgements
            //			int numWebBookingsToAcknowledge = 0;
            //			if (WebBookingsToAcknowledge != null)
            //				numWebBookingsToAcknowledge = WebBookingsToAcknowledge.Count;
            //
            //			string webBookings = "";
            //			for (int webBookingCounter = 0; webBookingCounter < numWebBookingsToAcknowledge; webBookingCounter++)
            //				webBookings += "&bookingUID=" + ((string) WebBookingsToAcknowledge[webBookingCounter]).Trim();

            #endregion

            #region Sort Out Start and End Dates

            //			long startVal;
            //			
            //			clsDaysBetween tempDaysBetween = daysBetween(RefDate, 
            //				firstDayValue);
            //			if (tempDaysBetween.errDescription.success)
            //				startVal = tempDaysBetween.daysBetween - 1;
            //			else
            //			{
            //				LogThis(DateTime.Now, "startVal out of Range",
            //					tempDaysBetween.errDescription.errorForUser, 
            //					currentFunction,
            //					"RefDate = " + RefDate.ToString()
            //					+ " firstDayValue= " + firstDayValue.ToString());
            //				
            //				startVal = 0;
            //			}			

            //endVal is the number of days worth of data to send. This is either
            //the lookahead value specified or
            //the number of days worth of data left in the database subsequent to the reference date
            //whichever is the smallest.
            //			int endVal = Lookahead + 1;
            //			if ((Lookahead + 1) > (numDays + startVal))
            //				endVal = numDays + startVal;

            #endregion

            #region Get Availability (via occupancy)
            //Loop through data adding di, rt and oc for each date
            //			string availabilities = "";
            //
            //			for (int dateIndex = 0; dateIndex < endVal; dateIndex++) 
            //			{
            //				for (int webRoomCounter = 0; webRoomCounter < theseWebRoomTypes.Count; 
            //					webRoomCounter++)
            //				{
            //					clsWebRoomType thisWebRoomType = (clsWebRoomType) theseWebRoomTypes[webRoomCounter];
            //
            //					if (thisWebRoomType.Availability.Count < dateIndex + 1 ||
            //						(int) thisWebRoomType.Availability[dateIndex] == (int) thisWebRoomType.maxNumberOfRooms)
            //						//						availabilities += "&oc=" + (thisWebRoomType.maxNumberOfRooms).ToString().Trim();
            //						//Do Nothing here now
            //						Application.DoEvents();
            //					else
            //					{
            //						availabilities += "&di=" + dateIndex.ToString().Trim();
            //						availabilities += "&rt=" + thisWebRoomType.webRoomTypeId.ToString().Trim();
            //						availabilities += "&oc=" + (thisWebRoomType.maxNumberOfRooms 
            //							- (int) thisWebRoomType.Availability[dateIndex]).ToString().Trim();
            //
            //					}
            //					Application.DoEvents();
            //				}
            //			}

            #endregion


            //			string postParams = "referenceDate=" + formatDateForWeb(RefDate) 
            //				+ "&Curl=" + Curl 
            //				+ "&pass=" + Password 
            //				+ "&updateChangedOnly=False"
            //				+ roomTypes 
            //				+ webBookings 
            //				+ availabilities;
            //		
            //
            //			UpdatingWebsite("", 0, "");
            //
            //			string response = httpPost(WebsiteSendUrl, postParams);
            //
            //			UpdatingWebsite("", 100, "");
            //			
            //			errDescription = parseServerResponseForGeneralError(response);
            //
            //			if (!errDescription.success)
            //			{
            //				requestSucceeded = false;
            //				LogThis(DateTime.Now, "Failure attempting to upload availability", errDescription.errorForUser, 
            //					currentFunction, 
            //					errDescription.logFileDescription += aCrLf + 
            //					formatDateForWeb(RefDate) + aCrLf +
            //					roomTypes + aCrLf + webBookings + aCrLf  + availabilities);
            //				return requestSucceeded;
            //			}

            #region Parse Response for legitimacy
            //			availabilitiesUpdated = parseResponseToSend(response);
            //			if (availabilitiesUpdated < 0)
            //			{
            //				errDescription.errNum = 2001;
            //				errDescription.errorForUser = "Error Updating Availability ";
            //				errDescription.logFileDescription = response;
            //				errDescription.errorForUser = errDescription.errorForUser;
            //
            //				LogThis(DateTime.Now, "Failure attempting to upload availability: ", 
            //					errDescription.errNum.ToString() + " " + errDescription.errorForUser,	currentFunction, errDescription.logFileDescription);
            //
            //				requestSucceeded = false;
            //				return requestSucceeded;
            //			}
            #endregion

            #endregion

            //We have succeeded!
            requestSucceeded = true;

            for (int counter = 0; counter < theseWebRoomTypes.Count; counter++)
            {
                clsWebRoomType thisWebRoomType = (clsWebRoomType)theseWebRoomTypes[counter];
                thisWebRoomType.AvailabilityAtTimeOfLastUpdate = thisWebRoomType.Availability;
                //Not Required				SetWebRoomType(counter);
            }
            for (int counter = 0; counter < theseRhsRoomTypes.Count; counter++)
            {
                clsRhsRoomType thisRhsRoomType = (clsRhsRoomType)theseRhsRoomTypes[counter];
                thisRhsRoomType.AvailabilityAtTimeOfLastUpdate = thisRhsRoomType.Availability;
                //Not Required				SetRhsRoomType(counter);
            }
            Save();

            #region Do some logging


            //			switch (availabilitiesUpdated)
            //			{
            //				case 0:
            //					LogThis(DateTime.Now, "Zero availability Records Updated." , 
            //						"",	currentFunction, "");
            //					break;
            //				case 1:
            //					LogThis(DateTime.Now, "Successfully Updated: 1 record." , 
            //						"",	currentFunction, "");
            //					break;
            //				default:
            //					LogThis(DateTime.Now, "Successfully Updated: " + availabilitiesUpdated + " records." , 
            //						"",	currentFunction, "");
            //					break;
            //			}

            switch (numWebBookingsToAcknowledge)
            {
                case 0:
                    //Do Nothing
                    break;
                case 1:
                    LogThis(DateTime.Now, "Acknowledged receipt of: 1 booking.",
                        "", currentFunction, "");
                    break;
                default:
                    LogThis(DateTime.Now, "Acknowledged receipt of: " + (numWebBookingsToAcknowledge).ToString() + " bookings.",
                        "", currentFunction, "");
                    break;
            }
            #endregion

            return requestSucceeded;

        }


        #endregion

    }
}
