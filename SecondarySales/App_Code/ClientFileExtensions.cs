using Agfa.Common;
using System.Web;

public static class ClientFileExtensions
{

    public static void SendFileToBrowser(this IClientFileData FileToSend, string HTTPHandlerAbsolutePath)
    {
        if (FileToSend == null || string.IsNullOrEmpty(HTTPHandlerAbsolutePath))
            return;
        FileToSend.IsAttachment = true;
        HttpContext.Current.Session.Add("ClientFileData", (object)FileToSend);
        HttpContext.Current.Response.Redirect(HTTPHandlerAbsolutePath);
    }

}