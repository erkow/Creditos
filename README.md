
# Prueba

## Requisitos y Configuración

| Requisito | Detalle |
| :--- | :--- |
| **Entorno** | .NET 6 SDK |
| **Base de Datos** | SQL Server (2019+) |
| **Conexión** | Ajustar `DefaultConnection` y `SegundosReproceso` en `appsettings.json`. |

### Inicio

1.  **Configurar Base de Datos:** Ejecutar los scripts SQL adjuntos
2.  **Ejecutar Proyecto:** Iniciar la API con Visual Studio (F5) .
3.  **Probar:** Acceder a Swagger en la ruta base de la API (swagger) y utilizar los endpoints documentados.

##  Consideraciones

El proyecto sigue los lineamientos establecidos en el documento.

* **Trazabilidad Histórica:** La API está diseñada para insertar siempre nuevos registros en la tabla `RespuestaCreditoFinanciera` (no actualiza) para garantizar una trazabilidad del historial de respuestas.
* **Atomicidad ** La recepción de la respuesta y la notificación al asesor son una operación atómica para evitar inconsistencias en la base.
* **Validación:** Se usa la interfaz nativa IvalidatableObject en el Request para aplicar validaciones antes de procesar la solicitud.
