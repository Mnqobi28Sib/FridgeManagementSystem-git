using FridgeManagementSystem.Models;

namespace FridgeManagementSystem.Interfaces
{
    public interface IFridgeService
    {
        Task<Fridge> GetFridgeByIdAsync(int fridgeId);
        Task<IEnumerable<Fridge>> GetAllFridgesAsync(int customerId);
        Task AddFridgeAsync(Fridge fridge);
        Task UpdateFridgeAsync(Fridge fridge);
        Task DeleteFridgeAsync(int fridgeId);
    }
}
