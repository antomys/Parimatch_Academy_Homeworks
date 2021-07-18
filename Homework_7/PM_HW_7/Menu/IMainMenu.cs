using System.Threading.Tasks;

namespace PM_HW_7.Menu
{
    /// <summary>
    /// Main menu.
    /// Represents UI layer.
    /// </summary>
    internal interface IMainMenu
    {
        /// <summary>
        /// Main menu entry point.
        /// </summary>
        /// <returns>
        /// Returns status code.
        /// Zero for success.
        /// Non-zero for fail.
        /// </returns>
        Task<int> StartAsync();
    }
}
