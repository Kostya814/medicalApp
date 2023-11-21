using System;

namespace MedicalApp.Models;

public class MedicalCarts
{
    private int _id;
    private int _patientID;
    private string _infoPatient;
    private DateTime _date;

    public MedicalCarts(int id, int patientId, string infoPatient,DateTime dateTime)
    {
        _id = id;
        _patientID = patientId;
        _infoPatient = infoPatient;
        _date = dateTime;
    }

    public int Id => _id;

    public int PatientId => _patientID;

    public string InfoPatient => _infoPatient;
   
    public DateTime DateTime => _date;
   
         
        
   
}