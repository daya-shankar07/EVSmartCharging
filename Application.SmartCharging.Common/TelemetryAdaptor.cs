using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.EventCounterCollector;
using Microsoft.ApplicationInsights.WindowsServer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Application.SmartCharging.Common
{
    public class TelemetryAdaptor : ITelemetryAdaptor
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly IConfiguration _config;

        public TelemetryAdaptor( IConfiguration config)
        {
            _config = config;
            TelemetryConfiguration _telemetryConfiguration = TelemetryConfiguration.CreateDefault();
            _telemetryConfiguration.InstrumentationKey = _config.GetSection("AppSettings:instrumentationKey").ToString();
            _telemetryConfiguration.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());
            _telemetryConfiguration.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());

            var dependencyTrackingModule = new DependencyTrackingTelemetryModule();
            dependencyTrackingModule.Initialize(_telemetryConfiguration);
            var requestTrackingModule = new RequestTrackingTelemetryModule();
            requestTrackingModule.Initialize(_telemetryConfiguration);
            var eventTrackingModule = new EventCounterCollectionModule();
            eventTrackingModule.Initialize(_telemetryConfiguration);
            var appServiceTrackingModule = new AppServicesHeartbeatTelemetryModule();
            appServiceTrackingModule.Initialize(_telemetryConfiguration);
            _telemetryClient = new TelemetryClient(_telemetryConfiguration);
        }
        public void TrackDependency(DependencyTelemetry dependencyTelemetry)
        {
            _telemetryClient.TrackDependency(dependencyTelemetry);
        }

        public void TrackDependency(string dependencyTypeName, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            _telemetryClient.TrackDependency(dependencyTypeName, dependencyName, data, startTime, duration, success);
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            _telemetryClient.TrackEvent(eventName, properties, metrics);
        }

        public void TrackEvent(EventTelemetry eventTelemetry)
        {
            _telemetryClient.TrackEvent(eventTelemetry);
        }

        public void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            _telemetryClient.TrackException(exception, properties, metrics);
        }

        public void TrackException(ExceptionTelemetry exceptionTelemetry)
        {
            _telemetryClient.TrackException(exceptionTelemetry);
        }

        public void TrackTrace(TraceTelemetry traceTelemetry)
        {
            _telemetryClient.TrackTrace(traceTelemetry);
        }
        public void TrackTrace(string message, SeverityLevel severityLevel = SeverityLevel.Information, IDictionary<string, string> properties = null)
        {
            _telemetryClient.TrackTrace(message, severityLevel, properties);
        }
    }
}
