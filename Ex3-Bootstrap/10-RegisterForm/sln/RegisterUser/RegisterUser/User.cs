using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _RegisterUser
{
    class User
    {
        public User(string fname, string lname, string email, string emailConfirm, string pass, string passConfirm)
        {
            FirstName = fname;
            LastName = lname;
            Email = email;
            EmailConfirm = emailConfirm;
            PasswordHash = pass;
            PasswordConfirm = passConfirm;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public bool invalidName
        {
            get { return (FirstName.Length < 2 || FirstName.Length > 30) ||
                            (LastName.Length < 2 || FirstName.Length > 30); }
        }

        public string Email { get; }
        public string EmailConfirm { get; }
        public bool invalidEmail
        {
            get
            {
                var pattern = "^[a-zA-Z0-9]+@[a-zA-Z0-9]+.[a-zA-Z0-9]+$";
                Regex reg = new Regex(pattern);
                return reg.IsMatch(Email) == false;
            }
        }
        public bool mismatchEmail
        {
            get
            {
                return Email != EmailConfirm;
            }
        }

        public string PasswordHash { get; }
        public string PasswordConfirm { get; }
        public bool invalidPassword
        {
            get
            {
                string pattern = @"[a-z].*\d|\d.*[a-z]";
                Regex reg = new Regex(pattern);
                return (PasswordHash.Length < 4 || !reg.IsMatch(PasswordHash));
            }
        }
        public bool mismatchedPassowrd
        {
            get
            {
                return PasswordHash != PasswordConfirm;
            }
        }

        public bool formNotCompleted
        {
            get
            {
                return new string[] { FirstName, LastName, Email, EmailConfirm, PasswordHash, PasswordHash }.Any(s => string.IsNullOrEmpty(s));
            }
        }

        public bool invalidForm
        {
            get
            {
                return invalidName || invalidEmail || mismatchEmail || invalidPassword || mismatchedPassowrd || formNotCompleted;
            }
        }

    }
}
