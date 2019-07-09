using WinningGroup.Models;

namespace WinningGroup.Repository
{
    public interface IProductRepository
    {
        Product GetProductById(int id);
        string AddProduct();
    }
}
