using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Survey
{
    class Survey
    {
        static Dictionary<string, Action> fill = new Dictionary<string, Action>();

        static void Main()
        {
            Console.WriteLine("Content-Type: text/html \n\r");
            Console.WriteLine(@"<!DOCTYPE html>
            <html lang=""en"">
                <head>
                    <title></title>
                    <meta charset=""UTF-8"">
                    <link href=""/styles/style.css"" rel=""stylesheet"" type=""text/css"">
                    <script src=""/scripts/jquery-1.10.2.js""></script>
                    <script src=""/scripts/bootstrap.js""></script>
                </head>
                <body>");

            var requestType = Environment.GetEnvironmentVariable("REQUEST_METHOD");
            Console.WriteLine("<div>Method: {0}</div>", requestType);
            if(requestType == "GET")
            {
                GetForm();
            }
            else if(requestType == "POST")
            {
                var input = Console.ReadLine();

                string[] tokens = input.Split('&');
                foreach (var token in tokens)
                {
                    Console.WriteLine("<div>{0}</div>", token);
                }

                var result = new SurveyEntry()
                {
                    FirstName = tokens[0].Split('=')[1],
                    LastName = tokens[1].Split('=')[1],
                    BirthDate = tokens[2].Split('=')[1],
                    Gender = tokens[3].Split('=')[1],
                    Status = tokens[4].Split('=')[1],
                    Comment = tokens[5].Split('=')[1],
                    HasLaptop = tokens.FirstOrDefault(t => t.Contains("laptop")) != null,
                    HasSmartPhone = tokens.FirstOrDefault(t => t.Contains("smart")) != null,
                    HasMobilePhone = tokens.FirstOrDefault(t => t.Contains("mobile")) != null,
                    HasCar = tokens.FirstOrDefault(t => t.Contains("car")) != null,
                    HasBike = tokens.FirstOrDefault(t => t.Contains("bike")) != null
                };

                Console.WriteLine("<div>{0}</div>", result.ToString());

                File.AppendAllText("survey-results.csv", $"{result.ToString()}{Environment.NewLine}");
            }

            Console.WriteLine(@"
                </body>
            </html>");
        }

        static void GetForm()
        {
            Console.WriteLine(@"
                    <main>
                        <div class=""container body-content span=8 offset=2"">
                            <form class=""form-horizontal"" action=""Survey.exe"" method=""POST"">
                                <fieldset>
                                    <legend>Survey</legend>

                                    <div class=""form-group row"">
                                        <label class=""col-sm-4 control-label"">First Name</label>
                                        <div class=""col-sm-8"">
                                            <input type=""text"" name=""firstName"" class=""form-control"" id=""firstName"" placeholder=""First Name"">
                                        </div>
                                    </div>

                                    <div class=""form-group row"">
                                        <label class=""col-sm-4 control-label"">Last Name</label>
                                        <div class=""col-sm-8"">
                                            <input type=""text"" name=""lastName"" class=""form-control"" id=""lastName"" placeholder=""Last Name"">
                                        </div>
                                    </div>

                                    <div class=""form-group row"">
                                        <label class=""col-sm-4 control-label"">Birthdate</label>
                                        <div class=""col-sm-8"">
                                            <input type=""date"" name=""birthDate"" class=""form-control"" id=""birthDate"">
                                        </div>
                                    </div>

                                    <div class=""form-group row"">
                                        <label class=""col-sm-4 control-label"">Gender</label>
                                        <div class=""radio col-sm-8"">
                                            <div class=""radio"">
                                              <label><input type=""radio"" name=""gender""  checked=""checked"" value=""male"">Male</label>
                                            </div>
                                            <div class=""radio"">
                                              <label><input type=""radio"" name=""gender"" value=""female"">Female</label>
                                            </div>
                                            <div class=""radio"">
                                              <label><input type=""radio"" name=""gender"" value=""not answered"">Do not want to answer</label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class=""form-group row"">
                                        <label class=""col-sm-4 control-label"" for=""sel1"">Status</label>
                                        <select class=""form-control col-sm-8"" id=""sel1"" name=""status"">
                                            <option>Student</option>
                                            <option>Part-time Employee</option>
                                            <option>Full-time Employee</option>
                                            <option>Unemployed</option>
                                            <option>Do not want to answer</option>
                                        </select>
                                    </div>

                                    <div class=""form-group row"">
                                      <label for=""comments"" class=""col-sm-4 control-label"">Recommendations</label>
                                        <textarea class=""col-sm-8 form-control"" rows=""5"" id=""comments"" name=""comments""></textarea>
                                    </div>

                                    <div class=""small text-muted col-sm-8 col-sm-offset-4 row"">Please tell us what are your recommendations for us and how to improve our product.
                                    </div>

                                    <div class=""form-group row"">
                                        <label class=""col-sm-4 control-label"">You own:</label>
                                        <div class=""col-sm-8"">
                                            <label class=""checkbox-inline""><input type=""checkbox"" name=""laptop"">Laptop</label>
                                            <label class=""checkbox-inline""><input type=""checkbox"" name=""phone"">Smart Phone</label>
                                            <label class=""checkbox-inline""><input type=""checkbox"" name=""mobile"">Mobile Phone</label>
                                            <label class=""checkbox-inline""><input type=""checkbox"" name=""car"">Car</label>
                                            <label class=""checkbox-inline""><input type=""checkbox"" name=""bike"">Bike</label>
                                        </div>
                                    </div>

                                    <div class=""form-group"">
                                        <div class=""col-sm-4 col-sm-offset-4"">
                                            <button type=""reset"" class=""btn"">Reset all fields</button>
                                            <button type=""submit"" class=""btn btn-primary"">Send Survey</button>
                                        </div>
                                    </div>

                                </fieldset>
                            </form>
                        </div>
                    </main>");
        }
    }
}
