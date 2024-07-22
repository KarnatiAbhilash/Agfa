<%@ WebHandler Language="C#" Class="SendFileToBrowser" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.SessionState;
using System.Web.Services.Protocols;
using System.IO;
using Agfa.Common;
using Agfa.Common.CollectionsExtensions;

    
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [SoapDocumentService]
    [Serializable()]
public class SendFileToBrowser : IHttpHandler, IRequiresSessionState {

    private void CopyStream(Stream source, Stream target)
    {
        const int bufSize = 0x1000;
        byte[] buf = new byte[bufSize];
        int bytesRead = 0;
        while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
            target.Write(buf, 0, bytesRead);
    }// end:


    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public virtual void ProcessRequest(HttpContext context)
    {
        HttpSessionState session = context.Session;
        if (session != null)
        {
            IClientFileData FileData = (IClientFileData)session["ClientFileData"];

            if (FileData != null)
            {
                Send(context, FileData);
                context.Session.Remove("ClientFileData");
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
    /// </summary>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
    ///   </returns>
    public virtual bool IsReusable
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// Sends a specific file to the browser
    /// </summary>
    /// <param name="context">the current HTTP Context</param>
    /// <param name="FileData">The file data.</param>
    [WebMethod]
    [SoapDocumentMethod]
    [ScriptMethod]
    public virtual void Send(HttpContext context, IClientFileData FileData)
    {
        Boolean bContinue = false;
        if (FileData != null)
        {
            if (FileData.IsValid && FileData.IsDataValid)
            {
                bContinue = true;
            }
            else
            {
                bContinue = false;
            }
        }
        else
        {
            bContinue = false;
        }

        if (bContinue)
        {
            context.Response.ClearHeaders();
            context.Response.ClearContent();
            context.Response.Clear();
            if (FileData.IsAttachment)
            {
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileData.FileName + String.Format(";modification-date=\"{0}\";", DateTime.Now));
            }
            else
            {
                context.Response.AddHeader("Content-Disposition", "inline; filename=" + FileData.FileName+ String.Format(";modification-date=\"{0}\";", DateTime.Now));

            }
            // context.Response.Write(@"<style> .text {mso-number-format:\@; } </style>");
            // context.Response.Write(@"<style> TD {mso-number-format:\@; } </style>");
            context.Response.ContentType =  FileData.MIMEType;
            context.Response.Buffer = true;

            if (FileData.GetType() == typeof(ClientBinaryFile))
            {
                context.Response.BinaryWrite(((ClientBinaryFile)FileData).FileData);
                context.Response.Flush();
            }
            else if (FileData.GetType() == typeof(ClientTextFile))
            {
                context.Response.Write(((ClientTextFile)FileData).FileContents);
                context.Response.Flush();
            }
            else if (FileData.GetType() == typeof(ClientFileStream))
            {
                using (ClientFileStream cfs = ((ClientFileStream)FileData))
                {
                    context.Response.WriteFile(cfs.StreamFileName);
                    context.Response.Flush();
                }
            }
            context.Response.End();

        }
    }


}