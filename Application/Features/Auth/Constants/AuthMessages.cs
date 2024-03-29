﻿namespace Application.Features.Auth.Constants;

public class AuthMessages
{
    public const string UserDontExists = "AppUser don't exists.";
    public const string RefreshDontExists = "Refresh don't exists.";
    public const string UserMailAlreadyExists = "AppUser mail already exists.";
    public const string PasswordDontMatch = "Password don't match.";
    public static string RegistrationFailed = "Registration failed.";
    public static string RefreshExpired = "Refresh token expired.";
    public static string RefreshRevoked = "Refresh token expired.";
    public static string RoleDontExists = "Role don't exists.";
}