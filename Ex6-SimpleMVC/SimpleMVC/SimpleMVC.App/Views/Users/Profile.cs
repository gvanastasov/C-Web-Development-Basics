using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.Views.Users
{
    public class Profile : IRenderable<UserProfileViewModel>
    {
        public UserProfileViewModel Model
        {
            get; set;
        }

        public string Render()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<a href=\"/home/index\">Back to Home</a>");

            sb.AppendLine($"<h3>User: {Model.Username}</h3>");

            sb.AppendLine($"<form action=\"/notes/addnote?id={Model.UserId}\" method=\"POST\"><br/>");
            sb.AppendLine("<input type=\"text\" name=\"Title\"/><br/>");
            sb.AppendLine("<input type=\"text\" name=\"Content\"/><br/>");
            sb.AppendLine("<input type=\"submit\" value=\"Add Note\"<br/>");
            sb.AppendLine("</form><br/>");


            sb.AppendLine("<br/>");
            sb.AppendLine("<br/>");

            sb.AppendLine("<h3>Notes:</h3>");
            sb.AppendLine("<ul>");
            foreach (var note in Model.Notes)
            {
                sb.AppendLine($"<li>{note.Title} - {note.Content}</li>");
            }
            sb.AppendLine("</ul>");

            return sb.ToString();
        }
    }
}
