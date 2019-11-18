using System;
using System.Collections;
using System.Data;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.RuntimePublic.Db;
using FileTypeChecker;

namespace OutSystems.NssFileTrID {

	public class CssFileTrID: IssFileTrID {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ssFileBinary"></param>
        /// <param name="ssExtension"></param>
        public void MssVerifyFile(byte[] ssFileBinary, out string ssExtension)
        {
            FileTypeTeller Teller = new FileTypeTeller();
            ssExtension = Teller.GetFileExtension(ssFileBinary).GetExtensionsAsString();
		} // MssVerifyFile

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ssFileBinary"></param>
        /// <param name="ssExtensionGuess"></param>
        /// <param name="ssIsAsExpected"></param>
        public void MssVerifyIsExtension(byte[] ssFileBinary, string ssExtensionGuess, out bool ssIsAsExpected)
        {
            FileTypeTeller Teller = new FileTypeTeller();
            ssIsAsExpected = Teller.IsFileExtensionCorrect(ssExtensionGuess, ssFileBinary);
		} // MssVerifyIsExtension

	} // CssFileTrID

} // OutSystems.NssFileTrID

