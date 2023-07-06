using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcAspNetCore;

Console.WriteLine("Starting CLIENT application... http://192.168.229.128:50002 ...");
// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://192.168.229.128:50002"); // running this on an specific IP made it work
// using var channel = GrpcChannel.ForAddress("http://localhost:50002");  // this run ok in the host
var client = new Greeter.GreeterClient(channel);
for (int i = 0; i < 10; i++)
{
    try
    {
        var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
        Console.WriteLine("Greeting received from server: " + reply.Message);
    }
    catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.Unavailable)
    {
        Console.WriteLine("Error: gRPC Service Unavailable (Check Server, IP and port)");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.ToString());
    }
    Thread.Sleep(1000);
}
Console.WriteLine("Exiting CLIENT application...");
// Console.ReadKey();


// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
