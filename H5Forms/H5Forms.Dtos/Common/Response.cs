namespace H5Forms.Dtos.Common
{
    public class Response<T>
    {
        public Result Result { get; set; }

        public T Data { get; set; }
    }
}
