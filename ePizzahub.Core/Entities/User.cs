﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ePizzahub.Core.Entities
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            PaymentDetails = new HashSet<PaymentDetail>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}