using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace jQueryChartApplication
{
    public partial class jQueryGoogleChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<employeeDetails> GetChartData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(@"Data Source=DS-8CG9074361\SQLEXPRESS;Initial Catalog=OTC;Integrated Security=True"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Tags, COUNT(Id) AS Total  FROM Infractio GROUP BY Tags", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            List<employeeDetails> dataList = new List<employeeDetails>();
            foreach (DataRow dtrow in dt.Rows)
            {
                employeeDetails details = new employeeDetails();
                details.EmployeeCity = dtrow[0].ToString();
                details.Total = Convert.ToInt32(dtrow[1]);
                dataList.Add(details);
            }
            return dataList;
        }

        public class employeeDetails
        {
            public string EmployeeCity { get; set; }
            public int Total { get; set; }
        }

       
}
}