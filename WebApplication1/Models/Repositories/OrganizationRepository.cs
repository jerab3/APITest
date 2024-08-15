using WebApplication1.Filters;

namespace WebApplication1.Models.Repositories;

public static class OrganizationRepository
{
    private static List<Organization> organizations = new() {
        new Organization { Id=0, Name="Gatema", NumberOfEmployees=100},
        new Organization { Id=1, Name="Alza", NumberOfEmployees=600},
        new Organization { Id=2, Name="Google", NumberOfEmployees=250000}
    };

    public static bool OrganizationExists(int id)
    {
        return organizations.Any(x => x.Id == id);
    }

    public static Organization? GetOrganizationById(int id)
    {
        return organizations.FirstOrDefault(x => x.Id == id);
    }
    public static Organization? GetOrganizationByName(string name)
    {
        return organizations.FirstOrDefault(x => 
            !string.IsNullOrWhiteSpace(name) 
            && x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
    public static List<Organization> GetOrganizations()
    {
        return organizations;
    }

    public static void AddOrganization(Organization organization)
    {
        int maxId = organizations.Max(x => x.Id);
        organization.Id = maxId+1;

        organizations.Add(organization);
    }

    public static void UpdateOrganization(Organization organization)
    {
        var organizationToUpdate = organizations.First(x => x.Id ==organization.Id);

        organizationToUpdate.Name = organization.Name;
        organizationToUpdate.NumberOfEmployees = organization.NumberOfEmployees;
    }
}
