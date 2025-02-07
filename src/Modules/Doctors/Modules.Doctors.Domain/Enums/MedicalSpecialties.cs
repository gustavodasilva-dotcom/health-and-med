using System.ComponentModel.DataAnnotations;

namespace Modules.Doctors.Domain.Enums;

public enum MedicalSpecialties
{
    [Display(Name = "General physician")]
    GeneralPhysician = 0,

    [Display(Name = "Allergy and immunology")]
    AllergyAndImmunology = 1,

    Anesthesiology = 2,

    Dermatology = 3,

    [Display(Name = "Diagnostic radiology")]
    DiagnosticRadiology = 4,

    [Display(Name = "Emergency medicine")]
    EmergencyMedicine = 5,

    [Display(Name = "Family medicine")]
    FamilyMedicine = 6,

    [Display(Name = "Internal medicine")]
    InternalMedicine = 7,

    [Display(Name = "Medical genetics")]
    MedicalGenetics = 8,

    Neurology = 9,

    [Display(Name = "Nuclear medicine")]
    NuclearMedicine = 10,

    [Display(Name = "Obstetrics and gynecology")]
    ObstetricsAndGynecology = 11,

    Ophthalmology = 12,

    Pathology = 13,

    Pediatrics = 14,

    [Display(Name = "Physical medicine and rehabilitation")]
    PhysicalMedicineAndRehabilitation = 15,

    [Display(Name = "Preventive medicine")]
    PreventiveMedicine = 16,

    Psychiatry = 17,

    [Display(Name = "Radiation oncology")]
    RadiationOncology = 18,

    Surgery = 19,

    Urology = 20
}
