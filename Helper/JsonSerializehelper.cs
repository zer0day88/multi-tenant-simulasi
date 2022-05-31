using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Utility.JSON.Helper
{
    public class IgnoredProperties
    {
        public IgnoredProperties(string property_name)
        {
            this.property_name = property_name;
        }
        public string property_name { get; set; }
    }
    
    //todo: still in development don't use :)
    public class DynamicContractResolver : DefaultContractResolver
    {
        private readonly List<IgnoredProperties> _ignoredProperties;

        public DynamicContractResolver(List<IgnoredProperties> ignoredProperties)
        {
            _ignoredProperties = ignoredProperties;
        }
        

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            var data = properties.Where(p =>
                _ignoredProperties.All(i => 
                    p.PropertyName== i.property_name)).ToList();

            return data;
        }
    }
}