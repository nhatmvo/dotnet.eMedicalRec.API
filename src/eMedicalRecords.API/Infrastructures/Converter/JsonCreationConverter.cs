using System;
using System.Collections.Generic;
using eMedicalRecords.API.Applications.Commands.Template;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eMedicalRecords.API.Infrastructures.Converter
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jObject);
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                JArray jArray = JArray.Load(reader);
                List<T> list = new List<T>();
                foreach (var token in jArray)
                {
                    var jObject = token as JObject;
                    T target = Create(objectType, jObject);
                    serializer.Populate(jObject.CreateReader(), target);
                    list.Add(target);
                }
                return list;
            }
            else
            {
                JObject jObject = JObject.Load(reader);
                T target = Create(objectType, jObject);
                serializer.Populate(jObject.CreateReader(), target);
                return target;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}