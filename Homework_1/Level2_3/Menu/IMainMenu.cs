namespace Level2_3.Menu
{
    internal interface IMainMenu
    {
        /// <summary>
        /// Shows main menu in standard mode
        /// </summary>
        /// <returns></returns>
        int DialogMode();

        /// <summary>
        /// Shows main menu in console compile mode through dotnet run
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        int ConsoleMode(string[] args);
    }
}