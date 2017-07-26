# TaskWhenAll
Parallel c# task execution vs execute and wait separately

Le paso un codigo que hice para ir a buscar datos a un servicio para un source para una grilla de manera optima. 
Por ejemplo: por cada registro de la grilla, teníamos que llamar a un servicio para que nos devuelva el nombre por su Id. 

Haciendo uso de los métodos async podemos ejecutar las distintas llamadas al mismo tiempo y aguardar las respuestas, lo que haría que ejecutaramos todas las llamadas en paralelo.

En esta app comparo los dos casos mencionados, ejecutando las tareas con un FOR y haciendo un AWAIT en el bucle y ejecutando todas y haciendo un AWAIT general. Las tareas a ejecutar duran 5 segundos y se ejecutan 10 tareas. Los resultados hablan por si solos:
•	Ejecucion bucle FOR (Cada tarea por separado): 50.000 milisegundos - 50 segundos 
•	Ejecucion simultanea y esperando los resultados: 5.000 milisegundos - 5 segundos 
 
Con bonus track, el código también muestra como manejar las exceptions 😉 
