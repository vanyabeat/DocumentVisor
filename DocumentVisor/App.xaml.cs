using DocumentVisor.Model.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace DocumentVisor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            using var db = new ApplicationContext();
            db.Database.Migrate();
        }
    }
}