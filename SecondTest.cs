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
public class SecondTest : PageTest
{
    [Test]
    public async Task TestingFeedback()
    {
        //Navigimi tek url e aplikacionit qe e testojme dhe klikimi i pjeses se Log In

        await Page.GotoAsync("https://localhost:7106/");
        var locator = Page.GetByRole(AriaRole.Link, new() { Name = "Log In" });
        await locator.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.Load);

        //Detektimi dhe mbushja e fushave te kerkuara me nje email dhe password valid

        await Page.GetByLabel("Email").FillAsync("simpleUser@gmail.com");
        await Page.GetByLabel("Password").FillAsync("User123.");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

        //Pas Log in te sukesshem, navigohet nga Homepage ne pjesen per shkrimin e feedback

        await Page.GetByRole(AriaRole.Link, new() { Name = "Shkruaj Feedback" }).ClickAsync();

        //Shkrimi i permbajtjes
        await Page.GetByLabel("Permbajtja").FillAsync("I had a great experience studying Computer Science at UBT! " +
            "We learned a lot of interesting things and did many important courses.");
     
        //Selektimi i dates
        var dateInputSelector = "[data-testid=datta]";
        var dateToType = "09-11-2023";
        await Page.TypeAsync(dateInputSelector, dateToType);

        var typedDate = await Page.EvalOnSelectorAsync<string>(
            dateInputSelector,
            "input => input.value");

        // Selektimi i fakultetit dhe institucionit
        await Page.GetByLabel("FakultetiId").SelectOptionAsync("1");
        await Page.GetByLabel("InstitutiId").SelectOptionAsync("1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();

    }
}