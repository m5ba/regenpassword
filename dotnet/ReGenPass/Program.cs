// See https://aka.ms/new-console-template for more information
using ReGenPass;

Console.WriteLine("Hello, World! ReGenPass is here.");
if(args.Length != 4)
{
    Console.WriteLine("Usage: regenpass.exe <resource> <login> <iteration> <secret>");
    return;
}
PasswordContext passwordContext = new()
{
    Resource = args[0],
    Login = args[1],
    Iteration = Int32.Parse(args[2]),
    Secret = args[3]
};
string pwd = PassComputer.Compute(passwordContext);
Console.WriteLine("Computed Password:");
Console.WriteLine(pwd);