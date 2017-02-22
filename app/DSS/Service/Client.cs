using System;
using System.Text;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using System.Net.Http;
using DSS;
using System.Net;
using System.Threading.Tasks;

namespace DSS
{
	public class Client
	{
		HttpClient client;

		public string URL = null;


		public string Username;

		public string Password;



		public Client (IStorage storage)
		{

		    client = new HttpClient();

	
			setConnectionParams (storage);

		}

		public bool setConnectionParams(IStorage storage){

			Username = storage.get ("username");
			Password = storage.get ("password");
			URL 	 = storage.get ("url") ;

			return (URL!=null && Username!=null && Password!=null);
		}

		public string get(string path){



			string uri = getUrlWithPath (path);

			setAuth ();
		
			HttpResponseMessage response = null;
			try{
				
		    	response = client.GetAsync(uri).Result;

			}catch(WebException e){
				
				throw new NetworkException ();

			}

			if (response.IsSuccessStatusCode) {
				// by calling .Result you are performing a synchronous call
				var responseContent = response.Content; 

				// by calling .Result you are synchronously reading the result
				string responseString = responseContent.ReadAsStringAsync ().Result;

				return responseString;

			} else {
				return null;
			}

		}

		public string post (
			string path, 
			List<KeyValuePair<string, string>>postParameters, 
			string filepath=null)
		{

			setAuth ();
			using (var multipartFormDataContent = new MultipartFormDataContent())
			{
				foreach (var keyValuePair in postParameters)
				{
					string value = String.IsNullOrEmpty (keyValuePair.Value) ? "" : keyValuePair.Value;
					multipartFormDataContent.Add(new StringContent(value),
						String.Format("\"{0}\"", keyValuePair.Key));
				}

				FileStream stream    = null;
				String temp_filepath = null;
				try{



					if (filepath != null) {

						temp_filepath = Path.Combine (
							Environment.GetFolderPath (Environment.SpecialFolder.Personal), 
							Path.GetFileName (filepath) +  ".tmp"
						);



						File.Copy (filepath, temp_filepath, true);
						File.SetAttributes(temp_filepath, FileAttributes.Temporary);

						

						stream = new FileStream (temp_filepath, FileMode.Open);
						multipartFormDataContent.Add(new StreamContent(stream), "file", Path.GetFileName (filepath));

					}




					Task<HttpResponseMessage> postTask = null;
					try{

						postTask = client.PostAsync(getUrlWithPath (path), multipartFormDataContent);
						postTask.Wait();

					}catch(WebException e){

						throw new NetworkException ();

					}catch(ObjectDisposedException e){

						throw new NetworkException ();

					}


					if (stream != null) {
						stream.Close ();
					}

					if (postTask.Result.IsSuccessStatusCode) {

						var readTask = postTask.Result.Content.ReadAsStringAsync();
						readTask.Wait();

						return readTask.Result;

					} else {

						var readTask = postTask.Result.Content.ReadAsStringAsync();
						readTask.Wait();

						var result = readTask.Result;

						return null;
					}

				}catch(UnauthorizedAccessException e){

					return null;

				}finally{

					if (File.Exists (temp_filepath)) {
						File.SetAttributes(temp_filepath, FileAttributes.Temporary);
						File.Delete (temp_filepath);
					}


					if (stream != null) {
						stream.Close ();
						stream.Dispose ();
						stream = null;
					}

				}




			}

		}

	

		public string put (
			string path, 
			List<KeyValuePair<string, string>>postParameters, 
			string filepath=null)
		{

			setAuth ();
			using (var multipartFormDataContent = new MultipartFormDataContent())
			{
				foreach (var keyValuePair in postParameters)
				{
					string value = String.IsNullOrEmpty (keyValuePair.Value) ? "" : keyValuePair.Value;
					multipartFormDataContent.Add(new StringContent(value),
						String.Format("\"{0}\"", keyValuePair.Key));
				}






				Task<HttpResponseMessage> postTask = null;
				try{

					postTask = client.PostAsync(getUrlWithPath (path), multipartFormDataContent);
					postTask.Wait();

				}catch(WebException e){

					throw new NetworkException ();

				}catch(ObjectDisposedException e){

					throw new NetworkException ();

				}
		

				if (postTask.Result.IsSuccessStatusCode) {

					var readTask = postTask.Result.Content.ReadAsStringAsync();
					readTask.Wait();

					return readTask.Result;

				} else {

					var readTask = postTask.Result.Content.ReadAsStringAsync();
					readTask.Wait();

					var result = readTask.Result;

					return null;
				}


			}

		}




		private string getUrlWithPath(string path){
			return  URL  + "/api/v2/" + path;
		}


		private void setAuth(){
			var byteArray = Encoding.ASCII.GetBytes(Username + ":" + Password);
			var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
			client.DefaultRequestHeaders.Authorization = header;
		}




	}
}

