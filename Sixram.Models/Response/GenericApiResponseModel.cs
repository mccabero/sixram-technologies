namespace Sixram.Models.Response
{
    public record struct GenericApiResponseModel(int ResponseCode, string? ErrorMessage);
}
