namespace StudentSystem.Server.Services.UserServices
{
    public interface IUserService
    {
        Task<string> GetSingleUser();
        Task<List<User>?> DeleteUser(int id);
        Task<List<User>> GetAllUsers();
        Task<Student?> GetSingleStudent();
        Task<string> GetUserRole();
        Task<string> GetUserId();
        Task<string> GetUserEmail();


    }
}
