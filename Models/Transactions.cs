using Microsoft.VisualBasic;

namespace MedicalApp.Models;

public class Transactions
{
    private int _id;
    private double _price;
    private DateAndTime _date;
    private int _appointmentsID;

    public Transactions(int id, double price, DateAndTime date, int appointmentsId)
    {
        _id = id;
        _price = price;
        _date = date;
        _appointmentsID = appointmentsId;
    }

    public int Id => _id;

    public double Price => _price;

    public DateAndTime Date => _date;

    public int AppointmentsId => _appointmentsID;
}