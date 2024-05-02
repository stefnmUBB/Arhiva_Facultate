using System.IO;
using System;
using System.Xml.Serialization;

namespace SGBD.Lab2
{
    [XmlRoot("config")]
    public class Config
    {
        [XmlAttribute("parentTable")]
        public string ParentTable { get; set; }

        [XmlAttribute("childTable")]
        public string ChildTable { get; set; }

        public static Config FromXml(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));

            Config config;

            using (Stream reader = new FileStream(path, FileMode.Open))
                config = serializer.Deserialize(reader) as Config;       

            return config;
        }
    }
}
