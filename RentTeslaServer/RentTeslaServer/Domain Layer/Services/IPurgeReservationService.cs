namespace RentTeslaServer.Domain_Layer.Services
{
    public interface IPurgeReservationService
    {
        Task MoveToHistory();
    }
}