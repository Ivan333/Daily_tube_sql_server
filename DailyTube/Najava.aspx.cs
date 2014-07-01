using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;


namespace WebApplication4
{
    public partial class Najava : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["SQLStr"].ConnectionString;
            SqlConnection oc = new SqlConnection(conStr);

            try
            {


                oc.Open();
                string sel = "select * from zahomepage WHERE username = " + "'" + tbIme.Text + "'";
                SqlCommand oCmd = new SqlCommand(sel, oc);
                //  oCmd.Parameters.AddWithValue("@username", tbIme.Text);
                oCmd.CommandType = System.Data.CommandType.Text;

                SqlDataReader odr = oCmd.ExecuteReader();
                odr.Read();
                if (odr["password"].ToString().Equals(tbPass.Text))
                {
                    Session["imeK"] = tbIme.Text;
                    //Ivan
                    Session["idK"] = odr["POSETITEL_ID"].ToString();
                    if (odr["admin_id"].ToString().Trim() == "")
                    {
                        string odrId = odr["korisnik_id"].ToString();
                        odr.Close();
                        string query = "select distinct korisnik_id from pretplata where korisnik_id = " + Session["idK"].ToString();
                        SqlCommand cmd = new SqlCommand(query, oc);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        if (!reader.HasRows)
                        {
                            Session["posetitelId"] = odrId;
                            Response.Redirect("Pretplata.aspx?id=" + Session["idK"].ToString());
                        }
                        else
                        {
                            Response.Redirect("SiteVidea.aspx");
                            Session["posetitelId"] = odrId;
                        }


                    }
                    else
                    {

                        Response.Redirect("AdminPocetna.aspx");
                        Session["posetitelId"] = odr["admin_id"].ToString();
                    }

                }
                else
                {
                    lblPoraka.Text = "Погрешна лозинка";
                    Panel3.Attributes.Add("class", "centeredMessage uk-alert uk-alert-danger");
                }

            }
            catch (Exception ex)
            {
                lblPoraka.Text = "Се случи нешто, обидете се повторно " + ex.Message;
                Panel3.Attributes.Add("class", "centeredMessage uk-alert uk-alert-danger");
            }
            finally
            {
                oc.Close();
            }

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;

            string conString = ConfigurationManager.ConnectionStrings["SQLStr"].ConnectionString;
            SqlConnection oCon = new SqlConnection(conString);

            string insertUser = "insert into posetitel (posetitel_id, username, password, email) values ( NEXT VALUE FOR dbo.incrUser , '"
                + username + "', '" + password + "', '" + email + "')";

            try
            {
                oCon.Open();
                SqlCommand oCommand = new SqlCommand(insertUser, oCon);
                oCommand.CommandType = CommandType.Text;
                oCommand.ExecuteNonQuery();
                oCommand = new SqlCommand("insert into korisnik values(CONVERT( Float(53), (SELECT current_value FROM sys.sequences WHERE name = 'incrUser')))", oCon);
                oCommand.CommandType = CommandType.Text;
                oCommand.ExecuteNonQuery();
                lblPoraka.Text = "Регистрацијата е успешна";
                Panel3.Attributes.Add("class", "porakaOk");
                string sqlSel = "select posetitel_id from posetitel where username = '" + username + "'";
                DataSet ds = new DataSet();
                oCommand = new SqlCommand(sqlSel, oCon);
                SqlDataAdapter adapter = new SqlDataAdapter(oCommand);
                adapter.Fill(ds, "Posetitel");
                string korId = ds.Tables[0].Rows[0]["posetitel_id"].ToString();
                Session["imeK"] = username;

                Response.Redirect("Pretplata.aspx?id=" + korId);

            }
            catch (Exception ex)
            {
                lblPoraka.Text = ex.Message;
                Panel3.Attributes.Add("class", "centeredMessage uk-alert uk-alert-danger");

            }
            finally
            {
                oCon.Close();
            }
        }
    }
}