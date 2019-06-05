using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGenerator
{
    public class CustomContractResolver : DefaultContractResolver
    {
        private readonly IEnumerable<string> m_AllowedProps;

        public CustomContractResolver(IEnumerable<string> allowedProps)
        {
            m_AllowedProps = allowedProps;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var list = new List<JsonProperty>();
            foreach (string prop in m_AllowedProps)
            {
                if (type?.GetProperty(prop) == null || type.GetMember(prop).First() == null)
                    continue;

                var jsonProp = new JsonProperty
                {
                    PropertyName = prop,
                    PropertyType = type.GetProperty(prop).PropertyType,
                    Readable = true,
                    Writable = true,
                    ValueProvider = CreateMemberValueProvider(type.GetMember(prop).First())
                };

                list.Add(jsonProp);
            }

            return list;

            //List<JsonProperty> list = m_AllowedProps.Select(p => new JsonProperty
            //{
            //    PropertyName = p,
            //    PropertyType = type.GetProperty(p).PropertyType,
            //    Readable = true,
            //    Writable = true,
            //    ValueProvider = CreateMemberValueProvider(type.GetMember(p).First())
            //}).ToList();

            //return list;
        }
    }
}
