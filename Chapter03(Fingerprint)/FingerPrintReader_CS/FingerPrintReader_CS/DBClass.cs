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
// Database routines
// -----------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.OleDb;
using GrFingerXLib;
using System.Runtime.InteropServices;

// the template class
public class TTemplate
{
	// Template data.
	public System.Array _tpt;
	// Template size
	public int _size;

	public TTemplate(){
		// Create a byte buffer for the template
	   _tpt = new byte[(int)GRConstants.GR_MAX_SIZE_TEMPLATE];
	   _size = 0;
	}
}

// the database class
public class DBClass{
	
	// the connection object
	private OleDbConnection _connection;

	// temporary template for retrieving data from DB
	private TTemplate tptBlob;
	
	// the database we'll be connecting to
	public readonly string CONNECTION_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=GrFingerSample.mdb";
	
	public DBClass(){
	}

	// Open connection
	public bool openDB()
	{
		_connection = new OleDbConnection();
		_connection.ConnectionString = CONNECTION_STRING;
		try{
			_connection.Open();
		}
		catch{
			return false;
		}
		tptBlob = new TTemplate();
		return true;
	}//END

	// Close conection
	public bool closeDB()
	{
		if(_connection.State != ConnectionState.Closed)
		  _connection.Close();  
		return true;
	}

	// Clear database
	public bool clearDB()
	{
		OleDbCommand cmdClear = null;
		cmdClear = new OleDbCommand("DELETE FROM enroll", _connection);

		// run "clear" query
		if(_connection.State == ConnectionState.Open)
			cmdClear.ExecuteNonQuery();
		
		return true;
	}


	// Add template to database. Returns added template ID.
	public bool addTemplate(TTemplate tpt,ref int id) 
	{
		OleDbCommand cmdInsert = null;
		OleDbParameter dbParamInsert = null; 
		OleDbCommand cmdSelect =  null;

		try{
			// Create SQL command containing ? parameter for BLOB.
			cmdInsert = new OleDbCommand("INSERT INTO enroll(template) values(?) ", _connection);
			// Create parameter for ? contained in the SQL statement.
			System.Byte [] temp = new System.Byte[tpt._size + 1];
			System.Array.Copy(tpt._tpt, 0, temp, 0, tpt._size);

			dbParamInsert = new OleDbParameter("@template", OleDbType.VarBinary, tpt._size, 
							ParameterDirection.Input, false, 0, 0,"ID", 
							DataRowVersion.Current, temp);
			cmdInsert.Parameters.Add(dbParamInsert);

			//execute query
			if(_connection.State == ConnectionState.Open)
				cmdInsert.ExecuteNonQuery();
		}
		catch{
			return false;
		}

		try{
			// Create SQL command containing ? parameter for BLOB.
			cmdSelect = new OleDbCommand("SELECT top 1 ID FROM enroll ORDER BY ID DESC", _connection);
		    
			id = System.Convert.ToInt32(cmdSelect.ExecuteScalar());
		}
		catch {
			return false;  
		}

		return true;
	}

	// Returns an OleDbDataReader with all enrolled templates from database.
	public OleDbDataReader  getTemplates()
	{
		OleDbCommand  cmdGetTemplates;
		OleDbDataReader  rs;

		//setting up command 
		cmdGetTemplates =  new OleDbCommand("SELECT * FROM enroll", _connection);
		rs = cmdGetTemplates.ExecuteReader();

		return rs;
	}

	
	// Returns template with the supplied ID.
	public TTemplate getTemplate(int id)
	{
		OleDbCommand cmd = null;
		OleDbDataReader dr = null;
		tptBlob._size = 0;
		try
		{
			cmd =  new OleDbCommand(System.String.Concat("SELECT * FROM enroll WHERE ID = ", System.Convert.ToString((int)id)), _connection);
			dr = cmd.ExecuteReader();
			// Get query response
			dr.Read();
			getTemplate(dr);
			dr.Close();
		}
		catch{
			dr.Close();
		}
		return tptBlob;
	}

	// Return template data from an OleDbDataReader
	public TTemplate getTemplate(OleDbDataReader rs)
	{
		long readedBytes; 
		tptBlob._size = 0;
		// alloc space
		System.Byte[] temp = new System.Byte[
			(int)GRConstants.GR_MAX_SIZE_TEMPLATE];
		// get bytes
		readedBytes = rs.GetBytes(1, 0, temp, 0,temp.Length);
		// copy to structure
		System.Array.Copy(temp, 0, tptBlob._tpt,0,(int)readedBytes);
		// set real size
		tptBlob._size = (int)readedBytes;

		return tptBlob;
	}

	// Return enrollment ID from an OleDbDataReader
	public int getId(OleDbDataReader rs)
	{
		return rs.GetInt32(0);
	}
}
