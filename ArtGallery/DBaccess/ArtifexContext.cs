using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using ArtGallery.Models;
using BBMS.db_access;

namespace ArtGallery.DBaccess
{
    public class ArtifexContext: DbContext
    {
        private DBManager db = new DBManager();
        public DbSet<User> Users { set; get; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void SignUp(User u)
        {
            string query = "insert into [USER] values ('"+ u.USER_NAME +"', '"+ u.EMAIL +"','"+ 
                           u.PASSWORD +"', '"+ u.FNAME +"', '"+ u.MINIT +"', '"+ u.LNAME +"', '"+ 
                           u.PHONE +"', '"+ u.PROFILE_PIC +"')";
            int d =  db.ExecuteNonQuery(query);
        }
    }

    
    

}

