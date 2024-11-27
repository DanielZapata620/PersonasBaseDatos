using Ej3Personas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ej3Personas.Views
{
    /// <summary>
    /// Lógica de interacción para AgregarPersonaView.xaml
    /// </summary>
    public partial class AgregarPersonaView : UserControl
    {
        public AgregarPersonaView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CmbGenero.ItemsSource=Enum.GetValues(typeof(Sexo));
        }
    }
}
