using Stefanini.Entities;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Stefanini
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        private void logar()
        {
            string conectionString = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
            using (SqlConnection conexao = new SqlConnection(conectionString))
            {
                using (SqlCommand comando = new SqlCommand("select id, login, email, password, userroleid FROM UserSys where email= @email AND password = @senha", conexao))
                {
                    try
                    {
                        conexao.Open();

                        comando.Parameters.AddWithValue("@email", txtEmail.Text);
                        comando.Parameters.AddWithValue("@senha", txtSenha.Text);

                        IDataReader leitor = comando.ExecuteReader();

                        User user = null;
                        while (leitor.Read())
                        {
                            user = new User()
                            {
                                Id = Convert.ToInt32(leitor["id"]),
                                Email = Convert.ToString(leitor["email"]),
                                Login = Convert.ToString(leitor["login"]),
                                Password = Convert.ToString(leitor["password"]),
                                Administrator = Convert.ToInt32(leitor["userroleid"]) == 1
                            };

                            if (user != null) break;
                        }

                        if(user != null)
                        {
                            Session["userLogged"] = user;
                            Response.Redirect("~/Filtro.aspx");
                        }else
                        {
                            MessageBox.Show("Usuário e/ou senha incorreto(s)");
                        }

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

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            logar();
        }
    }
}