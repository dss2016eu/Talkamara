using System;
using System.Collections.Generic;

namespace DSS
{
	public class FrStringCatalog:AbstractStringsCatalog
	{


		#region implemented abstract members of AbstractStringsCatalog

		public override void loadCatalog ()
		{

			this.catalog = new Dictionary<string, string> ();

			this.catalog.Add ("Accept", "Accepter");
			this.catalog.Add ("Cancel", "Annuler");
			this.catalog.Add ("ChooseAnAction", "Sélectionnez une option");
			this.catalog.Add ("Error", "Erreur");
			this.catalog.Add ("Notice", "Attention");
			this.catalog.Add ("Success", "Opération réussie");

			this.catalog.Add ("LoginUsername", "Votre identifiant");
			this.catalog.Add ("LoginPassword", "Votre mot de passe");
			this.catalog.Add ("LoginButton", "Se connecter");
			this.catalog.Add ("LoginInternetRequired", "Aucune connexion n'a été détectée. Vous devez vous connecter à Internet pour ouvrir une session.");
			this.catalog.Add ("LoginIncorrectData", "Ces données ne sont pas correctes");


			this.catalog.Add ("Logout", "Se déconnecter");
			this.catalog.Add ("GoToUploadQueue", "Accéder à file d'attente de téléchargement");
			this.catalog.Add ("UploadVideo", "Envoyer vidéo");
			this.catalog.Add ("UploadImage", "Envoyer image");
			this.catalog.Add ("UploadAudio", "Envoyer audio");


			this.catalog.Add ("UploadInternetRequired", "Vous n'êtes pas connecté à Internet, aucun contenu ne pourra être envoyé sans connexion préalable.");
			this.catalog.Add ("UploadVimeoRequired", "Vimeo n'est pas correctement configuré sur la plateforme, aucune vidéo ne pourra être envoyée. ");

			this.catalog.Add ("ContentUploading", "Contenu en cours de chargement…");

			this.catalog.Add ("QueueTitle", "Vidéos en file d'attente");
			this.catalog.Add ("UploadToVimeo", "Envoyer sur Vimeo");
			this.catalog.Add ("RemoveContent", "Supprimer du contenu");
			this.catalog.Add ("ContentRemovedMessage", "Le contenu a bien été supprimé");

			this.catalog.Add ("ChooseTopics", "Sélectionnez un ou plusieurs thèmes");
			this.catalog.Add ("UploadContent", "Envoyer du contenu");
			this.catalog.Add ("InternetRequiredTryItLater", "Vous n'êtes pas connecté à Internet, veuillez réessayer ultérieurement.");
			this.catalog.Add ("LocationLabel", "Position (touchez l'écran pour mettre à jour)");
			this.catalog.Add ("ChooseFile", "Sélectionnez un fichier");
			this.catalog.Add ("ChooseFileImage", "Sélectionner fichier ou prendre photo");
			this.catalog.Add ("ChooseFileVideo", "Sélectionner fichier ou enregistrer vidéo");
			this.catalog.Add ("ChooseFileAudio", "Sélectionner fichier ou enregistrer audio");
			this.catalog.Add ("Project", "Projet");
			this.catalog.Add ("City", "Ville");
			this.catalog.Add ("ContentTitle", "Titre du contenu");
			this.catalog.Add ("ContentTitleDescription", "Titre et description");
			this.catalog.Add ("SelectTopics", "Sélectionnez des thèmes");
			this.catalog.Add ("Topics", "Thèmes");
			this.catalog.Add ("Route", "Chemin d'accès");
			this.catalog.Add ("RoutePosition", "Position dans le chemin d'accès");
			this.catalog.Add ("Position", "Position");
			this.catalog.Add ("RouteSelection", "Sélection du chemin d'accès (optionnel)");
			this.catalog.Add ("InterviewData", "Données de l'interview (optionnel)");
			this.catalog.Add ("PersonAge", "Âge de la personne");
			this.catalog.Add ("PersonGenre", "Genre de la personne");
			this.catalog.Add ("PersonLanguage", "Langage de la personne");
			this.catalog.Add ("UploadingContent", "Contenu en cours de chargement…");
			this.catalog.Add ("ContentTitleRequired", "Vous devez indiquer un titre");
			this.catalog.Add ("ContentFileRequired", "Vous devez sélectionner un fichier");
			this.catalog.Add ("ContentProjectRequired", "Vous devez sélectionner un projet");
			this.catalog.Add ("FileQueued", "Fichier placé en file d'attente de téléchargement");
			this.catalog.Add ("FileQueuedMessage", "Vous ne disposez pas de connexion Wifi, le fichier a été ajouté en file d'attente de télechargement.");
			this.catalog.Add ("VimeoErrorMessage", "Une erreur est survenue lors de l'envoi du fichier sur Vimeo.");
			this.catalog.Add ("UploadContentError", "Erreur survenue lors de l'envoi du contenu");
			this.catalog.Add ("UploadContentErrorMessage", "Une erreur est survenue lors de l'envoi du fichier");
			this.catalog.Add ("ContentUploaded", "Contenu envoyé");
			this.catalog.Add ("ContentUploadedMessage", "Fichier correctement envoyé");
			this.catalog.Add ("AttachImage", "Joindre image");
			this.catalog.Add ("AttachAudio", "Joindre audio");
			this.catalog.Add ("ChooseAudioFile", "Sélectionner fichier audio");

			this.catalog.Add ("ChooseFromGalery", "Sélectionner dans la galerie");
			this.catalog.Add ("TakeWithCam", "Prendre une photo avec l'appareil");
			this.catalog.Add ("AttachVideo", "Joindre vidéo");
			this.catalog.Add ("RecordWithCam", "Enregistrer avec l'appareil photo");
			this.catalog.Add ("Record", "Enregistrer audio");
			this.catalog.Add ("GeolocationDeactivatedMessage", "Le GPS ou la localisation sont désactivés.");
			this.catalog.Add ("CantDeterminePosition", "La position ne peut être déterminée");


			this.catalog.Add ("SelectARoute", "Sélectionnez un chemin d'accès");
			this.catalog.Add ("SelectALanguage", "Sélectionnez une langue");
			this.catalog.Add ("SelectACity", "Sélectionnez une ville");




			this.catalog.Add ("GenreUndefined", "Non défini");
			this.catalog.Add ("GenreMale", "Homme");
			this.catalog.Add ("GenreFemale", "Femme");

			this.catalog.Add ("AudioRecorderNotFound", "Aucune application enregistreur de son n'a été trouvée");

			this.catalog.Add ("LoadMore", "Charger plus de contenu");
			this.catalog.Add ("GoToContent", "Accédez à votre contenu");
			this.catalog.Add ("UserContentTitle", "Contenu envoyé");

			this.catalog.Add ("UpdateErrorMessage", "Erreur survenue lors de la mise à jour des données");
			this.catalog.Add ("UpdateSuccessMessage", "Mise à jour des données correctement effectuée");
			this.catalog.Add ("Options", "Options");
			this.catalog.Add ("LanguageLabel", "Langue");

	
			this.catalog.Add ("NotAValidUrl", "L’adresse indiquée est invalide");
			this.catalog.Add ("ProjectConfigLoadError", "Erreur lors du chargement de la configuration du projet");
			this.catalog.Add ("LoginUrl", "Adresse du projet (avec http:// ou https://)");

		}

		#endregion


		

	}
}

