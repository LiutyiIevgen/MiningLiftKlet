using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ML.ConfigSettings.Model
{
    public abstract class AbstractConfig<T>
    {
        
        public void Load()
        {
            String fullfilename = CONFIG_FILE_PATH + System.IO.Path.DirectorySeparatorChar + XML_FILENAME;
            if (!File.Exists(fullfilename))
            {
                GetDefault();
            }
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (XmlTextReader reader = new XmlTextReader(fullfilename))
                {
                    var config = GetObject();
                    config = (T)ser.Deserialize(reader);
                    CopyObject(config);
                    //elem;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The XML object could not be deserialized from file! \n" + ex.ToString());
                GetDefault();
            }
        }

        public void Save()
        {
            String fullfilename = CONFIG_FILE_PATH + System.IO.Path.DirectorySeparatorChar + XML_FILENAME;

            XmlSerializer xmlGen = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.UTF8;
            using (XmlWriter writer = XmlWriter.Create(fullfilename, settings))
            {
                xmlGen.Serialize(writer, GetObject());
                writer.Close();
            }
        }

        protected abstract T GetObject();

        protected abstract void GetDefault();

        protected abstract void CopyObject(T config);

        private static string GetWindowsLoginName()
        {
            // 422 Account holen
            String user422 = "error retrieving 422 username";
            System.Security.Principal.WindowsIdentity aWI;
            aWI = System.Security.Principal.WindowsIdentity.GetCurrent();
            if (aWI.Name.Contains("\\"))
            {
                String[] splitDomain = Regex.Split(aWI.Name, "\\\\");
                user422 = splitDomain[splitDomain.Length - 1];
            }
            else
            {
                user422 = aWI.Name;
            }
            return user422;
        }

        private static String XML_FILENAME = "MiningLift.config."
            + GetWindowsLoginName()
            + "-" + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString()
            + ".xml";

        private static String CONFIG_FILE_PATH =
             Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
