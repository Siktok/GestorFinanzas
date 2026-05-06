# FinanzaNova

FinanzaNova es una aplicacion ASP.NET Core MVC en .NET 10 para gestionar finanzas personales en local.

La primera version no incluye login, multiusuario, servicios externos ni SPA. Los datos se guardan en una base SQLite local mediante EF Core.

## Funcionalidades

- Dashboard con patrimonio total, liquidez, inversiones y balance mensual.
- Gestion de cuentas con saldo calculado.
- Gestion de categorias de ingresos y gastos.
- Registro de movimientos: ingresos, gastos y transferencias entre cuentas.
- Gestion de inversiones con valor actual manual.
- Graficos basicos con Razor, HTML y CSS, sin librerias externas.

## Modelo de calculo

El saldo de cada cuenta se calcula desde:

```text
Saldo inicial
+ ingresos confirmados
- gastos confirmados
- transferencias salientes confirmadas
+ transferencias entrantes confirmadas
- importe invertido en inversiones activas financiadas desde esa cuenta
```

El patrimonio total se calcula como:

```text
Liquidez total + valor actual de inversiones activas
```

Una inversion no se registra como gasto. Si una cuenta tiene 4.000 EUR y se crea una inversion activa de 1.000 EUR financiada desde esa cuenta, el resultado esperado es:

- Liquidez: 3.000 EUR.
- Inversiones: 1.000 EUR.
- Patrimonio total: 4.000 EUR.

Si el valor actual de esa inversion sube a 1.200 EUR, el patrimonio total pasa a 4.200 EUR.

## Persistencia local

La aplicacion usa SQLite con la cadena:

```json
"DefaultConnection": "Data Source=finanzanova.db"
```

Al arrancar la app se aplican las migraciones pendientes automaticamente. La base generada (`finanzanova.db`, `finanzanova.db-shm`, `finanzanova.db-wal`) no se versiona.

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
- `FinanzaNova.Web/Data`: `DbContext` y migraciones de EF Core.
- `FinanzaNova.Web/Models`: entidades financieras y ViewModels.
- `FinanzaNova.Web/Controllers`: controladores MVC.
- `FinanzaNova.Web/Views`: vistas Razor.
- `FinanzaNova.Web/wwwroot/css/site.css`: estilos propios.
