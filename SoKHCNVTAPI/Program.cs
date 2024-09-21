using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using QuestPDF.Infrastructure;
using Sentry;
using Serilog;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Extensions;
using SoKHCNVTAPI.Middleware;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Repositories;
using SoKHCNVTAPI.Repositories.CommonCategories;
using SoKHCNVTAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options => { options.AddServerHeader = false; });

// FOR PRODUCTION
builder.WebHost.UseIISIntegration();
builder.WebHost.UseIIS();
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());

// Logger
builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// ======== Services ========

// Add services to the container.
builder
    .Services
    .AddControllers(options => { options.ReturnHttpNotAcceptable = true; })
    .AddNewtonsoftJson()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errorList = context
                .ModelState
                .Where(x => x.Value!.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            // Custom response
            var result = new BadRequestObjectResult(new ErrorBaseResponse
            {
                Message = ReasonPhrases.GetReasonPhrase(StatusCodes.Status400BadRequest),
                Success = false,
                ErrorCode = 0,
                Errors = errorList
            });
            return result;
        };
    });

// Add Cache provider
builder
    .Services.AddEasyCaching(options =>
    {
        //use memory cache that named default
        options.UseInMemory("default");

        // // use memory cache with your own configuration
        // options.UseInMemory(config => 
        // {
        //     config.DBConfig = new InMemoryCachingOptions
        //     {
        //         // scan time, default value is 60s
        //         ExpirationScanFrequency = 60, 
        //         // total count of cache items, default value is 10000
        //         SizeLimit = 100 
        //     };
        //     // the max random second will be added to cache's expiration, default value is 120
        //     config.MaxRdSecond = 120;
        //     // whether enable logging, default is false
        //     config.EnableLogging = false;
        //     // mutex key's alive time(ms), default is 5000
        //     config.LockMs = 5000;
        //     // when mutex key alive, it will sleep some time, default is 300
        //     config.SleepMs = 300;
        // }, "m2");

        //use redis cache that named redis1
        // options.UseRedis(config => 
        // {
        //     config.DBConfig.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6379));
        // }, "redis1")
        // .WithMessagePack();            
    });

SentrySdk.Init(options =>
{
    // A Sentry Data Source Name (DSN) is required.
    // See https://docs.sentry.io/product/sentry-basics/dsn-explainer/
    // You can set it in the SENTRY_DSN environment variable, or you can set it in code here.
    options.Dsn = "https://8a95839391ef4d5f9395d52c557e3f4a@o1346167.ingest.sentry.io/4505130042523648";

    // When debug is enabled, the Sentry client will emit detailed debugging information to the console.
    // This might be helpful, or might interfere with the normal operation of your application.
    // We enable it here for demonstration purposes when first trying Sentry.
    // You shouldn't do this in your applications unless you're troubleshooting issues with Sentry.
    options.Debug = false;

    // This option is recommended. It enables Sentry's "Release Health" feature.
    options.AutoSessionTracking = true;

    // Enabling this option is recommended for client applications only. It ensures all threads use the same global scope.
    options.IsGlobalModeEnabled = false;

    // This option will enable Sentry's tracing features. You still need to start transactions and spans.
    options.EnableTracing = true;

    // Example sample rate for your transactions: captures 10% of transactions
    options.TracesSampleRate = 0.3;
});

// builder.Services.AddProblemDetails(options =>
//     options.CustomizeProblemDetails = context =>
//     {
//         if (context.ProblemDetails.Status == StatusCodes.Status415UnsupportedMediaType)
//         {
//             
//         }
//
//         context.ProblemDetails.Type = context.ProblemDetails.Type;
//         context.ProblemDetails.Title = "Bad Input";
//         context.ProblemDetails.Detail = context.ProblemDetails.Detail;
//     }
// );
//Add session
builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(5);
//    //options.IdleTimeout = TimeSpan.FromDays(2);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

// ======== Mapper ========
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var settings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>()!;
//builder.Services.AddAutoMapper(typeof(Program).Assembly);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperConfig());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddEndpointsApiExplorer();

