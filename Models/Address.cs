namespace MedicalApp.Models;

public class Address
{
    private int _id;
    private string _city;
    private string _street;
    private int _home;

    public Address(int id, string city, string street, int home)
    {
        _id = id;
        _city = city;
        _street = street;
        _home = home;
    }
}