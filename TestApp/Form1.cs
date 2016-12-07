using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisiOnlineInterface;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Auxillary Methods

        public const string crLf = "\r\n";

        /// <summary>
        /// DelimitedListToArrayList
        /// </summary>
        /// <param name="DelimitedList">DelimitedList</param>
        /// <param name="Delimiter">Delimiter</param>
        /// <returns>System.Collections.ArrayList</returns>
        public System.Collections.ArrayList DelimitedListToArrayList(string DelimitedList, string Delimiter)
        {
            System.Collections.ArrayList retVal = new System.Collections.ArrayList();
            int delimiterIndex = DelimitedList.IndexOf(Delimiter);
            int lenDelimiter = Delimiter.Length;

            while (delimiterIndex > -1)
            {
                string thisValue = DelimitedList.Substring(0, delimiterIndex);

                retVal.Add(thisValue);
                DelimitedList = DelimitedList.Substring(delimiterIndex + lenDelimiter);
                delimiterIndex = DelimitedList.IndexOf(Delimiter);
            }

            return retVal;
        }

        #endregion


        private void button11_Click(object sender, EventArgs e)
        {
            string delimiter = ";";

            string VisionlineIP = txtCreateKeyVisionlineIP.Text;
            string EndpointId = txtCreateKeyEndpointId.Text;
            string Username = txtCreateKeyUsername.Text;
            string Password = txtCreateKeyPassword.Text;

            ArrayList Doors = new ArrayList();
            string thisDoors = txtCreateKeyDoors.Text.Trim();

            if (thisDoors.Length > 0)
            {
                if (!thisDoors.EndsWith(delimiter))
                    thisDoors += delimiter;
                Doors = DelimitedListToArrayList(thisDoors, delimiter);
            }

            DateTime ExpireTime = DateTime.Now.AddDays(1);
            string thisExpireTime = txtCreateKeyExpireTime.Text.Trim();

            if (thisExpireTime.Length > 0)
            {
                try
                {
                    ExpireTime = Convert.ToDateTime(thisExpireTime);
                }
                catch (Exception error)
                {
                    ExpireTime = DateTime.Now.AddDays(1);
                }
            }

            string Description = txtCreateKeyDescription.Text;

            clsAccessVisiOnline thisAccessVisiOnline = new clsAccessVisiOnline();
            clsCreateKeyResponse thisAccessVisiOnlineResponse = thisAccessVisiOnline.CreateMobileAccessKeyFromParameters(
                VisionlineIP,
                EndpointId,
                Username,
                Password,
                Doors,
                ExpireTime);

            MessageBox.Show("thisAccessVisiOnlineResponse: " + crLf
                + "Success: " + thisAccessVisiOnlineResponse.thisError.Success.ToString() + crLf
                + "ErrorMessage: " + thisAccessVisiOnlineResponse.thisError.ErrorMessage + crLf
                + "Request: " + thisAccessVisiOnlineResponse.Request + crLf
                + "RawResponse: " + thisAccessVisiOnlineResponse.RawResponse + crLf
                + "credentialId: " + thisAccessVisiOnlineResponse.thisKeyResponse.credentialId + crLf
                + "accessKey: " + thisAccessVisiOnlineResponse.thisSessionResponse.accessKey+ crLf
                + "id: " + thisAccessVisiOnlineResponse.thisSessionResponse.id + crLf
                );

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string delimiter = ";";

            string VisionlineIP = txtCreateKeyVisionlineIP.Text;
            string EndpointId = txtCreateKeyEndpointId.Text;
            string Username = txtCreateKeyUsername.Text;
            string Password = txtCreateKeyPassword.Text;

            ArrayList Doors = new ArrayList();
            string thisDoors = txtCreateKeyDoors.Text.Trim();

            if (thisDoors.Length > 0)
            {
                if (!thisDoors.EndsWith(delimiter))
                    thisDoors += delimiter;
                Doors = DelimitedListToArrayList(thisDoors, delimiter);
            }

            DateTime ExpireTime = DateTime.Now.AddDays(1);
            string thisExpireTime = txtCreateKeyExpireTime.Text.Trim();

            if (thisExpireTime.Length > 0)
            {
                try
                {
                    ExpireTime = Convert.ToDateTime(thisExpireTime);
                }
                catch (Exception error)
                {
                    ExpireTime = DateTime.Now.AddDays(1);
                }
            }

            string Description = txtCreateKeyDescription.Text;

            clsAccessVisiOnline thisAccessVisiOnline = new clsAccessVisiOnline();
            clsGetDoorsResponse thisGetDoorsResponse = thisAccessVisiOnline.GetDoorsFromParameters(
                VisionlineIP,
                EndpointId,
                Username,
                Password);

            string DoorMessage = "No Doors Found" + crLf;

            if (thisGetDoorsResponse.thisError.Success == 1 && thisGetDoorsResponse.thisDoorResponse != null)
            {
                int numDoors = thisGetDoorsResponse.thisDoorResponse.Doors.Count;

                DoorMessage = numDoors.ToString() + " Door(s) found" + crLf;

                for (int counter = 0; counter < numDoors; counter++)
                {
                    DoorMessage += counter.ToString() + " " + thisGetDoorsResponse.thisDoorResponse.Doors[counter].Id + crLf;
                }
            }

            MessageBox.Show("thisGetDoorsResponse: " + crLf
                + "Success: " + thisGetDoorsResponse.thisError.Success.ToString() + crLf
                + "ErrorMessage: " + thisGetDoorsResponse.thisError.ErrorMessage + crLf
                + "Request: " + thisGetDoorsResponse.Request + crLf
                + "RawResponse: " + thisGetDoorsResponse.RawResponse + crLf
                + DoorMessage
                + "accessKey: " + thisGetDoorsResponse.thisSessionResponse.accessKey + crLf
                + "id: " + thisGetDoorsResponse.thisSessionResponse.id + crLf
                );
        }



        private void button2_Click(object sender, EventArgs e)
        {
            string delimiter = ";";

            string VisionlineIP = txtCreateKeyVisionlineIP.Text;
            string EndpointId = txtCreateKeyEndpointId.Text;
            string Username = txtCreateKeyUsername.Text;
            string Password = txtCreateKeyPassword.Text;

            DateTime ExpireTime = DateTime.Now.AddDays(1);
            string thisExpireTime = txtCreateKeyExpireTime.Text.Trim();

            if (thisExpireTime.Length > 0)
            {
                try
                {
                    ExpireTime = Convert.ToDateTime(thisExpireTime);
                }
                catch (Exception error)
                {
                    ExpireTime = DateTime.Now.AddDays(1);
                }
            }

            string Description = txtCreateKeyDescription.Text;

            ArrayList Doors = new ArrayList();
            string thisDoors = txtCreateKeyDoors.Text.Trim();

            if (thisDoors.Length > 0)
            {
                if (!thisDoors.EndsWith(delimiter))
                    thisDoors += delimiter;
                Doors = DelimitedListToArrayList(thisDoors, delimiter);
            }

            if (Doors.Count > 0)
                thisDoors = (string) Doors[0];

            clsAccessVisiOnline thisAccessVisiOnline = new clsAccessVisiOnline();
            clsGetCardsResponse thisGetCardsResponse = thisAccessVisiOnline.GetCardsFromParameters(
                VisionlineIP,
                EndpointId,
                Username,
                Password,
                thisDoors);

            string CardMessage = "No Cards Found" + crLf;

            if (thisGetCardsResponse.thisError.Success == 1 && thisGetCardsResponse.thisCardResponse != null)
            {
                int numCards = thisGetCardsResponse.thisCardResponse.Cards.Count;

                CardMessage = numCards.ToString() + " Card(s) found" + crLf;

                for (int counter = 0; counter < numCards; counter++)
                {
                    CardMessage += counter.ToString() + " " + thisGetCardsResponse.thisCardResponse.Cards[counter].Id + crLf;
                }
            }

            MessageBox.Show("thisGetCardsResponse: " + crLf
                + "Success: " + thisGetCardsResponse.thisError.Success.ToString() + crLf
                + "ErrorMessage: " + thisGetCardsResponse.thisError.ErrorMessage + crLf
                + "Request: " + thisGetCardsResponse.Request + crLf
                + "RawResponse: " + thisGetCardsResponse.RawResponse + crLf
                + CardMessage
                + "accessKey: " + thisGetCardsResponse.thisSessionResponse.accessKey + crLf
                + "id: " + thisGetCardsResponse.thisSessionResponse.id + crLf
                );
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string delimiter = ";";

            string VisionlineIP = txtCreateKeyVisionlineIP.Text;
            string EndpointId = txtCreateKeyEndpointId.Text;
            string Username = txtCreateKeyUsername.Text;
            string Password = txtCreateKeyPassword.Text;
            string JoinerId = txtCreateKeyJoinerId.Text;

            ArrayList Doors = new ArrayList();
            string thisDoors = txtCreateKeyDoors.Text.Trim();

            if (thisDoors.Length > 0)
            {
                if (!thisDoors.EndsWith(delimiter))
                    thisDoors += delimiter;
                Doors = DelimitedListToArrayList(thisDoors, delimiter);
            }

            DateTime ExpireTime = DateTime.Now.AddDays(1);
            string thisExpireTime = txtCreateKeyExpireTime.Text.Trim();

            if (thisExpireTime.Length > 0)
            {
                try
                {
                    ExpireTime = Convert.ToDateTime(thisExpireTime);
                }
                catch (Exception error)
                {
                    ExpireTime = DateTime.Now.AddDays(1);
                }
            }

            string Description = txtCreateKeyDescription.Text;

            clsAccessVisiOnline thisAccessVisiOnline = new clsAccessVisiOnline();
            clsCreateKeyResponse thisAccessVisiOnlineResponse = thisAccessVisiOnline.CreateMobileAccessKeyJoinerFromParameters(
                VisionlineIP,
                EndpointId,
                Username,
                Password,
                Doors,
                ExpireTime);

            MessageBox.Show("thisAccessVisiOnlineResponse: " + crLf
                + "Success: " + thisAccessVisiOnlineResponse.thisError.Success.ToString() + crLf
                + "ErrorMessage: " + thisAccessVisiOnlineResponse.thisError.ErrorMessage + crLf
                + "Request: " + thisAccessVisiOnlineResponse.Request + crLf
                + "RawResponse: " + thisAccessVisiOnlineResponse.RawResponse + crLf
                + "credentialId: " + thisAccessVisiOnlineResponse.thisKeyResponse.credentialId + crLf
                + "accessKey: " + thisAccessVisiOnlineResponse.thisSessionResponse.accessKey + crLf
                + "id: " + thisAccessVisiOnlineResponse.thisSessionResponse.id + crLf
                );
        }
    }
}
