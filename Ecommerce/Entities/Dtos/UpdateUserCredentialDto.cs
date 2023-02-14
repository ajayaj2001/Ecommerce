using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Entities.Dtos
{
    public class UpdateUserCredentialDto
    {
        ///<summary>
        ///password user
        ///</summary>
        [Required]
        public string Password { get; set; }

    }
}
