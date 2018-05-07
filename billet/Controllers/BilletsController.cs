using System;
using BoletoNet;
using System.Web.Http;
using billet.ViewModels;

namespace paymentslip.Controllers
{
    public class BilletsController : ApiController
    {
        [HttpPost]
        public byte[] Post([FromBody]Billet obj)
        {
            BoletoBancario bb = new BoletoBancario();
            bb.CodigoBanco = obj.BankCode;

            Cedente c = new Cedente("103.830.576-47", "Lucas Vinícius Batista Costa", "1540", "308813");
            Boleto b = new Boleto(obj.DueDate, obj.Value, "02", "01000000001", c);

            b.NumeroDocumento = "01000015235";

            b.Sacado = new Sacado(obj.PayerDocument, obj.PayerName);
            b.Sacado.Endereco.End = obj.PayerAddress;
            b.Sacado.Endereco.Bairro = obj.PayerDistrict;
            b.Sacado.Endereco.Cidade = obj.PayerCity;
            b.Sacado.Endereco.CEP = obj.PayerCep;
            b.Sacado.Endereco.UF = obj.PayerState;

            Instrucao instr = new Instrucao(001);
            instr.Descricao = "Não receber após o vencimento.";
            b.Instrucoes.Add(instr);

            bb.Boleto = b;
            bb.Boleto.Valida();

            return bb.MontaBytesPDF();
        }
    }
}
