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

        List<Message> Messages = new List<Message>();

        public override void OnActionExecuting(ActionExecutingContext context) {
            base.OnActionExecuting(context);
            TempData["Messages"] = Messages;
            Messages.Clear();
        }

        public void AddMessage(Message.type MessageType,string Message) {
            Messages.Add(new Message(MessageType, Message));
        }

    }
}
