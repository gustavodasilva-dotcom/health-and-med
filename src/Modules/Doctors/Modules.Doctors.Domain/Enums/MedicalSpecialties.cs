using System.ComponentModel.DataAnnotations;
using Modules.Doctors.Domain.Attributes;

namespace Modules.Doctors.Domain.Enums;

public enum MedicalSpecialties
{
    [AppointmentPrice(80.0f)]
    [Display(Name = "General physician")]
    GeneralPhysician = 0,

    [AppointmentPrice(300.0f)]
    [Display(Name = "Allergy and immunology")]
    AllergyAndImmunology = 1,

    [AppointmentPrice(560.0f)]
    Anesthesiology = 2,

    [AppointmentPrice(310.0f)]
    Dermatology = 3,

    [AppointmentPrice(220.0f)]
    [Display(Name = "Diagnostic radiology")]
    DiagnosticRadiology = 4,

    [AppointmentPrice(600.0f)]
    [Display(Name = "Emergency medicine")]
    EmergencyMedicine = 5,

    [AppointmentPrice(450.0f)]
    [Display(Name = "Family medicine")]
    FamilyMedicine = 6,

    [AppointmentPrice(890.0f)]
    [Display(Name = "Internal medicine")]
    InternalMedicine = 7,

    [AppointmentPrice(2189.0f)]
    [Display(Name = "Medical genetics")]
    MedicalGenetics = 8,

    [AppointmentPrice(1562.0f)]
    Neurology = 9,

    [AppointmentPrice(3010.0f)]
    [Display(Name = "Nuclear medicine")]
    NuclearMedicine = 10,

    [AppointmentPrice(450.0f)]
    [Display(Name = "Obstetrics and gynecology")]
    ObstetricsAndGynecology = 11,

    [AppointmentPrice(260.0f)]
    Ophthalmology = 12,

    [AppointmentPrice(490.0f)]
    Pathology = 13,

    [AppointmentPrice(212.0f)]
    Pediatrics = 14,

    [AppointmentPrice(530.0f)]
    [Display(Name = "Physical medicine and rehabilitation")]
    PhysicalMedicineAndRehabilitation = 15,

    [AppointmentPrice(610.0f)]
    [Display(Name = "Preventive medicine")]
    PreventiveMedicine = 16,

    [AppointmentPrice(540.0f)]
    Psychiatry = 17,

    [AppointmentPrice(930.0f)]
    [Display(Name = "Radiation oncology")]
    RadiationOncology = 18,

    [AppointmentPrice(2560.0f)]
    Surgery = 19,

    [AppointmentPrice(1025.0f)]
    Urology = 20
}
