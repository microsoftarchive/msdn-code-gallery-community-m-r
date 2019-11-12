
namespace Exemplo.WPF.MVVM.Model
{
    public class Estado
    {
        #region Construtor

        public Estado(int idEstado, string nome)
        {
            _idEstado = idEstado;
            _nome = nome;
        }

        #endregion

        #region Propriedades

        private int _idEstado;

        public int IdEstado
        {
            get { return this._idEstado; }
            set
            {
                if (value != _idEstado)
                {
                    this._idEstado = value;
                }
            }
        }

        private string _nome;

        public string NomeEstado
        {
            get { return this._nome; }
            set
            {
                if (value != _nome)
                {
                    this._nome = value;
                }
            }
        }
        #endregion
    }
}
