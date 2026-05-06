---
name: mvc-feature
description: Guia para crear o modificar funcionalidades ASP.NET Core MVC en FinanzaNova.Web. Usar cuando la tarea implique controladores, acciones, vistas Razor, entidades, ViewModels, servicios ligeros, navegacion MVC o nuevas paginas dentro de la aplicacion.
---

# MVC Feature

## Cuando usarla

Usar esta skill al crear o modificar funcionalidades de ASP.NET Core MVC en `FinanzaNova.Web`, incluyendo controladores, acciones, vistas Razor, entidades, ViewModels y servicios ligeros.

## Reglas

- Mantener la funcionalidad dentro de `FinanzaNova.Web`.
- Usar ASP.NET Core MVC convencional.
- No convertir la app en SPA.
- La V1 ya usa SQLite con EF Core; no introducir nueva persistencia, otra base de datos ni patrones complejos salvo que el usuario lo pida.
- No introducir autenticacion, autorizacion avanzada ni servicios externos sin instruccion explicita.
- Mantener las paginas simples y utiles para una plataforma financiera en fase inicial.
- Mantener la logica financiera compartida en servicios ligeros cuando evite duplicacion; `FinanzasResumenService` es el patron actual para saldos.
- Consultar `finance-conventions` (`../finance-conventions/SKILL.md`) cuando la funcionalidad muestre importes, porcentajes, fechas, estados o movimientos financieros.

## Flujo recomendado

1. Revisar `Program.cs`, `Controllers`, `Views`, `Models`, `Data` y `Services` antes de cambiar codigo.
2. Crear o ajustar el controlador minimo necesario.
3. Usar una accion MVC clara por comportamiento principal.
4. Crear una vista Razor en la carpeta correspondiente de `Views`.
5. Usar ViewModels simples cuando la vista necesite datos estructurados o calculados.
6. Reutilizar `FinanzaNovaDbContext` y `FinanzasResumenService` cuando la funcionalidad trabaje con datos existentes.
7. Actualizar navegacion solo si la funcionalidad debe ser accesible desde la UI principal.
8. Mantener nombres consistentes con el dominio financiero y en espanol cuando sean visibles para el usuario.

## Validacion

- Comprobar que el proyecto sigue compilando.
- Revisar que no se hayan introducido dependencias no pedidas.
- Ejecutar `dotnet restore FinanzaNova.slnx` y `dotnet build FinanzaNova.slnx` antes de entregar cambios.
