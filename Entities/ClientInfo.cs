using System;
using System.Collections.Generic;

namespace CyMvc.Entities;

public partial class ClientInfo
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public int Age { get; set; }

    public string Address { get; set; } = null!;

    public int ZipCode { get; set; }

    public string NameOfFather { get; set; } = null!;

    public string NameOfMother { get; set; } = null!;

    public string Religion { get; set; } = null!;

    public string CivilStatus { get; set; } = null!;

    public string Occupation { get; set; } = null!;
}
