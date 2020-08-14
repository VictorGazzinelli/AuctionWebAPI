﻿using AuctionWebAPI.Utils.Cryptography;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AuctionWebAPI.Util.Cryptography
{
    public class Cryptographer
    {
        static byte[] v_salt = Encoding.UTF8.GetBytes("MySecret");
        static String password = "MySecretPassword";

        public Cryptographer()
        {
        }

        public static string Decrypt(string cipherText)
        {
            PKCSKeyGenerator crypto = new PKCSKeyGenerator(password, v_salt, 17, 1);

            ICryptoTransform cryptoTransform = crypto.Decryptor;
            byte[] cipherBytes = System.Convert.FromBase64String(cipherText);
            byte[] clearBytes = cryptoTransform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(clearBytes);
        }

        public static string Encrypt(string cipherText)
        {
            PKCSKeyGenerator crypto = new PKCSKeyGenerator(password, v_salt, 17, 1);

            ICryptoTransform cryptoTransform = crypto.Encryptor;
            byte[] cipherBytes = Encoding.UTF8.GetBytes(cipherText);
            byte[] clearBytes = cryptoTransform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Convert.ToBase64String(clearBytes);
        }
    }
}
