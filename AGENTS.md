# AGENTS.md

## Contexto del proyecto

FinanzaNova es una aplicacion ASP.NET Core MVC en .NET 10 para una futura plataforma de gestion financiera.

## Instrucciones para agentes

- Mantener el proyecto en .NET 10 y ASP.NET Core MVC.
- Usar la estructura existente de `FinanzaNova.Web` para nuevas funcionalidades.
- No introducir autenticacion, base de datos ni Entity Framework salvo que el usuario lo pida explicitamente.
- Mantener las paginas simples hasta que exista una especificacion funcional clara.
- No versionar carpetas generadas como `.vs/`, `bin/` u `obj/`.

## Validacion recomendada

Antes de entregar cambios de codigo, ejecutar:

```powershell
dotnet restore FinanzaNova.slnx
dotnet build FinanzaNova.slnx
```
