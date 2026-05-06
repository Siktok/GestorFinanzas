---
name: sqlite-persistence
description: Guia para introducir o modificar persistencia SQLite con EF Core en FinanzaNova.Web. Usar solo cuando el usuario pida base de datos, almacenamiento local, historial, persistencia de informacion o una funcionalidad que deba guardar datos entre ejecuciones.
---

# SQLite Persistence

## Cuando usarla

Usar esta skill solo cuando el usuario pida persistencia, almacenamiento local, base de datos, historial de datos o una funcionalidad que claramente necesite guardar informacion entre ejecuciones.

## Estandar

La persistencia inicial del proyecto debe ser SQLite con EF Core.

## Reglas

- No anadir SQLite ni EF Core a tareas puramente visuales o de documentacion.
- Mantener la configuracion simple y local para desarrollo inicial.
- Usar entidades y `DbContext` dentro de `FinanzaNova.Web` hasta que exista una razon clara para separar proyectos.
- Usar migraciones de EF Core cuando se cambie el esquema.
- No introducir autenticacion, multiusuario ni reglas financieras avanzadas sin peticion explicita.
- No guardar secretos en `appsettings.json`.
- Mantener la base de datos generada fuera del control de versiones salvo peticion explicita.

## Flujo recomendado

1. Confirmar que la tarea requiere persistencia.
2. Anadir los paquetes de EF Core SQLite necesarios al proyecto web.
3. Crear un `DbContext` con un nombre alineado al proyecto, por ejemplo `FinanzaNovaDbContext`.
4. Definir entidades simples para el caso solicitado.
5. Configurar SQLite desde `Program.cs` y `appsettings.json`.
6. Crear migracion inicial o migracion incremental segun el estado del proyecto.
7. Usar acceso a datos sencillo desde controladores o servicios ligeros solo si aportan claridad.
8. Aplicar `finance-conventions` (`../finance-conventions/SKILL.md`) cuando las entidades representen importes, fechas, porcentajes, estados o movimientos financieros visibles.

## Validacion

- Ejecutar `dotnet restore FinanzaNova.slnx`.
- Ejecutar `dotnet build FinanzaNova.slnx`.
- Si se crean migraciones, revisar que reflejen solo el cambio solicitado.
- Comprobar que no se versionen archivos de base de datos generados salvo que el usuario lo pida.
