using System;
using System.Collections.Generic;

namespace DSS
{
	public class EsStringCatalog:AbstractStringsCatalog
	{


		#region implemented abstract members of AbstractStringsCatalog

		public override void loadCatalog ()
		{

			this.catalog = new Dictionary<string, string> ();

			this.catalog.Add ("Accept", "Aceptar");
			this.catalog.Add ("Cancel", "Cancelar");
			this.catalog.Add ("ChooseAnAction", "Elige una opción");
			this.catalog.Add ("Error", "Error");
			this.catalog.Add ("Notice", "Atención");
			this.catalog.Add ("Success", "Éxito");

			this.catalog.Add ("LoginUsername", "Tu usuario");
			this.catalog.Add ("LoginPassword", "Tu clave");
			this.catalog.Add ("LoginButton", "Entrar");
			this.catalog.Add ("LoginInternetRequired", "No se ha detectado ninguna conexión. Debes conectarte a Internet para iniciar sesión.");
			this.catalog.Add ("LoginIncorrectData", "Los datos no son correctos.");


			this.catalog.Add ("Logout", "Desconectar");
			this.catalog.Add ("GoToUploadQueue", "ir a cola de subida");
			this.catalog.Add ("UploadVideo", "Subir video");
			this.catalog.Add ("UploadImage", "Subir imagen");
			this.catalog.Add ("UploadAudio", "Subir audio");


			this.catalog.Add ("UploadInternetRequired", "No estás conectado a internet, no podrás subir contenido hasta que te conectes.");
			this.catalog.Add ("UploadVimeoRequired", "Vimeo no está configurado correctamente en la plataforma, no se podrán subir vídeos");

			this.catalog.Add ("ContentUploading", "Subiendo contenido...");

			this.catalog.Add ("QueueTitle", "Videos en cola");
			this.catalog.Add ("UploadToVimeo", "Subir a vimeo");
			this.catalog.Add ("RemoveContent", "Eliminar contenido");
			this.catalog.Add ("ContentRemovedMessage", "Contenido eliminado correctamente.");

			this.catalog.Add ("ChooseTopics", "Elija una o varias temáticas");
			this.catalog.Add ("UploadContent", "Subir contenido");
			this.catalog.Add ("InternetRequiredTryItLater", "No estás conectado a internet, inténtalo de nuevo más tarde.");
			this.catalog.Add ("LocationLabel", "Posición (toca para actualizar)");
			this.catalog.Add ("ChooseFile", "Elige un archivo");
			this.catalog.Add ("ChooseFileImage", "Elegir archivo o sacar foto");
			this.catalog.Add ("ChooseFileVideo", "Elegir archivo o grabar vídeo");
			this.catalog.Add ("ChooseFileAudio", "Elegir archivo o grabar audio");
			this.catalog.Add ("Project", "Proyecto");
			this.catalog.Add ("City", "Localidad");
			this.catalog.Add ("ContentTitle", "Título del contenido");
			this.catalog.Add ("ContentTitleDescription", "Título y descripción");
			this.catalog.Add ("SelectTopics", "Seleccione temáticas");
			this.catalog.Add ("Topics", "Temáticas");
			this.catalog.Add ("Route", "Ruta");
			this.catalog.Add ("RoutePosition", "Posición en la ruta");
			this.catalog.Add ("Position", "Posición");
			this.catalog.Add ("RouteSelection", "Selección de ruta (opcional)");
			this.catalog.Add ("InterviewData", "Datos de la entrevista (opcional)");
			this.catalog.Add ("PersonAge", "Edad de la persona");
			this.catalog.Add ("PersonGenre", "Género de la persona");
			this.catalog.Add ("PersonLanguage", "Lenguaje de la persona");
			this.catalog.Add ("UploadingContent", "Subiendo contenido...");
			this.catalog.Add ("ContentTitleRequired", "Debes indicar un título");
			this.catalog.Add ("ContentFileRequired", "Debes seleccionar un archivo");
			this.catalog.Add ("ContentProjectRequired", "Debes seleccionar un proyecto");
			this.catalog.Add ("FileQueued", "Archivo en cola de subida");
			this.catalog.Add ("FileQueuedMessage", "No dispones de conexión Wifi, el archivo se ha añadido a la cola de subida.");
			this.catalog.Add ("VimeoErrorMessage", "Ocurrió un error intentando subir el archivo a Vimeo.");
			this.catalog.Add ("UploadContentError", "Error al subir contenido");
			this.catalog.Add ("UploadContentErrorMessage", "Ocurrió un error al subir el archivo");
			this.catalog.Add ("ContentUploaded", "Contenido subido");
			this.catalog.Add ("ContentUploadedMessage", "Archivo subido correctamente");
			this.catalog.Add ("AttachImage", "Adjuntar imagen");
			this.catalog.Add ("AttachAudio", "Adjuntar audio");
			this.catalog.Add ("ChooseAudioFile", "Elegir archivo de audio");

			this.catalog.Add ("ChooseFromGalery", "Elegir de la galería");
			this.catalog.Add ("TakeWithCam", "Sacar con la cámara");
			this.catalog.Add ("AttachVideo", "Adjuntar vídeo");
			this.catalog.Add ("RecordWithCam", "Grabar con la cámara");
			this.catalog.Add ("Record", "Grabar audio");
			this.catalog.Add ("GeolocationDeactivatedMessage", "El GPS o la localización están desactivados.");
			this.catalog.Add ("CantDeterminePosition", "No se puede determinar la posición");


			this.catalog.Add ("SelectARoute", "Selecciona una ruta");
			this.catalog.Add ("SelectALanguage", "Selecciona un idioma");
			this.catalog.Add ("SelectACity", "Selecciona una localidad");




			this.catalog.Add ("GenreUndefined", "No definido");
			this.catalog.Add ("GenreMale", "Hombre");
			this.catalog.Add ("GenreFemale", "Mujer");

			this.catalog.Add ("AudioRecorderNotFound", "No se encontró ninguna aplicación grabadora de sonido");

			this.catalog.Add ("LoadMore", "Cargar más");
			this.catalog.Add ("GoToContent", "ir a tu contenido");
			this.catalog.Add ("UserContentTitle", "Contenido subido");

			this.catalog.Add ("UpdateErrorMessage", "Error actualizando los datos");
			this.catalog.Add ("UpdateSuccessMessage", "Datos actualizados correctamente");
			this.catalog.Add ("Options", "Opciones");
			this.catalog.Add ("LanguageLabel", "Idioma");

			this.catalog.Add ("NotAValidUrl", "La dirección indicada no es válida");
			this.catalog.Add ("ProjectConfigLoadError", "Error al cargar configuración de proyecto");
			this.catalog.Add ("LoginUrl", "Dirección del proyecto (con http:// o https://)");

		}

		#endregion


		

	}
}

