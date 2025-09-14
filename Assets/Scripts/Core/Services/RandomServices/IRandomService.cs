namespace Core.Services.RandomServices
{
    public interface IRandomService
    {
        int Next(int min, int max);
    }
}