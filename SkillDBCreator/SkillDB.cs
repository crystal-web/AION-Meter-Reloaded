/* 
AIONMeter is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

AIONMeter is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with AIONMeter.  If not, see <http://www.gnu.org/licenses/>.

Hüseyin Uslu, <shalafiraistlin nospam gmail dot com> 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace SkillDBCreator
{
    public class Skill : AIONMeter.Skill
    {
        public Skill(XmlNode skill_node)
        {
            id = Int32.Parse(skill_node.ChildNodes[0].InnerText);
            Console.Write("Processing skill ID: " + id.ToString() + ".. ");

            if ((skill_node.SelectSingleNode("type") != null) && (skill_node.SelectSingleNode("sub_type") != null))
            {
                // Add to types list
                string _type = skill_node.SelectSingleNode("type").InnerText.Trim().ToLower().Replace("İ", "I");
                try { SkillDB.types_list.Add(_type, SkillDB.types_list.Count); }
                catch (Exception e) { }

                // add to sub_types list
                string _sub_type = skill_node.SelectSingleNode("sub_type").InnerText.Trim().ToLower().Replace("İ", "I");
                try { SkillDB.sub_types_list.Add(_sub_type, SkillDB.sub_types_list.Count); }
                catch (Exception e) { }

                // set the type and sub_type
                this.sub_type = (AIONMeter.SUB_TYPES)SkillDB.sub_types_list[_sub_type];
                this.type = (AIONMeter.TYPES)SkillDB.types_list[_type];


                // query the skill name
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.aionarmory.com/ajaxTooltip.aspx?id=" + id.ToString() + "&type=6");
                WebResponse response = request.GetResponse();
                StreamReader web_reader = new StreamReader(response.GetResponseStream(), Encoding.Unicode);
                string result = web_reader.ReadToEnd();
                Match m;
                if ((m = SkillDB.pattern.Match(result)).Success)
                {
                    name = m.Groups["name"].Value;
                    name = name.Replace(@"\'", "'");
                    name = name.Replace(@"’", "'");
                }
                Console.WriteLine(name);



                // check the effect types
                this.effect_type = AIONMeter.EFFECT_TYPES.NONE;
                bool found_effect_type = false;

                for (int i = 1; i <= 3; i++)
                {
                    string effect = "effect" + i.ToString();
                    string effect_type = effect + "_type";
                    string effect_remain2 = effect + "_remain2";
                    string effect_checktime = effect + "_checktime";

                    if (skill_node.SelectSingleNode(effect_type) != null) // check the effect
                    {
                        // add the effect type
                        string _effect_type = skill_node.SelectSingleNode(effect_type).InnerText.ToUpper().Replace('İ', 'I');
                        try { SkillDB.effect_types_list.Add(_effect_type, SkillDB.effect_types_list.Count); }
                        catch (Exception e) { }

                        AIONMeter.EFFECT_TYPES et = (AIONMeter.EFFECT_TYPES)SkillDB.effect_types_list[_effect_type];
                        switch (et)
                        {
                            case AIONMeter.EFFECT_TYPES.SPELLATK:
                            case AIONMeter.EFFECT_TYPES.BLEED:
                            case AIONMeter.EFFECT_TYPES.POISON:
                            case AIONMeter.EFFECT_TYPES.HEAL:
                                effect_period = Int32.Parse(skill_node.SelectSingleNode(effect_remain2).InnerText) / 1000;
                                try
                                {
                                    effect_tick = Int32.Parse(skill_node.SelectSingleNode(effect_checktime).InnerText) / 1000;
                                }
                                catch (Exception e)
                                {
                                    effect_tick = 0;
                                }
                                this.effect_type = et;
                                found_effect_type = true;
                                break;
                        }
                    }

                    if (found_effect_type)
                        break;
                }

                try
                {
                    SkillDB.list.Add(name, this);
                }
                catch (Exception e)
                { }
            }
        }

        private string get_child_node(XmlNode node, string name)
        {
            XmlNode n = node.SelectSingleNode(name);

            return "";
        }
    }

    public static class SkillDB
    {
        public static Dictionary<string, Skill> list;
        public static Regex pattern;

        public static Dictionary<String, int> types_list;
        public static Dictionary<String, int> sub_types_list;
        public static Dictionary<String, int> effect_types_list;


        public static void Run()
        {
            list = new Dictionary<string, Skill>();
            types_list = new Dictionary<string, int>();
            sub_types_list = new Dictionary<string, int>();
            effect_types_list = new Dictionary<string, int>();
            effect_types_list.Add("NONE", 0);

            pattern = new Regex("<span class=\"spell-name\">(?<name>.*?)<\\/span>", RegexOptions.Compiled);

            XmlDocument doc = new XmlDocument();
            doc.Load("client_skills.xml");

            XmlNodeList skill_nodes = doc.GetElementsByTagName("skill_base_client");

            int i = 0;
            foreach (XmlNode skill_node in skill_nodes)
            {
                Skill s = new Skill(skill_node);
                i++;
                //if (i > 25) break;
            }

            write_types_db(types_list, "types.db");
            write_types_db(sub_types_list, "sub_types.db");
            write_types_db(effect_types_list, "effect_types.db");

            // Now save the parsed skill info
            build_skill_db();

        }

        private static void write_types_db(Dictionary<String, int> dic, string file)
        {
            StreamWriter w = new StreamWriter(file, false);
            foreach (KeyValuePair<string, int> entry in dic)
            {
                string val = entry.Key;
                w.WriteLine(val + ",");
            }
            w.Close();
        }

        public static void build_skill_db()
        {
            StreamWriter w = new StreamWriter("skill.db", false);
            foreach (KeyValuePair<string, Skill> entry in list)
            {
                if (entry.Value.name != null)
                {
                    string line = "list.Add(\"" + entry.Value.name + "\",";
                    line += "new Skill(" + entry.Value.id + ",";
                    line += "\"" + entry.Value.name + "\",";
                    line += "TYPES." + entry.Value.type + ",";
                    line += "SUB_TYPES." + entry.Value.sub_type + ",";
                    line += "EFFECT_TYPES." + entry.Value.effect_type + ",";
                    line += entry.Value.effect_period + ",";
                    line += entry.Value.effect_tick + "));";
                    w.WriteLine(line);
                }
            }
            w.Close();
            Console.WriteLine("Skills.db saved..");
            Console.ReadKey();
        }
    }
}
