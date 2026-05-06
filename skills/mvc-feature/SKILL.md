---
name: mvc-feature
description: Guia para crear o modificar funcionalidades ASP.NET Core MVC en FinanzaNova.Web. Usar cuando la tarea implique controladores, acciones, vistas Razor, modelos simples, ViewModels, navegacion MVC o nuevas paginas dentro de la aplicacion.
---

# MVC Feature

## Cuando usarla

Usar esta skill al crear o modificar funcionalidades de ASP.NET Core MVC en `FinanzaNova.Web`, incluyendo controladores, acciones, vistas Razor, modelos simples y ViewModels.

## Reglas

- Mantener la funcionalidad dentro de `FinanzaNova.Web`.
- Usar ASP.NET Core MVC convencional.
- No convertir la app en SPA.
- No introducir persistencia salvo que el usuario la pida o la funcionalidad dependa claramente de guardar datos.
- No introducir autenticacion, autorizacion avanzada ni servicios externos sin instruccion explicita.
- Mantener las paginas simples y utiles para una plataforma financiera en fase inicial.
- Consultar `finance-conventions` (`../finance-conventions/SKILL.md`) cuando la funcionalidad muestre importes, porcentajes, fechas, estados o movimientos financieros.

## Flujo recomendado

1. Revisar `Program.cs`, `Controllers`, `Views` y `Models` antes de cambiar codigo.
2. Crear o ajustar el controlador minimo necesario.
3. Usar una accion MVC clara por comportamiento principal.
4. Crear una vista Razor en la carpeta correspondiente de `Views`.
5. Usar ViewModels simples cuando la vista necesite datos estructurados.
6. Actualizar navegacion solo si la funcionalidad debe ser accesible desde la UI principal.
7. Mantener nombres consistentes con el dominio financiero y en espanol cuando sean visibles para el usuario.

## Validacion

- Comprobar que el proyecto sigue compilando.
- Revisar que no se hayan introducido dependencias no pedidas.
- Ejecutar `dotnet restore FinanzaNova.slnx` y `dotnet build FinanzaNova.slnx` antes de entregar cambios.
