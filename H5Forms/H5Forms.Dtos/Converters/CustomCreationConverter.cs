using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace H5Forms.Dtos.Converters
{
    public class CustomCreationConverter<A, T> : CustomCreationConverter<A> where T : A, new()
    {
        public override A Create(Type objectType)
        {
            return new T();
        }
    }
}
