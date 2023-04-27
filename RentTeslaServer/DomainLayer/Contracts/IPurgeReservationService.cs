namespace RentTeslaServer.DomainLayer.Contracts
{
    public interface IPurgeReservationService
    {
        Task MoveToHistory();
    }
}