using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtGallery.DBaccess
{
    public class StoredProcedures
    {
        public static string SignIn = "SignIn";
        public static string SignInAdmin = "SignInAdmin";
        public static string GetAdminID = "GetAdminID";
        public static string SignUp = "SignUp";
        public static string InsertExpert = "ADD_EXPERT";
        public static string InsertArtist = "InsertArtist";
        public static string InsertQualifications = "AddQualifications";
        public static string EmailAvailable = "EmailAvailable";
        public static string GetArtist = "GetArtist";
        public static string GetArtworkInfo = "GetArtworkInfo";
        public static string GetArtworks = "GetArtworks";
        public static string GetEmail = "GetEmail";
        public static string GetExpert = "GetExpert";
        public static string GetOrderById = "GetOrderById";
        public static string GetReportById = "GetReportById";
        public static string GetSortedOrders = "GetSortedOrders";
        public static string GetSortedReports = "GetSortedReports";
        public static string ProfileImagePath = "ProfileImagePath";
        public static string UserName_BY_EMAIL = "UserName_BY_EMAIL";
        public static string UserNameAvailable = "UserNameAvailable";
        public static string GetSortedProposedArtworks = "GetSortedProposedArtworks";
        public static string GetProposedArtworksByArtist = "GetProposedArtworksByArtist";
        public static string IsArtist = "IsArtist";
        public static string IsExpert = "IsExpert";
        public static string GetPassword = "GetPassword";
        public static string UpdatePassword = "UpdatePassword";
       
      

    }
}
