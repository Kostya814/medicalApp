using System;
using System.Collections.Generic;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using MedicalApp.Models;
using MySqlConnector;

namespace MedicalApp;

public partial class ListDiseases : Window
{
    private MedicalCarts _medicalCarts;
    private DBHelper db = new DBHelper();
    private List<Treatments> _treatmentsList;
    public ListDiseases(MedicalCarts medicalCarts)
    {
        InitializeComponent();
        _medicalCarts = medicalCarts;
        Update();
    }
    

    private void Update()
    {
        _treatmentsList = new List<Treatments>();
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM treatments " +
                                  "JOIN diseases " +
                                  "ON treatments.DiseasesID = diseases.id " +
                                  "JOIN typediseases " +
                                  "ON diseases.typeDiseases = typediseases.id " +
                                  "WHERE treatments.MedCarts = @MedCarts ";
                cmd.Parameters.AddWithValue("@MedCarts", _medicalCarts.Id);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _treatmentsList.Add(new Treatments(
                        reader.GetInt16("id"),
                        reader.GetInt16("MedCarts"),
                        reader.GetInt16("DiseasesID"),
                        reader.GetString("NameDiseases"),
                        "Тип болезни: " + reader.GetString("TypeName")));
                }
            }
            conn.Close();
        }

        listbox.ItemsSource = _treatmentsList;
    }
}