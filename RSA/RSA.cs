﻿using NBitcoin;
using System;

 namespace RSA
{
    public static class RSA
    {
        //Class to generate key
        public static Wallet KeyGenerate()
        { 
            Key privateKey = new Key();

            var v = privateKey.GetBitcoinSecret(Network.Main).GetAddress();
            var address = BitcoinAddress.Create(v.ToString(), Network.Main);

            return new Wallet { PublicKey = v.ToString(), PrivateKey = privateKey.GetBitcoinSecret(Network.Main).ToString() };
        }

        //Class To Sign Transaction
        public static string Sign(string privKey,string msgToSign)
        {
            //var address = Bitcoin.Address.Create(pubKey,Network.Main);
            //var pkh = (address as IPubkeyHashUsable);

            var secret = Network.Main.CreateBitcoinSecret(privKey);
            var signature = secret.PrivateKey.SignMessage(msgToSign);
            //var bol = pkh.VerifyMessage(msgToSign, signature));
            var v = secret.PubKey.VerifyMessage(msgToSign, signature);
            return signature;
        }

        public static bool Verify(string pbKey, string originalMessage, string signedMessage)
        {
            var address = BitcoinAddress.Create(pbKey, Network.Main);
            var pkh = (address as IPubkeyHashUsable);

            var bol = pkh.VerifyMessage(originalMessage, signedMessage);

            return bol;
        }
    }
}
