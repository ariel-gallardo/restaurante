# Restaurante Frontend

Repositorio frontend del sistema de restaurante organizado como un monorepo con Nx. La idea es centralizar el desarrollo de las dos aplicaciones del proyecto, compartir código donde tenga sentido y mantener tareas, dependencias y builds coordinados desde un solo workspace.

## Arquitectura

El workspace está dividido en aplicaciones y librerías compartidas:

- `apps/legacy-angularjs`: aplicación existente basada en AngularJS.
- `apps/restaurante-v2`: nueva aplicación frontend basada en Angular.
- `libs/shared/src/lib/shell`: librería compartida para lógica reutilizable entre las aplicaciones.

Nx se usa para administrar el monorepo, resolver dependencias entre proyectos y ejecutar tareas de forma consistente con `pnpm nx`.

## Estado de la comunicación entre apps

La comunicación entre `legacy-angularjs` y `restaurante-v2` se va a centralizar con Redux. Esto permitirá manejar estado compartido de forma predecible, desacoplar el intercambio de datos entre AngularJS y Angular, y dejar una base más ordenada para evolucionar la migración hacia la nueva versión.

## Convenciones de trabajo

- Cada app mantiene su propia responsabilidad de UI y navegación, aunque ambas conviven y se integran durante la transición entre AngularJS y Angular.
- La lógica compartida debe vivir en `libs/` para evitar duplicación.
- El estado global y los eventos compartidos entre las dos aplicaciones se resolverán con Redux.
- Las tareas del workspace deben ejecutarse con Nx para aprovechar cacheo, dependencias y consistencia entre proyectos.

## Comandos útiles

```sh
pnpm nx show projects
pnpm nx graph
pnpm nx run <project>:build
pnpm nx run <project>:serve
```

## Notas

Este README describe la estructura general actual del workspace y la dirección funcional del frontend. A medida que avance la implementación de Redux y la integración entre las dos aplicaciones, se pueden agregar ejemplos de estado compartido, flujos y convenciones de slices o stores.
