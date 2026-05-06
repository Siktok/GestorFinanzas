---
name: finance-conventions
description: Convenciones financieras de FinanzaNova para formatos, colores, estados, lenguaje y reglas de calculo visibles. Usar cuando una tarea muestre o modele fechas, importes, porcentajes, saldos, movimientos, inversiones, estados financieros, etiquetas visuales o textos de dominio financiero.
---

# Finance Conventions

## Cuando usarla

Usar esta skill en cualquier tarea que afecte datos financieros visibles, lenguaje de dominio, tablas, formularios, estados, importes, porcentajes, fechas, saldos, movimientos o indicadores.

## Reglas

- Usar textos visibles en espanol.
- Mantener tono claro, operativo y no promocional.
- Priorizar consistencia y legibilidad sobre decoracion.
- No depender solo del color para comunicar significado; acompanarlo con texto, etiqueta o icono accesible.
- Aplicar estas convenciones en UI, ViewModels, datos de ejemplo, tablas y mensajes visibles.
- Respetar las reglas de calculo documentadas en `docs/calculations.md` y centralizadas en `FinanzasResumenService`.

## Fechas

- UI en espanol: `dd/MM/yyyy`.
- Fecha y hora: `dd/MM/yyyy HH:mm`.
- Datos tecnicos o persistencia: ISO `yyyy-MM-dd` o `yyyy-MM-ddTHH:mm:ss` cuando aplique.
- Evitar fechas ambiguas sin ano completo, por ejemplo `05/06/26`.
- Usar nombres claros para vencimientos: `Fecha de vencimiento`, `Fecha de movimiento`, `Ultima actualizacion`.

## Importes

- Moneda por defecto: EUR.
- Formato UI: `1.234,56 €`.
- Importes negativos: `-123,45 €`, sin parentesis.
- Mostrar dos decimales en importes monetarios.
- Usar `Importe`, `Saldo`, `Ingreso`, `Gasto` y `Transferencia` de forma consistente.
- Evitar mezclar moneda y porcentaje en la misma columna sin etiquetas claras.
- Guardar `Movimiento.Importe` como valor positivo; el signo se deduce desde `MovimientoTipo`.

## Porcentajes

- Formato UI: `12,34 %`.
- Mostrar dos decimales por defecto.
- Usar un decimal solo en metricas simples donde mejore claramente la lectura.
- Diferenciar puntos porcentuales de porcentaje relativo cuando el texto lo requiera.

## Colores semanticos

- Positivo, ingreso o confirmado: verde.
- Negativo, gasto o error: rojo.
- Pendiente o advertencia: ambar.
- Informativo o neutro: azul o gris.
- Mantener suficiente contraste y no usar colores saturados como unico recurso visual.

## Estados base actuales

- `Pendiente`: movimiento registrado pero no confirmado.
- `Confirmado`: movimiento valido o conciliado; es el unico estado que afecta saldos y dashboard.
- `Cancelado`: movimiento anulado; no afecta saldos ni balance.
- Para inversiones, usar el booleano `Activa`; en UI puede mostrarse como `Activa` o `Archivada` segun el contexto.
- No introducir estados nuevos como `Vencido` o `Archivado` en movimientos salvo que la funcionalidad lo pida.

Para movimientos financieros, usar:

- `Ingreso`
- `Gasto`
- `Transferencia`

## Reglas de calculo V1

- El saldo de una cuenta no se persiste como valor actual; se calcula desde saldo inicial, movimientos confirmados e inversiones activas.
- Las transferencias mueven liquidez entre cuentas y no usan categoria.
- Las inversiones activas descuentan `ImporteInvertido` de la cuenta de financiacion y suman `ValorActual` al total de inversiones.
- Una inversion no es un gasto mensual ni debe registrarse como movimiento de tipo `Gasto`.
- El patrimonio total es `Liquidez total + valor actual de inversiones activas`.

## Lenguaje

- Usar terminos consistentes: `importe`, `saldo`, `movimiento`, `categoria`, `cuenta`, `vencimiento`.
- Preferir etiquetas breves en tablas y formularios.
- Evitar lenguaje de marketing en pantallas operativas.
- Usar mensajes de error accionables, por ejemplo `Introduce un importe valido`.

## Validacion

- Revisar que importes, fechas y porcentajes usen formato espanol.
- Revisar que estados y colores coincidan con estas convenciones.
- Revisar que movimientos, transferencias e inversiones respetan las reglas de calculo V1.
- Revisar que las tablas o tarjetas financieras no dependan solo del color para explicar el estado.
- Ejecutar `dotnet build FinanzaNova.slnx` si la tarea modifica vistas, modelos o codigo.
