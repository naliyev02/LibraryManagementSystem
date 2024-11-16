using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.Business.Utilities.Helpers;

internal static class RelationshipUpdateHelper
{
    public static async Task UpdateManyToManyAsync<T, TRepository>(IEnumerable<T> existingItems, IEnumerable<int> newItemIds, Func<T, int> getItemId, Func<int, T> createNewItem, TRepository repository) where TRepository : IGenericRepository<T>
    {
        var existingItemIds = existingItems.Select(getItemId).ToList();

        var itemsToRemove = existingItems.Where(x => !newItemIds.Contains(getItemId(x))).ToList();
        var itemsToAdd = newItemIds.Where(id => !existingItemIds.Contains(id)).ToList();

        foreach (var itemToRemove in itemsToRemove)
        {
            repository.Delete(itemToRemove);
        }

        foreach (var itemId in itemsToAdd)
        {
            await repository.CreateAsync(createNewItem(itemId));
        }
    }
}
