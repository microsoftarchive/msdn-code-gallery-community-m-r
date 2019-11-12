using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Exemplo.WPF.MVVM.Commands;
using Exemplo.WPF.MVVM.Model;

namespace Exemplo.WPF.MVVM.ViewModel
{
    public class ClubeDeFutebolViewModel : INotifyPropertyChanged
    {
        #region Propriedades

        //Propriedades utilizadas na View             
        private ObservableCollection<ClubeDeFutebol> _listClubesDeFutebol;

        public ObservableCollection<ClubeDeFutebol> ListClubesDeFutebol
        {
            get { return _listClubesDeFutebol; }

            set
            {
                _listClubesDeFutebol = value;
                this.NotifyPropertyChanged("ClubesDeFutebol");
            }
        }

        private ObservableCollection<Estado> _listEstados;

        public ObservableCollection<Estado> ListEstados
        {
            get { return _listEstados; }

            set
            {
                _listEstados = value;
                this.NotifyPropertyChanged("ListEstados");
            }
        }

        private ClubeDeFutebol _clube;

        public ClubeDeFutebol Clube
        {
            get { return _clube; }
            set
            {

                if (value != _clube)
                {
                    _clube = value;
                    //Notificar alteração da propriedade
                    this.NotifyPropertyChanged("Clube");
                }
            }
        }

        private Estado _estado;

        public Estado Estado
        {
            get { return _estado; }
            set
            {
                if (value != _estado)
                {
                    _estado = value;
                    //Notificar alteração da propriedade
                    this.NotifyPropertyChanged("Estado");
                }
              
            }
        }

        private int _idClube;

        public int IDClube
        {
            get { return this._idClube; }
            set
            {
                if (value != _idClube)
                {
                    _idClube = value;
                    //Notificar alteração da propriedade
                    this.NotifyPropertyChanged("IDClube");
                }
            }
        }

        private string _nome;

        public string Nome
        {
            get { return this._nome; }
            set
            {
                if (value != _nome)
                {
                    _nome = value;
                    //Notificar alteração da propriedade
                    this.NotifyPropertyChanged("Nome");
                }
            }
        }

        private string _tecnico;

        public string Tecnico
        {
            get { return this._tecnico; }
            set
            {
                if (value != _tecnico)
                {
                    _tecnico = value;
                    //Notificar alteração da propriedade
                    this.NotifyPropertyChanged("Tecnico");
                }
            }
        }
        #endregion

        #region Construtor

        public ClubeDeFutebolViewModel()
        {
            this.Initialize();
        }

        #endregion

        #region Comandos

        public ICommand SalvarClubeCommand { get; set; }
        public ICommand DeletarClubeCommand { get; set; }
        public ICommand CarregarDadosClubeCommand { get; set; }
        public ICommand CancelarEdicaoCommand { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Inicializa a ViewModel
        /// </summary>
        private void Initialize()
        {
            // Inicializa os comandos
            this.SalvarClubeCommand = new SalvarClubeCommand(this);
            this.DeletarClubeCommand = new DeletarClubeCommand(this);
            this.CarregarDadosClubeCommand = new CarregarDadosClubeCommand(this);
            this.CancelarEdicaoCommand = new CancelarEdicaoCommand(this);

           
            this._listClubesDeFutebol = new ObservableCollection<ClubeDeFutebol>();
            this._listEstados = new ObservableCollection<Estado>();
            
            //Monta a lista de estados
            _listEstados.Add(new Estado(1, "Acre"));
            _listEstados.Add(new Estado(2, "Alagoas"));
            _listEstados.Add(new Estado(3, "Amapá"));
            _listEstados.Add(new Estado(4, "Amazonas"));
            _listEstados.Add(new Estado(5, "Bahia"));
            _listEstados.Add(new Estado(6, "Ceará"));
            _listEstados.Add(new Estado(7, "Distrito Federal"));
            _listEstados.Add(new Estado(8, "Espírito Santo"));
            _listEstados.Add(new Estado(9, "Goiás"));
            _listEstados.Add(new Estado(10, "Maranhão"));
            _listEstados.Add(new Estado(11, "Mato Grosso"));
            _listEstados.Add(new Estado(12, "Mato Grosso do Sul"));
            _listEstados.Add(new Estado(13, "Minas Gerais"));
            _listEstados.Add(new Estado(14, "Pará"));
            _listEstados.Add(new Estado(15, "Paraíba"));
            _listEstados.Add(new Estado(16, "Paraná"));
            _listEstados.Add(new Estado(17, "Pernambuco"));
            _listEstados.Add(new Estado(18, "Piauí"));
            _listEstados.Add(new Estado(19, "Rio de Janeiro"));
            _listEstados.Add(new Estado(20, "Rio Grande do Norte"));
            _listEstados.Add(new Estado(21, "Rio Grande do Sul"));
            _listEstados.Add(new Estado(22, "Rondônia"));
            _listEstados.Add(new Estado(23, "Rondônia"));
            _listEstados.Add(new Estado(24, "Santa Catarina"));
            _listEstados.Add(new Estado(25, "São Paulo"));
            _listEstados.Add(new Estado(26, "Sergipe"));
            _listEstados.Add(new Estado(27, "Tocantins"));

        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
