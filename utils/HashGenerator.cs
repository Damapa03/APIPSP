using System.Security.Cryptography;
using System.Text;

namespace Fuerza_Bruta;

public class HashGenerator
{
    public string GenerarHashSHA256(string texto)
    {
        // Crear el objeto SHA256
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convertir el texto a bytes
            byte[] bytesTexto = Encoding.UTF8.GetBytes(texto);

            // Calcular el hash
            byte[] hashBytes = sha256.ComputeHash(bytesTexto);

            // Convertir los bytes del hash a una representación hexadecimal
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2")); // Formato hexadecimal (dos dígitos)
            }

            return sb.ToString();
        }
    }
}