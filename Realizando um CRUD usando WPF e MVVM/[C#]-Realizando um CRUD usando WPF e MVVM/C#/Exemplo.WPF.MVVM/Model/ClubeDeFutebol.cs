using System.ComponentModel;

namespace Exemplo.WPF.MVVM.Model
{
    public class ClubeDeFutebol : INotifyPropertyChanged
    {
        #region Construtor

        public ClubeDeFutebol()
        {

        }

        #endregion

        #region Propriedades

        private int _idClube;

        public int IdClube
        {
            get { return _idClube; }
            set
            {
                if (value != _idClube)
                {
                    _idClube = value;
                    //Notificar alteração da propriedade
                    this.NotifyPropertyChanged("IdClube");
                }
               
            }
        }

        private Estado _idEstado;

        public Estado IDEstado
        {
            get { return this._idEstado; }
            set
            {
                if (value != _idEstado)
                {
                    this._idEstado = value;
                    this.NotifyPropertyChanged("IDEstado");
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
                    this._nome = value;
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

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
