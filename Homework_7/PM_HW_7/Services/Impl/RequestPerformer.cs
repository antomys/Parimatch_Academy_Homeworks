namespace PM_HW_7.Services.Impl
{
    using System;
    using System.Threading.Tasks;
    using Logging;
    using Models;
    
    /// <summary>
    /// Request performer.
    /// </summary>
    internal class RequestPerformer : IRequestPerformer
    {
        private readonly IRequestHandler _requestHandler;
        private readonly IResponseHandler _responseHandler;
        private readonly ILogger _logger;
        
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="requestHandler">Request handler implementation.</param>
        /// <param name="responseHandler">Response handler implementation.</param>
        /// <param name="logger">Logger implementation.</param>
        public RequestPerformer(
            IRequestHandler requestHandler, 
            IResponseHandler responseHandler,
            ILogger logger)
        {
            _requestHandler = requestHandler;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> PerformRequestAsync(IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            try
            {
                if (requestOptions == null) throw new ArgumentNullException(nameof(requestOptions));

                if (!requestOptions.IsValid)
                {
                    Console.WriteLine($"Filtered invalid request: {requestOptions.Name}");
                    throw new ArgumentOutOfRangeException(nameof(requestOptions));
                }
                
                var response = await _requestHandler.HandleRequestAsync(requestOptions); //Todo: wrong content
                _logger.Log($"Proceeded and granted response! {response.Code}");
                await _responseHandler.HandleResponseAsync(response,requestOptions,responseOptions);
                
                return true;
            }
            catch (Exception exception)
            {
                _logger.Log(exception,exception.Message);
                return false;
            }
        }
    }
}
