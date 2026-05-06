---
name: finance-conventions
description: Convenciones financieras de FinanzaNova para formatos, colores, estados y lenguaje. Usar cuando una tarea muestre o modele fechas, importes, porcentajes, saldos, movimientos, estados financieros, etiquetas visuales o textos de dominio financiero.
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

## Estados base

- `Pendiente`: operacion registrada pero no confirmada.
- `Confirmado`: operacion valida o conciliada.
- `Cancelado`: operacion anulada.
- `Vencido`: obligacion o pago fuera de plazo.
- `Archivado`: elemento fuera del flujo activo.

Para movimientos financieros, usar:

- `Ingreso`
- `Gasto`
- `Transferencia`

## Lenguaje

- Usar terminos consistentes: `importe`, `saldo`, `movimiento`, `categoria`, `cuenta`, `vencimiento`.
- Preferir etiquetas breves en tablas y formularios.
- Evitar lenguaje de marketing en pantallas operativas.
- Usar mensajes de error accionables, por ejemplo `Introduce un importe valido`.

## Validacion

- Revisar que importes, fechas y porcentajes usen formato espanol.
- Revisar que estados y colores coincidan con estas convenciones.
- Revisar que las tablas o tarjetas financieras no dependan solo del color para explicar el estado.
- Ejecutar `dotnet build FinanzaNova.slnx` si la tarea modifica vistas, modelos o codigo.
