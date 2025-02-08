namespace Modules.Doctors.Application.Constants;

internal static class ErrorConstants
{
    public const string RegistrationNumberIsAlreadyInUseMessage = "The doctor's registration number is already in use.";

    public const string ShiftUnavailableMessage = "There's already a scheduled shift for this doctor at this specific period.";

    public const string ShiftNotFoundMessage = "Shift not found.";

    public const string NoDoctorWasFoundWithTheGivenRegistrationNumberMessage = "No doctor was found with the given registration number.";

    public const string InvalidOperationTitle = "Invalid operation";

    public const string NotFoundTitle = "Not found";
}
