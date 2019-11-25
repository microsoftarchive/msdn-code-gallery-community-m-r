using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition; // Para el reeconocimiento de voz


namespace Reconocimiento_de_voz_1
	{
	public partial class Form1 : Form
		{

		// Inicializamos motor de reconocimiento.
		SpeechRecognitionEngine reconocimiento_de_voz = new SpeechRecognitionEngine();

		string palabras;

		public Form1()
			{
			InitializeComponent();
			}		

		private void button1_Click(object sender, EventArgs e) //Boton escuchar. Configuración del reconocimiento
			{
			//Inicia la escucha con el dispositivo de entrada de audio predeterminado
			reconocimiento_de_voz.SetInputToDefaultAudioDevice(); // Usaremos el microfono predeterminado del sistema
			reconocimiento_de_voz.LoadGrammar(new DictationGrammar()); //Carga la gramatica de Windows
			reconocimiento_de_voz.SpeechRecognized += te_escucho; // Controlador de eventos. Se ejecutara al reconocer
			reconocimiento_de_voz.RecognizeAsync(RecognizeMode.Multiple); //Iniciamos reconocimiento
			label1.Text = "Te estoy escuchando cuentame: ";

			}
		void te_escucho(object sender, SpeechRecognizedEventArgs e)
			{
			palabras = e.Result.Text; // La variable palabras del tipo string toma las palabras reconocidas.
			textBox1.Text = palabras; // Muestra las palabras reconocidas en el textbox
			}

		private void button3_Click(object sender, EventArgs e) // Boton detener escucha
			{
			reconocimiento_de_voz.RecognizeAsyncStop(); //Detiene la escucha
			textBox1.Clear(); //limpia el textbox
			}

		private void button2_Click(object sender, EventArgs e) // Boton Salir
			{
			Application.Exit();
			}
		}
	}


//https://msdn.microsoft.com/es-sv/library/system.speech.recognition.aspx?tduid=(9e470a736dcc454cedb3e6175a4911fd)(256380)(2459594)(TnL5HPStwNw-QMsorr4C3E8RNLMzqgHxBg)()

//https://msdn.microsoft.com/es-es/magazine/dn857362.aspx

//https://msdn.microsoft.com/es-es/windows/uwp/input-and-devices/speech-recognition