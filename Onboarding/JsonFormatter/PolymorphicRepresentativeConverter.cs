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

            var pt = obj["cpf"];

            if (obj["cpf"] != null)
            {
                representative = new RepresentativePersonViewModel();
            }
            else if (obj["cnpj"] != null)
            {
                representative = new RepresentativeCompanyViewModel();
            }
            else
            {
                throw new NotSupportedException("Não é possível converter.");
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
