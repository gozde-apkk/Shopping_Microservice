﻿namespace Ms.Web.Services
{
    public interface ITokenProvider
    {
        void ClearToken();
        string? GetToken();
        void SetToken(string token);
    }
}