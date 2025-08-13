using System;
using API.Entities;

namespace API.Interfaces;

public interface ISpaceBodyService
{
    Task<IReadOnlyList<SpaceBody>> GetAllBodiesAsync();
}