// ======== AppContext ========
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// ======== Extensions ========
builder.Services.ConfigureServices(settings);

// ======== Disable API Validate ========
// builder.Services.Configure<ApiBehaviorOptions>(options
//     => options.SuppressModelStateInvalidFilter = false);
// builder.Services.AddScoped<ModelValidationAttribute>();

builder.Services
       .AddFluentEmail("neolock18@gmail.com")
       .AddRazorRenderer()
       .AddSmtpSender("in-v3.mailjet.com", 587, "", "");




// ======== Add services to the container. ========
builder.Services.AddScoped<ITokenService, TokenService>();
// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IHocViRepository, HocViRepository>();
builder.Services.AddScoped<IDoanhNghiepRepository, DoanhNghiepRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IDonViRepository, DonViRepository>();
builder.Services.AddScoped<IChuyenGiaRepository, ChuyenGiaRepository>();
builder.Services.AddScoped<IAnToanRepository, AnToanRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IDinhDanhCanBoRepository, DinhDanhCanBoRepository>();
builder.Services.AddScoped<IDinhDanhToChucRepository, DinhDanhToChucRepository>();
builder.Services.AddScoped<ILoaiHinhToChucRepository, LoaiHinhToChucRepository>();
builder.Services.AddScoped<IHinhThucSoHuuRepository, HinhThucSoHuuRepository>();
builder.Services.AddScoped<ILinhVucNghienCuuRepository, LinhVucNghienCuuRepository>();
builder.Services.AddScoped<IDinhDanhNhiemVuRepository, DinhDanhNhiemVuRepository>();
builder.Services.AddScoped<IThongKeRepository, ThongKeRepository>();
builder.Services.AddScoped<ITitleRepository, TitleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITrinhDoDaoTaoRepository, TrinhDoDaoTaoRepository>();
builder.Services.AddScoped<ILoaiHinhCongBoRepository, LoaiHinhCongBoRepository>();
builder.Services.AddScoped<ICapDoNhiemVuRepository, CapDoNhiemVuRepository>();
builder.Services.AddScoped<IMissionTypeRepository, LoaiNhiemVuRepository>();
builder.Services.AddScoped<ITrangThaiNhiemVuRepository, TrangThaiNhiemVuRepository>();
builder.Services.AddScoped<ILoaiDuAnRepository, LoaiDuAnRepository>();
builder.Services.AddScoped<IXQuangRepository, XQuangRepository>();
builder.Services.AddScoped<IFileStatusRepository, FileStatusRepository>();
builder.Services.AddScoped<INhiemVuRepository, NhiemVuRepository>();
builder.Services.AddScoped<IVaiTroThamGiaRepository, VaiTroThamGiaRepository>();
builder.Services.AddScoped<IProvinceRepository, TinhThanhRepository>();
builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();
builder.Services.AddScoped<IWardRepository, WardRepository>();
builder.Services.AddScoped<IToChucRepository, ToChucRepository>();
builder.Services.AddScoped<ICanBoRepository, CanBoRepository>();
builder.Services.AddScoped<IBucXaRepository, BucXaRepository>();
builder.Services.AddScoped<IThongTinRepository, ThongTinRepository>();
builder.Services.AddScoped<ISoHuuTriTueRepository, SoHuuTriTueRepository>();
builder.Services.AddScoped<ITieuChuanRepository, TieuChuanRepository>();
builder.Services.AddScoped<IWorkHistoryRepository, WorkHistoryRepository>();
builder.Services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
builder.Services.AddSingleton<IMemoryCachingService, MemoryCachingService>();
builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<ICongBoRepository, CongBoRepository>();
builder.Services.AddScoped<ILGSPRepository, LGSPRepository>();
builder.Services.AddScoped<IAnToanRepository, AnToanRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<IActivityLogUserRepository, ActivityLogUserRepository>();
builder.Services.AddScoped<IBieuDoRepository, BieuDoRepository>();

QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy"); // Config in ServiceExtension.cs
app.UseAuthentication();
app.UseAuthorization();
//app.UseSession();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();
app.MapGet("/", () => "Khoa Học Công Nghệ BRVT");
await app.RunAsync();