using System;
using BoletoNet;
using System.Web.Http;
using billet.ViewModels;
using System.Web.Http.Description;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System.IO;
using System.Collections.Generic;

namespace paymentslip.Controllers
{
    [RoutePrefix("api/Billets")]
    public class BilletsController : ApiController
    {
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        [ResponseType(typeof(Billet))]
        public dynamic Create([FromBody]Billet obj)
        {
            try
            {
                BoletoBancario bb = new BoletoBancario();
                bb.CodigoBanco = obj.BankCode;

                Cedente c = new Cedente(obj.Assignor.DocumentNumber, obj.Assignor.Name, obj.Assignor.Agency, obj.Assignor.AccountBank);
                Boleto b = new Boleto(DateTime.Parse(obj.DueDate), obj.Value, obj.WalletNumber, "01000000001", c);

                b.NumeroDocumento = obj.DocumentNumber;

                b.Sacado = new Sacado(obj.Payer.Document, obj.Payer.Name);
                b.Sacado.Endereco.End = obj.Payer.Address;
                b.Sacado.Endereco.Bairro = obj.Payer.District;
                b.Sacado.Endereco.Cidade = obj.Payer.City;
                b.Sacado.Endereco.CEP = obj.Payer.Cep;
                b.Sacado.Endereco.UF = obj.Payer.State;

                Instrucao instr = new Instrucao(001);
                instr.Descricao = "Não receber após o vencimento.";
                b.Instrucoes.Add(instr);

                bb.Boleto = b;
                bb.Boleto.Valida();

                string billetName = string.Format("billet_{0}.pdf", Guid.NewGuid());

                UploadBoleto(bb.MontaBytesPDF(), billetName);

                return new
                {
                    success = true,
                    url = string.Format("https://{0}.blob.core.windows.net/{1}/{2}", Environment.GetEnvironmentVariable("BLOB_AZURE_ACCOUNT_NAME"), Environment.GetEnvironmentVariable("BLOB_AZURE_CONTAINER"), billetName)
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    messages = new List<string> { ex.Message }
                };
            }
        }

        private void UploadBoleto(byte[] billetBytes, string billetName)
        {
            Stream stream = new MemoryStream(billetBytes);

            StorageCredentials credentials = new StorageCredentials(Environment.GetEnvironmentVariable("BLOB_AZURE_ACCOUNT_NAME"), Environment.GetEnvironmentVariable("BLOB_AZURE_ACCESS_KEY"));
            CloudStorageAccount storageAccount = new CloudStorageAccount(credentials, false);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(Environment.GetEnvironmentVariable("BLOB_AZURE_CONTAINER"));

            blobContainer.CreateIfNotExists();

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(billetName);

            blockBlob.UploadFromStream(stream);
        }
    }
}
