namespace StudentSystem.Server.Services.UserServices
{
    public interface IUserService
    {
        Task<string> GetSingleUser();
        Task<List<User>?> DeleteUser(int id);
        Task<List<User>> GetAllUsers();
        Task<Student?> GetSingleStudent();
        Task<int> GetSingleProfessor(int id);

        Task<string> GetUserRole();

        
    }
}
