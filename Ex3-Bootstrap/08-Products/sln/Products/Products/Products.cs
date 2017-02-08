using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Products
{
    class Products
    {
        static void Main()
        {
            Console.WriteLine("Content-type:text/html\r\n\r\n");
            Console.WriteLine(@"<!DOCTYPE html>
                                <html lang=""en"">

                                <head>
                                    <title></title>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"">
                                    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js""></script>
                                    <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js""></script>
                                    <link rel=""stylesheet"" type=""text/css"" href=""/styles/products.css"">
                                </head>
                                <body>");

            var requestType = Environment.GetEnvironmentVariable("REQUEST_METHOD");
            Console.WriteLine("<div>Method: {0}</div>", requestType);
            if(requestType == "GET")
            {
                List<OrderStatusType> statusTypes = new List<OrderStatusType>();
                using (var reader = new StreamReader("status.csv"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var kvp = line.Split(',');
                        var key = int.Parse(kvp[0]);
                        var value = kvp[1];
                        statusTypes.Add(new OrderStatusType() { Id = key, OrderType = value });
                    }
                }
                Console.WriteLine("<div>Status count: " + statusTypes.Count + "</div>");


                List<Product> products = new List<Product>();
                using (var reader = new StreamReader("orders.csv"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] productInfo = line.Split(',');

                        Product productEntry = new Product()
                        {
                            Id = int.Parse(productInfo[0]),
                            Name = productInfo[1],
                            ProductType = productInfo[2],
                            PaymentDate = productInfo[3],
                            DeliveryDate = productInfo[4],
                            OrderStatus = statusTypes.FirstOrDefault(s => s.Id == int.Parse(productInfo[5])).OrderType
                        };
                        products.Add(productEntry);
                    }
                }
                Console.WriteLine("<div>Product count: " + products.Count + "</div>");
                RenderProductsTable(ref products);
            }
            Console.WriteLine(@"</body>
                                </html>");
        }

        private static void RenderProductsTable(ref List<Product> products)
        {
            Console.WriteLine(@"<div class=""container"">
                                <h3 class=""text-muted h5"">Main Orders</h3>
                                <table id=""products-table"" class=""table"">
                                    <tr>
                                        <th>ID</th>
                                        <th>Name</th>
                                        <th>Payment Date</th>
                                        <th>Delivery Date</th>
                                        <th>Order Status</th>
                                        <th>Modify</th>
                                    </tr>");
            foreach (var product in products)
            {
                Console.WriteLine($"<tr class='{GetProductClass(product.OrderStatus)}'>" +
                                        $"<td>{product.Id}</td>" +
                                        $"<td><span>{product.Name}</span> ({product.ProductType})</td>" +
                                        $"<td>{product.PaymentDate}</td>" +
                                        $"<td>{product.DeliveryDate}</td>" +
                                        $"<td>{product.OrderStatus}</td>" +
                                        $"<td><a href=\"EditProduct.exe?Id={product.Id}\">Edit</a></td>" +
                                    "</tr>");
            }

            Console.WriteLine(@"</table>
                                </div>");
        }

        private static string GetProductClass(string orderStatus)
        {
            switch (orderStatus)
            {
                case "Delivered":
                    return "delivered";
                case "Declined":
                    return "declined";
                case "In Call to Confirm":
                    return "not-confirmed";
                case "Pending":
                default:
                    return "";
            }
        }
    }
}
