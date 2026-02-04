using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PizzaExpressClient
{
    public partial class Form1 : Form
    {
        // https://localhost:7243/api/pizze
        const string APIurl = "https://localhost:7243";
        static HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }
        public class Pizza
        {
            public int id { get; set; }
            public string nome { get; set; }
            public decimal prezzo { get; set; }
            public string categoria { get; set; }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            txtId.Enabled = false;
            txtName.Enabled = false;
            txtPrice.Enabled = false;
            txtCategory.Enabled = false;
            cmbBxCmd.Items.Clear();
            cmbBxCmd.Items.AddRange(new string[]{ 
                "Lista pizze", 
                "Aggiungi pizza", 
                "Trova pizza per ID", 
                "Rimuovi pizza",
                "Aggiorna pizza",
                "Cerca pizza"
            });
            lstBx.Items.Add("ID - Nome - Prezzo - Categoria");
        }

        private static async Task<string> GetReq()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(APIurl + "/api/pizze");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception e)
            {
                return "Error in get request: " + e.Message;
            }
        }
        private static async Task<string> FindReq(string txtId)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(APIurl + "/api/pizze/" + txtId);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception e)
            {
                return "Error in get request: " + e.Message;
            }
        }
        private static async Task<string> PostReq(string txtId, string txtNome, string txtPrezzo, string txtCategoria)
        {
            ///////// !!! NOTA IMPORTANTE: BISOGNA INSTALLARE IL PACCHETTO System.Text.Json !!! /////////
            try
            {
                var httpContent = new StringContent
                        (
                            JsonSerializer.Serialize(new Pizza
                            {
                                id = int.Parse(txtId),
                                nome = txtNome,
                                prezzo = decimal.Parse(txtPrezzo),
                                categoria = txtCategoria
                            }
                            ),
                            Encoding.UTF8,
                            "application/json"
                        );
                HttpResponseMessage response = await client.PostAsync(
                        APIurl + "/api/pizze", 
                        httpContent
                    );
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception e)
            {
                return "Error in post request: " + e.Message;
            }
        }
        private static async Task<string> PutReq(string txtId, string txtPrezzo)
        {
            try
            {
                var httpContent = new StringContent
                        (
                            JsonSerializer.Serialize(new
                            {
                                prezzo = decimal.Parse(txtPrezzo)
                            }
                            ),
                            Encoding.UTF8,
                            "application/json"
                        );
                HttpResponseMessage response = await client.PutAsync(
                        APIurl + "/api/pizze/" + txtId,
                        httpContent
                    );
                response.EnsureSuccessStatusCode();
                return "Pizza aggiornata con successo.";
            }
            catch (Exception e)
            {
                return "Error in put request: " + e.Message;
            }
        }
        private static async Task<string> DeleteReq(string txtId)
        {
            try
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(APIurl + "/api/pizze/" + txtId);
                    response.EnsureSuccessStatusCode();
                    return "Pizza eliminata con successo.";
                }
                catch (Exception e)
                {
                    return "Error in get request: " + e.Message;
                }
            }
            catch (Exception e)
            {
                return "Error in delete request: " + e.Message;
            }
        }
        private static async Task<string> SearchReq(string maxPrezzo, string categoria)
        {
            try
            {
                HttpResponseMessage response;
                if (maxPrezzo != "" || categoria != "")
                {
                    if (maxPrezzo != "")
                    {
                        response = await client.GetAsync(APIurl + $"/api/pizze/search?maxPrezzo={maxPrezzo}");
                    }
                    else if (categoria != "")
                    {
                        response = await client.GetAsync(APIurl + $"/api/pizze/search?categoria={categoria}");
                    }
                    else
                    {
                        response = await client.GetAsync(APIurl + $"/api/pizze/search?maxPrezzo={maxPrezzo}&categoria={categoria}");
                    }
                }
                else
                {
                    response = await client.GetAsync(APIurl + "/api/pizze");
                }
                
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception e)
            {
                return "Error in get request: " + e.Message;
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            switch (cmbBxCmd.SelectedIndex)
            {
                case 0:
                    // Lista pizze
                    JsonNode pizzeGetNode = JsonNode.Parse(await GetReq());
                    JsonNode pizzeListaNode = pizzeGetNode["pizze"];
                    foreach (var pizza in pizzeListaNode.AsArray())
                    {
                        lstBx.Items.Add($"{pizza["id"]} - {pizza["nome"]} - {pizza["prezzo"]} - {pizza["categoria"]}");
                    }
                    break;
                case 1:
                    // Aggiungi pizza
                    JsonNode pizzaPostNode = JsonNode.Parse(await PostReq(txtId: txtId.Text, txtNome: txtName.Text, txtPrezzo: txtPrice.Text, txtCategoria: txtCategory.Text));
                    lstBx.Items.Add($"{pizzaPostNode["id"]} - {pizzaPostNode["nome"]} - {pizzaPostNode["prezzo"]} - {pizzaPostNode["categoria"]}");
                    break;
                case 2:
                    // Trova pizza per ID
                    lstBx.Items.Add(await FindReq(txtId: txtId.Text));
                    JsonNode pizzaFindNode = JsonNode.Parse(await FindReq(txtId: txtId.Text));
                    lstBx.Items.Add($"{pizzaFindNode["id"]} - {pizzaFindNode["nome"]} - {pizzaFindNode["prezzo"]} - {pizzaFindNode["categoria"]}");
                    break;
                case 3:
                    // Rimuovi pizza
                    lstBx.Items.Add(await DeleteReq(txtId: txtId.Text));
                    break;
                case 4:
                    // Aggiorna pizza
                    lstBx.Items.Add(await PutReq(txtId: txtId.Text, txtPrezzo: txtPrice.Text));
                    break;
                case 5:
                    // Cerca pizza
                    JsonNode pizzeSearchNode = JsonNode.Parse(await SearchReq(maxPrezzo: txtPrice.Text, categoria: txtCategory.Text));
                    JsonNode pizzeSearchListaNode = pizzeSearchNode["pizze"];
                    foreach (var pizza in pizzeSearchListaNode.AsArray())
                    {
                        lstBx.Items.Add($"{pizza["id"]} - {pizza["nome"]} - {pizza["prezzo"]} - {pizza["categoria"]}");
                    }
                    break;
            }
        }
        private void cmbBxCmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbBxCmd.SelectedIndex)
            {
                case 0:
                    // Lista pizze
                    btnSend.Enabled = true;
                    txtId.Enabled = false;
                    txtName.Enabled = false;
                    txtPrice.Enabled = false;
                    txtCategory.Enabled = false;
                    break;
                case 1:
                    // Aggiungi pizza
                    btnSend.Enabled = true;
                    txtId.Enabled = true;
                    txtName.Enabled = true;
                    txtPrice.Enabled = true;
                    txtCategory.Enabled = true;
                    break;
                case 2:
                    // Trova pizza per ID
                    btnSend.Enabled = true;
                    txtId.Enabled = true;
                    txtName.Enabled = false;
                    txtPrice.Enabled = false;
                    txtCategory.Enabled = false;
                    break;
                case 3:
                    // Rimuovi pizza
                    btnSend.Enabled = true;
                    txtId.Enabled = true;
                    txtName.Enabled = false;
                    txtPrice.Enabled = false;
                    txtCategory.Enabled = false;
                    break;
                case 4:
                    // Aggiorna pizza
                    btnSend.Enabled = true;
                    txtId.Enabled = true;
                    txtName.Enabled = false;
                    txtPrice.Enabled = true;
                    txtCategory.Enabled = false;
                    break;
                case 5:
                    // Cerca pizza
                    btnSend.Enabled = true;
                    txtId.Enabled = false;
                    txtName.Enabled = false;
                    txtPrice.Enabled = true;
                    txtCategory.Enabled = true;
                    break;
                default:
                    btnSend.Enabled = false;
                    txtId.Enabled = false;
                    txtName.Enabled = false;
                    txtPrice.Enabled = false;
                    txtCategory.Enabled = false;
                    break;
            }
        }
    }
}
