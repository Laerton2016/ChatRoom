using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AplicativoCriptografia
{
    public class Cripta
    {
        
        private RSACryptoServiceProvider RSAprincipal;
        private UnicodeEncoding ByteConverter = new UnicodeEncoding();
        private RSAParameters chavePublica, chavePrivada;

        public RSAParameters GetChavePublica()
        {
            return chavePublica;
        }

        public RSAParameters GetChavePrivada()
        {
            return chavePrivada;
        }

        public Cripta()
        {
            RSAprincipal = new RSACryptoServiceProvider();
            //Coletando as chaves publica e privada
            chavePublica = RSAprincipal.ExportParameters(false);
            chavePrivada = RSAprincipal.ExportParameters(true);
        }

        public byte[] RSAEncrypt(string dados,  bool DoOAEPPadding)
        {
            try
            {
                byte[] DataToEncrypt = ByteConverter.GetBytes(dados);
                byte[] encryptedData;
                
                //Cria uma nova instancia do RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    // Importe as informações da chave RSA.Isso só precisa
                    // para incluir as informações da chave pública.
                    RSA.ImportParameters(chavePublica);

                    //Criptografe a matriz de bytes passada e especifique o preenchimento OAEP.
                    //O preenchimento OAEP só está disponível no Microsoft Windows XP ou posterior.
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            
            catch (CryptographicException e)
            {
                throw  new CryptographicException(e.Message);
            }

        }

        public string RSADecrypt(byte[] DataToDecrypt,  bool DoOAEPPadding)
        {
            try
            {

                
                byte[] decryptedData;
                
                
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Importa a chave privada
                    RSA.ImportParameters(chavePrivada);

                    //Discripografa a mensagem
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return ByteConverter.GetString(decryptedData);
            }
            catch (CryptographicException e)
            {
                throw new CryptographicException(e.Message);
            }

        }

    }
}
