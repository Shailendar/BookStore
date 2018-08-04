using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace BookStore.Models
{
    interface ICustomPrincipal : IPrincipal
    {
        int Id { get; set; }
        string Email { get; set; }
        string Role { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int CustomerId { get; set; }
    }

    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        //public bool IsInRole(string role)
        //{
        //    return Identity != null && Identity.IsAuthenticated &&
        //       !string.IsNullOrWhiteSpace(role) && Roles.IsUserInRole(Identity.Name, role);
        //}

        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CustomerId { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CustomerId { get; set; }
    }
}
