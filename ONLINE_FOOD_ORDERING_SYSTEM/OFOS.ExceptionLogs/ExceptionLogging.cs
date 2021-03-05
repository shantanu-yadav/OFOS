using System;
using System.IO;

namespace OFOS.ExceptionLogs
{
    public class ExceptionLogging
    {

        private static String ErrorlineNo, Errormsg, extype, ErrorLocation;

        public void LogInFile(Exception ex)
        {

            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();
            ErrorLocation = ex.Message.ToString();

            try
            {
                string filepath = @".\";

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + @"\" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
                if (!File.Exists(filepath))
                {


                    File.Create(filepath).Dispose();

                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + "\n" + "Error Line No :" + " " + ErrorlineNo + "\n" + "Error Message:" + " " + Errormsg + "\n" + "Exception Type:" + " " + extype + "\n" + "Error Location :" + " " + ErrorLocation + "\n";
                    sw.WriteLine("Exception Details on " + " " + DateTime.Now.ToString());
                    sw.WriteLine("\n");
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------------------------------------------------");
                    sw.WriteLine("\n");
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

    }
}
