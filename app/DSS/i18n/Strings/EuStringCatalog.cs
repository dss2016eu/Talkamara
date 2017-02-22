using System;
using System.Collections.Generic;

namespace DSS
{
	public class EuStringCatalog:AbstractStringsCatalog
	{


		#region implemented abstract members of AbstractStringsCatalog

		public override void loadCatalog ()
		{

			this.catalog = new Dictionary<string, string> ();

			this.catalog.Add ("Accept", "Onartu");
			this.catalog.Add ("Cancel", "Ezeztatu");
			this.catalog.Add ("ChooseAnAction", "Egin aukera bat");
			this.catalog.Add ("Error", "Errorea");
			this.catalog.Add ("Notice", "Kontuz");
			this.catalog.Add ("Success", "Ederki");

			this.catalog.Add ("LoginUsername", "Erabiltzailea");
			this.catalog.Add ("LoginPassword", "Pasahitza");
			this.catalog.Add ("LoginButton", "Sartu");
			this.catalog.Add ("LoginInternetRequired", "Ez dugu konexiorik detektatu. Saioa hasteko, internetera konektatu behar duzu.");
			this.catalog.Add ("LoginIncorrectData", "Datuak ez dira zuzenak.");


			this.catalog.Add ("Logout", "Deskonektatu");
			this.catalog.Add ("GoToUploadQueue", "Joan igotzeko zerrendara");
			this.catalog.Add ("UploadVideo", "Igo bideoa");
			this.catalog.Add ("UploadImage", "Igo irudia");
			this.catalog.Add ("UploadAudio", "Igo audioa");


			this.catalog.Add ("UploadInternetRequired", "Ez zaude internetera konektatuta, ezingo duzu edukirik igo konektatu ezean.");
			this.catalog.Add ("UploadVimeoRequired", "Vimeo ez dago behar bezala konfiguratuta plataforman. Ezingo duzu bideorik igo.");

			this.catalog.Add ("ContentUploading", "Edukia igotzen...");

			this.catalog.Add ("QueueTitle", "Igotzeko zain dauden bideoak");
			this.catalog.Add ("UploadToVimeo", "Vimeora igo");
			this.catalog.Add ("RemoveContent", "Ezabatu edukia");
			this.catalog.Add ("ContentRemovedMessage", "Edukia behar bezala ezabatu da.");

			this.catalog.Add ("ChooseTopics", "Aukeratu gai bat edo batzuk.");
			this.catalog.Add ("UploadContent", "Igo edukia");
			this.catalog.Add ("InternetRequiredTryItLater", "Ez zaude internetera konektatuta, ezingo duzu edukirik igo konektatu ezean.");
			this.catalog.Add ("LocationLabel", "Kokapena (ukitu eguneratzeko)");
			this.catalog.Add ("ChooseFile", "Aukeratu artxibo bat");
			this.catalog.Add ("ChooseFileImage", "Aukeratu artxiboa edo atera argazkia");
			this.catalog.Add ("ChooseFileVideo", "Aukeratu artxiboa edo grabatu bideoa");
			this.catalog.Add ("ChooseFileAudio", "Aukeratu artxiboa edo grabatu audioa");
			this.catalog.Add ("Project", "Proiektua");
			this.catalog.Add ("City", "Udalerria");
			this.catalog.Add ("ContentTitle", "Edukiaren izenburua");
			this.catalog.Add ("ContentTitleDescription", "Izenburua eta deskribapena");
			this.catalog.Add ("SelectTopics", "Aukeratu gaiak");
			this.catalog.Add ("Topics", "Gaiak");
			this.catalog.Add ("Route", "Ibilbidea");
			this.catalog.Add ("RoutePosition", "Ibilbideko kokapena");
			this.catalog.Add ("Position", "Posizioa");
			this.catalog.Add ("RouteSelection", "Aukeratu bidea (aukerakoa)");
			this.catalog.Add ("InterviewData", "Elkarrizketaren datuak (aukerakoa)");
			this.catalog.Add ("PersonAge", "Pertsonaren adina");
			this.catalog.Add ("PersonGenre", "Pertsonaren generoa");
			this.catalog.Add ("PersonLanguage", "Pertsonaren hizkuntza");
			this.catalog.Add ("UploadingContent", "Edukia igotzen...");
			this.catalog.Add ("ContentTitleRequired", "Izenburu bat zehaztu behar duzu");
			this.catalog.Add ("ContentFileRequired", "Artxibo bat aukeratu behar duzu");
			this.catalog.Add ("ContentProjectRequired", "Proiektua aukeratu behar duzu");
			this.catalog.Add ("FileQueued", "Artxiboa igotzeko zain");
			this.catalog.Add ("FileQueuedMessage", "Ez daukazu WiFi konexiorik, artxiboa ez dugu erantsi igotzeko zerrendara.");
			this.catalog.Add ("VimeoErrorMessage", "Errore bat gertatu da artxiboa Vimeora igotzeko garaian.");
			this.catalog.Add ("UploadContentError", "Errorea artxiboa igotzerakoan");
			this.catalog.Add ("UploadContentErrorMessage", "Errore bat gertatu da artxiboa igotzeko garaian.");
			this.catalog.Add ("ContentUploaded", "Edukia igo da");
			this.catalog.Add ("ContentUploadedMessage", "Artxiboa ondo kargatu da");
			this.catalog.Add ("AttachImage", "Atxiki irudia");
			this.catalog.Add ("AttachAudio", "Atxiki audioa");
			this.catalog.Add ("ChooseAudioFile", "Aukeratu audio-artxiboa");

			this.catalog.Add ("ChooseFromGalery", "Aukeratu galeriatik");
			this.catalog.Add ("TakeWithCam", "Atera kamerarekin");
			this.catalog.Add ("AttachVideo", "Atxiki bideoa");
			this.catalog.Add ("RecordWithCam", "Grabatu kamerarekin");
			this.catalog.Add ("Record", "Grabatu audioa");
			this.catalog.Add ("GeolocationDeactivatedMessage", "GPSa edo lokalizazioa desaktibatuta daude.");
			this.catalog.Add ("CantDeterminePosition", "Ezin da kokapena zehaztu");


			this.catalog.Add ("SelectARoute", "Ibilbidea aukeratu");
			this.catalog.Add ("SelectALanguage", "Hizkuntza aukeratu");
			this.catalog.Add ("SelectACity", "Udalerria aukeratu");




			this.catalog.Add ("GenreUndefined", "Zehaztu gabe");
			this.catalog.Add ("GenreMale", "Gizona");
			this.catalog.Add ("GenreFemale", "Emakumea");

			this.catalog.Add ("AudioRecorderNotFound", "Ez dugu soinua grabatzeko aplikaziorik topatu");

			this.catalog.Add ("LoadMore", "Gehiago kargatu");
			this.catalog.Add ("GoToContent", "Zeure edukira joan");
			this.catalog.Add ("UserContentTitle", "Edukia igo da");

			this.catalog.Add ("UpdateErrorMessage", "Errorea datuak eguneratzean");
			this.catalog.Add ("UpdateSuccessMessage", "Datuak behar bezala eguneratu dira");
			this.catalog.Add ("Options", "Aukerak");
			this.catalog.Add ("LanguageLabel", "Hizkuntza");

			this.catalog.Add ("NotAValidUrl", "Adierazitako helbidea ez da zuzena");
			this.catalog.Add ("ProjectConfigLoadError", "Errorea proiektuaren ezarpena hornitzean");
			this.catalog.Add ("LoginUrl", "Proiektuaren helbidea (http:// edo https://)");
		}

		#endregion


		

	}
}

