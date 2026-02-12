using PizzaClient.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace PizzaClient
{
    public partial class FormPizza : Form
    {
        //
        // Componenti del form
        //
        private readonly HttpClient _httpClient;
        private List<Pizza> _tutteLePizze = new();
        private readonly ListBox _lstPizze;
        private readonly TextBox _txtId, _txtNome, _txtPrezzo, _txtRicerca, _txtNote, _txtStato;
        private readonly ComboBox _cmbCategoria;
        private readonly Button _btnAggiungi, _btnAggiorna, _btnElimina, _btnElenco, _btnNuovaPizza;
        private readonly Button _btnIngredienti;

        public FormPizza()
        {
            // InitializeComponent();
            
            Text = "Gestione Pizze (Database SQLite)";

            // Imposta la dimensione fissa della finestra
            Size = new Size(1000, 600);
            MinimumSize = new Size(1000, 600);
            MaximumSize = new Size(1000, 600);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };

            // ======================================================
            //  PANNELLO SUPERIORE
            // ======================================================
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60
            };

            var lblTitolo = new Label
            {
                Text = "Gestione Pizze",
                Font = new Font("Arial", 20, FontStyle.Bold),
                Location = new Point(10, 15),
                AutoSize = true
            };

            pnlTop.Controls.Add(lblTitolo);

            // ======================================================
            //  PANNELLO SINISTRO
            // ======================================================
            var pnlLeft = new Panel
            {
                Dock = DockStyle.Left,
                Width = 300,
                Padding = new Padding(10)
            };

            _txtRicerca = new TextBox
            {
                PlaceholderText = "Cerca pizza...",
                Dock = DockStyle.Top,
                Height = 30
            };
            _txtRicerca.TextChanged += (s, e) => FiltraPizze();

            _btnElenco = new Button
            {
                Text = "Ricarica elenco",
                Dock = DockStyle.Top,
                Height = 35
            };
            _btnElenco.Click += async (s, e) => await CaricaPizze();

            _lstPizze = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10)
            };
            _lstPizze.SelectedIndexChanged += LstPizze_SelectedIndexChanged;

            pnlLeft.Controls.Add(_lstPizze);
            pnlLeft.Controls.Add(_btnElenco);
            pnlLeft.Controls.Add(_txtRicerca);

            // ======================================================
            //  PANNELLO DESTRO 
            // ======================================================
            var pnlRight = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Padding = new Padding(10),
            };

            // Colonna etichette stretta, colonna input larga e allineata ai pulsanti
            pnlRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18));
            pnlRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82));

            pnlRight.Controls.Add(new Label
            {
                Text = "Modifica pizza",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true
            }, 0, 0);
            pnlRight.SetColumnSpan(pnlRight.GetControlFromPosition(0, 0), 2);

            // ID
            _txtId = new TextBox { Visible = false };

            // Nome
            pnlRight.Controls.Add(new Label { Text = "Nome:" }, 0, 2);
            _txtNome = new TextBox();
            pnlRight.Controls.Add(_txtNome, 1, 2);

            // Prezzo
            pnlRight.Controls.Add(new Label { Text = "Prezzo:" }, 0, 3);
            _txtPrezzo = new TextBox();
            pnlRight.Controls.Add(_txtPrezzo, 1, 3);

            // Categoria
            pnlRight.Controls.Add(new Label { Text = "Categoria:" }, 0, 4);
            _cmbCategoria = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown };
            _cmbCategoria.Items.AddRange(new[] { "Classica", "Speciale", "Gourmet" });
            pnlRight.Controls.Add(_cmbCategoria, 1, 4);

            // Stato
            pnlRight.Controls.Add(new Label { Text = "Stato:"}, 0, 5);
            _txtStato = new TextBox
            {
                Height = 200,
                Width = 300,
                Anchor = AnchorStyles.Left
            };
            pnlRight.Controls.Add(_txtStato, 1, 5);

            // Note
            pnlRight.Controls.Add(new Label { Text = "Note:" }, 0, 6);
            _txtNote = new TextBox
            {
                Multiline = true,
                Height = 120,
                Width = 360,
                ScrollBars = ScrollBars.Vertical,
                Anchor = AnchorStyles.Left
            };
            pnlRight.Controls.Add(_txtNote, 1, 6);


            // ======================================================
            //  PANNELLO PULSANTI
            // ======================================================
            var pnlButtons = new FlowLayoutPanel { AutoSize = true };

            _btnNuovaPizza = new Button { Text = "Nuova Pizza", Width = 120 };
            _btnAggiungi = new Button { Text = "Aggiungi", Width = 120 };
            _btnAggiorna = new Button { Text = "Aggiorna", Width = 120 };
            _btnElimina = new Button { Text = "Elimina", Width = 120 };
            _btnIngredienti = new Button { Text = "Ottieni Ingredienti", Width = 120 };

            _btnAggiungi.Click += async (s, e) => await AggiungiPizza();
            _btnAggiorna.Click += async (s, e) => await AggiornaPizza();
            _btnElimina.Click += async (s, e) => await EliminaPizza();
            _btnNuovaPizza.Click += (s, e) => ModalitaInserimento();
            _btnIngredienti.Click += (s, e) => OttieniIngredienti();

            pnlButtons.Controls.AddRange(new Control[]
            {
                _btnNuovaPizza, _btnAggiungi, _btnAggiorna, _btnElimina, _btnIngredienti
            });

            pnlRight.Controls.Add(pnlButtons, 1, 7);

            // ===============================================
            // Aggiunge i pannelli

            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            Controls.Add(pnlTop);

            // ================================================
            // Verifica dei campi

            _txtNome.TextChanged += (s, e) => VerificaCampiInserimento();
            _txtPrezzo.TextChanged += (s, e) => VerificaCampiInserimento();
            _cmbCategoria.SelectedIndexChanged += (s, e) => VerificaCampiInserimento();

            // ======================================================
            Load += async (s, e) => 
            {
                await CaricaPizze();
                StatoIniziale();
            };
        }

        // ======================================================================
        //   __  __ ______ _______ ____  _____ _____ 
        //  |  \/  |  ____|__   __/ __ \|  __ \_   _|
        //  | \  / | |__     | | | |  | | |  | || |  
        //  | |\/| |  __|    | | | |  | | |  | || |  
        //  | |  | | |____   | | | |__| | |__| || |_ 
        //  |_|  |_|______|  |_|  \____/|_____/_____|
        // ======================================================================                                                                      

        // Stato iniziale
        private void StatoIniziale()
        {
            AbilitaCampi(false);
            _btnAggiungi.Enabled = false;
            _btnAggiorna.Enabled = false;
            _btnElimina.Enabled = false;
        }

        // Modalità inserimento nuova pizza
        private void ModalitaInserimento()
        {
            _lstPizze.ClearSelected();
            PulisciCampi();
            AbilitaCampi(true);

            _btnAggiungi.Enabled = false;
            _btnAggiorna.Enabled = false;
            _btnElimina.Enabled = false;
        }

        // Modalità modifica pizza esistente
        private void ModalitaModifica()
        {
            AbilitaCampi(true);
            _btnAggiungi.Enabled = false;
            _btnAggiorna.Enabled = true;
            _btnElimina.Enabled = true;
        }

        private void AbilitaCampi(bool abilita)
        {
            _txtNome.Enabled = abilita;
            _txtPrezzo.Enabled = abilita;
            _cmbCategoria.Enabled = abilita;
            _txtNote.Enabled = abilita;
            _txtStato.Enabled = abilita;
        }

        private void PulisciCampi()
        {
            _txtId.Clear();
            _txtNome.Clear();
            _txtPrezzo.Clear();
            _txtNote.Clear();
            _cmbCategoria.SelectedIndex = -1;
        }

        // Verifica che i campi obbligatori siano compilati
        private void VerificaCampiInserimento()
        {
            bool nomeOk = !string.IsNullOrWhiteSpace(_txtNome.Text);
            bool prezzoOk = decimal.TryParse(_txtPrezzo.Text, out _);
            bool categoriaOk = !string.IsNullOrWhiteSpace(_cmbCategoria.Text);

            _btnAggiungi.Enabled = nomeOk && prezzoOk && categoriaOk;
        }
        private void LstPizze_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_lstPizze.SelectedIndex < 0) return;

            var p = _tutteLePizze[_lstPizze.SelectedIndex];

            _txtId.Text = p.Id.ToString();
            _txtNome.Text = p.Nome;
            _txtPrezzo.Text = p.Prezzo.ToString();
            _cmbCategoria.Text = p.Categoria;
            _txtNote.Text = p.Note;

            ModalitaModifica();
        }

        private async Task CaricaPizze()
        {
            try
            {
                var pizze = await _httpClient.GetFromJsonAsync<List<Pizza>>("api/pizze");
                _tutteLePizze = pizze ?? new List<Pizza>();
            }
            catch
            {
                MessageBox.Show("Errore nella connessione al server.");
                _tutteLePizze = new List<Pizza>();
            }

            FiltraPizze();
        }

        private void FiltraPizze()
        {
            string filtro = _txtRicerca.Text.ToLower();
            _lstPizze.Items.Clear();

            foreach (var p in _tutteLePizze)
            {
                if (string.IsNullOrWhiteSpace(filtro) ||
                    p.Nome.ToLower().Contains(filtro) ||
                    p.Categoria.ToLower().Contains(filtro))
                {
                    _lstPizze.Items.Add(p.ToString());
                }
            }
        }

        // ======================================================================
        private async Task AggiungiPizza()
        {
            if (!decimal.TryParse(_txtPrezzo.Text, out decimal prezzo))
            {
                MessageBox.Show("Prezzo non valido.");
                return;
            }

            var pizza = new Pizza
            {
                Nome = _txtNome.Text,
                Prezzo = prezzo,
                Categoria = _cmbCategoria.Text,
                Stato = _txtStato.Text,
                Note = _txtNote.Text
            };

            var resp = await _httpClient.PostAsJsonAsync("api/pizze", pizza);

            if (!resp.IsSuccessStatusCode)
            {
                var err = await resp.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                MessageBox.Show(err["message"]);
                return;
            }

            await CaricaPizze();
        }

        // ======================================================================
        private async Task AggiornaPizza()
        {
            if (!int.TryParse(_txtId.Text, out int id))
            {
                MessageBox.Show("ID non valido.");
                return;
            }

            if (!decimal.TryParse(_txtPrezzo.Text, out decimal prezzo))
            {
                MessageBox.Show("Prezzo non valido.");
                return;
            }

            var pizza = new Pizza
            {
                Id = id,
                Nome = _txtNome.Text,
                Prezzo = prezzo,
                Categoria = _cmbCategoria.Text,
                Note = _txtNote.Text,
                Stato = _txtStato.Text
            };

            var resp = await _httpClient.PutAsJsonAsync($"api/pizze/{id}", pizza);

            if (!resp.IsSuccessStatusCode)
            {
                var err = await resp.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                MessageBox.Show(err["message"]);
                return;
            }

            await CaricaPizze();
        }

        // ======================================================================
        private async Task EliminaPizza()
        {
            DialogResult result = MessageBox.Show("Sei sicuro di voler eliminare la pizza?", "Prompt di eliminazione", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.No || result == DialogResult.Cancel)
            {
                return;
            }

            if (!int.TryParse(_txtId.Text, out int id))
            {
                MessageBox.Show("Seleziona una pizza da eliminare.");
                return;
            }

            await _httpClient.DeleteAsync($"api/pizze/{id}");

            await CaricaPizze();

            _lstPizze.ClearSelected();
            _txtId.Clear();
            _txtNome.Clear();
            _txtPrezzo.Clear();
            _txtNote.Clear();
            _txtStato.Clear();
            _cmbCategoria.SelectedIndex = -1;
        }

        // ======================================================================
        private async void OttieniIngredienti()
        {
            var _apiClient = new HttpClient();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://www.themealdb.com/api/json/v1/1/lookup.php?i=53014");
                var response = await _apiClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                MessageBox.Show(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                MessageBox.Show("Errore nella connessione all'endpoint.");
            }
        }

        // =======================================================================
        private void onFormLoad()
        {
            _btnAggiorna.Enabled = false;
            _btnElenco.Enabled = false;
            _btnElimina.Enabled = false;
        }
    }
}
