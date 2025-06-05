using System;
using System.Collections.Generic;

namespace Tutorial5.Models;

public partial class Trip
{
    public Trip()
    {
        ClientTrips = new HashSet<ClientTrip>();
        CountryTrips = new HashSet<CountryTrip>();
    }
    public int IdTrip { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int MaxPeople { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; }
    public virtual ICollection<CountryTrip> CountryTrips { get; set; }
    
}
