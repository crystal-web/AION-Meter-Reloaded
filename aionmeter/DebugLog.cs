using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AIONMeter
{
    public static class DebugLog // Debug logger, which matches read lines and parsed actions
    {
        private static StreamWriter debug_writer; // the output stream
        private static string debug_file; // the file name
        public static bool on = false; // is logger on?

        public static void start() // start the logger
        {
            Random r = new Random(DateTime.Now.Millisecond);
            debug_file = Config.get_application_path() + "/aionmeter_debug_" + r.Next().ToString() + ".log"; // put a random number on filename
            debug_writer = new StreamWriter(debug_file);
            on = true; // we're on now
        }

        public static void write_line(string data) // writes the line
        {
            try
            {
                debug_writer.WriteLine(data);
            }
            catch (Exception e){
                debug_writer.WriteLine(e.Message);
            }
        }

        public static void close() // closes the debuglog
        {
            try
            {
                debug_writer.Close(); // close the stream
                debug_writer = null;
                on = false; // we're no more on
                System.Diagnostics.Process.Start("explorer.exe", "/select," + debug_file); // browse the debug file using explorer
            }
            catch (Exception){}
        }
    }
}
