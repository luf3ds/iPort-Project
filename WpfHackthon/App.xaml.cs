using System.Configuration;
using System.Data;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.Windows.Markup;

namespace WpfHackthon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            // Garante que os controles do WPF usem a cultura correta para formatação
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(cultureInfo.IetfLanguageTag)));

            base.OnStartup(e);
        }
    }

}
