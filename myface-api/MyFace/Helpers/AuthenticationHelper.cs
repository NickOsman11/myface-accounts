namespace MyFace.Helpers
{
    public static class AuthenticationHelper
    {
        public static string DecodeAuthentication(string authenticationData)
        {
            if(authenticationData!= null)
            {
                var base64EncodedData =  authenticationData.Substring("Basic ".Length);
                byte[] base64Bytes = System.Convert.FromBase64String(base64EncodedData);
                string plainText = System.Text.Encoding.UTF8.GetString(base64Bytes);
                // string plainText = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));
                return plainText;  
            }
            return null;              
        }
       
        public static (string, string)? SplitUserNamePassword (string text)
        {
            if(text!= null)
            {
                string[] userNamePassword = text.Split(':');

                string username = userNamePassword[0];
                string password = userNamePassword[1];

                return (username, password);
            }

            return null;
           
        }
    }
}