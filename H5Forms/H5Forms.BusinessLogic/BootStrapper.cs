using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using H5Forms.Dtos.Converters;
using H5Forms.Dtos.Form.Controls;
using H5Forms.EfRepository;
using H5Forms.Entities;
using H5Forms.Entities.Form;
using Newtonsoft.Json;

namespace H5Forms.BusinessLogic
{
    public static class BootStrapper
    {
        public static void BootStrap()
        {
            Mapper.CreateMap<Form, Dtos.Form.Form>()
                .ForMember(dest => dest.Controls, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Controls) ? new List<Control>() : JsonConvert.DeserializeObject<List<Control>>(src.Controls, new JsonConverter[] { new ControlConverter(), new ValidationConverter()})));
            Mapper.CreateMap<Dtos.Form.Form, Form>()
                .ForMember(dest => dest.Controls, opt => opt.MapFrom(src => src.Controls == null ? null : JsonConvert.SerializeObject(src.Controls)));
            Mapper.CreateMap<Form, Dtos.Form.BasicForm>();

            Mapper.CreateMap<User, Dtos.User>();
            Mapper.CreateMap<Dtos.User, User>();            

            Database.SetInitializer<H5FormsContext>(new DbInitializer());

        }
    }
}
