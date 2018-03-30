using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Onboarding.ViewModel;
using System;

namespace Onboarding.JsonFormatter
{
    public class PolymorphicRepresentativeViewModelConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RepresentativeViewModel);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            RepresentativeViewModel representative;

            if (obj["discriminator"] != null && obj["discriminator"].ToString() == "RepresentativePerson")
            {
                representative = new RepresentativePersonViewModel();
            }
            else if (obj["discriminator"] != null && obj["discriminator"].ToString() == "RepresentativeCompany")
            {
                representative = new RepresentativeCompanyViewModel();
            }
            else
            {
                representative = new RepresentativeViewModel();
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
