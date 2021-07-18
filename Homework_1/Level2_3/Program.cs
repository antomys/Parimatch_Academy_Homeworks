namespace Level2_3
{
    using Menu;
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var menu = new MainMenu(args);
            
            if (args == null || args.Length == 0)
            {
                menu.DialogMode();
            }
            else
            {
                menu.ConsoleMode(args);
            }
            
            return 0;
        }
    }
}