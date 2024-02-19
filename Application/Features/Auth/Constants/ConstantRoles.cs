namespace Application.Features.Auth.Constants;

public static class ConstantRoles
{
    public static string MANAGER = "Manager";
    public static string ACCOUNTANT = "Accountant";
    public static string FINANCIALANALYST = "FinancialAnalyst";
    public static string USER = "User";
    public static string[] Roles => new[] { MANAGER, ACCOUNTANT, FINANCIALANALYST, USER };

}