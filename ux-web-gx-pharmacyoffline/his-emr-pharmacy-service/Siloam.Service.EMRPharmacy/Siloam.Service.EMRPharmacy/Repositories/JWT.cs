using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Siloam.Service.EMRPharmacy.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Repositories
{
    public enum JwtHashAlgorithm
    {
        HS256,
        HS384,
        HS512
    }

    public class JWT
    {
        private static Dictionary<JwtHashAlgorithm, Func<byte[], byte[], byte[]>> HashAlgorithms;

        static JWT()
        {
            HashAlgorithms = new Dictionary<JwtHashAlgorithm, Func<byte[], byte[], byte[]>>
            {
                { JwtHashAlgorithm.HS256, (key, value) => { using (var sha = new HMACSHA256(key)) { return sha.ComputeHash(value); } } },
                { JwtHashAlgorithm.HS384, (key, value) => { using (var sha = new HMACSHA384(key)) { return sha.ComputeHash(value); } } },
                { JwtHashAlgorithm.HS512, (key, value) => { using (var sha = new HMACSHA512(key)) { return sha.ComputeHash(value); } } }
            };
        }

        public string Encode(object payload, JwtHashAlgorithm algorithm)
        {
            var segments = new List<string>();
            var header = new { alg = algorithm.ToString(), typ = "JWT" };

            byte[] headerBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header, Formatting.None));
            byte[] payloadBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload, Formatting.None));

            segments.Add(Base64UrlEncode(headerBytes));
            segments.Add(Base64UrlEncode(payloadBytes));
            
            var tobeHashed = string.Join(".", segments.ToArray());
            var sha = new HMACSHA256(Encoding.UTF8.GetBytes(ValueStorage.AidoSecret));
            var hashedByteArray = sha.ComputeHash(Encoding.UTF8.GetBytes(tobeHashed));
            
            var securityKey = Base64UrlEncode(hashedByteArray);
            
            var tokenString = string.Join(".", tobeHashed, securityKey);
            return tokenString;
        }

        public string EncodeV2(object payload, JwtHashAlgorithm algorithm, string TargetSignature)
        {
            var segments = new List<string>();
            var header = new { alg = algorithm.ToString(), typ = "JWT" };

            byte[] headerBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header, Formatting.None));
            byte[] payloadBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload, Formatting.None));

            segments.Add(Base64UrlEncode(headerBytes));
            segments.Add(Base64UrlEncode(payloadBytes));

            var tobeHashed = string.Join(".", segments.ToArray());
            var sha = new HMACSHA256(Encoding.UTF8.GetBytes(TargetSignature));
            var hashedByteArray = sha.ComputeHash(Encoding.UTF8.GetBytes(tobeHashed));

            var securityKey = Base64UrlEncode(hashedByteArray);

            var tokenString = string.Join(".", tobeHashed, securityKey);
            return tokenString;
        }

        private static string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0];
            output = output.Replace('+', '-');
            output = output.Replace('/', '_');
            return output;
        }
    }
}
