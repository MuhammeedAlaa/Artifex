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
using PagedList;

namespace ArtGallery.DBaccess
{
    public class ArtifexContext : DbContext
    {
        private DBManager db = new DBManager();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public int SignUp(RegisterViewModel u)
        {
            string StoredProcedureName = StoredProcedures.SignUp;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("USER_NAME", u.USER_NAME);
            Parameters.Add("EMAIL", u.EMAIL);
            Parameters.Add("@PASSWORD", u.PASSWORD);
            Parameters.Add("@FNAME", u.FNAME);
            Parameters.Add("@MINIT", u.MINIT);
            Parameters.Add("@LNAME", u.LNAME);
            Parameters.Add("@PHONE", u.PHONE);
            Parameters.Add("@PROFILE_PIC", u.PROFILE_PIC);

            return db.ExecuteNonQuery_proc(StoredProcedureName, Parameters);
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            if (dt == null)
                return new List<T>();
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
                    {
                        var value = dr[column.ColumnName];
                        if (value == DBNull.Value)
                            value = null;
                        pro.SetValue(obj, value, null);
                    }
                    else
                        continue;
                }
            }

            return obj;
        }

        public DataTable SignIn(LoginViewModel u)
        {
            string StoredProcedureName = StoredProcedures.SignIn;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@EMAIL", u.email);
            Parameters.Add("@PASSWORD", u.password);
            return db.ExecuteReader_proc(StoredProcedureName, Parameters);
        }

        public bool UserNameAvailable(string Username)
        {
            string StoredProcedureName = StoredProcedures.UserNameAvailable;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Username", Username);
            return (int) db.ExecuteScalar_proc(StoredProcedureName, Parameters) == 0;

        }

        public bool EmailAvailable(string Email)
        {
            string StoredProcedureName = StoredProcedures.EmailAvailable;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", Email);
            return (int) db.ExecuteScalar_proc(StoredProcedureName, Parameters) == 0;
        }

        public string ProfileImagePath(string Email)
        {
            string StoredProcedureName = StoredProcedures.ProfileImagePath;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@email", Email);
            object result = db.ExecuteScalar_proc(StoredProcedureName, Parameters);
            return (result == DBNull.Value) ? string.Empty : result.ToString();
        }


        public void InsertExpert(string Email, string bio, string qul, int? Byear)
        {
            string StoredProcedureName = StoredProcedures.UserName_BY_EMAIL;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", Email);
            string username = (string) db.ExecuteReader_proc(StoredProcedureName, Parameters).Rows[0]["USER_NAME"];

            StoredProcedureName = StoredProcedures.InsertExpert;
            Parameters = new Dictionary<string, object>();
            Parameters.Add("@USER_NAME", username);
            Parameters.Add("@Bio", bio);
            Parameters.Add("@BYEAR", Byear);
            db.ExecuteNonQuery_proc(StoredProcedureName, Parameters);

            StoredProcedureName = StoredProcedures.InsertQualifications;
            Parameters = new Dictionary<string, object>();
            Parameters.Add("@USERNAME", username);
            Parameters.Add("@QUALI", qul);
            db.ExecuteNonQuery_proc(StoredProcedureName, Parameters);

        }

        public void InsertQual(ExpertViewModel e)
        {
            string StoredProcedureName = StoredProcedures.InsertQualifications;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@USERNAME", e.EXPERT_UNAME);
            Parameters.Add("@QUALI", e.QUALIFICATIONS);
            db.ExecuteNonQuery_proc(StoredProcedureName, Parameters);

        }

        public void InsertArtist(string Email, string bio, int? Byear, int START_SALARY, int END_SALARY)
        {
            string StoredProcedureName = StoredProcedures.UserName_BY_EMAIL;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", Email);
            string username = (string) db.ExecuteReader_proc(StoredProcedureName, Parameters).Rows[0]["USER_NAME"];

            StoredProcedureName = StoredProcedures.InsertArtist;
            Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserName", username);
            Parameters.Add("@Bio", bio);
            Parameters.Add("@BrithYear", Byear);
            Parameters.Add("@StrSalary", START_SALARY);
            Parameters.Add("@EndSalary", END_SALARY);
            db.ExecuteNonQuery_proc(StoredProcedureName, Parameters);
        }

        public Artist GetArtist(string Email)
        {
            string StoredProcedureName = StoredProcedures.GetArtist;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", Email);
            DataTable d = db.ExecuteReader_proc(StoredProcedureName, Parameters);

            if (d != null)
                return ConvertDataTable<Artist>(d)[0];
            else
                return null;
        }

        public List<ExpertViewModel> GetExpert(string Email)
        {
            string StoredProcedureName = StoredProcedures.GetExpert;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", Email);
            DataTable d = db.ExecuteReader_proc(StoredProcedureName, Parameters);

            if (d != null)
                return (ConvertDataTable<ExpertViewModel>(d));
            else
                return null;
        }


        public List<Order> GetSortedOrders(string criteria, bool asc)
        {
            string orderDirection;
            if (asc)
                orderDirection = "ASC";
            else
                orderDirection = "DESC";

            string StoredProcedureName = StoredProcedures.GetSortedOrders;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Criteria", criteria);
            Parameters.Add("@asc", orderDirection);
            return ConvertDataTable<Order>(db.ExecuteReader_proc(StoredProcedureName, Parameters));
        }

        public List<Order> GetOrderById(int id)
        {
            string StoredProcedureName = StoredProcedures.GetOrderById;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Id", id);
            return ConvertDataTable<Order>(db.ExecuteReader_proc(StoredProcedureName, Parameters));

        }

        public List<Report> GetSortedReports(string criteria, bool asc)
        {
            string orderDirection;
            if (asc)
                orderDirection = "ASC";
            else
                orderDirection = "DESC";

            string StoredProcedureName = StoredProcedures.GetSortedReports;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Criteria", criteria);
            Parameters.Add("@asc", orderDirection);
            return ConvertDataTable<Report>(db.ExecuteReader_proc(StoredProcedureName, Parameters));
        }

        public bool SolveReport(Report r)
        {
            string query = "UPDATE REPORT SET ADMIN_ID = " + r.ADMIN_ID + "WHERE REPORT_ID = " + r.REPORT_ID;
            return (db.ExecuteNonQuery(query) != 0);
        }

        public string GetUserName(string Email)
        {
            string StoredProcedureName = StoredProcedures.UserName_BY_EMAIL;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", Email);
            return (string) db.ExecuteScalar_proc(StoredProcedureName, Parameters);
        }

        public string GetEmail(string Uname)
        {
            string StoredProcedureName = StoredProcedures.GetEmail;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserName", Uname);
            return (string) db.ExecuteScalar_proc(StoredProcedureName, Parameters);
        }

        public List<Report> GetReportById(int id)
        {
            string StoredProcedureName = StoredProcedures.GetReportById;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Id", id);
            return ConvertDataTable<Report>(db.ExecuteReader_proc(StoredProcedureName, Parameters));
        }

        public void UpdatePassword(string EMAIL, ChangePasswordViewModel p)
        {
            string StoredProcedureName = StoredProcedures.GetPassword;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", EMAIL);
            string pass = (string) db.ExecuteScalar_proc(StoredProcedureName, Parameters);
            if (pass == p.PASSWORD)
            {
                StoredProcedureName = StoredProcedures.UpdatePassword;
                Parameters = new Dictionary<string, object>();
                Parameters.Add("@Email", EMAIL);
                Parameters.Add("@NewPassword", p.NEWPASSWORD);
                db.ExecuteNonQuery_proc(StoredProcedureName, Parameters);
            }

        }

        public List<Artwork> GetSortedProposedArtworks(string criteria, bool asc)
        {
            string orderDirection;
            if (asc)
                orderDirection = "ASC";
            else
                orderDirection = "DESC";

            string StoredProcedureName = StoredProcedures.GetSortedProposedArtworks;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Criteria", criteria);
            Parameters.Add("@asc", orderDirection);
            return ConvertDataTable<Artwork>(db.ExecuteReader_proc(StoredProcedureName, Parameters));
        }

        public List<Artwork> GetProposedArtworksByArtist(string name)
        {
            string StoredProcedureName = StoredProcedures.GetProposedArtworksByArtist;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@NAME", name);
            return ConvertDataTable<Artwork>(db.ExecuteReader_proc(StoredProcedureName, Parameters));
        }

        public bool IsArtist(string email)
        {
            string StoredProcedureName = StoredProcedures.IsArtist;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", email);
            return (int) db.ExecuteScalar_proc(StoredProcedureName, Parameters) != 0;
        }

        public bool IsExpert(string email)
        {
            string StoredProcedureName = StoredProcedures.IsExpert;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", email);
            return (int) db.ExecuteScalar_proc(StoredProcedureName, Parameters) != 0;
        }

        public List<Artwork> GetArtworkInfo2(string title)
        {
            string StoredProcedureName = StoredProcedures.GetArtworkInfo;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Title", title);
            return ConvertDataTable<Artwork>(db.ExecuteReader_proc(StoredProcedureName, Parameters));

        }

        public List<Artwork> GetArtWorkInfo(int code)
        {
            string StoredProcedureName = StoredProcedures.IsExpert;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            string query = "SELECT * FROM ARTWORK WHERE AW_CODE=" + code + ";";
            return ConvertDataTable<Artwork>(db.ExecuteReader(query));

        }

        public List<Artwork> GetArtworks()
        {
            string StoredProcedureName = StoredProcedures.GetArtworks;
            return ConvertDataTable<Artwork>(db.ExecuteReader_proc(StoredProcedureName, null));
        }

        public bool AddAdmin(Admin a)
        {
            string StoredProcedureName = StoredProcedures.AddAdmin;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", a.EMAIL);
            Parameters.Add("@PASSWORD", a.PASSWORD);
            Parameters.Add("@SALARY", a.SALARY);
            return db.ExecuteNonQuery_proc(StoredProcedureName, Parameters) != 0;
        }

        public void UpdateEvent(string title)
        {
<<<<<<< HEAD
            //string query = "UPDATE EVENT SET TICKETS_NUM=TICKETS_NUM - 1 WHERE TITLE = '"+ title+"'";
            //db.ExecuteNonQuery(query);
            string StoredProcedureName = StoredProcedures.SellTicket;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@title", title);
            db.ExecuteNonQuery_proc(StoredProcedureName,Parameters);
           
=======
            string query = "UPDATE EVENT SET TICKETS_NUM=TICKETS_NUM - 1 WHERE TITLE = '" + title + "'";
            db.ExecuteNonQuery(query);

>>>>>>> 6bfd8f19b76160cf5394ed21169861da14d932bf
        }

        public List<Event> GetEventInfo(string title)
        {
            string storedProcedureName = StoredProcedures.GetEventInfo;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@title", title);
            return ConvertDataTable<Event>(db.ExecuteReader_proc(storedProcedureName,Parameters));

        }

        public bool EditEvent(Event e, string oldtitle)
        {
<<<<<<< HEAD
            string storedProcedureName = StoredProcedures.EditEvent;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            
            Parameters.Add("@title", e.TITLE);
            Parameters.Add("@ADMIN_ID", e.ADMIN_ID);
            Parameters.Add("@IMAGE", e.IMAGE);
            Parameters.Add("@TICKET_PRICE", e.TICKET_PRICE);
            Parameters.Add("@EVENTDATE", e.EVENTDATE);
            Parameters.Add("@LOCATION", e.LOCATION);
            Parameters.Add("@TICKETS_NUM", e.TICKETS_NUM);
            Parameters.Add("@INFO", e.INFO);
            Parameters.Add("@oldtitle", oldtitle);

            return (db.ExecuteNonQuery_proc(storedProcedureName, Parameters) != null);
=======

            string query1 = "UPDATE EVENT SET TITLE = '" + e.TITLE + "', ADMIN_ID = " + e.ADMIN_ID + ", IMAGE = '" +
                            e.IMAGE + "' Where TITLE = '" + oldtitle + "'";
            string query2 = "update event set TICKET_PRICE = " + e.TICKET_PRICE + ", EVENTDATE = '" + e.EVENTDATE +
                            "', LOCATION = '" + e.LOCATION + "', TICKETS_NUM = " + e.TICKETS_NUM + ", INFO = '" +
                            e.INFO + "' Where TITLE = '" + oldtitle + "'";
            db.ExecuteNonQuery(query1);
            return (db.ExecuteNonQuery(query2) != null);
>>>>>>> 6bfd8f19b76160cf5394ed21169861da14d932bf

        }

        public List<Event> GetEvents()
        {
<<<<<<< HEAD
            string storedProcedureName = StoredProcedures.GetEvents;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            
            Parameters.Add("@NowDate", DateTime.Now.Date);
            return ConvertDataTable<Event>(db.ExecuteReader_proc(storedProcedureName, Parameters));
=======
            string query = "SELECT * FROM EVENT WHERE EVENTDATE >='" + DateTime.Now.Date + "'";
            return ConvertDataTable<Event>(db.ExecuteReader(query));
>>>>>>> 6bfd8f19b76160cf5394ed21169861da14d932bf
        }

        public List<Artwork> GetArtWorks(Artist A)
        {
            string storedProcedureName = StoredProcedures.GetArtWorksByArtist;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            
            Parameters.Add("@Artist_UName", A.ARTIST_UNAME);

            return ConvertDataTable<Artwork>(db.ExecuteReader_proc(storedProcedureName, Parameters));
        }

        public void InsertArtwork(Artwork a)
        {
<<<<<<< HEAD
            string storedProcedureName = StoredProcedures.InsertArtwork;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            //Defaults: ADMIN_ID = NULL, ACCEPTED = 0(INITIALLY NOT ACCEPTED), STATUS = 1(INTIAILLY AVAILABLE)
            Parameters.Add("@CATEGORY_NAME", a.CATEGORY_NAME);
            Parameters.Add("@ARTIST_UNAME", a.ARTIST_UNAME);
            Parameters.Add("@TITLE", a.TITLE);
            Parameters.Add("@PRIVACY", a.PRIVACY);
            Parameters.Add("@DESCRIPTION", a.DESCRIPTION);
            Parameters.Add("@WIDTH", a.WIDTH);
            Parameters.Add("@HEIGHT", a.HEIGHT);
            Parameters.Add("@DEPTH", a.DEPTH);
            Parameters.Add("@PRICE", a.PRICE);
            Parameters.Add("@MATERIAL", a.MATERIAL);
            Parameters.Add("@MEDIUM", a.MEDIUM);
            Parameters.Add("@SUBJECT",a.SUBJECT);
            Parameters.Add("@PHOTO", a.PHOTO);
            Parameters.Add("@YEAR", a.YEAR);

            db.ExecuteNonQuery_proc(storedProcedureName, Parameters);
=======
            //category is a foreign key get it from the user with drop down list
            string query = "INSERT INTO ARTWORK VALUES('" + a.CATEGORY_NAME + "','" + a.ARTIST_UNAME + "'," + "null"
                           + ",'" + a.TITLE + "',0,'" + a.PRIVACY + "',1,'" + a.DESCRIPTION + "'," + a.WIDTH + "," +
                           a.HEIGHT
                           + "," + a.DEPTH + "," + a.PRICE + ",'" + a.MATERIAL + "','" + a.MEDIUM + "','" + a.SUBJECT +
                           "','" + a.PHOTO + "'," + a.YEAR + ")";
            db.ExecuteNonQuery(query);
>>>>>>> 6bfd8f19b76160cf5394ed21169861da14d932bf
        }

        public DataTable GetCategories()
        {
            string storedProcedureName = StoredProcedures.GetCategories;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            return db.ExecuteReader_proc(storedProcedureName, Parameters);

        }

        public List<Survey> GetRequestedSurvey(ExpertViewModel e)
        {
            string query = "SELECT * FROM SURVEY AS S WHERE EXPERT_UNAME='" + e.EXPERT_UNAME +
                           "' AND S.SURVEY_ID NOT IN(SELECT R.SURVEY_ID FROM RECOMMEND AS R)";
            return ConvertDataTable<Survey>(db.ExecuteReader(query));

        }

