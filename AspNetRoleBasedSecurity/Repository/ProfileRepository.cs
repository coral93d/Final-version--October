
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Dapper;

using AspNetRoleBasedSecurity.Models;

namespace AspNetRoleBasedSecurity.Repository
{
    public class ProfileRepository
    {

        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["master"].ToString();
            con = new SqlConnection(constr);
        }

        public IEnumerable<PostModell> GetMasterDetails()
        {
            connection();
            List<PostModell> MasterData = new List<PostModell>();
            SqlCommand com = new SqlCommand("GetPost", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                var users = System.Web.HttpContext.Current.User.Identity.GetUserName();

                var getTags = string.Join(",", dr["Tags"]);
                if (users == Convert.ToString(dr["User_name"]))
                {
                    MasterData.Add(

                    new PostModell
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        getTags = Convert.ToString(dr["Tags"]),
                        Subject = Convert.ToString(dr["Subject"]),
                        user = Convert.ToString(dr["User_name"]),
                        DatePosted = Convert.ToDateTime(dr["DatePosted"]),
                        Status = Convert.ToString(dr["Status"]),
                        PageView = Convert.ToInt32(dr["PageView"])


                    }


                    );

                }
            }

            return MasterData;


        }              

        public bool ViewDetails(PostModell obj)
        {
            PostModell postmodel = new PostModell();
            connection();

            SqlCommand com = new SqlCommand("ViewDetails", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);

            con.Open();
            SqlDataReader rdr = com.ExecuteReader();

            while (rdr.Read())
            {


                postmodel.Title = rdr["Title"].ToString();
                postmodel.getTags = rdr["Tags"].ToString();
                postmodel.Subject = rdr["Subject"].ToString();

            }
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }
        }

        public IEnumerable<Profilee> GetMasterUser()
        {
            connection();
            List<Profilee> MasterData2 = new List<Profilee>();
            SqlCommand com = new SqlCommand("GetUser", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                var users = System.Web.HttpContext.Current.User.Identity.GetUserName();


                if (users == Convert.ToString(dr["User_name"]))
                {
                    MasterData2.Add(

                    new Profilee
                    {

                        user = Convert.ToString(dr["User_name"]),
                        About = Convert.ToString(dr["About"]),
                        Dname = Convert.ToString(dr["Dname"]),
                        getProcess = Convert.ToString(dr["Process"]),
                        Position = Convert.ToString(dr["Position"]),
                        Company = Convert.ToString(dr["Company"]),
                        Location = Convert.ToString(dr["Location"]),
                        Email = Convert.ToString(dr["Email"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        LogDate = Convert.ToDateTime(dr["LogDate"]),
                        ProfilePictureTheme=Convert.ToString(dr["ProfilePictureTheme"])

                    }


                    );

                }
            }

            return MasterData2;


        }

    

        public bool EditProfile(Profilee obj)
        {
            var Process = string.Join(",", obj.Process);
            connection();
            SqlCommand com = new SqlCommand("UpdateProfile", con);
            com.CommandType = CommandType.StoredProcedure;

            var user = System.Web.HttpContext.Current.User.Identity.GetUserName();

            com.Parameters.AddWithValue("@User_name", user);
            com.Parameters.AddWithValue("@Dname", obj.Dname);
            com.Parameters.AddWithValue("@About", obj.About);
            com.Parameters.AddWithValue("@Process", Process);
            com.Parameters.AddWithValue("@Position", obj.Position);
            com.Parameters.AddWithValue("@Company", obj.Company);
            com.Parameters.AddWithValue("@Location", obj.Location);
            com.Parameters.AddWithValue("@Email", obj.Email);
            com.Parameters.AddWithValue("@Gender", obj.Gender);
            com.Parameters.AddWithValue("@ProfilePictureTheme", obj.ProfilePictureTheme);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }

        public List<Profilee> GetMasterUsers()

        {

            connection();

            List<Profilee> MasterData2 = new List<Profilee>();

            SqlCommand com = new SqlCommand("GetUser", con);

            com.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataTable dt = new DataTable();



            con.Open();

            da.Fill(dt);

            con.Close();

            //Bind EmpModel generic list using dataRow     

            foreach (DataRow dr in dt.Rows)

            {

                var users = System.Web.HttpContext.Current.User.Identity.GetUserName();
                MasterData2.Add(
                new Profilee

                {
                    user = Convert.ToString(dr["User_name"]),
                    About = Convert.ToString(dr["About"]),

                    Dname = Convert.ToString(dr["Dname"]),

                    getProcess = Convert.ToString(dr["Process"]),

                    Position = Convert.ToString(dr["Position"]),

                    Company = Convert.ToString(dr["Company"]),

                    Location = Convert.ToString(dr["Location"]),

                    Email = Convert.ToString(dr["Email"]),

                    Gender = Convert.ToString(dr["Gender"]),

                    LogDate = Convert.ToDateTime(dr["LogDate"]),
                    ProfilePictureTheme = Convert.ToString(dr["ProfilePictureTheme"])

                }





                );

            }



            return MasterData2;





        }

        public IEnumerable<PostModell> GetProfileDetails(string user)
        {
            connection();
            List<PostModell> MasterData = new List<PostModell>();
            SqlCommand com = new SqlCommand("GetPost", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                // var users = System.Web.HttpContext.Current.User.Identity.GetUserName();

                var getTags = string.Join(",", dr["Tags"]);
                if (user == Convert.ToString(dr["User_name"]))
                {
                    MasterData.Add(

                    new PostModell
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        getTags = Convert.ToString(dr["Tags"]),
                        Subject = Convert.ToString(dr["Subject"]),
                        user = Convert.ToString(dr["User_name"]),
                        DatePosted = Convert.ToDateTime(dr["DatePosted"]),
                        Status = Convert.ToString(dr["Status"]),
                        PageView = Convert.ToInt32(dr["PageView"])

                    }


                    );

                }
            }

            return MasterData;


        }

        public IEnumerable<Profilee> GetProfileUser(string user)
        {
            connection();
            List<Profilee> MasterData2 = new List<Profilee>();
            SqlCommand com = new SqlCommand("GetUser", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                //var users = System.Web.HttpContext.Current.User.Identity.GetUserName();


                if (user == Convert.ToString(dr["User_name"]))
                {
                    MasterData2.Add(

                    new Profilee
                    {

                        user = Convert.ToString(dr["User_name"]),
                        About = Convert.ToString(dr["About"]),
                        Dname = Convert.ToString(dr["Dname"]),
                        getProcess = Convert.ToString(dr["Process"]),
                        Position = Convert.ToString(dr["Position"]),
                        Company = Convert.ToString(dr["Company"]),
                        Location = Convert.ToString(dr["Location"]),
                        Email = Convert.ToString(dr["Email"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        LogDate = Convert.ToDateTime(dr["LogDate"]),
                        ProfilePictureTheme = Convert.ToString(dr["ProfilePictureTheme"])

                    }


                    );

                }
            }

            return MasterData2;


        }


        public IEnumerable<ArticlesCommentss> Getcomments(string user)
        {
            connection();
            List<ArticlesCommentss> PostDetails = new List<ArticlesCommentss>();
            SqlCommand com = new SqlCommand("GetComments", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                // var users = System.Web.HttpContext.Current.User.Identity.GetUserName();
                if (user == Convert.ToString(dr["User_n"]))
                {

                    PostDetails.Add(

                new ArticlesCommentss
                {

                    CommentId = Convert.ToInt32(dr["CommentId"]),
                    Comments = Convert.ToString(dr["Comments"]),
                    ThisDateTime = Convert.ToDateTime(dr["ThisDateTime"]),
                    ArticleId = Convert.ToInt32(dr["ArticleId"]),
                    Rating = Convert.ToInt32(dr["Rating"]),
                    User_n = Convert.ToString(dr["User_n"]),
                    Status_Com = Convert.ToBoolean(dr["Status_Com"]),
                    Com_mark = Convert.ToBoolean(dr["Com_mark"])

                }


                );

                }
            }

            return PostDetails;


        }

        public IEnumerable<ArticlesCommentss> Getcommentss()
        {
            connection();
            List<ArticlesCommentss> PostDetails = new List<ArticlesCommentss>();
            SqlCommand com = new SqlCommand("GetComments", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                var users = System.Web.HttpContext.Current.User.Identity.GetUserName();
                

                    PostDetails.Add(

                new ArticlesCommentss
                {

                    CommentId = Convert.ToInt32(dr["CommentId"]),
                    Comments = Convert.ToString(dr["Comments"]),
                    ThisDateTime = Convert.ToDateTime(dr["ThisDateTime"]),
                    ArticleId = Convert.ToInt32(dr["ArticleId"]),
                    Rating = Convert.ToInt32(dr["Rating"]),
                    User_n = Convert.ToString(dr["User_n"]),
                    Status_Com = Convert.ToBoolean(dr["Status_Com"]),
                    Com_mark = Convert.ToBoolean(dr["Com_mark"])


                }


                );

                
            }

            return PostDetails;


        }


    }


}






