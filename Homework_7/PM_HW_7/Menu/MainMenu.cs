namespace PM_HW_7.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Logging;
    using Services;
    
    /// <summary>
    /// Main menu.
    /// </summary>
    internal class MainMenu : IMainMenu
    {
        private readonly IRequestPerformer _performer;
        private readonly IOptionsSource _optionsSource;
        private readonly ILogger _logger;
        
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options">Options source</param>
        /// <param name="performer">Request performer.</param>
        /// <param name="logger">Logger implementation.</param>
        public MainMenu(
            IRequestPerformer performer, 
            IOptionsSource options, 
            ILogger logger)
        {
            _performer = performer;
            _optionsSource = options;
            _logger = logger;
        }
        
        /// <summary>
        /// Starting Async performing tasks
        /// </summary>
        /// <returns> int</returns>
        public async Task<int> StartAsync()
        {
            Console.WriteLine("Volokhovych, SOLID");
            
            try
            {
                var options = await _optionsSource.GetOptionsAsync();

                /*var tasks = options.Select(opt
                    => _performer.PerformRequestAsync(opt.Item1, opt.Item2)).ToArray();*/
                
                var tasksDone = await Task.WhenAll(options.Select(opt
                    => _performer.PerformRequestAsync(opt.Item1, opt.Item2)).ToArray());
                
                
                Console.WriteLine("Well,That's it. Done!");
                return 0;
            }
            catch (PerformException performException)
            {
                Console.WriteLine($"Occured exception {performException}");
                _logger.Log(performException.ToString());
                return -1;
            }
        }
        /// <summary>
        /// Method to check whether all booleans are true
        /// </summary>
        /// <param name="booleans"></param>
        /// <returns>bool</returns>
        [Obsolete("Method IsAllComplete is not used anymore.")]
        private static bool IsAllComplete(IEnumerable<bool> booleans)
        {
            return booleans.All(t => t);
        }
    }
}
