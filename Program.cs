using BasicContactList;
using BasicContactListWithStreamAndCustomExtension;
using Serilog;
using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt")
    .CreateLogger();

log.Information("Started Working {0}", DateTime.Now);
Menu menu = new();
menu.MyMenu();