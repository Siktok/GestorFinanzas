# Modelo De Datos

La V1 usa SQLite con EF Core. El esquema se define en `FinanzaNovaDbContext` y se versiona mediante migraciones en `FinanzaNova.Web/Data/Migrations`.

## Entidades

### Cuenta

Representa una fuente de liquidez: banco, efectivo, ahorro, tarjeta u otra.

Campos clave:

- `Nombre`
- `Tipo`
- `SaldoInicial`
- `Activa`
- `FechaCreacion`

El saldo actual no se guarda en la tabla. Se calcula desde movimientos e inversiones activas.

### Categoria

Clasifica ingresos y gastos.

Campos clave:

- `Nombre`
- `Tipo`: `Ingreso` o `Gasto`
- `ColorHex`
- `Activa`

Las transferencias no usan categoria.

### Movimiento

Registra ingresos, gastos y transferencias.

Campos clave:

- `CuentaId`: cuenta origen.
- `CuentaDestinoId`: cuenta destino solo para transferencias.
- `CategoriaId`: categoria para ingresos y gastos.
- `Tipo`: `Ingreso`, `Gasto` o `Transferencia`.
- `Estado`: `Pendiente`, `Confirmado` o `Cancelado`.
- `Fecha`
- `Descripcion`
- `Importe`

`Importe` se guarda siempre como valor positivo. El signo se deduce del tipo de movimiento.

### Inversion

Representa una inversion con valor actual manual.

Campos clave:

- `CuentaReferenciaId`: cuenta de financiacion.
- `Nombre`
- `Tipo`
- `ImporteInvertido`
- `ValorActual`
- `FechaValoracion`
- `Activa`
- `Notas`

`ImporteInvertido` descuenta liquidez de la cuenta de financiacion si la inversion esta activa. `ValorActual` suma al total de inversiones activas.

## Relaciones

- Una cuenta puede tener muchos movimientos como origen.
- Una cuenta puede recibir muchas transferencias como destino.
- Una categoria puede clasificar muchos movimientos.
- Una cuenta puede financiar muchas inversiones.

Las eliminaciones se mantienen conservadoras: movimientos y transferencias restringen borrados de cuentas, y categorias o cuentas de referencia pueden quedar a `null` cuando aplica.
