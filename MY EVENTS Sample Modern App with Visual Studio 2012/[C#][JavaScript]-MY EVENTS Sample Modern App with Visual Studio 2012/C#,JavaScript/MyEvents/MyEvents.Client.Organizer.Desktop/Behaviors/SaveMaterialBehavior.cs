using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using MyEvents.Api.Client;

namespace MyEvents.Client.Organizer.Desktop.Behaviors
{
    /// <summary>
    /// Class that holds the click behavior on the datagrid to show the material
    /// </summary>
    public class SaveMaterialBehavior : Behavior<Grid>
    {

        /// <summary>
        /// Logic to execute when the behavior is attached to the grid 
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        }

        /// <summary>
        /// Logic to be executed when the behavior is deatached from the grid
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
        }

        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Material selectedMaterial = (Material)(sender as Grid).DataContext;

            var myEventsClient = SimpleIoc.Default.GetInstance<IMyEventsClient>();

            myEventsClient.MaterialService.GetMaterialAsync(selectedMaterial.MaterialId, (result) =>
            {
                Stream myStream;
                SaveFileDialog saveMaterialDialog = new SaveFileDialog();

                saveMaterialDialog.FilterIndex = 2;
                saveMaterialDialog.RestoreDirectory = true;
                saveMaterialDialog.FileName = selectedMaterial.Name;

                if (saveMaterialDialog.ShowDialog().Value == true)
                {
                    if ((myStream = saveMaterialDialog.OpenFile()) != null)
                    {
                        myStream.Write(result.Content, 0, result.Content.Length);
                        myStream.Close();
                    }
                }
            });
        }
    }
}
