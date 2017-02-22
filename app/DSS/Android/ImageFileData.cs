
using Android.Graphics;
using Android.Media;
using Java.IO;
using DSS.Interfaces;
using Android.Content;
using Android.Net;
using Android.App;
using Android.Provider;
using DSS.Droid;
using System.Threading.Tasks;
using Java.Lang;

namespace DSS.Android
{
	public class ImageFileData:IImageFileData
	{

		MainActivity context;

		public ImageFileData (MainActivity context)
		{

			this.context = context;

		}

		public  string rotateByMetadata(string fileName, bool broadcast=true){


				string old = fileName;
		
				fileName = System.IO.Path.Combine (
					System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), 
					System.IO.Path.GetFileName(old) 
				);



				System.IO.File.Copy (old, fileName, true);
				System.IO.File.SetAttributes(fileName, System.IO.FileAttributes.Temporary);


				File file = new File (fileName);
				if(file.Exists ()){

					Bitmap resizedBitmap = BitmapFactory.DecodeStream (this.context.ContentResolver.OpenInputStream (Uri.FromFile (file)));
					using (Matrix mtx = new Matrix ()) {


						ExifInterface exif = new ExifInterface (fileName);
						var orientation = (Orientation)exif.GetAttributeInt (ExifInterface.TagOrientation, (int)Orientation.Undefined);



						try {

							bool transform = false;

							switch (orientation) {
								case Orientation.Undefined: // Nexus 7 landscape...
									break;
								case Orientation.Normal: // landscape
									break;
								case Orientation.FlipHorizontal:
									break;
								case Orientation.Rotate180:
									mtx.PostRotate (180);
									transform = true;
									break;
								case Orientation.FlipVertical:
									break;
								case Orientation.Transpose:
									break;
								case Orientation.Rotate90: // portrait
									mtx.PreRotate (90);
									transform = true;
									break;
								case Orientation.Transverse:
									break;
								case Orientation.Rotate270: // might need to flip horizontally too...
									mtx.PreRotate (270);
									transform = true;
									break;
								default:
									mtx.PreRotate (90);
									transform = true;
									break;
							}

							if(transform){

							   resizedBitmap = Bitmap.CreateBitmap (resizedBitmap, 0, 0, resizedBitmap.Width, resizedBitmap.Height, mtx, false);

							}else{
							
								return old;

							}




						} catch (OutOfMemoryError e) {
							this.disponseBitmap (resizedBitmap);
							return fileName;

						} catch (System.AggregateException e) {
							this.disponseBitmap (resizedBitmap);
							return fileName;


						}finally{
						


						}


						

					}

					using (System.IO.FileStream stream = new System.IO.FileStream (fileName, System.IO.FileMode.Create)) {

						try {

							if(stream != null){

								resizedBitmap.CompressAsync (Bitmap.CompressFormat.Jpeg, 80, stream);

								if(broadcast){
									string uri = MediaStore.Images.Media.InsertImage (this.context.ContentResolver, resizedBitmap, System.IO.Path.GetFileName (fileName), System.IO.Path.GetFileName (fileName));
									this.context.SendBroadcast (new Intent (Intent.ActionMediaScannerScanFile, Uri.Parse (uri)));
								}

							}


						} catch (System.AggregateException e) {

							var m = e.Message;


						} catch (OutOfMemoryError e) {

							var m = e.Message;

						}catch(System.ArgumentException e){

							var m = e.Message;

						} finally {

							this.disponseBitmap (resizedBitmap);
							System.GC.Collect ();

						}


				}



				}

				return fileName;


		

		}


		private void disponseBitmap(Bitmap resizedBitmap){
			resizedBitmap.Recycle ();
			resizedBitmap.Dispose ();
		}





	}
}

