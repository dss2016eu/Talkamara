using System;
using SQLite;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DSS.Entity
{
	public class Content
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Letter { get; set; }
		public string Date { get; set; }
		public string Project { get; set; }



		public string JSONData { get; set;}

		public Content ()
		{


		}

		public string getContentType(){
			switch (this.Letter) {

			case "I":
				return "image";
			case "V": 
				return "video";
				
			case "A": 
				return "audio";
				
			}

			return "content";
		}

		public void setLetter(string content_type){
			switch (content_type) {

			case "audio":
				this.Letter = "A";
				break;
			case "image": 
				this.Letter =  "I";
				break;
			case "video": 
				this.Letter =  "V";
				break;
			}

		}


	}

	public class JSONContent
	{
		public string title{get;set;}
		public string description{get;set;}
		public string content_type{get;set;}
		public string file{get;set;}
		public string preview{get;set;}
		public int project{get;set;}
		public string lat{get;set;}
		public string lon{get;set;}
		public int route{get;set;}
		public int route_position{get;set;}
		public string genre{get;set;}
		public int age{get;set;}
		public string language{get;set;}
		public List<int> topics{get;set;}
	}
}

