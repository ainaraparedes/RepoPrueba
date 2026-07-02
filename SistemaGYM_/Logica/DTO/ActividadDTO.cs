namespace SistemaGYM.Logica.DTOs;

public record ActividadDto(
    int ActividadId, 
    string Nombre, 
    string Descripcion, 
    string HoraInicio, 
    string HoraFin, 
    int ProfesorId, 
    string Dias
);

public record ActividadCreateDto(
    string Nombre, 
    string Descripcion, 
    string HoraInicio, 
    string HoraFin, 
    int ProfesorId, 
    string Dias
);