using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;

namespace ChartControlApplication
{
    public partial class ChartControlDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetEmployeeChartInfo();
            }
        }

        private void GetEmployeeChartInfo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(@"Data Source=DS-8CG9074361\SQLEXPRESS;Initial Catalog=OTC;Integrated Security=True"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Tags as Name, COUNT(Id) AS Total  FROM Infractio GROUP BY Tags", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }

            EmployeeChartInfo.Series[0].Points.DataBindXY(x, y);
            EmployeeChartInfo.Series[0].ChartType = SeriesChartType.Pie;
            EmployeeChartInfo.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            EmployeeChartInfo.Legends[0].Enabled = true;
        }
    }
}