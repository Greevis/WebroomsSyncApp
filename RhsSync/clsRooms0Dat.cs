using System;

namespace RhsSync
{
    /// <summary>
    /// Summary description for clsRhsRoomDataFile.
    /// </summary>
    public class clsRooms0Dat : clsBase
    {
        /// <summary>
        /// Initialiser
        /// </summary>
        public clsRooms0Dat()
        {
        }

        #region Publicly Exposed Variables

        /// <summary>
        /// Room Number
        /// </summary>
        public int number;					//Max Length 2 (assuming 16 bit int)

        /// <summary>
        /// Index
        /// </summary>
        public int Index;				//Max Length 2 (assuming 16 bit int)

        /// <summary>
        /// Whether this room is allowed to be allocated
        /// </summary>
        public bool inUse;				//Max Length 1

        #endregion

    }
}
