using AutoMapper;
using H5Forms.Entities.Form;

namespace H5Forms.BusinessLogic
{
    public static class BootStrapper
    {
        public static void BootStrap()
        {
            Mapper.CreateMap<Form, Dtos.Form>();
        }
    }
}
