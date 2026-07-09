# Proyecto Restaurante

Proyecto fullstack para gestion de restaurante con backend .NET y frontend en monorepo usando Nx.

## Frontend (Monorepo Nx)

El frontend esta organizado en un monorepo con Nx para centralizar apps, compartir codigo y ejecutar tareas de forma consistente.

- `apps/legacy-angularjs`: aplicacion existente basada en AngularJS.
- `apps/restaurante-v2`: nueva aplicacion frontend basada en Angular.
- `libs/shared/shell`: libreria compartida para logica reutilizable.

### Comunicacion entre aplicaciones

La integracion entre AngularJS y Angular se maneja desde el mismo workspace. El estado compartido entre `legacy-angularjs` y `restaurante-v2` se va a centralizar con Redux para desacoplar la comunicacion y facilitar la migracion progresiva.

## Tecnologias

- Nx (monorepo, grafo de dependencias, ejecucion de tareas, cacheo)
- Angular (nueva aplicacion `restaurante-v2`)
- AngularJS (aplicacion legacy `legacy-angularjs`)
- Redux (estado compartido entre ambas aplicaciones frontend)
- .NET / C# (backend)
- WebSockets (actualizacion de estados y comunicacion en tiempo real)
- SQLite (actual) y SQL Server (objetivo)
- Entity Framework y NHibernate (segun modulo/caso de uso)

## Comandos utiles del frontend

```sh
cd Restaurante.Frontend
pnpm nx show projects
pnpm nx graph
pnpm nx run <project>:build
pnpm nx run <project>:serve
```

## Ideas y roadmap

- Segun el rol, definir el tipo de tarea que cada usuario puede hacer.
- Implementar WebSockets para actualizar estados del pedido.
- Implementar WebSockets para comunicacion con soporte y equipo interno.
- Implementar rol de soporte o reasignar uno existente.
- Implementar mapas para localizacion cliente/restaurante.
- Asignar y consolidar medios de pago y logica de devolucion.
- Evaluar microservicios en modulos que lo requieran.

## Accounts de prueba

Default test accounts are seeded automatically. See your local secrets or environment configuration for credentials.
Do **not** commit real passwords to this file.
