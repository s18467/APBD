namespace StudentVisor.Models;

/// <summary>
/// Interface for entities - models  basic scheme with Id as key value
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Key value of each model.
    /// </summary>
    public string Id { get; set; }
}