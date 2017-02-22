using System;
using System.Collections.Generic;
using DSS.Entity;

namespace DSS
{
	public class TaxonomyModel
	{

		private string data;

		public const string CACHE_KEY = "taxonomies.list";

		public IStorage storage;
		public Client  client;

		private List<Term> topics;
		private List<Term> projects;
		private List<Term> routes;
		private List<Term> cities;

		private List<Language> languages;

		private i18n translator;

		public TaxonomyModel (Client client, IStorage storage, i18n translator)
		{
			this.storage = storage;
			this.client = client;

			this.projects = new List<Term>();
			this.topics = new List<Term>();
			this.routes = new List<Term> ();
			this.languages = new List<Language> (); 

			this.translator = translator;

		}

		public void resetCache(){
			this.storage.remove (TaxonomyModel.CACHE_KEY);
		}

		public void updateData(){


			data = storage.get (TaxonomyModel.CACHE_KEY);

			if (string.IsNullOrEmpty (data)) {
				data = client.get ("taxonomies");

				if (!string.IsNullOrEmpty (data)) {
					storage.set ("taxonomies.list", data, new TimeSpan(1));
				} else
					return;


			}


			var json = Newtonsoft.Json.Linq.JObject.Parse (data);
			if (topics == null) {
				topics = new List<Term> ();
			} else {
				topics.Clear ();
			}

			if (projects == null) {
				projects = new List<Term> ();
			} else {
				projects.Clear ();
			}

			if (routes == null) {
				routes = new List<Term> ();
			} else {
				routes.Clear ();
			}

			if (cities == null) {
				cities = new List<Term> ();
			} else {
				cities.Clear ();
			}

			this.routes.Add (new Term{
				Id = 0,
				Name = translator.Trans ("SelectARoute")
			});

			this.cities.Add (new Term{
				Id = 0,
				Name = translator.Trans ("SelectACity")
			});


			if (languages == null) {
				languages = new List<Language> ();
			} else {
				languages.Clear ();
			}
			this.languages.Add (new Language{
				Iso = "",
				Name = translator.Trans ("SelectALanguage")
			});

			foreach (var item in json["topics"]) {

				topics.Add (new Term(){ Id= (Int32)item["id"], Name=(string)item["name"] });

			}

			foreach (var item in json["projects"]) {

				projects.Add (new Term(){ Id= (Int32)item["id"], Name=(string)item["name"] });

			}

			foreach (var item in json["routes"]) {

				routes.Add (new Term(){ Id= (Int32)item["id"], Name=(string)item["name"] });

			}

			foreach (var item in json["cities"]) {

				cities.Add (new Term(){ Id= (Int32)item["id"], Name=(string)item["name"] });

			}

			foreach (var item in json["languages"]) {

				languages.Add (new Language(){ Iso= (string)item["iso"], Name=(string)item["name"] });

			}
		}


		public List<Term> getTopics(){

			if (this.topics.Count<1) {
				updateData ();
			}

			return topics;
		}

		public List<Term> getProjects(){

			if (this.projects.Count<1) {
				updateData ();
			}

			return projects;
		}


		public List<Term> getRoutes(){

			if (this.routes.Count<1) {
				updateData ();
			}

			return routes;
		}

		public List<Term> getCities(){

			if (this.cities.Count<1) {
				updateData ();
			}

			return cities;
		}

		public List<Language> getLanguages(){

			if (this.languages.Count<1) {
				updateData ();
			}

			return this.languages;
		}

		public List<Genre> getGenres(){

			List<Genre> items = new List<Genre> ();

			items.Add (new Genre(){
				Id = "",
				Name = translator.Trans ("GenreUndefined")
			});

			items.Add (new Genre(){
				Id = "female",
				Name = translator.Trans ("GenreFemale")
			});

			items.Add (new Genre(){
				Id = "male",
				Name =  translator.Trans ("GenreMale")
			});

			return items;
		}

	}
}

