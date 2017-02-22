using System;
using System.Collections.Generic;

namespace DSS
{
	public class EnStringCatalog:AbstractStringsCatalog
	{


		#region implemented abstract members of AbstractStringsCatalog

		public override void loadCatalog ()
		{

			this.catalog = new Dictionary<string, string> ();

			this.catalog.Add ("Accept", "Accept");
			this.catalog.Add ("Cancel", "Cancel");
			this.catalog.Add ("ChooseAnAction", "Choose an option");
			this.catalog.Add ("Error", "Error");
			this.catalog.Add ("Notice", "Attention");
			this.catalog.Add ("Success", "Success");

			this.catalog.Add ("LoginUsername", "Your user");
			this.catalog.Add ("LoginPassword", "Your password");
			this.catalog.Add ("LoginButton", "Enter");
			this.catalog.Add ("LoginInternetRequired", "No connection detected. You must be connected to the Internet to log in.");
			this.catalog.Add ("LoginIncorrectData", "Data are not correct");


			this.catalog.Add ("Logout", "Disconnect");
			this.catalog.Add ("GoToUploadQueue", "Go to upload queue");
			this.catalog.Add ("UploadVideo", "Upload video");
			this.catalog.Add ("UploadImage", "Upload image");
			this.catalog.Add ("UploadAudio", "Upload audio");


			this.catalog.Add ("UploadInternetRequired", "You are not connected to the Internet, you will not be able to upload content until you are connected");
			this.catalog.Add ("UploadVimeoRequired", "Vimeo is not configured correctly in the platform,  it is not possible to upload videos");

			this.catalog.Add ("ContentUploading", "Uploading content…");

			this.catalog.Add ("QueueTitle", "Videos in queue");
			this.catalog.Add ("UploadToVimeo", "Upload to Vimeo");
			this.catalog.Add ("RemoveContent", "Remove contento");
			this.catalog.Add ("ContentRemovedMessage", "Content removed correctly");

			this.catalog.Add ("ChooseTopics", "Choose one or several themes");
			this.catalog.Add ("UploadContent", "Upload content");
			this.catalog.Add ("InternetRequiredTryItLater", "You are not connected to the Internet, try again later.");
			this.catalog.Add ("LocationLabel", "Position (touch to update)");
			this.catalog.Add ("ChooseFile", "Choose a file");
			this.catalog.Add ("ChooseFileImage", "Choose file or take photo");
			this.catalog.Add ("ChooseFileVideo", "Choose file or record video");
			this.catalog.Add ("ChooseFileAudio", "Choose file or record audio");
			this.catalog.Add ("Project", "Project");
			this.catalog.Add ("City", "Town");
			this.catalog.Add ("ContentTitle", "Content title");
			this.catalog.Add ("ContentTitleDescription", "Title and description");
			this.catalog.Add ("SelectTopics", "Select themes");
			this.catalog.Add ("Topics", "Themes");
			this.catalog.Add ("Route", "Path");
			this.catalog.Add ("RoutePosition", "Position on the path");
			this.catalog.Add ("Position", "Position");
			this.catalog.Add ("RouteSelection", "Path selection (optional)");
			this.catalog.Add ("InterviewData", "Interview data (optional)");
			this.catalog.Add ("PersonAge", "Age of the person");
			this.catalog.Add ("PersonGenre", "Sex of the person");
			this.catalog.Add ("PersonLanguage", "Language of the person");
			this.catalog.Add ("UploadingContent", "Uploading content");
			this.catalog.Add ("ContentTitleRequired", "You must provide a title");
			this.catalog.Add ("ContentFileRequired", "You must select a file");
			this.catalog.Add ("ContentProjectRequired", "You must select a project");
			this.catalog.Add ("FileQueued", "File in upload queue");
			this.catalog.Add ("FileQueuedMessage", "No Wi-Fi connection available, file added to upload queue");
			this.catalog.Add ("VimeoErrorMessage", "An error occurred while trying to upload the file to Vimeo");
			this.catalog.Add ("UploadContentError", "Error while uploading content");
			this.catalog.Add ("UploadContentErrorMessage", "An error occurred while uploading file");
			this.catalog.Add ("ContentUploaded", "Content uploaded");
			this.catalog.Add ("ContentUploadedMessage", "File uploaded correctly");
			this.catalog.Add ("AttachImage", "Attach image");
			this.catalog.Add ("AttachAudio", "Attach audio");
			this.catalog.Add ("ChooseAudioFile", "Choose audio file");

			this.catalog.Add ("ChooseFromGalery", "Choose from gallery");
			this.catalog.Add ("TakeWithCam", "Take with camera");
			this.catalog.Add ("AttachVideo", "Attach video");
			this.catalog.Add ("RecordWithCam", "Record with camera");
			this.catalog.Add ("Record", "Record audio");
			this.catalog.Add ("GeolocationDeactivatedMessage", "GPS or location off");
			this.catalog.Add ("CantDeterminePosition", "Position cannot be determined");


			this.catalog.Add ("SelectARoute", "Select a path");
			this.catalog.Add ("SelectALanguage", "Select a language");
			this.catalog.Add ("SelectACity", "Select a town");




			this.catalog.Add ("GenreUndefined", "Not defined");
			this.catalog.Add ("GenreMale", "Male");
			this.catalog.Add ("GenreFemale", "Female");

			this.catalog.Add ("AudioRecorderNotFound", "No sound recording application found");

			this.catalog.Add ("LoadMore", "Load more");
			this.catalog.Add ("GoToContent", "Go to your content");
			this.catalog.Add ("UserContentTitle", "Content uploaded");

			this.catalog.Add ("UpdateErrorMessage", "Error while updating data");
			this.catalog.Add ("UpdateSuccessMessage", "Data updated correctly");
			this.catalog.Add ("Options", "Options");
			this.catalog.Add ("LanguageLabel", "Language");


			this.catalog.Add ("NotAValidUrl", "Specified address is not valid");
			this.catalog.Add ("ProjectConfigLoadError", "Error loading project configuration");
			this.catalog.Add ("LoginUrl", "Project address (with http:// or https://)");

		}

		#endregion


		

	}
}

