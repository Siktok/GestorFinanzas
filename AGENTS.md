# AGENTS.md

## Contexto del proyecto

FinanzaNova es una aplicacion ASP.NET Core MVC en .NET 10 para una futura plataforma de gestion financiera.

La solucion principal es `FinanzaNova.slnx` y la aplicacion web vive en `FinanzaNova.Web`.

## Reglas base

- Mantener el proyecto en .NET 10 (`net10.0`) y ASP.NET Core MVC.
- Usar la estructura existente de `FinanzaNova.Web` para nuevas funcionalidades.
- Mantener las paginas simples hasta que exista una especificacion funcional clara.
- No introducir autenticacion, servicios externos, SPA ni arquitectura compleja salvo que el usuario lo pida explicitamente.
- No introducir persistencia en tareas que no la requieran.
- Cuando una tarea requiera persistencia, usar SQLite con EF Core como opcion estandar inicial.
- No versionar carpetas generadas como `.vs/`, `bin/` u `obj/`.

## Convenciones del proyecto

- Controladores MVC en `FinanzaNova.Web/Controllers`.
- Vistas Razor en `FinanzaNova.Web/Views`.
- Modelos simples y ViewModels en `FinanzaNova.Web/Models`, salvo que el proyecto crezca y se acuerde otra estructura.
- Archivos estaticos en `FinanzaNova.Web/wwwroot`.
- Estilos propios en `FinanzaNova.Web/wwwroot/css/site.css`.
- Reutilizar Bootstrap local antes de introducir nuevas dependencias de UI.
- Mantener textos visibles en espanol cuando se este trabajando en paginas de producto.

## Skills del proyecto

Las skills siguen el formato Agent Skills: cada skill vive en `skills/<nombre>/SKILL.md` con frontmatter YAML `name` y `description`.
El archivo se llama `SKILL.md` por estandar, pero el nombre visible de la skill siempre debe ser el valor de `name`.
Antes de crear o modificar skills, consulta `skill-standards` en `skills/skill-standards/SKILL.md`.

- `skill-standards` (`skills/skill-standards/SKILL.md`): usar al crear, migrar o revisar skills del proyecto.
- `finance-conventions` (`skills/finance-conventions/SKILL.md`): usar en tareas con datos financieros visibles, formatos, estados, colores o lenguaje de dominio.
- `mvc-feature` (`skills/mvc-feature/SKILL.md`): usar al crear o modificar funcionalidades MVC, controladores, acciones, vistas o ViewModels.
- `razor-ui` (`skills/razor-ui/SKILL.md`): usar al cambiar layout, vistas Razor, navegacion, estilos CSS o UI basada en Bootstrap.
- `sqlite-persistence` (`skills/sqlite-persistence/SKILL.md`): usar solo cuando el usuario pida persistencia o una funcionalidad que claramente necesite guardar datos.
- `review-validation` (`skills/review-validation/SKILL.md`): usar antes de entregar cambios de codigo o cuando el usuario pida una revision.

Si una tarea cruza varias areas, aplica las skills relevantes en este orden: estandar de skills, convenciones financieras, persistencia, funcionalidad MVC, UI Razor y validacion.

## Flujo recomendado

1. Inspeccionar la estructura actual antes de editar.
2. Limitar los cambios al alcance pedido por el usuario.
3. Preferir patrones ya presentes en `FinanzaNova.Web`.
4. Evitar refactors no solicitados.
5. Validar antes de entregar cambios de codigo.

## Validacion recomendada

Antes de entregar cambios de codigo, ejecutar:

```powershell
dotnet restore FinanzaNova.slnx
dotnet build FinanzaNova.slnx
```

Si no se puede ejecutar alguna validacion, indicarlo claramente en la entrega.
