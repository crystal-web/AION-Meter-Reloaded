using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using AIONMeter;

namespace AIONMeter.libs
{
    class SkillsNotFoundTracker
    {
        List<string> preventMultiSend = new List<string>();

        public void append(string skill) {
            if (!preventMultiSend.Contains(skill))
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://crystal-web.org/aionmeterSkillNotFoundTracker.php?skill=" + skill + "&version=" + Program.version + "&lng=" + Config.get_language());
                WebResponse response = request.GetResponse();
                StreamReader web_reader = new StreamReader(response.GetResponseStream(), Encoding.Unicode);
                string result = web_reader.ReadToEnd();
                preventMultiSend.Add(skill);
            }
        }

    }
}
