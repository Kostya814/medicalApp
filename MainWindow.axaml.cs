using System.Collections.Generic;
using System.Data;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MedicalApp.Models;
using MySqlConnector;
using Tmds.DBus.Protocol;

namespace MedicalApp;

public partial class MainWindow : Window
{
    private List<Patients> _patients;
    private List<Employee> _employees;
    public MainWindow()
    {
        InitializeComponent();
        Update();
    }

    private void Update()
    {
        _employees = new List<Employee>();
        _patients = new List<Patients>();
        DBHelper db = new DBHelper();
        using (var connection = new MySqlConnection(db._connectionString.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Patients";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _patients.Add(new Patients(reader.GetInt16("id"),
                        reader.GetString("login"),
                        reader.GetString("password"),
                        reader.GetString("FirstName"),
                        reader.GetString("SecondName"),
                        reader.GetString("LastName"),
                        reader.GetDateTime("birthday"),
                        reader.GetString("Gender"),
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
    }

    private void logIn(object? sender, RoutedEventArgs e)
    {
        foreach (var aEmployee in _employees)
        {
            if(!aEmployee.log(tbUserName.Text, tbPassword.Text)) continue;
            new ProfileEmployee(aEmployee).Show();
        }
        foreach (var patient in _patients)
        {
            if(!patient.log(tbUserName.Text, tbPassword.Text)) continue;
            new ProfilePatients(patient).Show();
        }
        Close();
    }
}