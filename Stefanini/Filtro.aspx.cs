using Stefanini.Entities;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stefanini
{
    public partial class _Filtro : Page
    {
        User user
        {
            get
            {
                return (User)Session["userLogged"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (user != null)
            {
                lblSeller.Visible = lstSeller.Visible = user.Administrator;

            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!IsPostBack)
            {
                ClearFields();


            }


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void Filtrar()
        {
            String sqlCommand = @"
                           
                                SELECT
	                                Class.Name  as Classification,
	                                Cus.Name,
	                                Cus.Phone,
	                                G.Name	    as	Gender,
	                                C.Name	    as	City,
	                                R.Name	    as	Region,
	                                CONVERT (VARCHAR, Cus.LastPurchase, 103) as LastPurchase,
	                                U.Login     as Seller
                                FROM Customer Cus
                                INNER JOIN Classification Class ON CUS.ClassificationId = Class.Id
                                INNER JOIN Gender G on Cus.GenderId = G.Id
                                INNER JOIN City C ON Cus.CityId = C.id
                                Inner Join Region R ON Cus.RegionId = R.Id
                                Inner Join USERSYS U On Cus.UserId = U.Id
                                WHERE 
                                    (@city = -1 OR C.Id = @city) AND
                                    (@classification = -1 OR Class.Id = @classification) AND
                                    (@gender = -1 OR G.Id = @gender) AND
                                    (@region = -1 OR R.Id = @region) AND
                                    (@seller = -1 OR U.Id = @seller) AND
                                    Cus.Name LIKE '%' + @name + '%' AND
                                    (LEN(@LastPurchase) = 0 OR Cus.LastPurchase BETWEEN @LastPurchase AND @Until);
                                ";


            string conectionString = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
            using (SqlConnection conexao = new SqlConnection(conectionString))
            {
                using (SqlCommand comando = new SqlCommand(sqlCommand, conexao))
                {
                    try
                    {
                        conexao.Open();

                        if (!user.Administrator)
                        {
                            comando.Parameters.AddWithValue("@seller", user.Id);
                        }else
                        {
                            comando.Parameters.AddWithValue("@seller", lstSeller.SelectedItem.Value);
                        }

                        comando.Parameters.AddWithValue("@city", lstCity.SelectedItem.Value);
                        comando.Parameters.AddWithValue("@classification", lstClassification.SelectedItem.Value);
                        comando.Parameters.AddWithValue("@gender", lstGender.SelectedItem.Value);
                        comando.Parameters.AddWithValue("@region", lstRegion.SelectedItem.Value);

                        comando.Parameters.AddWithValue("@name", txtName.Text);

                        if (!String.IsNullOrEmpty(txtLastPurchase.Text))
                        {
                            comando.Parameters.AddWithValue("@LastPurchase", Convert.ToDateTime(txtLastPurchase.Text));
                            comando.Parameters.AddWithValue("@Until", Convert.ToDateTime(txtUntil.Text));
                        }else
                        {
                            comando.Parameters.AddWithValue("@LastPurchase", string.Empty);
                            comando.Parameters.AddWithValue("@Until", String.Empty);
                        }




                        IDataReader leitor = comando.ExecuteReader();
                        var dataTable = new DataTable();
                        dataTable.Load(leitor);

                        gvResult.Columns[7].Visible = user.Administrator;

                        gvResult.DataSource = dataTable;
                        gvResult.DataBind();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (conexao.State == ConnectionState.Open)
                            conexao.Close();
                    }
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtLastPurchase.Text = txtName.Text = txtLastPurchase.Text = String.Empty;
            LoaDddl(lstGender, "name", "id", "select id, name from GENDER");
            LoaDddl(lstCity, "name", "id", "select id, name, regionid from CITY");
            LoaDddl(lstRegion, "name", "id", "select id, name from REGION");
            LoaDddl(lstClassification, "name", "id", "select id, name from CLASSIFICATION");
            LoaDddl(lstSeller, "login", "id", "select id, login from USERSYS where UserRoleId = 2");
        }




        private void LoaDddl(DropDownList ddl, String DataTextField, String DataValuesField, String SqlCommand)
        {
            var dt = ExecSQL(SqlCommand);
            

            ddl.DataSource = dt;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValuesField;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("< Todos >", "-1"));
        }

        private DataTable ExecSQL(String sqlcmd)
        {
            string conectionString = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
            using (SqlConnection conexao = new SqlConnection(conectionString))
            {
                using (SqlCommand comando = new SqlCommand(sqlcmd, conexao))
                {
                    try
                    {
                        conexao.Open();
                        IDataReader leitor = comando.ExecuteReader();
                        var dataTable = new DataTable();
                        dataTable.Load(leitor);

                        return dataTable;
                    }catch(Exception e)
                    {
                        return null;
                    }
                    finally
                    {
                        if (conexao.State == ConnectionState.Open)
                            conexao.Close();
                    }
                }
            }
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Session["USER"] = null;
            Response.Redirect("~/Default.aspx");
        }
    }
}