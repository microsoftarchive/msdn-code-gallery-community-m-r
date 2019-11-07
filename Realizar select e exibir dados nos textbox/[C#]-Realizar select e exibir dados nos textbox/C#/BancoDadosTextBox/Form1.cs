using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BancoDadosTextBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //classe cliente e suas propriedades
        public class Cliente
        {
            public int IdCliente { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public DateTime DataNascimento { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregaListBox();
        }

        private void CarregaListBox()
        {
            //instância da conexão
            SqlCeConnection conn = new SqlCeConnection(@"Data Source=|DataDirectory|\Clientes.sdf");

            //string com o comando a ser executado
            string sql = "SELECT Nome from Cliente";

            //instância do comando recebendo como parâmetro
            //a string com o comando e a conexão
            SqlCeCommand cmd = new SqlCeCommand(sql, conn);

            //abro conexão
            conn.Open();

            //instância do leitor
            SqlCeDataReader leitor = cmd.ExecuteReader();

            //enquanto leitor lê
            while (leitor.Read())
            {
                //para cada iteração adiciono o nome
                //ao listbox
                listBox1.Items.Add(leitor["Nome"].ToString());
            }

            //fecha conexão
            conn.Close();
        }


        //método que faz a consulta no bd e obtém o cliente
        //cujo o nome é informado pelo parâmetro
        private Cliente ObterClientePorNome(string nome)
        {
            //objeto cliente que será retornado pelo método
            Cliente cliente = new Cliente();

            //instância da conexão
            SqlCeConnection conn = new SqlCeConnection(@"Data Source=|DataDirectory|\Clientes.sdf");

            //string com o comando a ser executado
            string sql = "SELECT * from Cliente WHERE Nome=@Nome";

            //instância do comando recebendo como parâmetro
            //a string com o comando e a conexão
            SqlCeCommand cmd = new SqlCeCommand(sql, conn);

            //informo o parâmetro do comando
            cmd.Parameters.AddWithValue("@Nome", nome);

            //abro conexão
            conn.Open();

            //instância do leitor
            SqlCeDataReader leitor = cmd.ExecuteReader();

            //enquanto leitor lê
            while (leitor.Read())
            {
                //passo os valores para o objeto cliente
                //que será retornado
                cliente.IdCliente = Convert.ToInt32(leitor["IdCliente"].ToString());
                cliente.Nome = leitor["Nome"].ToString();
                cliente.Email = leitor["Email"].ToString();
                cliente.DataNascimento = Convert.ToDateTime(leitor["DataNascimento"].ToString());
            }

            //fecha conexão
            conn.Close();

            //Retorno o objeto cliente cujo o 
            //nome é igual ao informado no parâmetro
            return cliente;
        }

        //evento mouseclick do listbox
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //variável recebe o objeto cliente retornado pelo método
            Cliente cliente = ObterClientePorNome(listBox1.SelectedItem.ToString());

            //passo os valores para os textbox
            txtCodigo.Text = cliente.IdCliente.ToString();
            txtNome.Text = cliente.Nome;
            txtEmail.Text = cliente.Email;
            txtDataNascimento.Text = cliente.DataNascimento.ToShortDateString();
        }
    }
}
