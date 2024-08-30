Intcomex Tech Store - API & Frontend
Este proyecto es una solución técnica desarrollada como parte del proceso de selección en Intcomex. Consiste en un API desarrollado en .NET y un frontend en React con Vite, ambos orientados a la gestión y consulta de artículos de tecnología.


Tabla de Contenidos

-Descripción del Proyecto
-Tecnologías Utilizadas
-Requisitos Previos
-Instalación y Configuración
-Estructura del Proyecto
-Ejecutando el Proyecto
-Puntos Clave de Implementación
-Supuestos del Proyecto


Descripción del Proyecto

Este proyecto implementa un sistema de gestión de artículos que permite la consulta y filtrado de artículos por categoría y subcategoría. Adicionalmente, se ha implementado un chat en el frontend que se comunica con el API para realizar estas consultas.


Tecnologías Utilizadas

Backend: .NET 8.0, Entity Framework Core, MySQL
Frontend: React, Vite, Node.js
Base de Datos: MySQL
Autenticación: JWT (JSON Web Token)
Rate Limiting: AspNetCoreRateLimit
Control de Versiones: Git


Requisitos Previos
Antes de comenzar, asegúrate de tener instalados los siguientes requisitos en tu máquina:

-.NET 8.0 SDK
-Node.js y npm
-MySQL
-Git


Instalación y Configuración
Backend (.NET API)

-Clona el repositorio:
git clone https://github.com/Ryuganzh96/IntcomexTechStore.git

-Navega al directorio del proyecto:
cd IntcomexTechStore/IntcomexAPI

-Carga la copia de la base de datos (.sql) que es dejada en el mismo repositorio
ya que es necesario para correr el proyecto

-Configura la cadena de conexión en appsettings.json para conectar con tu base de datos MySQL.

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

-Configura la conexión al backend en los archivos de configuración del frontend.

-Ejecuta el proyecto:
npm run dev

Estructura del Proyecto

-IntcomexAPI/: Contiene el proyecto backend desarrollado en .NET.
-ChatFrontend/: Contiene el proyecto frontend desarrollado en React.
-Models/: Modelos de datos utilizados en el proyecto.
-Controllers/: Controladores del API.
-Services/: Servicios para lógica de negocio.
-Migrations/: Archivos de migraciones para la base de datos.


Ejecutando el Proyecto

Una vez que ambos proyectos estén configurados y corriendo, puedes acceder al frontend a través del navegador (generalmente en http://localhost:5173) y utilizar el API disponible en http://localhost:5000/api.


Puntos Clave de Implementación

-Autenticación: Se utiliza JWT para proteger los endpoints del API.
-Rate Limiting: Se implementó para evitar el abuso de las APIs.
-Paginación: El API permite consultar artículos con paginación para mejorar la eficiencia.
-Validación: Se incluyeron validaciones tanto en el frontend como en el backend para asegurar que los datos recibidos sean correctos.


Supuestos del Proyecto

-Se utilizará MySQL como motor de base de datos.
-El proyecto utiliza .NET 8.0 para el backend.
-Solo usuarios autenticados pueden acceder a los datos del API.
-Se crea una llave en la base de datos que es necesario encriptarla 
con el programa .bat encontrado en el proyecto para poder auntenticarse con el api.
para ahorrar tiempo en el proceso, esta seria la llave esperada RjdnVmIzUDRSMU4ya1Q1eVd6WDBhSA0K
-Se ha implementado un sistema de rate limiting para evitar el abuso del API.
-El frontend y backend están configurados para trabajar localmente con URLs específicas.
