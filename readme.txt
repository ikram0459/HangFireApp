https://www.youtube.com/watch?v=bK1NC6L4pm8&ab_channel=BlakeConnally [setup #hangfire event queue system on .netcore]
	create new asp.net core webapi empty app with https.
	goto Nuget: install package HangFire by Sergey Odinokov
	Add these lines in Startup.cs
		    public class Startup
		    {
			public void ConfigureServices(IServiceCollection services)
			{	//windows auth #connectionstring
			    services.AddHangfire(x => x.UseSqlServerStorage("Server=.;Initial Catalog=HangFireDB;Persist Security Info=True; Integrated Security=SSPI;MultipleActiveResultSets=True;")); 			
			    services.AddHangfireServer();
			}
			
			public void Configure(IApplicationBuilder app)
			{
			    app.UseHangfireDashboard();
			}
		    }
	Setup DB in sqlserver- Create new db name it HangFireDB
	Run App button says IISExpress, change that to HangFireApp and run it

	It would open up console with some logs, it will create DB with tables and open up browser
	https://localhost:5001/hangfire [go here to see hangfire console]

	https://www.youtube.com/watch?v=LgjDPL5h-94&ab_channel=BlakeConnally [add events in hangfire event queue system on .netcore]
		Add new project in above solution called - INService of type (.net core class library)
		Add folder Services and create this interface and serviceclass INService.cs
		    public interface IINService
		    {
			void RunTask();
		    }
		    public class INService : IINService
		    {
			public void RunTask()
			{
			    Console.WriteLine("This task is run from INService");
			}
		    }
		Register your service in IoC in Startup.cs in ConfigureService() by referencing above new classlib project
			services.AddScoped<IINService, INService.Services.INService>();
		HangFireApp>Create new MVC Empty controller called HangFireController.cs
			    public class HangFireController : Controller
			    {
				private readonly IINService _inService;
				public HangFireController(IINService inservice)
				{
				    this._inService = inservice;
				}
				[Route("Api/HangFire")]
				public IActionResult Index()
				{
				    this._inService.RunTask();
				    return View();
				}
			    }
		https://docs.hangfire.io/en/latest/getting-started/index.html [HangFire ecron functions here]
			https://docs.hangfire.io/en/latest/background-methods/performing-recurrent-tasks.html

			[Route("Api/HangFire")]	//Minutely cronjob can be added like this.
			public IActionResult Index()
			{
			    RecurringJob.AddOrUpdate("TestJob1", () => this._inService.RunTask(), Cron.Minutely);
			    return Ok();
			}