using QRCoder;

namespace GroupProject.Services
{
    public class QRCodeService
    {
        // Instantiate QRCode Generator
        private readonly QRCodeGenerator _generator;

        // Constructor for the service
        public QRCodeService(QRCodeGenerator generator)
        {
            _generator = generator;
        }
        
        // Default code from the tutorial. Used to create the QR Code for MFA.
        public string GetQRCodeAsBase64(string textToEncode)
        {
            QRCodeData qrCodeData = _generator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new Base64QRCode(qrCodeData);

            return qrCode.GetGraphic(4);
        }
    }
}