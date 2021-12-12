using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites {
    public class Message {

        #region Private Variables

        private int _Type;
        private string _Message;

        #endregion

        #region Constructors

        public Message(type type, string message) {
            _Type = (int)type;
            _Message = message;
        }

        #endregion

        #region Public Properties

        public enum type {
            Error = 1,
            Success = 0,
            Warning = -1
        }

        public string Type {
            get {
                string MessageType = "Type not found";
                if (_Type == 1) {
                    MessageType = "Error";
                } else if (_Type == 0) {
                    MessageType = "Success";
                } else if (_Type == -1) {
                    MessageType = "Warning";
                }
                return MessageType;
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