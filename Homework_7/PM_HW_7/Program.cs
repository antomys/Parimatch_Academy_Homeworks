namespace PM_HW_7
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Logging;
    using Menu;
    using Services.Impl;
    
    
    /// <summary>
    /// Entry point.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <returns>Returns exit code.</returns>
        private static async Task<int> Main()
        {
            try
            {
                var requestHandler = new RequestHandler(new HttpClient());
                var responseHandler = new ResponseHandler();
                var logger = new Logger();
                
                var performer = new RequestPerformer(requestHandler,responseHandler,logger);
                var options = new OptionsSource("options.json");
                
                
                var mainMenu = new MainMenu(performer, options, logger);
                return await mainMenu.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Critical unhandled exception");
                Console.WriteLine(ex);
                return -1;
            }
        }
    }
}
