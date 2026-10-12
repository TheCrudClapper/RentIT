namespace RentIT.Contracts.DTO.UserMicroservice.User;

public record UserResponse(Guid Id, string FirstName, string LastName, string Email, IList<string> Roles);


