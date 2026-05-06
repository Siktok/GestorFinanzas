---
name: sqlite-persistence
description: Guia para modificar la persistencia SQLite con EF Core ya existente en FinanzaNova.Web. Usar cuando el usuario pida cambios de esquema, base de datos, almacenamiento local, historial, persistencia de informacion o una funcionalidad que deba guardar datos entre ejecuciones.
---

# SQLite Persistence

## Cuando usarla

Usar esta skill solo cuando el usuario pida persistencia, almacenamiento local, base de datos, historial de datos, cambios de esquema o una funcionalidad que claramente necesite guardar informacion entre ejecuciones.

## Estandar

La V1 ya usa SQLite con EF Core en `FinanzaNova.Web`, mediante `FinanzaNovaDbContext`, migraciones en `FinanzaNova.Web/Data/Migrations` y la cadena `DefaultConnection`.

## Reglas

- No anadir SQLite ni EF Core a tareas puramente visuales o de documentacion.
- Mantener la configuracion simple y local para desarrollo inicial.
- Usar las entidades y `FinanzaNovaDbContext` dentro de `FinanzaNova.Web` hasta que exista una razon clara para separar proyectos.
- Usar migraciones de EF Core cuando se cambie el esquema.
- No introducir autenticacion, multiusuario ni reglas financieras avanzadas sin peticion explicita.
- No guardar secretos en `appsettings.json`.
- Mantener la base de datos generada fuera del control de versiones salvo peticion explicita.
- No versionar `finanzanova.db`, `finanzanova.db-shm` ni `finanzanova.db-wal`.

## Flujo recomendado

1. Confirmar que la tarea requiere cambiar datos persistidos o esquema.
2. Revisar `FinanzaNovaDbContext`, entidades, migraciones existentes y `docs/data-model.md`.
3. Definir el cambio minimo en entidades y relaciones.
4. Actualizar `FinanzaNovaDbContext` y configuracion solo si el modelo lo requiere.
5. Crear una migracion incremental con `dotnet ef migrations add`.
6. Revisar que la migracion refleja solo el cambio solicitado.
7. Usar acceso a datos sencillo desde controladores o servicios ligeros solo si aportan claridad.
8. Aplicar `finance-conventions` (`../finance-conventions/SKILL.md`) cuando las entidades representen importes, fechas, porcentajes, estados o movimientos financieros visibles.

## Validacion

- Ejecutar `dotnet restore FinanzaNova.slnx`.
- Ejecutar `dotnet build FinanzaNova.slnx`.
- Si se crean migraciones, revisar que reflejen solo el cambio solicitado.
- Comprobar que no se versionen archivos de base de datos generados salvo que el usuario lo pida.
