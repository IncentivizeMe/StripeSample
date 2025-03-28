using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using StripeBlazor.Components;

var builder = WebApplication.CreateBuilder(args);
StripeConfiguration.ApiKey = "API KEY";

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();


[Route("create-checkout-session")]
[ApiController]
public class CheckoutApiController : Controller
{
    [HttpPost]
    public ActionResult Create()
    {
        var domain = "http://localhost:4242";
        var options = new SessionCreateOptions
        {
            UiMode = "embedded",
            LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1R6u2VRUbWj2wCnFrrrh4dst",
                    Quantity = 1,
                  },
                },
            Mode = "payment",
            ReturnUrl = domain + "/return.html?session_id={CHECKOUT_SESSION_ID}",
        };
        var service = new SessionService();
        Session session = service.Create(options);

        return Json(new { clientSecret = session.ClientSecret });
    }
}

[Route("session-status")]
[ApiController]
public class SessionStatusController : Controller
{
    [HttpGet]
    public ActionResult SessionStatus([FromQuery] string session_id)
    {
        var sessionService = new SessionService();
        Session session = sessionService.Get(session_id);

        return Json(new { status = session.Status, customer_email = session.CustomerDetails.Email });
    }
}