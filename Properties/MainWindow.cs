using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using System.Collections.Generic;

namespace PDGTClient
{
    class MainWindow : Window
    {
        private List<string> req = new List<string>();
        [UI] private Label status = null;
        [UI] private Label display = null;
        [UI] private Label login_status = null;
        [UI] private Entry user = null;
        [UI] private Entry psw = null;
        [UI] private Button login = null;
        [UI] private Button signup = null;
        [UI] private Button delete = null;
        [UI] private Button update = null;
        [UI] private Button logout = null;
        [UI] private Button visits = null;
        [UI] private Button home = null;
        [UI] private Entry dataEntry = null;
        [UI] private ToggleButton Abruzzo = null;
        [UI] private ToggleButton Basilicata = null;
        [UI] private ToggleButton PABolzano = null;
        [UI] private ToggleButton Calabria = null;
        [UI] private ToggleButton Campania = null;
        [UI] private ToggleButton EmiliaRomagna = null;
        [UI] private ToggleButton FriuliVeneziaGiulia = null;
        [UI] private ToggleButton Lazio = null;
        [UI] private ToggleButton Liguria = null;
        [UI] private ToggleButton Lombardia = null;
        [UI] private ToggleButton Marche = null;
        [UI] private ToggleButton Molise = null;
        [UI] private ToggleButton Piemonte = null;
        [UI] private ToggleButton Puglia = null;
        [UI] private ToggleButton Sardegna = null;
        [UI] private ToggleButton Sicilia = null;
        [UI] private ToggleButton Toscana = null;
        [UI] private ToggleButton PATrento = null;
        [UI] private ToggleButton Umbria = null;
        [UI] private ToggleButton ValleDAosta = null;
        [UI] private ToggleButton Veneto = null;
        [UI] private Button search = null;  
        private Client client;

        public MainWindow() : this(new Builder("MainWindow.glade")) {}

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            DeleteEvent += Window_DeleteEvent;
            login.Clicked += login_Clicked;
            signup.Clicked += signup_Clicked;
            delete.Clicked += delete_Clicked;
            update.Clicked += update_Clicked;
            logout.Clicked += logout_Clicked;
            visits.Clicked += visits_Clicked;
            search.Clicked += search_Clicked;
            home.Clicked += home_Clicked;
            Abruzzo.Clicked += Abruzzo_Clicked;
            Basilicata.Clicked += Basilicata_Clicked;
            PABolzano.Clicked += PABolzano_Clicked;
            Calabria.Clicked += Calabria_Clicked;
            Campania.Clicked += Campania_Clicked;
            EmiliaRomagna.Clicked += EmiliaRomagna_Clicked;
            FriuliVeneziaGiulia.Clicked += FriuliVeneziaGiulia_Clicked;
            Lazio.Clicked += Lazio_Clicked;
            Liguria.Clicked += Liguria_Clicked;
            Lombardia.Clicked += Lombardia_Clicked;
            Marche.Clicked += Marche_Clicked;
            Molise.Clicked += Molise_Clicked;
            Piemonte.Clicked += Piemonte_Clicked;
            Puglia.Clicked += Puglia_Clicked;
            Sardegna.Clicked += Sardegna_Clicked;
            Sicilia.Clicked += Sicilia_Clicked;
            Toscana.Clicked += Toscana_Clicked;
            PATrento.Clicked += PATrento_Clicked;
            Umbria.Clicked += Umbria_Clicked;
            ValleDAosta.Clicked += ValleDAosta_Clicked;
            Veneto.Clicked += Veneto_Clicked;
            this.client = new Client();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a) {
            Application.Quit();
        }

