﻿using System;
using System.Collections.Generic;
using Com.Adjust.Testlibrary;
using Android.Util;
using Android.Content;

using Com.Adjust.Sdk;

namespace Example_ci
{
    public class CommandExecutor : Java.Lang.Object, ICommandListener
    {
        string TAG = "CommandExecutor";
        private string basePath;
        private Dictionary<string, object> savedInstances = new Dictionary<string, object>();
        private static string DefaultConfigName = "defaultConfig";
        private static string DefaultEventName = "defaultEvent";
        private Context context;

        public CommandExecutor(Context context)
        {
            this.context = context;
        }

        void ICommandListener.ExecuteCommand(string className, string methodName, IDictionary<string, IList<string>> paramsDict)
        {
            Log.Debug(TAG, "className: [" + className + "] | methodName: [" + methodName + "] | params size: [" + paramsDict.Count + "]");
            try
            {
                switch (methodName)
                {
                    case "factory": factory(paramsDict); break;
                    case "config": config(paramsDict); break;
                    case "start": start(paramsDict); break;
                    case "event": eventFunc(paramsDict); break;
                    case "trackEvent": trackEvent(paramsDict); break;
                    case "resume": resume(paramsDict); break;
                    case "pause": pause(paramsDict); break;
                    case "setEnabled": setEnabled(paramsDict); break;
                    case "setReferrer": setReferrer(paramsDict); break;
                    case "setOfflineMode": setOfflineMode(paramsDict); break;
                    case "sendFirstPackages": sendFirstPackages(paramsDict); break;
                    case "addSessionCallbackParameter": addSessionCallbackParameter(paramsDict); break;
                    case "addSessionPartnerParameter": addSessionPartnerParameter(paramsDict); break;
                    case "removeSessionCallbackParameter": removeSessionCallbackParameter(paramsDict); break;
                    case "removeSessionPartnerParameter": removeSessionPartnerParameter(paramsDict); break;
                    case "resetSessionCallbackParameters": resetSessionCallbackParameters(paramsDict); break;
                    case "resetSessionPartnerParameters": resetSessionPartnerParameters(paramsDict); break;
                    case "setPushToken": setPushToken(paramsDict); break;
                    case "teardown": teardown(paramsDict); break;
                    case "openDeeplink": openDeeplink(paramsDict); break;
                    case "sendReferrer": sendReferrer(paramsDict); break;
                    case "testBegin": testBegin(paramsDict); break;
                    case "testEnd": testEnd(paramsDict); break;
                }
            }
            catch (Java.Lang.Exception ex)
            {
                Log.Error(TAG, "executeCommand: failed to parse command. Check commands' syntax");
            }
        }

        private void factory(IDictionary<string, IList<string>> paramsDict)
        {
            if (paramsDict.ContainsKey("basePath"))
            {
                this.basePath = getFirstParameterValue(paramsDict, "basePath");
            }
        }

        private void config(IDictionary<string, IList<string>> paramsDict)
        {
            string configName = null;
            if (paramsDict.ContainsKey("configName"))
            {
                configName = getFirstParameterValue(paramsDict, "configName");
            }
            else
            {
                configName = DefaultConfigName;
            }

            AdjustConfig adjustConfig = null;
            if (savedInstances.ContainsKey(configName))
            {
                adjustConfig = (AdjustConfig)savedInstances[configName];
            }
            else
            {
                string environment = getFirstParameterValue(paramsDict, "environment");
                string appToken = getFirstParameterValue(paramsDict, "appToken");
                Context context = this.context;
                if (paramsDict.ContainsKey("context") && "null".Equals(paramsDict["context"]))
                {
                    context = null;
                }
                adjustConfig = new AdjustConfig(context, appToken, environment);
                adjustConfig.SetLogLevel(LogLevel.Verbose);
                savedInstances.Add(configName, adjustConfig);
            }

            if (paramsDict.ContainsKey("logLevel"))
            {
                string logLevelS = getFirstParameterValue(paramsDict, "logLevel");
                LogLevel logLevel = null;

                switch (logLevelS)
                {
                    case "verbose":
                        logLevel = LogLevel.Verbose;
                        break;
                    case "debug":
                        logLevel = LogLevel.Debug;
                        break;
                    case "info":
                        logLevel = LogLevel.Info;
                        break;
                    case "warn":
                        logLevel = LogLevel.Warn;
                        break;
                    case "error":
                        logLevel = LogLevel.Error;
                        break;
                    case "assert":
                        logLevel = LogLevel.Assert;
                        break;
                    case "suppress":
                        logLevel = LogLevel.Supress;
                        break;
                }

                adjustConfig.SetLogLevel(logLevel);
            }

            if (paramsDict.ContainsKey("defaultTracker"))
            {
                string defaultTracker = getFirstParameterValue(paramsDict, "defaultTracker");
                adjustConfig.SetDefaultTracker(defaultTracker);
            }

            if (paramsDict.ContainsKey("delayStart"))
            {
                string delayStartS = getFirstParameterValue(paramsDict, "delayStart");
                double delayStart = Double.Parse(delayStartS);
                adjustConfig.SetDelayStart(delayStart);
            }

            if (paramsDict.ContainsKey("deviceKnown"))
            {
                string deviceKnownS = getFirstParameterValue(paramsDict, "deviceKnown");
                bool deviceKnown = "true".Equals(deviceKnownS);
                adjustConfig.SetDeviceKnown(deviceKnown);
            }

            if (paramsDict.ContainsKey("eventBufferingEnabled"))
            {
                string eventBufferingEnabledS = getFirstParameterValue(paramsDict, "eventBufferingEnabled");
                var eventBufferingEnabled = "true".Equals(eventBufferingEnabledS);
                adjustConfig.SetEventBufferingEnabled((Java.Lang.Boolean)eventBufferingEnabled);
            }

            if (paramsDict.ContainsKey("sendInBackground"))
            {
                string sendInBackgroundS = getFirstParameterValue(paramsDict, "sendInBackground");
                var sendInBackground = "true".Equals(sendInBackgroundS);
                adjustConfig.SetSendInBackground(sendInBackground);
            }

            if (paramsDict.ContainsKey("userAgent"))
            {
                string userAgent = getFirstParameterValue(paramsDict, "userAgent");
                adjustConfig.SetUserAgent(userAgent);
            }
            // XXX add listeners
        }

