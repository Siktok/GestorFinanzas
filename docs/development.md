# Desarrollo

## Requisitos

- .NET SDK 10.
- Git.

## Ejecutar En Local

```powershell
dotnet restore FinanzaNova.slnx
dotnet build FinanzaNova.slnx
dotnet run --project FinanzaNova.Web
```

La aplicacion muestra la URL local al arrancar. En desarrollo suele usar `http://localhost:5085`.

## Base SQLite Local

La cadena por defecto es:

```json
"DefaultConnection": "Data Source=finanzanova.db"
```

Al arrancar, la app aplica migraciones pendientes automaticamente con EF Core. Si la base no existe, se crea vacia con el esquema actual.

Los datos locales no se versionan. `.gitignore` excluye:

```text
*.db
*.db-shm
*.db-wal
```

Para reiniciar los datos locales, para la app y borra `FinanzaNova.Web/finanzanova.db*`. Al volver a arrancar se creara una base vacia.

## Migraciones

Cuando cambie el esquema:

```powershell
dotnet ef migrations add NombreMigracion --project FinanzaNova.Web --startup-project FinanzaNova.Web --output-dir Data\Migrations
dotnet build FinanzaNova.slnx
```

Las migraciones se versionan porque describen el esquema. La base `.db` no se versiona porque contiene datos locales.

## Validacion

Antes de cerrar cambios de codigo:

```powershell
dotnet restore FinanzaNova.slnx
dotnet build FinanzaNova.slnx
dotnet test FinanzaNova.slnx --no-build
```

Si la app esta arrancada y bloquea `FinanzaNova.Web.exe`, para el proceso antes de compilar.

## Tests

`FinanzaNova.Tests` contiene la base de tests automatizados del proyecto:

- Tests de `FinanzasResumenService` con SQLite en memoria para reglas de saldo, transferencias e inversiones.
- Tests de integracion MVC con `WebApplicationFactory` para comprobar que las rutas principales cargan.

Usa `dotnet test FinanzaNova.slnx --no-build` despues de compilar la solucion.

## Git

Usa ramas `codex/<descripcion>` para cambios hechos por Codex salvo que se pida otra cosa.

No incluir en commits:

- `.vs/`
- `bin/`
- `obj/`
- `*.db`
- `*.db-shm`
- `*.db-wal`

Mantener commits separados cuando mezclen codigo de producto y documentacion operativa para agentes.
