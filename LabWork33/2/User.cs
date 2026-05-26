using System;

namespace UserDataGridApp
{
    public class User
    {
        public string Address { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Category { get; set; }
        public bool IsArchived { get; set; }

        public string HiddenPassword => new string('*', Password?.Length ?? 0);
    }
}