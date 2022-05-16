using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
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
using GeneradorLicencias.Logic;

namespace GeneradorLicencias
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BotonGenerar_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                            //jac
                            //ujsdfsds
                if (trialCheck.IsChecked == true)
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    string fechaActual = valorCalendario.SelectedDate.ToString();
                    DateTime fecha = Convert.ToDateTime(fechaActual);

                    if (macTexto.Text == "")
                    {
                        licenciaTexto.Text = CryptoLogic.EncryptString(fecha.ToString("yyyy-MM-dd"), "JAC");

                    }
                    else
                    {
                        licenciaTexto.Text = CryptoLogic.EncryptString(fecha.ToString("yyyy-MM-dd") + "_" + macTexto.Text, "JAC");
                    }
                }

                if(noTrialCheck.IsChecked == true)
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    //DateTime? fechaCalendario = valorCalendario.SelectedDate;
                    string fechaActual = (valorCalendario.SelectedDate).ToString();
                    DateTime fecha = Convert.ToDateTime(fechaActual);
                    if (macTexto.Text == "")
                    {
                        licenciaTexto.Text = CryptoLogic.EncryptString(fecha.AddYears(50).ToString("yyyy-MM-dd"), "JAC");
                        //licenciaTexto.Text = CryptoLogic.EncryptString(fecha.AddYears(50).ToShortDateString(), "JAC");
                    }
                    else
                    {
                        licenciaTexto.Text = CryptoLogic.EncryptString(fecha.AddYears(50).ToString("yyyy-MM-dd") + "_" + macTexto.Text, "JAC");
                        //licenciaTexto.Text = CryptoLogic.EncryptString(fecha.AddYears(50).ToShortDateString() + "_" + macTexto.Text, "JAC");

                    }
                }

            }

            catch(Exception ex)
            {

            }

        }
    }
}
