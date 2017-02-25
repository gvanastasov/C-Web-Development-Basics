using SharpStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpStore.Pages
{
    public class ProductsPage : Page
    {

        private IList<Knive> knives;

        public ProductsPage(string htmlPath, IList<Knive> knives) : base(htmlPath)
        {
            this.knives = knives;
        }

        public ProductsPage() : this("../../content/products.html", new List<Knive>()) { }

        public ProductsPage(IList<Knive> knives) : this()
        {
            this.knives = knives;
        }


        private string GetProductsTemplate()
        {
            StringBuilder products = new StringBuilder();
            foreach (Knive knife in this.knives)
            {
                products.AppendLine($"<div class=\"col-sm-4\">\r\n\t\t\t\t" +
                                    $"<div class=\"thumbnail\">\r\n\t\t\t\t  " +
                                    $"<img src=\"{knife.ImageUrl}\">\r\n\t\t\t\t  " +
                                    $"<div class=\"caption\">\r\n\t\t\t\t\t" +
                                    $"<h3>{knife.Name}</h3>\r\n\t\t\t\t\t" +
                                    $"<p>${knife.Price}</p>\r\n\t\t\t\t\t" +
                                    $"<p><a href=\"#\" class=\"btn btn-primary\" role=\"button\">Buy Now" +
                                    $"</a></p>\r\n\t\t\t\t  " +
                                    $"</div>\r\n\t\t\t\t<" +
                                    $"/div>\r\n\t\t\t\t\r\n\t\t\t" +
                                    $"</div>");
            }

            return products.ToString();
        }

        public override string ToString()
        {
            var insertionIndex = base.ToString().IndexOf("</main>");

            return base.ToString().Insert(insertionIndex, this.GetProductsTemplate());
        }
    }
}
