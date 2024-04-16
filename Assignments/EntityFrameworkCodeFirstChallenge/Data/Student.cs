﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCodeFirstChallenge.Data {
  public class Student {
    [Key] public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}
