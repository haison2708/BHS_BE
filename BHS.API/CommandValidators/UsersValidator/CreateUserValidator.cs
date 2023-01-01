using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BHS.API.CommandValidators.UsersValidator;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator(IIdentityService identityService, IConfiguration configuration,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.Identity).Must(userId =>
        {
            try
            {
                var token = identityService.GetToken();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", token);
                    using (var response = httpClient.GetAsync($"{configuration["identityUrl"]}/account/getuserinfo")
                               .Result)
                    {
                        var apiResponse = response.Content.ReadAsStringAsync().Result;
                        var responseDeserializeObject = (JObject)JsonConvert.DeserializeObject(apiResponse)!;
                        return responseDeserializeObject["identity"]!.Value<string>() == userId;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }).WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        RuleFor(x => new { x.Gender, x.Status }).Must(x => x.Gender is 1 or 0 && x.Status is 1 or 0)
            .WithErrorCode(ErrorCode.IncorrectValue).WithMessage(localizer["MustCorrectBitValue"]);
    }
}