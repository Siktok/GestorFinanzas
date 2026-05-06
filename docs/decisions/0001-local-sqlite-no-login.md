# 0001 - SQLite Local Sin Login

## Estado

Aceptada.

## Contexto

FinanzaNova V1 es una aplicacion personal para gestionar finanzas en local. El objetivo inicial es validar el flujo de cuentas, movimientos, inversiones, dashboard y calculos sin introducir complejidad de producto innecesaria.

## Decision

La V1 usa:

- ASP.NET Core MVC en .NET 10.
- SQLite local con EF Core.
- Sin login.
- Sin multiusuario.
- Sin servicios externos.
- Sin SPA.

La base se crea localmente como `finanzanova.db` y no se versiona. El esquema se versiona mediante migraciones EF Core.

## Consecuencias

Ventajas:

- Arranque simple en cualquier maquina con .NET SDK 10.
- Datos personales quedan en local.
- Menos superficie de seguridad y despliegue.
- Cambios de modelo controlados mediante migraciones.

Limitaciones:

- No hay sincronizacion entre dispositivos.
- No hay usuarios ni permisos.
- No hay importacion bancaria ni cotizaciones automaticas.
- El backup de datos locales depende del usuario.

Estas limitaciones son aceptables para la V1. Si el producto necesita multiusuario, sincronizacion o servicios externos, se debera registrar una nueva decision tecnica.
