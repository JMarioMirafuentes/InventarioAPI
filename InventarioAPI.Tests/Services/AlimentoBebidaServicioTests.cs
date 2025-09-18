

//using AutoMapper;
//using InventarioAPI.Data;
//using InventarioAPI.Dtos;
//using InventarioAPI.Models;
//using InventarioAPI.Services;
//using Microsoft.EntityFrameworkCore;

//public class AlimentoBebidaServicioTests
//{
//    private readonly IMapper _mapper;

//    public AlimentoBebidaServicioTests()
//    {
//        var config = new MapperConfiguration(cfg =>
//        {
//            //cfg.CreateMap<AlimentoBebida, AlimentoBebidaDTO>()
//            //    .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus == AlimentoBebeidaEstatus.Activo));
//            //cfg.CreateMap<AlimentoBebidaDTO, AlimentoBebida>()
//            //    .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus ? AlimentoBebeidaEstatus.Activo : AlimentoBebeidaEstatus.Inactivo));
//        });
//        _mapper = config.CreateMapper();
//    }

//    [Fact]
//    public async Task ObtenerTodos()
//    {
//        var options = new DbContextOptionsBuilder<InventarioDbContext>()
//            .UseInMemoryDatabase(databaseName: "IventarioDbContext")
//            .Options;

//        using var context = new InventarioDbContext(options);

//        context.AlimentosBebidas.AddRange(
//            new AlimentoBebida {Id=1, Nombre = "Pizza italiana", Descripcion = "Grande 8 rebanadas", Estatus = AlimentoBebeidaEstatus.Activo },
//            new AlimentoBebida {Id=2, Nombre = "Jugo naranja", Descripcion = "600 ml", Estatus = AlimentoBebeidaEstatus.Inactivo }, 
//            new AlimentoBebida {Id=3, Nombre = "Coca cola", Descripcion = "600 ml", Estatus = AlimentoBebeidaEstatus.Activo },
//            new AlimentoBebida {Id=4, Nombre = "Hamburguesa con queso", Descripcion = "Sencilla", Estatus = AlimentoBebeidaEstatus.Inactivo }
//        );
//        await context.SaveChangesAsync();

//        var servicio = new AlimentoBebidaServicio(context, _mapper);

//        var result = await servicio.ObtenerTodos();

//        Assert.NotNull(result);
//        var list = result.ToList();
//        Assert.Equal(4, list.Count);
//        Assert.Contains(list, x => x.Nombre == "Pizza italiana" && x.Estatus == true);
//        Assert.Contains(list, x => x.Nombre == "Jugo naranja" && x.Estatus == false);
//    }
//    [Fact]
//    public async Task GuardarAlimentoBebida()
//    {
//        var options = new DbContextOptionsBuilder<InventarioDbContext>()
//            .UseInMemoryDatabase("IventarioDbContext")
//            .Options;

//        using var context = new InventarioDbContext(options);
//        var service = new AlimentoBebidaServicio(context, _mapper);

//        var dto = new AlimentoBebida
//        {
//            Id = 5,
//            Nombre = "Cacahuate japones",
//            Descripcion = "Individual 52 grs",
//            Estatus = AlimentoBebeidaEstatus.Activo
//        };

//        var result = await service.GuardarAlimentoBebida(dto);

//        Assert.NotNull(result);
//        Assert.True(result.Id > 0);
//        Assert.Equal("Cacahuate japones", result.Nombre);
//    }

//    [Fact]
//    public async Task ObtenerAlimentoBebidaId()
//    {
//        var options = new DbContextOptionsBuilder<InventarioDbContext>()
//            .UseInMemoryDatabase("IventarioDbContext")
//            .Options;

//        using var context = new InventarioDbContext(options);

//        var entity = new AlimentoBebida { Id = 6, Nombre = "Pizza italiana", Descripcion = "Grande 8 rebanadas", Estatus = AlimentoBebeidaEstatus.Activo };
//        context.AlimentosBebidas.Add(entity);
//        await context.SaveChangesAsync();

//        var service = new AlimentoBebidaServicio(context, _mapper);

//        var result = await service.ObtenerAlimentoBebidaId(entity.Id);

//        Assert.NotNull(result);
//        Assert.Equal("Pizza italiana", result.Nombre);
//        Assert.True(result.Estatus);
//    }

//    [Fact]
//    public async Task ActualizarAlimentoBebidaId()
//    {
//        var options = new DbContextOptionsBuilder<InventarioDbContext>()
//            .UseInMemoryDatabase("IventarioDbContext")
//            .Options;

//        using var context = new InventarioDbContext(options);

//        var entity = new AlimentoBebida { Id = 7, Nombre = "Pizza italiana", Descripcion = "Grande 8 rebanadas", Estatus = AlimentoBebeidaEstatus.Activo };
//        context.AlimentosBebidas.Add(entity);
//        await context.SaveChangesAsync();

//        var service = new AlimentoBebidaServicio(context, _mapper);

//        var dtoActualizado = new AlimentoBebida
//        {
//            Id = 7,
//            Nombre = "Ensalada Mixta",
//            Descripcion = "Lechuga y tomate",
//            Estatus = AlimentoBebeidaEstatus.Inactivo
//        };

//        var success = await service.ActualizarAlimentoBebida(dtoActualizado);

//        Assert.True(success);

//        var updatedEntity = await context.AlimentosBebidas.FindAsync(entity.Id);
//        Assert.Equal("Ensalada Mixta", updatedEntity?.Nombre);
//        Assert.Equal("Lechuga y tomate", updatedEntity?.Descripcion);
//        Assert.Equal(AlimentoBebeidaEstatus.Inactivo, updatedEntity?.Estatus);
//    }

//    [Fact]
//    public async Task EliminarAlimentoBebidaId()
//    {
//        var options = new DbContextOptionsBuilder<InventarioDbContext>()
//            .UseInMemoryDatabase("IventarioDbContext")
//            .Options;

//        using var context = new InventarioDbContext(options);

//        var entity = new AlimentoBebida { Id=8, Nombre = "Sopa", Descripcion = "Verduras", Estatus = AlimentoBebeidaEstatus.Activo };
//        context.AlimentosBebidas.Add(entity);
//        await context.SaveChangesAsync();

//        var service = new AlimentoBebidaServicio(context, _mapper);

//        var success = await service.EliminarAlimentoBebidaId(entity.Id);

//        Assert.True(success);

//        var deleted = await context.AlimentosBebidas.FindAsync(entity.Id);
//        Assert.Null(deleted);
//    }
//    [Fact]
//    public async Task CambiarEstatusAlimentoBebidaId()
//    {
//        var options = new DbContextOptionsBuilder<InventarioDbContext>()
//            .UseInMemoryDatabase("IventarioDbContext")
//            .Options;

//        using var context = new InventarioDbContext(options);
//        var entity = new AlimentoBebida
//        {
//            Id=9,
//            Nombre = "Refresco",
//            Descripcion = "Con gas",
//            Estatus = AlimentoBebeidaEstatus.Activo
//        };
//        context.AlimentosBebidas.Add(entity);
//        await context.SaveChangesAsync();

//        var service = new AlimentoBebidaServicio(context, _mapper);

//        var resultado = await service.CambiarEstatusAlimentoBebidaId(entity.Id, false);
//        Assert.True(resultado);
//        var updatedEntity = await context.AlimentosBebidas.FindAsync(entity.Id);
//        Assert.Equal(AlimentoBebeidaEstatus.Inactivo, updatedEntity?.Estatus);
//    }

//}
