using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.Models
{
    [Serializable]
    public class SavedSettings : ISerializable
    {
        public string FindText;
        public bool IsFindCaseSensitive;
        public bool IsFindCirculated;
        public bool IsFindUp;
        public string ReplacedText;

        public SavedSettings()
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FindText", FindText, typeof(string));
            info.AddValue("IsFindCaseSensitive", IsFindCaseSensitive, typeof(bool));
            info.AddValue("IsFindCirculated", IsFindCirculated, typeof(bool));
            info.AddValue("IsFindUp", IsFindUp, typeof(bool));
            info.AddValue("ReplacedText", ReplacedText, typeof(string));
        }

        public SavedSettings(SerializationInfo info, StreamingContext context)
        {
            FindText = (string)info.GetValue("FindText", typeof(string));
            IsFindCaseSensitive = info.GetBoolean("IsFindCaseSensitive");
            IsFindCirculated = info.GetBoolean("IsFindCirculated");
            IsFindUp = info.GetBoolean("IsFindUp");
            ReplacedText = info.GetString("ReplacedText");
        }
    }
}
