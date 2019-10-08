using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetRoleBasedSecurity
{
    public partial class Report : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ReportViewer1.LocalReport.Refresh();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}