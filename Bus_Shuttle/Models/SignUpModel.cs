using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DomainModel;
namespace Bus_Shuttle.Models
{
    public class SignUpModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public bool IsManager { get; set; }
    
        public bool IsDriver { get; set; }
    
        public bool IsAuthorizedDriver { get; set; }
    }
}