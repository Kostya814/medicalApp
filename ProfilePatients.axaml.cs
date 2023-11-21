using System;
using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using MedicalApp.Models;
using MySqlConnector;

namespace MedicalApp;

public partial class ProfilePatients : Window
{
    private Patients _patients;
    private List<Appointments> _appointmentsList;
    private DBHelper db = new DBHelper();
    public ProfilePatients(Patients patient)
    {
        _patients = patient;
        InitializeComponent();
        TbPatients.Text+=_patients.FirstName +" "+ _patients.SecondName+ " " + _patients.LastName;
        Update();
        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
        timer.Start();
    
    
    }
    private void Timer_Tick(object sender, EventArgs e)
    {
        Update();
    }

    private void Update()
    {
        _appointmentsList = new List<Appointments>();
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Appointments " +
                                  "join employee " +
                                  "on Appointments.DoctorID = employee.id " +
                                  "join employeeposition " +
                                  "on employee.Position = employeeposition.id " +
                                  "join diseases " +
                                  "on Appointments.Diseases = diseases.id " +
                                  "WHERE Appointments.PatientID = @id ";
                cmd.Parameters.AddWithValue("@id", _patients.Id);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _appointmentsList.Add(new Appointments(
                        reader.GetInt16("id"),
                        reader.GetInt16("PatientID"),
                        reader.GetDateTime("AppointmentDate"),
                        reader.GetInt16("DoctorID"),
                        reader.GetInt16("Diseases"),
                        reader.GetBoolean("Attendance"),
                        "",
                        reader.GetString("FirstName")+ 
                        " " + reader.GetString("SecondName") + 
                        " " + reader.GetString("LastName")+
                        " | " +reader.GetString("NamePosition"),
                        reader.GetString("NameDiseases")
                        ));
                }
            }
            conn.Close();
        }
        ListAppointments.ItemsSource = _appointmentsList;
    }
    private void LogOut(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }

    private void Click(object? sender, RoutedEventArgs e)
    {
        new AddAppointments(_patients).Show();
    }
}