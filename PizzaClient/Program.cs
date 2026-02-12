namespace PizzaClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AttendiServer();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormPizza());
        }

        static void AttendiServer()
        {
            const string URL = "http://localhost:5000";

            using var client = new HttpClient();

            client.Timeout = TimeSpan.FromSeconds(2);

            bool serverOnline = false;

            while (!serverOnline)
            {
                try
                {
                    var resp = client.GetAsync($"{URL}/api/pizze").Result;

                    serverOnline = resp.IsSuccessStatusCode;
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}