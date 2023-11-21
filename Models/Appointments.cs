using System;
using Microsoft.VisualBasic;

namespace MedicalApp.Models;

public class Appointments
{
    private int _id;
    private int _patientID;
    private DateTime _AppointmentDate;
    private int _doctorID;
    private int _diseasesid;
    private bool _attendance;
    private string _infoPatient;
    private string _infoDoctor;
    private string _infodiseases;

    public Appointments(int id, int patientId, DateTime appointmentDate, int doctorId, int diseases)
    {
        _id = id;
        _patientID = patientId;
        _AppointmentDate = appointmentDate;
        _doctorID = doctorId;
        _diseasesid = diseases;
    }

    public Appointments(int id, int patientId, DateTime appointmentDate, int doctorId, int diseases, bool attendance, string infoPatient)
    {
        _id = id;
        _patientID = patientId;
        _AppointmentDate = appointmentDate;
        _doctorID = doctorId;
        _diseasesid = diseases;
        _attendance = attendance;
        _infoPatient = infoPatient;
    }
    public Appointments(int id, int patientId, DateTime appointmentDate, int doctorId, int diseases, bool attendance, string infoPatient,string infoDoctor,string infoDiseases)
    {
        _id = id;
        _patientID = patientId;
        _AppointmentDate = appointmentDate;
        _doctorID = doctorId;
        _diseasesid = diseases;
        _attendance = attendance;
        _infoDoctor = infoDoctor;
        _infodiseases = infoDiseases;
    }

    public int Id => _id;

    public int PatientId => _patientID;
    public string InfoPatient => _infoPatient;
    public DateTime AppointmentDate => _AppointmentDate;

    public int DoctorId => _doctorID;

    public string Diseases => _infodiseases;
    public bool Attendance => _attendance;
    
    public string InfoDoctor => _infoDoctor;
}