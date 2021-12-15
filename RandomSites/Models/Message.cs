using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites {
    public class Message {

        #region Private Variables

        private MessageType _Type;
        private string _Message;

        #endregion

        #region Constructors

        public Message(MessageType type, string message) {
            _Type = type;
            _Message = message;
        }

        #endregion

        #region Public Properties

        public enum MessageType {
            Error = 1,
            Success = 0,
            Warning = -1
        }

        public string Type {
            get {
                return Enum.GetName(typeof(MessageType),_Type);
            }
        }

        public string MessageString {
            get {
                return _Message;
            }
        }

        #endregion
    }
}