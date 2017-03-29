using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using System;
using System.Threading.Tasks;

namespace CreateDeviceIdentity
{
    class Program
    {
        static string _deviceId = "device1";
        static string _connectionString = "<CXString>";
        static RegistryManager _registryManager;
        static void Main(string[] args)
        {
            try
            {
                _registryManager = RegistryManager.CreateFromConnectionString(_connectionString);
                AddDeviceAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        static async Task AddDeviceAsync()
        {
            Device device;
            try
            {
                device = await _registryManager.AddDeviceAsync(new Device(_deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await _registryManager.GetDeviceAsync(_deviceId);
            }

            Console.WriteLine($"Generated device key: {device?.Authentication.SymmetricKey.PrimaryKey}");
        }
    }
}
