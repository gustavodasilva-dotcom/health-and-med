namespace Modules.Doctors.Domain.Attributes;

[AttributeUsage(AttributeTargets.All)]
public sealed class AppointmentPriceAttribute(float value)
    : Attribute
{
    public float Value { get; } = value;
}
