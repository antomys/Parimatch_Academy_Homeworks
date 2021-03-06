namespace PM_HW_7.Services
{
    using System;
    using System.Threading.Tasks;
    using Models;
    
    /// <summary>
    /// Response handler.
    /// </summary>
    internal interface IResponseHandler
    {
        /// <summary>
        /// Response handler.
        /// </summary>
        /// <param name="response">Required response.</param>
        /// <param name="requestOptions">Required source request options.</param>
        /// <param name="responseOptions">Required response options.</param>
        /// <returns>Returns awaiter.</returns>
        /// <exception cref="ArgumentNullException">One of required parameters are missing.</exception>
        Task HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions);
    }
}
