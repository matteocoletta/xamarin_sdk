using Android.App;
using Android.Widget;
using Android.OS;

using Com.Adjust.Testlibrary;
using Com.Adjust.Sdk;

namespace Example_ci
{
    [Activity(Label = "Example_ci", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private static TestLibrary testLibrary;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			string baseUrl = "https://10.0.2.2:8443";
            AdjustFactory.SetTestingMode(baseUrl);
            testLibrary = new TestLibrary(baseUrl, new CommandExecutor(this));
			testLibrary.SetTests("current/Test_SessionEventCallbacks");
			testLibrary.InitTestSession("android4.11.4");
		}

        public static void AddInfoToSend(string key, string value)
		{
			if (null != testLibrary)
			{
				testLibrary.AddInfoToSend(key, value);
			}
		}

	    public static void SendInfoToServer()
		{
			if (null != testLibrary)
			{
                testLibrary.SendInfoToServer();
			}
		}

	}
}

