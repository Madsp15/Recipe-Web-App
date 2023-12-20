namespace Tests.FrontendTests;

using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class RecipeTests : PageTest
{
   
      [Test]
    public async Task ShouldCreateAndDeleteRecipe()
    {
        await Page.GotoAsync("http://localhost:4200/home");

        await Page.GetByLabel("menu-icon").ClickAsync();

        await Page.GetByText("Login / Logout").ClickAsync();

        await Page.GetByLabel("Email").ClickAsync();

        await Page.GetByLabel("Email").ClickAsync();

        await Page.GetByLabel("Email").FillAsync("test@mail.dk");

        await Page.GetByLabel("Password").ClickAsync();

        await Page.GetByLabel("Password").FillAsync("12345678!");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        await Page.GetByLabel("menu-icon").ClickAsync();

        await Page.GetByText("Create new Recipe").ClickAsync();

        await Page.Locator("ion-backdrop").ClickAsync();

        await Page.GetByLabel("Recipe Name:").ClickAsync();

        await Page.GetByLabel("Recipe Name:").FillAsync("test recipe");

        await Page.GetByLabel("Write a short description of").ClickAsync();

        await Page.GetByLabel("Write a short description of").FillAsync("test");

        await Page.GetByLabel("Servings:").ClickAsync();

        await Page.GetByLabel("Servings:").FillAsync("2");

        await Page.GetByPlaceholder("Enter duration in minutes").ClickAsync();

        await Page.GetByPlaceholder("Enter duration in minutes").FillAsync("2");
        
        await Page.GetByText("MinutesHoursDuration:").ClickAsync();

        await Page.GetByRole(AriaRole.Radio, new() { Name = "Hours" }).ClickAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();

        await Page.Locator("input[type=\"file\"]").ClickAsync();

        await Page.Locator("input[type=\"file\"]").SetInputFilesAsync(new[] { "17tootired-grilled-cheese-articleLarge.png" });

        await Page.GetByLabel("Add Tags:").ClickAsync();

        await Page.GetByLabel("Add Tags:").FillAsync("test");

        await Page.GetByLabel("Add Tags:").PressAsync("Enter");

        await Page.GetByLabel("Add Instruction:").ClickAsync();

        await Page.GetByLabel("Add Instruction:").FillAsync("mix");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Add Instruction" }).ClickAsync();

        await Page.GetByLabel("Add Instruction:").ClickAsync();

        await Page.GetByLabel("Add Instruction:").FillAsync("cook");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Add Instruction" }).ClickAsync();

        await Page.GetByLabel("Quantity:").ClickAsync();

        await Page.GetByLabel("Quantity:").FillAsync("2");

        await Page.GetByLabel("Select unit, Unit:").ClickAsync();

        await Page.GetByRole(AriaRole.Radio, new() { Name = "kg" }).ClickAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();

        await Page.GetByLabel("Ingredient:").ClickAsync();

        await Page.GetByLabel("Ingredient:").FillAsync("Rice");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Add Ingredient" }).ClickAsync();

        await Page.GetByLabel("Quantity:").ClickAsync();

        await Page.GetByLabel("Quantity:").FillAsync("6");

        await Page.GetByLabel("Select unit, Unit:").ClickAsync();

        await Page.GetByRole(AriaRole.Radio, new() { Name = "g", Exact = true }).ClickAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();

        await Page.GetByLabel("Ingredient:").ClickAsync();

        await Page.GetByLabel("Ingredient:").FillAsync("Meat");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Add Ingredient" }).ClickAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "Save Recipe" }).ClickAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "PROFILE" }).ClickAsync();

        await Expect(Page.Locator("app-recipe-profile")).ToContainTextAsync("test");

        await Expect(Page.Locator("app-recipe-profile")).ToContainTextAsync("test recipe");

        await Page.GetByRole(AriaRole.Img, new() { Name = "Delete" }).Nth(1).ClickAsync();

    }

}
