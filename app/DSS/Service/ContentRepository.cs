using System;
using DSS.Entity;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DSS.Service
{
	public class ContentRepository:AbstractConnection
	{



		public override void setUp(){
			this.dbconn.CreateTable<Content> ();
		}



		public Content get(int id){

			return dbconn.Find<Content> (id);

		}

		public Content getRemote(Client client, int id){

			var response = client.get ("contents/" + id.ToString ());

			if (!String.IsNullOrEmpty (response)) {
				var item = Newtonsoft.Json.Linq.JObject.Parse (response);


				Content content = new Content (){ 
					Id = (Int32)item ["id"], 
					Date = item["date"]!=null?item["date"].ToString() :"" ,
					Name = (string)item ["title"]
				};
				content.setLetter (item["content_type"].ToString ());
				content.JSONData = this.getJSONDataFromResponse (item);



				return content;
			}


			return null;

		}

		public int remove(Content item){

			return dbconn.Delete (item);

		}

		public List<Content> getAll(){
			return dbconn.Query<Content> ("Select * from Content order by ID DESC");
		}

		public List<Content> getAllRemote(Client client, int page=1){

			var response = client.get ("contents?page=" + page.ToString ());




			List<Content> items = new List<Content> ();

			if (!String.IsNullOrEmpty (response)) {

				var json = Newtonsoft.Json.Linq.JArray.Parse (response);

				foreach (var item in json.Children ()) {

					if (!Util.IsJTokenNullOrEmpty (item ["id"])) {


						try{

							Content content = new Content (){ 
								Id = (Int32)item ["id"], 
								Date = !Util.IsJTokenNullOrEmpty (item["date"])? item["date"].ToString () :"", 
								Name = (string)item ["title"], 
								Letter = "T" 
							};
							content.setLetter (item["content_type"].ToString ());
							content.JSONData = this.getJSONDataFromResponse (item);

							items.Add (content);

						}catch(FormatException e){
							
						}



					}



				}
			}



			return items;


		}


		public string getJSONDataFromResponse(JToken data){


			JSONContent item    = new JSONContent();
			item.title          = data["title"].ToString ();
			item.description    = Util.IsJTokenNullOrEmpty (data["description"])?"":data["description"].ToString ();
			item.content_type   = data["content_type"].ToString ();
				item.project        = Util.IsJTokenNullOrEmpty (data["project_id"])?0:(Int32)data["project_id"];
				item.lat 			= Util.IsJTokenNullOrEmpty (data["lat"])?"":data["lat"].ToString ();
				item.lon 			= Util.IsJTokenNullOrEmpty (data["lon"])?"":data["lon"].ToString ();
				item.file 			= Util.IsJTokenNullOrEmpty (data["content_url"])?"":data["content_url"].ToString ();
			item.preview 	     = Util.IsJTokenNullOrEmpty (data["preview_url"])?"":data["preview_url"].ToString ();
				item.route 			= Util.IsJTokenNullOrEmpty (data["route"]) ? 0 : (Int32)data["route"];
				item.route_position = Util.IsJTokenNullOrEmpty (data["route_position"]) ? 0 : (Int32)data["route_position"];
			item.topics 		= new List<int>();

				item.language 	    = Util.IsJTokenNullOrEmpty (data["language"])? "" : data["language"].ToString ();
				item.genre 		    = Util.IsJTokenNullOrEmpty (data["genre"])? "" : data["genre"].ToString ();


			item.age 		    = Util.IsJTokenNullOrEmpty (data["age"])? 0 : (Int32)data["age"];

			if (data ["topics"] != null) {
				foreach(var topic_id in data["topics"]){
					
					if(topic_id!=null) 
						item.topics.Add ((Int32)topic_id);
					
				}
			}



			return JsonConvert.SerializeObject(item);

		}

		public Content set(Content content){


			if ( content.Id != 0 ) {
				dbconn.Update (content);
			} else {
				dbconn.Insert (content);
			}



			return content;

		}




	}
}

