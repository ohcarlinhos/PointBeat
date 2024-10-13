namespace PointBeat.App;

public record UserDto(string Name, string Email, string AddressStreet, int AddressNumber);

public record PointDto(DateTime Hour, string UserId, string CompanyId);

public record CompanyDto(string Name, string AddressStreet, int AddressNumber);