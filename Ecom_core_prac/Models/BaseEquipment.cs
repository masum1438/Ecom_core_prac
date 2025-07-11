﻿// Models/BaseEquipment.cs
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Ecom_core_prac.Models
{
    [Keyless]
    public class BaseEquipment
    {
        
        public int CustId { get; set; }
      
        public string CustName { get; set; }
        
        public string ProductDetails { get; set; }
      
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
       
        public string PaymentMethod { get; set; }
        public decimal Price { get; set; }
        //CustId	CustName	Address	PhoneNumber	ProductDetails	Price	OrderDate	PaymentMethod

        public DateTime? OrderDate { get; set; }
       
    }
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BaseEquipment> BaseEquipments { get; set; }
    }
}