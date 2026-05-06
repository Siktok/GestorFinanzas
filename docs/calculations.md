# Calculos Financieros

Este documento describe las reglas de calculo de la V1. La implementacion principal esta en `FinanzasResumenService` y el dashboard usa esos resultados.

## Saldo De Cuenta

El saldo de una cuenta se calcula asi:

```text
Saldo inicial
+ ingresos confirmados
- gastos confirmados
- transferencias salientes confirmadas
+ transferencias entrantes confirmadas
- importe invertido en inversiones activas financiadas desde esa cuenta
```

Solo los movimientos con estado `Confirmado` afectan al saldo.

## Liquidez Total

La liquidez total es la suma de saldos calculados de las cuentas activas.

Las inversiones activas reducen la liquidez de su cuenta de financiacion por `ImporteInvertido`.

## Inversiones Total

El total de inversiones es la suma de `ValorActual` de inversiones activas.

Una inversion archivada no descuenta liquidez y no suma al total de inversiones.

## Patrimonio Total

El patrimonio total es:

```text
Liquidez total + valor actual de inversiones activas
```

La compra de una inversion no es un gasto. Es un cambio de composicion patrimonial: baja la liquidez y sube el bloque de inversiones.

## Escenarios

Cuenta con `4.000 €`, inversion activa de `1.000 €` financiada desde esa cuenta y valor actual de `1.000 €`:

```text
Liquidez: 3.000 €
Inversiones: 1.000 €
Patrimonio total: 4.000 €
```

Si el valor actual sube a `1.200 €`:

```text
Liquidez: 3.000 €
Inversiones: 1.200 €
Patrimonio total: 4.200 €
```

Si la inversion se archiva:

```text
Liquidez: vuelve a no descontar esa inversion
Inversiones: deja de incluir esa inversion
```

## Balance Mensual

El balance mensual del dashboard usa ingresos y gastos confirmados del mes.

Las inversiones no cuentan como gastos mensuales y las transferencias no afectan al balance mensual.
