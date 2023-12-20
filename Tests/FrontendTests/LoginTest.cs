namespace Tests.FrontendTests;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginTest : PageTest
{
    [Test]
    public async Task ShouldBeAbleToLoginAndHaveProfil()
    {
        await Page.GotoAsync("http://localhost:4200/home");

        await Page.GetByLabel("menu-icon").ClickAsync();

        await Page.GetByText("Login / Logout").ClickAsync();

        await Page.GetByLabel("Email").ClickAsync();

        await Page.GetByLabel("Email").FillAsync("cat@man.dk");

        await Page.GetByLabel("Password").ClickAsync();

        await Page.GetByLabel("Password").FillAsync("12345678!");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        await Page.Locator("ion-button").Filter(new() { HasText = "PROFILE" }).Locator("button").ClickAsync();

        await Expect(Page.Locator("app-recipe-profile")).ToContainTextAsync("Catman");

    }
    
    [Test]
    public async Task ShouldBeAbleToLoginAndOut()
    {
        await Page.GotoAsync("http://localhost:4200/home");

        await Page.GetByLabel("menu-icon").ClickAsync();

        await Page.GetByText("Login / Logout").ClickAsync();

        await Page.GetByLabel("Email").ClickAsync();

        await Page.GetByLabel("Email").FillAsync("Gunnar@dungeon.com");

        await Page.GetByLabel("Password").ClickAsync();

        await Page.GetByLabel("Password").FillAsync("12345678!");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "PROFILE" }).ClickAsync();

        await Page.GetByLabel("menu-icon").ClickAsync();

        await Page.GetByText("Login / Logout").ClickAsync();

    }
}