<<<<<<< HEAD
=======
        public List<Recommend> GetRecommended(int surveyId)
        {
            string query = "SELECT * FROM RECOMMEND WHERE SURVEY_ID = " + surveyId;
            return ConvertDataTable<Recommend>(db.ExecuteReader(query));
        }
>>>>>>> 6bfd8f19b76160cf5394ed21169861da14d932bf

        public Survey GetSurveyInfo(int code)
        {
            string query = "SELECT * FROM SURVEY WHERE SURVEY_ID='" + code + "'";
            return ConvertDataTable<Survey>(db.ExecuteReader(query))[0];
            //string storedProcedureName = StoredProcedures.GetSurveyInfo;
            //Dictionary<string, object> Parameters = new Dictionary<string, object>();

            //Parameters.Add("@code", code);

            //return ConvertDataTable<Survey>(db.ExecuteReader_proc(storedProcedureName, Parameters))[0];
        }


        public void InsertSurveyRequest(Survey s)
        {
<<<<<<< HEAD
            string storedProcedureName = StoredProcedures.InsertSurveyRequest;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            //DEFAULTS: RATING = NULL INITIALLY
            Parameters.Add("@EXPERT_UNAME", s.EXPERT_UNAME);
            Parameters.Add("@USER_NAME", s.USER_NAME);
            Parameters.Add("@BUDGET", s.BUDGET);
            Parameters.Add("@MORE_INFO", s.MORE_INFO);

            db.ExecuteNonQuery_proc(storedProcedureName, Parameters);
=======
            string query = "INSERT INTO SURVEY VALUES('" + s.EXPERT_UNAME + "','" + s.USER_NAME + "'," + s.BUDGET +
                           ",'" + s.MORE_INFO + "',null)";
            db.ExecuteNonQuery(query);
>>>>>>> 6bfd8f19b76160cf5394ed21169861da14d932bf
        }

        public List<Survey> GetUserSurveys(string username)
        {
            string query = "SELECT * FROM SURVEY WHERE USER_NAME = '" + username +"'";
            return ConvertDataTable<Survey>(db.ExecuteReader(query));
        }

    public void InsertCustomOrder(CustomOrderUserViewModel c)
        {
            //ArtistsBelowBudget
            string query = "SELECT ARTIST_UNAME FROM ARTIST WHERE START_SALARY <=" + c.Budget;
             List<Artist> l = ConvertDataTable<Artist>(db.ExecuteReader(query));
            if (l.Count == 0)
                return;
            Random random = new Random();
            int artist = random.Next(0, l.Count);
            string uname = l[artist].ARTIST_UNAME;
            //Create an artwork
            query = "INSERT INTO ARTWORK VALUES('" + c.Category + "','" + uname + "'," + "null"
                + ",'" + c.TITLE + "',0,1,1,'" + c.DESCRIPTION + "'," + c.WIDTH + "," + c.HEIGHT
                + "," + c.DEPTH + "," + c.Budget + ",'" + c.MATERIAL + "','" + c.MEDIUM + "','" + "null" + "','" + "null" + "'," + "null" + ")";

            if (db.ExecuteNonQuery(query) == 0)
                return;
            query = "INSERT INTO dbo.[ORDER] VALUES(1,'" + DateTime.Now.ToString("mm/dd/yyyy")
            +"','" + c.Deadline.ToString().Substring(0,9) +"');";

            db.ExecuteNonQuery(query);
        }
        public Artwork GetArtworkWithCode(int code)
        {
            string storedProcedureName = StoredProcedures.GetArtworkWithCode;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            Parameters.Add("@code", code);

            return ConvertDataTable<Artwork>(db.ExecuteReader_proc(storedProcedureName, Parameters))[0];

        }

        public void InsertBillingInfo(BillingInfo b, string uname)
        {
            string query = "INSERT INTO BILLING_INFO VALUES('" + b.CARD_NUM + "','" + uname + "','"
                + b.STREET + "','" + b.CITY + "','" + b.CARD_HOLDER_NAME + "'," + b.CVV + ",'" + b.EXPIRY_DATE.ToString().Substring(0, 9) + "');";
            db.ExecuteNonQuery(query);
            //string storedProcedureName = StoredProcedures.InsertBillingInfo;
            //Dictionary<string, object> Parameters = new Dictionary<string, object>();

            //Parameters.Add("@CARD_NUM", b.CARD_NUM);
            //Parameters.Add("@uname", uname);
            //Parameters.Add("@STREET", b.STREET);
            //Parameters.Add("@CITY", b.CITY);
            //Parameters.Add("@CARD_HOLDER_NAME", b.CARD_HOLDER_NAME);
            //Parameters.Add("@CVV", b.CVV);
            //Parameters.Add("@EXPIRY_DATE", b.EXPIRY_DATE.ToString().Substring(0, 9));

            //db.ExecuteNonQuery_proc(storedProcedureName, Parameters);
        }

        public bool ApproveArtwork(int adminId, int code, int state)
        {
            string storedProcedureName = StoredProcedures.ApproveArtwork;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            Parameters.Add("@state", state);
            Parameters.Add("@adminId", adminId);
            Parameters.Add("@code", code);

            return db.ExecuteNonQuery_proc(storedProcedureName, Parameters) != 0;
        }


        public DataTable adminsignin(LoginViewModel u) {
            string StoredProcedureName = StoredProcedures.SignInAdmin;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@EMAIL", u.email);
            Parameters.Add("@PASSWORD", u.password);
            return db.ExecuteReader_proc(StoredProcedureName, Parameters);
        }
        public string adminid(LoginViewModel u)
        {
            string StoredProcedureName = StoredProcedures.GetAdminID;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@EMAIL", u.email);
            Parameters.Add("@PASSWORD", u.password);
            string id = Convert.ToString(db.ExecuteReader_proc(StoredProcedureName, Parameters).Rows[0]["Admin_Id"]);
            return id;
        }
        public bool CreateEvent(Event e)
        {
            string storedProcedureName = StoredProcedures.CreateEvent;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            Parameters.Add("@TITLE", e.TITLE);
            Parameters.Add("@ADMIN_ID", e.ADMIN_ID);
            Parameters.Add("@IMAGE", e.IMAGE);
            Parameters.Add("@TICKET_PRICE", e.TICKET_PRICE);
            Parameters.Add("@EVENTDATE", e.EVENTDATE);
            Parameters.Add("@LOCATION", e.LOCATION);
            Parameters.Add("@TICKETS_NUM", e.TICKETS_NUM);
            Parameters.Add("@INFO", e.INFO);

            return db.ExecuteNonQuery_proc(storedProcedureName, Parameters) != 0;
        }

        public List<Artist> GetArtists()
        {
            string storedProcedureName = StoredProcedures.GetArtists;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            var lst =  ConvertDataTable<Artist>(db.ExecuteReader_proc(storedProcedureName, Parameters));
            foreach (var obj in lst)
            {
                obj.Selected = "";
            }
            return lst;
        }


        public List<Artwork> GetArtworksforrecommanded()
        {

            string query = "select * from ARTWORK WHERE PRIVACY=0";


            var lst = ConvertDataTable<Artwork>(db.ExecuteReader(query));
            foreach (var obj in lst)
            {
                obj.Selected = "";
            }
            return lst;
        }

        public bool InviteArtist(string EventTitle, string[] artists)
        {

            foreach (var artist in artists)
            {
                string query = "INSERT INTO INVITED VALUES('" + EventTitle + "' ,'" + artist + "')";
                if (db.ExecuteNonQuery(query) == 0)
                    return false;
            }

            return true;
        }

        public List<ShippingCompany> GetCompanies()
        {
            
            string storedProcedureName = StoredProcedures.GetCompanies;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            return ConvertDataTable<ShippingCompany>(db.ExecuteReader_proc(storedProcedureName, Parameters));
        }

        public bool Addcompany(ShippingCompany C)
        {
            string storedProcedureName = StoredProcedures.Addcompany;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            Parameters.Add("@NAME", C.NAME);
            Parameters.Add("@EMAIL", C.EMAIL);
            Parameters.Add("@PHONE", C.PHONE);
            Parameters.Add("@SHIPPING_FEES", C.SHIPPING_FEES);

            return (db.ExecuteNonQuery_proc(storedProcedureName, Parameters) != 0);


        }

        public bool deleteCompany(string C)
        {
            string storedProcedureName = StoredProcedures.deleteCompany;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            Parameters.Add("@Shipping_name", C);

            return (db.ExecuteNonQuery_proc(storedProcedureName, Parameters) != 0);
        }
        public OrderInfo GetOrderInfo(int id)
        {
            string storedProcedureName = StoredProcedures.GetOrderInfo;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            Parameters.Add("@ORDER_ID", id);

            return ConvertDataTable<OrderInfo>(db.ExecuteReader_proc(storedProcedureName, Parameters))[0];
        }

        public bool AssignOrder(OrderInfo o)
        {
            string query = "UPDATE ORDER_INFO SET ADMIN_ID = " + o.ADMIN_ID + ", SHIPPING_NAME = '" + o.SHIPPING_NAME +
                           "' WHERE ORDER_ID = "+ o.ORDER_ID;
            if (db.ExecuteNonQuery(query) == 0)
                return false;
            query = "UPDATE [ORDER] SET STATUS = 1, DELIVERY_DATE = '" + DateTime.Now.AddDays(10).ToString("MM/dd/yyyy") +
                    "' WHERE ORDER_ID = " + o.ORDER_ID;
            return (db.ExecuteNonQuery(query) != 0);
        }
        public List<Artwork> GetArtworkInfo(string title)
        {
            //string query = "SELECT * FROM Artwork WHERE TITLE='" + title + "';";
            //return ConvertDataTable<Artwork>(db.ExecuteReader(query));
            string storedProcedureName = StoredProcedures.GetArtworkInfo;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            Parameters.Add("@Title", title);

            return ConvertDataTable<Artwork>(db.ExecuteReader_proc(storedProcedureName, Parameters));

        }
        public void rateArtwork(int rating,int code,string uname) {
            string StoredProcedureName = StoredProcedures.InsertRating;
            Dictionary<string, object> Parameters2 = new Dictionary<string, object>();
            Parameters2.Add("@uname", uname);
            Parameters2.Add("@AWCODE", code);
            Parameters2.Add("@rating", rating);
            db.ExecuteNonQuery_proc(StoredProcedureName, Parameters2);
        }
        public int GetRate() 
        {
            string StoredProcedureName = StoredProcedures.OldRating;
            DataTable oldrate = db.ExecuteReader_proc(StoredProcedureName, null);
            int rating = (int)oldrate.Rows[0]["SUM"];
            rating /= (int)oldrate.Rows[0]["COUNT"];
            return rating;
        }
        public void addFav(int code, string uname,int rating)
        {
            rateArtwork(rating, code, uname);
            string query = "INSERT INTO FAV_AW VALUES(" + code + ",'" + uname  + "');";
            db.ExecuteNonQuery(query);
        }
        public List<Artwork> GetFavourite(string email)
        {
            string storedProcedureName = StoredProcedures.GetFavourite;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            Parameters.Add("@EMAIL", email);

            return ConvertDataTable<Artwork>(db.ExecuteReader_proc(storedProcedureName, Parameters));
        }

        public List<Expert> GetExperts()
        {
            string storedProcedureName = StoredProcedures.GetExperts;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            return ConvertDataTable<Expert>(db.ExecuteReader_proc(storedProcedureName, Parameters));
        }

        public List<string> GetExpertMails()
        {
            //string query = "SELECT EMAIL FROM [USER] JOIN EXPERT ON EXPERT_UNAME = USER_NAME";
            //return db.ExecuteReader(query).AsEnumerable().Select(x => x[0].ToString()).ToList();
            string storedProcedureName = StoredProcedures.GetExpertMails;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();

            return db.ExecuteReader_proc(storedProcedureName, Parameters).AsEnumerable().Select(x => x[0].ToString()).ToList();
        }

        public bool RecommendAW(int awcode, int surveyid)
        {
            string query = "INSERT INTO RECOMMEND VALUES (" + surveyid + ", " + awcode + ")";
            return db.ExecuteNonQuery(query) != 0;
        }
        

    }
}

