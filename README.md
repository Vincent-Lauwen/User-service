# User-Service

## Manually use docker
1. `docker-compose -f docker-compose.yml up` : start container in Docker

## Need db context to migrate?
1. `Add-Migration DBInit` &
2. `Update-Database` to migrate models to database

## Need to auth with docker?
1. `echo githubtoken | docker login ghcr.io -u Vincent-Lauwen --password-stdin`

## Problems while running?

1. Remove `./output` (it will remove all databases)
