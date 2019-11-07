using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BuscaDgvWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //classe funcionario e seus atributos
        public class Funcionario
        {
            public int IdFuncionario { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
        }

        //Método para inserir que recebe um 
        //objeto Funcionario como parâmetro
        private List<Funcionario> ObterLista(string nomeFuncionario)
        {
            //Instância da conexão
            SqlConnection conn = new SqlConnection(@"Aqui sua ConnectiomString");
            //Instância da lista que será retornada
            List<Funcionario> lista = new List<Funcionario>();
            // query do Comando utilizando o parâmetro recebido pelo método 
            string query = "SELECT IdFuncionario, Nome, Email FROM Funcionario WHERE Nome LIKE '%" + nomeFuncionario + "%'";
            //Instância do comando
            SqlCommand cmd = new SqlCommand(query, conn);
            //Abro conexão
            conn.Open();
            //instância do leitor
            SqlDataReader leitor = cmd.ExecuteReader();
            //Se há linhas
            if (leitor.HasRows)
            {
                //enquanto lê
                while (leitor.Read())
                {
                    //Instância de um novo objeto funcionario
                    Funcionario f = new Funcionario();
                    //Recupero os valores 
                    f.IdFuncionario = Convert.ToInt32(leitor["IdFuncionario"]);
                    f.Nome = leitor["Nome"].ToString();
                    f.Email = leitor["Email"].ToString();
                    //Adiciono a lista
                    lista.Add(f);
                }
            }
            //Fecho conexão
            conn.Close();
            //Retorno a lista 
            return lista;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvClientes.DataSource = ObterLista(txtBuscar.Text);
        }

        //Click do botão Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Seto o retorno do método ObterLista como data source 
            //do DataGridView passando o txtBuscar como parâmetro
            //ou seja o método vai retornar um list dos dados que 
            //"contenham" o que foi digitado no Textbox
            dgvClientes.DataSource = ObterLista(txtBuscar.Text);
        }
    }
}
