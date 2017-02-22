using System;
using System.ComponentModel;
using DSS.Entity;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Dynamic;

namespace DSS.ViewModel
{
	public class ContentViewModel: INotifyPropertyChanged
	{
		public  event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}


		public const string FILE_TYPE_AUDIO = "audio";
		public const string FILE_TYPE_VIDEO = "video";
		public const string FILE_TYPE_IMAGE = "image";


		string contentUrl;

		public string ContentUrl {
			get {
				return contentUrl;
			}
			set {
				contentUrl = value;
			}
		}

		string thumbUrl;

		public string ThumbUrl {
			get {
				return thumbUrl;
			}
			set {
				thumbUrl = value;
			}
		}

		bool displayLayout;
		public bool DisplayLayout {
			get {
				return displayLayout;
			}
			set {
				if (displayLayout != value)
				{
					displayLayout = value;



					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("DisplayLayout"));
					}
				}
			}
		}

		bool loading;

		public bool Loading {
			get {
				return loading;
			}
			set {
				if (loading != value)
				{
					loading = value;



					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Loading"));
					}
				}
			}
		}

		bool enabled = true;

		public bool Enabled {
			get {
				return enabled;
			}
			set {
				if (enabled != value)
				{
					enabled = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Enabled"));
					}
				}
			}
		}


		ImageSource source;

		public ImageSource FileSource {
			get {
				return source;
			}
			set {

				if (source != value)
				{
					source = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("FileSource"));
					}
				}
			}
		}

		string 	   file;

		public string File {
			get {
				return file;
			}
			set {
				if (file != value)
				{
					file = value;

					FileSource = ImageSource.FromFile (file);

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("File"));
					}
				}
			}
		}

		string contentType;

		public string ContentType {
			get {
				return contentType;
			}
			set {
				contentType = value;
			}
		}

		string latitude;

		public string Latitude {
			get {
				return latitude;
			}
			set {
				latitude = value.Replace (",",".");
			}
		}

		string longitude;

		public string Longitude {
			get {
				return longitude;
			}
			set {
				longitude = value.Replace (",",".");
			}
		}

		string     title;

		public string Title {
			get {
				return title;
			}
			set {
				if (title != value)
				{
					title = value;

					OnPropertyChanged("Title");
				}
			}
		}

		string     description;

		public string Description {
			get {
				return description;
			}
			set {
				if (description != value)
				{
					description = value;

					OnPropertyChanged("Description");
				}
			}
		}

		Term       project;

		public Term Project {
			get {
				return project;
			}
			set {
				if (project != value)
				{
					project = value;

					OnPropertyChanged("Project");
				}
			}
		}




		List<Term> projects;

		public List<Term> Projects {
			get {
				return projects;
			}
			set {
				if (projects != value)
				{
					projects = value;

					OnPropertyChanged("Projects");
				}

			}
		}

		List<Term> topics;

		public List<Term> Topics {
			get {
				return topics;
			}
			set {
				topics = value;
			}
		}


		List<Term> routes;

		public List<Term> Routes {
			get {
				return routes;
			}
			set {
				routes = value;
			}
		}

		Term       route;

		public Term Route {
			get {
				return route;
			}
			set {
				if (route != value)
				{
					route = value;

					OnPropertyChanged("Route");
				}
			}
		}

		Int16      routePosition;

		public Int16 RoutePosition {
			get {
				return routePosition;
			}
			set {
				if (routePosition != value)
				{
					routePosition = value;

					OnPropertyChanged("RoutePosition");
				}
			}
		}


		Term city;

		public Term City {
			get {
				return city;
			}
			set {
				if (city != value)
				{
					city = value;

					OnPropertyChanged("City");
				}
			}
		}

		List<Term> cities;

		public List<Term> Cities {
			get {
				return cities;
			}
			set {
				cities = value;
			}
		}

		Int16      age;

		public Int16 Age {
			get {
				return age;
			}
			set {
				if (age != value)
				{
					age = value;

					OnPropertyChanged("Age");
				}
			}
		}

		Language       language;

		public Language Language {
			get {
				return language;
			}
			set {
				if (language != value)
				{
					language = value;

					OnPropertyChanged("Language");
				}
			}
		}

		Genre 	   genre;



		public Genre Genre {
			get {
				return genre;
			}
			set {
				if (genre != value)
				{
					genre = value;

					OnPropertyChanged("Genre");
				}
			}
		}


		public ContentViewModel(){
			this.Topics   = new List<Term>();
			this.Projects = new List<Term>();
			this.Routes = new List<Term> ();
			this.Route = null;
			this.Project  = null;
			this.Description = "";
			this.Age = 18;
		}


		public void loadFromJSON(
			JSONContent data, 
			List<Term> projects, 
			List<Term> topics,
			List<Language> languages,
			List<Genre> genres,
			List<Term> routes
		){

			Title       = data.title;
			Description = data.description;
			Latitude    = !String.IsNullOrEmpty (data.lat) ? data.lat : "";
			Longitude   = !String.IsNullOrEmpty (data.lon)?data.lon:"";
			ContentType = data.content_type;
			File        = data.file;

			RoutePosition =  (short)data.route_position; 
			if (!String.IsNullOrEmpty (data.route.ToString ())) {
				foreach(Term _route in routes){
					if (_route.Id == data.route) {
						Route = _route;
					}
				}
			}
			if (!String.IsNullOrEmpty (data.project.ToString ())) {
				foreach(Term _project in projects){
					if (_project.Id == data.project) {
						Project = _project;
					}
				}
			}

			Age = (short)data.age;

			if (!String.IsNullOrEmpty (data.genre.ToString ())) {
				foreach(Genre _genre in genres){
					if (_genre.Id == data.genre) {
						Genre = _genre;
					}
				}
			}

			if (!String.IsNullOrEmpty (data.language)) {
				foreach(Language _lang in languages){
					if (_lang.Iso == data.language) {
						Language = _lang;
					}
				}
			}

			foreach(var _topic in data.topics){

				foreach (var topic in topics) {

					if (topic.Id == (int)_topic) {
						Topics.Add (topic);
					}

				}

			}


		}

		public string getJSONString(){

			JSONContent item = new JSONContent();
			item.title        = Title;
			item.description  = Description;
			item.content_type = ContentType;
			item.project      = Project != null ? Project.Id : 0;
			item.lat = Latitude;
			item.lon = Longitude;
			item.file = File;

			item.route = Route != null ?  Route.Id : 0;
			item.route_position = RoutePosition;
			item.topics = new List<int>();

			item.language = Language != null ? Language.Iso : null;
			item.genre = Genre != null ? Genre.Id : "";
			item.age = Age;

			for (int i = 0; i < Topics.Count; i++) {
				var topic_id = Topics [i].Id;
				item.topics.Add (topic_id);
			}



			return JsonConvert.SerializeObject(item);

		}


		public List<KeyValuePair<string, string>> getValues(){

			int route_id = Route != null ? Route.Id : 0;
			int city_id  = City != null ? City.Id : 0;
			string genre_id = Genre != null ? Genre.Id : "";
			string lang_iso = Language != null ? Language.Iso : "";

			List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>> ();
			values.Add (new KeyValuePair<string, string>("content_type", ContentType));
			values.Add (new KeyValuePair<string, string>("project", Project.Id.ToString ()));
			values.Add (new KeyValuePair<string, string>("route", route_id.ToString () ));
			values.Add (new KeyValuePair<string, string>("route_position", RoutePosition.ToString ()));
			values.Add (new KeyValuePair<string, string>("title", Title));

			values.Add (new KeyValuePair<string, string>("description", Description));
			values.Add (new KeyValuePair<string, string>("city", city_id.ToString ()));
			values.Add (new KeyValuePair<string, string>("lat", Latitude));
			values.Add (new KeyValuePair<string, string>("lon", Longitude));
			values.Add (new KeyValuePair<string, string>("content_status", "publish"));

			values.Add (new KeyValuePair<string, string>("genre", genre_id.ToString ()));
			values.Add (new KeyValuePair<string, string>("language", lang_iso));
			values.Add (new KeyValuePair<string, string>("age", Age.ToString ()));


			if (!String.IsNullOrEmpty (this.ContentUrl)) {
				values.Add (new KeyValuePair<string, string>("content_url", this.ContentUrl));
			}

			if (!String.IsNullOrEmpty (this.ThumbUrl)) {
				values.Add (new KeyValuePair<string, string>("thumb_url", this.ThumbUrl));
			}

			for (int i = 0; i < Topics.Count; i++) {
				var topic_id = Topics [i].Id.ToString ();
				values.Add (new KeyValuePair<string, string>("topics[]", topic_id));
			}

			return values;

		}




	}
}

