# Realizar busca em DataGridView em C# com base de dados Sql Server Express
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- C#
- Visual Studio 2010 SDK
- SQL Server
- ADO.NET
- Windows Forms
- .NET Framework
## Topics
- Controls
- C#
- SQL
- SQL Server
- ADO.NET
- DataGridView
## Updated
- 06/29/2012
## Description

<h1>Introdu&ccedil;&atilde;o</h1>
<p><em>Este projeto tem como objetivo demonstrar como pode ser feito uma busca utilizando base de dados SqlServerExpress em WindowsForms utilizando a linguagem C#, tem como foco demonstrar por meio de c&oacute;digos como &eacute; feita a conex&atilde;o com
 o DB bem como a recupera&ccedil;&atilde;o dos dados com par&acirc;metro especificado.</em></p>
<h1><span>Criando o Exemplo</span></h1>
<p><em>Para elaborar o exemplo foi criado um projeto em WindowsForms utilizando a linguagem C# e tamb&eacute;m foi criado um banco de dados no SqlExpress.</em></p>
<p><em>Segue a imagem da tabela utilizada para realizar a consulta bem como suas colunas:</em></p>
<p><em><img id="60389" src="60389-sem%20t%c3%adtulo.png" alt="" width="406" height="175"></em></p>
<p>&nbsp;</p>
<p><em>Segue abaixo a tela criada em windowsForms para a exibi&ccedil;&atilde;o dos dados (DataGridView) e os controles para a realiza&ccedil;&atilde;o da busca, que neste exemplo quero recuperar todos os funcionarios cujo o nome contenha as letras informadas
 no TextBox.</em></p>
