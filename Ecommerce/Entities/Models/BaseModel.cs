using System.ComponentModel.DataAnnotations;
using System;

namespace Customer.Entities.Models
{
    public class BaseModel
    {
        ///<summary>
        /// unique id field 
        ///</summary>
        [Key]
        public Guid Id { get; set; }

        ///<summary>
        /// when created have time & date 
        ///</summary>
        public string CreatedAt { get; set; }

        ///<summary>
        /// when last updated have time & date 
        ///</summary>
        public string UpdatedAt { get; set; }

        ///<summary>
        /// who created have userId 
        ///</summary>
        public Guid CreatedBy { get; set; }

        ///<summary>
        /// who updated have userId 
        ///</summary>
        public Guid UpdatedBy { get; set; }

        ///<summary>
        /// Is active 
        ///</summary>
        public Boolean IsActive { get; set; }=true;


    }
}
