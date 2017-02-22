using System;
using DSS.Event;

namespace DSS.Interfaces
{
	public interface IMediaFilePicker
	{

		void setTranslator(i18n translator);

		void pickAudio();

		void takeAudio();

		/*void pickImage();

		void takeImage(IFolderManager folderManager);

		void pickVideo();

		void takeVideo(IFolderManager folderManager);

	    event System.EventHandler VideoFilePicked;

	    event System.EventHandler ImageFilePicked;

	    

		void OnVideoFilePicked(MediaFilePickerEventArgs e);

		void OnImageFilePicked(MediaFilePickerEventArgs e);*/

		event System.EventHandler AudioFilePicked;

		void OnAudioFilePicked(MediaFilePickerEventArgs e);

	}
}

