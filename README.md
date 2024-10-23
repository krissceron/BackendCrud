# BackendCrud

Este proyecto contiene el backend para un sistema de autenticación (login) y CRUD (Create, Read, Update, Delete) de usuarios. Ha sido desarrollado utilizando el framework **ASP.NET Core 8** y está escrito en **C#**. El proyecto está diseñado para manejar la autenticación utilizando **JWT** y garantizar que las operaciones del CRUD estén seguras mediante el uso de tokens de autenticación.

## Descripción

El proyecto **BackendCrud** es una API RESTful diseñada para gestionar usuarios en una aplicación. Incluye funcionalidades de inicio de sesión, registro de nuevos usuarios y operaciones CRUD para la gestión de estos usuarios. La autenticación de los usuarios se maneja a través de **JSON Web Tokens (JWT)**, asegurando que solo los usuarios autenticados puedan realizar ciertas operaciones.

---

## Características

- **Autenticación con JWT**: Protege rutas y asegura que solo los usuarios autenticados puedan acceder a los recursos.
- **CRUD de usuarios**: Permite crear, leer, actualizar y eliminar información de los usuarios registrados.
- **Validación robusta**: Valida los datos ingresados en los formularios de registro e inicio de sesión.
- **Encriptación de contraseñas**: Las contraseñas de los usuarios se almacenan de manera segura utilizando hashing.
- **Framework ASP.NET Core 8**: Desarrollado sobre la última versión del framework .NET, garantizando eficiencia y compatibilidad con las últimas mejoras de la plataforma.

---

## Requisitos

Para poder ejecutar este proyecto, necesitarás tener instalados los siguientes componentes:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) o cualquier base de datos compatible con .NET
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) o [Visual Studio Code](https://code.visualstudio.com/) con la extensión de C#
- **Git** para clonar el repositorio: [Descargar Git](https://git-scm.com/)

---

## Instalación

1. **Clonar el repositorio**:

   ```bash
   git clone https://github.com/krissceron/BackendCrud.git


2. **Navegar al directorio del proyecto:**:

   ```bash
   cd BackendCrud
   
3. **Restaurar las dependencias:**
   Si estás utilizando Visual Studio:
   Abre el archivo .sln en Visual Studio y restaura los paquetes NuGet.
   Si prefieres la línea de comandos:

4. **Configurar la base de datos**:
   Asegúrate de que el servidor SQL esté corriendo y la cadena de conexión esté configurada correctamente en el archivo appsettings.json

## Configuración

En el archivo `appsettings.json`, asegúrate de configurar la cadena de conexión para tu base de datos SQL. A continuación un ejemplo básico:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=CrudDB;User Id=your_user;Password=your_password;"
    }
    
Adicionalmente, asegúrate de configurar las claves para la autenticación JWT:

    ```json
    "Jwt": {
      "Key": "YourSuperSecretKey",
      "Issuer": "BackendCrudIssuer",
      "Audience": "BackendCrudAudience",
      "ExpiresInMinutes": 60
    }

## Ejecución
Para ejecutar el proyecto localmente, utiliza el siguiente comando:
    ```bash
    dotnet run

## Endpoints
### Autenticación
- **POST**/api/auth/register: Registra a un nuevo usuario.
- **POST**/api/auth/login: Inicia sesión y genera un JWT.
### Usuarios (protegido con JWT)
- **GET**/api/users: Obtiene la lista de usuarios (solo para usuarios autenticados).
- **GET**/api/users/{id}: Obtiene los detalles de un usuario específico.
- **POST**/api/users: Crea un nuevo usuario.
- **PUT**/api/users/{id}: Actualiza los datos de un usuario existente.
- **DELETE**/api/users/{id}: Elimina un usuario.

## Seguridad
La API utiliza JWT (JSON Web Tokens) para la autenticación y protección de las rutas. Solo los usuarios autenticados pueden acceder a los endpoints que gestionan usuarios.

Para enviar una solicitud a las rutas protegidas, incluye el token JWT en el encabezado de la solicitud:
    ```bash
    Authorization: Bearer <token>
    
El token se genera cuando el usuario inicia sesión y tiene un tiempo de expiración configurable.

## Contribuciones
Si deseas contribuir al proyecto, sigue los siguientes pasos:

1. Haz un fork del repositorio.
2. Crea una nueva rama (git checkout -b feature/nueva-funcionalidad).
3. Realiza tus cambios y haz commit (git commit -m 'Añadir nueva funcionalidad').
4. Sube los cambios a tu fork (git push origin feature/nueva-funcionalidad).
5. Crea un Pull Request.

## Licencia
Este proyecto está licenciado bajo la MIT License. Consulta el archivo LICENSE para más detalles.

## Contacto
Para cualquier consulta o sugerencia, puedes contactarme a través de:
- **Email:** krissceron03@outlook.com
- **Github:** krissceron