<p><em><img id="60390" src="60390-sem%20t%c3%adtulo.png" alt="" width="395" height="270"></em></p>
<p><em>foi utilizado neste exemplo um DataGridView nomeado de &quot;dgvClientes&quot;, um Label com a propriedade Text setada para &quot;Buscar:&quot;, um TextBox com a propriedade Name setado para &quot;txtBuscar&quot; e por fim um</em></p>
<p><span style="font-size:20px; font-weight:bold">Descri&ccedil;&atilde;o</span></p>
<p><em>Este exemplo tem o objetivo demonstrar de maneira pr&aacute;tica como pode ser feito uma consulta em uma base de dados com um par&acirc;metro espec&iacute;fico que vai desde a conex&atilde;o com DB at&eacute; a recupera&ccedil;&atilde;o e exibi&ccedil;&atilde;o
 dos resultados obtidos pela consulta.</em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Editar Script</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System;
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

        //M&eacute;todo para inserir que recebe um 
        //objeto Funcionario como par&acirc;metro
        private List&lt;Funcionario&gt; ObterLista(string nomeFuncionario)
        {
            //Inst&acirc;ncia da conex&atilde;o
            SqlConnection conn = new SqlConnection(@&quot;Aqui sua ConnectiomString&quot;);
            //Inst&acirc;ncia da lista que ser&aacute; retornada
            List&lt;Funcionario&gt; lista = new List&lt;Funcionario&gt;();
            // query do Comando utilizando o par&acirc;metro recebido pelo m&eacute;todo 
            string query = &quot;SELECT IdFuncionario, Nome, Email FROM Funcionario WHERE Nome LIKE '%&quot; &#43; nomeFuncionario &#43; &quot;%'&quot;;
            //Inst&acirc;ncia do comando
            SqlCommand cmd = new SqlCommand(query, conn);
            //Abro conex&atilde;o
            conn.Open();
            //inst&acirc;ncia do leitor
            SqlDataReader leitor = cmd.ExecuteReader();
            //Se h&aacute; linhas
            if (leitor.HasRows)
            {
                //enquanto l&ecirc;
                while (leitor.Read())
                {
                    //Inst&acirc;ncia de um novo objeto funcionario
                    Funcionario f = new Funcionario();
                    //Recupero os valores 
                    f.IdFuncionario = Convert.ToInt32(leitor[&quot;IdFuncionario&quot;]);
                    f.Nome = leitor[&quot;Nome&quot;].ToString();
                    f.Email = leitor[&quot;Email&quot;].ToString();
                    //Adiciono a lista
                    lista.Add(f);
                }
            }
            //Fecho conex&atilde;o
            conn.Close();
            //Retorno a lista 
            return lista;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvClientes.DataSource = ObterLista(txtBuscar.Text);
        }

        //Click do bot&atilde;o Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Seto o retorno do m&eacute;todo ObterLista como data source 
            //do DataGridView passando o txtBuscar como par&acirc;metro
            //ou seja o m&eacute;todo vai retornar um list dos dados que 
            //&quot;contenham&quot; o que foi digitado no Textbox
            dgvClientes.DataSource = ObterLista(txtBuscar.Text);
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Windows.Forms;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Data.SqlClient;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;BuscaDgvWindowsForms&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;Form1&nbsp;:&nbsp;Form&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Form1()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//classe&nbsp;funcionario&nbsp;e&nbsp;seus&nbsp;atributos</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;Funcionario&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;IdFuncionario&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Nome&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Email&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//M&eacute;todo&nbsp;para&nbsp;inserir&nbsp;que&nbsp;recebe&nbsp;um&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//objeto&nbsp;Funcionario&nbsp;como&nbsp;par&acirc;metro</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;List&lt;Funcionario&gt;&nbsp;ObterLista(<span class="cs__keyword">string</span>&nbsp;nomeFuncionario)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Inst&acirc;ncia&nbsp;da&nbsp;conex&atilde;o</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlConnection&nbsp;conn&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SqlConnection(@<span class="cs__string">&quot;Aqui&nbsp;sua&nbsp;ConnectiomString&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Inst&acirc;ncia&nbsp;da&nbsp;lista&nbsp;que&nbsp;ser&aacute;&nbsp;retornada</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;Funcionario&gt;&nbsp;lista&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;Funcionario&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;query&nbsp;do&nbsp;Comando&nbsp;utilizando&nbsp;o&nbsp;par&acirc;metro&nbsp;recebido&nbsp;pelo&nbsp;m&eacute;todo&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;query&nbsp;=&nbsp;<span class="cs__string">&quot;SELECT&nbsp;IdFuncionario,&nbsp;Nome,&nbsp;Email&nbsp;FROM&nbsp;Funcionario&nbsp;WHERE&nbsp;Nome&nbsp;LIKE&nbsp;'%&quot;</span>&nbsp;&#43;&nbsp;nomeFuncionario&nbsp;&#43;&nbsp;<span class="cs__string">&quot;%'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Inst&acirc;ncia&nbsp;do&nbsp;comando</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlCommand&nbsp;cmd&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SqlCommand(query,&nbsp;conn);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Abro&nbsp;conex&atilde;o</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;conn.Open();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//inst&acirc;ncia&nbsp;do&nbsp;leitor</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlDataReader&nbsp;leitor&nbsp;=&nbsp;cmd.ExecuteReader();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Se&nbsp;h&aacute;&nbsp;linhas</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(leitor.HasRows)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//enquanto&nbsp;l&ecirc;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(leitor.Read())&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Inst&acirc;ncia&nbsp;de&nbsp;um&nbsp;novo&nbsp;objeto&nbsp;funcionario</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Funcionario&nbsp;f&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Funcionario();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Recupero&nbsp;os&nbsp;valores&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;f.IdFuncionario&nbsp;=&nbsp;Convert.ToInt32(leitor[<span class="cs__string">&quot;IdFuncionario&quot;</span>]);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;f.Nome&nbsp;=&nbsp;leitor[<span class="cs__string">&quot;Nome&quot;</span>].ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;f.Email&nbsp;=&nbsp;leitor[<span class="cs__string">&quot;Email&quot;</span>].ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Adiciono&nbsp;a&nbsp;lista</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lista.Add(f);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Fecho&nbsp;conex&atilde;o</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;conn.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Retorno&nbsp;a&nbsp;lista&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;lista;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Form1_Load(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dgvClientes.DataSource&nbsp;=&nbsp;ObterLista(txtBuscar.Text);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Click&nbsp;do&nbsp;bot&atilde;o&nbsp;Buscar</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;btnBuscar_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Seto&nbsp;o&nbsp;retorno&nbsp;do&nbsp;m&eacute;todo&nbsp;ObterLista&nbsp;como&nbsp;data&nbsp;source&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//do&nbsp;DataGridView&nbsp;passando&nbsp;o&nbsp;txtBuscar&nbsp;como&nbsp;par&acirc;metro</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//ou&nbsp;seja&nbsp;o&nbsp;m&eacute;todo&nbsp;vai&nbsp;retornar&nbsp;um&nbsp;list&nbsp;dos&nbsp;dados&nbsp;que&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&quot;contenham&quot;&nbsp;o&nbsp;que&nbsp;foi&nbsp;digitado&nbsp;no&nbsp;Textbox</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dgvClientes.DataSource&nbsp;=&nbsp;ObterLista(txtBuscar.Text);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
