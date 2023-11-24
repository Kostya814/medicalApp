using System;
using System.Data.Common;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MedicalApp.Models;
using MySqlConnector;

namespace MedicalApp;

public partial class PayWindow : Window
{
    private DBHelper db = new DBHelper();
    private Appointments _appointments;
    public PayWindow(Appointments appointments)
    {
        _appointments = appointments;
        InitializeComponent();
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Pay(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO transactions (Price,Date,AppointmentsID) " +
                                  "VALUES (@Price,@Date,@AppointmentsID)";
                cmd.Parameters.AddWithValue("@Price", 500);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@AppointmentsID", _appointments.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}