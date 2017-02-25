using SharpStore.Data;
using SharpStore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpStore.Services
{
    public class MessagesService
    {
        private SharpStoreContext context;

        public MessagesService()
        {
            this.context = new SharpStoreContext();
        }

        public void AddMessageFromFormData(string formData)
        {
            var parameters = ParametersHandler.ParseRequestParameters(formData);

            if(parameters.Count == 3)
            {
                Message message = new Message()
                {
                    Sender = parameters["email"],
                    Subject = parameters["subject"],
                    Body = parameters["message"]
                };

                context.Messages.Add(message);
                context.SaveChanges();
            }
        }
    }
}
