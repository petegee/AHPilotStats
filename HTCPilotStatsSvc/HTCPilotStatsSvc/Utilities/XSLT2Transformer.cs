using System.Xml;
using Saxon.Api;

namespace My2Cents.HTC.PilotScoreSvc.Utilities
{
    internal class XSLT2Transformer
    {
        private readonly XmlDocument _docToTransform;
        private readonly XmlTextReader _xsltDocReader;

        public XSLT2Transformer(XmlDocument docToTransform, XmlTextReader xsltDocReader)
        {
            _docToTransform = docToTransform;
            _xsltDocReader = xsltDocReader;
        }

        public XmlDocument DoTransform()
        {
            var processor = new Processor();

            var input = processor.NewDocumentBuilder().Wrap(_docToTransform);

            // Create a compiler
            var compiler = processor.NewXsltCompiler();

            var builder = processor.NewDocumentBuilder();
            var xsltSheetNode = builder.Build(_xsltDocReader);

            // Compile the stylesheet
            var transformer = compiler.Compile(xsltSheetNode).Load();

            // Run the transformation
            transformer.InitialContextNode = input;
            var result = new DomDestination();
            transformer.Run(result);

            return result.XmlDocument;
        }
    }
}