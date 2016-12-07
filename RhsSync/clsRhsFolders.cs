using System;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.Collections;
using System.ComponentModel;
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

namespace RhsSync
{
    public class clsRhsFolders : clsBase
    {


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


    }
}
