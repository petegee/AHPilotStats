using System;
using System.Collections.Generic;
using System.Text;
using Saxon.Api;
using System.Xml;

namespace My2Cents.HTC.PilotScoreSvc.Utilities
{
    class XSLT2Transformer
    {
        XmlDocument _docToTransform = null;
        XmlTextReader _xsltDocReader = null;

        public XSLT2Transformer(XmlDocument docToTransform, XmlTextReader xsltDocReader)
        {
            _docToTransform = docToTransform;
            _xsltDocReader = xsltDocReader;
        }

        public XmlDocument DoTransform()
        {
            Processor processor = new Processor();

            XdmNode input = processor.NewDocumentBuilder().Wrap(_docToTransform);

            // Create a compiler
            XsltCompiler compiler = processor.NewXsltCompiler();

            DocumentBuilder builder = processor.NewDocumentBuilder();
            XdmNode xsltSheetNode = builder.Build(_xsltDocReader);

            // Compile the stylesheet
            XsltTransformer transformer = compiler.Compile(xsltSheetNode).Load();

            // Run the transformation
            transformer.InitialContextNode = input;
            DomDestination result = new DomDestination();
            transformer.Run(result);

            return result.XmlDocument;
        }

    }
}
