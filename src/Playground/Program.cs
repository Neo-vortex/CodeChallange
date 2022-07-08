// See https://aka.ms/new-console-template for more information
using ILGPU;

Console.WriteLine("Hello, World!");
using var context = Context.CreateDefault();
foreach (var device in context)
{
    // Create accelerator for the given device
    using var accelerator = device.CreateAccelerator(context);
   
    Console.WriteLine( accelerator);
}