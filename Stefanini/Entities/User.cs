using System;

namespace Stefanini.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public bool Administrator { get; set; }
    }
}