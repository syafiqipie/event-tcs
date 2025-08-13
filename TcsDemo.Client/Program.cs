using Microsoft.Extensions.Logging;
using TcsDemo.Client;
using TcsDemo.Server;

ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
DummyFirmware fw = new();
fw.SetLogger(factory);
SimpleHal hal = new(fw);
hal.SetLogger(factory);

await hal.MoveMotorAsync(100);
