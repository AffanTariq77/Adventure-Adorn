using Blaze.Common.Core;
using System;
using System.Collections.Generic;

namespace Blaze.Model.ViewModels
{
    public class UserView : EntityView<Guid>
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName {  get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
