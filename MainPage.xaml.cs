using CommunityToolkit.Maui.Views;
using Tarea2._4PM2Grupo1.Models;
using Tarea2._4PM2Grupo1.Views;
using Xamarin.Essentials;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Tarea2._4PM2Grupo1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        string videoPath;

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            var videoOptions = new Xamarin.Essentials.MediaPickerOptions
            {
                Title = "Capturar video"
            };

            try
            {
                var video = await Xamarin.Essentials.MediaPicker.CaptureVideoAsync(videoOptions);

                if (video != null)
                {
                    using (var stream = await video.OpenReadAsync())
                    {
                        videoPath = await GetVideoPathAsync(stream);
                        img.Source = MediaSource.FromUri(new Uri(videoPath));
                    }
                }
            }
            catch (Xamarin.Essentials.FeatureNotSupportedException)
            {
                // La captura de video no es compatible en este dispositivo
                await DisplayAlert("Error", "La captura de video no es compatible en este dispositivo.", "Ok");
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                // No se otorgó permiso para acceder a la cámara o al almacenamiento
                await DisplayAlert("Error", "No se otorgó permiso para acceder a la cámara o al almacenamiento.", "Ok");
            }
            catch (Exception ex)
            {
                // Otro error inesperado
                await DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "Ok");
            }
        }



        private async void btnSQlite_Clicked(object sender, EventArgs e)
        {
            // Verificar que se haya capturado un video
            if (string.IsNullOrEmpty(videoPath))
            {
                await DisplayAlert("Atencion", "Debe grabar un video.", "Ok");
                return;
            }

            // Verificar que se hayan ingresado nombres y descripción
            if (string.IsNullOrWhiteSpace(txtnombre.Text) || string.IsNullOrWhiteSpace(txtdescripcion.Text))
            {
                await DisplayAlert("Atencion", "Llen los campos nombre y descripcion", "Ok");
                return;
            }

            var video = new Videorecord
            {
                nombres = txtnombre.Text,
                descripcion = txtdescripcion.Text,
                imagen = await File.ReadAllBytesAsync(videoPath)
            };

            var resultado = await App.BaseDatos.VideoSave(video);

            if (resultado != 0)
            {
                await DisplayAlert("Atencion", "Guardado Exitoso", "Ok");

                // Limpiar los campos y la imagen después de guardar
                txtnombre.Text = string.Empty;
                txtdescripcion.Text = string.Empty;
                videoPath = string.Empty;
                img.Source = null;
            }
            else
            {
                await DisplayAlert("Atencion", "Ha ocurrido un erro", "Ok");
            }

            await Navigation.PopAsync();
        }

        private async Task<string> GetVideoPathAsync(Stream stream)
        {
            string videoFileName = "video.mp4";

            // Obtiene el directorio de almacenamiento específico de la aplicación
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string videoPath = Path.Combine(appDataDir, videoFileName);

            // Guarda el video en el directorio de almacenamiento específico de la aplicación
            using (FileStream fileStream = new FileStream(videoPath, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }

            return videoPath;
        }

        private async void btnLista_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaVideo());
        }


    }
}
