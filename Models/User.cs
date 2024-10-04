using System;
using System.Collections.Generic;

namespace API.Models;

public partial class User
{
    public int Code { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Path { get; set; }
}
