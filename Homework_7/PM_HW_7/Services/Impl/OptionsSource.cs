namespace PM_HW_7.Services.Impl
{
    using System.Text.Json;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PM_HW_7.Models.Impl;
    using Models;
    using Exceptions;
    internal class OptionsSource : IOptionsSource
    {
        private readonly string _fileName;
        public OptionsSource(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Gets options from gives .json file (ASYNC)
        /// </summary>
        /// <returns>options</returns>
        /// <exception cref="InvalidDataException"> If data that was read is invalid or empty</exception>
        public async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetOptionsAsync()
        {
            try
            {
                await using var fs = new FileStream(_fileName, FileMode.Open);
                var requestOptions = await JsonSerializer.DeserializeAsync<List<RequestOptions>>(fs);

                if (requestOptions == null) throw new InvalidDataException(nameof(_fileName));

                var option = requestOptions
                    .Select(op => ((IRequestOptions) op, (IResponseOptions) op));

                return option;

            }
            catch (PerformException)
            {
                return null;
            }
        }
    }
}