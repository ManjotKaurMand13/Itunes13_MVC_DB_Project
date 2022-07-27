using System;
using System.Collections.Generic;

namespace Itunes_13.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float Wallet { get; set; }
        
        public User()
        {
            
        }
        
        public User(string name, float wallet)
        {
            Name = name;
            Wallet = wallet;
        }
    }
}
