using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Domain.Entities;

/// <summary>
/// Serves as a base class for all entities in the domain model.
/// it provides shared properties such as a unique identifier (ID).
/// </summary>

public class BaseEntity 
{
 [Key] public int ID { get; set; }
  
}
