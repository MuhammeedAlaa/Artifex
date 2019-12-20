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
            string query = "select count(*) from [USER] where USER_NAME ='" + Username + "'";
            return (int)db.ExecuteScalar(query) == 0;
        }

        public bool EmailAvailable(string Email)
        {
            string query = "select count(*) from [USER] where EMAIL ='" + Email + "'";
            return (int)db.ExecuteScalar(query) == 0;
        }

        public string ProfileImagePath(string Email)
        {

            string query = "select PROFILE_PIC from [USER] where EMAIL ='" + Email + "'";
            return (string) db.ExecuteScalar(query);

        }


        public void InsertExpert(string Email, string bio, string qul, int? Byear ) 
        {
            string query = "SELECT USER_NAME FROM[dbo].[USER] WHERE EMAIL = '" + Email+"';";
            string username = (string)db.ExecuteReader(query).Rows[0]["USER_NAME"];
            query = "INSERT INTO [dbo].[EXPERT]([EXPERT_UNAME],[BIO],[BYEAR])VALUES" +
                "('" + username + "','" + bio + "'," + Byear + ");";
            db.ExecuteNonQuery(query);
            query = "INSERT INTO EXP_QUALIFICATIONS VALUES('" + username+ "','" + qul + "');";
            db.ExecuteNonQuery(query);
        }
        public void InsertQual(ExpertViewModel e)
        {
            string query = "INSERT INTO EXP_QUALIFICATIONS VALUES('" + e.EXPERT_UNAME + "','" + e.QUALIFICATIONS + "');";
            db.ExecuteNonQuery(query);
        }

        public void InsertArtist(string Email, string bio, int? Byear, int START_SALARY, int END_SALARY)
        {
            string query = "SELECT USER_NAME FROM[dbo].[USER] WHERE EMAIL = '" + Email + "';";
            string username = (string)db.ExecuteReader(query).Rows[0]["USER_NAME"];
            query = "INSERT INTO [dbo].[ARTIST]([ARTIST_UNAME],[BIO],[BYEAR],[START_SALARY],[END_SALARY])" +
                "VALUES('" + username + "','" + bio + "','" + Byear + "'," + START_SALARY + "," + END_SALARY + ");";
            db.ExecuteNonQuery(query);
        }
        public Artist GetArtist(string Email) 
        {
            string query = "SELECT A.* FROM ARTIST A JOIN [dbo].[USER] U ON U.USER_NAME = A.ARTIST_UNAME WHERE  U.EMAIL ='" + Email + "';";
            DataTable d = db.ExecuteReader(query);
            if (d != null)
                return ConvertDataTable<Artist>(d)[0];
            else
                return null;
        }

        public List<ExpertViewModel> GetExpert(string Email)
        {

            string query = "SELECT E.*,ES.QUALIFICATIONS FROM EXPERT E JOIN[dbo].[USER] U ON U.USER_NAME = E.EXPERT_UNAME join EXP_QUALIFICATIONS ES on E.EXPERT_UNAME = ES.EXPERT_UNAME  WHERE U.EMAIL = '" + Email + "'; ";
            DataTable d = db.ExecuteReader(query);
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

            string query = "SELECT * FROM [ORDER] ORDER BY " + criteria + " " + orderDirection;
            return ConvertDataTable<Order>(db.ExecuteReader(query));

        }

        public List<Order> GetOrderById(int id)
        {
            string query = "SELECT * FROM [ORDER] WHERE ORDER_ID = " + id;
            return ConvertDataTable<Order>(db.ExecuteReader(query));

        }
        public List<Report> GetSortedReports(string criteria, bool asc)
        {
            string orderDirection;
            if (asc)
                orderDirection = "ASC";
            else
                orderDirection = "DESC";

            string query = "SELECT * FROM REPORT ORDER BY " + criteria + " " + orderDirection;
            return ConvertDataTable<Report>(db.ExecuteReader(query));

        }

        public bool SolveReport(Report r)
        {
            string query = "UPDATE REPORT SET ADMIN_ID = " + r.ADMIN_ID + "WHERE REPORT_ID = " + r.REPORT_ID;
            return (db.ExecuteNonQuery(query) != 0);
        }
        public string GetUserName(string Email) 
        {
            string query = "SELECT USER_NAME FROM [dbo].[USER] WHERE EMAIL ='" + Email + "';";
            return (string)db.ExecuteScalar(query);
        }

        public string GetEmail(string Uname)
        {
            string query = "SELECT EMAIL FROM [dbo].[USER] WHERE USER_NAME ='" + Uname + "';";
            return (string)db.ExecuteScalar(query);
        }

        public List<Report> GetReportById(int id)
        {
            string query = "SELECT * FROM REPORT WHERE REPORT_ID = " + id;
            return ConvertDataTable<Report>(db.ExecuteReader(query));
        }

        public void UpdatePassword(string EMAIL, ChangePasswordViewModel p)
        {
            string query = "SELECT [dbo].[USER].PASSWORD FROM [dbo].[USER] WHERE EMAIL='" + EMAIL + "';";
            string pass = (string) db.ExecuteScalar(query);
            if (pass == p.PASSWORD)
            {
                query = "UPDATE [dbo].[USER] SET [PASSWORD] ='" + p.NEWPASSWORD + "' WHERE EMAIL='" + EMAIL + "';";
                db.ExecuteNonQuery(query);
            }

        }

        public List<Artwork> GetSortedProposedArtworks(string criteria, bool asc)
        {
            string orderDirection;
            if (asc)
                orderDirection = "ASC";
            else
                orderDirection = "DESC";

            string query = "SELECT * FROM Artwork WHERE ADMIN_ID IS NULL ORDER BY " + criteria + " " + orderDirection;
            return ConvertDataTable<Artwork>(db.ExecuteReader(query));

        }

        public List<Artwork> GetProposedArtworksByArtist(string name)
        {
            string query = "SELECT * FROM Artwork WHERE ADMIN_ID IS NULL AND ARTIST_UNAME LIKE '%" + name + "%'";
            return ConvertDataTable<Artwork>(db.ExecuteReader(query));
        }
        public bool IsArtist(string email)
        {
            string query = "SELECT COUNT(*) FROM ARTIST JOIN [dbo].[USER] ON ARTIST_UNAME = USER_NAME WHERE EMAIL ='" + email + "';";
            return (int)db.ExecuteScalar(query) != 0;
        }
   
        public bool IsExpert(string email) 
        {
            string query = "SELECT COUNT(*) FROM Expert JOIN [dbo].[USER] ON EXPERT_UNAME = USER_NAME WHERE EMAIL ='" + email + "';";
            return (int)db.ExecuteScalar(query) != 0;
        }
        public List<Event> GetEventInfo(string title)
        {
            string query = "SELECT * FROM EVENT WHERE TITLE='" + title + "';";
            return ConvertDataTable<Event>(db.ExecuteReader(query));
        
        }
        public List<Artwork> GetArtWorkInfo(int code)
        {
            string query = "SELECT * FROM ARTWORK WHERE AW_CODE=" + code + ";";
            return ConvertDataTable<Artwork>(db.ExecuteReader(query));

        }

        public void UpdateEvent(string title)
        {
            string query = "UPDATE EVENT SET TICKETS_NUM=TICKETS_NUM - 1 WHERE TITLE = '"+ title+"'";
           db.ExecuteNonQuery(query);

        }

        public bool EditEvent(Event e, string oldtitle)
        {
            
            string query1 = "UPDATE EVENT SET TITLE = '" + e.TITLE + "', ADMIN_ID = " + e.ADMIN_ID + ", IMAGE = '" + e.IMAGE + "' Where TITLE = '" + oldtitle+"'";
            string query2 = "update event set TICKET_PRICE = " + e.TICKET_PRICE + ", EVENTDATE = '" + e.EVENTDATE + 
                            "', LOCATION = '" + e.LOCATION + "', TICKETS_NUM = " + e.TICKETS_NUM + ", INFO = '" + e.INFO + "' Where TITLE = '" + oldtitle+"'";
            db.ExecuteNonQuery(query1);
            return (db.ExecuteNonQuery(query2) != null);

        }
        public List<Event> GetEvents()
        {
            string query = "SELECT * FROM EVENT WHERE EVENTDATE >='"+ DateTime.Now.Date+"'";
            return ConvertDataTable<Event>(db.ExecuteReader(query));
        }
        public List<Artwork> GetArtWorks(Artist A)
        {
            string query = "SELECT * FROM ARTWORK WHERE ARTIST_UNAME='" + A.ARTIST_UNAME + "'";
            return ConvertDataTable<Artwork>(db.ExecuteReader(query));
        }
        public void InsertArtwork(Artwork a) 
        {
            //category is a foreign key get it from the user with drop down list
            string query = "INSERT INTO ARTWORK VALUES('" + a.CATEGORY_NAME + "','" + a.ARTIST_UNAME + "'," + "null"
                 + ",'" + a.TITLE + "',0,'" + a.PRIVACY + "',1,'" + a.DESCRIPTION + "'," + a.WIDTH + "," + a.HEIGHT
                 + "," + a.DEPTH + "," + a.PRICE + ",'" + a.MATERIAL + "','" + a.MEDIUM + "','" + a.SUBJECT + "','" + a.PHOTO + "'," + a.YEAR + ")";
            db.ExecuteNonQuery(query);
        }
        public DataTable GetCategories()
        {
            string query = "SELECT NAME FROM CATEGORY";
            return db.ExecuteReader(query);
        }
        public List<Survey> GetRequestedSurvey(ExpertViewModel e)
        {
            string query = "SELECT * FROM SURVEY WHERE EXPERT_UNAME='" + e.EXPERT_UNAME + "'";
            return ConvertDataTable<Survey>(db.ExecuteReader(query));
        }
        public Survey GetSurveyInfo(int code)
        {
            string query = "SELECT * FROM SURVEY WHERE SURVEY_ID='" + code + "'";
            return ConvertDataTable<Survey>(db.ExecuteReader(query))[0];
        }
        

        public void InsertSurveyRequest(Survey s)
        {
            string query = "INSERT INTO SURVEY VALUES('"+s.EXPERT_UNAME + "','" + s.USER_NAME +"'," + s.BUDGET +
                ",'" + s.ORIENTATION +"','" + s.MORE_INFO + "',null)";
             db.ExecuteNonQuery(query);
        }
        
        public void InsertCustomOrder(CustomOrderUserViewModel c)
        {
            string query = "SELECT ARTIST_UNAME FROM ARTIST WHERE START_SALARY <=" + c.Budget;
             List<Artist> l = ConvertDataTable<Artist>(db.ExecuteReader(query));
            if (l.Count == 0)
                return;
            Random random = new Random();
            int artist = random.Next(0, l.Count);
            string uname = l[artist].ARTIST_UNAME;
            query = "INSERT INTO ARTWORK VALUES('" + c.Category + "','" + uname + "'," + "null"
                + ",'" + c.TITLE + "',0,1,1,'" + c.DESCRIPTION + "'," + c.WIDTH + "," + c.HEIGHT
                + "," + c.DEPTH + "," + c.Budget + ",'" + c.MATERIAL + "','" + c.MEDIUM + "','" + "null" + "','" + "null" + "'," + "null" + ")";
            db.ExecuteNonQuery(query);
            query = "INSERT INTO dbo.[ORDER] VALUES(1,'" + DateTime.Now.ToShortTimeString().Substring(0,9)
            +"','" + c.Deadline.ToString().Substring(0,9) +"');";
            db.ExecuteNonQuery(query);
        }
        public Artwork GetArtworkWithCode(int code)
        {
            string query = "SELECT * FROM Artwork WHERE AW_CODE = "+ code;
            return ConvertDataTable<Artwork>(db.ExecuteReader(query))[0];
        }

        public void InsertBillingInfo(BillingInfo b, string uname)
        {
            string query = "INSERT INTO BILLING_INFO VALUES('" + b.CARD_NUM + "','" + uname + "','"
                + b.STREET + "','" + b.CITY + "','" + b.CARD_HOLDER_NAME + "'," + b.CVV + ",'" + b.EXPIRY_DATE.ToString().Substring(0,9) + "');"; 
            db.ExecuteNonQuery(query);
        }

        public bool ApproveArtwork(int adminId, int code, int state)
        {
            string query = "UPDATE ARTWORK set ACCEPTED = "+ state +", ADMIN_ID = " + adminId + " where AW_CODE = " + code;
            return db.ExecuteNonQuery(query) != 0;
        }


        //PasswordHasher P = new PasswordHasher();

        //string pas = logeduser.password;
        //logeduser.password = P.HashPassword(logeduser.password);
        //    bool f = Convert.ToBoolean(P.VerifyHashedPassword(logeduser.password, pas));

        public bool CreateEvent(Event e)
        {
            string query = "INSERT INTO EVENT VALUES('" + e.TITLE + "', "+ e.ADMIN_ID+", '" + e.IMAGE + "', " + e.TICKET_PRICE + ", '" +
            e.EVENTDATE + "', '" + e.LOCATION + "'," + e.TICKETS_NUM + ", '" + e.INFO + "')";
            return db.ExecuteNonQuery(query) != 0;
        }

        public List<Artist> GetArtists()
        {
            string query = "select * from Artist";
            
            var lst =  ConvertDataTable<Artist>(db.ExecuteReader(query));
            foreach (var obj in lst)
            {
                obj.Selected = "";
            }
            return lst;
        }


        public List<Artwork> GetArtworksforrecommanded()
        {
            string query = "select * from ARTWORK";

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
            string query = "SELECT * FROM SHIPPING_COMPANY";
            return ConvertDataTable<ShippingCompany>(db.ExecuteReader(query));
        }

        public bool Addcompany(ShippingCompany C)
        {
            string query = "INSERT INTO SHIPPING_COMPANY VALUES('" +C.NAME+"', '"+C.EMAIL + "', '" + C.PHONE+ "', "+C.SHIPPING_FEES  +")";
            return (db.ExecuteNonQuery(query) != 0);
        }

        public bool deleteCompany(string C)
        {
            string query = "DELETE FROM SHIPPING_COMPANY WHERE NAME = '" + C + "'";
            return (db.ExecuteNonQuery(query) != 0);
        }

        public List<Artwork> GetArtworks()
        {
            string query = "SELECT * FROM Artwork";
            return ConvertDataTable<Artwork>(db.ExecuteReader(query));
        }

        public OrderInfo GetOrderInfo(int id)
        {
            string query = "SELECT * FROM ORDER_INFO WHERE ORDER_ID = " + id;
            return ConvertDataTable<OrderInfo>(db.ExecuteReader(query))[0];
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
            string query = "SELECT * FROM Artwork WHERE TITLE='" + title + "';";
            return ConvertDataTable<Artwork>(db.ExecuteReader(query));

        }
        public void rateArtwork(int rating,int code,string uname) {

            string query = "INSERT INTO RATE_AW VALUES('" + uname + "'," + code + "," + rating + ");";
            db.ExecuteNonQuery(query);
            query = "UPDATE ARTWORK SET STATUS =0 WHERE AW_CODE=" + code + ";";
            db.ExecuteNonQuery(query);
        }
        public void addFav(int code, string uname)
        {
            string query = "INSERT INTO FAV_AW VALUES(" + code + ",'" + uname  + "');";
            db.ExecuteNonQuery(query);
        }
        public List<Artwork> GetFavourite(string email)
        {
            string query = "SELECT * FROM FAV_AW FA JOIN ARTWORK A ON FA.AW_CODE = A.AW_CODE JOIN [dbo].[USER] U ON FA.USER_NAME = U.USER_NAME" +
                " WHERE U.EMAIL ='" + email + "';";
           return ConvertDataTable<Artwork>(db.ExecuteReader(query));
        }
        

    }
}

