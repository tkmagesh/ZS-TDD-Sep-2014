namespace POSSystem
{
    public interface ISaleEventListener
    {
        void NewSaleInitialized();
        void EndSale();
        void AddProduct(string barCode, int quantity);
    }
}