using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyTube
{
    public partial class BrisenjeKomentari : System.Web.UI.Page
    {
        string conString = ConfigurationManager.ConnectionStrings["SQLStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["imeK"] != null)
                this.Master.lblUser_Text = Session["imeK"].ToString();
            SqlConnection oCon = new SqlConnection(conString);
            string selectSql = "";
            if (Request.QueryString["user"] != null)
                selectSql = "select * from \"Komentari_od_korisnici\" where username = '" + Request.QueryString["user"].ToString() + "'";
            else
                selectSql = "select * from \"Komentari_od_korisnici\"";
            try
            {
                oCon.Open();
                SqlCommand com = new SqlCommand(selectSql, oCon);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Komentari_od_korisnici");
                GridView1.DataSource = ds;
                GridView1.DataBind();
                if(GridView1.Rows.Count == 0 && Request.QueryString["user"] != null)
                {
                    Label3.Text = "Не постојат коментари од избраниот корисник.";
                    Panel2.CssClass = "uk-alert uk-alert-warning";
                }
            }
            catch (Exception ex)
            {
                lblPoraka.Text = "Се случи грешка при комуникација со базата на податоци\n" + ex.Message;
                Panel1.Attributes.Add("class", "uk-alert uk-alert-danger");
            }
            finally
            {
                oCon.Close();
            }
        }
        protected DataSet getSource()
        {
            SqlConnection oCon = new SqlConnection(conString);
            string selectSql = "select * from \"Komentari_od_korisnici\"";
            DataSet ds = new DataSet();
            try
            {
                oCon.Open();
                SqlCommand com = new SqlCommand(selectSql, oCon);
                SqlDataAdapter adapter = new SqlDataAdapter(com);

                adapter.Fill(ds, "Komentari_od_korisnici");
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lblPoraka.Text = "Се случи грешка при комуникација со базата на податоци\n" + ex.Message;
                Panel1.Attributes.Add("class", "uk-alert uk-alert-danger");
            }
            finally
            {
                oCon.Close();
            }
            return ds;
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection oCon = new SqlConnection(conString);
            string deleteKom = "delete from komentar where komentar_id = " + GridView1.DataKeys[e.RowIndex].Value.ToString();
            DataSet ds = new DataSet();
            try
            {
                oCon.Open();
                SqlCommand cmd = new SqlCommand(deleteKom, oCon);
                cmd.ExecuteNonQuery();
                ds = getSource();
                GridView1.DataSource = ds;
                GridView1.DataBind();
                lblPoraka.Text = "Коментарот е успешно избришан";
                Panel1.Attributes.Add("class", "uk-alert uk-alert-success");
            }
            catch (Exception ex)
            {
                lblPoraka.Text = "Се случи грешка при комуникација со базата на податоци\n" + ex.Message;
                Panel1.Attributes.Add("class", "uk-alert uk-alert-danger");
            }

            finally
            {
                oCon.Close();
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = getSource();
        }
    }
}