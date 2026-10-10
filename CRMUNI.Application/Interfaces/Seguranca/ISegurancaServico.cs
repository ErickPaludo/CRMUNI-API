using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.Interfaces.Segurança
{
    public interface ISegurancaServico
    {
        (string salt,string hash) CriaSenhaArgon(string senha, string? salt = null);
        bool ValidaSenhaArgon(string senhaBanco, string senha, string salt);
    }
}
