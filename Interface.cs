using System;
using System.Collections;
using System.Data;
using OutSystems.HubEdition.RuntimePlatform;

namespace OutSystems.NssFileTrID {

	public interface IssFileTrID {

		/// <summary>
		/// Returns the effective File Extension for the given File, by carving its binary contents.
		/// </summary>
		/// <param name="ssFileBinary">Examples:
		/// .pdf
		/// .docx
		/// .exe</param>
		/// <param name="ssExtension"></param>
		void MssVerifyFile(byte[] ssFileBinary, out string ssExtension);

		/// <summary>
		/// Returns if the given file matches its path extension, by carving the binary contents of the file.
		/// </summary>
		/// <param name="ssFileBinary"></param>
		/// <param name="ssExtensionGuess">Guess of the extension of the file:
		/// Examples:
		/// 
		/// .bmp
		/// .docx
		/// .pdf</param>
		/// <param name="ssIsAsExpected"></param>
		void MssVerifyIsExtension(byte[] ssFileBinary, string ssExtensionGuess, out bool ssIsAsExpected);

	} // IssFileTrID

} // OutSystems.NssFileTrID
