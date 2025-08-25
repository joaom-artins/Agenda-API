namespace Service.Commons;

public class RegexStrings
{
    public static string Password => @"(?=[A-Za-z0-9 !#$%&()*+,-./:;<=>?@[\]^_{|}~]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[ !#$%&()*+,-./:;<=>?@[\]^_{|}~])(?=.{6,30}).*$";
    public static string PhoneNumber => @"^[0-9]{8,15}$";
}
