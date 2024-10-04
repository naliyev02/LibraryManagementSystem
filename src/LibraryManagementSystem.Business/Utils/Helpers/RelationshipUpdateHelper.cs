using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.Business.Utils.Helpers;

public static class RelationshipUpdateHelper
{
    public static async Task UpdateManyToManyAsync<T, TDto, TRepository> (IEnumerable<T> existingItems, IEnumerable<TDto> newItemDtos, Func<T, int> getItemId, Func<TDto, int> getDtoItemId, Func<int, T> createNewItem, TRepository repository) where TRepository : IGenericRepository<T>
    {
        var existingItemIds = existingItems.Select(getItemId).ToList();
        var newItemIds = newItemDtos.Select(getDtoItemId).ToList();

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
