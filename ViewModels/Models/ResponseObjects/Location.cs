using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCage.Geocode
{
    [DataContract]
    public class Location
    {
        [DataMember(Name = "Annotations")]
        public Annotations Annotations { get; set; }

        [DataMember(Name = "Formatted")]
        public string Formatted { get; set; }

        [DataMember(Name = "Components")]
        public Dictionary<string, string> ComponentsDictionary { get; set; }

        [DataMember(Name = "Geometry")]
        public Point Geometry { get; set; }

        [DataMember(Name = "Bounds")]
        public Bounds Bounds { get; set; }

        [DataMember(Name = "Confidence")]
        public int Confidence { get; set; }
    }
}