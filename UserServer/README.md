# [Microservice name]

## Running it (development)
1. `docker compose -f docker-compose.yml up`: Starts database in Docker
2. `Add-Migration DBInit` &
3. `Update-Database` to migrate models to database
2. Run service from Intellij or use mvn

## Building it (prod)

1. `docker compose -f docker-compose.prod.yml build`
2. `docker compose -f docker-compose.prod.yml push`

## Problems while running?

1. Remove `./output` (it will remove all databases)
