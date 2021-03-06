﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockChainClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockChainClient.Api
{
    [Produces("application/json")]
    [Route("")]
    public class BlockChainClientController : ControllerBase
    {
        //Creatte new wallet and get(generate) a new key
        [HttpGet("wallet/new")]
        public IActionResult new_wallet()
        {
            var wallet = RSA.RSA.KeyGenerate();
            var rsp = new
            {
                private_key = wallet.PrivateKey,
                public_key = wallet.PublicKey
            };
            return Ok(rsp);
        }

        [HttpPost("generate/transaction")]
        public IActionResult new_transaction(TransactionClient transaction)
        {
            var sign = RSA.RSA.Sign(transaction.sender_private_key, transaction.ToString());
            var rsp = new { transaction = transaction, signature = sign };
            return Ok(rsp);
        }
    }
}