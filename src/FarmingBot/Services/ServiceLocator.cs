namespace FarmingBot.Services
{
    public class ServiceLocator
    {
        public static IUserData GetUserData()
        {
            return new ServiceOperations();
        }

        public static IFarmingOperations GetFarmingOperations()
        {
            return new ServiceOperations();
        }

    }
}