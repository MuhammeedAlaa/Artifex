using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Web;
using ArtGallery.Models;
using ArtGallery.ViewModels;
using db_access;

namespace ArtGallery.DBaccess
{
    public class ArtifexContext: DbContext
    {
        private DBManager db = new DBManager();
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public int SignUp(RegisterViewModel u)
        {
            string query = "insert into [USER] values ('" + u.USER_NAME + "', '" + u.EMAIL + "','" +
                           u.PASSWORD + "', '" + u.FNAME + "', '" + u.MINIT + "', '" + u.LNAME + "', '" +
                           u.PHONE + "', '" + u.PROFILE_PIC + "')";
            return  db.ExecuteNonQuery(query);
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public DataTable SignIn(LoginViewModel u)
        {
            string query = "SELECT*" +
                "FROM[dbo].[USER]" +
                "WHERE EMAIL = '" + u.email +
                "' AND PASSWORD = '" + u.password + "';";
            return db.ExecuteReader(query);
        }

        public bool UserNameAvailable(string Username)
        {
            string query = "select count(*) from [USER] where USER_NAME ='" + Username + "'";
            return (int)db.ExecuteScalar(query) == 0;
        }
<<<<<<< HEAD
        public string ProfileImagePath (string Email){

            string query = "select PROFILE_PIC from [USER] where EMAIL ='" + Email + "'";
            return (string)db.ExecuteScalar(query);
            

=======

        public List<Order> GetSortedOrders(string criteria, bool asc)
        {
            string orderDirection;
            if (asc)
                orderDirection = "ASC";
            else
                orderDirection = "DESC";

            string query = "SELECT * FROM [ORDER] ORDER BY " + criteria +" "+ orderDirection;
            return ConvertDataTable<Order>(db.ExecuteReader(query));
>>>>>>> 05e5063f4d42d2c7d5315db42a5ebbd65e44e42c
        }
    }

    
    

}

