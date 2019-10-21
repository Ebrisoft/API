using System.Collections.Generic;

namespace API.Unauthorized.Response
{
    public class SignIn
    {
        //  Properties
        //  ==========

        public IEnumerable<string> Roles { get; }

        //  Constructors
        //  ============

        public SignIn(IEnumerable<string> roles)
        {
            Roles = roles;
        }
    }
}