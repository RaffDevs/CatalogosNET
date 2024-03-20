using APICatalogo.App.Context;
using APICatalogo.App.Domain.Category.Models.Mappers;
using APICatalogo.App.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoTest.Setup;

public class CategoryControllerSetup
{
    public IUnitOfWork repository;
    public IMapper mapper;
    public static DbContextOptions<DatabaseContext> databaseContextOptions { get; }

    public static string connectionString =
        "Host=dpg-cnnfg8acn0vc738id4fg-a.oregon-postgres.render.com;Port=5432;Pooling=true;Database=dev_msk7;User Id=raffdevs;Password=g5a63kypggcgeOmYw6UxtarpH7fGpxDj;";

    static CategoryControllerSetup()
    {
        databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseNpgsql(connectionString)
            .Options;
    }

    public CategoryControllerSetup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CategoryDTOMapperProfile());
        });
        mapper = config.CreateMapper();
        var context = new DatabaseContext(databaseContextOptions);
        repository = new UnitOfWork(context);
    }
}