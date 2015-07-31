using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Bluetooth;
using Blacksun.Bluetooth.Android;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidBluetoothClient))]
namespace Blacksun.Bluetooth.Android
{
    public class AndroidBluetoothClient : IBluetoothClient
    {

        private BluetoothAdapter btAdapter;

        public AndroidBluetoothClient()
        {
            btAdapter = BluetoothAdapter.DefaultAdapter;
        }

        public async Task<bool> IsBluetoothOn()
        {
            var bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (bluetoothAdapter == null)
            {
                // Device does not support Bluetooth
                throw new Exception("Device does not support Bluetooth");
            }
            else
            {
                return bluetoothAdapter.IsEnabled;
            }
        }

        public async Task<List<IBluetoothDevice>> GetPairedDevices()
        {

            var devices = new List<IBluetoothDevice>();

            // Get a set of currently paired devices 
 			var pairedDevices = btAdapter.BondedDevices; 
 			 
 			// If there are paired devices, add each one to the ArrayAdapter 
 			if (pairedDevices.Count > 0) 
            { 
 				foreach (var paireddevice in pairedDevices)
 				{

 				    var device = new AndroidBluetoothDevice() {Name = paireddevice.Name, Address = paireddevice.Address};
 				    device.BluetoothDevice = paireddevice;
 				    try
 				    {
                        switch (paireddevice.Type)
                        {
                            case global::Android.Bluetooth.BluetoothDeviceType.Classic:
                                device.Type = BluetoothDeviceType.Classic;
                                break;
                            case global::Android.Bluetooth.BluetoothDeviceType.Dual:
                                device.Type = BluetoothDeviceType.Dual;
                                break;
                            case global::Android.Bluetooth.BluetoothDeviceType.Le:
                                device.Type = BluetoothDeviceType.Le;
                                break;
                            case global::Android.Bluetooth.BluetoothDeviceType.Unknown:
                                device.Type = BluetoothDeviceType.Unknown;
                                break;
                        }
 				    }
 				    catch (Exception ex)
 				    {
 				        
 				    }

 				    try
 				    {
                        var uuids = paireddevice.GetUuids().ToList();

 				        foreach (var uuid in uuids)
 				        {

                            var stringUUID = uuid.ToString();
 				            device.UniqueIdentifiers.Add(Guid.Parse(stringUUID));
 				        }

 				    }
 				    catch (Exception wz)
 				    {
 				        
 				    }

                    devices.Add(device);
 				} 
 			} 



            return devices;
        }

        public async Task<IBluetoothDevice> FindDeviceByIdentifier(string identifier)
        {
            var bluetoothClient = new AndroidBluetoothClient();
            var devices = await bluetoothClient.GetPairedDevices();

            foreach (var device in devices)
            {
                if (device.ContainsUniqueIdentifier(identifier))
                {
                    device.SetUniqueIdentifier(identifier);
                    return device;
                }

            }

            return null;
        }

        
    }
}