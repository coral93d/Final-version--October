using AspNetRoleBasedSecurity.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Dapper;

namespace AspNetRoleBasedSecurity.Repository
{
    public class PostRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["master"].ToString();
            con = new SqlConnection(constr);

        }

       

        public bool AddPost(PostModel obj)
        {

            var Tags = string.Join(",", obj.Tags);            
            connection();
            SqlCommand com = new SqlCommand("AddNewPostDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            var user = System.Web.HttpContext.Current.User.Identity.GetUserName();
            com.Parameters.AddWithValue("@Title", obj.Title);
            com.Parameters.AddWithValue("@Tags", Tags);
            com.Parameters.AddWithValue("@Subject", obj.Subject);
            com.Parameters.AddWithValue("@User_name", user);           
            com.Parameters.AddWithValue("@DatePosted", DateTime.Now);
            com.Parameters.AddWithValue("@Samples", obj.Samples);
            //Trace.Write(obj.Tags);
            // Trace.Write(obj.Title);


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
        //To view employee details with generic list     
        public List<PostModel> GetAllPost()
        {
            connection();
            List<PostModel> PostList = new List<PostModel>();


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
                var Approver_name = System.Web.HttpContext.Current.User.Identity.GetUserName();
                var getTags = string.Join(",", dr["Tags"]);
              
                    PostList.Add(
               
                    new PostModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        getTags = Convert.ToString(dr["Tags"]),
                        Subject = Convert.ToString(dr["Subject"]),
                        user = Convert.ToString(dr["User_name"]),
                        DatePosted = Convert.ToDateTime(dr["DatePosted"]),
                        Status = Convert.ToString(dr["Status"]),
                        Samples = Convert.ToString(dr["Samples"]),
                        PageView = Convert.ToInt32(dr["PageView"]),
                        Approver_name = Convert.ToString(dr["Approver_name"])


                    }


                    );
                }


            return PostList;


        }

        public List<Tags> Tags()
        {
            connection();
            List<Tags> TagList = new List<Tags>();
            SqlCommand com = new SqlCommand("GetTags", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                TagList.Add(

                new Tags
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Track_Name = Convert.ToString(dr["Track_Name"]),
                    Descripition = Convert.ToString(dr["Descripition"]),
                    On_time = Convert.ToString(dr["On_time"]),
                    BC = Convert.ToString(dr["BC"]),
                    CC = Convert.ToString(dr["CC"]),
                    EU = Convert.ToString(dr["EU"]),
                    Updates = Convert.ToString(dr["Updates"])
                }


                );
            }


            return TagList;


        }

        public List<Users> Users()
        {
            connection();
            List<Users> PostList = new List<Users>();
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

                
                    PostList.Add(

                new Users
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

            return PostList;


        }
        public bool ViewDetails(PostModel obj)
        {
            PostModel postmodel = new PostModel();
            connection();

            SqlCommand com = new SqlCommand("ViewDetails", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);

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


        public List<PostModel>UserProfile()

        {
            connection();
            List<PostModel> PostList = new List<PostModel>();
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
                    PostList.Add(

                    new PostModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        getTags = Convert.ToString(dr["Tags"]),
                        Subject = Convert.ToString(dr["Subject"]),
                        user = Convert.ToString(dr["User_name"]),
                        DatePosted = Convert.ToDateTime(dr["DatePosted"])

                    }


                    );

                }
            }

            return PostList;


        }
        public bool EditProfile(Profile obj)
        {
                      
            connection();
            SqlCommand com = new SqlCommand("UpdateProfile", con);
            com.CommandType = CommandType.StoredProcedure;

            var user = System.Web.HttpContext.Current.User.Identity.GetUserName();

            com.Parameters.AddWithValue("@User_name", user);
            com.Parameters.AddWithValue("@Dname", obj.Dname);            
            com.Parameters.AddWithValue("@About", obj.About);          
            com.Parameters.AddWithValue("@Process", obj.Process);
            com.Parameters.AddWithValue("@Position", obj.Position);  
            com.Parameters.AddWithValue("@Company", obj.Company);
            com.Parameters.AddWithValue("@Location", obj.Location);
            com.Parameters.AddWithValue("@Email", obj.Email);
            com.Parameters.AddWithValue("@Gender", obj.Gender);

            //Trace.Write(obj.Tags);
            // Trace.Write(obj.Title);


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

        public DataSet GetName(string prefix)

        {
            connection();
            SqlCommand com = new SqlCommand("Select * from Infractio where Title like '%'+@prefix+'%'", con);

            com.Parameters.AddWithValue("@prefix", prefix);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            con.Open();
            da.Fill(ds);
            con.Close();
            return ds;

        }
        public bool UpdatePost(PostModel obj)
        {
            var Approver_name = System.Web.HttpContext.Current.User.Identity.GetUserName();
            connection();
            SqlCommand com = new SqlCommand("UpdatePostDetails", con);
           
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);
            com.Parameters.AddWithValue("@Title", obj.Title);
            com.Parameters.AddWithValue("@Tags", obj.getTags);
            com.Parameters.AddWithValue("@Subject", obj.Subject);
            com.Parameters.AddWithValue("@User_name", obj.user);
            com.Parameters.AddWithValue("@DatePosted", obj.DatePosted);
            com.Parameters.AddWithValue("@Status", obj.Status);
            com.Parameters.AddWithValue("@PageView", obj.PageView);
            com.Parameters.AddWithValue("@Samples", obj.Samples);           
            com.Parameters.AddWithValue("@Approver_name", Approver_name);

            
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



        public bool UpdateTags(Tags obj)
        {

            connection();
            SqlCommand com = new SqlCommand("UpdateTag", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);
            com.Parameters.AddWithValue("@Track_Name", obj.Track_Name);
            com.Parameters.AddWithValue("@Descripition", obj.Descripition);
            com.Parameters.AddWithValue("@BC", obj.BC);
            com.Parameters.AddWithValue("@CC", obj.CC);
            com.Parameters.AddWithValue("@EU", obj.EU);
            com.Parameters.AddWithValue("@On_time", obj.On_time);
            com.Parameters.AddWithValue("@Updates", obj.Updates);
           

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


        public List<PostModel> GetAllPostUser()
        {
            connection();
            List<PostModel> PostList = new List<PostModel>();
            var Approver_name = System.Web.HttpContext.Current.User.Identity.GetUserName();

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
                var getTags = string.Join(",", dr["Tags"]);
                PostList.Add(

                    new PostModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        getTags = Convert.ToString(dr["Tags"]),
                        Subject = Convert.ToString(dr["Subject"]),
                        user = Convert.ToString(dr["User_name"]),
                        DatePosted = Convert.ToDateTime(dr["DatePosted"]),
                        Status = Convert.ToString(dr["Status"]),
                         Samples = Convert.ToString(dr["Samples"]),
                        PageView = Convert.ToInt32(dr["PageView"]),
                        Approver_name = Convert.ToString(dr["Approver_name"])
                    }


                    );


            }

            return PostList;


        }


        public IEnumerable<ArticlesComments> Getcomments()
        {
            connection();
            List<ArticlesComments> PostDetails = new List<ArticlesComments>();
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

                new ArticlesComments
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

        public List<Users> UserTag()
        {
            connection();
            List<Users> PostList = new List<Users>();
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

                if (users == Convert.ToString(dr["User_name"])) { 
                    PostList.Add(

            new Users
            {

                userr = Convert.ToString(dr["User_name"]),
                getProcess = Convert.ToString(dr["Process"])
               
            }
           

            );

                }

            }

            return PostList;


        }
        public List<test> bulk()
        {
            connection();
            List<test> UserList = new List<test>();


            SqlCommand com = new SqlCommand("bulk", con);
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
                //
                // var getTags = string.Join(",", dr["Tags"]);

                UserList.Add(

                new test
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    userName = Convert.ToString(dr["UserName"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"])



                }


                );
            }


            return UserList;


        }
    }


}






