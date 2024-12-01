
namespace ResumeDataAccess.Entities;

public interface IAggregateRoot<T>
{
    T Id { get; set; }
}
