namespace ApiEstoqueASP.Services.Interfaces
{
    public interface ISeedUserRoleInitial
    {
        Task SeedRoles();
        Task SeedUsers();
    }
}
