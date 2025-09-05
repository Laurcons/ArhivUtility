using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArhivUtility {
  internal class TrackingService {
    private static readonly HttpClient client = new HttpClient();

    private static async Task _trackEventAsync(string eventName, object eventData) {
      // fetch machine data
      var userName = SystemInformation.UserName;
      var machineName = Environment.MachineName;
      var deviceName = $"{machineName}: {userName}";
      var appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      try {
        var payload = new Dictionary<string, object>() {
          { "eventName", eventName },
          { "eventData", eventData },
          { "deviceName", deviceName },
          { "appVersion", appVersion }
        };
        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
#if DEBUG
        var response = await client.PostAsync("https://arvutil-dev.laurcons.ro/v1/tr", content);
#else
        var response = await client.PostAsync("https://arvutil.laurcons.ro/v1/tr", content);
#endif
        string responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Tracked event: {responseString}");
      }
      catch (Exception ex) {
        // do nothing... tough luck, but we don't interrupt the user
        Console.Error.WriteLine(ex.ToString());
      }
    }

    public static void TrackEvent(string eventName, object eventData) {
      _ = _trackEventAsync(eventName, eventData);
    }
  }
}
