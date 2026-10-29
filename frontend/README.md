# Aula · Frontend

Interfaz web desarrollada con Angular 22 para consumir la API ASP.NET Core de cursos y estudiantes.

## Requisitos

- Node.js 22.12 o superior
- .NET 10 y PostgreSQL con la base configurada en `../appsettings.json`

## Ejecución

1. En `../`, iniciar la API con `dotnet run --launch-profile http` (http://localhost:5275).
2. En `frontend/`, ejecutar `npm install` y luego `npm start`.
3. Abrir http://localhost:4200. El frontend llama directamente a la API en http://localhost:5275; el backend permite este origen mediante CORS.

La aplicación permite listar, buscar, crear, editar y eliminar cursos y estudiantes. Los formularios muestran validaciones y mensajes de respuesta de la API.
