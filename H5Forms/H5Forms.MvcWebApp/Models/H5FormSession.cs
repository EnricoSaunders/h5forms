using H5Forms.Dtos.Form;
using H5Forms.Infrastructure;

namespace H5Forms.MvcWebApp.Models
{
    public class H5FormSession : SessionInfo<H5FormSession>
    {
        public Form Form { get; set; }
    }
}