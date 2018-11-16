# Check Out Order Total Kata API
This is an API for a grocery point-of-sales system. A user is able to add items to the grocery store, create coupons and promotions, and add items to cart to see total price reflected based on coupons and items added.

## Requirements
 * Your solution should have an API to configure the above kinds of prices, specials and markdowns. Instead of UPC or SKU codes, you may simply use strings such as "ground beef" or "soup" to describe what has been scanned or entered.
 * It should repeatedly accept a scanned item or item and weight through an API call. It must keep an accurate current total through the process.
 * Clerks make mistakes. They need to be able to remove items from an order, immediately correcting the current total.
 * You are not responsible for the display system or printing a receipt; only calculating the pre-tax total. The display system or receipt may query your code for the total.
 * Your solution must not use a database. Everything will fit in memory.

## Use Cases
 1. Accept a scanned item. The total should reflect an increase by the eaches price after the scan. You will need a way to configure the prices of scannable items prior to being scanned.
 2. Accept a scanned item and a weight. The total should reflect an increase of the price of the item for the given weight.
 3. Support a markdown. A marked-down item will reflect the eaches cost less the markdown when scanned. A weighted item with a markdown will reflect that reduction in cost per unit.
 4. Support a special in the form of "Buy N items get M at %X off." For example, "Buy 1 get 1 free" or "Buy 2 get 1 half off."
 5. Support a special in the form of "N for $X." For example, "3 for $5.00"
 6. Support a limit on specials, for example, "buy 2 get 1 free, limit 6" would prevent getting a third free item.
 7. Support removing a scanned item, keeping the total correct after possibly invalidating a special.
 8. Support "Buy N, get M of equal or lesser value for %X off" on weighted items.



## Build Commands
 1. Run "dotnet restore" command in Package Manager Console
 2. Start via Visual Studio.
 
## Notes
 * Logs will be generated in ..\logs\logYYYYMMDD.txt
 
## Sources
 - Caching - https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-2.1
 - Swagger - https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.1&tabs=visual-studio
 - NuGet Package Restore - https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore
 - Serilog - https://github.com/serilog/serilog/wiki/Getting-Started
 - Unit Testing Example - https://code-maze.com/unit-testing-aspnetcore-web-api/
 - Global Exception Handling - https://code-maze.com/global-error-handling-aspnetcore/
