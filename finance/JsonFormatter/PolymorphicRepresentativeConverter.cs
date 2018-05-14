using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using finance.ViewModels.Invoices;
using System;

namespace finance.JsonFormatter
{
    public class PolymorphicRepresentativeViewModelConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Representative);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            Representative representative;

            if ((obj["discriminator"] != null && obj["discriminator"].ToString() == "RepresentativePerson")
                || (obj["Discriminator"] != null && obj["Discriminator"].ToString() == "RepresentativePerson"))
            {
                representative = new RepresentativePerson();
            }
            else if ((obj["discriminator"] != null && obj["discriminator"].ToString() == "RepresentativeCompany")
                || (obj["Discriminator"] != null && obj["Discriminator"].ToString() == "RepresentativeCompany"))
            {
                representative = new RepresentativeCompany();
            }
            else
            {
                representative = new Representative();
            }

            serializer.Populate(obj.CreateReader(), representative);

            return representative;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
