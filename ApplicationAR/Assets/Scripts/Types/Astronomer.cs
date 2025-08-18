using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Astronomer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string BirthPlace { get; set; }
    public string ImageUrl { get; set; }
    public AstronomerOccupation Occupation { get; set; }
    public string Description { get; set; }
    public bool Married { get; set; }
    public string Gender { get; set; }
    public int TelescopeAmount { get; set; }
}

public enum AstronomerOccupation
{
    Scientist,
    Hobbyist,
    Astronaut,
    Researcher
}