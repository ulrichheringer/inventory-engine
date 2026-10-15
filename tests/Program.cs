using GameInventory = InventoryEngine.Inventory.Inventory;
using InventoryEngine.Inventory;

var ore = new ItemDefinition("ore", "Ore", 10, 0.5);
var bag = new GameInventory(2); var chest = new GameInventory(2);
if (bag.Add(ore, 25) != 20 || bag.Slots[0]?.Count != 10 || bag.Slots[1]?.Count != 10) throw new Exception("Stacking or capacity failed.");
if (bag.TransferTo(0, 7, chest) != 7 || bag.Slots[0]?.Count != 3 || chest.Slots[0]?.Count != 7) throw new Exception("Transfer failed.");
Console.WriteLine("Inventory checks passed.");
