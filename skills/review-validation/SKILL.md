---
name: review-validation
description: Guia para revisar cambios y validar FinanzaNova antes de entregar. Usar antes de cerrar una tarea de codigo, al revisar un diff, al comprobar una implementacion o cuando el usuario pida una revision tecnica del proyecto.
---

# Review Validation

## Cuando usarla

Usar esta skill antes de entregar cambios de codigo o cuando el usuario pida revisar el proyecto, una rama, un diff o una implementacion.

## Reglas

- Priorizar bugs, regresiones, riesgos y validaciones faltantes.
- Verificar que el proyecto sigue en .NET 10 y ASP.NET Core MVC.
- Confirmar que no se han introducido autenticacion, servicios externos, SPA, arquitectura compleja o persistencia fuera del alcance pedido.
- Confirmar que SQLite con EF Core sigue siendo la persistencia local de la V1 y que no se ha introducido otra base de datos sin peticion explicita.
- Confirmar que los calculos financieros respetan `docs/calculations.md` y `FinanzasResumenService` cuando aplique.
- Confirmar que las skills del proyecto mantienen el formato `skills/<name>/SKILL.md` y que las referencias visibles usan el nombre de skill, no solo `SKILL.md`.
- No revertir cambios ajenos.
- No incluir `.vs/`, `bin/`, `obj/` ni archivos de base de datos generados sin peticion explicita.

## Flujo recomendado

1. Revisar el estado del repositorio.
2. Revisar los archivos modificados y contrastarlos con el alcance pedido.
3. Comprobar que las skills aplicables fueron respetadas.
4. Ejecutar validaciones de .NET.
5. Reportar fallos de forma concreta, con archivo y causa cuando sea posible.

## Validacion

Ejecutar:

```powershell
dotnet restore FinanzaNova.slnx
dotnet build FinanzaNova.slnx
dotnet test FinanzaNova.slnx --no-build
```

Si alguna validacion no se puede ejecutar, explicar el motivo y el riesgo residual.
