﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Teage.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TeageEntities : DbContext
    {
        public TeageEntities()
            : base("name=TeageEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Captcha> Captcha { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Express> Express { get; set; }
        public DbSet<ExpressCal> ExpressCal { get; set; }
        public DbSet<ExpressType> ExpressType { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<UserInfoRole> UserInfoRole { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersAddress> UsersAddress { get; set; }
        public DbSet<UserStates> UserStates { get; set; }
    }
}
