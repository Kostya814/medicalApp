namespace MedicalApp.Models;

public class Diseases
{
    private int _id;
    private string _nameDiseases;
    private int _typeDiseases;
    private string _typeDiseasesName;
    

    public Diseases(int id, string nameDiseases, int typeDiseases, string typeDiseasesName)
    {
        _id = id;
        _nameDiseases = nameDiseases;
        _typeDiseases = typeDiseases;
        _typeDiseasesName = typeDiseasesName;
        
    }

    public int Id => _id;

    public string NameDiseases => _nameDiseases;

    public int TypeDiseases => _typeDiseases;

    public string TypeDiseasesName => _typeDiseasesName;
    

    
}