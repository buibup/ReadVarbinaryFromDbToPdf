using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ToSaveFileTo = Server.MapPath("~\\File\\Report.pdf");

            var query = @"SELECT MR_Pictures->PIC_websysDocument->docData
FROM MR_Adm
WHERE MRADM_ADM_DR->PAADM_PAPMI_DR->PAPMI_No = '81-17-000017'
AND MR_Pictures->PIC_Path = '1268'";

            using (OdbcConnection cn = new OdbcConnection("DSN=LIVE-TRAK"))

            {

                cn.Open();

                using (OdbcCommand cmd = new OdbcCommand(query, cn))

                {

                    using (OdbcDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.Default))

                    {

                        if (dr.Read())

                        {



                            byte[] fileData = (byte[])dr.GetValue(0);

                            using (System.IO.FileStream fs = new System.IO.FileStream(ToSaveFileTo, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))

                            {

                                using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))

                                {

                                    bw.Write(fileData);

                                    bw.Close();

                                }

                            }

                        }



                        dr.Close();

                        Response.Redirect("~\\File\\Report.pdf");

                    }

                }

            }
        }
    }
}