        private void start(IDictionary<string, IList<string>> paramsDict)
        {
            config(paramsDict);
            string configName = null;
            if (paramsDict.ContainsKey("configName"))
            {
                configName = getFirstParameterValue(paramsDict, "configName");
            }
            else
            {
                configName = DefaultConfigName;
            }

            AdjustConfig adjustConfig = (AdjustConfig)savedInstances[configName];

            adjustConfig.SetBasePath(basePath);
            Adjust.OnCreate(adjustConfig);
        }

        private void eventFunc(IDictionary<string, IList<string>> paramsDict)
        {
            string eventName = null;
            if (paramsDict.ContainsKey("eventName"))
            {
                eventName = getFirstParameterValue(paramsDict, "eventName");
            }
            else
            {
                eventName = DefaultEventName;
            }

            AdjustEvent adjustEvent = null;
            if (savedInstances.ContainsKey(eventName))
            {
                adjustEvent = (AdjustEvent)savedInstances[eventName];
            }
            else
            {
                string eventToken = getFirstParameterValue(paramsDict, "eventToken");
                adjustEvent = new AdjustEvent(eventToken);
                savedInstances.Add(eventName, adjustEvent);
            }

            if (paramsDict.ContainsKey("revenue"))
            {
                var revenueParams = (IList<string>)paramsDict["revenue"];
                Log.Debug(TAG, "Received revenue of size: " + revenueParams.Count);
                string currency = revenueParams[0];
                var revenue = Java.Lang.Double.ParseDouble(revenueParams[1]);
                Log.Debug(TAG, "Received revenue of currency [" + currency + "] and revenue [" + revenue + "]");
                adjustEvent.SetRevenue(revenue, currency);
            }

            if (paramsDict.ContainsKey("callbackParams"))
            {
                var callbackParams = (IList<string>)paramsDict["callbackParams"];
                for (int i = 0; i < callbackParams.Count; i = i + 2)
                {
                    string key = callbackParams[i];
                    string value = callbackParams[i + 1];
                    Log.Debug(TAG, "Received callbackParam of key [" + key + "] and value [" + value + "]");
                    adjustEvent.AddCallbackParameter(key, value);
                }
            }

            if (paramsDict.ContainsKey("partnerParams"))
            {
                var partnerParams = (IList<string>)paramsDict["partnerParams"];
                for (int i = 0; i < partnerParams.Count; i = i + 2)
                {
                    string key = partnerParams[i];
                    string value = partnerParams[i + 1];
                    Log.Debug(TAG, "Received partnerParams of key [" + key + "] and value [" + value + "]");
                    adjustEvent.AddPartnerParameter(key, value);
                }
            }

            if (paramsDict.ContainsKey("orderId"))
            {
                string orderId = getFirstParameterValue(paramsDict, "orderId");
                adjustEvent.SetOrderId(orderId);
            }
        }

        private void trackEvent(IDictionary<string, IList<string>> paramsDict)
        {
            eventFunc(paramsDict);
            string eventName = null;
            if (paramsDict.ContainsKey("eventName"))
            {
                eventName = getFirstParameterValue(paramsDict, "eventName");
            }
            else
            {
                eventName = DefaultEventName;
            }
            AdjustEvent adjustEvent = (AdjustEvent)savedInstances[eventName];
            Adjust.TrackEvent(adjustEvent);
        }

