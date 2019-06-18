using System;
using System.Collections.Generic;
using RestSharp;
using Rest.Dp;

namespace Rest.Dp.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDefaultApi
    {
        /// <summary>
        ///  Analog is to write or read on analog pins on the Arduino
        /// </summary>
        /// <param name="pinNumber"></param>
        /// <returns></returns>
        void AnalogPinNumberGet (int? pinNumber);
        /// <summary>
        ///  Digital is to write or read on digital pins on the Arduino.
        /// </summary>
        /// <param name="pinNumber"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        void DigitalPinNumberStatusGet (int? pinNumber, int? status);
        /// <summary>
        ///  You can also access a description of all the variables that were declared on the board with a single command. This is useful to automatically build graphical interfaces based on the variables exposed to the API.
        /// </summary>
        /// <returns></returns>
        void IdGet ();
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class DefaultApi : IDefaultApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public DefaultApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DefaultApi(String basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }
    
        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(String basePath)
        {
            this.ApiClient.BasePath = basePath;
        }
    
        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public String GetBasePath(String basePath)
        {
            return this.ApiClient.BasePath;
        }
    
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient {get; set;}
    
        /// <summary>
        ///  Analog is to write or read on analog pins on the Arduino
        /// </summary>
        /// <param name="pinNumber"></param> 
        /// <returns></returns>            
        public void AnalogPinNumberGet (int? pinNumber)
        {
            
            // verify the required parameter 'pinNumber' is set
            if (pinNumber == null) throw new ApiException(400, "Missing required parameter 'pinNumber' when calling AnalogPinNumberGet");
            
    
            var path = "/analog/{pin number}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "pin number" + "}", ApiClient.ParameterToString(pinNumber));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling AnalogPinNumberGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling AnalogPinNumberGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        ///  Digital is to write or read on digital pins on the Arduino.
        /// </summary>
        /// <param name="pinNumber"></param> 
        /// <param name="status"></param> 
        /// <returns></returns>            
        public void DigitalPinNumberStatusGet (int? pinNumber, int? status)
        {
            
            // verify the required parameter 'pinNumber' is set
            if (pinNumber == null) throw new ApiException(400, "Missing required parameter 'pinNumber' when calling DigitalPinNumberStatusGet");
            
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling DigitalPinNumberStatusGet");
            
    
            var path = "/digital/{pin number}/{status}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "pin number" + "}", ApiClient.ParameterToString(pinNumber));
path = path.Replace("{" + "status" + "}", ApiClient.ParameterToString(status));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling DigitalPinNumberStatusGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling DigitalPinNumberStatusGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        ///  You can also access a description of all the variables that were declared on the board with a single command. This is useful to automatically build graphical interfaces based on the variables exposed to the API.
        /// </summary>
        /// <returns></returns>            
        public void IdGet ()
        {
            
    
            var path = "/id";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling IdGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling IdGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
    }
}
