using System;
using System.Collections.Generic;
using System.IO;

namespace MPScrabbleSoloutionV3
{
    internal class ReportLog
    {
        // Private Properties
        private DateTime currentTime = new DateTime();
        private List<string> Entrys = new List<string>();
        private string fileName;
        private string fName;
        // Public Properties
        public int totalEnteries = 0;

        public ReportLog(string FileName)
        {
            // create a file name containing the date and time
            fName = FileName;
            currentTime = DateTime.Now;
            CreateFileName(fName, currentTime);
        }
        //*********************************
        //*********************************
        private void CreateFileName(string f, DateTime dt)
        {
            // create a file name containing the date and time
            fileName = f + "-" + dt.ToString("yyyyMMddHHmm") + ".Log";
        }
        //*********************************
        public void AddEntry(string header, string detail)
        {
            // find out the current time
            currentTime = DateTime.Now;

            // add the current time to the log entry types and a
            string textHeader = currentTime.ToLongTimeString() + " - ";
            string textDetail = currentTime.ToLongTimeString() + " -- ";

            // if there is a header entry add that to the log
            if (header != "?")
            {
                textHeader += header;
                Entrys.Add(textHeader);
                totalEnteries++;
            }

            // if there is a detail entry add that to the log
            if (detail != "?")
            {
                textDetail += detail;
                Entrys.Add(textDetail);
                totalEnteries++;
            }
        }
        //*********************************
        public void PrintLog()
        {
            // Print the log to a text file
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                foreach (string line in Entrys)
                    outputFile.WriteLine(line);
            }
        }
        //*********************************
        public void Reset()
        {
            Entrys.Clear();
            // reset filename
            currentTime = DateTime.Now;
            CreateFileName(fName, currentTime);
        }
    }
}