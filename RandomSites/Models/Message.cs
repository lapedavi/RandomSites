using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RandomSites {

    public class Message {

        #region Private Variables

        private MessageType _Type;
        private string _MessageString;

        #endregion

        #region Constructors

        public Message() {

        }

        public Message(MessageType type, string message) {
            _Type = type;
            _MessageString = message;
        }

        #endregion

        #region Public Properties

        public enum MessageType {
            Error = 1,
            Success = 0,
            Warning = -1
        }

        [JsonIgnore]
        public string Type {
            get {
                return Enum.GetName(typeof(MessageType), _Type);
            }
        }

        public string MessageString {
            get {
                return _MessageString;
            }
            set {
                _MessageString = value;
            }
        }

        public int TypeInt {
            get {
                return ((int)_Type);
            }
            set {
                if (value == 1) {
                    _Type = MessageType.Error;
                } else if (value == 0) {
                    _Type = MessageType.Success;
                } else if (value == -1) {
                    _Type = MessageType.Warning;
                }
            }
        }

        #endregion

    }

    public static class Messages {

        public static List<Message> Deserialize(List<string> MessageList) {
            List<Message> retList = new List<Message>();
            foreach (string jsonString in MessageList) {
                Message message = JsonSerializer.Deserialize<Message>(jsonString);
                retList.Add(message);
            }
            return retList;
        }

        public static string[] Serizalize(List<Message> MessageList) {
            List<string> retList = new List<string>();
            foreach (Message message in MessageList) {
                string jsonString = JsonSerializer.Serialize(message);
                retList.Add(jsonString);
            }
            return retList.ToArray();
        }
    }
}
