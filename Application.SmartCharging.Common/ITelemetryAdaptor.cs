using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.Common
{
    public interface ITelemetryAdaptor
    {
        /// <summary>
        /// This method logs trace telemetry in application insights.
        /// </summary>
        /// <param name="traceTelemetry">Trace telemetry object</param>
        void TrackTrace(TraceTelemetry traceTelemetry);
        /// <summary>
        /// This method logs trace telemetry in application insights.
        /// </summary>
        /// <param name="message">Trace message</param>
        /// <param name="severityLevel">Trace severity</param>
        /// <param name="properties">Trace properties</param>
        void TrackTrace(string message, SeverityLevel severityLevel = SeverityLevel.Information, IDictionary<string, string> properties = null);
        /// <summary>
        /// This method logs exception telemetry in application insights.
        /// </summary>
        /// <param name="exception">Exception object</param>
        /// <param name="properties">Exception properties</param>
        /// <param name="metrics">Exception metrics</param>
        void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
        /// <summary>
        /// This method logs exception telemetry in application insights.
        /// </summary>
        /// <param name="exceptionTelemetry">Exception telemetry object</param>
        void TrackException(ExceptionTelemetry exceptionTelemetry);
        /// <summary>
        /// This method logs event telemetry in application insights.
        /// </summary>
        /// <param name="eventName">Event name</param>
        /// <param name="properties">Event properties</param>
        /// <param name="metrics">Event metrics</param>
        void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
        /// <summary>
        /// This method logs event telemetry in application insights.
        /// </summary>
        /// <param name="eventTelemetry">Event telemetry object</param>
        void TrackEvent(EventTelemetry eventTelemetry);
        /// <summary>
        /// This method logs dependency telemetry in application insights.
        /// </summary>
        /// <param name="dependencyTelemetry">Dependency telemetry object</param>
        void TrackDependency(DependencyTelemetry dependencyTelemetry);
        /// <summary>
        /// This method logs dependency telemetry in application insights.
        /// </summary>
        /// <param name="dependencyTypeName">Dependency type name</param>
        /// <param name="dependencyName">Dependency name</param>
        /// <param name="data">Dependency data</param>
        /// <param name="startTime">Depenedency start time</param>
        /// <param name="duration">Dependency duration</param>
        /// <param name="success">Success status</param>
        void TrackDependency(string dependencyTypeName, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success);
        
      }
}
