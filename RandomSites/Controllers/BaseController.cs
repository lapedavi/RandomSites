using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RandomSites {
    public class BaseController : Controller {

        public BaseController() {
        }

        private List<string> _Messages;// = new List<Message>();

        public override void OnActionExecuting(ActionExecutingContext context) {
            base.OnActionExecuting(context);
            _Messages = GetMessages();
            TempData["Messages"] = _Messages;
        }

        private List<string> GetMessages() {
            List<string> retList = new List<string>();
            if (TempData != null && TempData.ContainsKey("Messages")) {
                object tempValue = TempData["Messages"];
                if (tempValue != null) { // verifying not empty
                    // got converted to a string array
                    string[] messages = (string[])TempData["Messages"];
                    retList = messages.ToList();
                }
            }
            return retList;
        }

        protected void AddMessage(Message.MessageType messageType, string Message) {
            _Messages.Add(JsonSerializer.Serialize(new Message(messageType, Message)));
        }

    }
}
