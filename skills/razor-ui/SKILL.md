---
name: razor-ui
description: Guia para disenar y ajustar la interfaz Razor de FinanzaNova.Web. Usar cuando la tarea implique vistas Razor, layout, navegacion, estilos CSS, Bootstrap, componentes visuales o contenido visible para usuarios.
---

# Razor UI

## Cuando usarla

Usar esta skill al modificar vistas Razor, layout, navegacion, CSS, Bootstrap o cualquier parte visible de la interfaz de `FinanzaNova.Web`.

## Reglas

- Usar Razor MVC y Bootstrap local ya incluido en `wwwroot/lib`.
- Centralizar estilos propios en `FinanzaNova.Web/wwwroot/css/site.css` cuando sea razonable.
- Mantener una interfaz sobria, clara y adecuada para gestion financiera.
- Priorizar legibilidad, responsive basico y navegacion predecible.
- No introducir frameworks frontend, bundlers ni librerias visuales nuevas sin peticion explicita.
- Mantener pantallas operativas de dashboard, CRUD y tablas antes que landing pages o marketing visual pesado.
- Usar `CultureInfo.GetCultureInfo("es-ES")` o la cultura ya configurada para importes y fechas en vistas.
- Consultar `finance-conventions` (`../finance-conventions/SKILL.md`) para formato de fechas, importes, porcentajes, estados, colores y lenguaje financiero.

## Flujo recomendado

1. Revisar `_Layout.cshtml`, la vista afectada y `site.css`.
2. Reutilizar clases Bootstrap antes de crear CSS nuevo.
3. Mantener componentes visuales simples: contenedores, tablas, formularios, alertas y botones claros.
4. Usar textos visibles en espanol y orientados al uso financiero.
5. Comprobar que las vistas reflejan correctamente cuentas, movimientos, categorias e inversiones cuando usen datos persistidos.
6. Evitar cambios globales de layout si la tarea solo afecta a una vista.

## Validacion

- Comprobar que las vistas Razor compilan con `dotnet build FinanzaNova.slnx`.
- Revisar que no haya texto roto, placeholders innecesarios ni estilos globales excesivos.
- Si se arranca la app localmente, revisar la pagina afectada en navegador.
