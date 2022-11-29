using oShopSolution.ViewModels.System.Users;

namespace Admin_site.Interface
{
    public interface IWorkContext
    {
        LoginRequest CurrentUser { get; }
    }
}
