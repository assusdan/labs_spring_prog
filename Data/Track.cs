using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Labs
{
    class Track
    {

        public const String UnknownAuthor = "Unknown author";

        public String TrackName { get; private set; }
        public String AuthorName { get; private set; }
        public String FullName
        {
            get
            {
                return new StringBuilder().Append(AuthorName).Append(" - ").Append(TrackName).ToString();
            }
        }

        public Track(String authorName, String trackName)
        {
            TrackName = trackName;
            AuthorName = authorName;
        }


        public Track(String trackName) : this(UnknownAuthor, trackName)
        {
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) return false;

            if (obj.GetType().Equals(typeof(Track))) 
            {
                return (((Track)obj).FullName.Equals(FullName));
            }

            return false;
        }

        public override int GetHashCode()
        {
            return 733961487 + EqualityComparer<string>.Default.GetHashCode(FullName);
        }
    }
}
