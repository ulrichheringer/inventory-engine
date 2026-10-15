# inventory-engine

A small inventory engine and UI demo with slots, stack limits, weight, and partial transfers.

## Run

Requires the .NET 10 SDK. Browser builds also need the WebAssembly workload: `dotnet workload install wasm-tools`.

### Desktop

~~~sh
dotnet run --project InventoryEngine.Desktop/InventoryEngine.Desktop.csproj
~~~

### Browser

~~~sh
dotnet run --project InventoryEngine.Browser/InventoryEngine.Browser.csproj
~~~

## Example

Move five items from the backpack to the chest.
