using Tarea2._4PM2Grupo1.Controllers;

namespace Tarea2._4PM2Grupo1
{
    public partial class App : Application
    {
        static DBvideo basedatos;
        public static string VideoPath { get; set; }


        public static DBvideo BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new DBvideo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EmpleadosDB.db3"));
                }
                return basedatos;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
