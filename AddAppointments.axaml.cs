using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MedicalApp.Models;
using Microsoft.VisualBasic;
using MySqlConnector;

namespace MedicalApp;

public partial class AddAppointments : Window
{
    private Patients _patients;
    private List<Employee> _employees;
    private List<Diseases> _diseases;
    private DBHelper db = new DBHelper();
    public AddAppointments(Patients patients)
    {
        _patients = patients;
        InitializeComponent();
        Update();
    }

    public void Update()
    {
        _employees = new List<Employee>();
        _diseases = new List<Diseases>();
        using (var connection = new MySqlConnection(db._connectionString.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Employee " +
                                      "JOIN EmployeePosition " +
                                      "ON EmployeePosition.id = Employee.Position";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _employees.Add(new Employee(reader.GetInt16("id"),
                        reader.GetString("login"),
                        reader.GetString("password"),
                        reader.GetString("FirstName"),
                        reader.GetString("SecondName"),
                        reader.GetString("LastName"),
                        reader.GetDateTime("birthday"),
                        reader.GetString("Gender"),
                        reader.GetInt16("Position"),
                        reader.GetString("NamePosition"),
                        reader.GetInt16("Address")));
                }
            }
            connection.Close();
        }
        using (var connection = new MySqlConnection(db._connectionString.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM diseases " +
                                      "join typediseases " +
                                      "on diseases.typeDiseases = typediseases.id";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _diseases.Add(new Diseases(
                        reader.GetInt16("id"),
                        reader.GetString("NameDiseases"),
                        reader.GetInt16("typeDiseases"),
                        reader.GetString("TypeName")));
                }
            }
            connection.Close();
        }

        CbDiseases.ItemsSource = _diseases;
        CbDoctors.ItemsSource = _employees;
    }
    private void Insert(object? sender, RoutedEventArgs e)
    {
        if (CbDiseases.SelectedItem == null || CbDoctors.SelectedItem ==null) return;
        
        DateTime selectedDate = Date.SelectedDate.Value.DateTime;
        TimeSpan selectedTime = Time.SelectedTime.GetValueOrDefault();
        DateTime date = selectedDate + selectedTime;
        using (var connection = new MySqlConnection(db._connectionString.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO appointments (PatientID, AppointmentDate, DoctorID, Diseases) " +
                                      "VALUES (@PatientID,@AppointmentDate,@DoctorID,@Diseases)";
                command.Parameters.AddWithValue("@PatientID",_patients.Id);
                command.Parameters.AddWithValue("@AppointmentDate",date);
                command.Parameters.AddWithValue("@DoctorID",(CbDoctors.SelectedItem as Employee).Id);
                command.Parameters.AddWithValue("@Diseases",(CbDiseases.SelectedItem as Diseases).Id);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}