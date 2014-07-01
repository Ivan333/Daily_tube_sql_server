using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyTube
{
    public partial class DodavanjeVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["imeK"] != null)
                this.Master.lblUser_Text = Session["imeK"].ToString();
            if (!IsPostBack)
            {
                string conString = ConfigurationManager.ConnectionStrings["SQLStr"].ConnectionString;
                SqlConnection oCon = new SqlConnection(conString);
                string selectKategorija = "select * from kategorija";
                try
                {
                    oCon.Open();
                    SqlCommand oCommand = new SqlCommand(selectKategorija, oCon);
                    oCommand.CommandType = CommandType.Text;
                    SqlDataReader oReader = oCommand.ExecuteReader();
                    while (oReader.Read())
                    {
                        ddlKategorija.Items.Add(new ListItem(oReader["kategorija_ime"].ToString(), oReader["kategorija_id"].ToString()));
                        
                    }
                }
                catch (Exception ex)
                {
                    lblDodavanjeVideoErr.Text = "Се случи грешка при комуникација со базата на податоци\n" + ex.Message;
                    Panel2.Attributes.Add("class", "porakaError");
                }
                finally
                {
                    oCon.Close();
                }
            }
        }
        protected void ddlSajt_SelectedIndexChanged(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["SQLStr"].ConnectionString;
            SqlConnection oCon = new SqlConnection(conString);
            string sajt = ddlSajt.SelectedItem.Text;
            string selectKanal = "select * from \"Kanali_od_sajtovi\" where sajt_ime = '" + sajt + "'";

            try
            {
                oCon.Open();
                SqlCommand oCommand = new SqlCommand(selectKanal, oCon);
                oCommand.CommandType = CommandType.Text;
                SqlDataReader oReader = oCommand.ExecuteReader();
                ddlKanal.Items.Clear();
                while (oReader.Read())
                {
                    
                    ddlKanal.Items.Add(new ListItem(oReader["kanal_ime"].ToString(), oReader["kanal_id"].ToString()));
                }
            }
            catch(Exception ex)
            {
                lblDodavanjeVideoErr.Text = "Се случи грешка при комуникација со базата на податоци\n" + ex.Message;
                Panel2.Attributes.Add("class", "porakaError");
            }
            finally
            {
                oCon.Close();
            }
        }
        protected void btnDodadiVideo_Click(object sender, EventArgs e)
        {
            string naslov = txtNaslovVideo.Text;
            string urlVideo = txtUrlVideo.Text;
            string sajt = ddlSajt.SelectedItem.Text;
            string kanal = ddlKanal.SelectedItem.Text;
            string kategorija = ddlKategorija.SelectedItem.Text;
            string urlSlika = txtUrlSlika.Text;
            string opis = txtOpisVideo.Text;
            string kategorijaID = ddlKategorija.SelectedValue.ToString();
            string kanalID = ddlKanal.SelectedValue.ToString();
            string sajtID = ddlSajt.SelectedValue.ToString();
            string conString = ConfigurationManager.ConnectionStrings["SQLStr"].ConnectionString;
            SqlConnection oCon = new SqlConnection(conString);
            string insertVideo = "insert into video (video_id, video_naslov, br_pregledi, video_url, video_rejting, kategorija_id, sajt_id, kanal_id, opis, slika, data) values (NEXT VALUE FOR dbo.incrVideo, '"
                + naslov + "', " + "0, '" + urlVideo + "', 0, " + kategorijaID + ", " +  sajtID + ", "  + kanalID + ", '" + opis + "', '" + urlSlika + "', sysdate)";
            string insertTag = "insert into tagovi values (CONVERT( Float(53), (SELECT current_value FROM sys.sequences WHERE name = 'incrVideo')), @tagovi)";
            string cleared = naslov.Replace("(", "");
            cleared = naslov.Replace(")", "");
            cleared = naslov.Replace("-", "");
            string tagovi = cleared.Replace(" ", ", ");
            try
            {
                oCon.Open();
                SqlCommand oCommand = new SqlCommand(insertVideo, oCon);
                oCommand.CommandType = CommandType.Text;
                oCommand.ExecuteNonQuery();
                oCommand = new SqlCommand(insertTag, oCon);
                oCommand.Parameters.AddWithValue("@tagovi", tagovi.ToLower());
                oCommand.ExecuteNonQuery();
                lblDodavanjeVideoErr.Text = "Видеото е успешно додадено";
                Panel2.Attributes.Add("class", "porakaOk");
                
            }
            catch (Exception ex)
            {
                lblDodavanjeVideoErr.Text = "Се случи грешка при комуникација со базата на податоци\n" + ex.Message;
                Panel2.Attributes.Add("class", "porakaError");
            }
            finally
            {
                oCon.Close();
            }
        }
        protected void txtUrlSlika_TextChanged(object sender, EventArgs e)
        {
            imgVideoSlika.ImageUrl = txtUrlSlika.Text;
        }
        protected void imgVideoSlika_Load(object sender, EventArgs e)
        {
            imgVideoSlika.ImageUrl = "images/shared/nema_slika.gif";
        }
    }
}