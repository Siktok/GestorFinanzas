---
name: skill-standards
description: Estandar local para crear, migrar, revisar y mantener skills de FinanzaNova siguiendo el formato Agent Skills. Usar cuando se creen nuevas skills, se modifiquen skills existentes, se revise su estructura o se documenten convenciones para agentes.
---

# Skill Standards

## Cuando usarla

Usar esta skill antes de crear, migrar, modificar o revisar cualquier skill del proyecto.

## Reglas

- Guardar todas las skills locales bajo `skills/`.
- Cada skill debe vivir en una carpeta propia: `skills/<name>/SKILL.md`.
- El nombre de carpeta y el campo `name` deben coincidir exactamente.
- Usar nombres en kebab-case, en minusculas, sin espacios, sin guiones iniciales o finales y sin guiones dobles.
- Incluir frontmatter YAML con `name` y `description`.
- Escribir `description` indicando que hace la skill y cuando debe usarse.
- Usar el valor de `name` como nombre visible de la skill en indices, enlaces y documentacion.
- No usar `SKILL.md` como etiqueta visible cuando se referencie una skill; usar `nombre-de-la-skill` (`ruta/SKILL.md`).
- Mantener `SKILL.md` breve, accionable y centrado en informacion que otro agente no pueda inferir facilmente.
- Evitar duplicar reglas generales de `AGENTS.md`; enlazar o mencionar la skill relacionada cuando haga falta.
- Mantener las skills alineadas con `README.md`, `docs/` y el estado real de `FinanzaNova.Web`.
- Usar recursos opcionales solo cuando aporten valor real: `scripts/`, `references/` o `assets/`.

## Estructura recomendada

Usar esta plantilla base:

```markdown
---
name: nombre-de-la-skill
description: Que hace la skill y cuando debe usarse.
---

# Nombre de la Skill

## Cuando usarla

## Reglas

## Flujo recomendado

## Validacion
```

## Flujo recomendado

1. Confirmar que la nueva skill captura un flujo reutilizable, no una instruccion puntual.
2. Elegir un nombre corto en kebab-case.
3. Crear `skills/<name>/SKILL.md`.
4. Escribir una `description` suficientemente especifica para que un agente sepa cuando activarla.
5. Mantener las instrucciones en imperativo o infinitivo y con pasos concretos.
6. Actualizar `AGENTS.md` para referenciar la skill si debe estar disponible para agentes futuros.
7. Revisar que no existan skills solapadas que convenga fusionar.

## Validacion

- Confirmar que `skills/<name>/SKILL.md` existe.
- Confirmar que `name` coincide con `<name>`.
- Confirmar que `description` no esta vacia y explica uso y contexto.
- Confirmar que los enlaces relativos a otras skills o recursos funcionan desde la carpeta de la skill.
- Confirmar que la skill no describe como futuro algo que ya existe en la aplicacion.
