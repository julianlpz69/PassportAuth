# PassportAuth con Google

* Paquetes necesarios
  * Microsoft.AspNetCore.Authentication.Google
    
    ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/ec6ccfce-5575-4334-88e4-cc280fcbf238)

* Microsoft.AspNetCore.OpenApi
  
    ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/8fd4afbc-ba1a-492b-9931-731b7c132a6b)


## Configuracion

Antes de comenzar necesitamos obtener las claves de la API de GOOGLE, para obtenerlas debemos cumplir con los siguientes pasos:

* Entrar y registrarnos en el siguiente enlace: https://console.developers.google.com/

* Una vez registrados, En la seccion de Apis y Sevicios habilitados damos click en la opcion de "CREAR PROYECTO"
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/32f31b2b-78a8-486b-b866-369bbeb6492a)

* Ponemos el nombre al nuevo proyecto y le damos en "CREAR"
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/edb5f528-15f4-4741-b22a-a5596a4554c2)

* Despues no dirigimos a la seccion de "Pantalla de consetimiento"
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/44f5cc4a-bd15-453b-83e1-c4a6b42ec7e1)


* Seleccionamos la opcion de "Externos" y le damos click en "CREAR"
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/e1912b47-bf99-4203-85a5-24fbf53da5c8)


* El siguiente paso es completar los datos requeridos, que son datos basicos como nombre del proyecto y un correo electronico
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/b6852ae8-2f6a-470f-b59c-b58ea481cafc)

* Mas abajo tambien ponemos el correo electronico y le damos click en "GUARDAR Y CONTINUAR"
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/2af036fe-d946-4cd2-a5fb-d446f54477d4)

* En lo siguiente le tambien le damos click en "GUARDAR Y CONTINUAR"
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/f29ec443-e31c-4fe7-8f35-ea56657f746a)

* Los pasos del 2 al 4 simplemente le damos click en guardar y continuar

* Una vez terminado con lo anterior, nos dirigimos a la seccion de "Credenciales" y damos click en "CREAR CREDENCIALES"
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/006671b6-378a-4a31-9692-b8f256178276)

* Nos apareceran 4 opciones, debemos seleccionar la que dice "ID de cliente de OAuth"
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/bef9785d-0c48-4eaa-bb39-b11766f80457)

* Seleccionamos "Aplicacion web" y ponemos un nombre
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/de95b009-40d7-4b85-a234-3e07909df8ba)

* En el siguiente paso lo que debemos hacer es configurar las Uri
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/29b86e7e-2317-47a6-a116-336a70723303)

* En la primera colocamos la ruta https de nuestro programa, que la podemos encontrar en el archivo Json de Properties o al hacer dotnet run, tambien obtenemos la ruta, importante tener en cuenta que debe ser "HTTPS"
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/679047d5-c2d2-4019-82a4-bf45f59aa9fc)
  

  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/c302f571-96c1-43cf-9631-972cf0cd5655)

* En la segunda ponemos la misma ruta solo que le agregamos lo siguiente: /signin-google, se utiliza para que Google redireccione a los usuarios de vuelta a nuestra aplicación después de una autenticación exitosa.

* Despues de poner la segunda ruta le damos click en "CREAR" y nos dará el cliente ID y el secreto del Cliente, que son los que necesitamos para continuar con la configuracion
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/aa9265ec-23ca-490b-9357-d1d492699c48)

## Configuracion del program

  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/a24ae29e-120e-41d1-9501-1efd8ae71958)

* Esta sección es importante para la autenticación con Passport usando como proveedor externo a Google. Aquí configuramos el esquema de autenticación predeterminado como CookieAuthentication, 
  lo que permite que nuestra aplicación utilice cookies para gestionar las sesiones de usuario. Además, agregamos la autenticación de Google como una opción, donde debemos poner el ClientId y el ClientSecret 
  obtenidos del Google Developer Console. También se mapean algunas reclamaciones personalizadas, como la imagen del perfil del usuario.

* Nos aseguramos de configurar los aspectos necesarios del procesamiento de solicitudes y respuestas
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/d31c4d2b-9f1d-4bb7-8a51-3b2ee0df6c60)


## Configuracion del Controlador
* El primer endpoind que usamos es el de "google-login", donde lo que estamos haciendo basicamente es redirigir a los usuarios al inicio login de google, despues en esta linea de codigo "var properties = new AuthenticationProperties"
  lo que estamos haciendo es crear las propiedades de autenticación que controlan el comportamiento de la autenticación con Google. En este caso, se configura la URL de redirección después de la autenticación
  y se establece la propiedad "prompt" en "select_account" para forzar la selección de una cuenta de Google incluso si ya hay una sesión iniciada, luego en la linea "return Challenge(properties, GoogleDefaults.AuthenticationScheme);"
  se inicia el proceso de autenticación utilizando el esquema de autenticación de Google (GoogleDefaults.AuthenticationScheme). Esto redirigirá al usuario a la página de inicio de sesión de Google, donde proporcionará sus credenciales
  y luego será redirigido de vuelta a la aplicación después de recibir la autenticación exitosa.
  
    ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/08ffc582-3400-45af-a9fe-add47530ddea)
    
* En el segundo metodo "google-response", en esta linea: "var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);" lo que estamos realizando es la autenticación del usuario, utilizando el esquema de autenticación
  de cookies (CookieAuthenticationDefaults.AuthenticationScheme). Esto significa que se verifica si el usuario está autenticado y se establece su identidad en función de la información de autenticación almacenada en las cookies,
  en la siguiente linea "var claims = result.Principal.Identities.FirstOrDefault()..." se extraen las reclamaciones (claims) del usuario autenticado, como su nombre, correo electrónico, etc. Estas reclamaciones las almacenamos
  en una estructura de datos JSON para ser devueltas como respuesta. y finalmente en esta otra linea "return Json(claims);", se devuelve una respuesta JSON que contiene las reclamaciones del usuario autenticado. Esto permite
  que nuestra aplicación acceda a la información del usuario después de la autenticación.
  
  ![image](https://github.com/julianlpz69/PassportAuth/assets/133609079/2f3d4544-9196-4630-8c51-7f21ddd0c914)

  
### Link video Youtube
  https://youtu.be/nVdXit3sRmk
