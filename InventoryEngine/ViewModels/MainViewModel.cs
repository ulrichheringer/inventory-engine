using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using InventoryEngine.Inventory;
using GameInventory = InventoryEngine.Inventory.Inventory;

namespace InventoryEngine.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly GameInventory _bag = new(6);
    private readonly GameInventory _chest = new(6);
    private readonly ItemDefinition _ore = new("ore", "Minério", 10, 0.5);
    private readonly ItemDefinition _potion = new("potion", "Poção", 5, 0.2);
    [ObservableProperty] private int _slotIndex;
    [ObservableProperty] private int _amount = 5;
    [ObservableProperty] private string _bagContents = "";
    [ObservableProperty] private string _chestContents = "";
    [ObservableProperty] private string _status = "";

    public MainViewModel()
    {
        _bag.Add(_ore, 24); _bag.Add(_potion, 2); _chest.Add(_ore, 5);
        Refresh();
    }

    [RelayCommand]
    private void ToChest() { var moved = _bag.TransferTo(SlotIndex, Amount, _chest); Status = $"Transferidos {moved} itens da mochila para o baú."; Refresh(); }

    [RelayCommand]
    private void ToBag() { var moved = _chest.TransferTo(SlotIndex, Amount, _bag); Status = $"Transferidos {moved} itens do baú para a mochila."; Refresh(); }

    private void Refresh()
    {
        BagContents = Format(_bag); ChestContents = Format(_chest);
    }

    private static string Format(GameInventory inventory) => string.Join(Environment.NewLine,
        inventory.Slots.Select((stack, index) => $"{index}: {(stack is null ? "vazio" : $"{stack.Item.Name} × {stack.Count}")}"))
        + $"\nPeso: {inventory.TotalWeight:F1}";
}
