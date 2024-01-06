var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//◊¢≤·
builder.Services.AddCors(c => c.AddPolicy("any", p => p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));//øÁ”Ú
builder.Services.AddSingleton(typeof(CQIE.OnlineVote.Utility.ConfigService));
builder.Services.AddScoped(typeof(CQIE.OnlineVote.DBManager.DbContexts.LMSDbContext));
builder.Services.AddScoped<CQIE.OnlineVote.DBManager.IDbManager, CQIE.OnlineVote.DBManager.DbManagerImp>();

#region 
builder.Services.AddScoped<CQIE.OnlineVote.Services.ISysuerService, CQIE.OnlineVote.Services.SysuerServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.ISystemMenuService, CQIE.OnlineVote.Services.SystemMenuServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.IUserRoleService,CQIE.OnlineVote.Services.UserRoleServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.ISysRoleService,CQIE.OnlineVote.Services.SysRoleServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.ISysUserSingerService,CQIE.OnlineVote.Services.SysUserSingerServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.ISysuerInformationService,CQIE.OnlineVote.Services.SysuerInformationServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.IMenuRoleService,CQIE.OnlineVote.Services.MenuRoleServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.IVoteService,CQIE.OnlineVote.Services.VoteServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.IBattleService,CQIE.OnlineVote.Services.BattleServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.ICompetitionService,CQIE.OnlineVote.Services.CompetitionServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.ICompetitonRoundService, CQIE.OnlineVote.Services.CompetitionRoundServiceImp>();
builder.Services.AddScoped<CQIE.OnlineVote.Services.IBattleUserService, CQIE.OnlineVote.Services.BattleUserServiceImp>();
#endregion


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.UseStaticFiles();
app.MapControllers();
app.Run();

