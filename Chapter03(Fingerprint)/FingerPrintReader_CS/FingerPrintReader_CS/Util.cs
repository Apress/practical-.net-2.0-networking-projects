/*
 -------------------------------------------------------------------------------
 GrFinger Sample
 (c) 2005 Griaule Tecnologia Ltda.
 http://www.griaule.com
 -------------------------------------------------------------------------------

 This sample is provided with "GrFinger Fingerprint Recognition Library" and
 can't run without it. It's provided just as an example of using GrFinger
 Fingerprint Recognition Library and should not be used as basis for any
 commercial product.

 Griaule Tecnologia makes no representations concerning either the merchantability
 of this software or the suitability of this sample for any particular purpose.

 THIS SAMPLE IS PROVIDED BY THE AUTHOR "AS IS" AND ANY EXPRESS OR
 IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 IN NO EVENT SHALL GRIAULE BE LIABLE FOR ANY DIRECT, INDIRECT,
 INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

 You can download the free version of GrFinger directly from Griaule website.
                                                                   
 These notices must be retained in any copies of any part of this
 documentation and/or sample.

 -------------------------------------------------------------------------------
*/

// -----------------------------------------------------------------------------------
// Support and fingerprint management routines
// -----------------------------------------------------------------------------------

using GrFingerXLib;
using System;
using System.Drawing;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// Raw image data type.
public struct TRawImage 
{
	// Image data.
	public object img;
	// Image width.
	public int width;
	// Image height.
	public int height;
	// Image resolution.
	public int Res;
};

public class Util
{

	// Some constants to make our code cleaner
	public const int ERR_CANT_OPEN_BD = -999;
	public const int ERR_INVALID_ID = -998;
	public const int ERR_INVALID_TEMPLATE = -997;

	// -----------------------------------------------------------------------------------
	// Support functions
	// -----------------------------------------------------------------------------------

	// This class creates an Util class with some functions
	// to help us to develop our GrFinger Application
	public Util(ListBox lbLog, PictureBox pbPic, 
		Button btEnroll, Button btnExtract, Button btIdentify, Button btVerify,
		CheckBox cbAutoExtract, CheckBox  cbAutoIdentify)
	{
		_lbLog = lbLog;
		_pbPic = pbPic;
		_btEnroll = btEnroll;
		_btExtract = btnExtract;
		_btIdentify = btIdentify;
		_btVerify = btVerify;
		_cbAutoExtract = cbAutoExtract;
		_cbAutoIdentify = cbAutoIdentify;
		_DB = null;
		_tpt = null;
	}

	~Util()
	{
	}

	//  Write a message in log box.
	public void WriteLog(String msg) 
	{
		_lbLog.Items.Add(msg);
		_lbLog.SelectedIndex = _lbLog.Items.Count - 1;
		_lbLog.ClearSelected();
	}

