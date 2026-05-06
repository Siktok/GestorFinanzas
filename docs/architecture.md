# Arquitectura

FinanzaNova es una aplicacion ASP.NET Core MVC en .NET 10. La V1 esta pensada para uso local, sin login, sin servicios externos y sin frontend SPA.

## Estructura

- `FinanzaNova.slnx`: solucion principal.
- `FinanzaNova.Web`: aplicacion web MVC.
- `FinanzaNova.Web/Controllers`: controladores MVC por area funcional.
- `FinanzaNova.Web/Models`: entidades de dominio, enums y ViewModels.
- `FinanzaNova.Web/Data`: `FinanzaNovaDbContext` y migraciones EF Core.
- `FinanzaNova.Web/Services`: logica compartida de calculo.
- `FinanzaNova.Web/Views`: vistas Razor.
- `FinanzaNova.Web/wwwroot/css/site.css`: estilos propios.

## Capas

Los controladores coordinan lectura/escritura de datos, validacion de formularios y seleccion de vistas. Las vistas Razor contienen la presentacion y usan Bootstrap local junto con estilos propios.

La persistencia vive en `FinanzaNovaDbContext` con SQLite. Las entidades se mantienen dentro de `FinanzaNova.Web` porque el proyecto aun es pequeno y no requiere separacion por capas o proyectos.

`FinanzasResumenService` centraliza el calculo de saldos para evitar duplicar reglas entre dashboard, cuentas y futuras pantallas.

## Dependencias

- ASP.NET Core MVC.
- EF Core SQLite.
- Bootstrap local incluido en `wwwroot/lib`.
- jQuery y validacion unobtrusive ya incluidos por la plantilla MVC.

No se usan servicios externos, autenticacion, autorizacion avanzada, bundlers ni frameworks SPA.

## Convenciones

- Textos visibles en espanol.
- Moneda unica: EUR.
- Formato de fechas en UI: `dd/MM/yyyy`.
- Importes en UI con formato espanol, por ejemplo `1.234,56 €`.
- Los archivos SQLite generados no se versionan.
