Intcomex Tech Store - API & Frontend
Este proyecto es una soluci�n t�cnica desarrollada como parte del proceso de selecci�n en Intcomex. Consiste en un API desarrollado en .NET y un frontend en React con Vite, ambos orientados a la gesti�n y consulta de art�culos de tecnolog�a.


Tabla de Contenidos

-Descripci�n del Proyecto
-Tecnolog�as Utilizadas
-Requisitos Previos
-Instalaci�n y Configuraci�n
-Estructura del Proyecto
-Ejecutando el Proyecto
-Puntos Clave de Implementaci�n
-Supuestos del Proyecto


Descripci�n del Proyecto

Este proyecto implementa un sistema de gesti�n de art�culos que permite la consulta y filtrado de art�culos por categor�a y subcategor�a. Adicionalmente, se ha implementado un chat en el frontend que se comunica con el API para realizar estas consultas.


Tecnolog�as Utilizadas

Backend: .NET 8.0, Entity Framework Core, MySQL
Frontend: React, Vite, Node.js
Base de Datos: MySQL
Autenticaci�n: JWT (JSON Web Token)
Rate Limiting: AspNetCoreRateLimit
Control de Versiones: Git


Requisitos Previos
Antes de comenzar, aseg�rate de tener instalados los siguientes requisitos en tu m�quina:

-.NET 8.0 SDK
-Node.js y npm
-MySQL
-Git


Instalaci�n y Configuraci�n
Backend (.NET API)

-Clona el repositorio:
git clone https://github.com/Ryuganzh96/IntcomexTechStore.git

-Navega al directorio del proyecto:
cd IntcomexTechStore/IntcomexAPI

-Carga la copia de la base de datos (.sql) que es dejada en el mismo repositorio
ya que es necesario para correr el proyecto

-Configura la cadena de conexi�n en appsettings.json para conectar con tu base de datos MySQL.

-Restaura las dependencias del proyecto:
dotnet restore

-Aplica las migraciones para crear la base de datos:
dotnet ef database update

-Ejecuta el proyecto:
dotnet run

Frontend (React)

-Navega al directorio del frontend:
cd IntcomexTechStore/ChatFrontend/chatfrontend.client

-Instala las dependencias:
npm install

-Configura la conexi�n al backend en los archivos de configuraci�n del frontend.

-Ejecuta el proyecto:
npm run dev

Estructura del Proyecto

-IntcomexAPI/: Contiene el proyecto backend desarrollado en .NET.
-ChatFrontend/: Contiene el proyecto frontend desarrollado en React.
-Models/: Modelos de datos utilizados en el proyecto.
-Controllers/: Controladores del API.
-Services/: Servicios para l�gica de negocio.
-Migrations/: Archivos de migraciones para la base de datos.


Ejecutando el Proyecto

Una vez que ambos proyectos est�n configurados y corriendo, puedes acceder al frontend a trav�s del navegador (generalmente en http://localhost:5173) y utilizar el API disponible en http://localhost:5000/api.


Puntos Clave de Implementaci�n

-Autenticaci�n: Se utiliza JWT para proteger los endpoints del API.
-Rate Limiting: Se implement� para evitar el abuso de las APIs.
-Paginaci�n: El API permite consultar art�culos con paginaci�n para mejorar la eficiencia.
-Validaci�n: Se incluyeron validaciones tanto en el frontend como en el backend para asegurar que los datos recibidos sean correctos.


Supuestos del Proyecto

-Se utilizar� MySQL como motor de base de datos.
-El proyecto utiliza .NET 8.0 para el backend.
-Solo usuarios autenticados pueden acceder a los datos del API.
-Se crea una llave en la base de datos que es necesario encriptarla 
con el programa .bat encontrado en el proyecto para poder auntenticarse con el api.
para ahorrar tiempo en el proceso, esta seria la llave esperada RjdnVmIzUDRSMU4ya1Q1eVd6WDBhSA0K
-Se ha implementado un sistema de rate limiting para evitar el abuso del API.
-El frontend y backend est�n configurados para trabajar localmente con URLs espec�ficas.