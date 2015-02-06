using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AIONMeter
{
    public class Filter : IDisposable // Regex Filter
    {
        public Regex regex; // the regex
        public delegate void delegate_callback(GroupCollection matches);
        public delegate_callback callback; // the callback function for a match
        public Boolean combat_filter; // is it a filter that match combat actions?
        private bool disposed = false;

        public Filter(string pattern, delegate_callback _callback)
        {
            regex = new Regex(pattern, RegexOptions.Compiled); // Compile the regex for faster matches
            callback = _callback;
            combat_filter = false;
        }

        public Filter(string pattern, delegate_callback _callback, Boolean _combat_filter)
        {
            regex = new Regex(pattern, RegexOptions.Compiled); // Compile the regex for faster matches
            callback = _callback;
            combat_filter = _combat_filter;
        }

        public Boolean Run(string line)
        {
            Match m;
            if ((m = regex.Match(line)).Success) // if a match
            {
                callback(m.Groups); // call the callback function with match groups
                return true;
            }
            return false;
        }

        ~Filter()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // the GC shouldn't try to finalize the object as disposed it
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    callback = null;
                    regex = null;
                }
            }
            disposed = true;
        }
    }
}
