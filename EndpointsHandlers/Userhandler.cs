using loja_api.Mapper.User;
using loja_api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace loja_api.EndpointsHandlers;

public class Userhandler
{

    public static async Task<Results<Ok<IEnumerable<UserDTO>>, NotFound<string>>> GetUsers(UserServices userServices,
                                                                        string? email)
    {
        try
        {
            var user = await userServices.GetUser(email);

            if (user == null) 
                return TypedResults.NotFound("Usuario nao encontrado, Verifique o console para mais erros");

            return TypedResults.Ok(user);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.ToString());
        }
    }

    public static async Task<Results<Ok<UserDTO>, NotFound<string>>> GetUserId(UserServices userServices,
                                                                        Guid Id)
    {
        try
        {
            var user = await userServices.GetUserID(Id);

            if (user == null)
                return TypedResults.NotFound("Usuario nao encontrado, Verifique o console para mais erros");

            return TypedResults.Ok(user);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.ToString());
        }
    }

    public static async Task<Results<Ok<UserDTO>, BadRequest<string>>> CreateUser(UserServices userServices,
                                                                        [FromBody]
                                                                        CreateUserDTO createUser)
    {
        try
        {
            var user = await userServices.CreateUser(createUser);

            if (user == null)
                return TypedResults.BadRequest("Nao foi possivel criar, Verifique o console para mais erros");

            return TypedResults.Ok(user);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }

    public static async Task<Results<Ok<UserDTO>, BadRequest<string>>> UpdateUser(UserServices userServices,
                                                                        [FromBody]
                                                                        UserUpdateDTO UpdateUser)
    {
        try
        {
            var user = await userServices.UpdateUser(UpdateUser);

            if (user == null)
                return TypedResults.BadRequest("Nao foi possivel Alterar, Verifique o console para mais erros");

            return TypedResults.Ok(user);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }

    public static async Task<Results<Ok<string>, BadRequest<string>>>DeleteUser(UserServices userServices,
                                                                        Guid Id)
    {
        try
        {
            var user = await userServices.DeleteUser(Id);

            if (user == null)
                return TypedResults.BadRequest("Nao foi possivel Alterar, Verifique o console para mais erros");

            return TypedResults.Ok(user);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }

    public static async Task<Results<Ok<UserDTO>, BadRequest<string>>> UpdateLogin(UserServices userServices,
                                                                        [FromBody]
                                                                         UserLoginDTO UpdateUser)
    {
        try
        {
            var user = await userServices.UpdateLogin(UpdateUser);

            if (user == null)
                return TypedResults.BadRequest("Nao foi possivel Alterar, Verifique o console para mais erros");

            return TypedResults.Ok(user);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }

    public static async Task<Results<Ok, BadRequest<string>>> Login(UserServices userServices,
                                                    [FromBody]
                                                    UserLoginDTO UpdateUser)
    {
        try
        {
            var user = await userServices.Login(UpdateUser);

            

            if (user == false)
                return TypedResults.BadRequest("Nao foi possivel Alterar, Verifique o console para mais erros");

            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }
}
