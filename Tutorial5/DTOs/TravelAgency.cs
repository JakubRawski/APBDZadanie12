namespace Tutorial5.DTOs;

public class TravelAgency
{
    public class TripDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
        public ICollection<CountryDto> Countries { get; set; }
        public ICollection<ClientDto> Clients { get; set; }
    }
    public class CountryDto
    {
        public string Name { get; set; }
    }
    public class ClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class PaginatedResponseDto<T>
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int AllPages { get; set; }
        public ICollection<T> Trips { get; set; }
        
    }
}