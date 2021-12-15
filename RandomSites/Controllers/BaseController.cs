using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites {
    public class BaseController : Controller {

        public BaseController() {
        }

        private List<Message> _Messages;// = new List<Message>();

        public override void OnActionExecuting(ActionExecutingContext context) {
            base.OnActionExecuting(context);
            _Messages = GetMessages();
            TempData["Messages"] = _Messages;
            _Messages.Clear();
        }

        private List<Message> GetMessages() {
            List<Message> retList = new List<Message>();
            if(TempData != null && TempData.ContainsKey("Messages")) {
                object TempValue = TempData["Messages"];
                if(TempValue != null) {
                    Message[] messages = (Message[])TempData["Messages"];
                    retList = messages.ToList();
                }
            }
            return retList;
        }

        public void AddMessage(Message.type MessageType,string Message) {
            _Messages.Add(new Message(MessageType, Message));
        }

    }
}
