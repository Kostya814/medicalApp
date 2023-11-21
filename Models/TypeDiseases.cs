namespace MedicalApp.Models;

public class TypeDiseases
{
    private int _id;
    private string _typeName;

    public TypeDiseases(int id, string typeName)
    {
        _id = id;
        _typeName = typeName;
    }

    public int Id => _id;

    public string TypeName => _typeName;
}