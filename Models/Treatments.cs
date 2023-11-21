namespace MedicalApp.Models;

public class Treatments
{
    private int _id;
    private int _medCarts;
    private int _diseasesID;
    private string _diseasesName;
    private string _typediseases;
    public Treatments(int id, int medCarts, int diseasesId, string diseasesName,string typediseases)
    {
        _id = id;
        _medCarts = medCarts;
        _diseasesID = diseasesId;
        _diseasesName = diseasesName;
        _typediseases = typediseases;
    }

    public string Typediseases => _typediseases;

    public int Id => _id;

    public int MedCarts => _medCarts;

    public int DiseasesId => _diseasesID;

    public string DiseasesName => _diseasesName;
}