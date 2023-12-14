# GoodFood Groupe 1

GoodFood est une société de restauration fondée en 1992, la compagnie est implenté dans plusieurs region de France ainsi qu'en Belgique et au Luxembourg 

## Objectif

Concevoir une solution applicative en microservice avec une application client web et une application client mobile

## Microservices

| Service            | Port  | Langage      | Base de données | Assigné  |
| ------------------ | ----- | ------------ | --------------- | -------- |
| Gateway            | 50000 | TS (Nest.js) |                 | Benjamin |
| Auth               | 50001 | TS (Nest.js) | MongoDB         | Lucile   |
| Product / Basket   | 50002 | Go           | PostgreSQL      | Benjamin |
| Delivery / Order   | 50003 | C# (ASP.NET) | PostgreSQL      | Maxime   |
| Stock / Management | 50004 | C# (ASP.NET) | PostgreSQL      | Benjamin |
| Mailling           | 50005 | JS (NodeJS)  |                 | Lucile   |

## Architecture de dossier

```
.
├── README.md
├── (...) autre fichiers tel que .gitignore, etc...
├── Apps/
│   ├── mobile/
│   ├── web/
│   └── docker-compose.apps.yml
└── Services/
    ├── gateway/
    ├── auth/
    ├── delivery/
    ├── product/
    ├── franchise/
    ├── mailling/
    └── docker-compose.services.yml
```

## Installation

### Docker

Utiliser docker pour executé les microservices et l'api gateway avec cette commande :

```shell
docker-compose up -f services/docker-compose.services.yml
```

Utiliser docker pour executé l'application client web et le build de l'application mobile avec vette commande :

```shell
docker-compose up -f apps/docker-compose.apps.yml
```

## Membres

+Maxime ADLER
+Lucile TRIPER
+Benjamin PERCHEPIED