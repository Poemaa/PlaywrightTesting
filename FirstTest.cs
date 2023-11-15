using System;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace ProjectQualityTesting;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FirstTest : PageTest
{
    [Test]
    public async Task TestingAdminServices()
    {
        //Navigimi tek url e aplikacionit qe e testojme dhe klikimi i pjeses se Log In

        await Page.GotoAsync("https://localhost:7106/");
        var locator = Page.GetByRole(AriaRole.Link, new() { Name = "Log In" });
        await locator.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.Load);

        //Detektimi dhe mbushja e fushave te kerkuara me nje email dhe password valid

        await Page.GetByLabel("Email").FillAsync("admin@gmail.com");
        await Page.GetByLabel("Password").FillAsync("Admin123.");

        //Klikimi i chechbox per te ruajtur kredencialet edhe per hera te tjera

        await Page.GetByRole(AriaRole.Checkbox, new() { Name = "Remember me" }).CheckAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

        //Pas Log in te sukesshem, navigohet nga Homepage ne pjesen ku shfaqet lisa e fakulteteve

        await Page.GetByRole(AriaRole.Link, new() { Name = "Shiko Fakultetet" }).ClickAsync();

        //Editimi i nje fakulteti specifik duke nderruar emailin dhe institutin perkates si dhe ruajtja e ndryshimeve

        await Page.GetByRole(AriaRole.Link, new() { Name = "Edit" }).Nth(3).ClickAsync();
        await Page.GetByLabel("Email").FillAsync("mar@ubt.com");
        await Page.GetByLabel("InstitutiId").SelectOptionAsync("1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

    }
}