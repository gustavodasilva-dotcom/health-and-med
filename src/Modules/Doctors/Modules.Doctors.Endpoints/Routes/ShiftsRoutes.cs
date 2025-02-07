namespace Modules.Doctors.Endpoints.Routes;

internal static class ShiftsRoutes
{
    public const string Tags = "Doctor's Shifts";

    public const string GetShifts = "api/doctors/shifts/{from:datetime}/{to:datetime}";
    
    public const string GetShiftById = "api/doctors/shifts/{id:guid}";
    
    public const string CreateShift = "api/doctors/shifts";
    
    public const string UpdateShift = "api/doctors/shifts/{id:guid}";
    
    public const string DeleteShift = "api/doctors/shifts/{id:guid}";
}
