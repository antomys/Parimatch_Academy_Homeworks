namespace PM_HW_7.Models.Impl
{
    using System;
    using System.Text.Json.Serialization;
    internal class RequestOptions : IRequestOptions, IResponseOptions
    {
        /// <inheritdoc/>
        [JsonPropertyName("name")] public string Name { get; set; }
        
        [JsonPropertyName("address")] public string Address { get;set; }
        
        [JsonPropertyName("method")] public string MethodFromJson { get;set; }
        
        [JsonPropertyName("contentType")] public string ContentType { get;set; }
        
        [JsonPropertyName("body")] public string Body { get;set; }
        
        [JsonPropertyName("path")] public string Path { get;set; }

        public bool IsValid => Validate();
        
        public RequestMethod Method { get; private set; }
        
        #region PrivateValidation
        /// <summary>
        /// Method to validate fields of deserialized JSON
        /// </summary>
        /// <returns>bool</returns>
        private bool Validate()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Path) && !ValidateLink() && !ValidateRequest();
        }

        private bool ValidateLink()
        {
            return !Uri.IsWellFormedUriString(Address, UriKind.Absolute);
        }

        private bool ValidateRequest()
        {
            if (!Enum.TryParse(typeof(RequestMethod), MethodFromJson, true, out var method)) return true;
            if (method != null) Method = (RequestMethod) method;
            
            return false;
        }
        

        #endregion
        
    }
}