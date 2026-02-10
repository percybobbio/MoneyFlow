using System.Security.Cryptography;
using System.Text;

namespace MoneyFlow.Utilities
{
    public class Sha256Hasher
    {
        //Este metodo se usa para generar un hash de una cadena de texto, en este caso se usará para generar el hash de la contraseña del usuario antes de guardarla en la base de datos, esto es una buena práctica de seguridad para proteger las contraseñas de los usuarios en caso de que la base de datos sea comprometida.
        public static string ComputeHash(string input)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i< hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
