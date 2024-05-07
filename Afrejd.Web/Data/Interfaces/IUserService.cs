namespace Afrejd.Web.Data.Interfaces
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetCustomers();
    }
}