        private void setReferrer(IDictionary<string, IList<string>> paramsDict)
        {
            string referrer = getFirstParameterValue(paramsDict, "referrer");
            Adjust.SetReferrer(referrer);
        }

        private void pause(IDictionary<string, IList<string>> paramsDict)
        {
            Adjust.OnPause();
        }

        private void resume(IDictionary<string, IList<string>> paramsDict)
        {
            Adjust.OnResume();
        }

        private void setEnabled(IDictionary<string, IList<string>> paramsDict)
        {
            Boolean enabled = Boolean.Parse(getFirstParameterValue(paramsDict, "enabled"));
            Adjust.Enabled = enabled;
        }

        private void setOfflineMode(IDictionary<string, IList<string>> paramsDict)
        {
            Boolean enabled = Boolean.Parse(getFirstParameterValue(paramsDict, "enabled"));
            Adjust.SetOfflineMode(enabled);
        }

        private void sendFirstPackages(IDictionary<string, IList<string>> paramsDict)
        {
            Adjust.SendFirstPackages();
        }

        private void addSessionCallbackParameter(IDictionary<string, IList<string>> paramsDict)
        {
            if (paramsDict.ContainsKey("KeyValue"))
            {
                var list = paramsDict["KeyValue"];
                for (var i = 0; i < list.Count; i = i + 2)
                {
                    string key = list[i];
                    string value = list[i + 1];
                    Adjust.AddSessionCallbackParameter(key, value);
                }
            }
        }

        private void addSessionPartnerParameter(IDictionary<string, IList<string>> paramsDict)
        {
            if (paramsDict.ContainsKey("KeyValue"))
            {
                var list = paramsDict["KeyValue"];
                for (var i = 0; i < list.Count; i = i + 2)
                {
                    string key = list[i];
                    string value = list[i + 1];
                    Adjust.AddSessionPartnerParameter(key, value);
                }
            }
		}

        private void removeSessionCallbackParameter(IDictionary<string, IList<string>> paramsDict)
        {
            if (paramsDict.ContainsKey("Key"))
            {
				var list = paramsDict["Key"];
                for (var i = 0; i < list.Count; i++)
                {
                    string key = list[i];
                    Adjust.RemoveSessionCallbackParameter(key);
                }
            }
        }

        private void removeSessionPartnerParameter(IDictionary<string, IList<string>> paramsDict)
        {
			if (paramsDict.ContainsKey("Key"))
			{
				var list = paramsDict["Key"];
				for (var i = 0; i < list.Count; i++)
				{
					string key = list[i];
					Adjust.RemoveSessionPartnerParameter(key);
				}
			}
		}

        private void resetSessionCallbackParameters(IDictionary<string, IList<string>> paramsDict)
        {
            Adjust.ResetSessionCallbackParameters();
        }

        private void resetSessionPartnerParameters(IDictionary<string, IList<string>> paramsDict)
        {
            Adjust.ResetSessionPartnerParameters();
        }

        private void setPushToken(IDictionary<string, IList<string>> paramsDict)
        {
            string token = getFirstParameterValue(paramsDict, "pushToken");
            Adjust.SetPushToken(token);
        }

        private void teardown(IDictionary<string, IList<string>> paramsDict)
        {
            string deleteStatestring = getFirstParameterValue(paramsDict, "deleteState");
            var deleteState = Java.Lang.Boolean.ParseBoolean(deleteStatestring);

            Log.Debug("TestApp", "calling teardown with delete state");
            AdjustFactory.Teardown(this.context, deleteState);
        }

        private void openDeeplink(IDictionary<string, IList<string>> paramsDict)
        {
            string deeplink = getFirstParameterValue(paramsDict, "deeplink");
            Adjust.AppWillOpenUrl(Android.Net.Uri.Parse(deeplink));
        }

        private void sendReferrer(IDictionary<string, IList<string>> paramsDict)
        {
            string referrer = getFirstParameterValue(paramsDict, "referrer");
            Adjust.SetReferrer(referrer);
        }

        private void testBegin(IDictionary<string, IList<string>> paramsDict)
        {
            if (paramsDict.ContainsKey("basePath"))
                this.basePath = getFirstParameterValue(paramsDict, "basePath");

            AdjustFactory.Teardown(this.context, true);
            AdjustFactory.TimerInterval = -1;
            AdjustFactory.TimerStart = -1;
            AdjustFactory.SessionInterval = -1;
            AdjustFactory.SubsessionInterval = -1;
            savedInstances.Clear();
        }

        private void testEnd(IDictionary<string, IList<string>> paramsDict)
        {
            AdjustFactory.Teardown(this.context, true);
        }

        private string getFirstParameterValue(IDictionary<string, IList<string>> paramsDict, String key)
        {
            if (paramsDict.ContainsKey(key) && paramsDict[key].Count > 1)
            {
                return paramsDict[key][0];
            }

            return null;
        }
    }
}
