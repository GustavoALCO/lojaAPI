using AutoMapper;
using loja_api.Context;
using loja_api.Mapper.Emploree;
using loja_api.Mapper.MarketCart;
using loja_api.Mapper.Product;
using loja_api.Mapper.User;
using MercadoPago.Resource.Preference;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace loja_api.Services;

public class MarketCartService
{

    private readonly ContextDB _DB;

    private readonly IMapper _mapper;

    private readonly ILogger _logger;

    private readonly HashService _hashService;

    private readonly MercadoPagoService _mercadoPagoService;

    public MarketCartService(ContextDB DB, IMapper mapper, ILogger<EmployeeDTO> logger, HashService hashService, MercadoPagoService mercadoPagoService)
    {
        _DB = DB;
        _mapper = mapper;
        _logger = logger;
        _hashService = hashService;
        _mercadoPagoService = mercadoPagoService;
    }

    public async Task<string> CreatePaymant(MarketCartDTO marketCartDTO)
    {
 
        try
        {
            var user = _mapper.Map<UserDTO>(await _DB.Users.FirstOrDefaultAsync(u => u.IdUser == marketCartDTO.UserId));

            if (user == null)
                return "";

            marketCartDTO.AttDate.Assunto.Add("Pedido Realizado");
            marketCartDTO.AttDate.Data.Add(DateTime.UtcNow.ToString());

            var url = await _mercadoPagoService.CreatePaymant(marketCartDTO, user);

            marketCartDTO.MarketCartId = url.Id;
            await _DB.AddAsync(marketCartDTO);

            await _DB.SaveChangesAsync();

            return url.SandboxInitPoint.ToString();

            
        }
        catch (Exception ex)
        {
            return $"{ex.Message}";
        }
    }

    public async Task<Preference> CreatePaymantTest()
    {
        UserDTO user = new UserDTO
        {
            Email = "Teste@gmail.com",
            Name = "TesteUser",
            Surname = "test"
        };

        // Criando um carrinho fictício
        MarketCartDTO market = new MarketCartDTO
        {
            Price = 100,
        };
        try
        {
            var url = await _mercadoPagoService.CreatePaymantTest(market, user);

            return url;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message}");
            return null;
        }
    }

    public async Task<bool> RecibeWebHook(dynamic data)
    {
        //Captura as Variaveis inportantes do json 
        string id = data.id;
        string type = data.type;
        string date = data.date_created;
        
        //Faz Uma chamada ao banco de dados para verificar se o Id passado pelo Json é valido 
        var marketCart = _DB.MarketCart.FirstOrDefault(m => m.MarketCartId == id);

        //Retorna Nulo se caso for nulo
        if (marketCart == null)
            return false;

        //Adiciona Informaçoes passadas em uma lista de Assuntos e Datas
        marketCart.AttDate.Assunto.Add(type);
        marketCart.AttDate.Data.Add(date);

        //Salva as Alteraçoes Feitas
        await _DB.SaveChangesAsync();

        //Retorna True para ser tratado no Endpoint
        return true;
    }
}
