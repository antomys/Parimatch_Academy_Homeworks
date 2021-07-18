namespace PM_HW_7.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    
    /// <summary>
    /// Options source.
    /// </summary>
    internal interface IOptionsSource
    {
        /// <summary>
        /// Provides request-response option pairs.
        /// </summary>
        /// <returns>Returns options enumeration.</returns>
        public Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetOptionsAsync();
    }
}
