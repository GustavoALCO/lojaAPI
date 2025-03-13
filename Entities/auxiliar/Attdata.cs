using Microsoft.EntityFrameworkCore;

namespace loja_api.Entities.auxiliar;

[Owned]
public class Attdata
{
    public List<string> Assunto { get; set; }    

    public List<DateTime> Data {  get; set; }
}
