using FridgeManagementSystem.Data;
using FridgeManagementSystem.Interfaces;
using FridgeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeManagementSystem.Services
{
    public class FridgeService : IFridgeService
    {
        private readonly ApplicationDbContext _context;

        public FridgeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Fridge> GetFridgeByIdAsync(int fridgeId)
        {
            return await _context.Fridges.FindAsync(fridgeId);
        }

        public async Task<IEnumerable<Fridge>> GetAllFridgesAsync(int customerId)
        {
            return await _context.Fridges
                .Where(f => f.CustomerId == customerId) // Assuming Fridge has a CustomerId property
                .ToListAsync();
        }

        public async Task AddFridgeAsync(Fridge fridge)
        {
            await _context.Fridges.AddAsync(fridge);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFridgeAsync(Fridge fridge)
        {
            _context.Fridges.Update(fridge);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFridgeAsync(int fridgeId)
        {
            var fridge = await _context.Fridges.FindAsync(fridgeId);
            if (fridge != null)
            {
                _context.Fridges.Remove(fridge);
                await _context.SaveChangesAsync();
            }
        }
    }
}
