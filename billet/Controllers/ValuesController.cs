using System.Web.Http;
using System.Collections.Generic;
using BoletoNet;
using System;

namespace paymentslip.Controllers
{
    public class ValuesController : ApiController
    {
        public byte[] Get()
        {
            BoletoBancario bb = new BoletoBancario();
            bb.CodigoBanco = 237;

            DateTime _dia = DateTime.Now;
            DateTime vencimento = Convert.ToDateTime(_dia.AddDays(5).ToString("dd/MM/yyyy"));

            Cedente c = new Cedente("103.830.576-47", "Lucas Vinícius Batista Costa", "1540", "308813");

            decimal valorboleto = 500.55M;
            Boleto b = new Boleto(vencimento, valorboleto, "02", "01000000001", c);
            b.NumeroDocumento = "01000015235";

            b.Sacado = new Sacado("087.455.636-87", "Amanda Flávia Ribeiro Costa");
            b.Sacado.Endereco.End = "Avenida das tulipas 1060";
            b.Sacado.Endereco.Bairro = "Sapucaias";
            b.Sacado.Endereco.Cidade = "Contagem";
            b.Sacado.Endereco.CEP = "30881-230";
            b.Sacado.Endereco.UF = "MG";

            Instrucao instr = new Instrucao(001);
            instr.Descricao = "Não Receber após o vencimento";
            b.Instrucoes.Add(instr);

            bb.Boleto = b;
            bb.Boleto.Valida();

            return bb.MontaBytesPDF();
        }
    }
}
