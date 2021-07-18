namespace PM_HW_7.Services.Impl
{ 
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Models;
    internal class ResponseHandler : IResponseHandler
    {
        /// <inheritdoc/>
        public async Task HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            if (requestOptions == null) throw new ArgumentNullException(nameof(requestOptions));

            if (!requestOptions.IsValid) throw new ArgumentOutOfRangeException(nameof(requestOptions));

            await File.WriteAllTextAsync(responseOptions.Path, response.ToString());
        }
    }
}