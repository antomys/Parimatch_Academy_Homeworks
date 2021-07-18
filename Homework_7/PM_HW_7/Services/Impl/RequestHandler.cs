namespace PM_HW_7.Services.Impl
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
    using Models;
    using PM_HW_7.Models.Impl;
    internal class RequestHandler : IRequestHandler
    {
        private readonly HttpClient _httpClient;
        private string _responseBody;
        
        public RequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        /// <inheritdoc/>
        public async Task<IResponse> HandleRequestAsync(IRequestOptions requestOptions)
        {
            if (requestOptions == null) throw new ArgumentNullException(nameof(requestOptions));

            if (!requestOptions.IsValid) throw new ArgumentOutOfRangeException(nameof(requestOptions));
            
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            var stringContent = 
                string.IsNullOrEmpty(requestOptions.Body) ? new StringContent("") : new StringContent(requestOptions.Body);
            

            try
            {
                var response = requestOptions.Method switch
                {
                    RequestMethod.Undefined => throw new InvalidOperationException(nameof(requestOptions.Method)),

                    RequestMethod.Get => await _httpClient.GetAsync(requestOptions.Address,
                        cancellationTokenSource.Token),

                    RequestMethod.Post => await _httpClient.PostAsync(requestOptions.Address, stringContent,
                        cancellationTokenSource.Token),

                    RequestMethod.Put => await _httpClient.PutAsync(requestOptions.Address, stringContent,
                        cancellationTokenSource.Token),

                    RequestMethod.Patch => await _httpClient.PatchAsync(requestOptions.Address, stringContent,
                        cancellationTokenSource.Token),

                    RequestMethod.Delete => await _httpClient.DeleteAsync(requestOptions.Address,
                        cancellationTokenSource.Token),
                    _ => throw new PerformException(nameof(requestOptions.Method))
                };
                
                _responseBody = response.IsSuccessStatusCode
                    ? await response.Content.ReadAsStringAsync(cancellationTokenSource.Token)
                    : string.Empty;

                return new Response(true, (int) response.StatusCode, _responseBody);
            }
            catch (TaskCanceledException)
            {
                return new Response(false, 0, string.Empty);
            }
        }
    }
}