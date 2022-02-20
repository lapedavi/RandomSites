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

        protected void AddSuccess(string mess) {
            AddMessage(mess, Message.MessageType.Success);
        }
        protected void AddWarning(string mess) {
            AddMessage(mess, Message.MessageType.Warning);
        }
        protected void AddError(string mess) {
            AddMessage(mess, Message.MessageType.Error);
        }

        protected void AddPermissionError(string actionVerb, string objectAttempted) {
            AddError(String.Format(
                "I am sorry, you are not allowed to {0} {1}.",
                actionVerb, objectAttempted));
        }

        protected void AddMessage(string Message, Message.MessageType messageType) {
            _Messages.Add(JsonSerializer.Serialize(new Message(messageType, Message)));
        }

    }
}
