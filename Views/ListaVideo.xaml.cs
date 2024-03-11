﻿using Tarea2._4PM2Grupo1.Models;

namespace Tarea2._4PM2Grupo1.Views;

public partial class ListaVideo : ContentPage
{
    public ListaVideo()
    {
        InitializeComponent();
    }

    private async void toolmenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        listapersonas.ItemsSource = await App.BaseDatos.obtenerListaVideo();
    }

    private async void liestapersonas_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        try
        {
            var persona = (Videorecord)e.Item;

            var result = await DisplayActionSheet("Selecciona una opción", "Cancelar", null, "Ver Video", "Borrar");

            if (result == "Ver Video")
            {
                Vista_Video page = new Vista_Video();
                page.BindingContext = persona;
                await Navigation.PushAsync(page);
            }
            else if (result == "Borrar")
            {
                await App.BaseDatos.VideoDelete(persona);
                listapersonas.ItemsSource = await App.BaseDatos.obtenerListaVideo();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar ItemTapped: {ex.Message}");
        }
    }
}
