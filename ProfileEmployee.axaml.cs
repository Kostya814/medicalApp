using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using MedicalApp.Models;
using MySqlConnector;
using Tmds.DBus.Protocol;

namespace MedicalApp;

public partial class ProfileEmployee : Window
{
    private List<MedicalCarts> _medicalCartsList;
    private List<Appointments> _appointmentsList;
    private Employee _employee;
    private bool isSearch = true;
    DBHelper db = new DBHelper();
    public ProfileEmployee(Employee employee)
    {
        InitializeComponent();
        _employee = employee;
        string emp = employee.FirstName +" "+ 
                     employee.SecondName+ " " + 
                     employee.LastName+ " | " + 
                     employee.PositionName;
        tbEmployee.Text += emp;
        Update();
        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(3);
        timer.Tick += Timer_Tick;
        timer.Start();
    }
    private void Timer_Tick(object sender, EventArgs e)
    {
        Update();
    }

    public void Update()
    {
        if (tbSearch.Text == "" || tbSearch.Text == null) isSearch = false;
        else isSearch = true;
        if(isSearch) return;
        _appointmentsList = new List<Appointments>();
        _medicalCartsList = new List<MedicalCarts>();
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select * from medicalcarts " +
                                  "join patients " +
                                  "on medicalcarts.PatientID = patients.id";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _medicalCartsList.Add(new MedicalCarts(
                        reader.GetInt16("id"),
                        reader.GetInt16("PatientID"),
                        reader.GetString("FirstName")+ 
                        " " + reader.GetString("SecondName") + 
                        " " + reader.GetString("LastName"),
                        reader.GetDateTime("birthday")));
                }
            }
            conn.Close();
        }
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Appointments " + 
                                  "join patients " +
                                  "on Appointments.PatientID = patients.id "+
                                  "WHERE Appointments.DoctorID = @id and Appointments.Attendance = 0 and AppointmentDate >= @AppointmentDate ";
                cmd.Parameters.AddWithValue("@AppointmentDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", _employee.Id);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _appointmentsList.Add(new Appointments(
                        reader.GetInt16("id"),
                        reader.GetInt16("PatientID"),
                        reader.GetDateTime("AppointmentDate"),
                        _employee.Id,
                        reader.GetInt16("Diseases"),
                        reader.GetBoolean("Attendance"),
                        reader.GetString("FirstName")+ 
                        " " + reader.GetString("SecondName") + 
                        " " + reader.GetString("LastName")));
                }
            }
            conn.Close();
        }
        listmed.ItemsSource = _medicalCartsList;
        ListAppointments.ItemsSource = _appointmentsList;
    }
    private void LogOut(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
        
    }

    private void ToggleButton_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        CheckBox check = sender as CheckBox;
        if (check == null) return;
        if (check.IsChecked == true)
        {
            Appointments item = check.DataContext as Appointments;
            using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Update Appointments " +
                                      "Set Attendance = @Attendance " +
                                      "Where id = @id";
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Attendance", 1);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        Update();
    }

    private void Listmed_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        new ListDiseases(listmed.SelectedItem as MedicalCarts).Show();
    }

    private void Search(object? sender, TextChangedEventArgs e)
    {
        isSearch = true;
        var list = _medicalCartsList.Where(u => u.InfoPatient.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
        listmed.ItemsSource = list;
    }
}