using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAnalyser.Utils
{
    public class Utilitarios
    {

			private static string GetRandomSalt()
			{
			//não alterar esta linha pois se não as senhas vão ficar zuadas
				return BCrypt.Net.BCrypt.GenerateSalt(12);
			}

			public static string HashPassword(string password)
			{
				return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
			}

			public static bool ValidatePassword(string password, string correctHash)
			{
				return BCrypt.Net.BCrypt.Verify(password, correctHash);
			}
	}
}