        private async void login_Clicked(object sender, EventArgs a) {
            try {
                this.status.Text = await this.client.login(display, user, psw, login_status);    
            } catch (Exception){
                display.Text = "unable to connect to server";
            }
        }
        private async void signup_Clicked(object sender, EventArgs a) {
            try {
                this.status.Text = await this.client.signup(display, user, psw);    
            } catch (Exception){
                display.Text = "unable to connect to server";
            }
            
        }
        private async void delete_Clicked(object sender, EventArgs a) {
            try {
                this.status.Text = await this.client.delete(display, user, login_status);    
            } catch (Exception){
                display.Text = "unable to connect to server";
            }      
            psw.Text= "";
        }
        private async void update_Clicked(object sender, EventArgs a) {
            try {
                this.status.Text = await this.client.update(display, user, psw);    
            } catch (Exception){
                display.Text = "unable to connect to server";
            }  
        }
        private async void logout_Clicked(object sender, EventArgs a) {
            try {
                this.status.Text = await this.client.logout(display, user, psw, login_status);    
            } catch (Exception){
                display.Text = "unable to connect to server";
            }
        }
        private async void visits_Clicked(object sender, EventArgs a) {
            try {
                this.status.Text = await this.client.visits(display, req, login_status);    
            } catch (Exception){
                display.Text = "unable to connect to server";
            }
        }
        private async void search_Clicked(object sender, EventArgs a) {
            try {
                this.status.Text = await this.client.search(display, user, req, dataEntry);    
            } catch (Exception){
                display.Text = "unable to connect to server";
            }
            psw.Text="";
        }
        private async void home_Clicked(object sender, EventArgs a) {
            try {
                await this.client.home(display);    
            } catch (Exception){
                display.Text = "unable to connect to server";
            }
            psw.Text= "";
        }
        
        private void Abruzzo_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Abruzzo");
            } else {
                this.req.Remove("Abruzzo");
            }
        }
        private void Basilicata_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Basilicata");
            } else {
                this.req.Remove("Basilicata");
            }
        }
        private void PABolzano_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("P.A. Bolzano");
            } else {
                this.req.Remove("P.A. Bolzano");
            }
        }
        private void Calabria_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Calabria");
            } else {
                this.req.Remove("Calabria");
            };
        }
        private void Campania_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Campania");
            } else {
                this.req.Remove("Campania");
            }
        }
        private void EmiliaRomagna_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Emilia-Romagna");
            } else {
                this.req.Remove("Emilia-Romagna");
            }
        }
        private void FriuliVeneziaGiulia_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Friuli Venezia Giulia");
            } else {
                this.req.Remove("Friuli Venezia Giulia");
            }
        }
        private void Lazio_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Lazio");
            } else {
                this.req.Remove("Lazio");
            }
        }
        private void Liguria_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Liguria");
            } else {
                this.req.Remove("Liguria");
            }
        }
        private void Lombardia_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Lombardia");
            } else {
                this.req.Remove("Lombardia");
            }
        }
        private void Marche_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Marche");
            } else {
                this.req.Remove("Marche");
            }
        }
        private void Molise_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Molise");
            } else {
                this.req.Remove("Molise");
            }
        }
        private void Piemonte_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Piemonte");
            } else {
                this.req.Remove("Piemonte");
            }
        }
        private void Puglia_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Puglia");
            } else {
                this.req.Remove("Puglia");
            }
        }
        private void Sardegna_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Sardegna");
            } else {
                this.req.Remove("Sardegna");
            }
        }
        private void Sicilia_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Sicilia");
            } else {
                this.req.Remove("Sicilia");
            }
        }
        private void Toscana_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Toscana");
            } else {
                this.req.Remove("Toscana");
            }
        }
        private void PATrento_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("P.A. Trento");
            } else {
                this.req.Remove("P.A. Trento");
            }
        }
        private void Umbria_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Umbria");
            } else {
                this.req.Remove("Umbria");
            }
        }
        private void ValleDAosta_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Valle d'Aosta");
            } else {
                this.req.Remove("Valle d'Aosta");
            }
        }
        private void Veneto_Clicked(object sender, EventArgs a) {
            if (((ToggleButton) sender).Active){
                this.req.Add("Veneto");
            } else {
                this.req.Remove("Veneto");
            }
        }
    }
}