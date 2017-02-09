using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EditProduct
{
    class EditProduct
    {
        static void Main()
        {
            Console.WriteLine("Content-type:text/html\r\n\r\n");
            Console.WriteLine(@"<!DOCTYPE html>
                                <html lang=""en"">
                                    <head>
                                        <title>Edit Products</title>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"">
                                    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js""></script>
                                    <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js""></script>
                                    <link rel=""stylesheet"" type=""text/css"" href=""/style/products.css"">
                                    </head>
                                    <body>");


            var requestType = Environment.GetEnvironmentVariable("REQUEST_METHOD");
            Console.WriteLine("<div>Method: {0}</div>", requestType);

            if (requestType == "GET")
            {
                var query = Environment.GetEnvironmentVariable("QUERY_STRING");
                Console.WriteLine("<div>Query: {0}</div>", query);

                string[] tokens = query.Split(new char[] { '&', '=' }, StringSplitOptions.RemoveEmptyEntries);
                int editId = int.Parse(tokens[1]);

                List<OrderStatusType> statusTypes = new List<OrderStatusType>();
                using (var reader = new StreamReader("status.csv"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var kvp = line.Split(',');
                        var key = int.Parse(kvp[0]);
                        var value = kvp[1];
                        statusTypes.Add(new OrderStatusType() { Id = key, OrderStatus = value });
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
                            OrderStatusId = statusTypes.FirstOrDefault(s => s.Id == int.Parse(productInfo[5])).Id
                        };
                        products.Add(productEntry);
                    }
                }
                Console.WriteLine("<div>Product count: " + products.Count + "</div>");

                var product = products.FirstOrDefault(p => p.Id == editId);
                Console.WriteLine("<div>Product to show: " + product.Name + "</div>");


                DrawEditForm(product);
            }
            else if(requestType == "POST")
            {
                string input = Console.ReadLine();
                Console.WriteLine("<div>Heloo</div>");
                if(input != "")
                {
                    // process update
                    Console.WriteLine($"<div>Heloo: {input} blabal</div>");

                    List<OrderStatusType> statusTypes = new List<OrderStatusType>();
                    using (var reader = new StreamReader("status.csv"))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var kvp = line.Split(',');
                            var key = int.Parse(kvp[0]);
                            var value = kvp[1];
                            statusTypes.Add(new OrderStatusType() { Id = key, OrderStatus = value });
                        }
                    }

                    string[] tokens = input.Split(new char[] { '&', '=' }, StringSplitOptions.RemoveEmptyEntries);
                    var productId = int.Parse(tokens[1]);
                    var productNewStatus = int.Parse(tokens[3]);

                    string[] lines = File.ReadAllLines("orders.csv");

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string line = lines[i];
                        if(line.Contains(','))
                        {
                            var lineData = line.Split(',');
                            if(int.Parse(lineData[0]) == productId)
                            {
                                lineData[5] = productNewStatus.ToString();
                                lines[i] = string.Join(",", lineData);
                                break;
                            }
                        }
                    }

                    File.WriteAllLines("orders.csv", lines);
                    Console.WriteLine($@"<div>Succesfully edited status of order with ID: {productId}</div>
                                         <a href=""Products.exe"">Back to Orders</a>");
                }
            }

            Console.WriteLine(@"</body>
                                </html>");
        }

        private static void DrawEditForm(Product product)
        {
            Console.WriteLine($@"<div class=""container"">
                                <a href=""Products.exe"">Back to Orders</a>
                                <h3>Edit Order</h3>
                                <form action=""EditProduct.exe"" method=""post"" class=""form-horizontal col-md-6 col-xs-offset-1"">
                                    <div class=""form-group"">
                                        <label for=""Id"" class=""control-label col-sm-3"">Id</label>
                                        <div class=""col-sm-9"">
                                            <input type=""number"" class=""form-control"" id=""Id"" readonly name=""productId"" value=""{product.Id}"">
                                        </div>
                                    </div>
                                    <div class=""form-group"">
                                        <label for=""Name"" class=""control-label col-sm-3"">Name</label>
                                        <div class=""col-sm-9"">
                                            <input type=""text"" class=""form-control"" id=""Name"" readonly value=""{product.Name}"">
                                        </div>
                                    </div>
                                    <div class=""form-group"">
                                        <label for=""Type"" class=""control-label col-sm-3"">Type</label>
                                        <div class=""col-sm-9"">
                                            <input type=""text"" class=""form-control"" id=""Type"" readonly value=""{product.ProductType}"">
                                        </div>
                                    </div>
                                    <div class=""form-group"">
                                        <label for=""pDate"" class=""control-label col-sm-3"">Payment Date</label>
                                        <div class=""col-sm-9"">
                                            <input type=""text"" class=""form-control"" id=""pDate"" readonly value=""{product.PaymentDate}"">
                                        </div>
                                    </div>
                                    <div class=""form-group"">
                                        <label for=""dDate"" class=""control-label col-sm-3"">Delivery Date</label>
                                        <div class=""col-sm-9"">
                                            <input type=""text"" class=""form-control"" id=""dDate"" readonly value=""{product.DeliveryDate}"">
                                        </div>
                                    </div>
                                    <div class=""form-group"">
                                        <label for=""status"" class=""control-label col-sm-3"">Status</label>
                                        <div class=""col-sm-9"">
                                            <select class=""form-control"" id=""status"" name=""orderStatus"">
                                                    <option value=""1"" {((product.OrderStatusId == 1) ? "selected" : "")}>Pending</option>
                                                    <option value=""2"" {((product.OrderStatusId == 2) ? "selected" : "")}>Delivered</option>
                                                    <option value=""3"" {((product.OrderStatusId == 3) ? "selected" : "")}>Declined</option>
                                                    <option value=""4"" {((product.OrderStatusId == 4) ? "selected" : "")}>In Call to Confirm</option>
                                                </select>
                                        </div>
                                    </div>
                                    <button type=""submit"" class=""btn btn-primary pull-right"">Update</button>
                                </form>
                            </div>");
        }
    }
}
