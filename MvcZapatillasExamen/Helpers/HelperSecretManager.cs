using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace MvcZapatillasExamen.Helpers
{
    public static class HelperSecretManager
    {
        public static string GetSecret(string secretName, string region)
        {
            var config = new AmazonSecretsManagerConfig
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(region)
            };

            using var client = new AmazonSecretsManagerClient(config);

            var request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT",
            };
            GetSecretValueResponse response;
            try
            {
                response = client.GetSecretValueAsync(request).Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al recuperar el secreto '{secretName}' de AWS: {ex.Message}", ex);
            }
            return response.SecretString;
        }
    }
}