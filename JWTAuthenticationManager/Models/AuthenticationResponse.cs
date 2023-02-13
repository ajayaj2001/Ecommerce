﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JWTAuthenticationManager.Models
{
    public class AuthenticationResponse
    {
        public string UserName { get; set; }

        public string JwtToken { get; set; }

        public int ExpiresIn { get; set; }  
    }
}