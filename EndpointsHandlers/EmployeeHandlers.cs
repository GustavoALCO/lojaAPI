using loja_api.Mapper.Emploree;
using loja_api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace loja_api.EndpointsHandlers;

public class EmployeeHandlers
{
    public static async Task<Results<Ok<IEnumerable<EmployeeDTO>>, NotFound<string>>> GetEmployees(EmployeeService EmployeeService,
                                                                       string? email)
    {
        try
        {
            var Employee = await EmployeeService.GetEmployee(email);

            if (Employee == null)
                return TypedResults.NotFound("Usuario nao encontrado, Verifique o console para mais erros");

            return TypedResults.Ok(Employee);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.ToString());
        }
    }

    public static async Task<Results<Ok<EmployeeDTO>, NotFound<string>>> GetEmployeeId(EmployeeService EmployeeService,
                                                                        int Id)
    {
        try
        {
            var Employee = await EmployeeService.GetEmployeeID(Id);

            if (Employee == null)
                return TypedResults.NotFound("Usuario nao encontrado, Verifique o console para mais erros");

            return TypedResults.Ok(Employee);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.ToString());
        }
    }

    public static async Task<Results<Ok<EmployeeDTO>, BadRequest<string>>> CreateEmployee(EmployeeService EmployeeService,
                                                                        [FromBody]
                                                                        EmployeeCreateDTO createEmployee)
    {
        try
        {
            var Employee = await EmployeeService.CreateEmployee(createEmployee);

            if (Employee == null)
                return TypedResults.BadRequest("Nao foi possivel criar, Verifique o console para mais erros");

            return TypedResults.Ok(Employee);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }

    public static async Task<Results<Ok<EmployeeDTO>, BadRequest<string>>> UpdateEmployee(EmployeeService EmployeeService,
                                                                        [FromBody]
                                                                        EmployeeUpdateDTO UpdateEmployee)
    {
        try
        {
            var Employee = await EmployeeService.UpdateEmployee(UpdateEmployee);

            if (Employee == null)
                return TypedResults.BadRequest("Nao foi possivel Alterar, Verifique o console para mais erros");

            return TypedResults.Ok(Employee);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }

    public static async Task<Results<Ok<string>, BadRequest<string>>> DeleteEmployee(EmployeeService EmployeeService,
                                                                        int Id)
    {
        try
        {
            var Employee = await EmployeeService.DeleteEmployee(Id);

            if (Employee == null)
                return TypedResults.BadRequest("Nao foi possivel Alterar, Verifique o console para mais erros");

            return TypedResults.Ok(Employee);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }

    public static async Task<Results<Ok<EmployeeDTO>, BadRequest<string>>> UpdateLogin(EmployeeService EmployeeService,
                                                                        [FromBody]
                                                                         EmployeeLoginDTO UpdateEmployee)
    {
        try
        {
            var Employee = await EmployeeService.UpdateLogin(UpdateEmployee);

            if (Employee == null)
                return TypedResults.BadRequest("Nao foi possivel Alterar, Verifique o console para mais erros");

            return TypedResults.Ok(Employee);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }

    public static async Task<Results<Ok, BadRequest<string>>> Login(EmployeeService EmployeeService,
                                                    [FromBody]
                                                    EmployeeLoginDTO UpdateEmployee)
    {
        try
        {
            var Employee = await EmployeeService.LoginEmployee(UpdateEmployee);



            if (Employee == false)
                return TypedResults.BadRequest("Nao foi possivel Alterar, Verifique o console para mais erros");

            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }
}
