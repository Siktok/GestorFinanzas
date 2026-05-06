# FinanzaNova

Proyecto ASP.NET Core MVC en .NET 10 para una futura aplicacion de gestion financiera.

## Estado actual

El proyecto contiene una estructura MVC base y una pagina Home placeholder con el mensaje:

> Tu panel financiero estara disponible proximamente.

No incluye todavia autenticacion, base de datos, modelos financieros ni paginas adicionales.

## Requisitos

- .NET SDK 10

## Ejecutar en local

```powershell
dotnet restore FinanzaNova.slnx
dotnet build FinanzaNova.slnx
dotnet run --project FinanzaNova.Web
```

Despues de arrancar la aplicacion, abre la URL local indicada por `dotnet run`.

## Estructura principal

- `FinanzaNova.slnx`: solucion del proyecto.
- `FinanzaNova.Web`: aplicacion ASP.NET Core MVC.
- `FinanzaNova.Web/Views/Home/Index.cshtml`: pagina Home placeholder.