	// Write and describe an error.
	public void WriteError(GrFingerXLib.GRConstants errorCode) 
	{
		switch ((int)errorCode) 
		{
			case (int)GRConstants.GR_ERROR_INITIALIZE_FAIL:
				WriteLog("Fail to Initialize GrFingerX. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_NOT_INITIALIZED:
				WriteLog("The GrFingerX Library is not initialized. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_FAIL_LICENSE_READ:
				WriteLog("License not found. See manual for troubleshooting. (Error:" + errorCode + ")");
				MessageBox.Show("License not found. See manual for troubleshooting.");
				return;
			case (int)GRConstants.GR_ERROR_NO_VALID_LICENSE:
				WriteLog("The license is not valid. See manual for troubleshooting. (Error:" + errorCode + ")");
				MessageBox.Show("The license is not valid. See manual for troubleshooting.");
				return;
			case (int)GRConstants.GR_ERROR_NULL_ARGUMENT:
				WriteLog("The parameter have a null value. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_FAIL:
				WriteLog("Fail to create a GDI object. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_ALLOC:
				WriteLog("Fail to create a context. Cannot allocate memory. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_PARAMETERS:
				WriteLog("One or more parameters are out of bound. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_WRONG_USE:
				WriteLog("This function cannot be called at this time. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_EXTRACT:
				WriteLog("Template Extraction failed. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_SIZE_OFF_RANGE:
				WriteLog("Image is too larger or too short.  (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_RES_OFF_RANGE:
				WriteLog("Image have too low or too high resolution. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_CONTEXT_NOT_CREATED:
				WriteLog("The Context could not be created. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_INVALID_CONTEXT:
				WriteLog("The Context does not exist. (Error:" + errorCode + ")");
				return;

				// Capture error codes

			case (int)GRConstants.GR_ERROR_CONNECT_SENSOR:
				WriteLog("Error while connection to sensor. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_CAPTURING:
				WriteLog("Error while capturing from sensor. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_CANCEL_CAPTURING:
				WriteLog("Error while stop capturing from sensor. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_INVALID_ID_SENSOR:
				WriteLog("The idSensor is invalid. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_SENSOR_NOT_CAPTURING:
				WriteLog("The sensor is not capturing. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_INVALID_EXT:
				WriteLog("The File have a unknown extension. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_INVALID_FILENAME:
				WriteLog("The filename is invalid. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_INVALID_FILETYPE:
				WriteLog("The file type is invalid. (Error:" + errorCode + ")");
				return;
			case (int)GRConstants.GR_ERROR_SENSOR:
				WriteLog("The sensor raise an error. (Error:" + errorCode + ")");
				return;

				// Our error codes
			case ERR_INVALID_TEMPLATE:
				WriteLog("Invalid Template. (Error:"+errorCode+")");
				return;
			case ERR_INVALID_ID:
				WriteLog("Invalid ID. (Error:"+errorCode+")");
				return;
			case ERR_CANT_OPEN_BD:
				WriteLog("Unable to connect to DataBase. (Error:"+errorCode+")");
				return;

			default:
				WriteLog("Error:" + errorCode);
				return;
		}
	}

  // Check if we have a valid template
	private bool TemplateIsValid() {
		// Check the template size and data
		return ((_tpt._size > 0) && (_tpt._tpt != null));
	}

	// -----------------------------------------------------------------------------------
	// Main functions for fingerprint recognition management
	// -----------------------------------------------------------------------------------

	// Initializes GrFinger ActiveX and all necessary utilities.
	public int InitializeGrFinger(AxGrFingerXLib.AxGrFingerXCtrl grfingerx) 
	{
		GRConstants result;
	  
		_grfingerx = grfingerx;
		//Check DataBase Class.
		if (_DB == null)
			_DB = new DBClass();
		//Open DataBase
		if(_DB.openDB()==false) 
		{
			return ERR_CANT_OPEN_BD;
		}

		//Create a new Template
		if (_tpt == null)
			_tpt = new TTemplate();

		//Create a new raw image
		_raw = new TRawImage();

		//Initialize library
		result = (GRConstants)_grfingerx.Initialize();
		if (result < 0) return (int)result;
		return (int)_grfingerx.CapInitialize();
	}

	//  Finalizes library and close DB.
	public void FinalizeUtil() {
		// finalize library
		_grfingerx.Finalize();
		_grfingerx.CapFinalize();
		// close DB
		_DB.closeDB();
	    _raw.img = null;
		_tpt = null;
		_DB = null;
	}

	// Display fingerprint image on screen
	public void PrintBiometricDisplay(bool isBiometric, GrFingerXLib.GRConstants contextId) 
	{
		// handle to finger image
		System.Drawing.Image handle = null;
		// screen HDC
		IntPtr hdc = GetDC(System.IntPtr.Zero); 

		if (isBiometric) {
		  // get image with biometric info
			_grfingerx.BiometricDisplay(ref _tpt._tpt,
				ref _raw.img,_raw.width,_raw.height,_raw.Res,hdc.ToInt32(),
				ref handle,(int)contextId);
		} else {
		  // get raw image
			_grfingerx.CapRawImageToHandle(ref _raw.img,_raw.width,
				_raw.height, hdc.ToInt32(), ref handle);
		}
		
		// draw image on picture box
		if (handle != null) 
		{
			_pbPic.Image = handle;
			_pbPic.Update();
		}

		// release screen HDC
		ReleaseDC(System.IntPtr.Zero,hdc);
	}

	// Add a fingerprint template to database
	public int Enroll() 
	{
		int id = 0;
		// Checks if template is valid.
		if (TemplateIsValid())
		{
			// Adds template to database and returns template ID.
			_DB.addTemplate(_tpt, ref id);
			return id;
		} 
		else 
		{
			return -1;
		}
	}

	// Extract a fingerprint template from current image
	public int ExtractTemplate() 
	{
		int result;

		// set current buffer size for the extract template
		_tpt._size = (int)GRConstants.GR_MAX_SIZE_TEMPLATE;
		result = (int)_grfingerx.Extract( 
			ref _raw.img, _raw.width, _raw.height, _raw.Res,
			ref _tpt._tpt,ref _tpt._size,
			(int)GRConstants.GR_DEFAULT_CONTEXT);
		// if error, set template size to 0
		if (result < 0)
		{
			// Result < 0 => extraction problem
			_tpt._size = 0;
		}
		return result;
	}

	// Identify current fingerprint on our database
	public int Identify(ref int score) {
		GRConstants result;
		int id;
		OleDbDataReader rs;
		TTemplate tptRef;

		// Checking if template is valid.
		if(!TemplateIsValid()) return ERR_INVALID_TEMPLATE;
		// Starting identification process and supplying query template.
		result = (GRConstants) _grfingerx.IdentifyPrepare(ref _tpt._tpt,
			(int)GRConstants.GR_DEFAULT_CONTEXT);
		// error?
		if (result < 0) return (int)result;
		// Getting enrolled templates from database.
		rs = _DB.getTemplates();
		while(rs.Read())
		{
			// Getting current template from recordset.
			tptRef = _DB.getTemplate(rs);

			// Comparing current template.
			result = (GRConstants) _grfingerx.Identify(ref tptRef._tpt, ref score,(int)GRConstants.GR_DEFAULT_CONTEXT);
			    
			// Checking if query template and the reference template match.
			if(result == GRConstants.GR_MATCH)
			{
				id = _DB.getId(rs);
				rs.Close();
				return id;
			}
			else if (result < 0)
			{
				rs.Close();
				return (int)result;
			}
		}

		// Closing recordset.
		rs.Close();
		return (int)GRConstants.GR_NOT_MATCH;
	}

	// Check current fingerprint against another one in our database
	public int Verify(int id, ref int score) {
		TTemplate tptRef;

		// Checking if template is valid.
		if(!TemplateIsValid()) return ERR_INVALID_TEMPLATE;

		// Getting template with the supplied ID from database.
		tptRef = _DB.getTemplate(id);

		// Checking if ID was found.
		if ((tptRef._tpt==null) || (tptRef._size == 0))
		{
			return ERR_INVALID_ID;
		}

		// Comparing templates.
		return (int) _grfingerx.Verify(ref _tpt._tpt,ref tptRef._tpt,
			ref score, (int)GRConstants.GR_DEFAULT_CONTEXT);
	}

	// Show GrFinger version and type
	public void MessageVersion() 
	{
		byte majorVersion=0,minorVersion=0;
		GRConstants result;
		string vStr = "";

		result = (GRConstants)_grfingerx.GetGrFingerVersion(ref majorVersion,
			ref minorVersion);
		if(result == GRConstants.GRFINGER_FULL)
			vStr = "FULL";
		else if(result == GRConstants.GRFINGER_LIGHT)
			vStr = "LIGHT";
		else if(result == GRConstants.GRFINGER_FREE)
			vStr = "FREE";

		MessageBox.Show("The GrFinger DLL version is " +
			majorVersion + "." + minorVersion + ". \n" +
			"The license type is '" + vStr + "'.","GrFinger Version");
	}

	//Importing necessary HDC functions
	[DllImport("user32.dll",EntryPoint="GetDC")]
	public static extern IntPtr GetDC(IntPtr ptr);

	[DllImport("user32.dll",EntryPoint="ReleaseDC")]
	public static extern IntPtr ReleaseDC(IntPtr hWnd,IntPtr hDc);

	// Database class.
	public DBClass _DB;
	// The last acquired image.
	public TRawImage _raw;
	// Reference to main form Image.
	public PictureBox _pbPic;

	// The template extracted from last acquired image.
	private TTemplate _tpt;
	// Reference to main form log.
	private ListBox _lbLog;
  	//references Main form Auto Extract Check Box
	private CheckBox _cbAutoExtract;
  	//references Main form Auto Identify Check Box
	private CheckBox _cbAutoIdentify;
	//references Main form enroll button
	Button _btEnroll;
	//references Main form extract button
	Button _btExtract;
	//references Main form identify button
	Button _btIdentify;
	//references Main form verify button
	Button _btVerify;
	// GrFingerX component
	AxGrFingerXLib.AxGrFingerXCtrl _grfingerx;
};
