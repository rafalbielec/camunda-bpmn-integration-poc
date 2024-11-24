# Bpmn Processing Engine Tech Spec

## Platform and libraries

- .NET 6
- PostgreSQL 13.3
- FluentMigrator 3.3.2
- EntityFrameworkCore 6.0.4
- Camunda Platform Run 7.16.0

## PostgreSql connection string used in the solution

```json
"ConnectionStrings": 
{
    "Default": "Server=localhost;Port=5432;Database=bpmn;User Id=postgres;Password=sa123456;"
}
```

## Camunda start command parameters

```console
start.bat --webapps --rest --swaggerui
```
