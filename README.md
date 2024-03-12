# Proyecto de API Rest en .net

En este proyecto de API, se podrá crear usuarios, hacer login y listar los usuarios registrados.

## 1.- Tener instalado base de datos Postgres
Se conecta por medio de Postgres, se debe tener instalado postgres.

## 2.- Crear la conexión a postgres
Se debe crear la base de datos, usuario y clave según el connection string ubicacion en el appsettings.json

## 3.- Luego ejecutar en el Package Manager Console
Update-Database ya que la API se desarrollo con Code First

## 4.- La tabla creada debe ser "Users"
Se verifica que la tabla se haya creado correctamente por medio de PgAdmin

## 5.- Abrir el swagger
Se puede ejecutar la API y se abria el swagger

## 6.- Primero ejecutar el registro de usuarios: 
	POST /user/registeruser
	{
	  "username": "prueba",
	  "password": "1234"
	}

	Response:
		El campo "Messages", debe indicar: "Registro correcto del usuario"


## 7.- Luego hacer login con el usuario creado: 
	POST /user/login
	{
	  "username": "prueba",
	  "password": "1234"
	}

	Response:
		El campo "Messages", debe indicar: "Login correcto del usuario"



## 8.- Listar usuarios registrados

	Este endpoint esta protegido por el token, este debe tomarse del resultado del login y colocarlo como Bearer Token en la ejecución de este método

	GET /user/users
	
	Response:
		Muestra el listado de usuarios registrados, el Id y el username