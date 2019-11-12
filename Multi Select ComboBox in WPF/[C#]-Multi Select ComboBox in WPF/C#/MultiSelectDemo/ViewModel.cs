using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MultiSelectDemo
{
    public class ViewModel : ViewModelBase
    {

        private Dictionary<string, object> _items;
        private Dictionary<string, object> _selectedItems;
      

        public Dictionary<string, object> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                NotifyPropertyChanged("Items");
            }
        }

        public Dictionary<string, object> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                NotifyPropertyChanged("SelectedItems");
            }
        }

        

        public ViewModel()
        {
            Items = new Dictionary<string, object>();
            Items.Add("Chennai", "MAS");
            Items.Add("Trichy", "TPJ");
            Items.Add("Bangalore", "SBC");
            Items.Add("Coimbatore", "CBE");

            SelectedItems = new Dictionary<string, object>();
            SelectedItems.Add("Chennai", "MAS");
            SelectedItems.Add("Trichy", "TPJ");
        }

        private void Submit()
        {
            foreach (KeyValuePair<string, object> s in SelectedItems)
                MessageBox.Show(s.Key);
        }


    }


}
