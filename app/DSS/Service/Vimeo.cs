using System;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;

namespace DSS.Service
{
	public class Vimeo
	{

		private string token;

		private HttpClient client;

		private string complete_url;

		private string upload_url = "https://1234.cloud.vimeo.com/";

		private string api_url    = "https://api.vimeo.com";


		public Vimeo (string access_token)
		{
			this.token = access_token;
			this.client = new HttpClient();
		}

		public void setAccessToken(string access_token){
			this.token = access_token;
		}

		public String getAccessToken(){
			return this.token;
		}



		public async  Task<Video> Upload(string filepath){

			if (!File.Exists (filepath)) {
				return null;
			}

			String ticket = this.GetUploadTicketId ().Result;

			if (!String.IsNullOrEmpty (ticket)) {

	

				//Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;






				String temp_filepath = Path.Combine (
					Environment.GetFolderPath (Environment.SpecialFolder.Personal), 
					Path.GetFileName (filepath) +  ".tmp"
				);

			

				if (File.Exists (temp_filepath)) {
					File.SetAttributes(temp_filepath, FileAttributes.Temporary);
					File.Delete (temp_filepath);
				}

				try{
					
					File.Copy (filepath, temp_filepath, true);
					File.SetAttributes(temp_filepath, FileAttributes.Temporary);

				}catch(UnauthorizedAccessException e){
					return null;
				}


				FileStream fileStream = null;
				try{

					fileStream = File.OpenRead(temp_filepath);



					HttpResponseMessage response = await client.PutAsync (
							this.upload_url + "/upload?ticket_id=" + ticket, 
							new StreamContent (fileStream)
					);

						response.EnsureSuccessStatusCode();

						File.Delete (temp_filepath);

						if (response.IsSuccessStatusCode) {

							return this.completeUpload ();

						}



					
				}
				catch(Exception e){

					var test = e;

				}
				finally{

					if (File.Exists (temp_filepath)) {
						File.SetAttributes(temp_filepath, FileAttributes.Temporary);
						File.Delete (temp_filepath);
					}

				
					if (fileStream != null) {
						fileStream.Close ();
						fileStream.Dispose ();
						fileStream = null;
					}


				}

			}

			return null;

		}

		private Video completeUpload(){

			string uri = this.api_url + this.complete_url;

			setAuth ();

			HttpResponseMessage response = client.DeleteAsync(uri).Result;

		

			if (response.IsSuccessStatusCode) {

				this.complete_url = null;
				string path = response.Headers.Location.AbsolutePath;


				if (!String.IsNullOrEmpty (path)) {

					//detalle del video
					var json_raw = this.get (path);
					if (!String.IsNullOrEmpty (json_raw)) {
						

						JObject json = JObject.Parse (json_raw);

						return new Video{
							Link = (string)json.GetValue ("link")
						};
					}
				}





			} 
				
			return null;
			

		}

		private  Task<string> GetUploadTicketId(){

			this.complete_url = null;
			return Task<string>.Factory.StartNew (() => {

				List<KeyValuePair<string, string>>postParameters = new List<KeyValuePair<string, string>>();
				postParameters.Add (new KeyValuePair<string, string>("type","streaming"));

				String json_raw   = this.post ("/me/videos", postParameters);

				if (!String.IsNullOrEmpty (json_raw)) {

					JObject json = JObject.Parse (json_raw);

					if (!String.IsNullOrEmpty (json.GetValue ("ticket_id").ToString ())) {

						try{

							string ticket    = json.GetValue ("ticket_id").ToString ();

							this.upload_url  = json.GetValue ("upload_link_secure").ToString ();

							this.complete_url = json.GetValue ("complete_uri").ToString ();

							return ticket;
							
						}catch(NullReferenceException e){
							
							string m = e.Message;

						}finally{
							
						}


					}

				}

				return null;

			});


		}

		private string get(string path){

			string uri = this.api_url + path;

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



		public string post ( string path,  List<KeyValuePair<string, string>>postParameters=null) {

				setAuth ();
		

				FormUrlEncodedContent DataContent = null;
				if (postParameters != null) {

					DataContent = new FormUrlEncodedContent (postParameters);

				}

				
				Task<HttpResponseMessage> postTask = null;
				try{

					postTask = client.PostAsync(this.api_url + path , DataContent);
					postTask.Wait();

				}catch(WebException e){

					throw new NetworkException ();

			}catch(ObjectDisposedException e){

				throw new NetworkException ();

			}

				if (postTask.Result.IsSuccessStatusCode) {

					DataContent.Dispose ();
					var readTask = postTask.Result.Content.ReadAsStringAsync();
					readTask.Wait();

					return readTask.Result;

				} else {
				
					DataContent.Dispose ();

					return null;
				}




		}

		private void setAuth(){
			
			var header = new AuthenticationHeaderValue("bearer", this.token);
			client.DefaultRequestHeaders.Authorization = header;
		}

	}

	public class Video{

		string link;

		public string Link {
			get {
				return link;
			}
			set {
				link = value;
			}
		}

		string thumb;

		public string Thumb {
			get {
				return thumb;
			}
			set {
				thumb = value;
			}
		}
	}
}

