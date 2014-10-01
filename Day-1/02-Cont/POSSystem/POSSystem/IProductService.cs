namespace POSSystem
{
    public interface IProductService
    {
        Product GetProductInfo(string manufacturerCode, string productCode);
    }
}