namespace Tutorial5.Services;

public interface IClientService
{
    Task<bool> DeleteClientAsync(int idClient);
}