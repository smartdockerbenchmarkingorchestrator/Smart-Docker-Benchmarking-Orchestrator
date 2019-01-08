using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers
{
    public static class IsBase64Helper
    {
        //https://stackoverflow.com/questions/6309379/how-to-check-for-a-valid-base64-encoded-string
        public static bool IsBase64(this string base64String, out byte[] bytes)
        {
            bytes = null;
            // Credit: oybek http://stackoverflow.com/users/794764/oybek
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                bytes = Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception)
            {
                // Handle the exception
            }

            return false;
        }
    }
}
