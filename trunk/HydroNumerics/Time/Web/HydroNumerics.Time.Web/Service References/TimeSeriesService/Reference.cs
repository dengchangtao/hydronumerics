﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 3.0.40624.0
// 
namespace HydroNumerics.Time.Web.TimeSeriesService {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseTimeSeries", Namespace="http://schemas.datacontract.org/2004/07/HydroNumerics.Time.Core")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HydroNumerics.Time.Web.TimeSeriesService.TimespanSeries))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HydroNumerics.Time.Web.TimeSeriesService.TimestampSeries))]
    public partial class BaseTimeSeries : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool AllowExtrapolationField;
        
        private string NameField;
        
        private string descriptionField;
        
        private int idField;
        
        private double relaxationFactorField;
        
        private HydroNumerics.Time.Web.TimeSeriesService.Unit unitField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool AllowExtrapolation {
            get {
                return this.AllowExtrapolationField;
            }
            set {
                if ((this.AllowExtrapolationField.Equals(value) != true)) {
                    this.AllowExtrapolationField = value;
                    this.RaisePropertyChanged("AllowExtrapolation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.descriptionField, value) != true)) {
                    this.descriptionField = value;
                    this.RaisePropertyChanged("description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int id {
            get {
                return this.idField;
            }
            set {
                if ((this.idField.Equals(value) != true)) {
                    this.idField = value;
                    this.RaisePropertyChanged("id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double relaxationFactor {
            get {
                return this.relaxationFactorField;
            }
            set {
                if ((this.relaxationFactorField.Equals(value) != true)) {
                    this.relaxationFactorField = value;
                    this.RaisePropertyChanged("relaxationFactor");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HydroNumerics.Time.Web.TimeSeriesService.Unit unit {
            get {
                return this.unitField;
            }
            set {
                if ((object.ReferenceEquals(this.unitField, value) != true)) {
                    this.unitField = value;
                    this.RaisePropertyChanged("unit");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Unit", Namespace="http://schemas.datacontract.org/2004/07/HydroNumerics.Core")]
    public partial class Unit : object, System.ComponentModel.INotifyPropertyChanged {
        
        private double conversionFactorField;
        
        private double conversionOffsetField;
        
        private string descriptionField;
        
        private HydroNumerics.Time.Web.TimeSeriesService.Dimension dimensionField;
        
        private string idField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double conversionFactor {
            get {
                return this.conversionFactorField;
            }
            set {
                if ((this.conversionFactorField.Equals(value) != true)) {
                    this.conversionFactorField = value;
                    this.RaisePropertyChanged("conversionFactor");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double conversionOffset {
            get {
                return this.conversionOffsetField;
            }
            set {
                if ((this.conversionOffsetField.Equals(value) != true)) {
                    this.conversionOffsetField = value;
                    this.RaisePropertyChanged("conversionOffset");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.descriptionField, value) != true)) {
                    this.descriptionField = value;
                    this.RaisePropertyChanged("description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HydroNumerics.Time.Web.TimeSeriesService.Dimension dimension {
            get {
                return this.dimensionField;
            }
            set {
                if ((object.ReferenceEquals(this.dimensionField, value) != true)) {
                    this.dimensionField = value;
                    this.RaisePropertyChanged("dimension");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string id {
            get {
                return this.idField;
            }
            set {
                if ((object.ReferenceEquals(this.idField, value) != true)) {
                    this.idField = value;
                    this.RaisePropertyChanged("id");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TimespanSeries", Namespace="http://schemas.datacontract.org/2004/07/HydroNumerics.Time.Core")]
    public partial class TimespanSeries : HydroNumerics.Time.Web.TimeSeriesService.BaseTimeSeries {
        
        private System.Collections.ObjectModel.ObservableCollection<HydroNumerics.Time.Web.TimeSeriesService.TimespanValue> itemsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<HydroNumerics.Time.Web.TimeSeriesService.TimespanValue> items {
            get {
                return this.itemsField;
            }
            set {
                if ((object.ReferenceEquals(this.itemsField, value) != true)) {
                    this.itemsField = value;
                    this.RaisePropertyChanged("items");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TimestampSeries", Namespace="http://schemas.datacontract.org/2004/07/HydroNumerics.Time.Core")]
    public partial class TimestampSeries : HydroNumerics.Time.Web.TimeSeriesService.BaseTimeSeries {
        
        private System.Collections.ObjectModel.ObservableCollection<HydroNumerics.Time.Web.TimeSeriesService.TimestampValue> itemsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<HydroNumerics.Time.Web.TimeSeriesService.TimestampValue> items {
            get {
                return this.itemsField;
            }
            set {
                if ((object.ReferenceEquals(this.itemsField, value) != true)) {
                    this.itemsField = value;
                    this.RaisePropertyChanged("items");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TimestampValue", Namespace="http://schemas.datacontract.org/2004/07/HydroNumerics.Time.Core")]
    public partial class TimestampValue : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.DateTime TimeField;
        
        private double ValueField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Time {
            get {
                return this.TimeField;
            }
            set {
                if ((this.TimeField.Equals(value) != true)) {
                    this.TimeField = value;
                    this.RaisePropertyChanged("Time");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Value {
            get {
                return this.ValueField;
            }
            set {
                if ((this.ValueField.Equals(value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Dimension", Namespace="http://schemas.datacontract.org/2004/07/HydroNumerics.Core")]
    public partial class Dimension : object, System.ComponentModel.INotifyPropertyChanged {
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TimespanValue", Namespace="http://schemas.datacontract.org/2004/07/HydroNumerics.Time.Core")]
    public partial class TimespanValue : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.DateTime endTimeField;
        
        private System.DateTime startTimeField;
        
        private double valField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime endTime {
            get {
                return this.endTimeField;
            }
            set {
                if ((this.endTimeField.Equals(value) != true)) {
                    this.endTimeField = value;
                    this.RaisePropertyChanged("endTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime startTime {
            get {
                return this.startTimeField;
            }
            set {
                if ((this.startTimeField.Equals(value) != true)) {
                    this.startTimeField = value;
                    this.RaisePropertyChanged("startTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double val {
            get {
                return this.valField;
            }
            set {
                if ((this.valField.Equals(value) != true)) {
                    this.valField = value;
                    this.RaisePropertyChanged("val");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="TimeSeriesService.TimeSeriesService")]
    public interface TimeSeriesService {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:TimeSeriesService/GetTimeStampSeries", ReplyAction="urn:TimeSeriesService/GetTimeStampSeriesResponse")]
        System.IAsyncResult BeginGetTimeStampSeries(System.AsyncCallback callback, object asyncState);
        
        HydroNumerics.Time.Web.TimeSeriesService.TimestampSeries EndGetTimeStampSeries(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface TimeSeriesServiceChannel : HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class GetTimeStampSeriesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetTimeStampSeriesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public HydroNumerics.Time.Web.TimeSeriesService.TimestampSeries Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((HydroNumerics.Time.Web.TimeSeriesService.TimestampSeries)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class TimeSeriesServiceClient : System.ServiceModel.ClientBase<HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService>, HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService {
        
        private BeginOperationDelegate onBeginGetTimeStampSeriesDelegate;
        
        private EndOperationDelegate onEndGetTimeStampSeriesDelegate;
        
        private System.Threading.SendOrPostCallback onGetTimeStampSeriesCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public TimeSeriesServiceClient() {
        }
        
        public TimeSeriesServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TimeSeriesServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TimeSeriesServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TimeSeriesServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<GetTimeStampSeriesCompletedEventArgs> GetTimeStampSeriesCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService.BeginGetTimeStampSeries(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetTimeStampSeries(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        HydroNumerics.Time.Web.TimeSeriesService.TimestampSeries HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService.EndGetTimeStampSeries(System.IAsyncResult result) {
            return base.Channel.EndGetTimeStampSeries(result);
        }
        
        private System.IAsyncResult OnBeginGetTimeStampSeries(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService)(this)).BeginGetTimeStampSeries(callback, asyncState);
        }
        
        private object[] OnEndGetTimeStampSeries(System.IAsyncResult result) {
            HydroNumerics.Time.Web.TimeSeriesService.TimestampSeries retVal = ((HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService)(this)).EndGetTimeStampSeries(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetTimeStampSeriesCompleted(object state) {
            if ((this.GetTimeStampSeriesCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetTimeStampSeriesCompleted(this, new GetTimeStampSeriesCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetTimeStampSeriesAsync() {
            this.GetTimeStampSeriesAsync(null);
        }
        
        public void GetTimeStampSeriesAsync(object userState) {
            if ((this.onBeginGetTimeStampSeriesDelegate == null)) {
                this.onBeginGetTimeStampSeriesDelegate = new BeginOperationDelegate(this.OnBeginGetTimeStampSeries);
            }
            if ((this.onEndGetTimeStampSeriesDelegate == null)) {
                this.onEndGetTimeStampSeriesDelegate = new EndOperationDelegate(this.OnEndGetTimeStampSeries);
            }
            if ((this.onGetTimeStampSeriesCompletedDelegate == null)) {
                this.onGetTimeStampSeriesCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetTimeStampSeriesCompleted);
            }
            base.InvokeAsync(this.onBeginGetTimeStampSeriesDelegate, null, this.onEndGetTimeStampSeriesDelegate, this.onGetTimeStampSeriesCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService CreateChannel() {
            return new TimeSeriesServiceClientChannel(this);
        }
        
        private class TimeSeriesServiceClientChannel : ChannelBase<HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService>, HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService {
            
            public TimeSeriesServiceClientChannel(System.ServiceModel.ClientBase<HydroNumerics.Time.Web.TimeSeriesService.TimeSeriesService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginGetTimeStampSeries(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("GetTimeStampSeries", _args, callback, asyncState);
                return _result;
            }
            
            public HydroNumerics.Time.Web.TimeSeriesService.TimestampSeries EndGetTimeStampSeries(System.IAsyncResult result) {
                object[] _args = new object[0];
                HydroNumerics.Time.Web.TimeSeriesService.TimestampSeries _result = ((HydroNumerics.Time.Web.TimeSeriesService.TimestampSeries)(base.EndInvoke("GetTimeStampSeries", _args, result)));
                return _result;
            }
        }
    }
}
