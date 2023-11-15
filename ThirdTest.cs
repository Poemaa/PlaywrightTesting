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
public class ThirdTest : PageTest
{
    [Test]
    public async Task TestingRegister()
    {
        //Navigimi tek url e aplikacionit qe e testojme dhe klikimi i pjeses se Register

        await Page.GotoAsync("https://localhost:7106/");
        var locator = Page.GetByRole(AriaRole.Link, new() { Name = "Register" });
        await locator.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.Load);

        //Klikimi i butonit Register dhe testimi i tij me fusha te zbrazeta

        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

        //Rishkrimi i te dhenave pas validimit se nuk duhet te kete fusha te zbrazeta
        //Emri/Mbiemri
        await Page.GetByLabel("Emri", new() { Exact = true }).FillAsync("NewUser1");
        await Page.GetByLabel("Mbiemri").FillAsync("Test");

        //Data e Lindjes
        var BirthdaySelector = "[data-testid=datta1]";
        var dateToType = "01-11-2023";
        await Page.TypeAsync(BirthdaySelector, dateToType);
        var typedDate = await Page.EvalOnSelectorAsync<string>(
            BirthdaySelector,
            "input => input.value" );

        //Zgjedhja e Fakultetit
        await Page.GetByLabel("IdentifikimiFakultetit").FillAsync("2");

        //Data e Fillimit te Studimeve
        var StartStudiesSelector = "[data-testid=datta2]";
        var dateToType2 = "09-11-2023";
        await Page.TypeAsync(StartStudiesSelector, dateToType2);
        var typedDate2 = await Page.EvalOnSelectorAsync<string>(
            StartStudiesSelector,
            "input => input.value");

        //Mesatarja e notes
        await Page.GetByLabel("MesatarjaNotes").FillAsync("8.6");

        //Email dhe Password + butoni Register
        await Page.GetByLabel("Email").FillAsync("newuser1@gmail.com");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("New123.");
        await Page.GetByLabel("Confirm Password").FillAsync("New123.");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

        //Klikimi tek profili per te derguar emailin per verifikim dhe log out
        await Page.GetByRole(AriaRole.Link, new() { Name = "Hello newuser1@gmail.com" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Email" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Send verification email" }).ClickAsync();
        Thread.Sleep(2000);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Logout" }).ClickAsync();

    }
}
