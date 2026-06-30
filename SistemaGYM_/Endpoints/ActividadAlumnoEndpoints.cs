using SistemaGYM.Logica;

namespace SistemaGYM.Endpoints;

public static class ActividadAlumnoEndpoints
{
    public static void MapActividadAlumnoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/alumnos/{id:int}/actividades/{actividadId:int}",
            async (int id, int actividadId, IActividadAlumnoLogica logica) =>
        {
            var (resultado, data) = await logica.InscribirAsync(id, actividadId);

            return resultado switch
            {
                ResultadoInscripcion.AlumnoNoEncontrado =>
                    Results.Json(new { status = 404, message = "Alumno no encontrado" }, statusCode: 404),
                ResultadoInscripcion.ActividadNoEncontrada =>
                    Results.Json(new { status = 404, message = "Actividad no encontrada" }, statusCode: 404),
                ResultadoInscripcion.YaInscripto =>
                    Results.Json(new { status = 409, message = "El alumno ya está inscripto en esta actividad" }, statusCode: 409),
                _ =>
                    Results.Json(new { status = 201, message = "Inscripción realizada correctamente", data }, statusCode: 201)
            };
        });

        app.MapDelete("/api/alumnos/{id:int}/actividades/{actividadId:int}",
            async (int id, int actividadId, IActividadAlumnoLogica logica) =>
        {
            var resultado = await logica.DarDeBajaAsync(id, actividadId);

            if (resultado == ResultadoInscripcion.InscripcionNoEncontrada)
                return Results.Json(new { status = 404, message = "No se encontró una inscripción activa para ese alumno en esa actividad" }, statusCode: 404);

            return Results.Json(new { status = 204, message = "Inscripción dada de baja correctamente" }, statusCode: 204);
        });

        app.MapGet("/api/actividades/{id:int}/alumnos",
            async (int id, IActividadAlumnoLogica logica) =>
        {
            var (resultado, data) = await logica.ObtenerAlumnosPorActividadAsync(id);

            if (resultado == ResultadoInscripcion.ActividadNoEncontrada)
                return Results.Json(new { status = 404, message = "Actividad no encontrada" }, statusCode: 404);

            if (!data!.Any())
                return Results.Json(new { status = 204, message = "No hay alumnos inscriptos en esta actividad" }, statusCode: 204);

            return Results.Ok(new { status = 200, message = "Alumnos inscriptos obtenidos correctamente", data });
        });
    }
}