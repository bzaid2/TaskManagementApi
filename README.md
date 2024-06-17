# TaskManagement

Api rest para el seguimiento de tareas realizado con el patrón [Mediator]("https://es.wikipedia.org/wiki/Mediator_(patr%C3%B3n_de_dise%C3%B1o)"), se requiere tener instalado net core 8 para su funcionamiento.

## Instalación de las herramientas de entity framework core tools

Si no se dispone de la siguiente instalación en el equipo donde se ejecutará la creación de la base de datos, no será posible levantar la aplicación. Se puede omitir estos pasos si se cuenta con dotnet cli instalado.

```cmd
Install-Package Microsoft.EntityFrameworkCore.Tools
Update-Package Microsoft.EntityFrameworkCore.Tools
```

## Creación de la base de datos

Es necesario tener la base de datos creada, puesto que la aplicación utiliza la migraciones con el enfoque de "code first"

Podemos crear un contenedor docker con la siguiente línea de comandos. En caso de contar con una instancia de Microsoft SQL server, omitir este comando.

```cmd
docker run -d --name my-mssql-server -p 1433:1433 -v ~/apps/mssql:/var/lib/mssqlql/data -e ACCEPT_EULA=Y -e SA_PASSWORD=mssql1Ipw mcr.microsoft.com/mssql/server:2019-latest
```

Conectarse al servidor MSSQL y crear la base de datos con el nombre "TaskManagement"

Para configurar la cadena de conexión, es necesario abrir el proyecto y localizar el proyecto con nombre "TaskManagement.Host" y localizar el archivo de configuración "appsettings.Development.json". El cuál tendrá la siguiente estructura.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "Application": "{Colocar la cadena de conexión a la base de datos creada}"
  },
  "TokenSettinsg": {
	...
  },
  "SwaggerSettings": {
	...
  }
}
```

Iniciar la "Consola del Administrador de paquetes" y colocar como predeterminado el proyecto TaskManagement.Infraestructure

![Captura de pantalla 2024-06-17 095522](https://i.ibb.co/0260HqT/Captura-de-pantalla-2024-06-17-095522.png)

Escribimos el siguiente comando en la terminal abierta

```cmd
update-database
```

![Captura-de-pantalla-2024-06-17-095815](https://i.ibb.co/mGZQwYM/Captura-de-pantalla-2024-06-17-095815.png)

## Uso del api

Si la configuración de la base de datos fue correcta, al presionar F5 para ejecutar el proyecto, nos cargara la siguiente pantalla del swagger.

![swagger](https://i.ibb.co/WKR5nxp/image.png)



Dentro de la carpeta "Postman" del directorio raíz del proyecto, se encuentran las peticiones para poder realizar las pruebas de una forma más simple, solo es necesario importar la colección.
