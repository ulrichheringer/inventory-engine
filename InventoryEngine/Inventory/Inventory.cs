namespace InventoryEngine.Inventory;

public sealed record ItemDefinition(string Id, string Name, int MaxStack, double Weight);
public sealed record ItemStack(ItemDefinition Item, int Count);

public sealed class Inventory(int capacity)
{
    public int Capacity { get; } = capacity > 0 ? capacity : throw new ArgumentOutOfRangeException(nameof(capacity));
    public List<ItemStack?> Slots { get; } = Enumerable.Repeat<ItemStack?>(null, capacity).ToList();
    public double TotalWeight => Slots.Sum(s => s is null ? 0 : s.Count * s.Item.Weight);

    public int Add(ItemDefinition item, int amount)
    {
        var requested = Math.Max(0, amount);
        var remaining = requested;
        for (var i = 0; i < Slots.Count && remaining > 0; i++)
        {
            if (Slots[i] is not { } stack || stack.Item.Id != item.Id || stack.Count >= item.MaxStack) continue;
            var moved = Math.Min(remaining, item.MaxStack - stack.Count);
            Slots[i] = stack with { Count = stack.Count + moved }; remaining -= moved;
        }
        for (var i = 0; i < Slots.Count && remaining > 0; i++)
        {
            if (Slots[i] is not null) continue;
            var moved = Math.Min(remaining, item.MaxStack);
            Slots[i] = new(item, moved); remaining -= moved;
        }
        return requested - remaining;
    }

    public int TransferTo(int slotIndex, int amount, Inventory destination)
    {
        if (slotIndex < 0 || slotIndex >= Slots.Count || Slots[slotIndex] is not { } stack) return 0;
        var requested = Math.Min(Math.Max(0, amount), stack.Count);
        var moved = destination.Add(stack.Item, requested);
        if (moved == stack.Count) Slots[slotIndex] = null;
        else if (moved > 0) Slots[slotIndex] = stack with { Count = stack.Count - moved };
        return moved;
    }
}
